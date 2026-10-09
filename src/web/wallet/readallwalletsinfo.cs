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
   public class readallwalletsinfo : GXProcedure
   {
      public readallwalletsinfo( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readallwalletsinfo( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_walletList )
      {
         this.AV10walletList = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_walletList=this.AV10walletList;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> executeUdp( )
      {
         execute(out aP0_walletList);
         return AV10walletList ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_walletList )
      {
         this.AV10walletList = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography") ;
         SubmitImpl();
         aP0_walletList=this.AV10walletList;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtWallet1 = AV11wallets;
         new GeneXus.Programs.wallet.readallwallets(context ).execute( out  GXt_objcol_SdtWallet1) ;
         AV11wallets = GXt_objcol_SdtWallet1;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV11wallets.Count )
         {
            AV8wallet = ((GeneXus.Programs.wallet.SdtWallet)AV11wallets.Item(AV12GXV1));
            AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
            AV9walletInfo.gxTpr_Walletname = AV8wallet.gxTpr_Walletname;
            AV9walletInfo.gxTpr_Wallettype = AV8wallet.gxTpr_Wallettype;
            AV9walletInfo.gxTpr_Useauthenticator = AV8wallet.gxTpr_Useauthenticator;
            AV9walletInfo.gxTpr_Networktype = AV8wallet.gxTpr_Networktype;
            AV9walletInfo.gxTpr_Walletreadbalanceonstart = AV8wallet.gxTpr_Walletreadbalanceonstart;
            AV10walletList.Add(AV9walletInfo, 0);
            AV12GXV1 = (int)(AV12GXV1+1);
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
         AV10walletList = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography");
         AV11wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         GXt_objcol_SdtWallet1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         AV8wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private int AV12GXV1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> AV10walletList ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> AV11wallets ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> GXt_objcol_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtWallet AV8wallet ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV9walletInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_walletList ;
   }

}
