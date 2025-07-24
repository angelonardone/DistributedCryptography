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
namespace GeneXus.Programs.electrum {
   public class getelectrumconfigservers : GXProcedure
   {
      public getelectrumconfigservers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getelectrumconfigservers( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters )
      {
         this.AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_ConnectionParameters=this.AV9ConnectionParameters;
      }

      public GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> executeUdp( )
      {
         execute(out aP0_ConnectionParameters);
         return AV9ConnectionParameters ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters )
      {
         this.AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         SubmitImpl();
         aP0_ConnectionParameters=this.AV9ConnectionParameters;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10directory.Source = "Wallets";
         if ( ! AV10directory.Exists() )
         {
            AV10directory.Create();
         }
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV8configFile.Source = "Wallets"+(GXt_boolean2 ? "/" : "\\")+"electrum.conf";
         if ( AV8configFile.Exists() )
         {
            AV9ConnectionParameters.FromJSonFile(AV8configFile, null);
         }
         else
         {
            GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3 = AV9ConnectionParameters;
            new GeneXus.Programs.electrum.defaultparameters(context ).execute( out  GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3) ;
            AV9ConnectionParameters = GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3;
            AV8configFile.WriteAllText(AV9ConnectionParameters.ToJSonString(false), "");
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
         AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV10directory = new GxDirectory(context.GetPhysicalPath());
         AV8configFile = new GxFile(context.GetPhysicalPath());
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3 = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         /* GeneXus formulas. */
      }

      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private GxFile AV8configFile ;
      private GxDirectory AV10directory ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV9ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3 ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ;
   }

}
