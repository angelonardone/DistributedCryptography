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
   public class estimatemusig2vsize : GXProcedure
   {
      public estimatemusig2vsize( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public estimatemusig2vsize( IGxContext context )
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
         this.AV8description = aP3_description;
         this.AV25virtualSize = 0 ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP4_virtualSize=this.AV25virtualSize;
         aP5_error=this.AV9error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                string aP3_description ,
                                out long aP4_virtualSize )
      {
         execute(aP0_sendAllCoins, aP1_sendCoins, aP2_sendTo, aP3_description, out aP4_virtualSize, out aP5_error);
         return AV9error ;
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
         this.AV8description = aP3_description;
         this.AV25virtualSize = 0 ;
         this.AV9error = "" ;
         SubmitImpl();
         aP4_virtualSize=this.AV25virtualSize;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWalletInfo1 = AV26walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV26walletInfo = GXt_SdtWalletInfo1;
         AV10group_sdt.FromJSonString(AV27websession.Get("Group_EDIT_WALLET"), null);
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) )
         {
            AV9error = "Open the Wallet Balance tab of the group first";
            cleanup();
            if (true) return;
         }
         AV22transactionFee = NumberUtil.Val( "0.00001000", ".");
         GXt_objcol_SdtSDTAddressHistory2 = AV23transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV8description,  AV20sendCoins,  AV22transactionFee, out  GXt_objcol_SdtSDTAddressHistory2) ;
         AV23transactionsToSend = GXt_objcol_SdtSDTAddressHistory2;
         if ( AV23transactionsToSend.Count == 0 )
         {
            AV9error = "The group has no coins to send";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'BUILD UTXOS' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV10group_sdt, out  AV11groupEO) ;
         AV14ok = AV13muSig2Processor.fromsdt(AV11groupEO);
         if ( ! AV14ok )
         {
            AV9error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         if ( ( AV10group_sdt.gxTpr_Grouptype == 80 ) && AV10group_sdt.gxTpr_Amigroupowner )
         {
            AV17path = "owner";
         }
         else
         {
            AV17path = "leaf";
         }
         AV12jsonText = AV13muSig2Processor.estimatevsize(AV24utxos.ToJSonString(false), StringUtil.Trim( AV21sendTo), "", AV19sendAllCoins, AV17path, AV26walletInfo.gxTpr_Networktype);
         AV18result.FromJSonString(AV12jsonText, null);
         if ( AV18result.gxTpr_Success )
         {
            AV25virtualSize = AV18result.gxTpr_Vsize;
         }
         else
         {
            AV9error = AV18result.gxTpr_Error;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'BUILD UTXOS' Routine */
         returnInSub = false;
         AV24utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV23transactionsToSend.Count )
         {
            AV15oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23transactionsToSend.Item(AV28GXV1));
            AV16oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV16oneUtxo.gxTpr_Txid = StringUtil.Trim( AV15oneTransaction.gxTpr_Receivedtransactionid);
            AV16oneUtxo.gxTpr_Vout = AV15oneTransaction.gxTpr_Recivedn;
            AV16oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV15oneTransaction.gxTpr_Balance, 17, 8));
            AV16oneUtxo.gxTpr_Sequence = AV15oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV15oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV16oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV16oneUtxo.gxTpr_Ischange = false;
            }
            AV16oneUtxo.gxTpr_Address = StringUtil.Trim( AV15oneTransaction.gxTpr_Receivedaddress);
            AV24utxos.Add(AV16oneUtxo, 0);
            AV28GXV1 = (int)(AV28GXV1+1);
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
         AV9error = "";
         AV26walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV27websession = context.GetSession();
         AV23transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory2 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV11groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV13muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV17path = "";
         AV12jsonText = "";
         AV24utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV18result = new GeneXus.Programs.wallet.registered.SdtMuSig2VsizeResult(context);
         AV15oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV16oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         /* GeneXus formulas. */
      }

      private int AV28GXV1 ;
      private long AV25virtualSize ;
      private decimal AV20sendCoins ;
      private decimal AV22transactionFee ;
      private string AV21sendTo ;
      private string AV9error ;
      private bool AV19sendAllCoins ;
      private bool returnInSub ;
      private bool AV14ok ;
      private string AV12jsonText ;
      private string AV8description ;
      private string AV17path ;
      private IGxSession AV27websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV26walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV23transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV11groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV13muSig2Processor ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV24utxos ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2VsizeResult AV18result ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV15oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV16oneUtxo ;
      private long aP4_virtualSize ;
      private string aP5_error ;
   }

}
