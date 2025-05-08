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
namespace GeneXus.Programs.wallet.registered {
   public class buildtransactionmusig2 : GXProcedure
   {
      public buildtransactionmusig2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildtransactionmusig2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_isFinalCall ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           bool aP2_sendAllCoins ,
                           decimal aP3_transactionFee ,
                           string aP4_networkType ,
                           decimal aP5_inSendCoins ,
                           string aP6_sendTo ,
                           string aP7_returnTo ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                           out long aP9_virtualSize ,
                           out string aP10_hexTransaction ,
                           out bool aP11_verified ,
                           out string aP12_error )
      {
         this.AV22isFinalCall = aP0_isFinalCall;
         this.AV17group_sdt = aP1_group_sdt;
         this.AV49sendAllCoins = aP2_sendAllCoins;
         this.AV60transactionFee = aP3_transactionFee;
         this.AV24networkType = aP4_networkType;
         this.AV20inSendCoins = aP5_inSendCoins;
         this.AV51sendTo = aP6_sendTo;
         this.AV46returnTo = aP7_returnTo;
         this.AV61transactionsToSend = aP8_transactionsToSend;
         this.AV63virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV62verified = false ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP8_transactionsToSend=this.AV61transactionsToSend;
         aP9_virtualSize=this.AV63virtualSize;
         aP10_hexTransaction=this.AV18hexTransaction;
         aP11_verified=this.AV62verified;
         aP12_error=this.AV11error;
      }

      public string executeUdp( bool aP0_isFinalCall ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                bool aP2_sendAllCoins ,
                                decimal aP3_transactionFee ,
                                string aP4_networkType ,
                                decimal aP5_inSendCoins ,
                                string aP6_sendTo ,
                                string aP7_returnTo ,
                                ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                                out long aP9_virtualSize ,
                                out string aP10_hexTransaction ,
                                out bool aP11_verified )
      {
         execute(aP0_isFinalCall, aP1_group_sdt, aP2_sendAllCoins, aP3_transactionFee, aP4_networkType, aP5_inSendCoins, aP6_sendTo, aP7_returnTo, ref aP8_transactionsToSend, out aP9_virtualSize, out aP10_hexTransaction, out aP11_verified, out aP12_error);
         return AV11error ;
      }

      public void executeSubmit( bool aP0_isFinalCall ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 bool aP2_sendAllCoins ,
                                 decimal aP3_transactionFee ,
                                 string aP4_networkType ,
                                 decimal aP5_inSendCoins ,
                                 string aP6_sendTo ,
                                 string aP7_returnTo ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                                 out long aP9_virtualSize ,
                                 out string aP10_hexTransaction ,
                                 out bool aP11_verified ,
                                 out string aP12_error )
      {
         this.AV22isFinalCall = aP0_isFinalCall;
         this.AV17group_sdt = aP1_group_sdt;
         this.AV49sendAllCoins = aP2_sendAllCoins;
         this.AV60transactionFee = aP3_transactionFee;
         this.AV24networkType = aP4_networkType;
         this.AV20inSendCoins = aP5_inSendCoins;
         this.AV51sendTo = aP6_sendTo;
         this.AV46returnTo = aP7_returnTo;
         this.AV61transactionsToSend = aP8_transactionsToSend;
         this.AV63virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV62verified = false ;
         this.AV11error = "" ;
         SubmitImpl();
         aP8_transactionsToSend=this.AV61transactionsToSend;
         aP9_virtualSize=this.AV63virtualSize;
         aP10_hexTransaction=this.AV18hexTransaction;
         aP11_verified=this.AV62verified;
         aP12_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
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
         AV18hexTransaction = "";
         AV11error = "";
         /* GeneXus formulas. */
      }

      private long AV63virtualSize ;
      private decimal AV60transactionFee ;
      private decimal AV20inSendCoins ;
      private string AV24networkType ;
      private string AV51sendTo ;
      private string AV46returnTo ;
      private string AV11error ;
      private bool AV22isFinalCall ;
      private bool AV49sendAllCoins ;
      private bool AV62verified ;
      private string AV18hexTransaction ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV61transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ;
      private long aP9_virtualSize ;
      private string aP10_hexTransaction ;
      private bool aP11_verified ;
      private string aP12_error ;
   }

}
