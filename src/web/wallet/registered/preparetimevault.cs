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
   public class preparetimevault : GXProcedure
   {
      public preparetimevault( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public preparetimevault( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_created ,
                           out Guid aP2_dataGroupId ,
                           out Guid aP3_bountyGroupId ,
                           out bool aP4_isActive ,
                           out DateTime aP5_restoreDate ,
                           out bool aP6_canActivate ,
                           out string aP7_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV11created = false ;
         this.AV13dataGroupId = Guid.Empty ;
         this.AV9bountyGroupId = Guid.Empty ;
         this.AV19isActive = false ;
         this.AV21restoreDate = DateTime.MinValue ;
         this.AV10canActivate = false ;
         this.AV15error = "" ;
         initialize();
         ExecuteImpl();
         aP1_created=this.AV11created;
         aP2_dataGroupId=this.AV13dataGroupId;
         aP3_bountyGroupId=this.AV9bountyGroupId;
         aP4_isActive=this.AV19isActive;
         aP5_restoreDate=this.AV21restoreDate;
         aP6_canActivate=this.AV10canActivate;
         aP7_error=this.AV15error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out bool aP1_created ,
                                out Guid aP2_dataGroupId ,
                                out Guid aP3_bountyGroupId ,
                                out bool aP4_isActive ,
                                out DateTime aP5_restoreDate ,
                                out bool aP6_canActivate )
      {
         execute(aP0_groupId, out aP1_created, out aP2_dataGroupId, out aP3_bountyGroupId, out aP4_isActive, out aP5_restoreDate, out aP6_canActivate, out aP7_error);
         return AV15error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_created ,
                                 out Guid aP2_dataGroupId ,
                                 out Guid aP3_bountyGroupId ,
                                 out bool aP4_isActive ,
                                 out DateTime aP5_restoreDate ,
                                 out bool aP6_canActivate ,
                                 out string aP7_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV11created = false ;
         this.AV13dataGroupId = Guid.Empty ;
         this.AV9bountyGroupId = Guid.Empty ;
         this.AV19isActive = false ;
         this.AV21restoreDate = DateTime.MinValue ;
         this.AV10canActivate = false ;
         this.AV15error = "" ;
         SubmitImpl();
         aP1_created=this.AV11created;
         aP2_dataGroupId=this.AV13dataGroupId;
         aP3_bountyGroupId=this.AV9bountyGroupId;
         aP4_isActive=this.AV19isActive;
         aP5_restoreDate=this.AV21restoreDate;
         aP6_canActivate=this.AV10canActivate;
         aP7_error=this.AV15error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11created = false;
         AV10canActivate = false;
         GXt_SdtGroup_SDT1 = AV16group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV17groupId, out  GXt_SdtGroup_SDT1) ;
         AV16group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV16group_sdt.gxTpr_Groupid) )
         {
            AV15error = "We couldn't find the Time Encrypted Vault";
            cleanup();
            if (true) return;
         }
         if ( ( AV16group_sdt.gxTpr_Grouptype == 20 ) && AV16group_sdt.gxTpr_Amigroupowner && (0==AV16group_sdt.gxTpr_Subgrouptype) )
         {
            AV12data_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV12data_group_sdt.gxTpr_Groupname = StringUtil.Trim( AV16group_sdt.gxTpr_Groupname);
            AV12data_group_sdt.gxTpr_Amigroupowner = true;
            AV12data_group_sdt.gxTpr_Grouptype = 20;
            AV12data_group_sdt.gxTpr_Subgrouptype = 30;
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.registered.creategroup(context ).execute(  AV12data_group_sdt, out  AV18grpupId, out  GXt_char2) ;
            AV15error = GXt_char2;
            GXt_char2 = AV14encryptionKey;
            new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char2) ;
            AV14encryptionKey = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               AV15error = "We couldn't create Data Group: " + AV15error;
               cleanup();
               if (true) return;
            }
            AV12data_group_sdt.gxTpr_Groupid = AV18grpupId;
            AV12data_group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid = AV18grpupId;
            AV12data_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = AV14encryptionKey;
            GXt_char2 = AV15error;
            GXt_guid3 = AV12data_group_sdt.gxTpr_Groupid;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV12data_group_sdt,  AV12data_group_sdt.gxTpr_Encpassword, out  GXt_guid3, out  GXt_char2) ;
            AV12data_group_sdt.gxTpr_Groupid = GXt_guid3;
            AV15error = GXt_char2;
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV12data_group_sdt, out  GXt_char2) ;
            AV15error += GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               cleanup();
               if (true) return;
            }
            AV8bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV8bount_group_sdt.gxTpr_Groupname = StringUtil.Trim( AV16group_sdt.gxTpr_Groupname);
            AV8bount_group_sdt.gxTpr_Amigroupowner = true;
            AV8bount_group_sdt.gxTpr_Grouptype = 20;
            AV8bount_group_sdt.gxTpr_Subgrouptype = 20;
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.registered.creategroup(context ).execute(  AV8bount_group_sdt, out  AV18grpupId, out  GXt_char2) ;
            AV15error = GXt_char2;
            GXt_char2 = AV14encryptionKey;
            new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char2) ;
            AV14encryptionKey = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               AV15error = "We couldn't create Bounty Group: " + AV15error;
               cleanup();
               if (true) return;
            }
            AV8bount_group_sdt.gxTpr_Groupid = AV18grpupId;
            AV8bount_group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid = AV18grpupId;
            AV8bount_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = AV14encryptionKey;
            GXt_char2 = AV15error;
            GXt_guid3 = AV8bount_group_sdt.gxTpr_Groupid;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV8bount_group_sdt,  AV8bount_group_sdt.gxTpr_Encpassword, out  GXt_guid3, out  GXt_char2) ;
            AV8bount_group_sdt.gxTpr_Groupid = GXt_guid3;
            AV15error = GXt_char2;
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV8bount_group_sdt, out  GXt_char2) ;
            AV15error += GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               cleanup();
               if (true) return;
            }
            AV16group_sdt.gxTpr_Subgrouptype = 10;
            AV16group_sdt.gxTpr_Bountygroupid = AV8bount_group_sdt.gxTpr_Groupid;
            AV16group_sdt.gxTpr_Datagroupid = AV12data_group_sdt.gxTpr_Groupid;
            AV16group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = "";
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV16group_sdt,  AV16group_sdt.gxTpr_Encpassword, out  AV18grpupId, out  GXt_char2) ;
            AV15error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               GXt_char2 = AV15error;
               new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV16group_sdt, out  GXt_char2) ;
               AV15error = GXt_char2;
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               cleanup();
               if (true) return;
            }
            AV11created = true;
         }
         AV13dataGroupId = AV16group_sdt.gxTpr_Datagroupid;
         AV9bountyGroupId = AV16group_sdt.gxTpr_Bountygroupid;
         AV19isActive = AV16group_sdt.gxTpr_Isactive;
         GXt_SdtGroup_SDT_TimeConstrainItem4 = AV20oneTimeConstrain;
         new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV16group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem4) ;
         AV20oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem4;
         AV21restoreDate = AV20oneTimeConstrain.gxTpr_Date;
         if ( new GeneXus.Programs.wallet.registered.isgroupreadytoactivate(context).executeUdp(  AV16group_sdt.gxTpr_Datagroupid) && new GeneXus.Programs.wallet.registered.isgroupreadytoactivate(context).executeUdp(  AV16group_sdt.gxTpr_Bountygroupid) )
         {
            AV10canActivate = true;
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
         AV13dataGroupId = Guid.Empty;
         AV9bountyGroupId = Guid.Empty;
         AV21restoreDate = DateTime.MinValue;
         AV15error = "";
         AV16group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12data_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18grpupId = Guid.Empty;
         AV14encryptionKey = "";
         AV8bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_guid3 = Guid.Empty;
         GXt_char2 = "";
         AV20oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem4 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         /* GeneXus formulas. */
      }

      private string AV15error ;
      private string AV14encryptionKey ;
      private string GXt_char2 ;
      private DateTime AV21restoreDate ;
      private bool AV11created ;
      private bool AV19isActive ;
      private bool AV10canActivate ;
      private Guid AV17groupId ;
      private Guid AV13dataGroupId ;
      private Guid AV9bountyGroupId ;
      private Guid AV18grpupId ;
      private Guid GXt_guid3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12data_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8bount_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV20oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem4 ;
      private bool aP1_created ;
      private Guid aP2_dataGroupId ;
      private Guid aP3_bountyGroupId ;
      private bool aP4_isActive ;
      private DateTime aP5_restoreDate ;
      private bool aP6_canActivate ;
      private string aP7_error ;
   }

}
