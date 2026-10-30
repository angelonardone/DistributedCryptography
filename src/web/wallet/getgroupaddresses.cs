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
   public class getgroupaddresses : GXProcedure
   {
      public getgroupaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP1_addresses ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography") ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_addresses=this.AV8addresses;
         aP2_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP1_addresses )
      {
         execute(aP0_groupId, out aP1_addresses, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP1_addresses ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography") ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_addresses=this.AV8addresses;
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         AV11error = "";
         GXt_int1 = AV15groupType;
         new GeneXus.Programs.wallet.registered.setgroupeditwallet(context ).execute(  AV14groupId, out  GXt_int1) ;
         AV15groupType = GXt_int1;
         if ( ( AV15groupType != 30 ) && ( AV15groupType != 50 ) && ( AV15groupType != 60 ) && ( AV15groupType != 80 ) && ( AV15groupType != 90 ) )
         {
            AV11error = "The address list is available for the multisignature groups (Legacy, Delegated, Multi Signature)";
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT2 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT2) ;
         AV13group_sdt = GXt_SdtGroup_SDT2;
         GXt_SdtWalletInfo3 = AV31walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV31walletInfo = GXt_SdtWalletInfo3;
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV14groupId, out  AV29transactionFileName, out  AV24requestsFileName, out  AV22receivingType, out  AV10changeType) ;
         AV28StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV29transactionFileName, out  AV11error), null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV14groupId, out  AV23requests, out  AV9auxError) ;
         AV25reserved = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV32GXV1 = 1;
         while ( AV32GXV1 <= AV23requests.Count )
         {
            AV19oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV23requests.Item(AV32GXV1));
            if ( StringUtil.StrCmp(AV19oneRequest.gxTpr_Status, "Pending") == 0 )
            {
               AV25reserved.Add(StringUtil.Trim( AV19oneRequest.gxTpr_Address), 0);
            }
            AV32GXV1 = (int)(AV32GXV1+1);
         }
         AV33GXV2 = 1;
         while ( AV33GXV2 <= AV28StoredTransactions.gxTpr_Transaction.Count )
         {
            AV30TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV28StoredTransactions.gxTpr_Transaction.Item(AV33GXV2));
            AV12found = false;
            AV34GXV3 = 1;
            while ( AV34GXV3 <= AV8addresses.Count )
            {
               AV18oneAddress = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV8addresses.Item(AV34GXV3));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV18oneAddress.gxTpr_Address), StringUtil.Trim( AV30TransactionItem.gxTpr_Scriptpubkey_address)) == 0 )
               {
                  AV12found = true;
                  if (true) break;
               }
               AV34GXV3 = (int)(AV34GXV3+1);
            }
            if ( ! AV12found )
            {
               AV18oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
               if ( AV30TransactionItem.gxTpr_Addressgeneratedtype == AV10changeType )
               {
                  AV18oneAddress.gxTpr_Chainindex = 1;
               }
               else
               {
                  AV18oneAddress.gxTpr_Chainindex = 0;
               }
               AV18oneAddress.gxTpr_Addressindex = AV30TransactionItem.gxTpr_Addresscreationsequence;
               AV18oneAddress.gxTpr_Address = StringUtil.Trim( AV30TransactionItem.gxTpr_Scriptpubkey_address);
               AV18oneAddress.gxTpr_Status = "Used";
               AV18oneAddress.gxTpr_Received = (decimal)(0);
               AV18oneAddress.gxTpr_Balance = (decimal)(0);
               AV18oneAddress.gxTpr_Transactions = 0;
            }
            AV18oneAddress.gxTpr_Received = (decimal)(AV18oneAddress.gxTpr_Received+(AV30TransactionItem.gxTpr_Value));
            AV18oneAddress.gxTpr_Transactions = (long)(AV18oneAddress.gxTpr_Transactions+1);
            if ( (DateTime.MinValue==AV30TransactionItem.gxTpr_Usedin.gxTpr_Datetime) )
            {
               AV18oneAddress.gxTpr_Balance = (decimal)(AV18oneAddress.gxTpr_Balance+(AV30TransactionItem.gxTpr_Value));
            }
            if ( ! AV12found )
            {
               AV8addresses.Add(AV18oneAddress, 0);
            }
            AV33GXV2 = (int)(AV33GXV2+1);
         }
         AV21receivingGap = (long)(20+AV25reserved.Count);
         if ( AV15groupType == 50 )
         {
            new GeneXus.Programs.wallet.registered.deriveaddresssforlegacy(context ).execute(  AV28StoredTransactions,  AV13group_sdt,  AV31walletInfo.gxTpr_Networktype,  AV21receivingGap,  false, out  AV27sdt_addressessRec, out  AV11error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               new GeneXus.Programs.wallet.registered.deriveaddresssforlegacy(context ).execute(  AV28StoredTransactions,  AV13group_sdt,  AV31walletInfo.gxTpr_Networktype,  10,  true, out  AV26sdt_addressessChange, out  AV11error) ;
            }
         }
         else
         {
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegated(context ).execute(  AV28StoredTransactions,  AV13group_sdt,  AV31walletInfo.gxTpr_Networktype,  AV21receivingGap,  false, out  AV27sdt_addressessRec, out  AV11error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               new GeneXus.Programs.wallet.registered.deriveaddresssfordelegated(context ).execute(  AV28StoredTransactions,  AV13group_sdt,  AV31walletInfo.gxTpr_Networktype,  10,  true, out  AV26sdt_addressessChange, out  AV11error) ;
            }
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV16nextFound = false;
         AV35GXV4 = 1;
         while ( AV35GXV4 <= AV27sdt_addressessRec.Count )
         {
            AV17one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV27sdt_addressessRec.Item(AV35GXV4));
            AV18oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
            AV18oneAddress.gxTpr_Chainindex = 0;
            AV18oneAddress.gxTpr_Addressindex = AV17one_address.gxTpr_Creationsequence;
            AV18oneAddress.gxTpr_Address = StringUtil.Trim( AV17one_address.gxTpr_Address);
            AV18oneAddress.gxTpr_Status = "Unused";
            AV36GXV5 = 1;
            while ( AV36GXV5 <= AV25reserved.Count )
            {
               AV20oneReserved = AV25reserved.GetString(AV36GXV5);
               if ( StringUtil.StrCmp(StringUtil.Trim( AV20oneReserved), StringUtil.Trim( AV18oneAddress.gxTpr_Address)) == 0 )
               {
                  AV18oneAddress.gxTpr_Status = "Reserved";
                  if (true) break;
               }
               AV36GXV5 = (int)(AV36GXV5+1);
            }
            if ( ( StringUtil.StrCmp(AV18oneAddress.gxTpr_Status, "Unused") == 0 ) && ! AV16nextFound )
            {
               AV18oneAddress.gxTpr_Status = "Next";
               AV16nextFound = true;
            }
            AV8addresses.Add(AV18oneAddress, 0);
            AV35GXV4 = (int)(AV35GXV4+1);
         }
         AV16nextFound = false;
         AV37GXV6 = 1;
         while ( AV37GXV6 <= AV26sdt_addressessChange.Count )
         {
            AV17one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV26sdt_addressessChange.Item(AV37GXV6));
            AV18oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
            AV18oneAddress.gxTpr_Chainindex = 1;
            AV18oneAddress.gxTpr_Addressindex = AV17one_address.gxTpr_Creationsequence;
            AV18oneAddress.gxTpr_Address = StringUtil.Trim( AV17one_address.gxTpr_Address);
            if ( AV16nextFound )
            {
               AV18oneAddress.gxTpr_Status = "Unused";
            }
            else
            {
               AV18oneAddress.gxTpr_Status = "Next";
               AV16nextFound = true;
            }
            AV8addresses.Add(AV18oneAddress, 0);
            AV37GXV6 = (int)(AV37GXV6+1);
         }
         AV38GXV7 = 1;
         while ( AV38GXV7 <= AV8addresses.Count )
         {
            AV18oneAddress = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV8addresses.Item(AV38GXV7));
            if ( AV18oneAddress.gxTpr_Chainindex == 1 )
            {
               AV18oneAddress.gxTpr_Chain = "Change";
            }
            else
            {
               AV18oneAddress.gxTpr_Chain = "Receiving";
            }
            AV18oneAddress.gxTpr_Path = StringUtil.Trim( StringUtil.Str( (decimal)(AV18oneAddress.gxTpr_Chainindex), 4, 0))+"/"+StringUtil.Trim( StringUtil.Str( (decimal)(AV18oneAddress.gxTpr_Addressindex), 10, 0));
            AV39GXV8 = 1;
            while ( AV39GXV8 <= AV23requests.Count )
            {
               AV19oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV23requests.Item(AV39GXV8));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV19oneRequest.gxTpr_Address), StringUtil.Trim( AV18oneAddress.gxTpr_Address)) == 0 )
               {
                  AV18oneAddress.gxTpr_Label = StringUtil.Trim( AV19oneRequest.gxTpr_Label);
               }
               AV39GXV8 = (int)(AV39GXV8+1);
            }
            AV38GXV7 = (int)(AV38GXV7+1);
         }
         AV8addresses.Sort("chainIndex,addressIndex");
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
         AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         AV11error = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV31walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV29transactionFileName = "";
         AV24requestsFileName = "";
         AV28StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV23requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV9auxError = "";
         AV25reserved = new GxSimpleCollection<string>();
         AV19oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV30TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV18oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
         AV27sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV26sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV17one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV20oneReserved = "";
         /* GeneXus formulas. */
      }

      private short AV15groupType ;
      private short GXt_int1 ;
      private short AV22receivingType ;
      private short AV10changeType ;
      private int AV32GXV1 ;
      private int AV33GXV2 ;
      private int AV34GXV3 ;
      private int AV35GXV4 ;
      private int AV36GXV5 ;
      private int AV37GXV6 ;
      private int AV38GXV7 ;
      private int AV39GXV8 ;
      private long AV21receivingGap ;
      private string AV11error ;
      private string AV29transactionFileName ;
      private string AV24requestsFileName ;
      private string AV9auxError ;
      private string AV20oneReserved ;
      private bool AV12found ;
      private bool AV16nextFound ;
      private Guid AV14groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> AV8addresses ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV31walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV28StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV23requests ;
      private GxSimpleCollection<string> AV25reserved ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV19oneRequest ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV30TransactionItem ;
      private GeneXus.Programs.wallet.SdtWalletAddressItem AV18oneAddress ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV27sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV26sdt_addressessChange ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV17one_address ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP1_addresses ;
      private string aP2_error ;
   }

}
