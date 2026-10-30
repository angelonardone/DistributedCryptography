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
   public class getmusig2sendcontext : GXProcedure
   {
      public getmusig2sendcontext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getmusig2sendcontext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out Guid aP0_groupId ,
                           out bool aP1_ownerPaysAlone ,
                           out string aP2_networkType )
      {
         this.AV9groupId = Guid.Empty ;
         this.AV11ownerPaysAlone = false ;
         this.AV10networkType = "" ;
         initialize();
         ExecuteImpl();
         aP0_groupId=this.AV9groupId;
         aP1_ownerPaysAlone=this.AV11ownerPaysAlone;
         aP2_networkType=this.AV10networkType;
      }

      public string executeUdp( out Guid aP0_groupId ,
                                out bool aP1_ownerPaysAlone )
      {
         execute(out aP0_groupId, out aP1_ownerPaysAlone, out aP2_networkType);
         return AV10networkType ;
      }

      public void executeSubmit( out Guid aP0_groupId ,
                                 out bool aP1_ownerPaysAlone ,
                                 out string aP2_networkType )
      {
         this.AV9groupId = Guid.Empty ;
         this.AV11ownerPaysAlone = false ;
         this.AV10networkType = "" ;
         SubmitImpl();
         aP0_groupId=this.AV9groupId;
         aP1_ownerPaysAlone=this.AV11ownerPaysAlone;
         aP2_networkType=this.AV10networkType;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8group_sdt.FromJSonString(AV13websession.Get("Group_EDIT_WALLET"), null);
         AV9groupId = AV8group_sdt.gxTpr_Groupid;
         AV11ownerPaysAlone = (bool)(((AV8group_sdt.gxTpr_Grouptype==80)&&AV8group_sdt.gxTpr_Amigroupowner));
         GXt_SdtWalletInfo1 = AV12walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV12walletInfo = GXt_SdtWalletInfo1;
         AV10networkType = AV12walletInfo.gxTpr_Networktype;
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
         AV9groupId = Guid.Empty;
         AV10networkType = "";
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13websession = context.GetSession();
         AV12walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private string AV10networkType ;
      private bool AV11ownerPaysAlone ;
      private Guid AV9groupId ;
      private IGxSession AV13websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV12walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private Guid aP0_groupId ;
      private bool aP1_ownerPaysAlone ;
      private string aP2_networkType ;
   }

}
