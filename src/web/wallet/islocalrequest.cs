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
using GeneXus.Http.Server;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet {
   public class islocalrequest : GXProcedure
   {
      public islocalrequest( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public islocalrequest( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out bool aP0_isLocal )
      {
         this.AV9isLocal = false ;
         initialize();
         ExecuteImpl();
         aP0_isLocal=this.AV9isLocal;
      }

      public bool executeUdp( )
      {
         execute(out aP0_isLocal);
         return AV9isLocal ;
      }

      public void executeSubmit( out bool aP0_isLocal )
      {
         this.AV9isLocal = false ;
         SubmitImpl();
         aP0_isLocal=this.AV9isLocal;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9isLocal = false;
         if ( GxRegex.IsMatch(AV8httpRequest.BaseURL,"\\/\\/localhost:") )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
                 System.Net.IPAddress gxRemote = (context.HttpContext != null) ? context.HttpContext.Connection.RemoteIpAddress : null;
            /* User Code */
                 if (gxRemote != null && gxRemote.IsIPv4MappedToIPv6) gxRemote = gxRemote.MapToIPv4();
            /* User Code */
                 AV9isLocal = (gxRemote != null && System.Net.IPAddress.IsLoopback(gxRemote));
            /* User Code */
                 if (System.Environment.GetEnvironmentVariable("DISTCRYPT_TRUST_LOCALHOST_NAME") == "1") AV9isLocal = true;
            /* User Code */
             }
            /* User Code */
             catch (Exception)
            /* User Code */
             {
            /* User Code */
                 AV9isLocal = false;
            /* User Code */
             }
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
         AV8httpRequest = new GxHttpRequest( context);
         /* GeneXus formulas. */
      }

      private bool AV9isLocal ;
      private GxHttpRequest AV8httpRequest ;
      private bool aP0_isLocal ;
   }

}
