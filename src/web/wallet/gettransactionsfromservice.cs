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
                           SdtGxGetAddressess aP1_addressess_to_look_for ,
                           out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ,
                           out string aP3_error )
      {
         this.AV58transactionFileName = aP0_transactionFileName;
         this.AV60addressess_to_look_for = aP1_addressess_to_look_for;
         this.AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context) ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_StoredTransactions=this.AV33StoredTransactions;
         aP3_error=this.AV13error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                SdtGxGetAddressess aP1_addressess_to_look_for ,
                                out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions )
      {
         execute(aP0_transactionFileName, aP1_addressess_to_look_for, out aP2_StoredTransactions, out aP3_error);
         return AV13error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 SdtGxGetAddressess aP1_addressess_to_look_for ,
                                 out GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ,
                                 out string aP3_error )
      {
         this.AV58transactionFileName = aP0_transactionFileName;
         this.AV60addressess_to_look_for = aP1_addressess_to_look_for;
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
         new GeneXus.Programs.wallet.gettransctionsfromlocaldb(context ).execute(  AV60addressess_to_look_for, out  AV38transactionsFromService, out  GXt_char1) ;
         AV13error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV33StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
            AV45transactionsFromFile.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV58transactionFileName, out  AV13error), null);
            AV61GXV1 = 1;
            while ( AV61GXV1 <= AV45transactionsFromFile.gxTpr_Transaction.Count )
            {
               AV50oneTempTransactionFromFile = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV45transactionsFromFile.gxTpr_Transaction.Item(AV61GXV1));
               AV51found = false;
               AV62GXV2 = 1;
               while ( AV62GXV2 <= AV38transactionsFromService.gxTpr_Transaction.Count )
               {
                  AV43transaction = ((SdtGxTransactionItem)AV38transactionsFromService.gxTpr_Transaction.Item(AV62GXV2));
                  if ( ( StringUtil.StrCmp(AV50oneTempTransactionFromFile.gxTpr_Transactionid, AV43transaction.gxTpr_Transactionid) == 0 ) && ( AV50oneTempTransactionFromFile.gxTpr_N == AV43transaction.gxTpr_N ) )
                  {
                     AV51found = true;
                     if (true) break;
                  }
                  AV62GXV2 = (int)(AV62GXV2+1);
               }
               if ( ! AV51found )
               {
                  AV33StoredTransactions.gxTpr_Transaction.Add(AV50oneTempTransactionFromFile, 0);
               }
               AV61GXV1 = (int)(AV61GXV1+1);
            }
            AV63GXV3 = 1;
            while ( AV63GXV3 <= AV38transactionsFromService.gxTpr_Transaction.Count )
            {
               AV43transaction = ((SdtGxTransactionItem)AV38transactionsFromService.gxTpr_Transaction.Item(AV63GXV3));
               AV51found = false;
               AV59tempDesctiption = "";
               AV64GXV4 = 1;
               while ( AV64GXV4 <= AV45transactionsFromFile.gxTpr_Transaction.Count )
               {
                  AV50oneTempTransactionFromFile = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV45transactionsFromFile.gxTpr_Transaction.Item(AV64GXV4));
                  if ( ( StringUtil.StrCmp(AV50oneTempTransactionFromFile.gxTpr_Transactionid, AV43transaction.gxTpr_Transactionid) == 0 ) && ( AV50oneTempTransactionFromFile.gxTpr_N == AV43transaction.gxTpr_N ) )
                  {
                     AV53oneFoundTransactionFromFile = (GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)(AV50oneTempTransactionFromFile.Clone());
                     if ( AV43transaction.gxTpr_Confirmations > AV50oneTempTransactionFromFile.gxTpr_Confirmations )
                     {
                        AV53oneFoundTransactionFromFile.gxTpr_Confirmations = AV43transaction.gxTpr_Confirmations;
                        AV53oneFoundTransactionFromFile.gxTpr_Datetime = AV43transaction.gxTpr_Datetime;
                     }
                     AV59tempDesctiption = AV50oneTempTransactionFromFile.gxTpr_Description;
                     AV51found = true;
                     if (true) break;
                  }
                  AV64GXV4 = (int)(AV64GXV4+1);
               }
               if ( ! AV51found )
               {
                  /* Execute user subroutine: 'CREATE FROM SERVICE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               else
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV43transaction.gxTpr_Used.gxTpr_Usedid))) )
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
               AV63GXV3 = (int)(AV63GXV3+1);
            }
            GXt_char1 = AV13error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV58transactionFileName,  AV33StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
            AV13error = GXt_char1;
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
         AV65GXV5 = 1;
         while ( AV65GXV5 <= AV43transaction.gxTpr_Used.gxTpr_Usedto.Count )
         {
            AV48oneUsedServiceAddress = ((SdtGXUsedToItem)AV43transaction.gxTpr_Used.gxTpr_Usedto.Item(AV65GXV5));
            AV49oneUsedTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
            AV49oneUsedTransactionFromFile.gxTpr_Scriptpubkey_address = AV48oneUsedServiceAddress.gxTpr_Scriptpubkey_address;
            AV49oneUsedTransactionFromFile.gxTpr_N = AV48oneUsedServiceAddress.gxTpr_N;
            AV49oneUsedTransactionFromFile.gxTpr_Value = AV48oneUsedServiceAddress.gxTpr_Value;
            AV47oneTransactionFromFile.gxTpr_Usedin.gxTpr_Usedto.Add(AV49oneUsedTransactionFromFile, 0);
            AV65GXV5 = (int)(AV65GXV5+1);
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
         AV38transactionsFromService = new SdtGxTransactions(context);
         AV45transactionsFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV50oneTempTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV43transaction = new SdtGxTransactionItem(context);
         AV59tempDesctiption = "";
         AV53oneFoundTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         GXt_char1 = "";
         AV47oneTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV52one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         GXt_SdtSDT_Addressess_SDT_AddressessItem2 = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV48oneUsedServiceAddress = new SdtGXUsedToItem(context);
         AV49oneUsedTransactionFromFile = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
         /* GeneXus formulas. */
      }

      private int AV61GXV1 ;
      private int AV62GXV2 ;
      private int AV63GXV3 ;
      private int AV64GXV4 ;
      private int AV65GXV5 ;
      private string AV58transactionFileName ;
      private string AV13error ;
      private string GXt_char1 ;
      private bool AV51found ;
      private bool returnInSub ;
      private string AV59tempDesctiption ;
      private SdtGxGetAddressess AV60addressess_to_look_for ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV33StoredTransactions ;
      private SdtGxTransactions AV38transactionsFromService ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV45transactionsFromFile ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV50oneTempTransactionFromFile ;
      private SdtGxTransactionItem AV43transaction ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV53oneFoundTransactionFromFile ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV47oneTransactionFromFile ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV52one_sdt_address ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem GXt_SdtSDT_Addressess_SDT_AddressessItem2 ;
      private SdtGXUsedToItem AV48oneUsedServiceAddress ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem AV49oneUsedTransactionFromFile ;
      private GeneXus.Programs.wallet.SdtStoredTransactions aP2_StoredTransactions ;
      private string aP3_error ;
   }

}
