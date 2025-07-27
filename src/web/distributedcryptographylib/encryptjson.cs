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
   public class encryptjson : GXProcedure
   {
      public encryptjson( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptjson( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_clearText ,
                           string aP1_inEncryptionKey ,
                           out string aP2_encryptedKey ,
                           out string aP3_encryptedText ,
                           out string aP4_error )
      {
         this.AV23clearText = aP0_clearText;
         this.AV28inEncryptionKey = aP1_inEncryptionKey;
         this.AV25encryptedKey = "" ;
         this.AV24encryptedText = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptedKey=this.AV25encryptedKey;
         aP3_encryptedText=this.AV24encryptedText;
         aP4_error=this.AV12error;
      }

      public string executeUdp( string aP0_clearText ,
                                string aP1_inEncryptionKey ,
                                out string aP2_encryptedKey ,
                                out string aP3_encryptedText )
      {
         execute(aP0_clearText, aP1_inEncryptionKey, out aP2_encryptedKey, out aP3_encryptedText, out aP4_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_clearText ,
                                 string aP1_inEncryptionKey ,
                                 out string aP2_encryptedKey ,
                                 out string aP3_encryptedText ,
                                 out string aP4_error )
      {
         this.AV23clearText = aP0_clearText;
         this.AV28inEncryptionKey = aP1_inEncryptionKey;
         this.AV25encryptedKey = "" ;
         this.AV24encryptedText = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP2_encryptedKey=this.AV25encryptedKey;
         aP3_encryptedText=this.AV24encryptedText;
         aP4_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtKeyInfo1 = AV26keyInfo;
         new GeneXus.Programs.wallet.getdfaultjasonkey(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV26keyInfo = GXt_SdtKeyInfo1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV28inEncryptionKey)) )
         {
            AV29EncryptionResult = AV30EncryptionService.encrypt(AV23clearText, "");
            AV10encryptionKey = AV29EncryptionResult.gxTpr_Generatedkey;
         }
         else
         {
            AV29EncryptionResult = AV30EncryptionService.encrypt(AV23clearText, AV28inEncryptionKey);
            AV10encryptionKey = AV28inEncryptionKey;
         }
         if ( AV29EncryptionResult.gxTpr_Success )
         {
            AV24encryptedText = AV29EncryptionResult.gxTpr_Ciphertext;
            GXt_char2 = AV12error;
            new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  AV26keyInfo.gxTpr_Publickey,  AV10encryptionKey, out  AV25encryptedKey, out  GXt_char2) ;
            AV12error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               new GeneXus.Programs.wallet.setlastjasonencritpedkey(context ).execute(  StringUtil.Trim( AV25encryptedKey)) ;
            }
         }
         else
         {
            AV12error = AV29EncryptionResult.gxTpr_Errormessage;
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
         AV25encryptedKey = "";
         AV24encryptedText = "";
         AV12error = "";
         AV26keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV29EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV30EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV10encryptionKey = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV28inEncryptionKey ;
      private string AV25encryptedKey ;
      private string AV12error ;
      private string AV10encryptionKey ;
      private string GXt_char2 ;
      private string AV23clearText ;
      private string AV24encryptedText ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV26keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV29EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV30EncryptionService ;
      private string aP2_encryptedKey ;
      private string aP3_encryptedText ;
      private string aP4_error ;
   }

}
