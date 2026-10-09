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
   public class savetimevaultdate : GXProcedure
   {
      public savetimevaultdate( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savetimevaultdate( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           DateTime aP1_restoreDate ,
                           out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13restoreDate = aP1_restoreDate;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                DateTime aP1_restoreDate )
      {
         execute(aP0_groupId, aP1_restoreDate, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 DateTime aP1_restoreDate ,
                                 out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13restoreDate = aP1_restoreDate;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( (DateTime.MinValue==AV13restoreDate) )
         {
            AV8error = "Please select the date when the backup became available to be restored.";
            cleanup();
            if (true) return;
         }
         if ( DateTimeUtil.ResetTime ( AV13restoreDate ) <= DateTimeUtil.ResetTime ( Gx_date ) )
         {
            AV8error = "The date must be in the future";
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV9group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV10groupId, out  GXt_SdtGroup_SDT1) ;
         AV9group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV9group_sdt.gxTpr_Groupid) || ! AV9group_sdt.gxTpr_Amigroupowner )
         {
            AV8error = "Only the owner of the Time Encrypted Vault can change it";
            cleanup();
            if (true) return;
         }
         if ( AV9group_sdt.gxTpr_Isactive )
         {
            AV8error = "The vault is active: use Change Restore Date";
            cleanup();
            if (true) return;
         }
         AV12oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV12oneTimeConstrain.gxTpr_Sequence = 0;
         AV12oneTimeConstrain.gxTpr_Address = "";
         AV12oneTimeConstrain.gxTpr_Date = AV13restoreDate;
         AV9group_sdt.gxTpr_Timeconstrain.Clear();
         AV9group_sdt.gxTpr_Timeconstrain.Add(AV12oneTimeConstrain, 0);
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV9group_sdt,  StringUtil.Trim( AV9group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV11grpupId, out  GXt_char2) ;
         AV8error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_char2 = AV8error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV9group_sdt, out  GXt_char2) ;
            AV8error = GXt_char2;
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
         AV8error = "";
         Gx_date = DateTime.MinValue;
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV11grpupId = Guid.Empty;
         GXt_char2 = "";
         Gx_date = DateTimeUtil.Today( context);
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      private string AV8error ;
      private string GXt_char2 ;
      private DateTime AV13restoreDate ;
      private DateTime Gx_date ;
      private Guid AV10groupId ;
      private Guid AV11grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV12oneTimeConstrain ;
      private string aP2_error ;
   }

}
