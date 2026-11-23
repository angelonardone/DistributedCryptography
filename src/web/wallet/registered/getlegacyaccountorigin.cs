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
   public class getlegacyaccountorigin : GXProcedure
   {
      public getlegacyaccountorigin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlegacyaccountorigin( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_origin )
      {
         this.AV11origin = "" ;
         initialize();
         ExecuteImpl();
         aP0_origin=this.AV11origin;
      }

      public string executeUdp( )
      {
         execute(out aP0_origin);
         return AV11origin ;
      }

      public void executeSubmit( out string aP0_origin )
      {
         this.AV11origin = "" ;
         SubmitImpl();
         aP0_origin=this.AV11origin;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11origin = "";
         GXt_char1 = AV9fingerprint;
         new GeneXus.Programs.wallet.getmasterfingerprint(context ).execute( out  GXt_char1) ;
         AV9fingerprint = GXt_char1;
         GXt_SdtExtKeyInfo2 = AV8extKeyInfoBIP48;
         new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV8extKeyInfoBIP48 = GXt_SdtExtKeyInfo2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV9fingerprint))) && ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV8extKeyInfoBIP48.gxTpr_Extended.gxTpr_Nuterpublickey))) )
         {
            GXt_SdtWalletInfo3 = AV12walletInfo;
            new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
            AV12walletInfo = GXt_SdtWalletInfo3;
            if ( StringUtil.StrCmp(AV12walletInfo.gxTpr_Networktype, "MainNet") == 0 )
            {
               AV10keyPath = "m/48'/0'/0'/1'";
            }
            else if ( StringUtil.StrCmp(AV12walletInfo.gxTpr_Networktype, "TestNet") == 0 )
            {
               AV10keyPath = "m/48'/1'/0'/1'";
            }
            else
            {
               AV10keyPath = "m/48'/1'/0'/1'";
            }
            AV11origin = "[" + StringUtil.Lower( StringUtil.Trim( AV9fingerprint)) + StringUtil.Substring( StringUtil.Trim( AV10keyPath), 2, 100) + "]" + StringUtil.Trim( AV8extKeyInfoBIP48.gxTpr_Extended.gxTpr_Nuterpublickey);
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
         AV11origin = "";
         AV9fingerprint = "";
         GXt_char1 = "";
         AV8extKeyInfoBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV12walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV10keyPath = "";
         /* GeneXus formulas. */
      }

      private string AV9fingerprint ;
      private string GXt_char1 ;
      private string AV10keyPath ;
      private string AV11origin ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV8extKeyInfoBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV12walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private string aP0_origin ;
   }

}
