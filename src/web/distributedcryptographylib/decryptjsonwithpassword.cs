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
namespace GeneXus.Programs.distributedcryptographylib {
   public class decryptjsonwithpassword : GXProcedure
   {
      public decryptjsonwithpassword( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptjsonwithpassword( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptionKey ,
                           out string aP2_clearText ,
                           out string aP3_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV11encryptionKey = aP1_encryptionKey;
         this.AV8clearText = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV12error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptionKey ,
                                out string aP2_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptionKey, out aP2_clearText, out aP3_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptionKey ,
                                 out string aP2_clearText ,
                                 out string aP3_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV11encryptionKey = aP1_encryptionKey;
         this.AV8clearText = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15DecryptionResult = AV16EncryptionService.decrypt(AV10encryptedText, AV11encryptionKey);
         if ( AV15DecryptionResult.gxTpr_Success )
         {
            AV8clearText = AV15DecryptionResult.gxTpr_Decryptedtext;
         }
         else
         {
            AV12error = AV15DecryptionResult.gxTpr_Errormessage;
         }
         cleanup();
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
         AV12error = "";
         AV15DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV16EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         /* GeneXus formulas. */
      }

      private string AV11encryptionKey ;
      private string AV12error ;
      private string AV10encryptedText ;
      private string AV8clearText ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV15DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV16EncryptionService ;
      private string aP2_clearText ;
      private string aP3_error ;
   }

}
