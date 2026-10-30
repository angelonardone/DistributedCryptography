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
   public class getwallettransactions : GXProcedure
   {
      public getwallettransactions( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwallettransactions( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> aP1_transactions ,
                           out decimal aP2_confirmedBalance ,
                           out decimal aP3_pendingBalance ,
                           out string aP4_error )
      {
         this.AV29transactionFileName = aP0_transactionFileName;
         this.AV30transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography") ;
         this.AV9confirmedBalance = 0 ;
         this.AV21pendingBalance = 0 ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_transactions=this.AV30transactions;
         aP2_confirmedBalance=this.AV9confirmedBalance;
         aP3_pendingBalance=this.AV21pendingBalance;
         aP4_error=this.AV11error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> aP1_transactions ,
                                out decimal aP2_confirmedBalance ,
                                out decimal aP3_pendingBalance )
      {
         execute(aP0_transactionFileName, out aP1_transactions, out aP2_confirmedBalance, out aP3_pendingBalance, out aP4_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> aP1_transactions ,
                                 out decimal aP2_confirmedBalance ,
                                 out decimal aP3_pendingBalance ,
                                 out string aP4_error )
      {
         this.AV29transactionFileName = aP0_transactionFileName;
         this.AV30transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography") ;
         this.AV9confirmedBalance = 0 ;
         this.AV21pendingBalance = 0 ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_transactions=this.AV30transactions;
         aP2_confirmedBalance=this.AV9confirmedBalance;
         aP3_pendingBalance=this.AV21pendingBalance;
         aP4_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV30transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography");
         AV9confirmedBalance = 0;
         AV21pendingBalance = 0;
         AV28StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV29transactionFileName, out  AV11error), null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV10dbTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV8auxError), null);
         AV32txids = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV36GXV1 = 1;
         while ( AV36GXV1 <= AV28StoredTransactions.gxTpr_Transaction.Count )
         {
            AV31TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV28StoredTransactions.gxTpr_Transaction.Item(AV36GXV1));
            AV18oneTxid = StringUtil.Trim( AV31TransactionItem.gxTpr_Transactionid);
            /* Execute user subroutine: 'ADD TXID' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV31TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) && ( StringUtil.StrCmp(AV31TransactionItem.gxTpr_Usedin.gxTpr_Transactionid, "MULTISIGNATURE IN PROGRESS") != 0 ) )
            {
               AV18oneTxid = StringUtil.Trim( AV31TransactionItem.gxTpr_Usedin.gxTpr_Transactionid);
               /* Execute user subroutine: 'ADD TXID' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
            }
            if ( (DateTime.MinValue==AV31TransactionItem.gxTpr_Usedin.gxTpr_Datetime) )
            {
               if ( AV31TransactionItem.gxTpr_Confirmations >= 1 )
               {
                  AV9confirmedBalance = (decimal)(AV9confirmedBalance+(AV31TransactionItem.gxTpr_Value));
               }
               else
               {
                  AV21pendingBalance = (decimal)(AV21pendingBalance+(AV31TransactionItem.gxTpr_Value));
               }
            }
            AV36GXV1 = (int)(AV36GXV1+1);
         }
         AV37GXV2 = 1;
         while ( AV37GXV2 <= AV32txids.Count )
         {
            AV18oneTxid = ((string)AV32txids.Item(AV37GXV2));
            AV17oneTx = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
            AV17oneTx.gxTpr_Transactionid = StringUtil.Trim( AV18oneTxid);
            AV23receivedOurs = 0;
            AV27spentOurs = 0;
            AV20outputsTotal = 0;
            AV19outputsKnown = false;
            AV14hasReceived = false;
            AV22receivedLabel = "";
            AV26spentLabel = "";
            AV25spentDate = (DateTime)(DateTime.MinValue);
            AV35confirmationsKnown = false;
            AV38GXV3 = 1;
            while ( AV38GXV3 <= AV28StoredTransactions.gxTpr_Transaction.Count )
            {
               AV31TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV28StoredTransactions.gxTpr_Transaction.Item(AV38GXV3));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV31TransactionItem.gxTpr_Transactionid), StringUtil.Trim( AV18oneTxid)) == 0 )
               {
                  AV23receivedOurs = (decimal)(AV23receivedOurs+(AV31TransactionItem.gxTpr_Value));
                  AV14hasReceived = true;
                  AV17oneTx.gxTpr_Datetime = AV31TransactionItem.gxTpr_Datetime;
                  AV17oneTx.gxTpr_Confirmations = AV31TransactionItem.gxTpr_Confirmations;
                  AV35confirmationsKnown = true;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV22receivedLabel)) && String.IsNullOrEmpty(StringUtil.RTrim( AV31TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) )
                  {
                     AV22receivedLabel = StringUtil.Trim( AV31TransactionItem.gxTpr_Description);
                  }
               }
               if ( StringUtil.StrCmp(StringUtil.Trim( AV31TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.Trim( AV18oneTxid)) == 0 )
               {
                  AV27spentOurs = (decimal)(AV27spentOurs+(AV31TransactionItem.gxTpr_Value));
                  AV25spentDate = AV31TransactionItem.gxTpr_Usedin.gxTpr_Datetime;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV26spentLabel)) )
                  {
                     AV26spentLabel = StringUtil.Trim( AV31TransactionItem.gxTpr_Description);
                  }
                  if ( ! AV19outputsKnown && ( AV31TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count > 0 ) )
                  {
                     AV19outputsKnown = true;
                     AV39GXV4 = 1;
                     while ( AV39GXV4 <= AV31TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count )
                     {
                        AV33usedToItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV31TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV39GXV4));
                        AV20outputsTotal = (decimal)(AV20outputsTotal+(AV33usedToItem.gxTpr_Value));
                        AV39GXV4 = (int)(AV39GXV4+1);
                     }
                  }
               }
               AV38GXV3 = (int)(AV38GXV3+1);
            }
            if ( ( AV27spentOurs > Convert.ToDecimal( 0 )) )
            {
               AV17oneTx.gxTpr_Received = (decimal)(0);
               AV17oneTx.gxTpr_Sent = (decimal)(AV27spentOurs-AV23receivedOurs);
               if ( ! AV19outputsKnown )
               {
                  AV17oneTx.gxTpr_Kind = "Sent, not seen yet";
                  AV17oneTx.gxTpr_Pending = true;
               }
               else if ( AV27spentOurs >= AV20outputsTotal )
               {
                  AV17oneTx.gxTpr_Fee = (decimal)(AV27spentOurs-AV20outputsTotal);
                  if ( ( AV20outputsTotal - AV23receivedOurs <= Convert.ToDecimal( 0 )) )
                  {
                     AV17oneTx.gxTpr_Kind = "To yourself";
                  }
                  else
                  {
                     AV17oneTx.gxTpr_Kind = "Sent";
                  }
               }
               else
               {
                  AV17oneTx.gxTpr_Kind = "Sent";
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV26spentLabel)) )
               {
                  AV17oneTx.gxTpr_Label = AV26spentLabel;
               }
               else
               {
                  AV17oneTx.gxTpr_Label = AV22receivedLabel;
               }
               if ( ! AV14hasReceived )
               {
                  AV17oneTx.gxTpr_Datetime = AV25spentDate;
               }
            }
            else
            {
               AV17oneTx.gxTpr_Kind = "Received";
               AV17oneTx.gxTpr_Received = AV23receivedOurs;
               AV17oneTx.gxTpr_Sent = (decimal)(0);
               AV17oneTx.gxTpr_Label = AV22receivedLabel;
            }
            AV15hex = "";
            AV40GXV5 = 1;
            while ( AV40GXV5 <= AV10dbTransactions.Count )
            {
               AV16oneDbTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV10dbTransactions.Item(AV40GXV5));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV16oneDbTransaction.gxTpr_Transactionid), StringUtil.Trim( AV18oneTxid)) == 0 )
               {
                  AV15hex = StringUtil.Trim( AV16oneDbTransaction.gxTpr_Hex);
                  if ( ! AV35confirmationsKnown )
                  {
                     AV17oneTx.gxTpr_Confirmations = AV16oneDbTransaction.gxTpr_Confirmations;
                     AV35confirmationsKnown = true;
                  }
                  if (true) break;
               }
               AV40GXV5 = (int)(AV40GXV5+1);
            }
            if ( AV17oneTx.gxTpr_Confirmations == 0 )
            {
               AV17oneTx.gxTpr_Pending = true;
            }
            if ( ( AV17oneTx.gxTpr_Fee > Convert.ToDecimal( 0 )) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV15hex)) )
            {
               new GeneXus.Programs.wallet.gettransactionvsize(context ).execute(  AV15hex, out  AV34virtualSize, out  AV8auxError) ;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8auxError)) && ( AV34virtualSize > 0 ) )
               {
                  AV17oneTx.gxTpr_Virtualsize = AV34virtualSize;
                  AV17oneTx.gxTpr_Feerate = NumberUtil.Round( AV17oneTx.gxTpr_Fee*100000000/ (decimal)(AV34virtualSize), 2);
               }
            }
            if ( (DateTime.MinValue==AV17oneTx.gxTpr_Datetime) )
            {
               AV17oneTx.gxTpr_Datetime = DateTimeUtil.Now( context);
            }
            AV30transactions.Add(AV17oneTx, 0);
            AV37GXV2 = (int)(AV37GXV2+1);
         }
         AV30transactions.Sort("dateTime,transactionId");
         AV24runningBalance = 0;
         AV41GXV6 = 1;
         while ( AV41GXV6 <= AV30transactions.Count )
         {
            AV17oneTx = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV30transactions.Item(AV41GXV6));
            AV24runningBalance = (decimal)(AV24runningBalance+(AV17oneTx.gxTpr_Received-AV17oneTx.gxTpr_Sent));
            AV17oneTx.gxTpr_Balance = AV24runningBalance;
            AV41GXV6 = (int)(AV41GXV6+1);
         }
         AV30transactions.Sort("[dateTime],[transactionId]");
         cleanup();
      }

      protected void S111( )
      {
         /* 'ADD TXID' Routine */
         returnInSub = false;
         AV13found = false;
         AV42GXV7 = 1;
         while ( AV42GXV7 <= AV32txids.Count )
         {
            AV12existingTxid = ((string)AV32txids.Item(AV42GXV7));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV12existingTxid), StringUtil.Trim( AV18oneTxid)) == 0 )
            {
               AV13found = true;
               if (true) break;
            }
            AV42GXV7 = (int)(AV42GXV7+1);
         }
         if ( ! AV13found && ! String.IsNullOrEmpty(StringUtil.RTrim( AV18oneTxid)) )
         {
            AV32txids.Add(StringUtil.Trim( AV18oneTxid), 0);
         }
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
         AV30transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography");
         AV11error = "";
         AV28StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV10dbTransactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV8auxError = "";
         AV32txids = new GxSimpleCollection<string>();
         AV31TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV18oneTxid = "";
         AV17oneTx = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
         AV22receivedLabel = "";
         AV26spentLabel = "";
         AV25spentDate = (DateTime)(DateTime.MinValue);
         AV33usedToItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
         AV15hex = "";
         AV16oneDbTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         AV12existingTxid = "";
         /* GeneXus formulas. */
      }

      private int AV36GXV1 ;
      private int AV37GXV2 ;
      private int AV38GXV3 ;
      private int AV39GXV4 ;
      private int AV40GXV5 ;
      private int AV41GXV6 ;
      private int AV42GXV7 ;
      private long AV34virtualSize ;
      private decimal AV9confirmedBalance ;
      private decimal AV21pendingBalance ;
      private decimal AV23receivedOurs ;
      private decimal AV27spentOurs ;
      private decimal AV20outputsTotal ;
      private decimal AV24runningBalance ;
      private string AV29transactionFileName ;
      private string AV11error ;
      private string AV8auxError ;
      private DateTime AV25spentDate ;
      private bool returnInSub ;
      private bool AV19outputsKnown ;
      private bool AV14hasReceived ;
      private bool AV35confirmationsKnown ;
      private bool AV13found ;
      private string AV15hex ;
      private string AV18oneTxid ;
      private string AV22receivedLabel ;
      private string AV26spentLabel ;
      private string AV12existingTxid ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> AV30transactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV28StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV10dbTransactions ;
      private GxSimpleCollection<string> AV32txids ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV31TransactionItem ;
      private GeneXus.Programs.wallet.SdtWalletTransactionItem AV17oneTx ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem AV33usedToItem ;
      private GeneXus.Programs.sudodb.SdtTransaction AV16oneDbTransaction ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> aP1_transactions ;
      private decimal aP2_confirmedBalance ;
      private decimal aP3_pendingBalance ;
      private string aP4_error ;
   }

}
