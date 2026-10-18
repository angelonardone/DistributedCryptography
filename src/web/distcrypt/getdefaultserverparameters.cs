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
namespace GeneXus.Programs.distcrypt {
   public class getdefaultserverparameters : GXProcedure
   {
      public getdefaultserverparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getdefaultserverparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters )
      {
         this.AV9ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context) ;
         initialize();
         ExecuteImpl();
         aP0_ServerParameters=this.AV9ServerParameters;
      }

      public GeneXus.Programs.distcrypt.SdtServerParameters executeUdp( )
      {
         execute(out aP0_ServerParameters);
         return AV9ServerParameters ;
      }

      public void executeSubmit( out GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters )
      {
         this.AV9ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context) ;
         SubmitImpl();
         aP0_ServerParameters=this.AV9ServerParameters;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV10wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV10wallet = GXt_SdtWallet1;
         AV9ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         if ( StringUtil.StrCmp(AV10wallet.gxTpr_Networktype, "MainNet") == 0 )
         {
            AV9ServerParameters.gxTpr_Serverhost = "auth.distributedcryptography.com";
         }
         else
         {
            AV9ServerParameters.gxTpr_Serverhost = "auth-testnet.distributedcryptography.com";
         }
         AV9ServerParameters.gxTpr_Serverport = 80;
         AV9ServerParameters.gxTpr_Serversecure = false;
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
         AV9ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         /* GeneXus formulas. */
      }

      private GeneXus.Programs.distcrypt.SdtServerParameters AV9ServerParameters ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters ;
   }

}
