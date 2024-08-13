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
namespace GeneXus.Programs.general.ui {
   public class sidebaritemsoneaddressdp : GXProcedure
   {
      public sidebaritemsoneaddressdp( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sidebaritemsoneaddressdp( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtProgramNames_ProgramName1 = AV5wwProgramNames;
         new GeneXus.Programs.general.ui.listmenuoneaddress(context ).execute( out  GXt_objcol_SdtProgramNames_ProgramName1) ;
         AV5wwProgramNames = GXt_objcol_SdtProgramNames_ProgramName1;
         AV9GXV1 = 1;
         while ( AV9GXV1 <= AV5wwProgramNames.Count )
         {
            AV6wwProgramName = ((GeneXus.Programs.general.ui.SdtProgramNames_ProgramName)AV5wwProgramNames.Item(AV9GXV1));
            Gxm1sidebaritems = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
            Gxm2rootcol.Add(Gxm1sidebaritems, 0);
            Gxm1sidebaritems.gxTpr_Id = AV6wwProgramName.gxTpr_Name;
            Gxm1sidebaritems.gxTpr_Title = AV6wwProgramName.gxTpr_Description;
            Gxm1sidebaritems.gxTpr_Target = AV6wwProgramName.gxTpr_Link;
            Gxm1sidebaritems.gxTpr_Hassubitems = false;
            AV9GXV1 = (int)(AV9GXV1+1);
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
         AV5wwProgramNames = new GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName>( context, "ProgramName", "distributedcryptography");
         GXt_objcol_SdtProgramNames_ProgramName1 = new GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName>( context, "ProgramName", "distributedcryptography");
         AV6wwProgramName = new GeneXus.Programs.general.ui.SdtProgramNames_ProgramName(context);
         Gxm1sidebaritems = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
         /* GeneXus formulas. */
      }

      private int AV9GXV1 ;
      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> Gxm2rootcol ;
      private GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> AV5wwProgramNames ;
      private GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> GXt_objcol_SdtProgramNames_ProgramName1 ;
      private GeneXus.Programs.general.ui.SdtProgramNames_ProgramName AV6wwProgramName ;
      private GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem Gxm1sidebaritems ;
      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_Gxm2rootcol ;
   }

}
