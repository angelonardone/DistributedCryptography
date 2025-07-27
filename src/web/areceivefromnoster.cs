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
namespace GeneXus.Programs {
   public class areceivefromnoster : GXProcedure
   {
      public static int Main( string[] args )
      {
         return new areceivefromnoster().MainImpl(args); ;
      }

      public int executeCmdLine( string[] args )
      {
         return ExecuteCmdLine(args); ;
      }

      protected override int ExecuteCmdLine( string[] args )
      {
         Guid aP0_key = new Guid()  ;
         string aP1_topic = new string(' ',0)  ;
         string aP2_message = new string(' ',0)  ;
         DateTime aP3_datetime = new DateTime()  ;
         if ( 0 < args.Length )
         {
            aP0_key=((Guid)(StringUtil.StrToGuid( (string)(args[0]))));
         }
         else
         {
            aP0_key=Guid.Empty;
         }
         if ( 1 < args.Length )
         {
            aP1_topic=((string)(args[1]));
         }
         else
         {
            aP1_topic="";
         }
         if ( 2 < args.Length )
         {
            aP2_message=((string)(args[2]));
         }
         else
         {
            aP2_message="";
         }
         if ( 3 < args.Length )
         {
            aP3_datetime=((DateTime)(context.localUtil.CToT( (string)(args[3]), 1)));
         }
         else
         {
            aP3_datetime=DateTime.MinValue;
         }
         execute(aP0_key, aP1_topic, aP2_message, aP3_datetime);
         return GX.GXRuntime.ExitCode ;
      }

      public areceivefromnoster( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public areceivefromnoster( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_key ,
                           string aP1_topic ,
                           string aP2_message ,
                           DateTime aP3_datetime )
      {
         this.AV10key = aP0_key;
         this.AV14topic = aP1_topic;
         this.AV11message = aP2_message;
         this.AV8datetime = aP3_datetime;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_key ,
                                 string aP1_topic ,
                                 string aP2_message ,
                                 DateTime aP3_datetime )
      {
         this.AV10key = aP0_key;
         this.AV14topic = aP1_topic;
         this.AV11message = aP2_message;
         this.AV8datetime = aP3_datetime;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12NotificationInfo.gxTpr_Id = AV14topic;
         AV12NotificationInfo.gxTpr_Message = AV11message;
         AV13ServerSocket.broadcast( AV12NotificationInfo);
         AV31queueDirectory.Source = "Comqueue";
         if ( ! AV31queueDirectory.Exists() )
         {
            AV31queueDirectory.Create();
         }
         GXt_int1 = AV29miliseconds;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int1) ;
         AV29miliseconds = GXt_int1;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV32queueFile.Source = AV31queueDirectory.GetAbsoluteName()+(GXt_boolean3 ? "/" : "\\")+StringUtil.Trim( StringUtil.Str( (decimal)(AV29miliseconds), 18, 0))+".queue";
         AV32queueFile.WriteAllText(AV12NotificationInfo.ToJSonString(false, true), "");
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
         AV12NotificationInfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
         AV13ServerSocket = new GeneXus.Core.genexus.server.SdtSocket(context);
         AV31queueDirectory = new GxDirectory(context.GetPhysicalPath());
         AV32queueFile = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private long AV29miliseconds ;
      private long GXt_int1 ;
      private string AV14topic ;
      private DateTime AV8datetime ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private string AV11message ;
      private Guid AV10key ;
      private GxFile AV32queueFile ;
      private GxDirectory AV31queueDirectory ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV12NotificationInfo ;
      private GeneXus.Core.genexus.server.SdtSocket AV13ServerSocket ;
   }

}
