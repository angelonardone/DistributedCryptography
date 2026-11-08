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
   public class buildrubytransaction : GXProcedure
   {
      public buildrubytransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildrubytransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           decimal aP1_transactionFee ,
                           string aP2_networkType ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                           decimal aP4_inSendCoins ,
                           string aP5_sendTo ,
                           string aP6_returnTo ,
                           out long aP7_virtualSize ,
                           out string aP8_hexTransaction ,
                           out string aP9_error )
      {
         this.AV27sendAllCoins = aP0_sendAllCoins;
         this.AV29transactionFee = aP1_transactionFee;
         this.AV19networkType = aP2_networkType;
         this.AV30transactionsToSend = aP3_transactionsToSend;
         this.AV17inSendCoins = aP4_inSendCoins;
         this.AV28sendTo = aP5_sendTo;
         this.AV23returnTo = aP6_returnTo;
         this.AV31virtualSize = 0 ;
         this.AV13hexTransaction = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP7_virtualSize=this.AV31virtualSize;
         aP8_hexTransaction=this.AV13hexTransaction;
         aP9_error=this.AV11error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_transactionFee ,
                                string aP2_networkType ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                                decimal aP4_inSendCoins ,
                                string aP5_sendTo ,
                                string aP6_returnTo ,
                                out long aP7_virtualSize ,
                                out string aP8_hexTransaction )
      {
         execute(aP0_sendAllCoins, aP1_transactionFee, aP2_networkType, aP3_transactionsToSend, aP4_inSendCoins, aP5_sendTo, aP6_returnTo, out aP7_virtualSize, out aP8_hexTransaction, out aP9_error);
         return AV11error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_transactionFee ,
                                 string aP2_networkType ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                                 decimal aP4_inSendCoins ,
                                 string aP5_sendTo ,
                                 string aP6_returnTo ,
                                 out long aP7_virtualSize ,
                                 out string aP8_hexTransaction ,
                                 out string aP9_error )
      {
         this.AV27sendAllCoins = aP0_sendAllCoins;
         this.AV29transactionFee = aP1_transactionFee;
         this.AV19networkType = aP2_networkType;
         this.AV30transactionsToSend = aP3_transactionsToSend;
         this.AV17inSendCoins = aP4_inSendCoins;
         this.AV28sendTo = aP5_sendTo;
         this.AV23returnTo = aP6_returnTo;
         this.AV31virtualSize = 0 ;
         this.AV13hexTransaction = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP7_virtualSize=this.AV31virtualSize;
         aP8_hexTransaction=this.AV13hexTransaction;
         aP9_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11error = "";
         GXt_SdtWallet1 = AV32wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV32wallet = GXt_SdtWallet1;
         AV9changeChain = 0;
         AV10changeIndex = -1;
         if ( ! AV27sendAllCoins )
         {
            GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 = AV26sdt_Addresses;
            new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2) ;
            AV26sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2;
            AV33GXV1 = 1;
            while ( AV33GXV1 <= AV26sdt_Addresses.Count )
            {
               AV21one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV26sdt_Addresses.Item(AV33GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV21one_sdt_address.gxTpr_Address), StringUtil.Trim( AV23returnTo)) == 0 )
               {
                  AV9changeChain = AV21one_sdt_address.gxTpr_Generatedtype;
                  AV10changeIndex = AV21one_sdt_address.gxTpr_Creationsequence;
                  if (true) break;
               }
               AV33GXV1 = (int)(AV33GXV1+1);
            }
         }
         AV15inputs.Clear();
         AV34GXV2 = 1;
         while ( AV34GXV2 <= AV30transactionsToSend.Count )
         {
            AV22oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV30transactionsToSend.Item(AV34GXV2));
            AV14input = new GeneXus.Programs.wallet.SdtRubyInput(context);
            AV14input.gxTpr_Txhex = AV22oneAddressHistory.gxTpr_Receivedtransactionhex;
            AV14input.gxTpr_Vout = AV22oneAddressHistory.gxTpr_Recivedn;
            AV14input.gxTpr_Chain = AV22oneAddressHistory.gxTpr_Addressgeneratedtype;
            AV14input.gxTpr_Index = AV22oneAddressHistory.gxTpr_Addresscreationsequence;
            AV15inputs.Add(AV14input, 0);
            AV34GXV2 = (int)(AV34GXV2+1);
         }
         AV16inputsJson = AV15inputs.ToJSonString(false);
         AV20networkTypeText = AV19networkType;
         AV8amountText = StringUtil.Str( AV17inSendCoins, 16, 8);
         AV8amountText = StringUtil.Trim( AV8amountText);
         AV12feeText = StringUtil.Str( AV29transactionFee, 16, 8);
         AV12feeText = StringUtil.Trim( AV12feeText);
         AV18json = AV24rubyProcessor.signtransaction(StringUtil.Trim( AV32wallet.gxTpr_Rubydevice), AV20networkTypeText, AV16inputsJson, StringUtil.Trim( AV28sendTo), AV8amountText, StringUtil.Trim( AV23returnTo), AV9changeChain, (int)(AV10changeIndex), AV12feeText, AV27sendAllCoins);
         AV25rubyResult.FromJSonString(AV18json, null);
         if ( AV25rubyResult.gxTpr_Success )
         {
            AV13hexTransaction = AV25rubyResult.gxTpr_Hextransaction;
            AV31virtualSize = AV25rubyResult.gxTpr_Virtualsize;
         }
         else
         {
            AV11error = StringUtil.Trim( AV25rubyResult.gxTpr_Error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV11error = "Ruby did not sign the payment";
            }
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
         AV13hexTransaction = "";
         AV11error = "";
         AV32wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV26sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV21one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV15inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtRubyInput>( context, "RubyInput", "distributedcryptography");
         AV22oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV14input = new GeneXus.Programs.wallet.SdtRubyInput(context);
         AV16inputsJson = "";
         AV20networkTypeText = "";
         AV8amountText = "";
         AV12feeText = "";
         AV18json = "";
         AV24rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV25rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         /* GeneXus formulas. */
      }

      private short AV9changeChain ;
      private int AV33GXV1 ;
      private int AV34GXV2 ;
      private long AV31virtualSize ;
      private long AV10changeIndex ;
      private decimal AV29transactionFee ;
      private decimal AV17inSendCoins ;
      private string AV19networkType ;
      private string AV28sendTo ;
      private string AV23returnTo ;
      private string AV11error ;
      private bool AV27sendAllCoins ;
      private string AV13hexTransaction ;
      private string AV16inputsJson ;
      private string AV18json ;
      private string AV20networkTypeText ;
      private string AV8amountText ;
      private string AV12feeText ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV30transactionsToSend ;
      private GeneXus.Programs.wallet.SdtWallet AV32wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV26sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV21one_sdt_address ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtRubyInput> AV15inputs ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV22oneAddressHistory ;
      private GeneXus.Programs.wallet.SdtRubyInput AV14input ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV24rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV25rubyResult ;
      private long aP7_virtualSize ;
      private string aP8_hexTransaction ;
      private string aP9_error ;
   }

}
