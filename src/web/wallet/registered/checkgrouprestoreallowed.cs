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
   public class checkgrouprestoreallowed : GXProcedure
   {
      public checkgrouprestoreallowed( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public checkgrouprestoreallowed( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV9group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV9group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV9group_sdt.gxTpr_Groupid) )
         {
            AV8error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         if ( ! AV9group_sdt.gxTpr_Numofsharesreached || String.IsNullOrEmpty(StringUtil.RTrim( AV9group_sdt.gxTpr_Cleartextshare)) )
         {
            AV8error = "The minimum number of shares to recover the wallet was not reached yet";
            cleanup();
            if (true) return;
         }
         if ( ( AV9group_sdt.gxTpr_Grouptype == 10 ) && ! AV9group_sdt.gxTpr_Amigroupowner )
         {
            GXt_char2 = AV8error;
            new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV9group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV9group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV10group_sdt_owner, out  GXt_char2) ;
            AV8error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && AV10group_sdt_owner.gxTpr_Restorestopped )
            {
               AV8error = "The owner of this wallet STOPPED the restore on " + context.localUtil.TToC( AV10group_sdt_owner.gxTpr_Restorestoppeddatetime, 8, 5, 1, 2, "/", ":", " ");
            }
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
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_char2 = "";
         AV10group_sdt_owner = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string GXt_char2 ;
      private Guid AV11groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt_owner ;
      private string aP1_error ;
   }

}
