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
   public class gettransactionsfromservice : GXProcedure
   {
      public gettransactionsfromservice( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettransactionsfromservice( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           SdtGetTransactions__postInput aP1_transactions__postInput ,
                           out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ,
                           out string aP3_error )
      {
         this.AV58transactionFileName = aP0_transactionFileName;
         this.AV36transactions__postInput = aP1_transactions__postInput;
         this.AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context) ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_StoredTransactions=this.AV33StoredTransactions;
         aP3_error=this.AV13error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                SdtGetTransactions__postInput aP1_transactions__postInput ,
                                out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions )
      {
         execute(aP0_transactionFileName, aP1_transactions__postInput, out aP2_StoredTransactions, out aP3_error);
         return AV13error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 SdtGetTransactions__postInput aP1_transactions__postInput ,
                                 out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ,
                                 out string aP3_error )
      {
         this.AV58transactionFileName = aP0_transactionFileName;
         this.AV36transactions__postInput = aP1_transactions__postInput;
         this.AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context) ;
         this.AV13error = "" ;
         SubmitImpl();
         aP2_StoredTransactions=this.AV33StoredTransactions;
         aP3_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV13error;
         new GeneXus.Programs.wallet.gettransctionsfromlocaldb(context ).execute(  AV36transactions__postInput, out  AV38transactionsFromService, out  GXt_char1) ;
         AV13error = GXt_char1;
         AV19IsSuccess = true;
         if ( AV19IsSuccess )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
               AV45transactionsFromFile.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV58transactionFileName, out  AV13error), null);
               AV60GXV1 = 1;
               while ( AV60GXV1 <= AV45transactionsFromFile.gxTpr_Transaction.Count )
               {
                  AV50oneTempTransactionFromFile = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV45transactionsFromFile.gxTpr_Transaction.Item(AV60GXV1));
                  AV51found = false;
                  AV61GXV2 = 1;
                  while ( AV61GXV2 <= AV38transactionsFromService.gxTpr_Transaction.Count )
                  {
                     AV43transaction = ((SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem)AV38transactionsFromService.gxTpr_Transaction.Item(AV61GXV2));
                     if ( ( StringUtil.StrCmp(AV50oneTempTransactionFromFile.gxTpr_Transactionid, AV43transaction.gxTpr_Transactionid) == 0 ) && ( AV50oneTempTransactionFromFile.gxTpr_N == AV43transaction.gxTpr_N ) )
                     {
                        AV51found = true;
                        if (true) break;
                     }
                     AV61GXV2 = (int)(AV61GXV2+1);
                  }
                  if ( ! AV51found )
                  {
                     AV33StoredTransactions.gxTpr_Transaction.Add(AV50oneTempTransactionFromFile, 0);
                  }
                  AV60GXV1 = (int)(AV60GXV1+1);
               }
               AV62GXV3 = 1;
               while ( AV62GXV3 <= AV38transactionsFromService.gxTpr_Transaction.Count )
               {
                  AV43transaction = ((SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem)AV38transactionsFromService.gxTpr_Transaction.Item(AV62GXV3));
                  AV63GXV4 = 1;
                  while ( AV63GXV4 <= AV45transactionsFromFile.gxTpr_Transaction.Count )
                  {
                     AV50oneTempTransactionFromFile = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV45transactionsFromFile.gxTpr_Transaction.Item(AV63GXV4));
                     AV51found = false;
                     AV59tempDesctiption = "";
                     if ( ( StringUtil.StrCmp(AV50oneTempTransactionFromFile.gxTpr_Transactionid, AV43transaction.gxTpr_Transactionid) == 0 ) && ( AV50oneTempTransactionFromFile.gxTpr_N == AV43transaction.gxTpr_N ) )
                     {
                        AV53oneFoundTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
                        if ( AV43transaction.gxTpr_Confirmations > AV50oneTempTransactionFromFile.gxTpr_Confirmations )
                        {
                           AV50oneTempTransactionFromFile.gxTpr_Confirmations = AV43transaction.gxTpr_Confirmations;
                           AV50oneTempTransactionFromFile.gxTpr_Datetime = AV43transaction.gxTpr_Datetime;
                        }
                        AV53oneFoundTransactionFromFile = AV50oneTempTransactionFromFile;
                        AV55tempUsedTransactionId = StringUtil.Trim( AV43transaction.gxTpr_Used.gxTpr_Usedid);
                        AV59tempDesctiption = AV50oneTempTransactionFromFile.gxTpr_Description;
                        AV51found = true;
                        if (true) break;
                     }
                     AV63GXV4 = (int)(AV63GXV4+1);
                  }
                  if ( AV51found )
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV55tempUsedTransactionId)) )
                     {
                        AV33StoredTransactions.gxTpr_Transaction.Add(AV53oneFoundTransactionFromFile, 0);
                     }
                     else
                     {
                        /* Execute user subroutine: 'CREATE FROM SERVICE' */
                        S111 ();
                        if ( returnInSub )
                        {
                           cleanup();
                           if (true) return;
                        }
                     }
                  }
                  else
                  {
                     /* Execute user subroutine: 'CREATE FROM SERVICE' */
                     S111 ();
                     if ( returnInSub )
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
                  AV62GXV3 = (int)(AV62GXV3+1);
               }
               GXt_char1 = AV13error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV58transactionFileName,  AV33StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
               AV13error = GXt_char1;
            }
         }
         else
         {
            AV13error = AV18HttpMessage.ToJSonString(false, true);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'CREATE FROM SERVICE' Routine */
         returnInSub = false;
         AV47oneTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV47oneTransactionFromFile.gxTpr_Description = StringUtil.Trim( AV59tempDesctiption);
         AV47oneTransactionFromFile.gxTpr_Transactionid = AV43transaction.gxTpr_Transactionid;
         AV47oneTransactionFromFile.gxTpr_N = AV43transaction.gxTpr_N;
         AV47oneTransactionFromFile.gxTpr_Value = AV43transaction.gxTpr_Value;
         AV47oneTransactionFromFile.gxTpr_Scriptpubkey_address = AV43transaction.gxTpr_Scriptpubkey_address;
         AV47oneTransactionFromFile.gxTpr_Datetime = AV43transaction.gxTpr_Datetime;
         GXt_SdtSDT_Addressess_SDT_AddressessItem2 = AV52one_sdt_address;
         new GeneXus.Programs.wallet.searchinalladdress(context ).execute(  AV43transaction.gxTpr_Scriptpubkey_address, out  GXt_SdtSDT_Addressess_SDT_AddressessItem2) ;
         AV52one_sdt_address = GXt_SdtSDT_Addressess_SDT_AddressessItem2;
         AV47oneTransactionFromFile.gxTpr_Addresscreationsequence = (short)(AV52one_sdt_address.gxTpr_Creationsequence);
         AV47oneTransactionFromFile.gxTpr_Addressgeneratedtype = AV52one_sdt_address.gxTpr_Generatedtype;
         AV47oneTransactionFromFile.gxTpr_Usedin.gxTpr_Transactionid = AV43transaction.gxTpr_Used.gxTpr_Usedid;
         AV47oneTransactionFromFile.gxTpr_Usedin.gxTpr_N = AV43transaction.gxTpr_Used.gxTpr_Usedn;
         AV47oneTransactionFromFile.gxTpr_Usedin.gxTpr_Datetime = AV43transaction.gxTpr_Used.gxTpr_Useddatetime;
         AV47oneTransactionFromFile.gxTpr_Confirmations = AV43transaction.gxTpr_Confirmations;
         AV64GXV5 = 1;
         while ( AV64GXV5 <= AV43transaction.gxTpr_Used.gxTpr_Usedto.Count )
         {
            AV48oneUsedServiceAddress = ((SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item)AV43transaction.gxTpr_Used.gxTpr_Usedto.Item(AV64GXV5));
            AV49oneUsedTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
            AV49oneUsedTransactionFromFile.gxTpr_Scriptpubkey_address = AV48oneUsedServiceAddress.gxTpr_Scriptpubkey_address;
            AV49oneUsedTransactionFromFile.gxTpr_N = AV48oneUsedServiceAddress.gxTpr_N;
            AV49oneUsedTransactionFromFile.gxTpr_Value = AV48oneUsedServiceAddress.gxTpr_Value;
            AV47oneTransactionFromFile.gxTpr_Usedin.gxTpr_Usedto.Add(AV49oneUsedTransactionFromFile, 0);
            AV64GXV5 = (int)(AV64GXV5+1);
         }
         AV33StoredTransactions.gxTpr_Transaction.Add(AV47oneTransactionFromFile, 0);
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
         AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV13error = "";
         AV38transactionsFromService = new SdtGxExplorer_services_TxoutFromAddresses(context);
         AV45transactionsFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV50oneTempTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV43transaction = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem(context);
         AV59tempDesctiption = "";
         AV53oneFoundTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV55tempUsedTransactionId = "";
         GXt_char1 = "";
         AV18HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV47oneTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV52one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         GXt_SdtSDT_Addressess_SDT_AddressessItem2 = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV48oneUsedServiceAddress = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item(context);
         AV49oneUsedTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
         /* GeneXus formulas. */
      }

      private int AV60GXV1 ;
      private int AV61GXV2 ;
      private int AV62GXV3 ;
      private int AV63GXV4 ;
      private int AV64GXV5 ;
      private string AV58transactionFileName ;
      private string AV13error ;
      private string AV55tempUsedTransactionId ;
      private string GXt_char1 ;
      private bool AV19IsSuccess ;
      private bool AV51found ;
      private bool returnInSub ;
      private string AV59tempDesctiption ;
      private SdtGetTransactions__postInput AV36transactions__postInput ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV33StoredTransactions ;
      private SdtGxExplorer_services_TxoutFromAddresses AV38transactionsFromService ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV45transactionsFromFile ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV50oneTempTransactionFromFile ;
      private SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem AV43transaction ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV53oneFoundTransactionFromFile ;
      private GeneXus.Utils.SdtMessages_Message AV18HttpMessage ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV47oneTransactionFromFile ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV52one_sdt_address ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem GXt_SdtSDT_Addressess_SDT_AddressessItem2 ;
      private SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item AV48oneUsedServiceAddress ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem AV49oneUsedTransactionFromFile ;
      private GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ;
      private string aP3_error ;
   }

}
