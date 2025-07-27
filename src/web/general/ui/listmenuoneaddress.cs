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
namespace GeneXus.Programs.general.ui {
   public class listmenuoneaddress : GXProcedure
   {
      public listmenuoneaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public listmenuoneaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> aP0_ProgramNames )
      {
         this.AV8ProgramNames = new GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName>( context, "ProgramName", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_ProgramNames=this.AV8ProgramNames;
      }

      public GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> executeUdp( )
      {
         execute(out aP0_ProgramNames);
         return AV8ProgramNames ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> aP0_ProgramNames )
      {
         this.AV8ProgramNames = new GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName>( context, "ProgramName", "distributedcryptography") ;
         SubmitImpl();
         aP0_ProgramNames=this.AV8ProgramNames;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12Name = "ReturnToWallets";
         AV10description = "Return to Wallets";
         AV13Link = formatLink("wallet.returntowallets") ;
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ADDPROGRAM' Routine */
         returnInSub = false;
         AV9ProgramName = new GeneXus.Programs.general.ui.SdtProgramNames_ProgramName(context);
         AV9ProgramName.gxTpr_Name = AV12Name;
         AV9ProgramName.gxTpr_Description = AV10description;
         AV9ProgramName.gxTpr_Link = AV13Link;
         AV8ProgramNames.Add(AV9ProgramName, 0);
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
         AV8ProgramNames = new GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName>( context, "ProgramName", "distributedcryptography");
         AV12Name = "";
         AV10description = "";
         AV13Link = "";
         AV9ProgramName = new GeneXus.Programs.general.ui.SdtProgramNames_ProgramName(context);
         /* GeneXus formulas. */
      }

      private string AV12Name ;
      private string AV13Link ;
      private bool returnInSub ;
      private string AV10description ;
      private GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> AV8ProgramNames ;
      private GeneXus.Programs.general.ui.SdtProgramNames_ProgramName AV9ProgramName ;
      private GXBaseCollection<GeneXus.Programs.general.ui.SdtProgramNames_ProgramName> aP0_ProgramNames ;
   }

}
