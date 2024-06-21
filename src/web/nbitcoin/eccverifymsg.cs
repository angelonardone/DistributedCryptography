using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.nbitcoin {
   public class eccverifymsg : GXProcedure
   {
      public eccverifymsg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public eccverifymsg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_pubKey ,
                           string aP1_message ,
                           string aP2_signature ,
                           out bool aP3_verified ,
                           out string aP4_error )
      {
         this.AV12pubKey = aP0_pubKey;
         this.AV11message = aP1_message;
         this.AV13signature = aP2_signature;
         this.AV15verified = false ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP3_verified=this.AV15verified;
         aP4_error=this.AV10error;
      }

      public string executeUdp( string aP0_pubKey ,
                                string aP1_message ,
                                string aP2_signature ,
                                out bool aP3_verified )
      {
         execute(aP0_pubKey, aP1_message, aP2_signature, out aP3_verified, out aP4_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_pubKey ,
                                 string aP1_message ,
                                 string aP2_signature ,
                                 out bool aP3_verified ,
                                 out string aP4_error )
      {
         this.AV12pubKey = aP0_pubKey;
         this.AV11message = aP1_message;
         this.AV13signature = aP2_signature;
         this.AV15verified = false ;
         this.AV10error = "" ;
         SubmitImpl();
         aP3_verified=this.AV15verified;
         aP4_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          string hexPubKey = AV12pubKey;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPubKey);
         /* User Code */
          NBitcoin.PubKey publicKey = new NBitcoin.PubKey(bytes);
         /* User Code */
          	byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(AV11message);
         /* User Code */
          	NBitcoin.uint256 msgHash = NBitcoin.Crypto.Hashes.DoubleSHA256(msgBytes);
         /* User Code */
          NBitcoin.Crypto.ECDSASignature signature;
         /* User Code */
          if (NBitcoin.Crypto.ECDSASignature.TryParseFromCompact(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV13signature), out signature))
         /* User Code */
          {
         /* User Code */
          bool val = publicKey.Verify(msgHash, signature);
         /* User Code */
          AV15verified = val;
         /* User Code */
          }
         /* User Code */
          else
         /* User Code */
          {
         AV10error = "The signature is not properly formed";
         /* User Code */
          }
         /* User Code */
         	}
         /* User Code */
             catch (Exception ex)
         /* User Code */
             {
         /* User Code */
              AV10error = ex.Message.ToString();
         /* User Code */
             }
         this.cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV10error = "";
         /* GeneXus formulas. */
      }

      private string AV12pubKey ;
      private string AV13signature ;
      private string AV10error ;
      private bool AV15verified ;
      private string AV11message ;
      private bool aP3_verified ;
      private string aP4_error ;
   }

}
