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
   public class createunusedaddressesgroup : GXProcedure
   {
      public createunusedaddressesgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createunusedaddressesgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtGetTransactions__postInput aP0_transactions__postInput ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                           out decimal aP2_totalBalance ,
                           out string aP3_error )
      {
         this.AV28transactions__postInput = new SdtGetTransactions__postInput(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP0_transactions__postInput=this.AV28transactions__postInput;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
         aP3_error=this.AV11error;
      }

      public string executeUdp( out SdtGetTransactions__postInput aP0_transactions__postInput ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                out decimal aP2_totalBalance )
      {
         execute(out aP0_transactions__postInput, out aP1_SDTAddressHistory, out aP2_totalBalance, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( out SdtGetTransactions__postInput aP0_transactions__postInput ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                 out decimal aP2_totalBalance ,
                                 out string aP3_error )
      {
         this.AV28transactions__postInput = new SdtGetTransactions__postInput(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         this.AV11error = "" ;
         SubmitImpl();
         aP0_transactions__postInput=this.AV28transactions__postInput;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
         aP3_error=this.AV11error;
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
         AV28transactions__postInput = new SdtGetTransactions__postInput(context);
         AV32group_sdt.FromJSonString(AV33websession.Get("Group_EDIT"), null);
         AV54transactionFileName = StringUtil.Trim( AV32group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         AV24StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV54transactionFileName, out  AV11error), null);
         if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
         }
         else
         {
            AV11error = "We couldn't find the this type of wallet addresses";
            cleanup();
            if (true) return;
         }
         if ( AV32group_sdt.gxTpr_Grouptype == 200 )
         {
            GXt_char3 = AV57error1;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig1(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  20,  (short)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven)), out  AV20sdt_addressessRec, out  GXt_char3) ;
            AV57error1 = GXt_char3;
            GXt_char3 = AV58error2;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig1(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  10,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  AV19sdt_addressessChange, out  GXt_char3) ;
            AV58error2 = GXt_char3;
            AV11error = StringUtil.Trim( AV57error1) + StringUtil.Trim( AV58error2);
         }
         else if ( AV32group_sdt.gxTpr_Grouptype == 800 )
         {
            GXt_char3 = AV57error1;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig2(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  20,  (short)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven)), out  AV20sdt_addressessRec, out  GXt_char3) ;
            AV57error1 = GXt_char3;
            GXt_char3 = AV58error2;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig2(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  10,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  AV19sdt_addressessChange, out  GXt_char3) ;
            AV58error2 = GXt_char3;
            AV11error = StringUtil.Trim( AV57error1) + StringUtil.Trim( AV58error2);
         }
         else
         {
            AV11error = "We don't have a group associated with this addresses";
            cleanup();
            if (true) return;
         }
         AV59GXV1 = 1;
         while ( AV59GXV1 <= AV20sdt_addressessRec.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV20sdt_addressessRec.Item(AV59GXV1));
            AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV59GXV1 = (int)(AV59GXV1+1);
         }
         AV60GXV2 = 1;
         while ( AV60GXV2 <= AV19sdt_addressessChange.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV19sdt_addressessChange.Item(AV60GXV2));
            AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV60GXV2 = (int)(AV60GXV2+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV9allAddressess) ;
         AV61GXV3 = 1;
         while ( AV61GXV3 <= AV24StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV24StoredTransactions.gxTpr_Transaction.Item(AV61GXV3));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV27TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) && ( AV27TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count == 0 ) )
            {
               AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV27TransactionItem.gxTpr_Scriptpubkey_address, 0);
            }
            AV61GXV3 = (int)(AV61GXV3+1);
         }
         GXt_decimal4 = AV26totalBalance;
         new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV24StoredTransactions, out  AV21SDTAddressHistory, out  GXt_decimal4) ;
         AV26totalBalance = GXt_decimal4;
         AV62GXV4 = 1;
         while ( AV62GXV4 <= AV21SDTAddressHistory.Count )
         {
            AV16oneSDTAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV21SDTAddressHistory.Item(AV62GXV4));
            if ( AV16oneSDTAddressHistory.gxTpr_Confirmations < 6 )
            {
               AV63GXV5 = 1;
               while ( AV63GXV5 <= AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Count )
               {
                  AV56one_transactions__postInput = ((string)AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV63GXV5));
                  if ( StringUtil.StrCmp(AV56one_transactions__postInput, StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress)) == 0 )
                  {
                     AV55found = true;
                     if (true) break;
                  }
                  AV63GXV5 = (int)(AV63GXV5+1);
               }
               if ( ! AV55found )
               {
                  AV28transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), 0);
               }
            }
            AV62GXV4 = (int)(AV62GXV4+1);
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
         AV28transactions__postInput = new SdtGetTransactions__postInput(context);
         AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV11error = "";
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV31wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV32group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV33websession = context.GetSession();
         AV54transactionFileName = "";
         AV24StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV10deserializedExtPubKey = "";
         AV57error1 = "";
         AV20sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV58error2 = "";
         AV19sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_char3 = "";
         AV15one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV16oneSDTAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV56one_transactions__postInput = "";
         /* GeneXus formulas. */
      }

      private int AV59GXV1 ;
      private int AV60GXV2 ;
      private int AV61GXV3 ;
      private int AV62GXV4 ;
      private int AV63GXV5 ;
      private decimal AV26totalBalance ;
      private decimal GXt_decimal4 ;
      private string AV11error ;
      private string AV54transactionFileName ;
      private string AV10deserializedExtPubKey ;
      private string AV57error1 ;
      private string AV58error2 ;
      private string GXt_char3 ;
      private string AV56one_transactions__postInput ;
      private bool AV55found ;
      private IGxSession AV33websession ;
      private SdtGetTransactions__postInput AV28transactions__postInput ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV21SDTAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV31wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV9allAddressess ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV32group_sdt ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV24StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV20sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV19sdt_addressessChange ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_address ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV16oneSDTAddressHistory ;
      private SdtGetTransactions__postInput aP0_transactions__postInput ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
      private string aP3_error ;
   }

}
