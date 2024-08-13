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
namespace GeneXus.Programs.sudodb {
   public class gettransactionsfromaddress : GXProcedure
   {
      public gettransactionsfromaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettransactionsfromaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           ref SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService )
      {
         this.AV8address = aP0_address;
         this.AV22transactionsFromService = aP1_transactionsFromService;
         initialize();
         ExecuteImpl();
         aP1_transactionsFromService=this.AV22transactionsFromService;
      }

      public SdtGxExplorer_services_TxoutFromAddresses executeUdp( string aP0_address )
      {
         execute(aP0_address, ref aP1_transactionsFromService);
         return AV22transactionsFromService ;
      }

      public void executeSubmit( string aP0_address ,
                                 ref SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService )
      {
         this.AV8address = aP0_address;
         this.AV22transactionsFromService = aP1_transactionsFromService;
         SubmitImpl();
         aP1_transactionsFromService=this.AV22transactionsFromService;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV21transactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV11error), null);
         AV10DBvOUTs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvOUT.db", out  AV11error), null);
         AV9DBvINs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvIN.db", out  AV11error), null);
         AV10DBvOUTs.Sort("scriptPubKey_address");
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV10DBvOUTs.Count )
         {
            AV13oneDBvOUT = ((GeneXus.Programs.sudodb.SdtvOUT)AV10DBvOUTs.Item(AV24GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV13oneDBvOUT.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV8address)) == 0 )
            {
               AV16oneTransactionFromService = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem(context);
               AV16oneTransactionFromService.gxTpr_Transactionid = StringUtil.Trim( AV13oneDBvOUT.gxTpr_Transactionid);
               AV16oneTransactionFromService.gxTpr_N = AV13oneDBvOUT.gxTpr_N;
               AV16oneTransactionFromService.gxTpr_Value = AV13oneDBvOUT.gxTpr_Value;
               AV16oneTransactionFromService.gxTpr_Scriptpubkey_address = StringUtil.Trim( AV13oneDBvOUT.gxTpr_Scriptpubkey_address);
               AV20TransactionId = StringUtil.Trim( AV13oneDBvOUT.gxTpr_Transactionid);
               /* Execute user subroutine: 'GET DATETIME AND CONFIRMATIONS FROM TRANSACTIONID' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV16oneTransactionFromService.gxTpr_Datetime = AV17transactionDateTime;
               AV16oneTransactionFromService.gxTpr_Confirmations = AV23confirmations;
               AV25GXV2 = 1;
               while ( AV25GXV2 <= AV9DBvINs.Count )
               {
                  AV12oneDBvIN = ((GeneXus.Programs.sudodb.SdtvIN)AV9DBvINs.Item(AV25GXV2));
                  if ( ( StringUtil.StrCmp(AV12oneDBvIN.gxTpr_Vintransactionid, StringUtil.Trim( AV13oneDBvOUT.gxTpr_Transactionid)) == 0 ) && ( AV12oneDBvIN.gxTpr_Vinn == AV13oneDBvOUT.gxTpr_N ) )
                  {
                     AV18transactionFromServiceUsed = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used(context);
                     AV18transactionFromServiceUsed.gxTpr_Usedid = StringUtil.Trim( AV12oneDBvIN.gxTpr_Transactionid);
                     AV18transactionFromServiceUsed.gxTpr_Usedn = AV12oneDBvIN.gxTpr_Vinn;
                     AV20TransactionId = StringUtil.Trim( AV12oneDBvIN.gxTpr_Transactionid);
                     /* Execute user subroutine: 'GET DATETIME AND CONFIRMATIONS FROM TRANSACTIONID' */
                     S111 ();
                     if ( returnInSub )
                     {
                        cleanup();
                        if (true) return;
                     }
                     AV18transactionFromServiceUsed.gxTpr_Useddatetime = AV17transactionDateTime;
                     AV26GXV3 = 1;
                     while ( AV26GXV3 <= AV10DBvOUTs.Count )
                     {
                        AV14oneDBvOUT1 = ((GeneXus.Programs.sudodb.SdtvOUT)AV10DBvOUTs.Item(AV26GXV3));
                        if ( StringUtil.StrCmp(AV14oneDBvOUT1.gxTpr_Transactionid, StringUtil.Trim( AV12oneDBvIN.gxTpr_Transactionid)) == 0 )
                        {
                           AV19transactionFromServiceUsedItem = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item(context);
                           AV19transactionFromServiceUsedItem.gxTpr_N = AV14oneDBvOUT1.gxTpr_N;
                           AV19transactionFromServiceUsedItem.gxTpr_Value = AV14oneDBvOUT1.gxTpr_Value;
                           AV19transactionFromServiceUsedItem.gxTpr_Scriptpubkey_address = StringUtil.Trim( AV14oneDBvOUT1.gxTpr_Scriptpubkey_address);
                           AV18transactionFromServiceUsed.gxTpr_Usedto.Add(AV19transactionFromServiceUsedItem, 0);
                        }
                        AV26GXV3 = (int)(AV26GXV3+1);
                     }
                     AV16oneTransactionFromService.gxTpr_Used = AV18transactionFromServiceUsed;
                  }
                  AV25GXV2 = (int)(AV25GXV2+1);
               }
               AV22transactionsFromService.gxTpr_Transaction.Add(AV16oneTransactionFromService, 0);
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'GET DATETIME AND CONFIRMATIONS FROM TRANSACTIONID' Routine */
         returnInSub = false;
         AV27GXV4 = 1;
         while ( AV27GXV4 <= AV21transactions.Count )
         {
            AV15oneTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV21transactions.Item(AV27GXV4));
            if ( StringUtil.StrCmp(AV15oneTransaction.gxTpr_Transactionid, StringUtil.Trim( AV20TransactionId)) == 0 )
            {
               AV23confirmations = AV15oneTransaction.gxTpr_Confirmations;
               if ( AV15oneTransaction.gxTpr_Confirmations > 0 )
               {
                  AV17transactionDateTime = AV15oneTransaction.gxTpr_Blockdatetime;
               }
               if (true) break;
            }
            AV27GXV4 = (int)(AV27GXV4+1);
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
         AV21transactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV11error = "";
         AV10DBvOUTs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT>( context, "vOUT", "distributedcryptography");
         AV9DBvINs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN>( context, "vIN", "distributedcryptography");
         AV13oneDBvOUT = new GeneXus.Programs.sudodb.SdtvOUT(context);
         AV16oneTransactionFromService = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem(context);
         AV20TransactionId = "";
         AV17transactionDateTime = (DateTime)(DateTime.MinValue);
         AV12oneDBvIN = new GeneXus.Programs.sudodb.SdtvIN(context);
         AV18transactionFromServiceUsed = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used(context);
         AV14oneDBvOUT1 = new GeneXus.Programs.sudodb.SdtvOUT(context);
         AV19transactionFromServiceUsedItem = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item(context);
         AV15oneTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         /* GeneXus formulas. */
      }

      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private int AV26GXV3 ;
      private int AV27GXV4 ;
      private long AV23confirmations ;
      private string AV8address ;
      private string AV11error ;
      private string AV20TransactionId ;
      private DateTime AV17transactionDateTime ;
      private bool returnInSub ;
      private SdtGxExplorer_services_TxoutFromAddresses AV22transactionsFromService ;
      private SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV21transactions ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT> AV10DBvOUTs ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN> AV9DBvINs ;
      private GeneXus.Programs.sudodb.SdtvOUT AV13oneDBvOUT ;
      private SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem AV16oneTransactionFromService ;
      private GeneXus.Programs.sudodb.SdtvIN AV12oneDBvIN ;
      private SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used AV18transactionFromServiceUsed ;
      private GeneXus.Programs.sudodb.SdtvOUT AV14oneDBvOUT1 ;
      private SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item AV19transactionFromServiceUsedItem ;
      private GeneXus.Programs.sudodb.SdtTransaction AV15oneTransaction ;
   }

}
