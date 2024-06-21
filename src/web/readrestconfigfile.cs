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
   public class readrestconfigfile : GXProcedure
   {
      public readrestconfigfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readrestconfigfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_rest_parameters )
      {
         this.AV8rest_parameters = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_rest_parameters=this.AV8rest_parameters;
      }

      public GXBaseCollection<SdtSDT_Rest_Parameters_Item> executeUdp( )
      {
         execute(out aP0_rest_parameters);
         return AV8rest_parameters ;
      }

      public void executeSubmit( out GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_rest_parameters )
      {
         this.AV8rest_parameters = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography") ;
         SubmitImpl();
         aP0_rest_parameters=this.AV8rest_parameters;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9directory.Source = "Wallets";
         if ( ! AV9directory.Exists() )
         {
            AV9directory.Create();
         }
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV10configFile.Source = "Wallets"+(GXt_boolean2 ? "/" : "\\")+"connection.conf";
         if ( AV10configFile.Exists() )
         {
            AV8rest_parameters.FromJSonFile(AV10configFile, null);
         }
         else
         {
            GXt_objcol_SdtSDT_Rest_Parameters_Item3 = AV8rest_parameters;
            new defaultrestparameters(context ).execute( out  GXt_objcol_SdtSDT_Rest_Parameters_Item3) ;
            AV8rest_parameters = GXt_objcol_SdtSDT_Rest_Parameters_Item3;
            AV10configFile.WriteAllText(AV8rest_parameters.ToJSonString(false), "");
         }
         this.cleanup();
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
         AV8rest_parameters = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography");
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV10configFile = new GxFile(context.GetPhysicalPath());
         GXt_objcol_SdtSDT_Rest_Parameters_Item3 = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography");
         /* GeneXus formulas. */
      }

      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> aP0_rest_parameters ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> AV8rest_parameters ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> GXt_objcol_SdtSDT_Rest_Parameters_Item3 ;
      private GxFile AV10configFile ;
      private GxDirectory AV9directory ;
   }

}
