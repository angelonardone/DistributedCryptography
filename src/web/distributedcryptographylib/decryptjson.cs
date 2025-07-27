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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.distributedcryptographylib {
   public class decryptjson : GXProcedure
   {
      public decryptjson( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptjson( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptedKey ,
                           out string aP2_clearText ,
                           out string aP3_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_clearText=this.AV12clearText;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptedKey ,
                                out string aP2_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptedKey, out aP2_clearText, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptedKey ,
                                 out string aP2_clearText ,
                                 out string aP3_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_clearText=this.AV12clearText;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtKeyInfo1 = AV15keyInfo;
         new GeneXus.Programs.wallet.getdfaultjasonkey(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV15keyInfo = GXt_SdtKeyInfo1;
         GXt_char2 = AV9error;
         new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV15keyInfo.gxTpr_Privatekey,  AV14encryptedKey, out  AV8encryptionKey, out  GXt_char2) ;
         AV9error = GXt_char2;
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
         AV15keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_char2 = "";
         AV8encryptionKey = "";
         AV16DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV17EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         /* GeneXus formulas. */
      }

      private string AV14encryptedKey ;
      private string AV9error ;
      private string GXt_char2 ;
      private string AV8encryptionKey ;
      private string AV13encryptedText ;
      private string AV12clearText ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV15keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV16DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV17EncryptionService ;
      private string aP2_clearText ;
      private string aP3_error ;
   }

}
