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
   public class rubysetsessionkeys : GXProcedure
   {
      public rubysetsessionkeys( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public rubysetsessionkeys( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtRubyResult aP0_rubyPublic ,
                           GeneXus.Programs.wallet.SdtRubyResult aP1_rubyKeys ,
                           out string aP2_error )
      {
         this.AV17rubyPublic = aP0_rubyPublic;
         this.AV16rubyKeys = aP1_rubyKeys;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtRubyResult aP0_rubyPublic ,
                                GeneXus.Programs.wallet.SdtRubyResult aP1_rubyKeys )
      {
         execute(aP0_rubyPublic, aP1_rubyKeys, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtRubyResult aP0_rubyPublic ,
                                 GeneXus.Programs.wallet.SdtRubyResult aP1_rubyKeys ,
                                 out string aP2_error )
      {
         this.AV17rubyPublic = aP0_rubyPublic;
         this.AV16rubyKeys = aP1_rubyKeys;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         AV11extKeyInfo = AV17rubyPublic.gxTpr_Account;
         new GeneXus.Programs.wallet.setextkey(context ).execute(  AV11extKeyInfo) ;
         AV11extKeyInfo = AV17rubyPublic.gxTpr_Bip48;
         new GeneXus.Programs.wallet.setextkeybip48(context ).execute(  AV11extKeyInfo) ;
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.rubykeyinfofromextkey(context ).execute(  AV16rubyKeys.gxTpr_Jsonfiles, out  AV15keyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "Ruby JSON files key: " + StringUtil.Trim( AV9error);
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.setdefaultjasonkey(context ).execute(  AV15keyInfo) ;
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.rubykeyinfofromextkey(context ).execute(  AV16rubyKeys.gxTpr_Login, out  AV15keyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV19WebSession.Set("LoginDistCryptKey", AV15keyInfo.ToJSonString(false, true));
         }
         else
         {
            AV19WebSession.Set("LoginDistCryptKey", "");
         }
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.rubykeyinfofromextkey(context ).execute(  AV16rubyKeys.gxTpr_Fileencryption, out  AV15keyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV19WebSession.Set("FileEncKey", AV15keyInfo.ToJSonString(false, true));
         }
         else
         {
            AV19WebSession.Set("FileEncKey", "");
         }
         GXt_SdtExternalUser2 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV10externalUser = GXt_SdtExternalUser2;
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.rubykeyinfofromextkey(context ).execute(  AV16rubyKeys.gxTpr_Chat, out  AV15keyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV10externalUser.gxTpr_Chatkeyinfo = AV15keyInfo;
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.rubykeyinfofromextkey(context ).execute(  AV16rubyKeys.gxTpr_Groups, out  AV15keyInfo, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV10externalUser.gxTpr_Groupskeyinfo = AV15keyInfo;
               new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV10externalUser) ;
               GXt_char1 = AV9error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "distcrypt.conf",  AV10externalUser.ToJSonString(false, true), out  GXt_char1) ;
               AV9error = GXt_char1;
            }
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "Ruby chat and groups keys: " + StringUtil.Trim( AV9error);
            cleanup();
            if (true) return;
         }
         AV12HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV8auxError), null);
         if ( AV12HSMconfigSDT.gxTpr_Isactive )
         {
            GXt_SdtWallet3 = AV18wallet;
            new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet3) ;
            AV18wallet = GXt_SdtWallet3;
            AV13HsmManager.clear();
            AV14initOk = AV13HsmManager.initialize(AV16rubyKeys.gxTpr_Hsm, AV18wallet.gxTpr_Networktype);
            if ( ! AV14initOk )
            {
               AV9error = "There is a problem initializing the HSM module";
            }
         }
         AV15keyInfo.gxTpr_Privatekey = "";
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
         AV11extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV15keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV19WebSession = context.GetSession();
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char1 = "";
         AV12HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         AV8auxError = "";
         AV18wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char1 ;
      private string AV8auxError ;
      private bool AV14initOk ;
      private IGxSession AV19WebSession ;
      private GeneXus.Programs.wallet.SdtRubyResult AV17rubyPublic ;
      private GeneXus.Programs.wallet.SdtRubyResult AV16rubyKeys ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV11extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV15keyInfo ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV12HSMconfigSDT ;
      private GeneXus.Programs.wallet.SdtWallet AV18wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
      private GeneXus.Programs.hsm.SdtHsmManager AV13HsmManager ;
      private string aP2_error ;
   }

}
