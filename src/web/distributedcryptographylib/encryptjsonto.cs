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
   public class encryptjsonto : GXProcedure
   {
      public encryptjsonto( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptjsonto( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_clearText ,
                           string aP1_pubKey ,
                           out string aP2_encryptedKey ,
                           out string aP3_encryptedText ,
                           out string aP4_error )
      {
         this.AV12clearText = aP0_clearText;
         this.AV15pubKey = aP1_pubKey;
         this.AV14encryptedKey = "" ;
         this.AV13encryptedText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptedKey=this.AV14encryptedKey;
         aP3_encryptedText=this.AV13encryptedText;
         aP4_error=this.AV9error;
      }

      public string executeUdp( string aP0_clearText ,
                                string aP1_pubKey ,
                                out string aP2_encryptedKey ,
                                out string aP3_encryptedText )
      {
         execute(aP0_clearText, aP1_pubKey, out aP2_encryptedKey, out aP3_encryptedText, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_clearText ,
                                 string aP1_pubKey ,
                                 out string aP2_encryptedKey ,
                                 out string aP3_encryptedText ,
                                 out string aP4_error )
      {
         this.AV12clearText = aP0_clearText;
         this.AV15pubKey = aP1_pubKey;
         this.AV14encryptedKey = "" ;
         this.AV13encryptedText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_encryptedKey=this.AV14encryptedKey;
         aP3_encryptedText=this.AV13encryptedText;
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16EncryptionResult = AV17EncryptionService.encrypt(AV12clearText, "");
         AV8encryptionKey = AV16EncryptionResult.gxTpr_Generatedkey;
         if ( AV16EncryptionResult.gxTpr_Success )
         {
            AV13encryptedText = AV16EncryptionResult.gxTpr_Ciphertext;
            GXt_char1 = AV9error;
            new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  AV15pubKey,  AV8encryptionKey, out  AV14encryptedKey, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
            }
         }
         else
         {
            AV9error = AV16EncryptionResult.gxTpr_Errormessage;
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
         AV14encryptedKey = "";
         AV13encryptedText = "";
         AV9error = "";
         AV16EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV17EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV8encryptionKey = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV15pubKey ;
      private string AV14encryptedKey ;
      private string AV9error ;
      private string AV8encryptionKey ;
      private string GXt_char1 ;
      private string AV12clearText ;
      private string AV13encryptedText ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV16EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV17EncryptionService ;
      private string aP2_encryptedKey ;
      private string aP3_encryptedText ;
      private string aP4_error ;
   }

}
