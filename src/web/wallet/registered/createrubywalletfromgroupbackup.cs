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
namespace GeneXus.Programs.wallet.registered {
   public class createrubywalletfromgroupbackup : GXProcedure
   {
      public createrubywalletfromgroupbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createrubywalletfromgroupbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_walletName ,
                           string aP2_device ,
                           string aP3_newPass ,
                           out string aP4_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV23walletName = aP1_walletName;
         this.AV9device = aP2_device;
         this.AV18newPass = aP3_newPass;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP4_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_walletName ,
                                string aP2_device ,
                                string aP3_newPass )
      {
         execute(aP0_groupId, aP1_walletName, aP2_device, aP3_newPass, out aP4_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_walletName ,
                                 string aP2_device ,
                                 string aP3_newPass ,
                                 out string aP4_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV23walletName = aP1_walletName;
         this.AV9device = aP2_device;
         this.AV18newPass = aP3_newPass;
         this.AV10error = "" ;
         SubmitImpl();
         aP4_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.registered.checkgrouprestoreallowed(context ).execute(  AV14groupId, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.checkwalletcreation(context ).execute(  AV23walletName, out  GXt_char1) ;
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
         GXt_SdtWallet2 = AV8currentWallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV8currentWallet = GXt_SdtWallet2;
         AV16networkType = AV8currentWallet.gxTpr_Networktype;
         AV17networkTypeText = AV16networkType;
         GXt_SdtGroup_SDT3 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT3) ;
         AV13group_sdt = GXt_SdtGroup_SDT3;
         AV21rubyResult.FromJSonString(AV20rubyProcessor.seedfingerprint(StringUtil.Trim( AV13group_sdt.gxTpr_Cleartextshare)), null);
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            AV13group_sdt.gxTpr_Cleartextshare = "";
            AV10error = "The recovered seed is not valid: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         AV12fingerprint = StringUtil.Trim( AV21rubyResult.gxTpr_Fingerprint);
         AV19rubyOracle.FromJSonString(AV20rubyProcessor.createoracle(), null);
         AV15json = AV20rubyProcessor.pair(StringUtil.Trim( AV9device), AV19rubyOracle.gxTpr_Oraclekey, AV19rubyOracle.gxTpr_Encryptedoracle);
         AV21rubyResult.FromJSonString(AV15json, null);
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            AV13group_sdt.gxTpr_Cleartextshare = "";
            AV10error = "This Ruby already has a wallet: connect a new Ruby or reset this one (a Ruby holds one wallet). Ruby: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         AV15json = AV20rubyProcessor.restoreseed(StringUtil.Trim( AV9device), StringUtil.Trim( AV13group_sdt.gxTpr_Cleartextshare));
         AV13group_sdt.gxTpr_Cleartextshare = "";
         AV21rubyResult.FromJSonString(AV15json, null);
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            if ( AV21rubyResult.gxTpr_Cancelled )
            {
               AV10error = "Declined on Ruby: nothing was loaded on Ruby";
            }
            else if ( AV21rubyResult.gxTpr_Haswallet )
            {
               AV10error = "This Ruby already has a wallet: connect a new Ruby or reset this one (a Ruby holds one wallet)";
            }
            else
            {
               AV10error = "Ruby: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            }
            cleanup();
            if (true) return;
         }
         AV15json = AV20rubyProcessor.unlock(StringUtil.Trim( AV9device), AV17networkTypeText, AV19rubyOracle.gxTpr_Oraclekey, AV19rubyOracle.gxTpr_Encryptedoracle);
         AV21rubyResult.FromJSonString(AV15json, null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21rubyResult.gxTpr_Encryptedoracle)) )
         {
            AV19rubyOracle.gxTpr_Encryptedoracle = AV21rubyResult.gxTpr_Encryptedoracle;
         }
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            AV10error = "Ruby: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         if ( ! AV21rubyResult.gxTpr_Unlocked )
         {
            AV10error = "Ruby did not set the PIN: the wallet was not created";
            cleanup();
            if (true) return;
         }
         AV15json = AV20rubyProcessor.getpublicinfo(StringUtil.Trim( AV9device), AV17networkTypeText);
         AV21rubyResult.FromJSonString(AV15json, null);
         if ( ! AV21rubyResult.gxTpr_Success )
         {
            AV10error = "Ruby: " + StringUtil.Trim( AV21rubyResult.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( StringUtil.Trim( AV21rubyResult.gxTpr_Fingerprint)), StringUtil.Lower( StringUtil.Trim( AV12fingerprint))) != 0 )
         {
            AV10error = "Ruby holds another wallet (fingerprint " + StringUtil.Upper( StringUtil.Trim( AV21rubyResult.gxTpr_Fingerprint)) + "), not the recovered one (" + StringUtil.Upper( StringUtil.Trim( AV12fingerprint)) + "): the wallet was not created";
            cleanup();
            if (true) return;
         }
         AV11extendeSecretAndAuthenticator.gxTpr_Networktype = AV16networkType;
         AV11extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = "";
         AV11extendeSecretAndAuthenticator.gxTpr_Oraclekey = AV19rubyOracle.gxTpr_Oraclekey;
         AV22wallet.gxTpr_Walletname = AV23walletName;
         AV22wallet.gxTpr_Networktype = AV16networkType;
         AV22wallet.gxTpr_Wallettype = "BIP86";
         AV22wallet.gxTpr_Keystorage = "Ruby";
         AV22wallet.gxTpr_Rubydevice = StringUtil.Trim( AV9device);
         AV22wallet.gxTpr_Rubyfingerprint = AV21rubyResult.gxTpr_Fingerprint;
         AV22wallet.gxTpr_Encryptedoracle = AV19rubyOracle.gxTpr_Encryptedoracle;
         AV22wallet.gxTpr_Useauthenticator = false;
         GXt_char1 = AV10error;
         GXt_char4 = AV22wallet.gxTpr_Encryptedsecret;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV18newPass,  AV11extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char4, ref  GXt_char1) ;
         AV22wallet.gxTpr_Encryptedsecret = GXt_char4;
         AV10error = GXt_char1;
         AV11extendeSecretAndAuthenticator.gxTpr_Oraclekey = "";
         AV19rubyOracle.gxTpr_Oraclekey = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV22wallet) ;
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
         AV8currentWallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV16networkType = "";
         AV17networkTypeText = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT3 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV20rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV12fingerprint = "";
         AV19rubyOracle = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV15json = "";
         AV11extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV22wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char1 = "";
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private string AV23walletName ;
      private string AV18newPass ;
      private string AV10error ;
      private string AV16networkType ;
      private string AV12fingerprint ;
      private string GXt_char1 ;
      private string GXt_char4 ;
      private string AV15json ;
      private string AV9device ;
      private string AV17networkTypeText ;
      private Guid AV14groupId ;
      private GeneXus.Programs.wallet.SdtWallet AV8currentWallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT3 ;
      private GeneXus.Programs.wallet.SdtRubyResult AV21rubyResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV20rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV19rubyOracle ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV11extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.SdtWallet AV22wallet ;
      private string aP4_error ;
   }

}
