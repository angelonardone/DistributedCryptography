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
   public class getserverbaseurl : GXProcedure
   {
      public getserverbaseurl( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getserverbaseurl( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_BaseUrl )
      {
         this.AV8BaseUrl = "" ;
         initialize();
         ExecuteImpl();
         aP0_BaseUrl=this.AV8BaseUrl;
      }

      public string executeUdp( )
      {
         execute(out aP0_BaseUrl);
         return AV8BaseUrl ;
      }

      public void executeSubmit( out string aP0_BaseUrl )
      {
         this.AV8BaseUrl = "" ;
         SubmitImpl();
         aP0_BaseUrl=this.AV8BaseUrl;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtServerParameters1 = AV10ServerParameters;
         new GeneXus.Programs.distcrypt.getserverparameters(context ).execute( out  GXt_SdtServerParameters1) ;
         AV10ServerParameters = GXt_SdtServerParameters1;
         if ( AV10ServerParameters.gxTpr_Serversecure )
         {
            AV8BaseUrl = "https://";
            AV9defaultPort = 443;
         }
         else
         {
            AV8BaseUrl = "http://";
            AV9defaultPort = 80;
         }
         AV8BaseUrl += StringUtil.Trim( AV10ServerParameters.gxTpr_Serverhost);
         if ( ( AV10ServerParameters.gxTpr_Serverport > 0 ) && ( AV10ServerParameters.gxTpr_Serverport != AV9defaultPort ) )
         {
            AV8BaseUrl += ":" + StringUtil.Trim( StringUtil.Str( (decimal)(AV10ServerParameters.gxTpr_Serverport), 6, 0));
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
         AV8BaseUrl = "";
         AV10ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         GXt_SdtServerParameters1 = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         /* GeneXus formulas. */
      }

      private int AV9defaultPort ;
      private string AV8BaseUrl ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV10ServerParameters ;
      private GeneXus.Programs.distcrypt.SdtServerParameters GXt_SdtServerParameters1 ;
      private string aP0_BaseUrl ;
   }

}
