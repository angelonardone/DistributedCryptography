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
   public class deletegroupfromlist : GXProcedure
   {
      public deletegroupfromlist( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletegroupfromlist( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) )
         {
            AV9error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         if ( ( AV10group_sdt.gxTpr_Grouptype == 20 ) && ( AV10group_sdt.gxTpr_Subgrouptype == 10 ) )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.deletegroup(context ).execute(  AV10group_sdt.gxTpr_Datagroupid, out  GXt_char2) ;
            AV9error = GXt_char2;
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.deletegroup(context ).execute(  AV10group_sdt.gxTpr_Bountygroupid, out  GXt_char2) ;
            AV9error += GXt_char2;
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.deletegroup(context ).execute(  AV10group_sdt.gxTpr_Groupid, out  GXt_char2) ;
            AV9error += GXt_char2;
         }
         else
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.deletegroup(context ).execute(  AV10group_sdt.gxTpr_Groupid, out  GXt_char2) ;
            AV9error = GXt_char2;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
            AV12kept.Clear();
            AV15GXV1 = 1;
            while ( AV15GXV1 <= AV8all_groups_sdt.Count )
            {
               AV13one_group = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV15GXV1));
               AV14remove = false;
               if ( AV13one_group.gxTpr_Groupid == AV10group_sdt.gxTpr_Groupid )
               {
                  AV14remove = true;
               }
               if ( ( AV10group_sdt.gxTpr_Grouptype == 20 ) && ( AV10group_sdt.gxTpr_Subgrouptype == 10 ) )
               {
                  if ( ( AV13one_group.gxTpr_Groupid == AV10group_sdt.gxTpr_Datagroupid ) || ( AV13one_group.gxTpr_Groupid == AV10group_sdt.gxTpr_Bountygroupid ) )
                  {
                     AV14remove = true;
                  }
               }
               if ( ! AV14remove )
               {
                  AV12kept.Add(AV13one_group, 0);
               }
               AV15GXV1 = (int)(AV15GXV1+1);
            }
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV12kept.ToJSonString(false), out  GXt_char2) ;
            AV9error = GXt_char2;
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
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV12kept = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV13one_group = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV9error ;
      private string GXt_char2 ;
      private bool AV14remove ;
      private Guid AV11groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV12kept ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13one_group ;
      private string aP1_error ;
   }

}
