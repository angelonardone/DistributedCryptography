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
   public class getelectrumconfigfornetworktype : GXProcedure
   {
      public getelectrumconfigfornetworktype( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getelectrumconfigfornetworktype( IGxContext context )
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
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 = AV9ConnectionParameters;
         new GeneXus.Programs.electrum.getelectrumconfigservers(context ).execute( out  GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1) ;
         AV9ConnectionParameters = GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1;
         GXt_SdtWallet2 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV12wallet = GXt_SdtWallet2;
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
         AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11oneConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV9ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV13walletConnParameter ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV11oneConnParameter ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
   }

}
