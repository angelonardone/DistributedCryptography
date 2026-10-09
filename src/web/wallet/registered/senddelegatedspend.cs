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
   public class senddelegatedspend : GXProcedure
   {
      public senddelegatedspend( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public senddelegatedspend( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           decimal aP1_sendCoins ,
                           string aP2_sendTo ,
                           string aP3_description ,
                           decimal aP4_baseFee ,
                           string aP5_percentages ,
                           short aP6_finalPercent ,
                           out bool aP7_broadcast ,
                           out string aP8_errorTitle ,
                           out string aP9_error )
      {
         this.AV41sendAllCoins = aP0_sendAllCoins;
         this.AV42sendCoins = aP1_sendCoins;
         this.AV43sendTo = aP2_sendTo;
         this.AV17description = aP3_description;
         this.AV11baseFee = aP4_baseFee;
         this.AV37percentages = aP5_percentages;
         this.AV26finalPercent = aP6_finalPercent;
         this.AV12broadcast = false ;
         this.AV21errorTitle = "" ;
         this.AV19error = "" ;
         initialize();
         ExecuteImpl();
         aP7_broadcast=this.AV12broadcast;
         aP8_errorTitle=this.AV21errorTitle;
         aP9_error=this.AV19error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                string aP3_description ,
                                decimal aP4_baseFee ,
                                string aP5_percentages ,
                                short aP6_finalPercent ,
                                out bool aP7_broadcast ,
                                out string aP8_errorTitle )
      {
         execute(aP0_sendAllCoins, aP1_sendCoins, aP2_sendTo, aP3_description, aP4_baseFee, aP5_percentages, aP6_finalPercent, out aP7_broadcast, out aP8_errorTitle, out aP9_error);
         return AV19error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_sendCoins ,
                                 string aP2_sendTo ,
                                 string aP3_description ,
                                 decimal aP4_baseFee ,
                                 string aP5_percentages ,
                                 short aP6_finalPercent ,
                                 out bool aP7_broadcast ,
                                 out string aP8_errorTitle ,
                                 out string aP9_error )
      {
         this.AV41sendAllCoins = aP0_sendAllCoins;
         this.AV42sendCoins = aP1_sendCoins;
         this.AV43sendTo = aP2_sendTo;
         this.AV17description = aP3_description;
         this.AV11baseFee = aP4_baseFee;
         this.AV37percentages = aP5_percentages;
         this.AV26finalPercent = aP6_finalPercent;
         this.AV12broadcast = false ;
         this.AV21errorTitle = "" ;
         this.AV19error = "" ;
         SubmitImpl();
         aP7_broadcast=this.AV12broadcast;
         aP8_errorTitle=this.AV21errorTitle;
         aP9_error=this.AV19error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12broadcast = false;
         AV53chosenPercent = AV26finalPercent;
         GXt_SdtWalletInfo1 = AV51walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV51walletInfo = GXt_SdtWalletInfo1;
         AV32networkType = AV51walletInfo.gxTpr_Networktype;
         AV27group_sdt.FromJSonString(AV52websession.Get("Group_EDIT_WALLET"), null);
         AV34oneMuSigSignatures.FromJSonString(AV52websession.Get("MuSign_ONE"), null);
         AV47transactionFileName = StringUtil.Trim( AV27group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         GXt_SdtExtKeyInfo2 = AV23extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV23extKeyInfo = GXt_SdtExtKeyInfo2;
         AV8accountXprv = StringUtil.Trim( AV23extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8accountXprv)) )
         {
            AV21errorTitle = "Signing key not available";
            AV19error = "Please re-enter your password";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'DERIVE CHAIN KEYS' */
         S171 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19error)) )
         {
            AV21errorTitle = "We couldn't prepare the signing key: ";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV27group_sdt, out  AV28groupEO) ;
         AV33ok = AV16delegatedProcessor.fromsdt(AV28groupEO);
         if ( ! AV33ok )
         {
            AV21errorTitle = "We couldn't load the group";
            AV19error = "Open the Wallet Balance tab of the group and try again";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         if ( AV27group_sdt.gxTpr_Amigroupowner )
         {
            /* Execute user subroutine: 'OWNER SPENDS ALONE' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( (Guid.Empty==AV34oneMuSigSignatures.gxTpr_Id) )
         {
            /* Execute user subroutine: 'FIRST SIGNER' */
            S141 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else
         {
            /* Execute user subroutine: 'NEXT SIGNER' */
            S161 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV8accountXprv = "";
         AV40receivingXprv = "";
         AV15changeXprv = "";
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         cleanup();
      }

      protected void S111( )
      {
         /* 'OWNER SPENDS ALONE' Routine */
         returnInSub = false;
         AV9allCoins = AV41sendAllCoins;
         AV10amount = AV42sendCoins;
         AV18destination = StringUtil.Trim( AV43sendTo);
         AV25fee = AV11baseFee;
         GXt_objcol_SdtSDTAddressHistory3 = AV49transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV17description,  AV10amount,  AV25fee, out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV49transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
         GXt_char4 = AV14changeTo;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
         AV14changeTo = GXt_char4;
         /* Execute user subroutine: 'BUILD UTXOS' */
         S121 ();
         if (returnInSub) return;
         AV29jsonText = AV16delegatedProcessor.ownerspend(AV50utxos.ToJSonString(false), AV18destination, StringUtil.Trim( StringUtil.Str( AV10amount, 17, 8)), StringUtil.Trim( AV14changeTo), StringUtil.Trim( StringUtil.Str( AV25fee, 17, 8)), AV9allCoins, AV40receivingXprv, AV15changeXprv, AV32networkType);
         AV46spendResult.FromJSonString(AV29jsonText, null);
         if ( ! AV46spendResult.gxTpr_Success )
         {
            AV21errorTitle = "There was a problem building the transaction: ";
            AV19error = AV46spendResult.gxTpr_Error;
         }
         else
         {
            /* Execute user subroutine: 'BROADCAST' */
            S131 ();
            if (returnInSub) return;
         }
      }

      protected void S141( )
      {
         /* 'FIRST SIGNER' Routine */
         returnInSub = false;
         AV9allCoins = AV41sendAllCoins;
         AV10amount = AV42sendCoins;
         AV18destination = StringUtil.Trim( AV43sendTo);
         AV25fee = AV11baseFee;
         AV44signatureID = AV34oneMuSigSignatures.gxTpr_Id;
         AV31maxPercent = 0;
         AV39percentTokens = (GxSimpleCollection<string>)(GxRegex.Split(AV37percentages,","));
         AV54GXV1 = 1;
         while ( AV54GXV1 <= AV39percentTokens.Count )
         {
            AV38percentToken = ((string)AV39percentTokens.Item(AV54GXV1));
            if ( ( NumberUtil.Val( StringUtil.Trim( AV38percentToken), ".") > Convert.ToDecimal( AV31maxPercent )) )
            {
               AV31maxPercent = (short)(Math.Round(NumberUtil.Val( StringUtil.Trim( AV38percentToken), "."), 18, MidpointRounding.ToEven));
            }
            AV54GXV1 = (int)(AV54GXV1+1);
         }
         AV30maxFee = (decimal)(AV25fee*(100+AV31maxPercent)/ (decimal)(100));
         GXt_objcol_SdtSDTAddressHistory3 = AV49transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV17description,  AV10amount,  AV30maxFee, out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV49transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
         GXt_char4 = AV14changeTo;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
         AV14changeTo = GXt_char4;
         /* Execute user subroutine: 'BUILD UTXOS' */
         S121 ();
         if (returnInSub) return;
         AV29jsonText = AV16delegatedProcessor.buildspend(AV50utxos.ToJSonString(false), AV18destination, StringUtil.Trim( StringUtil.Str( AV10amount, 17, 8)), StringUtil.Trim( AV14changeTo), StringUtil.Trim( StringUtil.Str( AV25fee, 17, 8)), StringUtil.Trim( AV37percentages), AV9allCoins, AV32networkType);
         AV46spendResult.FromJSonString(AV29jsonText, null);
         if ( ! AV46spendResult.gxTpr_Success )
         {
            AV21errorTitle = "There was a problem building the transaction: ";
            AV19error = AV46spendResult.gxTpr_Error;
         }
         else
         {
            if ( AV27group_sdt.gxTpr_Minimumshares == 1 )
            {
               if ( AV53chosenPercent < 0 )
               {
                  AV53chosenPercent = 0;
               }
            }
            else
            {
               AV53chosenPercent = -1;
            }
            AV29jsonText = AV16delegatedProcessor.signspend(AV46spendResult.gxTpr_Bundle, AV40receivingXprv, AV15changeXprv, AV53chosenPercent, AV32networkType);
            AV46spendResult.FromJSonString(AV29jsonText, null);
            if ( ! AV46spendResult.gxTpr_Success )
            {
               AV21errorTitle = "There was a problem signing the transaction: ";
               AV19error = AV46spendResult.gxTpr_Error;
            }
            else
            {
               /* Execute user subroutine: 'FINISH OR FORWARD' */
               S151 ();
               if (returnInSub) return;
            }
         }
      }

      protected void S161( )
      {
         /* 'NEXT SIGNER' Routine */
         returnInSub = false;
         AV9allCoins = AV34oneMuSigSignatures.gxTpr_Sendallcoins;
         AV25fee = AV34oneMuSigSignatures.gxTpr_Transactionfee;
         AV49transactionsToSend = AV34oneMuSigSignatures.gxTpr_Transactions;
         AV44signatureID = AV34oneMuSigSignatures.gxTpr_Id;
         AV29jsonText = AV16delegatedProcessor.describespend(StringUtil.Trim( AV34oneMuSigSignatures.gxTpr_Psbt), StringUtil.Trim( AV27group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV27group_sdt.gxTpr_Extpubkeymultisigchange), AV32networkType);
         AV45spendInfo.FromJSonString(AV29jsonText, null);
         if ( ! AV45spendInfo.gxTpr_Success )
         {
            AV21errorTitle = "This payment can not be signed: ";
            AV19error = AV45spendInfo.gxTpr_Error;
         }
         else
         {
            AV10amount = AV45spendInfo.gxTpr_Amountbtc;
            AV18destination = StringUtil.Trim( AV45spendInfo.gxTpr_Sendto);
            AV14changeTo = StringUtil.Trim( AV45spendInfo.gxTpr_Changeto);
            AV13changeBelongs = true;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14changeTo)) )
            {
               GXt_boolean5 = AV13changeBelongs;
               GXt_boolean6 = false;
               new GeneXus.Programs.wallet.doesaddressbelongtowallet(context ).execute(  AV14changeTo,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), ref  GXt_boolean6, out  GXt_boolean5) ;
               AV13changeBelongs = GXt_boolean5;
            }
            if ( ! AV13changeBelongs )
            {
               AV21errorTitle = "This payment can not be signed: ";
               AV19error = "The 'change address' does not belong to this group or it has already been used";
            }
            else
            {
               if ( AV45spendInfo.gxTpr_Nextislast )
               {
                  if ( AV53chosenPercent < 0 )
                  {
                     AV53chosenPercent = 0;
                  }
               }
               else
               {
                  AV53chosenPercent = -1;
               }
               AV29jsonText = AV16delegatedProcessor.signspend(StringUtil.Trim( AV34oneMuSigSignatures.gxTpr_Psbt), AV40receivingXprv, AV15changeXprv, AV53chosenPercent, AV32networkType);
               AV46spendResult.FromJSonString(AV29jsonText, null);
               if ( ! AV46spendResult.gxTpr_Success )
               {
                  AV21errorTitle = "There was a problem signing the transaction: ";
                  AV19error = AV46spendResult.gxTpr_Error;
               }
               else
               {
                  /* Execute user subroutine: 'FINISH OR FORWARD' */
                  S151 ();
                  if (returnInSub) return;
               }
            }
         }
      }

      protected void S151( )
      {
         /* 'FINISH OR FORWARD' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV46spendResult.gxTpr_Txhex)) )
         {
            /* Execute user subroutine: 'BROADCAST' */
            S131 ();
            if (returnInSub) return;
            if ( AV12broadcast )
            {
               AV25fee = AV46spendResult.gxTpr_Feebtc;
               GXt_char4 = AV20errorAfter;
               new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV27group_sdt,  AV9allCoins,  AV49transactionsToSend,  AV10amount,  AV18destination,  AV14changeTo,  AV25fee,  AV44signatureID,  true,  AV46spendResult.gxTpr_Bundle, out  GXt_char4) ;
               AV20errorAfter = GXt_char4;
            }
         }
         else
         {
            GXt_char4 = AV19error;
            new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV27group_sdt,  AV9allCoins,  AV49transactionsToSend,  AV10amount,  AV18destination,  AV14changeTo,  AV25fee,  AV44signatureID,  false,  AV46spendResult.gxTpr_Bundle, out  GXt_char4) ;
            AV19error = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19error)) )
            {
               AV21errorTitle = "There was a problem forwarding the signature: ";
            }
         }
      }

      protected void S131( )
      {
         /* 'BROADCAST' Routine */
         returnInSub = false;
         GXt_char4 = AV19error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV46spendResult.gxTpr_Txhex, out  AV48TransactionId, out  GXt_char4) ;
         AV19error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV19error)) )
         {
            GXt_char4 = AV20errorAfter;
            new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV47transactionFileName,  AV48TransactionId,  AV49transactionsToSend, out  GXt_char4) ;
            AV20errorAfter = GXt_char4;
            AV55GXV2 = 1;
            while ( AV55GXV2 <= AV49transactionsToSend.Count )
            {
               AV35oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV49transactionsToSend.Item(AV55GXV2));
               AV35oneTransaction.gxTpr_Senttransactionid = StringUtil.Trim( AV48TransactionId);
               AV55GXV2 = (int)(AV55GXV2+1);
            }
            AV12broadcast = true;
         }
         else
         {
            AV21errorTitle = "There was a problem submitting the transaction: ";
         }
      }

      protected void S121( )
      {
         /* 'BUILD UTXOS' Routine */
         returnInSub = false;
         AV50utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV56GXV3 = 1;
         while ( AV56GXV3 <= AV49transactionsToSend.Count )
         {
            AV35oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV49transactionsToSend.Item(AV56GXV3));
            AV36oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV36oneUtxo.gxTpr_Txid = StringUtil.Trim( AV35oneTransaction.gxTpr_Receivedtransactionid);
            AV36oneUtxo.gxTpr_Vout = AV35oneTransaction.gxTpr_Recivedn;
            AV36oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV35oneTransaction.gxTpr_Balance, 17, 8));
            AV36oneUtxo.gxTpr_Sequence = AV35oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV35oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV36oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV36oneUtxo.gxTpr_Ischange = false;
            }
            AV36oneUtxo.gxTpr_Address = StringUtil.Trim( AV35oneTransaction.gxTpr_Receivedaddress);
            AV50utxos.Add(AV36oneUtxo, 0);
            AV56GXV3 = (int)(AV56GXV3+1);
         }
      }

      protected void S171( )
      {
         /* 'DERIVE CHAIN KEYS' Routine */
         returnInSub = false;
         AV22extKeyCreateChain.gxTpr_Createextkeytype = 70;
         AV22extKeyCreateChain.gxTpr_Extendedprivatekey = AV8accountXprv;
         AV22extKeyCreateChain.gxTpr_Networktype = AV32networkType;
         AV22extKeyCreateChain.gxTpr_Keypath = "2";
         GXt_char4 = AV19error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV22extKeyCreateChain,  "", out  AV24extKeyInfoChain, out  GXt_char4) ;
         AV19error = GXt_char4;
         AV40receivingXprv = StringUtil.Trim( AV24extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV19error)) )
         {
            AV24extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
            AV22extKeyCreateChain.gxTpr_Keypath = "3";
            GXt_char4 = AV19error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV22extKeyCreateChain,  "", out  AV24extKeyInfoChain, out  GXt_char4) ;
            AV19error = GXt_char4;
            AV15changeXprv = StringUtil.Trim( AV24extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         }
         AV24extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV22extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
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
         AV21errorTitle = "";
         AV19error = "";
         AV51walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV32networkType = "";
         AV27group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV52websession = context.GetSession();
         AV34oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV47transactionFileName = "";
         AV23extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV8accountXprv = "";
         AV28groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV16delegatedProcessor = new GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor(context);
         AV40receivingXprv = "";
         AV15changeXprv = "";
         AV18destination = "";
         AV49transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV14changeTo = "";
         AV29jsonText = "";
         AV50utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV46spendResult = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult(context);
         AV44signatureID = Guid.Empty;
         AV39percentTokens = new GxSimpleCollection<string>();
         AV38percentToken = "";
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV45spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV20errorAfter = "";
         AV48TransactionId = "";
         AV35oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV36oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         AV22extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV24extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV26finalPercent ;
      private short AV53chosenPercent ;
      private short AV31maxPercent ;
      private int AV54GXV1 ;
      private int AV55GXV2 ;
      private int AV56GXV3 ;
      private decimal AV42sendCoins ;
      private decimal AV11baseFee ;
      private decimal AV10amount ;
      private decimal AV25fee ;
      private decimal AV30maxFee ;
      private string AV43sendTo ;
      private string AV21errorTitle ;
      private string AV19error ;
      private string AV32networkType ;
      private string AV47transactionFileName ;
      private string AV8accountXprv ;
      private string AV40receivingXprv ;
      private string AV15changeXprv ;
      private string AV18destination ;
      private string AV14changeTo ;
      private string AV20errorAfter ;
      private string AV48TransactionId ;
      private string GXt_char4 ;
      private bool AV41sendAllCoins ;
      private bool AV12broadcast ;
      private bool returnInSub ;
      private bool AV33ok ;
      private bool AV9allCoins ;
      private bool AV13changeBelongs ;
      private bool GXt_boolean5 ;
      private bool GXt_boolean6 ;
      private string AV29jsonText ;
      private string AV17description ;
      private string AV37percentages ;
      private string AV38percentToken ;
      private Guid AV44signatureID ;
      private IGxSession AV52websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV51walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV27group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV34oneMuSigSignatures ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV23extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV28groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor AV16delegatedProcessor ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV49transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV50utxos ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult AV46spendResult ;
      private GxSimpleCollection<string> AV39percentTokens ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV45spendInfo ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV35oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV36oneUtxo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV22extKeyCreateChain ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV24extKeyInfoChain ;
      private bool aP7_broadcast ;
      private string aP8_errorTitle ;
      private string aP9_error ;
   }

}
