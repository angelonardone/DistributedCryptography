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
   public class getlocalgroupforrow : GXProcedure
   {
      public getlocalgroupforrow( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlocalgroupforrow( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_referenceGroupId ,
                           out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt )
      {
         this.AV12groupId = aP0_groupId;
         this.AV14referenceGroupId = aP1_referenceGroupId;
         this.AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         initialize();
         ExecuteImpl();
         aP2_group_sdt=this.AV11group_sdt;
      }

      public GeneXus.Programs.wallet.registered.SdtGroup_SDT executeUdp( Guid aP0_groupId ,
                                                                         Guid aP1_referenceGroupId )
      {
         execute(aP0_groupId, aP1_referenceGroupId, out aP2_group_sdt);
         return AV11group_sdt ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_referenceGroupId ,
                                 out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt )
      {
         this.AV12groupId = aP0_groupId;
         this.AV14referenceGroupId = aP1_referenceGroupId;
         this.AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         SubmitImpl();
         aP2_group_sdt=this.AV11group_sdt;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10found = false;
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV8all_groups_sdt.Count )
         {
            AV13one_group = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV15GXV1));
            if ( ! (Guid.Empty==AV12groupId) )
            {
               if ( AV13one_group.gxTpr_Groupid == AV12groupId )
               {
                  AV10found = true;
               }
            }
            else
            {
               if ( (Guid.Empty==AV13one_group.gxTpr_Groupid) && ( AV13one_group.gxTpr_Othergroup.gxTpr_Referencegroupid == AV14referenceGroupId ) )
               {
                  AV10found = true;
               }
            }
            if ( AV10found )
            {
               AV11group_sdt = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV13one_group.Clone());
               if (true) break;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
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
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV9error = "";
         AV13one_group = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV9error ;
      private bool AV10found ;
      private Guid AV12groupId ;
      private Guid AV14referenceGroupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13one_group ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt ;
   }

}
