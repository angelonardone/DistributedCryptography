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
   public class restorewalletfromsecret : GXProcedure
   {
      public restorewalletfromsecret( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public restorewalletfromsecret( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           string aP1_networkType ,
                           string aP2_walletType ,
                           short aP3_walletRestoreMethod ,
                           string aP4_mnemonicText ,
                           string aP5_coldCardPassword ,
                           string aP6_passphrase ,
                           string aP7_wifText ,
                           string aP8_brainText ,
                           string aP9_newPass ,
                           string aP10_authenticatorBase32 ,
                           out string aP11_error )
      {
         this.AV24walletName = aP0_walletName;
         this.AV19networkType = aP1_networkType;
         this.AV26walletType = aP2_walletType;
         this.AV25walletRestoreMethod = aP3_walletRestoreMethod;
         this.AV18mnemonicText = aP4_mnemonicText;
         this.AV10coldCardPassword = aP5_coldCardPassword;
         this.AV21passphrase = aP6_passphrase;
         this.AV28wifText = aP7_wifText;
         this.AV9brainText = aP8_brainText;
         this.AV20newPass = aP9_newPass;
         this.AV8authenticatorBase32 = aP10_authenticatorBase32;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP11_error=this.AV11error;
      }

      public string executeUdp( string aP0_walletName ,
                                string aP1_networkType ,
                                string aP2_walletType ,
                                short aP3_walletRestoreMethod ,
                                string aP4_mnemonicText ,
                                string aP5_coldCardPassword ,
                                string aP6_passphrase ,
                                string aP7_wifText ,
                                string aP8_brainText ,
                                string aP9_newPass ,
                                string aP10_authenticatorBase32 )
      {
         execute(aP0_walletName, aP1_networkType, aP2_walletType, aP3_walletRestoreMethod, aP4_mnemonicText, aP5_coldCardPassword, aP6_passphrase, aP7_wifText, aP8_brainText, aP9_newPass, aP10_authenticatorBase32, out aP11_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 string aP1_networkType ,
                                 string aP2_walletType ,
                                 short aP3_walletRestoreMethod ,
                                 string aP4_mnemonicText ,
                                 string aP5_coldCardPassword ,
                                 string aP6_passphrase ,
                                 string aP7_wifText ,
                                 string aP8_brainText ,
                                 string aP9_newPass ,
                                 string aP10_authenticatorBase32 ,
                                 out string aP11_error )
      {
         this.AV24walletName = aP0_walletName;
         this.AV19networkType = aP1_networkType;
         this.AV26walletType = aP2_walletType;
         this.AV25walletRestoreMethod = aP3_walletRestoreMethod;
         this.AV18mnemonicText = aP4_mnemonicText;
         this.AV10coldCardPassword = aP5_coldCardPassword;
         this.AV21passphrase = aP6_passphrase;
         this.AV28wifText = aP7_wifText;
         this.AV9brainText = aP8_brainText;
         this.AV20newPass = aP9_newPass;
         this.AV8authenticatorBase32 = aP10_authenticatorBase32;
         this.AV11error = "" ;
         SubmitImpl();
         aP11_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11error = "";
         if ( ( StringUtil.StrCmp(AV26walletType, "ImportedWIF") == 0 ) || ( StringUtil.StrCmp(AV26walletType, "BrainWallet") == 0 ) )
         {
            if ( StringUtil.StrCmp(AV26walletType, "ImportedWIF") == 0 )
            {
               AV15keyCreate.gxTpr_Createkeytype = 100;
               AV15keyCreate.gxTpr_Createtext = AV28wifText;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV28wifText)) )
               {
                  AV11error = "The WIF cannot be empty";
               }
            }
            else
            {
               AV15keyCreate.gxTpr_Createkeytype = 20;
               AV15keyCreate.gxTpr_Createtext = AV9brainText;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9brainText)) )
               {
                  AV11error = "The Brain text cannot be empty";
               }
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV15keyCreate.gxTpr_Networktype = AV19networkType;
               AV15keyCreate.gxTpr_Addresstype = 0;
               GXt_char1 = AV11error;
               new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV15keyCreate,  "", out  AV16keyInfo, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char1 = AV11error;
                  GXt_char2 = AV23wallet.gxTpr_Encryptedsecret;
                  new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV20newPass,  AV16keyInfo.gxTpr_Privatekey, out  GXt_char2, ref  GXt_char1) ;
                  AV23wallet.gxTpr_Encryptedsecret = GXt_char2;
                  AV11error = GXt_char1;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV23wallet.gxTpr_Wallettype = AV26walletType;
                     AV23wallet.gxTpr_Networktype = AV19networkType;
                     AV23wallet.gxTpr_Walletname = AV24walletName;
                     new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV23wallet) ;
                  }
               }
            }
         }
         else if ( ( StringUtil.StrCmp(AV26walletType, "BIP44") == 0 ) || ( StringUtil.StrCmp(AV26walletType, "BIP49") == 0 ) || ( StringUtil.StrCmp(AV26walletType, "BIP84") == 0 ) || ( StringUtil.StrCmp(AV26walletType, "BIP86") == 0 ) )
         {
            if ( AV25walletRestoreMethod == 20 )
            {
               AV22stagedFile = AV27webSession.Get("ColdcardStagedFile");
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV22stagedFile)) )
               {
                  AV11error = "Please, add the COLDCARD backup file again";
               }
               else
               {
                  GXt_char2 = AV11error;
                  new GeneXus.Programs.distributedcrypto.decryptcoldcardbackup(context ).execute(  StringUtil.Trim( AV10coldCardPassword),  AV22stagedFile, out  AV17mnemonic, out  GXt_char2) ;
                  AV11error = GXt_char2;
                  new GeneXus.Programs.wallet.discardcoldcardbackup(context ).execute( ) ;
               }
            }
            else
            {
               AV17mnemonic = AV18mnemonicText;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) && String.IsNullOrEmpty(StringUtil.RTrim( AV17mnemonic)) )
            {
               AV11error = "The Mnemoic text cannot be empty";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV13extKeyCreate.gxTpr_Networktype = AV19networkType;
               AV13extKeyCreate.gxTpr_Createextkeytype = 30;
               AV13extKeyCreate.gxTpr_Mnemoniclanguage = 10;
               AV13extKeyCreate.gxTpr_Createtext = AV17mnemonic;
               if ( StringUtil.StrCmp(AV26walletType, "BIP86") == 0 )
               {
                  AV13extKeyCreate.gxTpr_Keypath = "";
               }
               else if ( StringUtil.StrCmp(AV26walletType, "BIP44") == 0 )
               {
                  if ( StringUtil.StrCmp(AV19networkType, "MainNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/44'/0'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "TestNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/44'/1'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "RegTest") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/44'/1'/0'";
                  }
                  else
                  {
                     AV11error = "Network Type not sopported";
                  }
               }
               else if ( StringUtil.StrCmp(AV26walletType, "BIP49") == 0 )
               {
                  if ( StringUtil.StrCmp(AV19networkType, "MainNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/49'/0'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "TestNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/49'/1'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "RegTest") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/49'/1'/0'";
                  }
                  else
                  {
                     AV11error = "Network Type not sopported";
                  }
               }
               else
               {
                  if ( StringUtil.StrCmp(AV19networkType, "MainNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/84'/0'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "TestNet") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/84'/1'/0'";
                  }
                  else if ( StringUtil.StrCmp(AV19networkType, "RegTest") == 0 )
                  {
                     AV13extKeyCreate.gxTpr_Keypath = "m/84'/1'/0'";
                  }
                  else
                  {
                     AV11error = "Network Type not sopported";
                  }
               }
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               GXt_char2 = AV11error;
               new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV13extKeyCreate,  StringUtil.Trim( AV21passphrase), out  AV14extKeyInfo, out  GXt_char2) ;
               AV11error = GXt_char2;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV12extendeSecretAndAuthenticator.gxTpr_Networktype = AV19networkType;
               AV12extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV14extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
               AV12extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = StringUtil.Trim( AV8authenticatorBase32);
               AV23wallet.gxTpr_Walletname = AV24walletName;
               AV23wallet.gxTpr_Networktype = AV19networkType;
               AV23wallet.gxTpr_Wallettype = AV26walletType;
               AV23wallet.gxTpr_Useauthenticator = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV8authenticatorBase32)));
               GXt_char2 = AV11error;
               GXt_char1 = AV23wallet.gxTpr_Encryptedsecret;
               new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV20newPass,  AV12extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char1, ref  GXt_char2) ;
               AV23wallet.gxTpr_Encryptedsecret = GXt_char1;
               AV11error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV23wallet) ;
               }
            }
         }
         else
         {
            AV11error = "Please, selet a Wallet type you want to restore";
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
         AV11error = "";
         AV15keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         AV16keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV23wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV22stagedFile = "";
         AV27webSession = context.GetSession();
         AV17mnemonic = "";
         AV13extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV14extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV12extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         GXt_char2 = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV25walletRestoreMethod ;
      private string AV24walletName ;
      private string AV19networkType ;
      private string AV26walletType ;
      private string AV21passphrase ;
      private string AV28wifText ;
      private string AV9brainText ;
      private string AV20newPass ;
      private string AV8authenticatorBase32 ;
      private string AV11error ;
      private string GXt_char2 ;
      private string GXt_char1 ;
      private string AV18mnemonicText ;
      private string AV10coldCardPassword ;
      private string AV17mnemonic ;
      private string AV22stagedFile ;
      private IGxSession AV27webSession ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV15keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV16keyInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV23wallet ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV13extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV14extKeyInfo ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV12extendeSecretAndAuthenticator ;
      private string aP11_error ;
   }

}
