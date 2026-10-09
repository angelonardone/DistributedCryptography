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
                           decimal aP4_manaulFee ,
                           out bool aP5_broadcast ,
                           out string aP6_warning ,
                           out string aP7_errorTitle ,
                           out string aP8_error )
      {
         this.AV36sendAllCoins = aP0_sendAllCoins;
         this.AV37sendCoins = aP1_sendCoins;
         this.AV38sendTo = aP2_sendTo;
         this.AV15description = aP3_description;
         this.AV29manaulFee = aP4_manaulFee;
         this.AV10broadcast = false ;
         this.AV45warning = "" ;
         this.AV19errorTitle = "" ;
         this.AV17error = "" ;
         initialize();
         ExecuteImpl();
         aP5_broadcast=this.AV10broadcast;
         aP6_warning=this.AV45warning;
         aP7_errorTitle=this.AV19errorTitle;
         aP8_error=this.AV17error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                string aP3_description ,
                                decimal aP4_manaulFee ,
                                out bool aP5_broadcast ,
                                out string aP6_warning ,
                                out string aP7_errorTitle )
      {
         execute(aP0_sendAllCoins, aP1_sendCoins, aP2_sendTo, aP3_description, aP4_manaulFee, out aP5_broadcast, out aP6_warning, out aP7_errorTitle, out aP8_error);
         return AV17error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_sendCoins ,
                                 string aP2_sendTo ,
                                 string aP3_description ,
                                 decimal aP4_manaulFee ,
                                 out bool aP5_broadcast ,
                                 out string aP6_warning ,
                                 out string aP7_errorTitle ,
                                 out string aP8_error )
      {
         this.AV36sendAllCoins = aP0_sendAllCoins;
         this.AV37sendCoins = aP1_sendCoins;
         this.AV38sendTo = aP2_sendTo;
         this.AV15description = aP3_description;
         this.AV29manaulFee = aP4_manaulFee;
         this.AV10broadcast = false ;
         this.AV45warning = "" ;
         this.AV19errorTitle = "" ;
         this.AV17error = "" ;
         SubmitImpl();
         aP5_broadcast=this.AV10broadcast;
         aP6_warning=this.AV45warning;
         aP7_errorTitle=this.AV19errorTitle;
         aP8_error=this.AV17error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10broadcast = false;
         GXt_SdtWalletInfo1 = AV44walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV44walletInfo = GXt_SdtWalletInfo1;
         AV30networkType = AV44walletInfo.gxTpr_Networktype;
         AV25group_sdt.FromJSonString(AV46websession.Get("Group_EDIT_WALLET"), null);
         AV32oneMuSigSignatures.FromJSonString(AV46websession.Get("MuSign_ONE"), null);
         GXt_SdtExtKeyInfo2 = AV21extKeyInfoBIP48;
         new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV21extKeyInfoBIP48 = GXt_SdtExtKeyInfo2;
         AV28m48xprv = StringUtil.Trim( AV21extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV28m48xprv)) )
         {
            AV19errorTitle = "Signing key not available";
            AV17error = "Please re-enter your password";
            cleanup();
            if (true) return;
         }
         if ( (Guid.Empty==AV32oneMuSigSignatures.gxTpr_Id) )
         {
            AV8allCoins = AV36sendAllCoins;
            AV9amount = AV37sendCoins;
            AV16destination = AV38sendTo;
            AV23fee = AV29manaulFee;
            GXt_objcol_SdtSDTAddressHistory3 = AV43transactionsToSend;
            new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV15description,  AV9amount,  AV23fee, out  GXt_objcol_SdtSDTAddressHistory3) ;
            AV43transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
            GXt_char4 = AV12changeTo;
            new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
            AV12changeTo = GXt_char4;
            /* Execute user subroutine: 'BUILD LEGACY UTXOS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            GXt_SdtLegacyPsbtResult5 = AV34psbtResult;
            new GeneXus.Programs.wallet.registered.buildtransactionlegacy(context ).execute(  AV25group_sdt,  AV27legacyUtxos,  AV16destination,  AV9amount,  AV12changeTo,  AV23fee,  AV8allCoins,  AV39sequence,  AV26isChange,  AV30networkType, out  GXt_SdtLegacyPsbtResult5) ;
            AV34psbtResult = GXt_SdtLegacyPsbtResult5;
            if ( ! AV34psbtResult.gxTpr_Success )
            {
               AV19errorTitle = "There was a problem building the transaction: ";
               AV17error = AV34psbtResult.gxTpr_Error;
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            }
            else
            {
               GXt_SdtLegacyPsbtResult5 = AV40signResult;
               new GeneXus.Programs.wallet.registered.signlegacypsbt(context ).execute(  AV34psbtResult.gxTpr_Psbt,  AV39sequence,  AV11chainXprv,  AV30networkType, out  GXt_SdtLegacyPsbtResult5) ;
               AV40signResult = GXt_SdtLegacyPsbtResult5;
               if ( ! AV40signResult.gxTpr_Success )
               {
                  AV19errorTitle = "There was a problem signing the transaction: ";
                  AV17error = AV40signResult.gxTpr_Error;
                  new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               }
               else
               {
                  AV13combinedPsbt = AV40signResult.gxTpr_Psbt;
                  /* Execute user subroutine: 'TRY FINALIZE OR FORWARD' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
            }
         }
         else
         {
            AV8allCoins = AV32oneMuSigSignatures.gxTpr_Sendallcoins;
            AV9amount = AV32oneMuSigSignatures.gxTpr_Sendcoins;
            AV16destination = StringUtil.Trim( AV32oneMuSigSignatures.gxTpr_Sendto);
            AV23fee = AV32oneMuSigSignatures.gxTpr_Transactionfee;
            AV12changeTo = StringUtil.Trim( AV32oneMuSigSignatures.gxTpr_Changeto);
            AV43transactionsToSend = AV32oneMuSigSignatures.gxTpr_Transactions;
            /* Execute user subroutine: 'RESOLVE SEQUENCE FROM TRANSACTIONS' */
            S141 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            GXt_SdtLegacyPsbtResult5 = AV40signResult;
            new GeneXus.Programs.wallet.registered.signlegacypsbt(context ).execute(  AV32oneMuSigSignatures.gxTpr_Psbt,  AV39sequence,  AV11chainXprv,  AV30networkType, out  GXt_SdtLegacyPsbtResult5) ;
            AV40signResult = GXt_SdtLegacyPsbtResult5;
            if ( ! AV40signResult.gxTpr_Success )
            {
               AV19errorTitle = "There was a problem signing the transaction: ";
               AV17error = AV40signResult.gxTpr_Error;
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            }
            else
            {
               AV35psbtsToCombine = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               AV35psbtsToCombine.Add(StringUtil.Trim( AV32oneMuSigSignatures.gxTpr_Psbt), 0);
               AV35psbtsToCombine.Add(StringUtil.Trim( AV40signResult.gxTpr_Psbt), 0);
               GXt_SdtLegacyPsbtResult5 = AV14combineResult;
               new GeneXus.Programs.wallet.registered.combinelegacypsbts(context ).execute(  AV35psbtsToCombine,  AV30networkType, out  GXt_SdtLegacyPsbtResult5) ;
               AV14combineResult = GXt_SdtLegacyPsbtResult5;
               if ( ! AV14combineResult.gxTpr_Success )
               {
                  AV19errorTitle = "There was a problem combining the signatures: ";
                  AV17error = AV14combineResult.gxTpr_Error;
                  new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               }
               else
               {
                  AV13combinedPsbt = AV14combineResult.gxTpr_Psbt;
                  /* Execute user subroutine: 'TRY FINALIZE OR FORWARD' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
            }
         }
         AV28m48xprv = "";
         AV11chainXprv = "";
         cleanup();
      }

      protected void S111( )
      {
         /* 'TRY FINALIZE OR FORWARD' Routine */
         returnInSub = false;
         GXt_SdtLegacyFinalResult6 = AV24finalResult;
         new GeneXus.Programs.wallet.registered.finalizelegacypsbt(context ).execute(  AV13combinedPsbt,  AV30networkType, out  GXt_SdtLegacyFinalResult6) ;
         AV24finalResult = GXt_SdtLegacyFinalResult6;
         if ( AV24finalResult.gxTpr_Success )
         {
            GXt_char4 = AV17error;
            new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV24finalResult.gxTpr_Txhex, out  AV41TransactionId, out  GXt_char4) ;
            AV17error = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
            {
               AV42transactionFileName = StringUtil.Trim( AV25group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
               GXt_char4 = AV18errorAfter;
               new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV42transactionFileName,  AV41TransactionId,  AV43transactionsToSend, out  GXt_char4) ;
               AV18errorAfter = GXt_char4;
               GXt_char4 = AV18errorAfter;
               new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV25group_sdt,  AV8allCoins,  AV43transactionsToSend,  AV9amount,  AV16destination,  AV12changeTo,  AV23fee,  AV32oneMuSigSignatures.gxTpr_Id,  true,  AV13combinedPsbt, out  GXt_char4) ;
               AV18errorAfter = GXt_char4;
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               AV10broadcast = true;
            }
            else
            {
               AV19errorTitle = "There was a problem submitting the transaction: ";
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            }
         }
         else
         {
            AV45warning = AV24finalResult.gxTpr_Error;
            GXt_char4 = AV17error;
            new GeneXus.Programs.wallet.registered.updategroupandsendlegacypsbt(context ).execute(  AV25group_sdt,  AV8allCoins,  AV43transactionsToSend,  AV9amount,  AV16destination,  AV12changeTo,  AV23fee,  AV32oneMuSigSignatures.gxTpr_Id,  false,  AV13combinedPsbt, out  GXt_char4) ;
            AV17error = GXt_char4;
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17error)) )
            {
               AV19errorTitle = "There was a problem forwarding the signature: ";
            }
         }
      }

      protected void S121( )
      {
         /* 'BUILD LEGACY UTXOS' Routine */
         returnInSub = false;
         AV27legacyUtxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo>( context, "LegacyUtxo", "distributedcryptography");
         AV47GXV1 = 1;
         while ( AV47GXV1 <= AV43transactionsToSend.Count )
         {
            AV33oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV43transactionsToSend.Item(AV47GXV1));
            AV31oneLegacyUtxo = new GeneXus.Programs.wallet.registered.SdtLegacyUtxo(context);
            AV31oneLegacyUtxo.gxTpr_Txid = StringUtil.Trim( AV33oneTransaction.gxTpr_Receivedtransactionid);
            AV31oneLegacyUtxo.gxTpr_Vout = (int)(AV33oneTransaction.gxTpr_Recivedn);
            AV31oneLegacyUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV33oneTransaction.gxTpr_Balance, 17, 8));
            AV27legacyUtxos.Add(AV31oneLegacyUtxo, 0);
            AV39sequence = AV33oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV33oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV26isChange = true;
            }
            else
            {
               AV26isChange = false;
            }
            AV47GXV1 = (int)(AV47GXV1+1);
         }
         /* Execute user subroutine: 'DERIVE CHAIN XPRV' */
         S131 ();
         if (returnInSub) return;
      }

      protected void S131( )
      {
         /* 'DERIVE CHAIN XPRV' Routine */
         returnInSub = false;
         AV20extKeyCreateChain.gxTpr_Createextkeytype = 70;
         AV20extKeyCreateChain.gxTpr_Extendedprivatekey = AV28m48xprv;
         if ( AV26isChange )
         {
            AV20extKeyCreateChain.gxTpr_Keypath = "1";
         }
         else
         {
            AV20extKeyCreateChain.gxTpr_Keypath = "0";
         }
         AV20extKeyCreateChain.gxTpr_Networktype = AV30networkType;
         GXt_char4 = AV17error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV20extKeyCreateChain,  "", out  AV22extKeyInfoChain, out  GXt_char4) ;
         AV17error = GXt_char4;
         AV11chainXprv = StringUtil.Trim( AV22extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
      }

      protected void S141( )
      {
         /* 'RESOLVE SEQUENCE FROM TRANSACTIONS' Routine */
         returnInSub = false;
         AV48GXV2 = 1;
         while ( AV48GXV2 <= AV43transactionsToSend.Count )
         {
            AV33oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV43transactionsToSend.Item(AV48GXV2));
            AV39sequence = AV33oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV33oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV26isChange = true;
            }
            else
            {
               AV26isChange = false;
            }
            AV48GXV2 = (int)(AV48GXV2+1);
         }
         /* Execute user subroutine: 'DERIVE CHAIN XPRV' */
         S131 ();
         if (returnInSub) return;
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
         AV45warning = "";
         AV19errorTitle = "";
         AV17error = "";
         AV44walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV30networkType = "";
         AV25group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV46websession = context.GetSession();
         AV32oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV21extKeyInfoBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV28m48xprv = "";
         AV16destination = "";
         AV43transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV12changeTo = "";
         AV34psbtResult = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV27legacyUtxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo>( context, "LegacyUtxo", "distributedcryptography");
         AV40signResult = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV11chainXprv = "";
         AV13combinedPsbt = "";
         AV35psbtsToCombine = new GxSimpleCollection<string>();
         AV14combineResult = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         GXt_SdtLegacyPsbtResult5 = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV24finalResult = new GeneXus.Programs.wallet.registered.SdtLegacyFinalResult(context);
         GXt_SdtLegacyFinalResult6 = new GeneXus.Programs.wallet.registered.SdtLegacyFinalResult(context);
         AV41TransactionId = "";
         AV42transactionFileName = "";
         AV18errorAfter = "";
         AV33oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV31oneLegacyUtxo = new GeneXus.Programs.wallet.registered.SdtLegacyUtxo(context);
         AV20extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         GXt_char4 = "";
         AV22extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private int AV47GXV1 ;
      private int AV48GXV2 ;
      private long AV39sequence ;
      private decimal AV37sendCoins ;
      private decimal AV29manaulFee ;
      private decimal AV9amount ;
      private decimal AV23fee ;
      private string AV38sendTo ;
      private string AV45warning ;
      private string AV19errorTitle ;
      private string AV17error ;
      private string AV30networkType ;
      private string AV28m48xprv ;
      private string AV16destination ;
      private string AV12changeTo ;
      private string AV11chainXprv ;
      private string AV41TransactionId ;
      private string AV42transactionFileName ;
      private string AV18errorAfter ;
      private string GXt_char4 ;
      private bool AV36sendAllCoins ;
      private bool AV10broadcast ;
      private bool AV8allCoins ;
      private bool returnInSub ;
      private bool AV26isChange ;
      private string AV13combinedPsbt ;
      private string AV15description ;
      private IGxSession AV46websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV44walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV25group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV32oneMuSigSignatures ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV21extKeyInfoBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV43transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV34psbtResult ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo> AV27legacyUtxos ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV40signResult ;
      private GxSimpleCollection<string> AV35psbtsToCombine ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV14combineResult ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult GXt_SdtLegacyPsbtResult5 ;
      private GeneXus.Programs.wallet.registered.SdtLegacyFinalResult AV24finalResult ;
      private GeneXus.Programs.wallet.registered.SdtLegacyFinalResult GXt_SdtLegacyFinalResult6 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV33oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtLegacyUtxo AV31oneLegacyUtxo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV20extKeyCreateChain ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV22extKeyInfoChain ;
      private bool aP5_broadcast ;
      private string aP6_warning ;
      private string aP7_errorTitle ;
      private string aP8_error ;
   }

}
