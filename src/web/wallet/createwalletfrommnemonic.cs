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
   public class createwalletfrommnemonic : GXProcedure
   {
      public createwalletfrommnemonic( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createwalletfrommnemonic( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           string aP1_networkType ,
                           string aP2_mnemonicText ,
                           short aP3_mnemonicLanguage ,
                           string aP4_newPass ,
                           string aP5_authenticatorBase32 ,
                           out string aP6_error )
      {
         this.AV18walletName = aP0_walletName;
         this.AV15networkType = aP1_networkType;
         this.AV14mnemonicText = aP2_mnemonicText;
         this.AV13mnemonicLanguage = aP3_mnemonicLanguage;
         this.AV16newPass = aP4_newPass;
         this.AV8authenticatorBase32 = aP5_authenticatorBase32;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP6_error=this.AV9error;
      }

      public string executeUdp( string aP0_walletName ,
                                string aP1_networkType ,
                                string aP2_mnemonicText ,
                                short aP3_mnemonicLanguage ,
                                string aP4_newPass ,
                                string aP5_authenticatorBase32 )
      {
         execute(aP0_walletName, aP1_networkType, aP2_mnemonicText, aP3_mnemonicLanguage, aP4_newPass, aP5_authenticatorBase32, out aP6_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 string aP1_networkType ,
                                 string aP2_mnemonicText ,
                                 short aP3_mnemonicLanguage ,
                                 string aP4_newPass ,
                                 string aP5_authenticatorBase32 ,
                                 out string aP6_error )
      {
         this.AV18walletName = aP0_walletName;
         this.AV15networkType = aP1_networkType;
         this.AV14mnemonicText = aP2_mnemonicText;
         this.AV13mnemonicLanguage = aP3_mnemonicLanguage;
         this.AV16newPass = aP4_newPass;
         this.AV8authenticatorBase32 = aP5_authenticatorBase32;
         this.AV9error = "" ;
         SubmitImpl();
         aP6_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11extKeyCreate.gxTpr_Networktype = AV15networkType;
         AV11extKeyCreate.gxTpr_Createextkeytype = 30;
         AV11extKeyCreate.gxTpr_Mnemoniclanguage = AV13mnemonicLanguage;
         AV11extKeyCreate.gxTpr_Createtext = StringUtil.Trim( AV14mnemonicText);
         AV11extKeyCreate.gxTpr_Keypath = "";
         GXt_char1 = AV9error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV11extKeyCreate,  AV16newPass, out  AV12extKeyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV10extendeSecretAndAuthenticator.gxTpr_Networktype = AV15networkType;
            AV10extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV12extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
            AV10extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = StringUtil.Trim( AV8authenticatorBase32);
            AV17wallet.gxTpr_Walletname = AV18walletName;
            AV17wallet.gxTpr_Networktype = AV15networkType;
            AV17wallet.gxTpr_Wallettype = "BIP86";
            AV17wallet.gxTpr_Useauthenticator = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV8authenticatorBase32)));
            GXt_char1 = AV9error;
            GXt_char2 = AV17wallet.gxTpr_Encryptedsecret;
            new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV16newPass,  AV10extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char2, ref  GXt_char1) ;
            AV17wallet.gxTpr_Encryptedsecret = GXt_char2;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV17wallet) ;
            }
            else
            {
               AV9error = "There was a problem encrypting the Extended Key: " + AV9error;
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
         AV9error = "";
         AV11extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV12extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV10extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char1 = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private short AV13mnemonicLanguage ;
      private string AV18walletName ;
      private string AV15networkType ;
      private string AV16newPass ;
      private string AV8authenticatorBase32 ;
      private string AV9error ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private string AV14mnemonicText ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV11extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfo ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV10extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private string aP6_error ;
   }

}
