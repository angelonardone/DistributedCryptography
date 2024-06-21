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
   public class schnorrsignmsg : GXProcedure
   {
      public schnorrsignmsg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public schnorrsignmsg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_privateKey ,
                           string aP1_message ,
                           out string aP2_signature ,
                           out string aP3_error )
      {
         this.AV9privateKey = aP0_privateKey;
         this.AV11message = aP1_message;
         this.AV12signature = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_signature=this.AV12signature;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_privateKey ,
                                string aP1_message ,
                                out string aP2_signature )
      {
         execute(aP0_privateKey, aP1_message, out aP2_signature, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_privateKey ,
                                 string aP1_message ,
                                 out string aP2_signature ,
                                 out string aP3_error )
      {
         this.AV9privateKey = aP0_privateKey;
         this.AV11message = aP1_message;
         this.AV12signature = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_signature=this.AV12signature;
         aP3_error=this.AV8error;
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
          string hexPrivateKey = AV9privateKey;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
         /* User Code */
          NBitcoin.Key privateKey = new NBitcoin.Key(bytes);
         /* User Code */
          	byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(AV11message);
         /* User Code */
          	NBitcoin.uint256 msgHash = NBitcoin.Crypto.Hashes.DoubleSHA256(msgBytes);
         /* User Code */
          	var val = privateKey.SignTaprootKeySpend(msgHash).ToString();
         /* User Code */
          AV10val = val;
         AV12signature = AV10val;
         /* User Code */
         	}
         /* User Code */
             catch (Exception ex)
         /* User Code */
             {
         /* User Code */
              AV8error = ex.Message.ToString();
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
         AV12signature = "";
         AV8error = "";
         AV10val = "";
         /* GeneXus formulas. */
      }

      private string AV9privateKey ;
      private string AV12signature ;
      private string AV8error ;
      private string AV10val ;
      private string AV11message ;
      private string aP2_signature ;
      private string aP3_error ;
   }

}
