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
   public class updatetransactionsaftercoinsent : GXProcedure
   {
      public updatetransactionsaftercoinsent( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updatetransactionsaftercoinsent( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           string aP1_TransactionId ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                           out string aP3_error )
      {
         this.AV19transactionFileName = aP0_transactionFileName;
         this.AV18TransactionId = aP1_TransactionId;
         this.AV14transactionsToSend = aP2_transactionsToSend;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                string aP1_TransactionId ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend )
      {
         execute(aP0_transactionFileName, aP1_TransactionId, aP2_transactionsToSend, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 string aP1_TransactionId ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                 out string aP3_error )
      {
         this.AV19transactionFileName = aP0_transactionFileName;
         this.AV18TransactionId = aP1_TransactionId;
         this.AV14transactionsToSend = aP2_transactionsToSend;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV19transactionFileName, out  AV8error), null);
         AV20sentDateTime = DateTimeUtil.Now( context);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV21GXV1 = 1;
            while ( AV21GXV1 <= AV16StoredTransactions.gxTpr_Transaction.Count )
            {
               AV17oneStoredTransaction = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV16StoredTransactions.gxTpr_Transaction.Item(AV21GXV1));
               AV22GXV2 = 1;
               while ( AV22GXV2 <= AV14transactionsToSend.Count )
               {
                  AV15oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV14transactionsToSend.Item(AV22GXV2));
                  AV15oneTransaction.gxTpr_Sentdatetime = AV20sentDateTime;
                  if ( ( StringUtil.StrCmp(AV17oneStoredTransaction.gxTpr_Transactionid, AV15oneTransaction.gxTpr_Receivedtransactionid) == 0 ) && ( StringUtil.StrCmp(AV17oneStoredTransaction.gxTpr_Scriptpubkey_address, AV15oneTransaction.gxTpr_Receivedaddress) == 0 ) )
                  {
                     AV17oneStoredTransaction.gxTpr_Usedin.gxTpr_Datetime = AV15oneTransaction.gxTpr_Sentdatetime;
                     AV17oneStoredTransaction.gxTpr_Usedin.gxTpr_Transactionid = StringUtil.Trim( AV18TransactionId);
                     AV17oneStoredTransaction.gxTpr_Description = StringUtil.Trim( AV15oneTransaction.gxTpr_Description);
                     AV15oneTransaction.gxTpr_Senttransactionid = StringUtil.Trim( AV18TransactionId);
                  }
                  AV22GXV2 = (int)(AV22GXV2+1);
               }
               AV21GXV1 = (int)(AV21GXV1+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_char1 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV19transactionFileName,  AV16StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
               AV8error = GXt_char1;
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
         AV8error = "";
         AV16StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV20sentDateTime = (DateTime)(DateTime.MinValue);
         AV17oneStoredTransaction = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV15oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private string AV19transactionFileName ;
      private string AV18TransactionId ;
      private string AV8error ;
      private string GXt_char1 ;
      private DateTime AV20sentDateTime ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV14transactionsToSend ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV16StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV17oneStoredTransaction ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV15oneTransaction ;
      private string aP3_error ;
   }

}
