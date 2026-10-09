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
   public class adownloadfile : GXWebProcedure
   {
      public override void webExecute( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         initialize();
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "token");
            if ( ! entryPointCalled )
            {
               AV13token = gxfirstwebparm;
            }
         }
         if ( GxWebError == 0 )
         {
            ExecutePrivate();
         }
         cleanup();
      }

      public adownloadfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public adownloadfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_token )
      {
         this.AV13token = aP0_token;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_token )
      {
         this.AV13token = aP0_token;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12path = AV14webSession.Get("DL."+StringUtil.Trim( AV13token));
         AV11name = AV14webSession.Get("DLN."+StringUtil.Trim( AV13token));
         AV14webSession.Remove("DL."+StringUtil.Trim( AV13token));
         AV14webSession.Remove("DLN."+StringUtil.Trim( AV13token));
         AV9file.Source = AV12path;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV13token))) || String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12path))) || ! AV9file.Exists() )
         {
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("Content-Type", "text/plain");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("Cache-Control", "no-store");
            }
            AV10HttpResponse.AddString("This download is not available (it can be used only once, from the same session).");
         }
         else
         {
            AV8disposition = "attachment; filename=" + StringUtil.Trim( AV11name);
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("Content-Type", "application/octet-stream");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("Content-Disposition", AV8disposition);
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("Cache-Control", "no-store");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10HttpResponse.AppendHeader("X-Content-Type-Options", "nosniff");
            }
            AV10HttpResponse.AddFile(AV12path);
            /* User Code */
             try { System.IO.File.Delete(AV12path.Trim()); } catch { }
         }
         if ( context.WillRedirect( ) )
         {
            context.Redirect( context.wjLoc );
            context.wjLoc = "";
         }
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         base.cleanup();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         GXKey = "";
         gxfirstwebparm = "";
         AV12path = "";
         AV14webSession = context.GetSession();
         AV11name = "";
         AV9file = new GxFile(context.GetPhysicalPath());
         AV10HttpResponse = new GxHttpResponse( context);
         AV8disposition = "";
         /* GeneXus formulas. */
      }

      private short gxcookieaux ;
      private short nGotPars ;
      private short GxWebError ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private string AV13token ;
      private string AV12path ;
      private string AV11name ;
      private string AV8disposition ;
      private IGxSession AV14webSession ;
      private GxHttpResponse AV10HttpResponse ;
      private GxFile AV9file ;
   }

}
