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
namespace GeneXus.Programs.electrum {
   public class defaultparameters : GXProcedure
   {
      public defaultparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public defaultparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1connectionparameters = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         Gxm2rootcol.Add(Gxm1connectionparameters, 0);
         Gxm1connectionparameters.gxTpr_Connectiontype = "tcp";
         Gxm1connectionparameters.gxTpr_Hostname = "electrs-mainnet.distributedcryptography.com";
         Gxm1connectionparameters.gxTpr_Port = 50100;
         Gxm1connectionparameters.gxTpr_Secure = false;
         Gxm1connectionparameters.gxTpr_Networktype = "MainNet";
         Gxm1connectionparameters.gxTpr_Timeoutmiliseconds = 5000;
         Gxm1connectionparameters = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         Gxm2rootcol.Add(Gxm1connectionparameters, 0);
         Gxm1connectionparameters.gxTpr_Connectiontype = "tcp";
         Gxm1connectionparameters.gxTpr_Hostname = "electrs-testnet.distributedcryptography.com";
         Gxm1connectionparameters.gxTpr_Port = 50100;
         Gxm1connectionparameters.gxTpr_Secure = false;
         Gxm1connectionparameters.gxTpr_Networktype = "TestNet";
         Gxm1connectionparameters.gxTpr_Timeoutmiliseconds = 5000;
         Gxm1connectionparameters = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         Gxm2rootcol.Add(Gxm1connectionparameters, 0);
         Gxm1connectionparameters.gxTpr_Connectiontype = "tcp";
         Gxm1connectionparameters.gxTpr_Hostname = "localhost";
         Gxm1connectionparameters.gxTpr_Port = 50001;
         Gxm1connectionparameters.gxTpr_Secure = false;
         Gxm1connectionparameters.gxTpr_Networktype = "RegTest";
         Gxm1connectionparameters.gxTpr_Timeoutmiliseconds = 5000;
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
         Gxm1connectionparameters = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> Gxm2rootcol ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem Gxm1connectionparameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_Gxm2rootcol ;
   }

}
