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
   public class getdelegatedspendcontext : GXProcedure
   {
      public getdelegatedspendcontext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getdelegatedspendcontext( IGxContext context )
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
         this.AV18networkType = "" ;
         this.AV8amIgroupOwner = false ;
         this.AV17minSig = 0 ;
         this.AV19numMembers = 0 ;
         this.AV24pendingSpendId = Guid.Empty ;
         this.AV15isLastSigner = false ;
         this.AV25sendAllCoins = false ;
         this.AV26sendCoins = 0 ;
         this.AV27sendTo = "" ;
         this.AV11description = "" ;
         this.AV9baseFee = 0 ;
         this.AV28spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context) ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP0_networkType=this.AV18networkType;
         aP1_amIgroupOwner=this.AV8amIgroupOwner;
         aP2_minSig=this.AV17minSig;
         aP3_numMembers=this.AV19numMembers;
         aP4_pendingSpendId=this.AV24pendingSpendId;
         aP5_isLastSigner=this.AV15isLastSigner;
         aP6_sendAllCoins=this.AV25sendAllCoins;
         aP7_sendCoins=this.AV26sendCoins;
         aP8_sendTo=this.AV27sendTo;
         aP9_description=this.AV11description;
         aP10_baseFee=this.AV9baseFee;
         aP11_spendInfo=this.AV28spendInfo;
         aP12_error=this.AV12error;
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
         return AV12error ;
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
         this.AV18networkType = "" ;
         this.AV8amIgroupOwner = false ;
         this.AV17minSig = 0 ;
         this.AV19numMembers = 0 ;
         this.AV24pendingSpendId = Guid.Empty ;
         this.AV15isLastSigner = false ;
         this.AV25sendAllCoins = false ;
         this.AV26sendCoins = 0 ;
         this.AV27sendTo = "" ;
         this.AV11description = "" ;
         this.AV9baseFee = 0 ;
         this.AV28spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context) ;
         this.AV12error = "" ;
         SubmitImpl();
         aP0_networkType=this.AV18networkType;
         aP1_amIgroupOwner=this.AV8amIgroupOwner;
         aP2_minSig=this.AV17minSig;
         aP3_numMembers=this.AV19numMembers;
         aP4_pendingSpendId=this.AV24pendingSpendId;
         aP5_isLastSigner=this.AV15isLastSigner;
         aP6_sendAllCoins=this.AV25sendAllCoins;
         aP7_sendCoins=this.AV26sendCoins;
         aP8_sendTo=this.AV27sendTo;
         aP9_description=this.AV11description;
         aP10_baseFee=this.AV9baseFee;
         aP11_spendInfo=this.AV28spendInfo;
         aP12_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWalletInfo1 = AV29walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV29walletInfo = GXt_SdtWalletInfo1;
         AV18networkType = AV29walletInfo.gxTpr_Networktype;
         AV13group_sdt.FromJSonString(AV30websession.Get("Group_EDIT_WALLET"), null);
         AV8amIgroupOwner = AV13group_sdt.gxTpr_Amigroupowner;
         AV17minSig = AV13group_sdt.gxTpr_Minimumshares;
         AV19numMembers = 0;
         AV31GXV1 = 1;
         while ( AV31GXV1 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV21oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV31GXV1));
            if ( ! ( ( AV21oneContact.gxTpr_Contactid == AV21oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV19numMembers = (short)(AV19numMembers+1);
            }
            AV31GXV1 = (int)(AV31GXV1+1);
         }
         AV15isLastSigner = false;
         AV28spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV23oneMuSigSignatures.FromJSonString(AV30websession.Get("MuSign_ONE"), null);
         AV24pendingSpendId = AV23oneMuSigSignatures.gxTpr_Id;
         if ( (Guid.Empty==AV23oneMuSigSignatures.gxTpr_Id) )
         {
            if ( ! AV8amIgroupOwner && ( AV17minSig == 1 ) )
            {
               AV15isLastSigner = true;
            }
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV13group_sdt, out  AV14groupEO) ;
         AV20ok = AV10delegatedProcessor.fromsdt(AV14groupEO);
         if ( ! AV20ok )
         {
            AV12error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV16jsonText = AV10delegatedProcessor.describespend(StringUtil.Trim( AV23oneMuSigSignatures.gxTpr_Psbt), StringUtil.Trim( AV13group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV13group_sdt.gxTpr_Extpubkeymultisigchange), AV18networkType);
         AV28spendInfo.FromJSonString(AV16jsonText, null);
         if ( ! AV28spendInfo.gxTpr_Success )
         {
            AV12error = AV28spendInfo.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         AV25sendAllCoins = AV23oneMuSigSignatures.gxTpr_Sendallcoins;
         AV11description = StringUtil.Trim( AV23oneMuSigSignatures.gxTpr_Description);
         AV26sendCoins = AV28spendInfo.gxTpr_Amountbtc;
         AV27sendTo = StringUtil.Trim( AV28spendInfo.gxTpr_Sendto);
         AV15isLastSigner = AV28spendInfo.gxTpr_Nextislast;
         AV32GXV2 = 1;
         while ( AV32GXV2 <= AV28spendInfo.gxTpr_Levels.Count )
         {
            AV22oneLevel = ((GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem)AV28spendInfo.gxTpr_Levels.Item(AV32GXV2));
            if ( AV22oneLevel.gxTpr_Percent == 0 )
            {
               AV9baseFee = AV22oneLevel.gxTpr_Feebtc;
            }
            AV32GXV2 = (int)(AV32GXV2+1);
         }
         if ( AV28spendInfo.gxTpr_Isigned )
         {
            AV12error = "You have already signed this payment";
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
         AV18networkType = "";
         AV24pendingSpendId = Guid.Empty;
         AV27sendTo = "";
         AV11description = "";
         AV28spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV12error = "";
         AV29walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV30websession = context.GetSession();
         AV21oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV23oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV14groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV10delegatedProcessor = new GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor(context);
         AV16jsonText = "";
         AV22oneLevel = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem(context);
         /* GeneXus formulas. */
      }

      private short AV17minSig ;
      private short AV19numMembers ;
      private int AV31GXV1 ;
      private int AV32GXV2 ;
      private decimal AV26sendCoins ;
      private decimal AV9baseFee ;
      private string AV18networkType ;
      private string AV27sendTo ;
      private string AV12error ;
      private bool AV8amIgroupOwner ;
      private bool AV15isLastSigner ;
      private bool AV25sendAllCoins ;
      private bool AV20ok ;
      private string AV16jsonText ;
      private string AV11description ;
      private Guid AV24pendingSpendId ;
      private IGxSession AV30websession ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV28spendInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV29walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV21oneContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV23oneMuSigSignatures ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV14groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor AV10delegatedProcessor ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem AV22oneLevel ;
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
