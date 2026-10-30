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
   public class getspendablebalance : GXProcedure
   {
      public getspendablebalance( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getspendablebalance( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out long aP0_selectedCount ,
                           out decimal aP1_spendableBalance )
      {
         this.AV9selectedCount = 0 ;
         this.AV11spendableBalance = 0 ;
         initialize();
         ExecuteImpl();
         aP0_selectedCount=this.AV9selectedCount;
         aP1_spendableBalance=this.AV11spendableBalance;
      }

      public decimal executeUdp( out long aP0_selectedCount )
      {
         execute(out aP0_selectedCount, out aP1_spendableBalance);
         return AV11spendableBalance ;
      }

      public void executeSubmit( out long aP0_selectedCount ,
                                 out decimal aP1_spendableBalance )
      {
         this.AV9selectedCount = 0 ;
         this.AV11spendableBalance = 0 ;
         SubmitImpl();
         aP0_selectedCount=this.AV9selectedCount;
         aP1_spendableBalance=this.AV11spendableBalance;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.getcoincontrolcoins(context ).execute( out  AV8coins, out  AV12totalBalance, out  AV9selectedCount, out  AV10selectedTotal) ;
         if ( AV9selectedCount > 0 )
         {
            AV11spendableBalance = AV10selectedTotal;
         }
         else
         {
            AV11spendableBalance = AV12totalBalance;
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
         AV8coins = new GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem>( context, "CoinControlItem", "distributedcryptography");
         /* GeneXus formulas. */
      }

      private long AV9selectedCount ;
      private decimal AV11spendableBalance ;
      private decimal AV12totalBalance ;
      private decimal AV10selectedTotal ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> AV8coins ;
      private long aP0_selectedCount ;
      private decimal aP1_spendableBalance ;
   }

}
