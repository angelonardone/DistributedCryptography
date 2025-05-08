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
namespace GeneXus.Programs.wallet {
   public class readallwallets : GXProcedure
   {
      public readallwallets( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readallwallets( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> aP0_wallets )
      {
         this.AV11wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_wallets=this.AV11wallets;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> executeUdp( )
      {
         execute(out aP0_wallets);
         return AV11wallets ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> aP0_wallets )
      {
         this.AV11wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography") ;
         SubmitImpl();
         aP0_wallets=this.AV11wallets;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9directory.Source = "Wallets";
         if ( AV9directory.Exists() )
         {
            AV15GXV2 = 1;
            AV14GXV1 = AV9directory.GetDirectories();
            while ( AV15GXV2 <= AV14GXV1.ItemCount )
            {
               AV12walletDirectory = AV14GXV1.Item(AV15GXV2);
               AV17GXV4 = 1;
               AV16GXV3 = AV12walletDirectory.GetFiles("*.json");
               while ( AV17GXV4 <= AV16GXV3.ItemCount )
               {
                  AV8auxFile = AV16GXV3.Item(AV17GXV4);
                  GXt_boolean1 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
                  GXt_boolean2 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
                  AV13fileName = AV8auxFile.GetPath() + (GXt_boolean2 ? "/" : "\\") + AV8auxFile.GetName();
                  GXt_SdtWallet3 = AV10wallet;
                  new GeneXus.Programs.wallet.readwallet(context ).execute(  AV13fileName, out  GXt_SdtWallet3) ;
                  AV10wallet = GXt_SdtWallet3;
                  AV10wallet.gxTpr_Walletfilename = AV13fileName;
                  AV11wallets.Add(AV10wallet, 0);
                  AV17GXV4 = (int)(AV17GXV4+1);
               }
               AV15GXV2 = (int)(AV15GXV2+1);
            }
         }
         else
         {
            AV9directory.Create();
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
         AV11wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV14GXV1 = new GxDirectoryCollection();
         AV12walletDirectory = new GxDirectory(context.GetPhysicalPath());
         AV16GXV3 = new GxFileCollection();
         AV8auxFile = new GxFile(context.GetPhysicalPath());
         AV13fileName = "";
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV2 ;
      private int AV17GXV4 ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private string AV13fileName ;
      private GxFile AV8auxFile ;
      private GxDirectory AV9directory ;
      private GxDirectory AV12walletDirectory ;
      private GxFileCollection AV16GXV3 ;
      private GxDirectoryCollection AV14GXV1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> AV11wallets ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> aP0_wallets ;
   }

}
