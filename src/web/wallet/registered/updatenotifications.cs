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
namespace GeneXus.Programs.wallet.registered {
   public class updatenotifications : GXProcedure
   {
      public updatenotifications( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updatenotifications( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_action ,
                           out string aP1_error )
      {
         this.AV8action = aP0_action;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( string aP0_action )
      {
         execute(aP0_action, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_action ,
                                 out string aP1_error )
      {
         this.AV8action = aP0_action;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11targets.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "notifications.enc", out  AV9error), null);
         AV12kept = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV11targets.Count )
         {
            AV10target = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV13GXV1));
            if ( StringUtil.StrCmp(AV8action, "markAllRead") == 0 )
            {
               AV10target.gxTpr_Isread = true;
               AV12kept.Add(AV10target, 0);
            }
            else if ( StringUtil.StrCmp(AV8action, "deleteRead") == 0 )
            {
               if ( ! AV10target.gxTpr_Isread )
               {
                  AV12kept.Add(AV10target, 0);
               }
            }
            else
            {
               AV12kept.Add(AV10target, 0);
            }
            AV13GXV1 = (int)(AV13GXV1+1);
         }
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "notifications.enc",  AV12kept.ToJSonString(false), out  GXt_char1) ;
         AV9error = GXt_char1;
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
         AV9error = "";
         AV11targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV12kept = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV10target = new GeneXus.Programs.wallet.registered.SdtNotificationTarget(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV9error ;
      private string GXt_char1 ;
      private string AV8action ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV11targets ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV12kept ;
      private GeneXus.Programs.wallet.registered.SdtNotificationTarget AV10target ;
      private string aP1_error ;
   }

}
