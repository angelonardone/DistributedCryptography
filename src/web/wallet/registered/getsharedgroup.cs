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
   public class getsharedgroup : GXProcedure
   {
      public getsharedgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getsharedgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out Guid aP1_sharedGroupId ,
                           out string aP2_groupKey ,
                           out string aP3_accessToken ,
                           out bool aP4_isOwner ,
                           out string aP5_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV17sharedGroupId = Guid.Empty ;
         this.AV12groupKey = "" ;
         this.AV18accessToken = "" ;
         this.AV13isOwner = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_sharedGroupId=this.AV17sharedGroupId;
         aP2_groupKey=this.AV12groupKey;
         aP3_accessToken=this.AV18accessToken;
         aP4_isOwner=this.AV13isOwner;
         aP5_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out Guid aP1_sharedGroupId ,
                                out string aP2_groupKey ,
                                out string aP3_accessToken ,
                                out bool aP4_isOwner )
      {
         execute(aP0_groupId, out aP1_sharedGroupId, out aP2_groupKey, out aP3_accessToken, out aP4_isOwner, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out Guid aP1_sharedGroupId ,
                                 out string aP2_groupKey ,
                                 out string aP3_accessToken ,
                                 out bool aP4_isOwner ,
                                 out string aP5_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV17sharedGroupId = Guid.Empty ;
         this.AV12groupKey = "" ;
         this.AV18accessToken = "" ;
         this.AV13isOwner = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_sharedGroupId=this.AV17sharedGroupId;
         aP2_groupKey=this.AV12groupKey;
         aP3_accessToken=this.AV18accessToken;
         aP4_isOwner=this.AV13isOwner;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         AV13isOwner = false;
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) )
         {
            AV9error = "The group was not found in this wallet";
            cleanup();
            if (true) return;
         }
         AV17sharedGroupId = AV10group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV12groupKey = StringUtil.Trim( AV10group_sdt.gxTpr_Othergroup.gxTpr_Encpassword);
         if ( (Guid.Empty==AV17sharedGroupId) || String.IsNullOrEmpty(StringUtil.RTrim( AV12groupKey)) )
         {
            AV9error = "This group has no shared key yet: is it active?";
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.hmacsha256hex(context ).execute(  AV12groupKey,  "DistributedCryptography group access\\n"+StringUtil.Trim( AV17sharedGroupId.ToString()), out  AV18accessToken, out  AV9error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) && AV10group_sdt.gxTpr_Amigroupowner )
         {
            AV13isOwner = true;
            new GeneXus.Programs.wallet.registered.sendgroupaccess(context ).execute(  AV10group_sdt, out  AV9error) ;
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
         AV17sharedGroupId = Guid.Empty;
         AV12groupKey = "";
         AV18accessToken = "";
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private string AV12groupKey ;
      private string AV9error ;
      private bool AV13isOwner ;
      private string AV18accessToken ;
      private Guid AV11groupId ;
      private Guid AV17sharedGroupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private Guid aP1_sharedGroupId ;
      private string aP2_groupKey ;
      private string aP3_accessToken ;
      private bool aP4_isOwner ;
      private string aP5_error ;
   }

}
