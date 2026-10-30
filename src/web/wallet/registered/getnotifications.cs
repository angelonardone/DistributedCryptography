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
   public class getnotifications : GXProcedure
   {
      public getnotifications( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getnotifications( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> aP0_targets ,
                           out short aP1_unread )
      {
         this.AV10targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography") ;
         this.AV11unread = 0 ;
         initialize();
         ExecuteImpl();
         aP0_targets=this.AV10targets;
         aP1_unread=this.AV11unread;
      }

      public short executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> aP0_targets )
      {
         execute(out aP0_targets, out aP1_unread);
         return AV11unread ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> aP0_targets ,
                                 out short aP1_unread )
      {
         this.AV10targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography") ;
         this.AV11unread = 0 ;
         SubmitImpl();
         aP0_targets=this.AV10targets;
         aP1_unread=this.AV11unread;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10targets.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "notifications.enc", out  AV8error), null);
         AV11unread = 0;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV10targets.Count )
         {
            AV9target = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV10targets.Item(AV12GXV1));
            if ( ! AV9target.gxTpr_Isread )
            {
               AV11unread = (short)(AV11unread+1);
            }
            AV12GXV1 = (int)(AV12GXV1+1);
         }
         AV10targets.Sort("[index]");
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
         AV10targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV8error = "";
         AV9target = new GeneXus.Programs.wallet.registered.SdtNotificationTarget(context);
         /* GeneXus formulas. */
      }

      private short AV11unread ;
      private int AV12GXV1 ;
      private string AV8error ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV10targets ;
      private GeneXus.Programs.wallet.registered.SdtNotificationTarget AV9target ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> aP0_targets ;
      private short aP1_unread ;
   }

}
