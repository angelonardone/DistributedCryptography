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
   public class getlegacyspendcontext : GXProcedure
   {
      public getlegacyspendcontext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlegacyspendcontext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_networkType ,
                           out short aP1_numKeys ,
                           out short aP2_minSig ,
                           out Guid aP3_pendingSpendId ,
                           out bool aP4_sendAllCoins ,
                           out decimal aP5_sendCoins ,
                           out string aP6_sendTo ,
                           out string aP7_description ,
                           out decimal aP8_transactionFee )
      {
         this.AV12networkType = "" ;
         this.AV9numKeys = 0 ;
         this.AV8minSig = 0 ;
         this.AV14pendingSpendId = Guid.Empty ;
         this.AV15sendAllCoins = false ;
         this.AV16sendCoins = 0 ;
         this.AV17sendTo = "" ;
         this.AV10description = "" ;
         this.AV18transactionFee = 0 ;
         initialize();
         ExecuteImpl();
         aP0_networkType=this.AV12networkType;
         aP1_numKeys=this.AV9numKeys;
         aP2_minSig=this.AV8minSig;
         aP3_pendingSpendId=this.AV14pendingSpendId;
         aP4_sendAllCoins=this.AV15sendAllCoins;
         aP5_sendCoins=this.AV16sendCoins;
         aP6_sendTo=this.AV17sendTo;
         aP7_description=this.AV10description;
         aP8_transactionFee=this.AV18transactionFee;
      }

      public decimal executeUdp( out string aP0_networkType ,
                                 out short aP1_numKeys ,
                                 out short aP2_minSig ,
                                 out Guid aP3_pendingSpendId ,
                                 out bool aP4_sendAllCoins ,
                                 out decimal aP5_sendCoins ,
                                 out string aP6_sendTo ,
                                 out string aP7_description )
      {
         execute(out aP0_networkType, out aP1_numKeys, out aP2_minSig, out aP3_pendingSpendId, out aP4_sendAllCoins, out aP5_sendCoins, out aP6_sendTo, out aP7_description, out aP8_transactionFee);
         return AV18transactionFee ;
      }

      public void executeSubmit( out string aP0_networkType ,
                                 out short aP1_numKeys ,
                                 out short aP2_minSig ,
                                 out Guid aP3_pendingSpendId ,
                                 out bool aP4_sendAllCoins ,
                                 out decimal aP5_sendCoins ,
                                 out string aP6_sendTo ,
                                 out string aP7_description ,
                                 out decimal aP8_transactionFee )
      {
         this.AV12networkType = "" ;
         this.AV9numKeys = 0 ;
         this.AV8minSig = 0 ;
         this.AV14pendingSpendId = Guid.Empty ;
         this.AV15sendAllCoins = false ;
         this.AV16sendCoins = 0 ;
         this.AV17sendTo = "" ;
         this.AV10description = "" ;
         this.AV18transactionFee = 0 ;
         SubmitImpl();
         aP0_networkType=this.AV12networkType;
         aP1_numKeys=this.AV9numKeys;
         aP2_minSig=this.AV8minSig;
         aP3_pendingSpendId=this.AV14pendingSpendId;
         aP4_sendAllCoins=this.AV15sendAllCoins;
         aP5_sendCoins=this.AV16sendCoins;
         aP6_sendTo=this.AV17sendTo;
         aP7_description=this.AV10description;
         aP8_transactionFee=this.AV18transactionFee;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWalletInfo1 = AV19walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV19walletInfo = GXt_SdtWalletInfo1;
         AV12networkType = AV19walletInfo.gxTpr_Networktype;
         AV11group_sdt.FromJSonString(AV20websession.Get("Group_EDIT_WALLET"), null);
         AV9numKeys = (short)(AV11group_sdt.gxTpr_Contact.Count);
         AV8minSig = AV11group_sdt.gxTpr_Minimumshares;
         AV13oneMuSigSignatures.FromJSonString(AV20websession.Get("MuSign_ONE"), null);
         AV14pendingSpendId = AV13oneMuSigSignatures.gxTpr_Id;
         if ( ! (Guid.Empty==AV13oneMuSigSignatures.gxTpr_Id) )
         {
            AV15sendAllCoins = AV13oneMuSigSignatures.gxTpr_Sendallcoins;
            AV18transactionFee = AV13oneMuSigSignatures.gxTpr_Transactionfee;
            AV16sendCoins = AV13oneMuSigSignatures.gxTpr_Sendcoins;
            AV17sendTo = StringUtil.Trim( AV13oneMuSigSignatures.gxTpr_Sendto);
            AV10description = StringUtil.Trim( AV13oneMuSigSignatures.gxTpr_Description);
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
         AV12networkType = "";
         AV14pendingSpendId = Guid.Empty;
         AV17sendTo = "";
         AV10description = "";
         AV19walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20websession = context.GetSession();
         AV13oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         /* GeneXus formulas. */
      }

      private short AV9numKeys ;
      private short AV8minSig ;
      private decimal AV16sendCoins ;
      private decimal AV18transactionFee ;
      private string AV12networkType ;
      private string AV17sendTo ;
      private bool AV15sendAllCoins ;
      private string AV10description ;
      private Guid AV14pendingSpendId ;
      private IGxSession AV20websession ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV19walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV13oneMuSigSignatures ;
      private string aP0_networkType ;
      private short aP1_numKeys ;
      private short aP2_minSig ;
      private Guid aP3_pendingSpendId ;
      private bool aP4_sendAllCoins ;
      private decimal aP5_sendCoins ;
      private string aP6_sendTo ;
      private string aP7_description ;
      private decimal aP8_transactionFee ;
   }

}
