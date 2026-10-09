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
   public class getgroupeditview : GXProcedure
   {
      public getgroupeditview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupeditview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.wallet.registered.SdtGroupListItem aP0_groupView )
      {
         this.AV9groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context) ;
         initialize();
         ExecuteImpl();
         aP0_groupView=this.AV9groupView;
      }

      public GeneXus.Programs.wallet.registered.SdtGroupListItem executeUdp( )
      {
         execute(out aP0_groupView);
         return AV9groupView ;
      }

      public void executeSubmit( out GeneXus.Programs.wallet.registered.SdtGroupListItem aP0_groupView )
      {
         this.AV9groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context) ;
         SubmitImpl();
         aP0_groupView=this.AV9groupView;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8group_sdt.FromJSonString(AV10websession.Get("Group_EDIT"), null);
         AV9groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         AV9groupView.gxTpr_Groupid = AV8group_sdt.gxTpr_Groupid;
         AV9groupView.gxTpr_Referencegroupid = AV8group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV9groupView.gxTpr_Groupname = AV8group_sdt.gxTpr_Groupname;
         AV9groupView.gxTpr_Grouptype = AV8group_sdt.gxTpr_Grouptype;
         AV9groupView.gxTpr_Subgrouptype = AV8group_sdt.gxTpr_Subgrouptype;
         AV9groupView.gxTpr_Amigroupowner = AV8group_sdt.gxTpr_Amigroupowner;
         AV9groupView.gxTpr_Isactive = AV8group_sdt.gxTpr_Isactive;
         AV9groupView.gxTpr_Bountygroupid = AV8group_sdt.gxTpr_Bountygroupid;
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
         AV9groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV10websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV10websession ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem AV9groupView ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem aP0_groupView ;
   }

}
