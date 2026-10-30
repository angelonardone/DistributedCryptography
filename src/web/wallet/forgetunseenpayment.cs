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
   public class forgetunseenpayment : GXProcedure
   {
      public forgetunseenpayment( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public forgetunseenpayment( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           string aP1_transactionId ,
                           out string aP2_error )
      {
         this.AV14transactionFileName = aP0_transactionFileName;
         this.AV15transactionId = aP1_transactionId;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV11error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                string aP1_transactionId )
      {
         execute(aP0_transactionFileName, aP1_transactionId, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 string aP1_transactionId ,
                                 out string aP2_error )
      {
         this.AV14transactionFileName = aP0_transactionFileName;
         this.AV15transactionId = aP1_transactionId;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV14transactionFileName, out  AV11error), null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV10count = 0;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV13StoredTransactions.gxTpr_Transaction.Count )
         {
            AV16TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV13StoredTransactions.gxTpr_Transaction.Item(AV17GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV16TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.Trim( AV15transactionId)) == 0 )
            {
               AV10count = (short)(AV10count+1);
               if ( AV16TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count > 0 )
               {
                  AV11error = "The server has seen this payment: it can not be forgotten";
                  cleanup();
                  if (true) return;
               }
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         if ( AV10count == 0 )
         {
            AV11error = "No coin of this wallet is spent by that payment";
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV9auxError;
         new GeneXus.Programs.electrum.get_transaction(context ).execute(  StringUtil.Trim( AV15transactionId),  20, out  AV12messageResponse, out  GXt_char1) ;
         AV9auxError = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9auxError)) && ( StringUtil.StringSearch( AV12messageResponse, "\"result\"", 1) > 0 ) && ( StringUtil.StringSearch( AV12messageResponse, "\"error\"", 1) == 0 ) )
         {
            AV11error = "The network has this payment (in a block or waiting for one): it can not be forgotten. Press Rescan Balance in the Wallet Balance tab and the wallet will show it";
            cleanup();
            if (true) return;
         }
         AV18GXV2 = 1;
         while ( AV18GXV2 <= AV13StoredTransactions.gxTpr_Transaction.Count )
         {
            AV16TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV13StoredTransactions.gxTpr_Transaction.Item(AV18GXV2));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV16TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.Trim( AV15transactionId)) == 0 )
            {
               AV16TransactionItem.gxTpr_Usedin.gxTpr_Transactionid = "";
               AV16TransactionItem.gxTpr_Usedin.gxTpr_Datetime = AV8emptyDateTime;
               AV16TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Clear();
            }
            AV18GXV2 = (int)(AV18GXV2+1);
         }
         GXt_char1 = AV11error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV14transactionFileName,  AV13StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
         AV11error = GXt_char1;
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
         AV11error = "";
         AV13StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV16TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV9auxError = "";
         AV12messageResponse = "";
         AV8emptyDateTime = (DateTime)(DateTime.MinValue);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV10count ;
      private int AV17GXV1 ;
      private int AV18GXV2 ;
      private string AV14transactionFileName ;
      private string AV11error ;
      private string AV9auxError ;
      private string GXt_char1 ;
      private DateTime AV8emptyDateTime ;
      private string AV12messageResponse ;
      private string AV15transactionId ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV13StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV16TransactionItem ;
      private string aP2_error ;
   }

}
