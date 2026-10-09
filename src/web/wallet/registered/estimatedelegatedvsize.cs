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
namespace GeneXus.Programs.wallet.registered {
   public class estimatedelegatedvsize : GXProcedure
   {
      public estimatedelegatedvsize( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public estimatedelegatedvsize( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           decimal aP1_sendCoins ,
                           string aP2_sendTo ,
                           string aP3_description ,
                           out long aP4_virtualSize ,
                           out string aP5_error )
      {
         this.AV19sendAllCoins = aP0_sendAllCoins;
         this.AV20sendCoins = aP1_sendCoins;
         this.AV21sendTo = aP2_sendTo;
         this.AV9description = aP3_description;
         this.AV25virtualSize = 0 ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP4_virtualSize=this.AV25virtualSize;
         aP5_error=this.AV10error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                string aP3_description ,
                                out long aP4_virtualSize )
      {
         execute(aP0_sendAllCoins, aP1_sendCoins, aP2_sendTo, aP3_description, out aP4_virtualSize, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_sendCoins ,
                                 string aP2_sendTo ,
                                 string aP3_description ,
                                 out long aP4_virtualSize ,
                                 out string aP5_error )
      {
         this.AV19sendAllCoins = aP0_sendAllCoins;
         this.AV20sendCoins = aP1_sendCoins;
         this.AV21sendTo = aP2_sendTo;
         this.AV9description = aP3_description;
         this.AV25virtualSize = 0 ;
         this.AV10error = "" ;
         SubmitImpl();
         aP4_virtualSize=this.AV25virtualSize;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWalletInfo1 = AV26walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV26walletInfo = GXt_SdtWalletInfo1;
         AV14networkType = AV26walletInfo.gxTpr_Networktype;
         AV11group_sdt.FromJSonString(AV27websession.Get("Group_EDIT_WALLET"), null);
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) )
         {
            AV10error = "Open the Wallet Balance tab of the group first";
            cleanup();
            if (true) return;
         }
         AV22transactionFee = NumberUtil.Val( "0.00001000", ".");
         GXt_objcol_SdtSDTAddressHistory2 = AV23transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV9description,  AV20sendCoins,  AV22transactionFee, out  GXt_objcol_SdtSDTAddressHistory2) ;
         AV23transactionsToSend = GXt_objcol_SdtSDTAddressHistory2;
         if ( AV23transactionsToSend.Count == 0 )
         {
            AV10error = "The group has no coins to send";
            cleanup();
            if (true) return;
         }
         AV24utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV23transactionsToSend.Count )
         {
            AV16oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23transactionsToSend.Item(AV28GXV1));
            AV17oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV17oneUtxo.gxTpr_Txid = StringUtil.Trim( AV16oneTransaction.gxTpr_Receivedtransactionid);
            AV17oneUtxo.gxTpr_Vout = AV16oneTransaction.gxTpr_Recivedn;
            AV17oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV16oneTransaction.gxTpr_Balance, 17, 8));
            AV17oneUtxo.gxTpr_Sequence = AV16oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV16oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV17oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV17oneUtxo.gxTpr_Ischange = false;
            }
            AV17oneUtxo.gxTpr_Address = StringUtil.Trim( AV16oneTransaction.gxTpr_Receivedaddress);
            AV24utxos.Add(AV17oneUtxo, 0);
            AV28GXV1 = (int)(AV28GXV1+1);
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV11group_sdt, out  AV12groupEO) ;
         AV15ok = AV8delegatedProcessor.fromsdt(AV12groupEO);
         if ( ! AV15ok )
         {
            AV10error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV13jsonText = AV8delegatedProcessor.estimatevsize(AV24utxos.ToJSonString(false), StringUtil.Trim( AV21sendTo), "", AV19sendAllCoins, AV11group_sdt.gxTpr_Amigroupowner, AV14networkType);
         AV18result.FromJSonString(AV13jsonText, null);
         if ( AV18result.gxTpr_Success )
         {
            AV25virtualSize = AV18result.gxTpr_Vsize;
         }
         else
         {
            AV10error = AV18result.gxTpr_Error;
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
         AV10error = "";
         AV26walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV14networkType = "";
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV27websession = context.GetSession();
         AV23transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory2 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV24utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV16oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV17oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         AV12groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV8delegatedProcessor = new GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor(context);
         AV13jsonText = "";
         AV18result = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult(context);
         /* GeneXus formulas. */
      }

      private int AV28GXV1 ;
      private long AV25virtualSize ;
      private decimal AV20sendCoins ;
      private decimal AV22transactionFee ;
      private string AV21sendTo ;
      private string AV10error ;
      private string AV14networkType ;
      private bool AV19sendAllCoins ;
      private bool AV15ok ;
      private string AV13jsonText ;
      private string AV9description ;
      private IGxSession AV27websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV26walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV23transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV24utxos ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV16oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV17oneUtxo ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV12groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor AV8delegatedProcessor ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult AV18result ;
      private long aP4_virtualSize ;
      private string aP5_error ;
   }

}
