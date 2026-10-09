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
   public class getgrouplist : GXProcedure
   {
      public getgrouplist( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgrouplist( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> aP0_groupList )
      {
         this.AV11groupList = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem>( context, "GroupListItem", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_groupList=this.AV11groupList;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> executeUdp( )
      {
         execute(out aP0_groupList);
         return AV11groupList ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> aP0_groupList )
      {
         this.AV11groupList = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem>( context, "GroupListItem", "distributedcryptography") ;
         SubmitImpl();
         aP0_groupList=this.AV11groupList;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11groupList.Clear();
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV8all_groups_sdt.Count )
         {
            AV10group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV14GXV1));
            if ( ! AV10group_sdt.gxTpr_Othergroup.gxTpr_Invitationdeclined )
            {
               AV13show = true;
               if ( AV10group_sdt.gxTpr_Grouptype == 20 )
               {
                  if ( ( AV10group_sdt.gxTpr_Subgrouptype == 10 ) || ( ! AV10group_sdt.gxTpr_Amigroupowner && ( AV10group_sdt.gxTpr_Subgrouptype == 30 ) ) || ( ! AV10group_sdt.gxTpr_Amigroupowner && ( AV10group_sdt.gxTpr_Subgrouptype == 20 ) ) || (0==AV10group_sdt.gxTpr_Subgrouptype) )
                  {
                     AV13show = true;
                  }
                  else
                  {
                     AV13show = false;
                  }
               }
               if ( AV13show )
               {
                  AV12item = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
                  AV12item.gxTpr_Groupid = AV10group_sdt.gxTpr_Groupid;
                  AV12item.gxTpr_Referencegroupid = AV10group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
                  AV12item.gxTpr_Groupname = AV10group_sdt.gxTpr_Groupname;
                  AV12item.gxTpr_Grouptype = AV10group_sdt.gxTpr_Grouptype;
                  AV12item.gxTpr_Subgrouptype = AV10group_sdt.gxTpr_Subgrouptype;
                  AV12item.gxTpr_Amigroupowner = AV10group_sdt.gxTpr_Amigroupowner;
                  AV12item.gxTpr_Isactive = AV10group_sdt.gxTpr_Isactive;
                  AV12item.gxTpr_Bountygroupid = AV10group_sdt.gxTpr_Bountygroupid;
                  AV11groupList.Add(AV12item, 0);
               }
            }
            AV14GXV1 = (int)(AV14GXV1+1);
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
         AV11groupList = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem>( context, "GroupListItem", "distributedcryptography");
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12item = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private string AV9error ;
      private bool AV13show ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> AV11groupList ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem AV12item ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> aP0_groupList ;
   }

}
