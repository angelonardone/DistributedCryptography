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
   public class createunusedaddresses : GXProcedure
   {
      public createunusedaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createunusedaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                           out decimal aP2_totalBalance )
      {
         this.AV28addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         initialize();
         ExecuteImpl();
         aP0_addressess_to_look_for=this.AV28addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
      }

      public decimal executeUdp( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory )
      {
         execute(out aP0_addressess_to_look_for, out aP1_SDTAddressHistory, out aP2_totalBalance);
         return AV26totalBalance ;
      }

      public void executeSubmit( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                 out decimal aP2_totalBalance )
      {
         this.AV28addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         SubmitImpl();
         aP0_addressess_to_look_for=this.AV28addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExtKeyInfo1 = AV12extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV12extKeyInfoRoot = GXt_SdtExtKeyInfo1;
         GXt_SdtWallet2 = AV31wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV31wallet = GXt_SdtWallet2;
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV28addressess_to_look_for = new SdtGxGetAddressess(context);
         AV24StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "transactions.trn", out  AV11error), null);
         if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP44") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickey;
         }
         else if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP49") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh;
         }
         else if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP84") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwit;
         }
         else if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
         }
         else
         {
            GX_msglist.addItem("We couldn't find the this type of wallet addresses");
         }
         GXt_objcol_char3 = AV36reserved;
         new GeneXus.Programs.wallet.getreservedaddresses(context ).execute(  AV37noGroup, out  GXt_objcol_char3) ;
         AV36reserved = GXt_objcol_char3;
         AV35receivingGap = (long)(20+AV36reserved.Count);
         GXt_char4 = "";
         new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV24StoredTransactions,  AV10deserializedExtPubKey,  (short)(AV35receivingGap),  (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven)), out  AV20sdt_addressessRec, out  GXt_char4) ;
         GXt_char5 = "";
         new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV24StoredTransactions,  AV10deserializedExtPubKey,  10,  (short)(Math.Round(NumberUtil.Val( "1", "."), 18, MidpointRounding.ToEven)), out  AV19sdt_addressessChange, out  GXt_char5) ;
         AV42GXV1 = 1;
         while ( AV42GXV1 <= AV20sdt_addressessRec.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV20sdt_addressessRec.Item(AV42GXV1));
            AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV42GXV1 = (int)(AV42GXV1+1);
         }
         AV43GXV2 = 1;
         while ( AV43GXV2 <= AV19sdt_addressessChange.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV19sdt_addressessChange.Item(AV43GXV2));
            AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV43GXV2 = (int)(AV43GXV2+1);
         }
         AV44GXV3 = 1;
         while ( AV44GXV3 <= AV36reserved.Count )
         {
            AV34oneReserved = AV36reserved.GetString(AV44GXV3);
            AV33found = false;
            AV45GXV4 = 1;
            while ( AV45GXV4 <= AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
            {
               AV32one_to_look_for = ((string)AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV45GXV4));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV32one_to_look_for), StringUtil.Trim( AV34oneReserved)) == 0 )
               {
                  AV33found = true;
                  if (true) break;
               }
               AV45GXV4 = (int)(AV45GXV4+1);
            }
            if ( ! AV33found )
            {
               AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV34oneReserved), 0);
            }
            AV44GXV3 = (int)(AV44GXV3+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV9allAddressess) ;
         AV40dbTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV41auxError), null);
         AV46GXV5 = 1;
         while ( AV46GXV5 <= AV24StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV24StoredTransactions.gxTpr_Transaction.Item(AV46GXV5));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV27TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) )
            {
               AV38spendConfirmed = false;
               if ( AV27TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count > 0 )
               {
                  AV47GXV6 = 1;
                  while ( AV47GXV6 <= AV40dbTransactions.Count )
                  {
                     AV39oneDbTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV40dbTransactions.Item(AV47GXV6));
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV39oneDbTransaction.gxTpr_Transactionid), StringUtil.Trim( AV27TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) == 0 )
                     {
                        if ( AV39oneDbTransaction.gxTpr_Confirmations >= 6 )
                        {
                           AV38spendConfirmed = true;
                        }
                        if (true) break;
                     }
                     AV47GXV6 = (int)(AV47GXV6+1);
                  }
               }
               if ( ! AV38spendConfirmed )
               {
                  AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV27TransactionItem.gxTpr_Scriptpubkey_address, 0);
               }
            }
            AV46GXV5 = (int)(AV46GXV5+1);
         }
         new GeneXus.Programs.wallet.addrescanaddresses(context ).execute(  AV24StoredTransactions, ref  AV28addressess_to_look_for) ;
         GXt_decimal6 = AV26totalBalance;
         new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV24StoredTransactions, out  AV21SDTAddressHistory, out  GXt_decimal6) ;
         AV26totalBalance = GXt_decimal6;
         AV48GXV7 = 1;
         while ( AV48GXV7 <= AV21SDTAddressHistory.Count )
         {
            AV16oneSDTAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV21SDTAddressHistory.Item(AV48GXV7));
            AV33found = false;
            if ( AV16oneSDTAddressHistory.gxTpr_Confirmations < 6 )
            {
               AV49GXV8 = 1;
               while ( AV49GXV8 <= AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
               {
                  AV32one_to_look_for = ((string)AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV49GXV8));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), StringUtil.Trim( AV32one_to_look_for)) == 0 )
                  {
                     AV33found = true;
                     if (true) break;
                  }
                  AV49GXV8 = (int)(AV49GXV8+1);
               }
               if ( ! AV33found )
               {
                  AV28addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), 0);
               }
            }
            AV48GXV7 = (int)(AV48GXV7+1);
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
         AV28addressess_to_look_for = new SdtGxGetAddressess(context);
         AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV31wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV24StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV11error = "";
         AV10deserializedExtPubKey = "";
         AV36reserved = new GxSimpleCollection<string>();
         GXt_objcol_char3 = new GxSimpleCollection<string>();
         AV37noGroup = Guid.Empty;
         AV20sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_char4 = "";
         AV19sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_char5 = "";
         AV15one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV34oneReserved = "";
         AV32one_to_look_for = "";
         AV40dbTransactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV41auxError = "";
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV39oneDbTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         AV16oneSDTAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         /* GeneXus formulas. */
      }

      private int AV42GXV1 ;
      private int AV43GXV2 ;
      private int AV44GXV3 ;
      private int AV45GXV4 ;
      private int AV46GXV5 ;
      private int AV47GXV6 ;
      private int AV48GXV7 ;
      private int AV49GXV8 ;
      private long AV35receivingGap ;
      private decimal AV26totalBalance ;
      private decimal GXt_decimal6 ;
      private string AV11error ;
      private string AV10deserializedExtPubKey ;
      private string GXt_char4 ;
      private string GXt_char5 ;
      private string AV34oneReserved ;
      private string AV32one_to_look_for ;
      private string AV41auxError ;
      private bool AV33found ;
      private bool AV38spendConfirmed ;
      private Guid AV37noGroup ;
      private SdtGxGetAddressess AV28addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV21SDTAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV31wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV9allAddressess ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV24StoredTransactions ;
      private GxSimpleCollection<string> AV36reserved ;
      private GxSimpleCollection<string> GXt_objcol_char3 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV20sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV19sdt_addressessChange ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_address ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV40dbTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.sudodb.SdtTransaction AV39oneDbTransaction ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV16oneSDTAddressHistory ;
      private SdtGxGetAddressess aP0_addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
   }

}
