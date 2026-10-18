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
         GXt_char1 = AV11error;
         new GeneXus.Programs.wallet.checkwalletcreation(context ).execute(  AV23walletName, out  GXt_char1) ;
         AV11error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV30validDate = false;
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
              string gxTyped = (AV18newPass ?? "").Trim();
         /* User Code */
              AV29phraseLength = (short)System.Math.Min(gxTyped.Length, 9999);
         /* User Code */
              AV26differentChars = (short)System.Math.Min(new System.Collections.Generic.HashSet<char>(gxTyped).Count, 9999);
         /* User Code */
              AV28phrase = gxTyped.Normalize(System.Text.NormalizationForm.FormKD);
         /* User Code */
              System.DateTime gxDate = System.DateTime.MinValue;
         /* User Code */
              string gxDateTyped = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", AV24year, AV16month, AV10day);
         /* User Code */
              AV30validDate = AV24year >= 1000 && System.DateTime.TryParseExact(gxDateTyped, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out gxDate);
         /* User Code */
              AV25dateText = AV30validDate ? gxDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) : "";
         /* User Code */
              AV27numberText = AV9chosenPIN.ToString("0", System.Globalization.CultureInfo.InvariantCulture);
         /* User Code */
          }
         /* User Code */
          catch (Exception ex)
         /* User Code */
          {
         /* User Code */
              AV11error = ex.Message.ToString();
         /* User Code */
          }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            if ( AV29phraseLength < 20 )
            {
               AV11error = "The phrase is too short: use at least 20 characters. Five or six words that have nothing to do with each other make a good phrase.";
            }
            else if ( AV26differentChars < 8 )
            {
               AV11error = "The phrase repeats the same few characters. Use a phrase with more different letters.";
            }
            else if ( ! AV30validDate )
            {
               AV11error = "The date is not a real date. Type the year with its 4 digits, the month from 1 to 12 and a day of that month.";
            }
            else if ( ( AV9chosenPIN < Convert.ToDecimal( 10000000 )) )
            {
               AV11error = "The number is too short: use at least 8 digits. Zeros at the beginning do not count.";
            }
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV20salt = "DistributedCryptography/AdvanceBrainWallet/2|" + StringUtil.Trim( AV25dateText) + "|" + StringUtil.Trim( AV27numberText);
         GXt_char1 = AV11error;
         new GeneXus.Programs.distributedcrypto.argon2derivekey512(context ).execute(  AV28phrase,  AV20salt, out  AV21seedForKey, out  GXt_char1) ;
         AV11error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV19preSeed = AV20salt + "|" + StringUtil.Trim( AV21seedForKey);
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
         AV28phrase = "";
         AV25dateText = "";
         AV27numberText = "";
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
      private short AV29phraseLength ;
      private short AV26differentChars ;
      private decimal AV9chosenPIN ;
      private string AV23walletName ;
      private string AV17networkType ;
      private string AV18newPass ;
      private string AV8authenticatorBase32 ;
      private string AV11error ;
      private string AV25dateText ;
      private string AV27numberText ;
      private string AV21seedForKey ;
      private string AV15extKeySeed ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private bool AV30validDate ;
      private string AV20salt ;
      private string AV19preSeed ;
      private string AV28phrase ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV13extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV14extKeyInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV22wallet ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV12extendeSecretAndAuthenticator ;
      private string aP8_error ;
   }

}
