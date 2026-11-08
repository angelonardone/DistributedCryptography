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
namespace GeneXus.Programs.wallet {
   public class unlockrubywallet : GXProcedure
   {
      public unlockrubywallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public unlockrubywallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           string aP1_code ,
                           out string aP2_error )
      {
         this.AV17password = aP0_password;
         this.AV10code = aP1_code;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV12error;
      }

      public string executeUdp( string aP0_password ,
                                string aP1_code )
      {
         execute(aP0_password, aP1_code, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_password ,
                                 string aP1_code ,
                                 out string aP2_error )
      {
         this.AV17password = aP0_password;
         this.AV10code = aP1_code;
         this.AV12error = "" ;
         SubmitImpl();
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12error = "";
         GXt_SdtWallet1 = AV23wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV23wallet = GXt_SdtWallet1;
         GXt_char2 = AV12error;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV17password,  AV23wallet.gxTpr_Encryptedsecret, out  AV9clearText, ref  GXt_char2) ;
         AV12error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         AV13extendeSecretAndAuthenticator.FromJSonString(AV9clearText, null);
         AV9clearText = "";
         AV23wallet.gxTpr_Networktype = AV13extendeSecretAndAuthenticator.gxTpr_Networktype;
         new GeneXus.Programs.wallet.setwallet(context ).execute(  AV23wallet) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32)) )
         {
            AV14isValid = AV22TwoFactorAuthenticator.validatetwofactorpin(StringUtil.Trim( AV13extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32), StringUtil.Trim( AV10code), true);
            if ( ! AV14isValid )
            {
               AV12error = "Error validating Authenticator";
               cleanup();
               if (true) return;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV23wallet.gxTpr_Rubydevice)) || String.IsNullOrEmpty(StringUtil.RTrim( AV13extendeSecretAndAuthenticator.gxTpr_Oraclekey)) )
         {
            AV12error = "This Ruby wallet has no Ruby device or no oracle";
            cleanup();
            if (true) return;
         }
         AV11device = StringUtil.Trim( AV23wallet.gxTpr_Rubydevice);
         AV16networkTypeText = AV23wallet.gxTpr_Networktype;
         AV15json = AV19rubyProcessor.unlock(AV11device, AV16networkTypeText, StringUtil.Trim( AV13extendeSecretAndAuthenticator.gxTpr_Oraclekey), AV23wallet.gxTpr_Encryptedoracle);
         AV13extendeSecretAndAuthenticator.gxTpr_Oraclekey = "";
         AV21rubyResult.FromJSonString(AV15json, null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21rubyResult.gxTpr_Encryptedoracle)) )
         {
            AV23wallet.gxTpr_Encryptedoracle = AV21rubyResult.gxTpr_Encryptedoracle;
            new GeneXus.Programs.wallet.setwallet(context ).execute(  AV23wallet) ;
            GXt_char2 = AV8auxError;
            new GeneXus.Programs.wallet.saverubyoracle(context ).execute(  AV23wallet.gxTpr_Walletfilename,  AV21rubyResult.gxTpr_Encryptedoracle, out  GXt_char2) ;
            AV8auxError = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8auxError)) )
            {
               AV12error = "The oracle state could not be saved: " + StringUtil.Trim( AV8auxError);
               cleanup();
               if (true) return;
            }
         }
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            AV12error = "Ruby: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         if ( ! AV21rubyResult.gxTpr_Unlocked )
         {
            AV12error = "Wrong PIN on Ruby (" + StringUtil.Trim( AV21rubyResult.gxTpr_Oracleresult) + "). After 3 wrong PINs Ruby erases its wallet; it can be restored from the words";
            cleanup();
            if (true) return;
         }
         AV15json = AV19rubyProcessor.getpublicinfo(AV11device, AV16networkTypeText);
         AV20rubyPublic.FromJSonString(AV15json, null);
         if ( ! AV20rubyPublic.gxTpr_Success )
         {
            AV12error = "Ruby: " + StringUtil.Trim( AV20rubyPublic.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV23wallet.gxTpr_Rubyfingerprint)) && ( StringUtil.StrCmp(StringUtil.Trim( AV23wallet.gxTpr_Rubyfingerprint), StringUtil.Trim( AV20rubyPublic.gxTpr_Fingerprint)) != 0 ) )
         {
            AV12error = "This Ruby holds another wallet (fingerprint " + StringUtil.Trim( AV20rubyPublic.gxTpr_Fingerprint) + ", expected " + StringUtil.Trim( AV23wallet.gxTpr_Rubyfingerprint) + ")";
            cleanup();
            if (true) return;
         }
         AV15json = AV19rubyProcessor.getutilitykeys(AV11device, AV16networkTypeText);
         AV18rubyKeys.FromJSonString(AV15json, null);
         AV15json = "";
         if ( ! AV18rubyKeys.gxTpr_Success )
         {
            AV12error = "Ruby: " + StringUtil.Trim( AV18rubyKeys.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV12error;
         new GeneXus.Programs.wallet.rubysetsessionkeys(context ).execute(  AV20rubyPublic,  AV18rubyKeys, out  GXt_char2) ;
         AV12error = GXt_char2;
         AV18rubyKeys = new GeneXus.Programs.wallet.SdtRubyResult(context);
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
         AV12error = "";
         AV23wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9clearText = "";
         AV13extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV22TwoFactorAuthenticator = new GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator(context);
         AV11device = "";
         AV16networkTypeText = "";
         AV15json = "";
         AV19rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV21rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV8auxError = "";
         AV20rubyPublic = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV18rubyKeys = new GeneXus.Programs.wallet.SdtRubyResult(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV17password ;
      private string AV10code ;
      private string AV8auxError ;
      private string GXt_char2 ;
      private bool AV14isValid ;
      private string AV9clearText ;
      private string AV15json ;
      private string AV12error ;
      private string AV11device ;
      private string AV16networkTypeText ;
      private GeneXus.Programs.wallet.SdtWallet AV23wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV13extendeSecretAndAuthenticator ;
      private GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator AV22TwoFactorAuthenticator ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV19rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV21rubyResult ;
      private GeneXus.Programs.wallet.SdtRubyResult AV20rubyPublic ;
      private GeneXus.Programs.wallet.SdtRubyResult AV18rubyKeys ;
      private string aP2_error ;
   }

}
