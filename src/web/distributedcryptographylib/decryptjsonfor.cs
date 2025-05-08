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
   public class decryptjsonfor : GXProcedure
   {
      public decryptjsonfor( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptjsonfor( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptedKey ,
                           string aP2_privateKey ,
                           out string aP3_clearText ,
                           out string aP4_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV15privateKey = aP2_privateKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP3_clearText=this.AV12clearText;
         aP4_error=this.AV9error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptedKey ,
                                string aP2_privateKey ,
                                out string aP3_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptedKey, aP2_privateKey, out aP3_clearText, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptedKey ,
                                 string aP2_privateKey ,
                                 out string aP3_clearText ,
                                 out string aP4_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV15privateKey = aP2_privateKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP3_clearText=this.AV12clearText;
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV15privateKey,  AV14encryptedKey, out  AV8encryptionKey, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV16DecryptionResult = AV17EncryptionService.decrypt(AV13encryptedText, AV8encryptionKey);
            if ( AV16DecryptionResult.gxTpr_Success )
            {
               AV12clearText = AV16DecryptionResult.gxTpr_Decryptedtext;
            }
            else
            {
               AV9error = AV16DecryptionResult.gxTpr_Errormessage;
            }
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
         AV12clearText = "";
         AV9error = "";
         GXt_char1 = "";
         AV8encryptionKey = "";
         AV16DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV17EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         /* GeneXus formulas. */
      }

      private string AV14encryptedKey ;
      private string AV15privateKey ;
      private string AV9error ;
      private string GXt_char1 ;
      private string AV8encryptionKey ;
      private string AV13encryptedText ;
      private string AV12clearText ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV16DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV17EncryptionService ;
      private string aP3_clearText ;
      private string aP4_error ;
   }

}
