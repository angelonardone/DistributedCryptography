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
   public class getcoincontrolcoins : GXProcedure
   {
      public getcoincontrolcoins( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcoincontrolcoins( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> aP0_coins ,
                           out decimal aP1_totalBalance ,
                           out long aP2_selectedCount ,
                           out decimal aP3_selectedTotal )
      {
         this.AV8coins = new GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem>( context, "CoinControlItem", "distributedcryptography") ;
         this.AV17totalBalance = 0 ;
         this.AV14selectedCount = 0 ;
         this.AV15selectedTotal = 0 ;
         initialize();
         ExecuteImpl();
         aP0_coins=this.AV8coins;
         aP1_totalBalance=this.AV17totalBalance;
         aP2_selectedCount=this.AV14selectedCount;
         aP3_selectedTotal=this.AV15selectedTotal;
      }

      public decimal executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> aP0_coins ,
                                 out decimal aP1_totalBalance ,
                                 out long aP2_selectedCount )
      {
         execute(out aP0_coins, out aP1_totalBalance, out aP2_selectedCount, out aP3_selectedTotal);
         return AV15selectedTotal ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> aP0_coins ,
                                 out decimal aP1_totalBalance ,
                                 out long aP2_selectedCount ,
                                 out decimal aP3_selectedTotal )
      {
         this.AV8coins = new GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem>( context, "CoinControlItem", "distributedcryptography") ;
         this.AV17totalBalance = 0 ;
         this.AV14selectedCount = 0 ;
         this.AV15selectedTotal = 0 ;
         SubmitImpl();
         aP0_coins=this.AV8coins;
         aP1_totalBalance=this.AV17totalBalance;
         aP2_selectedCount=this.AV14selectedCount;
         aP3_selectedTotal=this.AV15selectedTotal;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDTAddressHistory1 = AV9historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory1) ;
         AV9historyWithBalance = GXt_objcol_SdtSDTAddressHistory1;
         GXt_objcol_svchar2 = AV13selected;
         new GeneXus.Programs.wallet.getselectedcoins(context ).execute( out  GXt_objcol_svchar2) ;
         AV13selected = GXt_objcol_svchar2;
         AV16stillSelected = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV8coins = new GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem>( context, "CoinControlItem", "distributedcryptography");
         AV14selectedCount = 0;
         AV15selectedTotal = 0;
         AV17totalBalance = 0;
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV9historyWithBalance.Count )
         {
            AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV18GXV1));
            if ( ( AV10oneAddressHistory.gxTpr_Balance > Convert.ToDecimal( 0 )) )
            {
               AV11oneCoin = new GeneXus.Programs.wallet.SdtCoinControlItem(context);
               AV11oneCoin.gxTpr_Outpoint = StringUtil.Trim( AV10oneAddressHistory.gxTpr_Receivedtransactionid)+":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV10oneAddressHistory.gxTpr_Recivedn), 10, 0));
               AV11oneCoin.gxTpr_Transactionid = AV10oneAddressHistory.gxTpr_Receivedtransactionid;
               AV11oneCoin.gxTpr_N = AV10oneAddressHistory.gxTpr_Recivedn;
               AV11oneCoin.gxTpr_Address = AV10oneAddressHistory.gxTpr_Receivedaddress;
               AV11oneCoin.gxTpr_Amount = AV10oneAddressHistory.gxTpr_Balance;
               AV11oneCoin.gxTpr_Confirmations = AV10oneAddressHistory.gxTpr_Confirmations;
               AV11oneCoin.gxTpr_Receiveddatetime = AV10oneAddressHistory.gxTpr_Receiveddatetime;
               AV11oneCoin.gxTpr_Description = AV10oneAddressHistory.gxTpr_Description;
               AV11oneCoin.gxTpr_Selected = false;
               AV19GXV2 = 1;
               while ( AV19GXV2 <= AV13selected.Count )
               {
                  AV12oneOutpoint = ((string)AV13selected.Item(AV19GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV12oneOutpoint), StringUtil.Trim( AV11oneCoin.gxTpr_Outpoint)) == 0 )
                  {
                     AV11oneCoin.gxTpr_Selected = true;
                     if (true) break;
                  }
                  AV19GXV2 = (int)(AV19GXV2+1);
               }
               if ( AV11oneCoin.gxTpr_Selected )
               {
                  AV14selectedCount = (long)(AV14selectedCount+1);
                  AV15selectedTotal = (decimal)(AV15selectedTotal+(AV11oneCoin.gxTpr_Amount));
                  AV16stillSelected.Add(StringUtil.Trim( AV11oneCoin.gxTpr_Outpoint), 0);
               }
               AV17totalBalance = (decimal)(AV17totalBalance+(AV11oneCoin.gxTpr_Amount));
               AV8coins.Add(AV11oneCoin, 0);
            }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         if ( AV16stillSelected.Count != AV13selected.Count )
         {
            new GeneXus.Programs.wallet.setselectedcoins(context ).execute(  AV16stillSelected) ;
         }
         AV8coins.Sort("[amount]");
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
         AV9historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV13selected = new GxSimpleCollection<string>();
         GXt_objcol_svchar2 = new GxSimpleCollection<string>();
         AV16stillSelected = new GxSimpleCollection<string>();
         AV10oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV11oneCoin = new GeneXus.Programs.wallet.SdtCoinControlItem(context);
         AV12oneOutpoint = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private long AV14selectedCount ;
      private decimal AV17totalBalance ;
      private decimal AV15selectedTotal ;
      private string AV12oneOutpoint ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> AV8coins ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9historyWithBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private GxSimpleCollection<string> AV13selected ;
      private GxSimpleCollection<string> GXt_objcol_svchar2 ;
      private GxSimpleCollection<string> AV16stillSelected ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
      private GeneXus.Programs.wallet.SdtCoinControlItem AV11oneCoin ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> aP0_coins ;
      private decimal aP1_totalBalance ;
      private long aP2_selectedCount ;
      private decimal aP3_selectedTotal ;
   }

}
