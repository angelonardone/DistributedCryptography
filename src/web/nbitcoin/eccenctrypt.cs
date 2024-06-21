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
   public class eccenctrypt : GXProcedure
   {
      public eccenctrypt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public eccenctrypt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_pubKey ,
                           string aP1_clearText ,
                           out string aP2_cypherText ,
                           out string aP3_error )
      {
         this.AV11pubKey = aP0_pubKey;
         this.AV10clearText = aP1_clearText;
         this.AV8cypherText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_cypherText=this.AV8cypherText;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_pubKey ,
                                string aP1_clearText ,
                                out string aP2_cypherText )
      {
         execute(aP0_pubKey, aP1_clearText, out aP2_cypherText, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_pubKey ,
                                 string aP1_clearText ,
                                 out string aP2_cypherText ,
                                 out string aP3_error )
      {
         this.AV11pubKey = aP0_pubKey;
         this.AV10clearText = aP1_clearText;
         this.AV8cypherText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_cypherText=this.AV8cypherText;
         aP3_error=this.AV9error;
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
          string hexPubKey = AV11pubKey;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPubKey);
         /* User Code */
          NBitcoin.PubKey publicKey = new NBitcoin.PubKey(bytes);
         /* User Code */
          string val = publicKey.Encrypt(AV10clearText);
         /* User Code */
          AV12val = val;
         AV8cypherText = AV12val;
         /* User Code */
         	}
         /* User Code */
             catch (Exception ex)
         /* User Code */
             {
         /* User Code */
              AV9error = ex.Message.ToString();
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
         AV8cypherText = "";
         AV9error = "";
         AV12val = "";
         /* GeneXus formulas. */
      }

      private string AV11pubKey ;
      private string AV9error ;
      private string AV12val ;
      private string AV10clearText ;
      private string AV8cypherText ;
      private string aP2_cypherText ;
      private string aP3_error ;
   }

}
