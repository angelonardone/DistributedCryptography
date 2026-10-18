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
namespace GeneXus.Programs.distcrypt {
   public class logintoserver : GXProcedure
   {
      public logintoserver( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public logintoserver( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV9error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV9error = "" ;
         SubmitImpl();
         aP0_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtKeyInfo1 = AV13keyInfo;
         new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV13keyInfo = GXt_SdtKeyInfo1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13keyInfo.gxTpr_Privatekey)) )
         {
            AV9error = "Open the wallet to log in to the server";
            cleanup();
            if (true) return;
         }
         GXt_SdtWallet2 = AV21wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV21wallet = GXt_SdtWallet2;
         GXt_SdtServerParameters3 = AV17serverParameters;
         new GeneXus.Programs.distcrypt.getserverparameters(context ).execute( out  GXt_SdtServerParameters3) ;
         AV17serverParameters = GXt_SdtServerParameters3;
         new GeneXus.Programs.distcrypt.restgetchallenge(context ).execute(  AV18ServerUrlTemplatingVar, out  AV8challengeOut, out  AV11HttpMessage, out  AV12IsSuccess) ;
         if ( ! AV12IsSuccess || ! String.IsNullOrEmpty(StringUtil.RTrim( AV8challengeOut.gxTpr_Error)) || String.IsNullOrEmpty(StringUtil.RTrim( AV8challengeOut.gxTpr_Challenge)) )
         {
            AV9error = "The server did not give a login challenge: " + StringUtil.Trim( AV11HttpMessage.gxTpr_Description) + StringUtil.Trim( AV8challengeOut.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         AV16message = "DistributedCryptography login" + StringUtil.Chr( 10) + "server: " + StringUtil.Lower( StringUtil.Trim( AV17serverParameters.gxTpr_Serverhost)) + StringUtil.Chr( 10) + "challenge: " + StringUtil.Lower( StringUtil.Trim( AV8challengeOut.gxTpr_Challenge));
         GXt_char4 = AV9error;
         new GeneXus.Programs.nbitcoin.signbip322(context ).execute(  AV13keyInfo.gxTpr_Privatekey,  AV13keyInfo.gxTpr_Address,  AV21wallet.gxTpr_Networktype,  AV16message, out  AV19signature, out  GXt_char4) ;
         AV9error = GXt_char4;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "Problems signing the login: " + StringUtil.Trim( AV9error);
            cleanup();
            if (true) return;
         }
         AV14loginIn.gxTpr_Username = StringUtil.Trim( AV13keyInfo.gxTpr_Address);
         AV14loginIn.gxTpr_Signature = StringUtil.Trim( AV19signature);
         AV14loginIn.gxTpr_Challenge = StringUtil.Trim( AV8challengeOut.gxTpr_Challenge);
         AV14loginIn.gxTpr_Userpubkey = StringUtil.Trim( AV13keyInfo.gxTpr_Publickey);
         new GeneXus.Programs.distcrypt.restlogin(context ).execute(  AV18ServerUrlTemplatingVar,  AV14loginIn, out  AV15loginOut, out  AV11HttpMessage, out  AV12IsSuccess) ;
         if ( ! AV12IsSuccess || ! String.IsNullOrEmpty(StringUtil.RTrim( AV15loginOut.gxTpr_Error)) || String.IsNullOrEmpty(StringUtil.RTrim( AV15loginOut.gxTpr_Token)) )
         {
            AV9error = "The server refused the login: " + StringUtil.Trim( AV11HttpMessage.gxTpr_Description) + StringUtil.Trim( AV15loginOut.gxTpr_Error);
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUser5 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser5) ;
         AV10externalUser = GXt_SdtExternalUser5;
         AV10externalUser.gxTpr_Externaltoken = StringUtil.Trim( AV15loginOut.gxTpr_Token);
         AV10externalUser.gxTpr_Userinfo = new();
         AV10externalUser.gxTpr_Userinfo.gxTpr_Username = StringUtil.Trim( AV13keyInfo.gxTpr_Address);
         AV10externalUser.gxTpr_Userinfo.gxTpr_Plan = StringUtil.Trim( AV15loginOut.gxTpr_Userplan);
         AV10externalUser.gxTpr_Keyinfo = AV13keyInfo;
         GXt_char4 = AV9error;
         new GeneXus.Programs.distcrypt.updateandsaveexternaluser(context ).execute(  AV10externalUser, out  GXt_char4) ;
         AV9error = GXt_char4;
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
         AV13keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV21wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV17serverParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         GXt_SdtServerParameters3 = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV18ServerUrlTemplatingVar = new GXProperties();
         AV8challengeOut = new SdtgetChallenge__postOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV16message = "";
         AV19signature = "";
         AV14loginIn = new Sdtlogin__postInput(context);
         AV15loginOut = new Sdtlogin__postOutput(context);
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser5 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char4 ;
      private bool AV12IsSuccess ;
      private string AV16message ;
      private string AV19signature ;
      private GXProperties AV18ServerUrlTemplatingVar ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV13keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV21wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV17serverParameters ;
      private GeneXus.Programs.distcrypt.SdtServerParameters GXt_SdtServerParameters3 ;
      private SdtgetChallenge__postOutput AV8challengeOut ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private Sdtlogin__postInput AV14loginIn ;
      private Sdtlogin__postOutput AV15loginOut ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser5 ;
      private string aP0_error ;
   }

}
