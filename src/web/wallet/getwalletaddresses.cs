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
   public class getwalletaddresses : GXProcedure
   {
      public getwalletaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwalletaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP0_addresses ,
                           out string aP1_error )
      {
         this.AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography") ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP0_addresses=this.AV8addresses;
         aP1_error=this.AV12error;
      }

      public string executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP0_addresses )
      {
         execute(out aP0_addresses, out aP1_error);
         return AV12error ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP0_addresses ,
                                 out string aP1_error )
      {
         this.AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography") ;
         this.AV12error = "" ;
         SubmitImpl();
         aP0_addresses=this.AV8addresses;
         aP1_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         AV12error = "";
         GXt_SdtWalletInfo1 = AV28walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV28walletInfo = GXt_SdtWalletInfo1;
         GXt_SdtExtKeyInfo2 = AV13extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV13extKeyInfoRoot = GXt_SdtExtKeyInfo2;
         if ( StringUtil.StrCmp(AV28walletInfo.gxTpr_Wallettype, "BIP44") == 0 )
         {
            AV11deserializedExtPubKey = AV13extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickey;
            AV20purpose = "44";
         }
         else if ( StringUtil.StrCmp(AV28walletInfo.gxTpr_Wallettype, "BIP49") == 0 )
         {
            AV11deserializedExtPubKey = AV13extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh;
            AV20purpose = "49";
         }
         else if ( StringUtil.StrCmp(AV28walletInfo.gxTpr_Wallettype, "BIP84") == 0 )
         {
            AV11deserializedExtPubKey = AV13extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwit;
            AV20purpose = "84";
         }
         else if ( StringUtil.StrCmp(AV28walletInfo.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV11deserializedExtPubKey = AV13extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
            AV20purpose = "86";
         }
         else
         {
            AV12error = "This wallet has only one address";
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(AV28walletInfo.gxTpr_Networktype, "MainNet") == 0 )
         {
            AV10coinType = "0";
         }
         else
         {
            AV10coinType = "1";
         }
         AV26StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "transactions.trn", out  AV12error), null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV29noGroup, out  AV22requests, out  AV9auxError) ;
         AV23reserved = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV30GXV1 = 1;
         while ( AV30GXV1 <= AV22requests.Count )
         {
            AV18oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV22requests.Item(AV30GXV1));
            if ( StringUtil.StrCmp(AV18oneRequest.gxTpr_Status, "Pending") == 0 )
            {
               AV23reserved.Add(StringUtil.Trim( AV18oneRequest.gxTpr_Address), 0);
            }
            AV30GXV1 = (int)(AV30GXV1+1);
         }
         AV31GXV2 = 1;
         while ( AV31GXV2 <= AV26StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV26StoredTransactions.gxTpr_Transaction.Item(AV31GXV2));
            AV14found = false;
            AV32GXV3 = 1;
            while ( AV32GXV3 <= AV8addresses.Count )
            {
               AV17oneAddress = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV8addresses.Item(AV32GXV3));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV17oneAddress.gxTpr_Address), StringUtil.Trim( AV27TransactionItem.gxTpr_Scriptpubkey_address)) == 0 )
               {
                  AV14found = true;
                  if (true) break;
               }
               AV32GXV3 = (int)(AV32GXV3+1);
            }
            if ( ! AV14found )
            {
               AV17oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
               AV17oneAddress.gxTpr_Chainindex = AV27TransactionItem.gxTpr_Addressgeneratedtype;
               AV17oneAddress.gxTpr_Addressindex = AV27TransactionItem.gxTpr_Addresscreationsequence;
               AV17oneAddress.gxTpr_Address = StringUtil.Trim( AV27TransactionItem.gxTpr_Scriptpubkey_address);
               AV17oneAddress.gxTpr_Status = "Used";
               AV17oneAddress.gxTpr_Received = (decimal)(0);
               AV17oneAddress.gxTpr_Balance = (decimal)(0);
               AV17oneAddress.gxTpr_Transactions = 0;
            }
            AV17oneAddress.gxTpr_Received = (decimal)(AV17oneAddress.gxTpr_Received+(AV27TransactionItem.gxTpr_Value));
            AV17oneAddress.gxTpr_Transactions = (long)(AV17oneAddress.gxTpr_Transactions+1);
            if ( (DateTime.MinValue==AV27TransactionItem.gxTpr_Usedin.gxTpr_Datetime) )
            {
               AV17oneAddress.gxTpr_Balance = (decimal)(AV17oneAddress.gxTpr_Balance+(AV27TransactionItem.gxTpr_Value));
            }
            if ( ! AV14found )
            {
               AV8addresses.Add(AV17oneAddress, 0);
            }
            AV31GXV2 = (int)(AV31GXV2+1);
         }
         AV21receivingGap = (long)(20+AV23reserved.Count);
         new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV26StoredTransactions,  AV11deserializedExtPubKey,  (short)(AV21receivingGap),  (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven)), out  AV25sdt_addressessRec, out  AV12error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            new GeneXus.Programs.wallet.registered.deriveaddresssfromextendedpubkey(context ).execute(  AV26StoredTransactions,  AV11deserializedExtPubKey,  10,  (short)(Math.Round(NumberUtil.Val( "1", "."), 18, MidpointRounding.ToEven)), out  AV24sdt_addressessChange, out  AV12error) ;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         AV15nextFound = false;
         AV33GXV4 = 1;
         while ( AV33GXV4 <= AV25sdt_addressessRec.Count )
         {
            AV16one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV25sdt_addressessRec.Item(AV33GXV4));
            AV17oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
            AV17oneAddress.gxTpr_Chainindex = AV16one_address.gxTpr_Generatedtype;
            AV17oneAddress.gxTpr_Addressindex = AV16one_address.gxTpr_Creationsequence;
            AV17oneAddress.gxTpr_Address = StringUtil.Trim( AV16one_address.gxTpr_Address);
            AV17oneAddress.gxTpr_Status = "Unused";
            AV34GXV5 = 1;
            while ( AV34GXV5 <= AV23reserved.Count )
            {
               AV19oneReserved = AV23reserved.GetString(AV34GXV5);
               if ( StringUtil.StrCmp(StringUtil.Trim( AV19oneReserved), StringUtil.Trim( AV17oneAddress.gxTpr_Address)) == 0 )
               {
                  AV17oneAddress.gxTpr_Status = "Reserved";
                  if (true) break;
               }
               AV34GXV5 = (int)(AV34GXV5+1);
            }
            if ( ( StringUtil.StrCmp(AV17oneAddress.gxTpr_Status, "Unused") == 0 ) && ! AV15nextFound )
            {
               AV17oneAddress.gxTpr_Status = "Next";
               AV15nextFound = true;
            }
            AV8addresses.Add(AV17oneAddress, 0);
            AV33GXV4 = (int)(AV33GXV4+1);
         }
         AV15nextFound = false;
         AV35GXV6 = 1;
         while ( AV35GXV6 <= AV24sdt_addressessChange.Count )
         {
            AV16one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV24sdt_addressessChange.Item(AV35GXV6));
            AV17oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
            AV17oneAddress.gxTpr_Chainindex = AV16one_address.gxTpr_Generatedtype;
            AV17oneAddress.gxTpr_Addressindex = AV16one_address.gxTpr_Creationsequence;
            AV17oneAddress.gxTpr_Address = StringUtil.Trim( AV16one_address.gxTpr_Address);
            if ( AV15nextFound )
            {
               AV17oneAddress.gxTpr_Status = "Unused";
            }
            else
            {
               AV17oneAddress.gxTpr_Status = "Next";
               AV15nextFound = true;
            }
            AV8addresses.Add(AV17oneAddress, 0);
            AV35GXV6 = (int)(AV35GXV6+1);
         }
         AV36GXV7 = 1;
         while ( AV36GXV7 <= AV8addresses.Count )
         {
            AV17oneAddress = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV8addresses.Item(AV36GXV7));
            if ( (Convert.ToDecimal( AV17oneAddress.gxTpr_Chainindex ) == NumberUtil.Val( "0", ".") ) )
            {
               AV17oneAddress.gxTpr_Chain = "Receiving";
            }
            else if ( (Convert.ToDecimal( AV17oneAddress.gxTpr_Chainindex ) == NumberUtil.Val( "1", ".") ) )
            {
               AV17oneAddress.gxTpr_Chain = "Change";
            }
            else
            {
               AV17oneAddress.gxTpr_Chain = "Chain "+StringUtil.Trim( StringUtil.Str( (decimal)(AV17oneAddress.gxTpr_Chainindex), 4, 0));
            }
            AV17oneAddress.gxTpr_Path = "m/"+StringUtil.Trim( AV20purpose)+"'/"+StringUtil.Trim( AV10coinType)+"'/0'/"+StringUtil.Trim( StringUtil.Str( (decimal)(AV17oneAddress.gxTpr_Chainindex), 4, 0))+"/"+StringUtil.Trim( StringUtil.Str( (decimal)(AV17oneAddress.gxTpr_Addressindex), 10, 0));
            AV37GXV8 = 1;
            while ( AV37GXV8 <= AV22requests.Count )
            {
               AV18oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV22requests.Item(AV37GXV8));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV18oneRequest.gxTpr_Address), StringUtil.Trim( AV17oneAddress.gxTpr_Address)) == 0 )
               {
                  AV17oneAddress.gxTpr_Label = StringUtil.Trim( AV18oneRequest.gxTpr_Label);
               }
               AV37GXV8 = (int)(AV37GXV8+1);
            }
            AV36GXV7 = (int)(AV36GXV7+1);
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
         AV12error = "";
         AV28walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV11deserializedExtPubKey = "";
         AV20purpose = "";
         AV10coinType = "";
         AV26StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV29noGroup = Guid.Empty;
         AV22requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV9auxError = "";
         AV23reserved = new GxSimpleCollection<string>();
         AV18oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV17oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
         AV25sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV24sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV16one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV19oneReserved = "";
         /* GeneXus formulas. */
      }

      private int AV30GXV1 ;
      private int AV31GXV2 ;
      private int AV32GXV3 ;
      private int AV33GXV4 ;
      private int AV34GXV5 ;
      private int AV35GXV6 ;
      private int AV36GXV7 ;
      private int AV37GXV8 ;
      private long AV21receivingGap ;
      private string AV12error ;
      private string AV11deserializedExtPubKey ;
      private string AV20purpose ;
      private string AV10coinType ;
      private string AV9auxError ;
      private string AV19oneReserved ;
      private bool AV14found ;
      private bool AV15nextFound ;
      private Guid AV29noGroup ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> AV8addresses ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV28walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV13extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV26StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV22requests ;
      private GxSimpleCollection<string> AV23reserved ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV18oneRequest ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.wallet.SdtWalletAddressItem AV17oneAddress ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV25sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV24sdt_addressessChange ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV16one_address ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> aP0_addresses ;
      private string aP1_error ;
   }

}
