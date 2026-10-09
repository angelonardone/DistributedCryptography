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
   public class estimatebountytransaction : GXProcedure
   {
      public estimatebountytransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public estimatebountytransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( decimal aP0_sendCoins ,
                           string aP1_sendTo ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                           out long aP3_virtualSize ,
                           out string aP4_error )
      {
         this.AV11sendCoins = aP0_sendCoins;
         this.AV12sendTo = aP1_sendTo;
         this.AV14transactionsToSend = aP2_transactionsToSend;
         this.AV16virtualSize = 0 ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_transactionsToSend=this.AV14transactionsToSend;
         aP3_virtualSize=this.AV16virtualSize;
         aP4_error=this.AV8error;
      }

      public string executeUdp( decimal aP0_sendCoins ,
                                string aP1_sendTo ,
                                ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                out long aP3_virtualSize )
      {
         execute(aP0_sendCoins, aP1_sendTo, ref aP2_transactionsToSend, out aP3_virtualSize, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( decimal aP0_sendCoins ,
                                 string aP1_sendTo ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                 out long aP3_virtualSize ,
                                 out string aP4_error )
      {
         this.AV11sendCoins = aP0_sendCoins;
         this.AV12sendTo = aP1_sendTo;
         this.AV14transactionsToSend = aP2_transactionsToSend;
         this.AV16virtualSize = 0 ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_transactionsToSend=this.AV14transactionsToSend;
         aP3_virtualSize=this.AV16virtualSize;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9group_sdt.FromJSonString(AV18websession.Get("Group_EDIT_WALLET"), null);
         GXt_SdtWalletInfo1 = AV17walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV17walletInfo = GXt_SdtWalletInfo1;
         AV13transactionFee = 0;
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.registered.buildtransactiontimebackup(context ).execute(  AV9group_sdt,  AV13transactionFee,  AV17walletInfo.gxTpr_Networktype,  AV11sendCoins,  AV12sendTo, ref  AV14transactionsToSend, out  AV16virtualSize, out  AV10hexTransaction, out  AV15verified, out  GXt_char2) ;
         AV8error = GXt_char2;
         AV10hexTransaction = "";
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
         AV8error = "";
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18websession = context.GetSession();
         AV17walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_char2 = "";
         AV10hexTransaction = "";
         /* GeneXus formulas. */
      }

      private long AV16virtualSize ;
      private decimal AV11sendCoins ;
      private decimal AV13transactionFee ;
      private string AV12sendTo ;
      private string AV8error ;
      private string GXt_char2 ;
      private bool AV15verified ;
      private string AV10hexTransaction ;
      private IGxSession AV18websession ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV14transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV17walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private long aP3_virtualSize ;
      private string aP4_error ;
   }

}
