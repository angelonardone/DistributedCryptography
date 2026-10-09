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
   public class getwalletinfo : GXProcedure
   {
      public getwalletinfo( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwalletinfo( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo )
      {
         this.AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         initialize();
         ExecuteImpl();
         aP0_walletInfo=this.AV9walletInfo;
      }

      public GeneXus.Programs.wallet.SdtWalletInfo executeUdp( )
      {
         execute(out aP0_walletInfo);
         return AV9walletInfo ;
      }

      public void executeSubmit( out GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo )
      {
         this.AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         SubmitImpl();
         aP0_walletInfo=this.AV9walletInfo;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV8wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV8wallet = GXt_SdtWallet1;
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV9walletInfo.gxTpr_Walletname = AV8wallet.gxTpr_Walletname;
         AV9walletInfo.gxTpr_Wallettype = AV8wallet.gxTpr_Wallettype;
         AV9walletInfo.gxTpr_Useauthenticator = AV8wallet.gxTpr_Useauthenticator;
         AV9walletInfo.gxTpr_Networktype = AV8wallet.gxTpr_Networktype;
         AV9walletInfo.gxTpr_Walletreadbalanceonstart = AV8wallet.gxTpr_Walletreadbalanceonstart;
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
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV8wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         /* GeneXus formulas. */
      }

      private GeneXus.Programs.wallet.SdtWalletInfo AV9walletInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV8wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtWalletInfo aP0_walletInfo ;
   }

}
