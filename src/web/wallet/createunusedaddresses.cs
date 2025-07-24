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
         this.AV32addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         initialize();
         ExecuteImpl();
         aP0_addressess_to_look_for=this.AV32addressess_to_look_for;
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
         this.AV32addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         SubmitImpl();
         aP0_addressess_to_look_for=this.AV32addressess_to_look_for;
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
         AV32addressess_to_look_for = new SdtGxGetAddressess(context);
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
         GXt_char3 = "";
         new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV24StoredTransactions,  AV10deserializedExtPubKey,  20,  (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven)), out  AV20sdt_addressessRec, out  GXt_char3) ;
         GXt_char4 = "";
         new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV24StoredTransactions,  AV10deserializedExtPubKey,  10,  (short)(Math.Round(NumberUtil.Val( "1", "."), 18, MidpointRounding.ToEven)), out  AV19sdt_addressessChange, out  GXt_char4) ;
         AV35GXV1 = 1;
         while ( AV35GXV1 <= AV20sdt_addressessRec.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV20sdt_addressessRec.Item(AV35GXV1));
            AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV35GXV1 = (int)(AV35GXV1+1);
         }
         AV36GXV2 = 1;
         while ( AV36GXV2 <= AV19sdt_addressessChange.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV19sdt_addressessChange.Item(AV36GXV2));
            AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV36GXV2 = (int)(AV36GXV2+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV9allAddressess) ;
         AV37GXV3 = 1;
         while ( AV37GXV3 <= AV24StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV24StoredTransactions.gxTpr_Transaction.Item(AV37GXV3));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV27TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) && ( AV27TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count == 0 ) )
            {
               AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV27TransactionItem.gxTpr_Scriptpubkey_address, 0);
            }
            AV37GXV3 = (int)(AV37GXV3+1);
         }
         GXt_decimal5 = AV26totalBalance;
         new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV24StoredTransactions, out  AV21SDTAddressHistory, out  GXt_decimal5) ;
         AV26totalBalance = GXt_decimal5;
         AV38GXV4 = 1;
         while ( AV38GXV4 <= AV21SDTAddressHistory.Count )
         {
            AV16oneSDTAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV21SDTAddressHistory.Item(AV38GXV4));
            AV34found = false;
            if ( AV16oneSDTAddressHistory.gxTpr_Confirmations < 6 )
            {
               AV39GXV5 = 1;
               while ( AV39GXV5 <= AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
               {
                  AV33one_to_look_for = ((string)AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV39GXV5));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), StringUtil.Trim( AV33one_to_look_for)) == 0 )
                  {
                     AV34found = true;
                     if (true) break;
                  }
                  AV39GXV5 = (int)(AV39GXV5+1);
               }
               if ( ! AV34found )
               {
                  AV32addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), 0);
               }
            }
            AV38GXV4 = (int)(AV38GXV4+1);
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
         AV32addressess_to_look_for = new SdtGxGetAddressess(context);
         AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV31wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV24StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV11error = "";
         AV10deserializedExtPubKey = "";
         AV20sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_char3 = "";
         AV19sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_char4 = "";
         AV15one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV16oneSDTAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV33one_to_look_for = "";
         /* GeneXus formulas. */
      }

      private int AV35GXV1 ;
      private int AV36GXV2 ;
      private int AV37GXV3 ;
      private int AV38GXV4 ;
      private int AV39GXV5 ;
      private decimal AV26totalBalance ;
      private decimal GXt_decimal5 ;
      private string AV11error ;
      private string AV10deserializedExtPubKey ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private string AV33one_to_look_for ;
      private bool AV34found ;
      private SdtGxGetAddressess AV32addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV21SDTAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV31wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV9allAddressess ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV24StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV20sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV19sdt_addressessChange ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_address ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV16oneSDTAddressHistory ;
      private SdtGxGetAddressess aP0_addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
   }

}
