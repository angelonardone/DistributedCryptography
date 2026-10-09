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
   public class createadvancebrainwallet : GXProcedure
   {
      public createadvancebrainwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createadvancebrainwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           string aP1_networkType ,
                           string aP2_newPass ,
                           short aP3_year ,
                           short aP4_month ,
                           short aP5_day ,
                           decimal aP6_chosenPIN ,
                           string aP7_authenticatorBase32 ,
                           out string aP8_error )
      {
         this.AV23walletName = aP0_walletName;
         this.AV17networkType = aP1_networkType;
         this.AV18newPass = aP2_newPass;
         this.AV24year = aP3_year;
         this.AV16month = aP4_month;
         this.AV10day = aP5_day;
         this.AV9chosenPIN = aP6_chosenPIN;
         this.AV8authenticatorBase32 = aP7_authenticatorBase32;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP8_error=this.AV11error;
      }

      public string executeUdp( string aP0_walletName ,
                                string aP1_networkType ,
                                string aP2_newPass ,
                                short aP3_year ,
                                short aP4_month ,
                                short aP5_day ,
                                decimal aP6_chosenPIN ,
                                string aP7_authenticatorBase32 )
      {
         execute(aP0_walletName, aP1_networkType, aP2_newPass, aP3_year, aP4_month, aP5_day, aP6_chosenPIN, aP7_authenticatorBase32, out aP8_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 string aP1_networkType ,
                                 string aP2_newPass ,
                                 short aP3_year ,
                                 short aP4_month ,
                                 short aP5_day ,
                                 decimal aP6_chosenPIN ,
                                 string aP7_authenticatorBase32 ,
                                 out string aP8_error )
      {
         this.AV23walletName = aP0_walletName;
         this.AV17networkType = aP1_networkType;
         this.AV18newPass = aP2_newPass;
         this.AV24year = aP3_year;
         this.AV16month = aP4_month;
         this.AV10day = aP5_day;
         this.AV9chosenPIN = aP6_chosenPIN;
         this.AV8authenticatorBase32 = aP7_authenticatorBase32;
         this.AV11error = "" ;
         SubmitImpl();
         aP8_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV20salt = StringUtil.Trim( StringUtil.Str( (decimal)(AV24year), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV16month), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV10day), 4, 0)) + StringUtil.Trim( StringUtil.Str( AV9chosenPIN, 28, 0));
         GXt_char1 = AV11error;
         new GeneXus.Programs.distributedcrypto.argon2derivekey512(context ).execute(  AV18newPass,  AV20salt, out  AV21seedForKey, out  GXt_char1) ;
         AV11error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV19preSeed = StringUtil.Trim( StringUtil.Str( (decimal)(AV24year), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV16month), 4, 0)) + StringUtil.Trim( AV21seedForKey) + StringUtil.Trim( StringUtil.Str( (decimal)(AV10day), 4, 0)) + StringUtil.Trim( StringUtil.Str( AV9chosenPIN, 28, 0));
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.sha512(context ).execute(  AV19preSeed, out  AV15extKeySeed, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV13extKeyCreate.gxTpr_Networktype = AV17networkType;
               AV13extKeyCreate.gxTpr_Createextkeytype = 50;
               AV13extKeyCreate.gxTpr_Seed = StringUtil.Trim( AV15extKeySeed);
               AV13extKeyCreate.gxTpr_Keypath = "";
               GXt_char1 = AV11error;
               new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV13extKeyCreate,  "", out  AV14extKeyInfo, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV22wallet.gxTpr_Walletname = AV23walletName;
                  AV22wallet.gxTpr_Networktype = AV17networkType;
                  AV22wallet.gxTpr_Wallettype = "BIP86";
                  AV12extendeSecretAndAuthenticator.gxTpr_Networktype = AV17networkType;
                  AV12extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV14extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
                  AV12extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = StringUtil.Trim( AV8authenticatorBase32);
                  AV22wallet.gxTpr_Useauthenticator = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV8authenticatorBase32)));
                  GXt_char1 = AV11error;
                  GXt_char2 = AV22wallet.gxTpr_Encryptedsecret;
                  new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV18newPass,  AV12extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char2, ref  GXt_char1) ;
                  AV22wallet.gxTpr_Encryptedsecret = GXt_char2;
                  AV11error = GXt_char1;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV22wallet) ;
                  }
                  else
                  {
                     AV11error = "There was a problem encrypting the Extended Key: " + AV11error;
                  }
               }
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
         AV11error = "";
         AV20salt = "";
         AV21seedForKey = "";
         AV19preSeed = "";
         AV15extKeySeed = "";
         AV13extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV14extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV22wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV12extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         GXt_char1 = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private short AV24year ;
      private short AV16month ;
      private short AV10day ;
      private decimal AV9chosenPIN ;
      private string AV23walletName ;
      private string AV17networkType ;
      private string AV18newPass ;
      private string AV8authenticatorBase32 ;
      private string AV11error ;
      private string AV21seedForKey ;
      private string AV15extKeySeed ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private string AV20salt ;
      private string AV19preSeed ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV13extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV14extKeyInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV22wallet ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV12extendeSecretAndAuthenticator ;
      private string aP8_error ;
   }

}
