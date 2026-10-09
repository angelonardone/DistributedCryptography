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
   public class getgroupdescriptors : GXProcedure
   {
      public getgroupdescriptors( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupdescriptors( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_groupName ,
                           out string aP1_receivingDescriptor ,
                           out string aP2_changeDescriptor ,
                           out string aP3_error )
      {
         this.AV11groupName = "" ;
         this.AV12receivingDescriptor = "" ;
         this.AV8changeDescriptor = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_groupName=this.AV11groupName;
         aP1_receivingDescriptor=this.AV12receivingDescriptor;
         aP2_changeDescriptor=this.AV8changeDescriptor;
         aP3_error=this.AV9error;
      }

      public string executeUdp( out string aP0_groupName ,
                                out string aP1_receivingDescriptor ,
                                out string aP2_changeDescriptor )
      {
         execute(out aP0_groupName, out aP1_receivingDescriptor, out aP2_changeDescriptor, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_groupName ,
                                 out string aP1_receivingDescriptor ,
                                 out string aP2_changeDescriptor ,
                                 out string aP3_error )
      {
         this.AV11groupName = "" ;
         this.AV12receivingDescriptor = "" ;
         this.AV8changeDescriptor = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP0_groupName=this.AV11groupName;
         aP1_receivingDescriptor=this.AV12receivingDescriptor;
         aP2_changeDescriptor=this.AV8changeDescriptor;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10group_sdt.FromJSonString(AV15websession.Get("Group_EDIT_WALLET"), null);
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) )
         {
            AV9error = "Open the Wallet Balance tab of the group first";
            cleanup();
            if (true) return;
         }
         AV11groupName = StringUtil.Trim( AV10group_sdt.gxTpr_Groupname);
         if ( ! AV10group_sdt.gxTpr_Isactive )
         {
            AV9error = "The group has no addresses yet: it has to be activated first";
            cleanup();
            if (true) return;
         }
         GXt_SdtWalletInfo1 = AV14walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV14walletInfo = GXt_SdtWalletInfo1;
         if ( AV10group_sdt.gxTpr_Grouptype == 50 )
         {
            GXt_SdtGroupDescriptorResult2 = AV13result;
            new GeneXus.Programs.wallet.registered.getlegacydescriptor(context ).execute(  AV10group_sdt,  AV14walletInfo.gxTpr_Networktype,  false, out  GXt_SdtGroupDescriptorResult2) ;
            AV13result = GXt_SdtGroupDescriptorResult2;
            if ( ! AV13result.gxTpr_Success )
            {
               AV9error = AV13result.gxTpr_Error;
               cleanup();
               if (true) return;
            }
            AV12receivingDescriptor = StringUtil.Trim( AV13result.gxTpr_Descriptor);
            GXt_SdtGroupDescriptorResult2 = AV13result;
            new GeneXus.Programs.wallet.registered.getlegacydescriptor(context ).execute(  AV10group_sdt,  AV14walletInfo.gxTpr_Networktype,  true, out  GXt_SdtGroupDescriptorResult2) ;
            AV13result = GXt_SdtGroupDescriptorResult2;
            if ( ! AV13result.gxTpr_Success )
            {
               AV9error = AV13result.gxTpr_Error;
               cleanup();
               if (true) return;
            }
            AV8changeDescriptor = StringUtil.Trim( AV13result.gxTpr_Descriptor);
         }
         else if ( AV10group_sdt.gxTpr_Grouptype == 30 )
         {
            GXt_SdtGroupDescriptorResult2 = AV13result;
            new GeneXus.Programs.wallet.registered.getdelegateddescriptor(context ).execute(  AV10group_sdt,  AV14walletInfo.gxTpr_Networktype,  false, out  GXt_SdtGroupDescriptorResult2) ;
            AV13result = GXt_SdtGroupDescriptorResult2;
            if ( ! AV13result.gxTpr_Success )
            {
               AV9error = AV13result.gxTpr_Error;
               cleanup();
               if (true) return;
            }
            AV12receivingDescriptor = StringUtil.Trim( AV13result.gxTpr_Descriptor);
            GXt_SdtGroupDescriptorResult2 = AV13result;
            new GeneXus.Programs.wallet.registered.getdelegateddescriptor(context ).execute(  AV10group_sdt,  AV14walletInfo.gxTpr_Networktype,  true, out  GXt_SdtGroupDescriptorResult2) ;
            AV13result = GXt_SdtGroupDescriptorResult2;
            if ( ! AV13result.gxTpr_Success )
            {
               AV9error = AV13result.gxTpr_Error;
               cleanup();
               if (true) return;
            }
            AV8changeDescriptor = StringUtil.Trim( AV13result.gxTpr_Descriptor);
         }
         else
         {
            AV9error = "This type of group has no descriptors";
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
         AV11groupName = "";
         AV12receivingDescriptor = "";
         AV8changeDescriptor = "";
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15websession = context.GetSession();
         AV14walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13result = new GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult(context);
         GXt_SdtGroupDescriptorResult2 = new GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult(context);
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string AV12receivingDescriptor ;
      private string AV8changeDescriptor ;
      private string AV11groupName ;
      private IGxSession AV15websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV14walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult AV13result ;
      private GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult GXt_SdtGroupDescriptorResult2 ;
      private string aP0_groupName ;
      private string aP1_receivingDescriptor ;
      private string aP2_changeDescriptor ;
      private string aP3_error ;
   }

}
