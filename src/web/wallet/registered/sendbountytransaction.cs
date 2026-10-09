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
   public class sendbountytransaction : GXProcedure
   {
      public sendbountytransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendbountytransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( decimal aP0_manaulFee ,
                           decimal aP1_sendCoins ,
                           string aP2_sendTo ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                           out string aP4_errorTitle ,
                           out string aP5_error )
      {
         this.AV12manaulFee = aP0_manaulFee;
         this.AV13sendCoins = aP1_sendCoins;
         this.AV14sendTo = aP2_sendTo;
         this.AV17transactionsToSend = aP3_transactionsToSend;
         this.AV9errorTitle = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP4_errorTitle=this.AV9errorTitle;
         aP5_error=this.AV8error;
      }

      public string executeUdp( decimal aP0_manaulFee ,
                                decimal aP1_sendCoins ,
                                string aP2_sendTo ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                                out string aP4_errorTitle )
      {
         execute(aP0_manaulFee, aP1_sendCoins, aP2_sendTo, aP3_transactionsToSend, out aP4_errorTitle, out aP5_error);
         return AV8error ;
      }

      public void executeSubmit( decimal aP0_manaulFee ,
                                 decimal aP1_sendCoins ,
                                 string aP2_sendTo ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ,
                                 out string aP4_errorTitle ,
                                 out string aP5_error )
      {
         this.AV12manaulFee = aP0_manaulFee;
         this.AV13sendCoins = aP1_sendCoins;
         this.AV14sendTo = aP2_sendTo;
         this.AV17transactionsToSend = aP3_transactionsToSend;
         this.AV9errorTitle = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP4_errorTitle=this.AV9errorTitle;
         aP5_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10group_sdt.FromJSonString(AV21websession.Get("Group_EDIT_WALLET"), null);
         GXt_SdtWalletInfo1 = AV20walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV20walletInfo = GXt_SdtWalletInfo1;
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.registered.buildtransactiontimebackup(context ).execute(  AV10group_sdt,  AV12manaulFee,  AV20walletInfo.gxTpr_Networktype,  AV13sendCoins,  AV14sendTo, ref  AV17transactionsToSend, out  AV19virtualSize, out  AV11hexTransaction, out  AV18verified, out  GXt_char2) ;
         AV8error = GXt_char2;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV9errorTitle = "There was a problem building the final transaction: ";
            cleanup();
            if (true) return;
         }
         if ( ! AV18verified )
         {
            AV9errorTitle = "There was a problem verifying the transaction: ";
            AV8error = "Transaction is not Verified";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV11hexTransaction, out  AV15TransactionId, out  GXt_char2) ;
         AV8error = GXt_char2;
         AV11hexTransaction = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV9errorTitle = "There was a problem submiting the transaction: ";
            cleanup();
            if (true) return;
         }
         AV16transactionFileName = StringUtil.Trim( AV10group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV16transactionFileName,  AV15TransactionId,  AV17transactionsToSend, out  GXt_char2) ;
         AV8error = GXt_char2;
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
         AV9errorTitle = "";
         AV8error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21websession = context.GetSession();
         AV20walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV11hexTransaction = "";
         AV15TransactionId = "";
         AV16transactionFileName = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private long AV19virtualSize ;
      private decimal AV12manaulFee ;
      private decimal AV13sendCoins ;
      private string AV14sendTo ;
      private string AV9errorTitle ;
      private string AV8error ;
      private string AV15TransactionId ;
      private string AV16transactionFileName ;
      private string GXt_char2 ;
      private bool AV18verified ;
      private string AV11hexTransaction ;
      private IGxSession AV21websession ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV17transactionsToSend ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV20walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private string aP4_errorTitle ;
      private string aP5_error ;
   }

}
