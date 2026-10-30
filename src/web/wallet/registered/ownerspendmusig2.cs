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
   public class ownerspendmusig2 : GXProcedure
   {
      public ownerspendmusig2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public ownerspendmusig2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           decimal aP1_sendCoins ,
                           string aP2_sendTo ,
                           string aP3_description ,
                           decimal aP4_fee ,
                           out bool aP5_broadcast ,
                           out string aP6_errorTitle ,
                           out string aP7_error )
      {
         this.AV28sendAllCoins = aP0_sendAllCoins;
         this.AV29sendCoins = aP1_sendCoins;
         this.AV30sendTo = aP2_sendTo;
         this.AV12description = aP3_description;
         this.AV19fee = aP4_fee;
         this.AV9broadcast = false ;
         this.AV15errorTitle = "" ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP5_broadcast=this.AV9broadcast;
         aP6_errorTitle=this.AV15errorTitle;
         aP7_error=this.AV13error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                string aP3_description ,
                                decimal aP4_fee ,
                                out bool aP5_broadcast ,
                                out string aP6_errorTitle )
      {
         execute(aP0_sendAllCoins, aP1_sendCoins, aP2_sendTo, aP3_description, aP4_fee, out aP5_broadcast, out aP6_errorTitle, out aP7_error);
         return AV13error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_sendCoins ,
                                 string aP2_sendTo ,
                                 string aP3_description ,
                                 decimal aP4_fee ,
                                 out bool aP5_broadcast ,
                                 out string aP6_errorTitle ,
                                 out string aP7_error )
      {
         this.AV28sendAllCoins = aP0_sendAllCoins;
         this.AV29sendCoins = aP1_sendCoins;
         this.AV30sendTo = aP2_sendTo;
         this.AV12description = aP3_description;
         this.AV19fee = aP4_fee;
         this.AV9broadcast = false ;
         this.AV15errorTitle = "" ;
         this.AV13error = "" ;
         SubmitImpl();
         aP5_broadcast=this.AV9broadcast;
         aP6_errorTitle=this.AV15errorTitle;
         aP7_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9broadcast = false;
         GXt_SdtWalletInfo1 = AV36walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV36walletInfo = GXt_SdtWalletInfo1;
         AV20group_sdt.FromJSonString(AV37websession.Get("Group_EDIT_WALLET"), null);
         if ( ! ( ( AV20group_sdt.gxTpr_Grouptype == 80 ) && AV20group_sdt.gxTpr_Amigroupowner ) )
         {
            AV15errorTitle = "Only the owner pays alone";
            AV13error = "Open a signing room to pay with the other members";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         AV32transactionFileName = StringUtil.Trim( AV20group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         GXt_SdtExtKeyInfo2 = AV17extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV17extKeyInfo = GXt_SdtExtKeyInfo2;
         AV8accountXprv = StringUtil.Trim( AV17extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8accountXprv)) )
         {
            AV15errorTitle = "Signing key not available";
            AV13error = "Please re-enter your password";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'DERIVE CHAIN KEYS' */
         S121 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV8accountXprv = "";
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV15errorTitle = "We couldn't prepare the signing key: ";
            cleanup();
            if (true) return;
         }
         GXt_objcol_SdtSDTAddressHistory3 = AV34transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV12description,  AV29sendCoins,  AV19fee, out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV34transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
         GXt_char4 = AV10changeTo;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
         AV10changeTo = GXt_char4;
         /* Execute user subroutine: 'BUILD UTXOS' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV20group_sdt, out  AV21groupEO) ;
         AV24ok = AV23muSig2Processor.fromsdt(AV21groupEO);
         AV22jsonText = AV23muSig2Processor.ownerspend(AV35utxos.ToJSonString(false), StringUtil.Trim( AV30sendTo), StringUtil.Trim( StringUtil.Str( AV29sendCoins, 17, 8)), StringUtil.Trim( AV10changeTo), StringUtil.Trim( StringUtil.Str( AV19fee, 17, 8)), AV28sendAllCoins, AV27receivingXprv, AV11changeXprv, AV36walletInfo.gxTpr_Networktype);
         AV27receivingXprv = "";
         AV11changeXprv = "";
         AV31spendResult.FromJSonString(AV22jsonText, null);
         if ( ! AV31spendResult.gxTpr_Success )
         {
            AV15errorTitle = "There was a problem building the transaction: ";
            AV13error = AV31spendResult.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV31spendResult.gxTpr_Txhex, out  AV33TransactionId, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char4 = AV14errorAfter;
            new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV32transactionFileName,  AV33TransactionId,  AV34transactionsToSend, out  GXt_char4) ;
            AV14errorAfter = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14errorAfter)) )
            {
               GXt_char4 = AV14errorAfter;
               new GeneXus.Programs.wallet.recordsentoutputs(context ).execute(  AV32transactionFileName,  AV33TransactionId,  AV31spendResult.gxTpr_Txhex, out  GXt_char4) ;
               AV14errorAfter = GXt_char4;
            }
            AV9broadcast = true;
         }
         else
         {
            AV15errorTitle = "There was a problem submitting the transaction: ";
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'BUILD UTXOS' Routine */
         returnInSub = false;
         AV35utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV38GXV1 = 1;
         while ( AV38GXV1 <= AV34transactionsToSend.Count )
         {
            AV25oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV34transactionsToSend.Item(AV38GXV1));
            AV26oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV26oneUtxo.gxTpr_Txid = StringUtil.Trim( AV25oneTransaction.gxTpr_Receivedtransactionid);
            AV26oneUtxo.gxTpr_Vout = AV25oneTransaction.gxTpr_Recivedn;
            AV26oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV25oneTransaction.gxTpr_Balance, 17, 8));
            AV26oneUtxo.gxTpr_Sequence = AV25oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV25oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV26oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV26oneUtxo.gxTpr_Ischange = false;
            }
            AV26oneUtxo.gxTpr_Address = StringUtil.Trim( AV25oneTransaction.gxTpr_Receivedaddress);
            AV35utxos.Add(AV26oneUtxo, 0);
            AV38GXV1 = (int)(AV38GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'DERIVE CHAIN KEYS' Routine */
         returnInSub = false;
         AV16extKeyCreateChain.gxTpr_Createextkeytype = 70;
         AV16extKeyCreateChain.gxTpr_Extendedprivatekey = AV8accountXprv;
         AV16extKeyCreateChain.gxTpr_Networktype = AV36walletInfo.gxTpr_Networktype;
         AV16extKeyCreateChain.gxTpr_Keypath = "2";
         GXt_char4 = AV13error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV16extKeyCreateChain,  "", out  AV18extKeyInfoChain, out  GXt_char4) ;
         AV13error = GXt_char4;
         AV27receivingXprv = StringUtil.Trim( AV18extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV18extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
            AV16extKeyCreateChain.gxTpr_Keypath = "3";
            GXt_char4 = AV13error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV16extKeyCreateChain,  "", out  AV18extKeyInfoChain, out  GXt_char4) ;
            AV13error = GXt_char4;
            AV11changeXprv = StringUtil.Trim( AV18extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         }
         AV18extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV16extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
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
         AV15errorTitle = "";
         AV13error = "";
         AV36walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV20group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV37websession = context.GetSession();
         AV32transactionFileName = "";
         AV17extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV8accountXprv = "";
         AV34transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV10changeTo = "";
         AV21groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV23muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV22jsonText = "";
         AV35utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV27receivingXprv = "";
         AV11changeXprv = "";
         AV31spendResult = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult(context);
         AV33TransactionId = "";
         AV14errorAfter = "";
         AV25oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV26oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         AV16extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV18extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private int AV38GXV1 ;
      private decimal AV29sendCoins ;
      private decimal AV19fee ;
      private string AV30sendTo ;
      private string AV15errorTitle ;
      private string AV13error ;
      private string AV32transactionFileName ;
      private string AV8accountXprv ;
      private string AV10changeTo ;
      private string AV27receivingXprv ;
      private string AV11changeXprv ;
      private string AV33TransactionId ;
      private string AV14errorAfter ;
      private string GXt_char4 ;
      private bool AV28sendAllCoins ;
      private bool AV9broadcast ;
      private bool returnInSub ;
      private bool AV24ok ;
      private string AV22jsonText ;
      private string AV12description ;
      private IGxSession AV37websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV36walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV17extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV34transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV21groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV23muSig2Processor ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV35utxos ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult AV31spendResult ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV25oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV26oneUtxo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV16extKeyCreateChain ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV18extKeyInfoChain ;
      private bool aP5_broadcast ;
      private string aP6_errorTitle ;
      private string aP7_error ;
   }

}
