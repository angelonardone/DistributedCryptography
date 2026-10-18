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
   public class getdefaultelectrumconfig : GXProcedure
   {
      public getdefaultelectrumconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getdefaultelectrumconfig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter )
      {
         this.AV11walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context) ;
         initialize();
         ExecuteImpl();
         aP0_walletConnParameter=this.AV11walletConnParameter;
      }

      public GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem executeUdp( )
      {
         execute(out aP0_walletConnParameter);
         return AV11walletConnParameter ;
      }

      public void executeSubmit( out GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter )
      {
         this.AV11walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context) ;
         SubmitImpl();
         aP0_walletConnParameter=this.AV11walletConnParameter;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV10wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV10wallet = GXt_SdtWallet1;
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem2 = AV8ConnectionParameters;
         new GeneXus.Programs.electrum.defaultparameters(context ).execute( out  GXt_objcol_SdtConnectionParameters_ConnectionParametersItem2) ;
         AV8ConnectionParameters = GXt_objcol_SdtConnectionParameters_ConnectionParametersItem2;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV8ConnectionParameters.Count )
         {
            AV9oneConnParameter = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV8ConnectionParameters.Item(AV12GXV1));
            if ( StringUtil.StrCmp(AV9oneConnParameter.gxTpr_Networktype, AV10wallet.gxTpr_Networktype) == 0 )
            {
               AV11walletConnParameter = (GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)(AV9oneConnParameter.Clone());
               if (true) break;
            }
            AV12GXV1 = (int)(AV12GXV1+1);
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
         AV11walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem2 = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV9oneConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private int AV12GXV1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV11walletConnParameter ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV8ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> GXt_objcol_SdtConnectionParameters_ConnectionParametersItem2 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV9oneConnParameter ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ;
   }

}
