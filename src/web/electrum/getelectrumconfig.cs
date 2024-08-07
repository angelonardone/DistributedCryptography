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
   public class getelectrumconfig : GXProcedure
   {
      public getelectrumconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getelectrumconfig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter )
      {
         this.AV13walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context) ;
         initialize();
         ExecuteImpl();
         aP0_walletConnParameter=this.AV13walletConnParameter;
      }

      public GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem executeUdp( )
      {
         execute(out aP0_walletConnParameter);
         return AV13walletConnParameter ;
      }

      public void executeSubmit( out GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter )
      {
         this.AV13walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context) ;
         SubmitImpl();
         aP0_walletConnParameter=this.AV13walletConnParameter;
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
         GXt_SdtWallet4 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet4) ;
         AV12wallet = GXt_SdtWallet4;
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV9ConnectionParameters.Count )
         {
            AV11oneConnParameter = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV9ConnectionParameters.Item(AV14GXV1));
            if ( StringUtil.StrCmp(AV11oneConnParameter.gxTpr_Networktype, AV12wallet.gxTpr_Networktype) == 0 )
            {
               AV13walletConnParameter = (GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)(AV11oneConnParameter.Clone());
               if (true) break;
            }
            AV14GXV1 = (int)(AV14GXV1+1);
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
         AV13walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV10directory = new GxDirectory(context.GetPhysicalPath());
         AV8configFile = new GxFile(context.GetPhysicalPath());
         AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3 = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet4 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11oneConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV9ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> GXt_objcol_SdtConnectionParameters_ConnectionParametersItem3 ;
      private GxFile AV8configFile ;
      private GxDirectory AV10directory ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV13walletConnParameter ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV11oneConnParameter ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet4 ;
   }

}
