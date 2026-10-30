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
   public class sendlegacyspend : GXProcedure
   {
      public sendlegacyspend( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendlegacyspend( IGxContext context )
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
         this.AV36sendAllCoins = aP0_sendAllCoins;
         this.AV37sendCoins = aP1_sendCoins;
         this.AV38sendTo = aP2_sendTo;
         this.AV15description = aP3_description;
         this.AV51baseFee = aP4_baseFee;
         this.AV61percentages = aP5_percentages;
         this.AV48finalPercent = aP6_finalPercent;
         this.AV10broadcast = false ;
         this.AV19errorTitle = "" ;
         this.AV17error = "" ;
         initialize();
         ExecuteImpl();
         aP7_broadcast=this.AV10broadcast;
         aP8_errorTitle=this.AV19errorTitle;
         aP9_error=this.AV17error;
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
         return AV17error ;
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
         this.AV36sendAllCoins = aP0_sendAllCoins;
         this.AV37sendCoins = aP1_sendCoins;
         this.AV38sendTo = aP2_sendTo;
         this.AV15description = aP3_description;
         this.AV51baseFee = aP4_baseFee;
         this.AV61percentages = aP5_percentages;
         this.AV48finalPercent = aP6_finalPercent;
         this.AV10broadcast = false ;
         this.AV19errorTitle = "" ;
         this.AV17error = "" ;
         SubmitImpl();
         aP7_broadcast=this.AV10broadcast;
         aP8_errorTitle=this.AV19errorTitle;
         aP9_error=this.AV17error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10broadcast = false;
         AV47chosenPercent = AV48finalPercent;
         GXt_SdtWalletInfo1 = AV44walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV44walletInfo = GXt_SdtWalletInfo1;
         AV30networkType = AV44walletInfo.gxTpr_Networktype;
         AV25group_sdt.FromJSonString(AV46websession.Get("Group_EDIT_WALLET"), null);
         AV32oneMuSigSignatures.FromJSonString(AV46websession.Get("MuSign_ONE"), null);
         AV42transactionFileName = StringUtil.Trim( AV25group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         GXt_SdtExtKeyInfo2 = AV55extKeyInfo;
         new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV55extKeyInfo = GXt_SdtExtKeyInfo2;
         AV50accountXprv = StringUtil.Trim( AV55extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV50accountXprv)) )
         {
            AV19errorTitle = "Signing key not available";
            AV17error = "Please re-enter your password";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'DERIVE CHAIN KEYS' */
         S161 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
         {
            AV19errorTitle = "We couldn't prepare the signing key: ";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptolegacyeo(context ).execute(  AV25group_sdt, out  AV56groupEO) ;
         AV59ok = AV54groupProcessor.fromsdt(AV56groupEO);
         if ( ! AV59ok )
         {
            AV19errorTitle = "We couldn't load the group";
            AV17error = "Open the Wallet Balance tab of the group and try again";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         if ( (Guid.Empty==AV32oneMuSigSignatures.gxTpr_Id) )
         {
            /* Execute user subroutine: 'FIRST SIGNER' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else
         {
            /* Execute user subroutine: 'NEXT SIGNER' */
            S141 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV50accountXprv = "";
         AV64receivingXprv = "";
         AV53changeXprv = "";
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         cleanup();
      }

      protected void S111( )
      {
         /* 'FIRST SIGNER' Routine */
         returnInSub = false;
         AV8allCoins = AV36sendAllCoins;
         AV9amount = AV37sendCoins;
         AV16destination = StringUtil.Trim( AV38sendTo);
         AV23fee = AV51baseFee;
         AV65signatureID = AV32oneMuSigSignatures.gxTpr_Id;
         AV49maxPercent = 0;
         AV63percentTokens = (GxSimpleCollection<string>)(GxRegex.Split(AV61percentages,","));
         AV73GXV1 = 1;
         while ( AV73GXV1 <= AV63percentTokens.Count )
         {
            AV62percentToken = ((string)AV63percentTokens.Item(AV73GXV1));
            if ( ( NumberUtil.Val( StringUtil.Trim( AV62percentToken), ".") > Convert.ToDecimal( AV49maxPercent )) )
            {
               AV49maxPercent = (short)(Math.Round(NumberUtil.Val( StringUtil.Trim( AV62percentToken), "."), 18, MidpointRounding.ToEven));
            }
            AV73GXV1 = (int)(AV73GXV1+1);
         }
         AV58maxFee = (decimal)(AV23fee*(100+AV49maxPercent)/ (decimal)(100));
         GXt_objcol_SdtSDTAddressHistory3 = AV43transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV15description,  AV9amount,  AV58maxFee, out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV43transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
         GXt_char4 = AV12changeTo;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
         AV12changeTo = GXt_char4;
         /* Execute user subroutine: 'BUILD UTXOS' */
         S121 ();
         if (returnInSub) return;
         AV57jsonText = AV54groupProcessor.buildlegacyspend(AV68utxos.ToJSonString(false), AV16destination, StringUtil.Trim( StringUtil.Str( AV9amount, 17, 8)), StringUtil.Trim( AV12changeTo), StringUtil.Trim( StringUtil.Str( AV23fee, 17, 8)), StringUtil.Trim( AV61percentages), AV8allCoins, AV30networkType);
         AV67spendResult.FromJSonString(AV57jsonText, null);
         if ( ! AV67spendResult.gxTpr_Success )
         {
            AV19errorTitle = "There was a problem building the transaction: ";
            AV17error = AV67spendResult.gxTpr_Error;
         }
         else
         {
            if ( AV25group_sdt.gxTpr_Minimumshares == 1 )
            {
               if ( AV47chosenPercent < 0 )
               {
                  AV47chosenPercent = 0;
               }
            }
            else
            {
               AV47chosenPercent = -1;
            }
            AV57jsonText = AV54groupProcessor.signlegacyspend(AV67spendResult.gxTpr_Bundle, AV64receivingXprv, AV53changeXprv, AV47chosenPercent, AV30networkType);
            AV67spendResult.FromJSonString(AV57jsonText, null);
            if ( ! AV67spendResult.gxTpr_Success )
            {
               AV19errorTitle = "There was a problem signing the transaction: ";
               AV17error = AV67spendResult.gxTpr_Error;
            }
            else
            {
               /* Execute user subroutine: 'FINISH OR FORWARD' */
               S131 ();
               if (returnInSub) return;
            }
         }
      }

      protected void S141( )
      {
         /* 'NEXT SIGNER' Routine */
         returnInSub = false;
         AV8allCoins = AV32oneMuSigSignatures.gxTpr_Sendallcoins;
         AV23fee = AV32oneMuSigSignatures.gxTpr_Transactionfee;
         AV43transactionsToSend = AV32oneMuSigSignatures.gxTpr_Transactions;
         AV65signatureID = AV32oneMuSigSignatures.gxTpr_Id;
         AV57jsonText = AV54groupProcessor.describelegacyspend(StringUtil.Trim( AV32oneMuSigSignatures.gxTpr_Psbt), StringUtil.Trim( AV25group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV25group_sdt.gxTpr_Extpubkeymultisigchange), AV30networkType);
         AV66spendInfo.FromJSonString(AV57jsonText, null);
         if ( ! AV66spendInfo.gxTpr_Success )
         {
            AV19errorTitle = "This payment can not be signed: ";
            AV17error = AV66spendInfo.gxTpr_Error;
         }
         else
         {
            AV9amount = AV66spendInfo.gxTpr_Amountbtc;
            AV16destination = StringUtil.Trim( AV66spendInfo.gxTpr_Sendto);
            AV12changeTo = StringUtil.Trim( AV66spendInfo.gxTpr_Changeto);
            AV52changeBelongs = true;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12changeTo)) )
            {
               GXt_boolean5 = AV52changeBelongs;
               GXt_boolean6 = false;
               new GeneXus.Programs.wallet.doesaddressbelongtowallet(context ).execute(  AV12changeTo,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), ref  GXt_boolean6, out  GXt_boolean5) ;
               AV52changeBelongs = GXt_boolean5;
               if ( ! AV52changeBelongs )
               {
                  GXt_char4 = AV70discoverError;
                  new GeneXus.Programs.wallet.discovergroupaddresses(context ).execute( out  AV69addressess_to_look_for, out  AV71SDTAddressHistory, out  AV72totalBalance, out  GXt_char4) ;
                  AV70discoverError = GXt_char4;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV70discoverError)) )
                  {
                     GXt_boolean6 = AV52changeBelongs;
                     GXt_boolean5 = false;
                     new GeneXus.Programs.wallet.doesaddressbelongtowallet(context ).execute(  AV12changeTo,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), ref  GXt_boolean5, out  GXt_boolean6) ;
                     AV52changeBelongs = GXt_boolean6;
                  }
               }
            }
            if ( ! AV52changeBelongs )
            {
               AV19errorTitle = "This payment can not be signed: ";
               AV17error = "The 'change address' does not belong to this group or it has already been used";
            }
            else
            {
               if ( AV66spendInfo.gxTpr_Nextislast )
               {
                  if ( AV47chosenPercent < 0 )
                  {
                     AV47chosenPercent = 0;
                  }
               }
               else
               {
                  AV47chosenPercent = -1;
               }
               AV57jsonText = AV54groupProcessor.signlegacyspend(StringUtil.Trim( AV32oneMuSigSignatures.gxTpr_Psbt), AV64receivingXprv, AV53changeXprv, AV47chosenPercent, AV30networkType);
               AV67spendResult.FromJSonString(AV57jsonText, null);
               if ( ! AV67spendResult.gxTpr_Success )
               {
                  AV19errorTitle = "There was a problem signing the transaction: ";
                  AV17error = AV67spendResult.gxTpr_Error;
               }
               else
               {
                  /* Execute user subroutine: 'FINISH OR FORWARD' */
                  S131 ();
                  if (returnInSub) return;
               }
            }
         }
      }

      protected void S131( )
      {
         /* 'FINISH OR FORWARD' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67spendResult.gxTpr_Txhex)) )
         {
            /* Execute user subroutine: 'BROADCAST' */
            S151 ();
            if (returnInSub) return;
            if ( AV10broadcast )
            {
               AV23fee = AV67spendResult.gxTpr_Feebtc;
               GXt_char4 = AV18errorAfter;
               new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV25group_sdt,  AV8allCoins,  AV43transactionsToSend,  AV9amount,  AV16destination,  AV12changeTo,  AV23fee,  AV65signatureID,  true,  AV67spendResult.gxTpr_Bundle, out  GXt_char4) ;
               AV18errorAfter = GXt_char4;
            }
         }
         else
         {
            GXt_char4 = AV17error;
            new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV25group_sdt,  AV8allCoins,  AV43transactionsToSend,  AV9amount,  AV16destination,  AV12changeTo,  AV23fee,  AV65signatureID,  false,  AV67spendResult.gxTpr_Bundle, out  GXt_char4) ;
            AV17error = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
            {
               AV19errorTitle = "There was a problem forwarding the signature: ";
            }
         }
      }

      protected void S151( )
      {
         /* 'BROADCAST' Routine */
         returnInSub = false;
         GXt_char4 = AV17error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV67spendResult.gxTpr_Txhex, out  AV41TransactionId, out  GXt_char4) ;
         AV17error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
         {
            GXt_char4 = AV18errorAfter;
            new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV42transactionFileName,  AV41TransactionId,  AV43transactionsToSend, out  GXt_char4) ;
            AV18errorAfter = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV18errorAfter)) )
            {
               GXt_char4 = AV18errorAfter;
               new GeneXus.Programs.wallet.recordsentoutputs(context ).execute(  AV42transactionFileName,  AV41TransactionId,  AV67spendResult.gxTpr_Txhex, out  GXt_char4) ;
               AV18errorAfter = GXt_char4;
            }
            AV74GXV2 = 1;
            while ( AV74GXV2 <= AV43transactionsToSend.Count )
            {
               AV33oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV43transactionsToSend.Item(AV74GXV2));
               AV33oneTransaction.gxTpr_Senttransactionid = StringUtil.Trim( AV41TransactionId);
               AV74GXV2 = (int)(AV74GXV2+1);
            }
            AV10broadcast = true;
         }
         else
         {
            AV19errorTitle = "There was a problem submitting the transaction: ";
         }
      }

      protected void S121( )
      {
         /* 'BUILD UTXOS' Routine */
         returnInSub = false;
         AV68utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV75GXV3 = 1;
         while ( AV75GXV3 <= AV43transactionsToSend.Count )
         {
            AV33oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV43transactionsToSend.Item(AV75GXV3));
            AV60oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV60oneUtxo.gxTpr_Txid = StringUtil.Trim( AV33oneTransaction.gxTpr_Receivedtransactionid);
            AV60oneUtxo.gxTpr_Vout = AV33oneTransaction.gxTpr_Recivedn;
            AV60oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV33oneTransaction.gxTpr_Balance, 17, 8));
            AV60oneUtxo.gxTpr_Sequence = AV33oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV33oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV60oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV60oneUtxo.gxTpr_Ischange = false;
            }
            AV60oneUtxo.gxTpr_Address = StringUtil.Trim( AV33oneTransaction.gxTpr_Receivedaddress);
            AV68utxos.Add(AV60oneUtxo, 0);
            AV75GXV3 = (int)(AV75GXV3+1);
         }
      }

      protected void S161( )
      {
         /* 'DERIVE CHAIN KEYS' Routine */
         returnInSub = false;
         AV20extKeyCreateChain.gxTpr_Createextkeytype = 70;
         AV20extKeyCreateChain.gxTpr_Extendedprivatekey = AV50accountXprv;
         AV20extKeyCreateChain.gxTpr_Networktype = AV30networkType;
         AV20extKeyCreateChain.gxTpr_Keypath = "0";
         GXt_char4 = AV17error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV20extKeyCreateChain,  "", out  AV22extKeyInfoChain, out  GXt_char4) ;
         AV17error = GXt_char4;
         AV64receivingXprv = StringUtil.Trim( AV22extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
         {
            AV22extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
            AV20extKeyCreateChain.gxTpr_Keypath = "1";
            GXt_char4 = AV17error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV20extKeyCreateChain,  "", out  AV22extKeyInfoChain, out  GXt_char4) ;
            AV17error = GXt_char4;
            AV53changeXprv = StringUtil.Trim( AV22extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         }
         AV22extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV20extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
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
         AV19errorTitle = "";
         AV17error = "";
         AV44walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV30networkType = "";
         AV25group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV46websession = context.GetSession();
         AV32oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV42transactionFileName = "";
         AV55extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV50accountXprv = "";
         AV56groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV54groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         AV64receivingXprv = "";
         AV53changeXprv = "";
         AV16destination = "";
         AV65signatureID = Guid.Empty;
         AV63percentTokens = new GxSimpleCollection<string>();
         AV62percentToken = "";
         AV43transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV12changeTo = "";
         AV57jsonText = "";
         AV68utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV67spendResult = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult(context);
         AV66spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV70discoverError = "";
         AV69addressess_to_look_for = new SdtGxGetAddressess(context);
         AV71SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV18errorAfter = "";
         AV41TransactionId = "";
         AV33oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV60oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         AV20extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV22extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV48finalPercent ;
      private short AV47chosenPercent ;
      private short AV49maxPercent ;
      private int AV73GXV1 ;
      private int AV74GXV2 ;
      private int AV75GXV3 ;
      private decimal AV37sendCoins ;
      private decimal AV51baseFee ;
      private decimal AV9amount ;
      private decimal AV23fee ;
      private decimal AV58maxFee ;
      private decimal AV72totalBalance ;
      private string AV38sendTo ;
      private string AV19errorTitle ;
      private string AV17error ;
      private string AV30networkType ;
      private string AV42transactionFileName ;
      private string AV50accountXprv ;
      private string AV64receivingXprv ;
      private string AV53changeXprv ;
      private string AV16destination ;
      private string AV12changeTo ;
      private string AV70discoverError ;
      private string AV18errorAfter ;
      private string AV41TransactionId ;
      private string GXt_char4 ;
      private bool AV36sendAllCoins ;
      private bool AV10broadcast ;
      private bool returnInSub ;
      private bool AV59ok ;
      private bool AV8allCoins ;
      private bool AV52changeBelongs ;
      private bool GXt_boolean6 ;
      private bool GXt_boolean5 ;
      private string AV57jsonText ;
      private string AV15description ;
      private string AV61percentages ;
      private string AV62percentToken ;
      private Guid AV65signatureID ;
      private IGxSession AV46websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV44walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV25group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV32oneMuSigSignatures ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV55extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV56groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV54groupProcessor ;
      private GxSimpleCollection<string> AV63percentTokens ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV43transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV68utxos ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult AV67spendResult ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV66spendInfo ;
      private SdtGxGetAddressess AV69addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV71SDTAddressHistory ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV33oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV60oneUtxo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV20extKeyCreateChain ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV22extKeyInfoChain ;
      private bool aP7_broadcast ;
      private string aP8_errorTitle ;
      private string aP9_error ;
   }

}
