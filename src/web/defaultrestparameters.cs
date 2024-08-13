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
namespace GeneXus.Programs {
   public class defaultrestparameters : GXProcedure
   {
      public defaultrestparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public defaultrestparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<SdtSDT_Rest_Parameters_Item> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1sdt_rest_parameters = new SdtSDT_Rest_Parameters_Item(context);
         Gxm2rootcol.Add(Gxm1sdt_rest_parameters, 0);
         Gxm1sdt_rest_parameters.gxTpr_Hostname = "explorer-mainnet.distributedcryptography.com";
         Gxm1sdt_rest_parameters.gxTpr_Port = 0;
         Gxm1sdt_rest_parameters.gxTpr_Secure = true;
         Gxm1sdt_rest_parameters.gxTpr_Restpath = "/GxExplorer/services/rest";
         Gxm1sdt_rest_parameters.gxTpr_Networktype = "MainNet";
         Gxm1sdt_rest_parameters = new SdtSDT_Rest_Parameters_Item(context);
         Gxm2rootcol.Add(Gxm1sdt_rest_parameters, 0);
         Gxm1sdt_rest_parameters.gxTpr_Hostname = "explorer-testnet.distributedcryptography.com";
         Gxm1sdt_rest_parameters.gxTpr_Port = 0;
         Gxm1sdt_rest_parameters.gxTpr_Secure = true;
         Gxm1sdt_rest_parameters.gxTpr_Restpath = "/GxExplorer/services/rest";
         Gxm1sdt_rest_parameters.gxTpr_Networktype = "TestNet";
         Gxm1sdt_rest_parameters = new SdtSDT_Rest_Parameters_Item(context);
         Gxm2rootcol.Add(Gxm1sdt_rest_parameters, 0);
         Gxm1sdt_rest_parameters.gxTpr_Hostname = "explorer-regtest.distributedcryptography.com";
         Gxm1sdt_rest_parameters.gxTpr_Port = 0;
         Gxm1sdt_rest_parameters.gxTpr_Secure = false;
         Gxm1sdt_rest_parameters.gxTpr_Restpath = "/GxExplorer/services/rest";
         Gxm1sdt_rest_parameters.gxTpr_Networktype = "RegTest";
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
         Gxm1sdt_rest_parameters = new SdtSDT_Rest_Parameters_Item(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> Gxm2rootcol ;
      private SdtSDT_Rest_Parameters_Item Gxm1sdt_rest_parameters ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_Gxm2rootcol ;
   }

}
