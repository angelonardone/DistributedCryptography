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
   public class haspendingtransactions : GXProcedure
   {
      public haspendingtransactions( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public haspendingtransactions( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           out bool aP1_hasPending )
      {
         this.AV11transactionFileName = aP0_transactionFileName;
         this.AV9hasPending = false ;
         initialize();
         ExecuteImpl();
         aP1_hasPending=this.AV9hasPending;
      }

      public bool executeUdp( string aP0_transactionFileName )
      {
         execute(aP0_transactionFileName, out aP1_hasPending);
         return AV9hasPending ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 out bool aP1_hasPending )
      {
         this.AV11transactionFileName = aP0_transactionFileName;
         this.AV9hasPending = false ;
         SubmitImpl();
         aP1_hasPending=this.AV9hasPending;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9hasPending = false;
         AV10StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV11transactionFileName, out  AV8error), null);
         AV15dbTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV16auxError), null);
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV10StoredTransactions.gxTpr_Transaction.Count )
         {
            AV12TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV10StoredTransactions.gxTpr_Transaction.Item(AV17GXV1));
            if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12TransactionItem.gxTpr_Usedin.gxTpr_Transactionid))) )
            {
               if ( AV12TransactionItem.gxTpr_Confirmations < 6 )
               {
                  AV9hasPending = true;
                  if (true) break;
               }
            }
            else
            {
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV12TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), "MULTISIGNATURE IN PROGRESS") == 0 ) )
               {
                  AV13spendConfirmed = false;
                  if ( AV12TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count > 0 )
                  {
                     AV18GXV2 = 1;
                     while ( AV18GXV2 <= AV15dbTransactions.Count )
                     {
                        AV14oneDbTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV15dbTransactions.Item(AV18GXV2));
                        if ( StringUtil.StrCmp(StringUtil.Trim( AV14oneDbTransaction.gxTpr_Transactionid), StringUtil.Trim( AV12TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) == 0 )
                        {
                           if ( AV14oneDbTransaction.gxTpr_Confirmations >= 6 )
                           {
                              AV13spendConfirmed = true;
                           }
                           if (true) break;
                        }
                        AV18GXV2 = (int)(AV18GXV2+1);
                     }
                  }
                  if ( ! AV13spendConfirmed )
                  {
                     AV9hasPending = true;
                     if (true) break;
                  }
               }
            }
            AV17GXV1 = (int)(AV17GXV1+1);
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
         AV10StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV8error = "";
         AV15dbTransactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV16auxError = "";
         AV12TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV14oneDbTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private int AV18GXV2 ;
      private string AV11transactionFileName ;
      private string AV8error ;
      private string AV16auxError ;
      private bool AV9hasPending ;
      private bool AV13spendConfirmed ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV10StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV15dbTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV12TransactionItem ;
      private GeneXus.Programs.sudodb.SdtTransaction AV14oneDbTransaction ;
      private bool aP1_hasPending ;
   }

}
