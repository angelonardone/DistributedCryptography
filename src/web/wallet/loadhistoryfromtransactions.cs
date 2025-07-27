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
   public class loadhistoryfromtransactions : GXProcedure
   {
      public loadhistoryfromtransactions( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public loadhistoryfromtransactions( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                           out decimal aP2_totalBalance )
      {
         this.AV20StoredTransactions = aP0_StoredTransactions;
         this.AV12SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV13totalBalance = 0 ;
         initialize();
         ExecuteImpl();
         aP1_SDTAddressHistory=this.AV12SDTAddressHistory;
         aP2_totalBalance=this.AV13totalBalance;
      }

      public decimal executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory )
      {
         execute(aP0_StoredTransactions, out aP1_SDTAddressHistory, out aP2_totalBalance);
         return AV13totalBalance ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                 out decimal aP2_totalBalance )
      {
         this.AV20StoredTransactions = aP0_StoredTransactions;
         this.AV12SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV13totalBalance = 0 ;
         SubmitImpl();
         aP1_SDTAddressHistory=this.AV12SDTAddressHistory;
         aP2_totalBalance=this.AV13totalBalance;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV9historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV20StoredTransactions.gxTpr_Transaction.Count )
         {
            AV14TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV20StoredTransactions.gxTpr_Transaction.Item(AV21GXV1));
            AV11oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
            AV11oneAddressHistory.gxTpr_Receiveddatetime = AV14TransactionItem.gxTpr_Datetime;
            AV11oneAddressHistory.gxTpr_Receivedaddress = AV14TransactionItem.gxTpr_Scriptpubkey_address;
            AV11oneAddressHistory.gxTpr_Receivedamount = AV14TransactionItem.gxTpr_Value;
            AV11oneAddressHistory.gxTpr_Receivedtransactionid = AV14TransactionItem.gxTpr_Transactionid;
            AV11oneAddressHistory.gxTpr_Recivedn = AV14TransactionItem.gxTpr_N;
            AV11oneAddressHistory.gxTpr_Addresscreationsequence = AV14TransactionItem.gxTpr_Addresscreationsequence;
            AV11oneAddressHistory.gxTpr_Addressgeneratedtype = AV14TransactionItem.gxTpr_Addressgeneratedtype;
            AV11oneAddressHistory.gxTpr_Sentdatetime = AV14TransactionItem.gxTpr_Usedin.gxTpr_Datetime;
            AV11oneAddressHistory.gxTpr_Senttransactionid = AV14TransactionItem.gxTpr_Usedin.gxTpr_Transactionid;
            AV11oneAddressHistory.gxTpr_Description = AV14TransactionItem.gxTpr_Description;
            AV11oneAddressHistory.gxTpr_Confirmations = AV14TransactionItem.gxTpr_Confirmations;
            if ( AV11oneAddressHistory.gxTpr_Confirmations == 0 )
            {
               AV11oneAddressHistory.gxTpr_Receiveddatetime = DateTimeUtil.Now( context);
            }
            AV11oneAddressHistory.gxTpr_Balance = ((DateTime.MinValue==AV11oneAddressHistory.gxTpr_Sentdatetime) ? AV11oneAddressHistory.gxTpr_Receivedamount : (decimal)(0));
            AV12SDTAddressHistory.Add(AV11oneAddressHistory, 0);
            AV13totalBalance = (decimal)(AV13totalBalance+(AV11oneAddressHistory.gxTpr_Balance));
            if ( ( AV11oneAddressHistory.gxTpr_Balance > Convert.ToDecimal( 0 )) )
            {
               AV9historyWithBalance.Add(AV11oneAddressHistory, 0);
            }
            new GeneXus.Programs.wallet.markinalladdressasused(context ).execute(  StringUtil.Trim( AV11oneAddressHistory.gxTpr_Receivedaddress)) ;
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         AV12SDTAddressHistory.Sort("[ReceivedDateTime],ReceivedAddress");
         AV9historyWithBalance.Sort("Balance");
         new GeneXus.Programs.wallet.sethistorywithbalance(context ).execute(  AV9historyWithBalance) ;
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
         AV12SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV9historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV14TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV11oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private decimal AV13totalBalance ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV20StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV12SDTAddressHistory ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9historyWithBalance ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV14TransactionItem ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV11oneAddressHistory ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
   }

}
