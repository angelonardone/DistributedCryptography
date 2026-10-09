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
   public class selectwalletbyname : GXProcedure
   {
      public selectwalletbyname( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public selectwalletbyname( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           out GeneXus.Programs.wallet.SdtWalletInfo aP1_walletInfo )
      {
         this.AV10walletName = aP0_walletName;
         this.AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         initialize();
         ExecuteImpl();
         aP1_walletInfo=this.AV9walletInfo;
      }

      public GeneXus.Programs.wallet.SdtWalletInfo executeUdp( string aP0_walletName )
      {
         execute(aP0_walletName, out aP1_walletInfo);
         return AV9walletInfo ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 out GeneXus.Programs.wallet.SdtWalletInfo aP1_walletInfo )
      {
         this.AV10walletName = aP0_walletName;
         this.AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context) ;
         SubmitImpl();
         aP1_walletInfo=this.AV9walletInfo;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_objcol_SdtWallet1 = AV11wallets;
         new GeneXus.Programs.wallet.readallwallets(context ).execute( out  GXt_objcol_SdtWallet1) ;
         AV11wallets = GXt_objcol_SdtWallet1;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV11wallets.Count )
         {
            AV8wallet = ((GeneXus.Programs.wallet.SdtWallet)AV11wallets.Item(AV12GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV8wallet.gxTpr_Walletname), StringUtil.Trim( AV10walletName)) == 0 )
            {
               new GeneXus.Programs.wallet.setwallet(context ).execute(  AV8wallet) ;
               GXt_SdtWalletInfo2 = AV9walletInfo;
               new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo2) ;
               AV9walletInfo = GXt_SdtWalletInfo2;
            }
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
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV11wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         GXt_objcol_SdtWallet1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         AV8wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWalletInfo2 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private int AV12GXV1 ;
      private string AV10walletName ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV9walletInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> AV11wallets ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> GXt_objcol_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtWallet AV8wallet ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo aP1_walletInfo ;
   }

}
