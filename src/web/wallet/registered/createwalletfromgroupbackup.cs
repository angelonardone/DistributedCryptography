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
   public class createwalletfromgroupbackup : GXProcedure
   {
      public createwalletfromgroupbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createwalletfromgroupbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_walletName ,
                           string aP2_newPass ,
                           out string aP3_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV18walletName = aP1_walletName;
         this.AV16newPass = aP2_newPass;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_walletName ,
                                string aP2_newPass )
      {
         execute(aP0_groupId, aP1_walletName, aP2_newPass, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_walletName ,
                                 string aP2_newPass ,
                                 out string aP3_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV18walletName = aP1_walletName;
         this.AV16newPass = aP2_newPass;
         this.AV9error = "" ;
         SubmitImpl();
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.registered.checkgrouprestoreallowed(context ).execute(  AV14groupId, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT2 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT2) ;
         AV13group_sdt = GXt_SdtGroup_SDT2;
         GXt_SdtWallet3 = AV8currentWallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet3) ;
         AV8currentWallet = GXt_SdtWallet3;
         AV15networkType = AV8currentWallet.gxTpr_Networktype;
         AV11extKeyCreate.gxTpr_Keypath = "";
         AV11extKeyCreate.gxTpr_Networktype = AV15networkType;
         AV11extKeyCreate.gxTpr_Createextkeytype = 70;
         AV11extKeyCreate.gxTpr_Extendedprivatekey = StringUtil.Trim( AV13group_sdt.gxTpr_Cleartextshare);
         GXt_char1 = AV9error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV11extKeyCreate,  "", out  AV12extKeyInfo, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV10extendeSecretAndAuthenticator.gxTpr_Networktype = AV15networkType;
            AV10extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = "";
            AV10extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV12extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
            GXt_char1 = AV9error;
            GXt_char4 = AV17wallet.gxTpr_Encryptedsecret;
            new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV16newPass,  AV10extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char4, ref  GXt_char1) ;
            AV17wallet.gxTpr_Encryptedsecret = GXt_char4;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV17wallet.gxTpr_Wallettype = "BIP86";
               AV17wallet.gxTpr_Networktype = AV15networkType;
               AV17wallet.gxTpr_Walletname = AV18walletName;
               new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV17wallet) ;
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
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8currentWallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV15networkType = "";
         AV11extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV12extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV10extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         GXt_char1 = "";
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private string AV18walletName ;
      private string AV16newPass ;
      private string AV9error ;
      private string AV15networkType ;
      private string GXt_char1 ;
      private string GXt_char4 ;
      private Guid AV14groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.SdtWallet AV8currentWallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV11extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfo ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV10extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private string aP3_error ;
   }

}
