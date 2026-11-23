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
   public class createrubyhotwallet : GXProcedure
   {
      public createrubyhotwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createrubyhotwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           int aP1_number ,
                           string aP2_newPass ,
                           out string aP3_error )
      {
         this.AV22walletName = aP0_walletName;
         this.AV17number = aP1_number;
         this.AV16newPass = aP2_newPass;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV10error;
      }

      public string executeUdp( string aP0_walletName ,
                                int aP1_number ,
                                string aP2_newPass )
      {
         execute(aP0_walletName, aP1_number, aP2_newPass, out aP3_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 int aP1_number ,
                                 string aP2_newPass ,
                                 out string aP3_error )
      {
         this.AV22walletName = aP0_walletName;
         this.AV17number = aP1_number;
         this.AV16newPass = aP2_newPass;
         this.AV10error = "" ;
         SubmitImpl();
         aP3_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.checkwalletcreation(context ).execute(  AV22walletName, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtWallet2 = AV18parent;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV18parent = GXt_SdtWallet2;
         if ( ( StringUtil.StrCmp(AV18parent.gxTpr_Keystorage, "Ruby") != 0 ) || String.IsNullOrEmpty(StringUtil.RTrim( AV18parent.gxTpr_Rubydevice)) )
         {
            AV10error = "Hot wallets are created from an open Ruby wallet";
            cleanup();
            if (true) return;
         }
         if ( AV17number < 0 )
         {
            AV10error = "The hot wallet number must be 0 or more";
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.checkhotwalletnumber(context ).execute(  AV17number, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV9device = StringUtil.Trim( AV18parent.gxTpr_Rubydevice);
         AV15networkTypeText = AV18parent.gxTpr_Networktype;
         AV14json = AV19rubyProcessor.getpublicinfo(AV9device, AV15networkTypeText);
         AV20rubyPublic.FromJSonString(AV14json, null);
         if ( ! AV20rubyPublic.gxTpr_Success )
         {
            AV10error = "Ruby: " + StringUtil.Trim( AV20rubyPublic.gxTpr_Error) + ". Open the Ruby wallet again (Ruby locks itself after a while)";
            cleanup();
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18parent.gxTpr_Rubyfingerprint)) && ( StringUtil.StrCmp(StringUtil.Trim( AV18parent.gxTpr_Rubyfingerprint), StringUtil.Trim( AV20rubyPublic.gxTpr_Fingerprint)) != 0 ) )
         {
            AV10error = "This Ruby holds another wallet (fingerprint " + StringUtil.Trim( AV20rubyPublic.gxTpr_Fingerprint) + ", expected " + StringUtil.Trim( AV18parent.gxTpr_Rubyfingerprint) + ")";
            cleanup();
            if (true) return;
         }
         AV14json = AV19rubyProcessor.getchildmnemonic(AV9device, AV17number);
         AV8childMnemonic.FromJSonString(AV14json, null);
         AV14json = "";
         if ( ! AV8childMnemonic.gxTpr_Success )
         {
            if ( AV8childMnemonic.gxTpr_Cancelled )
            {
               AV10error = "Declined on Ruby: nothing was created";
            }
            else
            {
               AV10error = "Ruby: " + StringUtil.Trim( AV8childMnemonic.gxTpr_Error);
            }
            cleanup();
            if (true) return;
         }
         AV12extKeyCreate.gxTpr_Networktype = AV18parent.gxTpr_Networktype;
         AV12extKeyCreate.gxTpr_Createextkeytype = 30;
         AV12extKeyCreate.gxTpr_Mnemoniclanguage = 10;
         AV12extKeyCreate.gxTpr_Createtext = StringUtil.Trim( AV8childMnemonic.gxTpr_Mnemonic);
         AV12extKeyCreate.gxTpr_Keypath = "";
         GXt_char1 = AV10error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV12extKeyCreate,  "", out  AV13extKeyInfo, out  GXt_char1) ;
         AV10error = GXt_char1;
         AV8childMnemonic = new GeneXus.Programs.wallet.SdtRubyChildMnemonic(context);
         AV12extKeyCreate.gxTpr_Createtext = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV11extendeSecretAndAuthenticator.gxTpr_Networktype = AV18parent.gxTpr_Networktype;
         AV11extendeSecretAndAuthenticator.gxTpr_Seed = AV13extKeyInfo.gxTpr_Seed;
         AV11extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = "";
         AV13extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV21wallet.gxTpr_Walletname = AV22walletName;
         AV21wallet.gxTpr_Networktype = AV18parent.gxTpr_Networktype;
         AV21wallet.gxTpr_Wallettype = "BIP86";
         AV21wallet.gxTpr_Keystorage = "Software";
         AV21wallet.gxTpr_Useauthenticator = false;
         AV21wallet.gxTpr_Hotwalletof = StringUtil.Trim( AV20rubyPublic.gxTpr_Fingerprint);
         AV21wallet.gxTpr_Hotwalletnumber = AV17number;
         GXt_char1 = AV10error;
         GXt_char3 = AV21wallet.gxTpr_Encryptedsecret;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV16newPass,  AV11extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char3, ref  GXt_char1) ;
         AV21wallet.gxTpr_Encryptedsecret = GXt_char3;
         AV10error = GXt_char1;
         AV11extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV21wallet) ;
         }
         else
         {
            AV10error = "There was a problem encrypting the Extended Key: " + AV10error;
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
         AV18parent = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9device = "";
         AV15networkTypeText = "";
         AV14json = "";
         AV19rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV20rubyPublic = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV8childMnemonic = new GeneXus.Programs.wallet.SdtRubyChildMnemonic(context);
         AV12extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV13extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV11extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV21wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char1 = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV17number ;
      private string AV22walletName ;
      private string AV16newPass ;
      private string GXt_char1 ;
      private string GXt_char3 ;
      private string AV14json ;
      private string AV10error ;
      private string AV9device ;
      private string AV15networkTypeText ;
      private GeneXus.Programs.wallet.SdtWallet AV18parent ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV19rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV20rubyPublic ;
      private GeneXus.Programs.wallet.SdtRubyChildMnemonic AV8childMnemonic ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV12extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV13extKeyInfo ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV11extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.SdtWallet AV21wallet ;
      private string aP3_error ;
   }

}
