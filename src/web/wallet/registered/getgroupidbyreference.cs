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
   public class getgroupidbyreference : GXProcedure
   {
      public getgroupidbyreference( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupidbyreference( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           out Guid aP1_groupId )
      {
         this.AV12referenceGroupId = aP0_referenceGroupId;
         this.AV11groupId = Guid.Empty ;
         initialize();
         ExecuteImpl();
         aP1_groupId=this.AV11groupId;
      }

      public Guid executeUdp( Guid aP0_referenceGroupId )
      {
         execute(aP0_referenceGroupId, out aP1_groupId);
         return AV11groupId ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 out Guid aP1_groupId )
      {
         this.AV12referenceGroupId = aP0_referenceGroupId;
         this.AV11groupId = Guid.Empty ;
         SubmitImpl();
         aP1_groupId=this.AV11groupId;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11groupId = Guid.Empty;
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV8all_groups_sdt.Count )
         {
            AV10group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV13GXV1));
            if ( AV10group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12referenceGroupId )
            {
               AV11groupId = AV10group_sdt.gxTpr_Groupid;
               if (true) break;
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV11groupId = Guid.Empty;
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV9error ;
      private Guid AV12referenceGroupId ;
      private Guid AV11groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private Guid aP1_groupId ;
   }

}
