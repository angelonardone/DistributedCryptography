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
   public class eccdecrypt : GXProcedure
   {
      public eccdecrypt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public eccdecrypt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_privateKey ,
                           string aP1_cypherText ,
                           out string aP2_clearText ,
                           out string aP3_error )
      {
         this.AV11privateKey = aP0_privateKey;
         this.AV9cypherText = aP1_cypherText;
         this.AV8clearText = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV10error;
      }

      public string executeUdp( string aP0_privateKey ,
                                string aP1_cypherText ,
                                out string aP2_clearText )
      {
         execute(aP0_privateKey, aP1_cypherText, out aP2_clearText, out aP3_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_privateKey ,
                                 string aP1_cypherText ,
                                 out string aP2_clearText ,
                                 out string aP3_error )
      {
         this.AV11privateKey = aP0_privateKey;
         this.AV9cypherText = aP1_cypherText;
         this.AV8clearText = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV10error;
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
          string hexPrivateKey = AV11privateKey;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
         /* User Code */
          NBitcoin.Key privateKey = new NBitcoin.Key(bytes);
         /* User Code */
          string val = privateKey.Decrypt(AV9cypherText);
         /* User Code */
          AV14val = val;
         AV8clearText = AV14val;
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
         AV8clearText = "";
         AV10error = "";
         AV14val = "";
         /* GeneXus formulas. */
      }

      private string AV11privateKey ;
      private string AV10error ;
      private string AV14val ;
      private string AV9cypherText ;
      private string AV8clearText ;
      private string aP2_clearText ;
      private string aP3_error ;
   }

}
