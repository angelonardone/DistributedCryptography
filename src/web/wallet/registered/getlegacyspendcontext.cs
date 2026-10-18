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
                           out bool aP1_amIgroupOwner ,
                           out short aP2_minSig ,
                           out short aP3_numMembers ,
                           out Guid aP4_pendingSpendId ,
                           out bool aP5_isLastSigner ,
                           out bool aP6_sendAllCoins ,
                           out decimal aP7_sendCoins ,
                           out string aP8_sendTo ,
                           out string aP9_description ,
                           out decimal aP10_baseFee ,
                           out GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo aP11_spendInfo ,
                           out string aP12_error )
      {
         this.AV12networkType = "" ;
         this.AV22amIgroupOwner = false ;
         this.AV8minSig = 0 ;
         this.AV21numMembers = 0 ;
         this.AV14pendingSpendId = Guid.Empty ;
         this.AV27isLastSigner = false ;
         this.AV15sendAllCoins = false ;
         this.AV16sendCoins = 0 ;
         this.AV17sendTo = "" ;
         this.AV10description = "" ;
         this.AV23baseFee = 0 ;
         this.AV32spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context) ;
         this.AV25error = "" ;
         initialize();
         ExecuteImpl();
         aP0_networkType=this.AV12networkType;
         aP1_amIgroupOwner=this.AV22amIgroupOwner;
         aP2_minSig=this.AV8minSig;
         aP3_numMembers=this.AV21numMembers;
         aP4_pendingSpendId=this.AV14pendingSpendId;
         aP5_isLastSigner=this.AV27isLastSigner;
         aP6_sendAllCoins=this.AV15sendAllCoins;
         aP7_sendCoins=this.AV16sendCoins;
         aP8_sendTo=this.AV17sendTo;
         aP9_description=this.AV10description;
         aP10_baseFee=this.AV23baseFee;
         aP11_spendInfo=this.AV32spendInfo;
         aP12_error=this.AV25error;
      }

      public string executeUdp( out string aP0_networkType ,
                                out bool aP1_amIgroupOwner ,
                                out short aP2_minSig ,
                                out short aP3_numMembers ,
                                out Guid aP4_pendingSpendId ,
                                out bool aP5_isLastSigner ,
                                out bool aP6_sendAllCoins ,
                                out decimal aP7_sendCoins ,
                                out string aP8_sendTo ,
                                out string aP9_description ,
                                out decimal aP10_baseFee ,
                                out GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo aP11_spendInfo )
      {
         execute(out aP0_networkType, out aP1_amIgroupOwner, out aP2_minSig, out aP3_numMembers, out aP4_pendingSpendId, out aP5_isLastSigner, out aP6_sendAllCoins, out aP7_sendCoins, out aP8_sendTo, out aP9_description, out aP10_baseFee, out aP11_spendInfo, out aP12_error);
         return AV25error ;
      }

      public void executeSubmit( out string aP0_networkType ,
                                 out bool aP1_amIgroupOwner ,
                                 out short aP2_minSig ,
                                 out short aP3_numMembers ,
                                 out Guid aP4_pendingSpendId ,
                                 out bool aP5_isLastSigner ,
                                 out bool aP6_sendAllCoins ,
                                 out decimal aP7_sendCoins ,
                                 out string aP8_sendTo ,
                                 out string aP9_description ,
                                 out decimal aP10_baseFee ,
                                 out GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo aP11_spendInfo ,
                                 out string aP12_error )
      {
         this.AV12networkType = "" ;
         this.AV22amIgroupOwner = false ;
         this.AV8minSig = 0 ;
         this.AV21numMembers = 0 ;
         this.AV14pendingSpendId = Guid.Empty ;
         this.AV27isLastSigner = false ;
         this.AV15sendAllCoins = false ;
         this.AV16sendCoins = 0 ;
         this.AV17sendTo = "" ;
         this.AV10description = "" ;
         this.AV23baseFee = 0 ;
         this.AV32spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context) ;
         this.AV25error = "" ;
         SubmitImpl();
         aP0_networkType=this.AV12networkType;
         aP1_amIgroupOwner=this.AV22amIgroupOwner;
         aP2_minSig=this.AV8minSig;
         aP3_numMembers=this.AV21numMembers;
         aP4_pendingSpendId=this.AV14pendingSpendId;
         aP5_isLastSigner=this.AV27isLastSigner;
         aP6_sendAllCoins=this.AV15sendAllCoins;
         aP7_sendCoins=this.AV16sendCoins;
         aP8_sendTo=this.AV17sendTo;
         aP9_description=this.AV10description;
         aP10_baseFee=this.AV23baseFee;
         aP11_spendInfo=this.AV32spendInfo;
         aP12_error=this.AV25error;
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
         AV22amIgroupOwner = false;
         AV8minSig = AV11group_sdt.gxTpr_Minimumshares;
         AV21numMembers = 1;
         AV33GXV1 = 1;
         while ( AV33GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV30oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV33GXV1));
            if ( ! ( ( AV30oneContact.gxTpr_Contactid == AV30oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV21numMembers = (short)(AV21numMembers+1);
            }
            AV33GXV1 = (int)(AV33GXV1+1);
         }
         AV27isLastSigner = false;
         AV32spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV13oneMuSigSignatures.FromJSonString(AV20websession.Get("MuSign_ONE"), null);
         AV14pendingSpendId = AV13oneMuSigSignatures.gxTpr_Id;
         if ( (Guid.Empty==AV13oneMuSigSignatures.gxTpr_Id) )
         {
            if ( AV8minSig == 1 )
            {
               AV27isLastSigner = true;
            }
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptolegacyeo(context ).execute(  AV11group_sdt, out  AV26groupEO) ;
         AV29ok = AV24groupProcessor.fromsdt(AV26groupEO);
         if ( ! AV29ok )
         {
            AV25error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV28jsonText = AV24groupProcessor.describelegacyspend(StringUtil.Trim( AV13oneMuSigSignatures.gxTpr_Psbt), StringUtil.Trim( AV11group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV11group_sdt.gxTpr_Extpubkeymultisigchange), AV12networkType);
         AV32spendInfo.FromJSonString(AV28jsonText, null);
         if ( ! AV32spendInfo.gxTpr_Success )
         {
            AV25error = AV32spendInfo.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         AV15sendAllCoins = AV13oneMuSigSignatures.gxTpr_Sendallcoins;
         AV10description = StringUtil.Trim( AV13oneMuSigSignatures.gxTpr_Description);
         AV16sendCoins = AV32spendInfo.gxTpr_Amountbtc;
         AV17sendTo = StringUtil.Trim( AV32spendInfo.gxTpr_Sendto);
         AV27isLastSigner = AV32spendInfo.gxTpr_Nextislast;
         AV34GXV2 = 1;
         while ( AV34GXV2 <= AV32spendInfo.gxTpr_Levels.Count )
         {
            AV31oneLevel = ((GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem)AV32spendInfo.gxTpr_Levels.Item(AV34GXV2));
            if ( AV31oneLevel.gxTpr_Percent == 0 )
            {
               AV23baseFee = AV31oneLevel.gxTpr_Feebtc;
            }
            AV34GXV2 = (int)(AV34GXV2+1);
         }
         if ( AV32spendInfo.gxTpr_Isigned )
         {
            AV25error = "You have already signed this payment";
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
         AV32spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV25error = "";
         AV19walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20websession = context.GetSession();
         AV30oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV13oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV26groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV24groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         AV28jsonText = "";
         AV31oneLevel = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem(context);
         /* GeneXus formulas. */
      }

      private short AV8minSig ;
      private short AV21numMembers ;
      private int AV33GXV1 ;
      private int AV34GXV2 ;
      private decimal AV16sendCoins ;
      private decimal AV23baseFee ;
      private string AV12networkType ;
      private string AV17sendTo ;
      private string AV25error ;
      private bool AV22amIgroupOwner ;
      private bool AV27isLastSigner ;
      private bool AV15sendAllCoins ;
      private bool AV29ok ;
      private string AV28jsonText ;
      private string AV10description ;
      private Guid AV14pendingSpendId ;
      private IGxSession AV20websession ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV32spendInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV19walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV30oneContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV13oneMuSigSignatures ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV26groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV24groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem AV31oneLevel ;
      private string aP0_networkType ;
      private bool aP1_amIgroupOwner ;
      private short aP2_minSig ;
      private short aP3_numMembers ;
      private Guid aP4_pendingSpendId ;
      private bool aP5_isLastSigner ;
      private bool aP6_sendAllCoins ;
      private decimal aP7_sendCoins ;
      private string aP8_sendTo ;
      private string aP9_description ;
      private decimal aP10_baseFee ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo aP11_spendInfo ;
      private string aP12_error ;
   }

}
