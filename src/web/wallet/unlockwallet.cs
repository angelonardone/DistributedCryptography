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
   public class unlockwallet : GXProcedure
   {
      public unlockwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public unlockwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo ,
                           out bool aP1_isOpen ,
                           out bool aP2_justUnlocked ,
                           out bool aP3_dispatchMessages ,
                           out string aP4_error )
      {
         this.AV30walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         this.AV20isOpen = false ;
         this.AV22justUnlocked = false ;
         this.AV11dispatchMessages = false ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP0_walletInfo=this.AV30walletInfo;
         aP1_isOpen=this.AV20isOpen;
         aP2_justUnlocked=this.AV22justUnlocked;
         aP3_dispatchMessages=this.AV11dispatchMessages;
         aP4_error=this.AV13error;
      }

      public string executeUdp( out GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo ,
                                out bool aP1_isOpen ,
                                out bool aP2_justUnlocked ,
                                out bool aP3_dispatchMessages )
      {
         execute(out aP0_walletInfo, out aP1_isOpen, out aP2_justUnlocked, out aP3_dispatchMessages, out aP4_error);
         return AV13error ;
      }

      public void executeSubmit( out GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo ,
                                 out bool aP1_isOpen ,
                                 out bool aP2_justUnlocked ,
                                 out bool aP3_dispatchMessages ,
                                 out string aP4_error )
      {
         this.AV30walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         this.AV20isOpen = false ;
         this.AV22justUnlocked = false ;
         this.AV11dispatchMessages = false ;
         this.AV13error = "" ;
         SubmitImpl();
         aP0_walletInfo=this.AV30walletInfo;
         aP1_isOpen=this.AV20isOpen;
         aP2_justUnlocked=this.AV22justUnlocked;
         aP3_dispatchMessages=this.AV11dispatchMessages;
         aP4_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV20isOpen = false;
         AV22justUnlocked = false;
         AV11dispatchMessages = false;
         AV13error = "";
         GXt_SdtWallet1 = AV29wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV29wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV24keyInfo;
         new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV24keyInfo = GXt_SdtKeyInfo2;
         GXt_SdtExtKeyInfo3 = AV18extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo3) ;
         AV18extKeyInfo = GXt_SdtExtKeyInfo3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV24keyInfo.gxTpr_Publickey)) && String.IsNullOrEmpty(StringUtil.RTrim( AV18extKeyInfo.gxTpr_Publickey)) )
         {
            GXt_SdtWallet1 = AV29wallet;
            new GeneXus.Programs.wallet.readwallet(context ).execute(  AV29wallet.gxTpr_Walletfilename, out  GXt_SdtWallet1) ;
            AV29wallet = GXt_SdtWallet1;
            AV27password = AV31webSession.Get("TempPassword");
            AV10code = AV31webSession.Get("TempPINAuthenticator");
            AV31webSession.Set("TempPassword", "");
            AV31webSession.Set("TempPINAuthenticator", "");
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV27password)) )
            {
               new GeneXus.Programs.wallet.setwallet(context ).execute(  AV29wallet) ;
               if ( ( StringUtil.StrCmp(AV29wallet.gxTpr_Wallettype, "BrainWallet") == 0 ) || ( StringUtil.StrCmp(AV29wallet.gxTpr_Wallettype, "ImportedWIF") == 0 ) )
               {
                  AV23keyCreate.gxTpr_Createkeytype = 30;
                  AV23keyCreate.gxTpr_Networktype = AV29wallet.gxTpr_Networktype;
                  AV23keyCreate.gxTpr_Addresstype = 0;
                  GXt_char4 = AV13error;
                  GXt_char5 = AV23keyCreate.gxTpr_Createtext;
                  new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV27password,  AV29wallet.gxTpr_Encryptedsecret, out  GXt_char5, ref  GXt_char4) ;
                  AV23keyCreate.gxTpr_Createtext = GXt_char5;
                  AV13error = GXt_char4;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     GXt_char5 = AV13error;
                     new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV23keyCreate,  "", out  AV24keyInfo, out  GXt_char5) ;
                     AV13error = GXt_char5;
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) || String.IsNullOrEmpty(StringUtil.RTrim( AV24keyInfo.gxTpr_Privatekey)) )
                     {
                        /* Execute user subroutine: 'COULD NOT DECRYPT' */
                        S111 ();
                        if ( returnInSub )
                        {
                           cleanup();
                           if (true) return;
                        }
                     }
                     else
                     {
                        new GeneXus.Programs.wallet.setkey(context ).execute(  AV24keyInfo) ;
                        new GeneXus.Programs.wallet.setdefaultjasonkey(context ).execute(  AV24keyInfo) ;
                        AV20isOpen = true;
                        AV22justUnlocked = true;
                     }
                  }
               }
               else
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV27password,  AV29wallet.gxTpr_Encryptedsecret, out  AV9clearText, ref  GXt_char5) ;
                  AV13error = GXt_char5;
                  AV14extendeSecretAndAuthenticator.FromJSonString(AV9clearText, null);
                  if ( StringUtil.StrCmp(AV14extendeSecretAndAuthenticator.gxTpr_Networktype, "MainNet") == 0 )
                  {
                     AV26kpBIP86 = "m/86'/0'/0'";
                     AV25kpBIP48 = "m/48'/0'/0'/1'";
                  }
                  else if ( StringUtil.StrCmp(AV14extendeSecretAndAuthenticator.gxTpr_Networktype, "TestNet") == 0 )
                  {
                     AV26kpBIP86 = "m/86'/1'/0'";
                     AV25kpBIP48 = "m/48'/1'/0'/1'";
                  }
                  else
                  {
                     AV26kpBIP86 = "m/86'/1'/0'";
                     AV25kpBIP48 = "m/48'/1'/0'/1'";
                  }
                  AV16extKeyCreate.gxTpr_Extendedprivatekey = AV14extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey;
                  if ( StringUtil.StrCmp(AV29wallet.gxTpr_Wallettype, "BIP86") == 0 )
                  {
                     AV16extKeyCreate.gxTpr_Keypath = AV26kpBIP86;
                  }
                  else
                  {
                     AV16extKeyCreate.gxTpr_Keypath = "";
                  }
                  AV16extKeyCreate.gxTpr_Createextkeytype = 70;
                  AV16extKeyCreate.gxTpr_Networktype = AV14extendeSecretAndAuthenticator.gxTpr_Networktype;
                  AV29wallet.gxTpr_Networktype = AV14extendeSecretAndAuthenticator.gxTpr_Networktype;
                  new GeneXus.Programs.wallet.setwallet(context ).execute(  AV29wallet) ;
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV16extKeyCreate,  "", out  AV18extKeyInfo, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32)) )
                     {
                        AV21isValid = AV28TwoFactorAuthenticator.validatetwofactorpin(StringUtil.Trim( AV14extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32), StringUtil.Trim( AV10code), true);
                        if ( ! AV21isValid )
                        {
                           AV13error = "Error validating Authenticator";
                           AV16extKeyCreate.gxTpr_Extendedprivatekey = "";
                           AV18extKeyInfo.gxTpr_Privatekey = "";
                           AV9clearText = "";
                           AV14extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = "";
                        }
                     }
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) || String.IsNullOrEmpty(StringUtil.RTrim( AV18extKeyInfo.gxTpr_Privatekey)) )
                     {
                        /* Execute user subroutine: 'COULD NOT DECRYPT' */
                        S111 ();
                        if ( returnInSub )
                        {
                           cleanup();
                           if (true) return;
                        }
                     }
                     else
                     {
                        new GeneXus.Programs.wallet.setextkey(context ).execute(  AV18extKeyInfo) ;
                        if ( StringUtil.StrCmp(AV29wallet.gxTpr_Wallettype, "BIP86") == 0 )
                        {
                           AV17extKeyCreateBIP48.gxTpr_Extendedprivatekey = AV14extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey;
                           AV17extKeyCreateBIP48.gxTpr_Keypath = AV25kpBIP48;
                           AV17extKeyCreateBIP48.gxTpr_Createextkeytype = 70;
                           AV17extKeyCreateBIP48.gxTpr_Networktype = AV14extendeSecretAndAuthenticator.gxTpr_Networktype;
                           GXt_char5 = AV8auxError;
                           new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV17extKeyCreateBIP48,  "", out  AV19extKeyInfoBIP48, out  GXt_char5) ;
                           AV8auxError = GXt_char5;
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8auxError)) )
                           {
                              AV19extKeyInfoBIP48.gxTpr_Privatekey = "";
                              AV19extKeyInfoBIP48.gxTpr_Chaincode = "";
                              AV19extKeyInfoBIP48.gxTpr_Wif = "";
                              AV19extKeyInfoBIP48.gxTpr_Encryptedwif = "";
                              AV19extKeyInfoBIP48.gxTpr_Mnemonic = "";
                              AV19extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekey = "";
                              AV19extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekeysegwitp2sh = "";
                              AV19extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekeysegwit = "";
                              AV19extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekeytaproot = "";
                              new GeneXus.Programs.wallet.setextkeybip48(context ).execute(  AV19extKeyInfoBIP48) ;
                           }
                        }
                        AV12emptyKeyInfo.gxTpr_Privatekey = "";
                        new GeneXus.Programs.wallet.setdefaultjasonkey(context ).execute(  AV12emptyKeyInfo) ;
                        new GeneXus.Programs.wallet.setlogindistcrypt(context ).execute( ) ;
                        new GeneXus.Programs.wallet.setfileenckey(context ).execute( ) ;
                        GXt_char5 = AV8auxError;
                        new GeneXus.Programs.distcrypt.sso.createandsaveexternaluser(context ).execute( out  GXt_char5) ;
                        AV8auxError = GXt_char5;
                        GXt_char5 = AV8auxError;
                        new GeneXus.Programs.hsm.inithsm(context ).execute( out  GXt_char5) ;
                        AV8auxError = GXt_char5;
                        new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                        AV20isOpen = true;
                        AV22justUnlocked = true;
                        GXt_SdtExternalUser6 = AV15externalUser;
                        new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser6) ;
                        AV15externalUser = GXt_SdtExternalUser6;
                        if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15externalUser.gxTpr_Externaltoken)) )
                        {
                           GXt_char5 = AV13error;
                           new GeneXus.Programs.nostr.startconnection(context ).execute( out  GXt_char5) ;
                           AV13error = GXt_char5;
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                           {
                              AV11dispatchMessages = true;
                           }
                        }
                     }
                  }
               }
            }
            else
            {
               AV13error = "Password can not be empty!";
            }
         }
         else
         {
            AV20isOpen = true;
         }
         GXt_SdtWalletInfo7 = AV30walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo7) ;
         AV30walletInfo = GXt_SdtWalletInfo7;
         cleanup();
      }

      protected void S111( )
      {
         /* 'COULD NOT DECRYPT' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV13error = "We couldn't decrypt the wallet with the password provided";
         }
         else
         {
            AV13error = StringUtil.Trim( AV13error) + ". We couldn't decrypt the wallet with the password provided";
         }
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
         AV30walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13error = "";
         AV29wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV24keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV18extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo3 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV27password = "";
         AV31webSession = context.GetSession();
         AV10code = "";
         AV23keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         GXt_char4 = "";
         AV9clearText = "";
         AV14extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV26kpBIP86 = "";
         AV25kpBIP48 = "";
         AV16extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV28TwoFactorAuthenticator = new GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator(context);
         AV17extKeyCreateBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV8auxError = "";
         AV19extKeyInfoBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV12emptyKeyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV15externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser6 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char5 = "";
         GXt_SdtWalletInfo7 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private string AV27password ;
      private string AV10code ;
      private string GXt_char4 ;
      private string AV26kpBIP86 ;
      private string AV25kpBIP48 ;
      private string AV8auxError ;
      private string GXt_char5 ;
      private bool AV20isOpen ;
      private bool AV22justUnlocked ;
      private bool AV11dispatchMessages ;
      private bool returnInSub ;
      private bool AV21isValid ;
      private string AV9clearText ;
      private string AV13error ;
      private IGxSession AV31webSession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV30walletInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV29wallet ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV24keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV18extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo3 ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV23keyCreate ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV14extendeSecretAndAuthenticator ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV16extKeyCreate ;
      private GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator AV28TwoFactorAuthenticator ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV17extKeyCreateBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV19extKeyInfoBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV12emptyKeyInfo ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV15externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser6 ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo7 ;
      private GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo ;
      private bool aP1_isOpen ;
      private bool aP2_justUnlocked ;
      private bool aP3_dispatchMessages ;
      private string aP4_error ;
   }

}
