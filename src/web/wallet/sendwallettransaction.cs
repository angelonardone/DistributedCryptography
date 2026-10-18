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
   public class sendwallettransaction : GXProcedure
   {
      public sendwallettransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendwallettransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           decimal aP1_manaulFee ,
                           string aP2_networkType ,
                           decimal aP3_sendCoins ,
                           string aP4_sendTo ,
                           string aP5_changeTo ,
                           string aP6_description ,
                           out string aP7_errorTitle ,
                           out string aP8_error )
      {
         this.AV15sendAllCoins = aP0_sendAllCoins;
         this.AV13manaulFee = aP1_manaulFee;
         this.AV14networkType = aP2_networkType;
         this.AV16sendCoins = aP3_sendCoins;
         this.AV17sendTo = aP4_sendTo;
         this.AV8changeTo = aP5_changeTo;
         this.AV9description = aP6_description;
         this.AV11errorTitle = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP7_errorTitle=this.AV11errorTitle;
         aP8_error=this.AV10error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                decimal aP1_manaulFee ,
                                string aP2_networkType ,
                                decimal aP3_sendCoins ,
                                string aP4_sendTo ,
                                string aP5_changeTo ,
                                string aP6_description ,
                                out string aP7_errorTitle )
      {
         execute(aP0_sendAllCoins, aP1_manaulFee, aP2_networkType, aP3_sendCoins, aP4_sendTo, aP5_changeTo, aP6_description, out aP7_errorTitle, out aP8_error);
         return AV10error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 decimal aP1_manaulFee ,
                                 string aP2_networkType ,
                                 decimal aP3_sendCoins ,
                                 string aP4_sendTo ,
                                 string aP5_changeTo ,
                                 string aP6_description ,
                                 out string aP7_errorTitle ,
                                 out string aP8_error )
      {
         this.AV15sendAllCoins = aP0_sendAllCoins;
         this.AV13manaulFee = aP1_manaulFee;
         this.AV14networkType = aP2_networkType;
         this.AV16sendCoins = aP3_sendCoins;
         this.AV17sendTo = aP4_sendTo;
         this.AV8changeTo = aP5_changeTo;
         this.AV9description = aP6_description;
         this.AV11errorTitle = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP7_errorTitle=this.AV11errorTitle;
         aP8_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDTAddressHistory1 = AV19transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV9description,  AV16sendCoins,  AV13manaulFee, out  GXt_objcol_SdtSDTAddressHistory1) ;
         AV19transactionsToSend = GXt_objcol_SdtSDTAddressHistory1;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.getrawtransfromcoinstosend(context ).execute( ref  AV19transactionsToSend, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            AV11errorTitle = "There was a problem getting the Raw Transaction from the server: ";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.buildtransaction(context ).execute(  AV15sendAllCoins,  AV13manaulFee,  AV14networkType,  AV19transactionsToSend,  AV16sendCoins,  AV17sendTo,  AV8changeTo, out  AV20virtualSize, out  AV12hexTransaction, out  GXt_char2) ;
         AV10error = GXt_char2;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV11errorTitle = "There was a problem building the final transaction: ";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV12hexTransaction, out  AV18TransactionId, out  GXt_char2) ;
         AV10error = GXt_char2;
         AV12hexTransaction = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV11errorTitle = "There was a problem submitting the transaction: ";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  "transactions.trn",  AV18TransactionId,  AV19transactionsToSend, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV11errorTitle = "The transaction was sent, but the wallet could not record it: ";
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
         AV11errorTitle = "";
         AV10error = "";
         AV19transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV12hexTransaction = "";
         AV18TransactionId = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private long AV20virtualSize ;
      private decimal AV13manaulFee ;
      private decimal AV16sendCoins ;
      private string AV14networkType ;
      private string AV17sendTo ;
      private string AV8changeTo ;
      private string AV11errorTitle ;
      private string AV10error ;
      private string AV18TransactionId ;
      private string GXt_char2 ;
      private bool AV15sendAllCoins ;
      private string AV12hexTransaction ;
      private string AV9description ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV19transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private string aP7_errorTitle ;
      private string aP8_error ;
   }

}
