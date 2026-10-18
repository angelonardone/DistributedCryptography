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
         GXt_SdtWallet1 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV12wallet = GXt_SdtWallet1;
         if ( new GeneXus.Programs.wallet.iswalletopen(context).executeUdp( ) )
         {
            GXt_char2 = AV15stored;
            new GeneXus.Programs.wallet.readjsonencfile(context ).execute(  "electrum.conf", out  AV14error, out  GXt_char2) ;
            AV15stored = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV15stored)) )
            {
               AV13walletConnParameter.FromJSonString(AV15stored, null);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13walletConnParameter.gxTpr_Hostname)) || ( StringUtil.StrCmp(AV13walletConnParameter.gxTpr_Networktype, AV12wallet.gxTpr_Networktype) != 0 ) )
         {
            GXt_SdtConnectionParameters_ConnectionParametersItem3 = AV13walletConnParameter;
            new GeneXus.Programs.electrum.getdefaultelectrumconfig(context ).execute( out  GXt_SdtConnectionParameters_ConnectionParametersItem3) ;
            AV13walletConnParameter = GXt_SdtConnectionParameters_ConnectionParametersItem3;
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
         AV13walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV15stored = "";
         GXt_char2 = "";
         AV14error = "";
         GXt_SdtConnectionParameters_ConnectionParametersItem3 = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string AV14error ;
      private string AV15stored ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV13walletConnParameter ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem GXt_SdtConnectionParameters_ConnectionParametersItem3 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ;
   }

}
