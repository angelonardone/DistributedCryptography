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
   public class createrubywallet : GXProcedure
   {
      public createrubywallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createrubywallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           string aP1_networkType ,
                           string aP2_device ,
                           string aP3_newPass ,
                           string aP4_authenticatorBase32 ,
                           out string aP5_error )
      {
         this.AV20walletName = aP0_walletName;
         this.AV13networkType = aP1_networkType;
         this.AV9device = aP2_device;
         this.AV15newPass = aP3_newPass;
         this.AV8authenticatorBase32 = aP4_authenticatorBase32;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP5_error=this.AV10error;
      }

      public string executeUdp( string aP0_walletName ,
                                string aP1_networkType ,
                                string aP2_device ,
                                string aP3_newPass ,
                                string aP4_authenticatorBase32 )
      {
         execute(aP0_walletName, aP1_networkType, aP2_device, aP3_newPass, aP4_authenticatorBase32, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 string aP1_networkType ,
                                 string aP2_device ,
                                 string aP3_newPass ,
                                 string aP4_authenticatorBase32 ,
                                 out string aP5_error )
      {
         this.AV20walletName = aP0_walletName;
         this.AV13networkType = aP1_networkType;
         this.AV9device = aP2_device;
         this.AV15newPass = aP3_newPass;
         this.AV8authenticatorBase32 = aP4_authenticatorBase32;
         this.AV10error = "" ;
         SubmitImpl();
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.checkwalletcreation(context ).execute(  AV20walletName, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9device)) )
         {
            AV10error = "Choose the Ruby device";
            cleanup();
            if (true) return;
         }
         AV14networkTypeText = AV13networkType;
         AV16rubyOracle.FromJSonString(AV17rubyProcessor.createoracle(), null);
         AV12json = AV17rubyProcessor.pair(StringUtil.Trim( AV9device), AV16rubyOracle.gxTpr_Oraclekey, AV16rubyOracle.gxTpr_Encryptedoracle);
         AV18rubyResult.FromJSonString(AV12json, null);
         if ( ! AV18rubyResult.gxTpr_Success )
         {
            AV10error = "Ruby could not be paired with this wallet: " + StringUtil.Trim( AV18rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         AV12json = AV17rubyProcessor.unlock(StringUtil.Trim( AV9device), AV14networkTypeText, AV16rubyOracle.gxTpr_Oraclekey, AV16rubyOracle.gxTpr_Encryptedoracle);
         AV18rubyResult.FromJSonString(AV12json, null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18rubyResult.gxTpr_Encryptedoracle)) )
         {
            AV16rubyOracle.gxTpr_Encryptedoracle = AV18rubyResult.gxTpr_Encryptedoracle;
         }
         if ( ! AV18rubyResult.gxTpr_Success )
         {
            AV10error = "Ruby: " + StringUtil.Trim( AV18rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         if ( ! AV18rubyResult.gxTpr_Unlocked )
         {
            AV10error = "Ruby did not accept the PIN";
            cleanup();
            if (true) return;
         }
         AV12json = AV17rubyProcessor.getpublicinfo(StringUtil.Trim( AV9device), AV14networkTypeText);
         AV18rubyResult.FromJSonString(AV12json, null);
         if ( ! AV18rubyResult.gxTpr_Success )
         {
            AV10error = "Ruby: " + StringUtil.Trim( AV18rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         AV11extendeSecretAndAuthenticator.gxTpr_Networktype = AV13networkType;
         AV11extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = "";
         AV11extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = StringUtil.Trim( AV8authenticatorBase32);
         AV11extendeSecretAndAuthenticator.gxTpr_Oraclekey = AV16rubyOracle.gxTpr_Oraclekey;
         AV19wallet.gxTpr_Walletname = AV20walletName;
         AV19wallet.gxTpr_Networktype = AV13networkType;
         AV19wallet.gxTpr_Wallettype = "BIP86";
         AV19wallet.gxTpr_Keystorage = "Ruby";
         AV19wallet.gxTpr_Rubydevice = StringUtil.Trim( AV9device);
         AV19wallet.gxTpr_Rubyfingerprint = AV18rubyResult.gxTpr_Fingerprint;
         AV19wallet.gxTpr_Encryptedoracle = AV16rubyOracle.gxTpr_Encryptedoracle;
         AV19wallet.gxTpr_Useauthenticator = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV8authenticatorBase32)));
         GXt_char1 = AV10error;
         GXt_char2 = AV19wallet.gxTpr_Encryptedsecret;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV15newPass,  AV11extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char2, ref  GXt_char1) ;
         AV19wallet.gxTpr_Encryptedsecret = GXt_char2;
         AV10error = GXt_char1;
         AV11extendeSecretAndAuthenticator.gxTpr_Oraclekey = "";
         AV16rubyOracle.gxTpr_Oraclekey = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV19wallet) ;
         }
         else
         {
            AV10error = "There was a problem encrypting the wallet secret: " + AV10error;
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
         AV10error = "";
         AV14networkTypeText = "";
         AV16rubyOracle = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV17rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV12json = "";
         AV18rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV11extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV19wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char1 = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV20walletName ;
      private string AV13networkType ;
      private string AV15newPass ;
      private string AV8authenticatorBase32 ;
      private string AV10error ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private string AV12json ;
      private string AV9device ;
      private string AV14networkTypeText ;
      private GeneXus.Programs.wallet.SdtRubyResult AV16rubyOracle ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV17rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV18rubyResult ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV11extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.SdtWallet AV19wallet ;
      private string aP5_error ;
   }

}
