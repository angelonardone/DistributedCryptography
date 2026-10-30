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
   public class addnotificationtarget : GXProcedure
   {
      public addnotificationtarget( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public addnotificationtarget( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_kind ,
                           Guid aP1_contactId ,
                           Guid aP2_groupId ,
                           Guid aP3_referenceGroupId ,
                           string aP4_toastType ,
                           string aP5_title ,
                           string aP6_text ,
                           out int aP7_index )
      {
         this.AV11kind = aP0_kind;
         this.AV8contactId = aP1_contactId;
         this.AV9groupId = aP2_groupId;
         this.AV20referenceGroupId = aP3_referenceGroupId;
         this.AV19toastType = aP4_toastType;
         this.AV18title = aP5_title;
         this.AV17text = aP6_text;
         this.AV10index = 0 ;
         initialize();
         ExecuteImpl();
         aP7_index=this.AV10index;
      }

      public int executeUdp( string aP0_kind ,
                             Guid aP1_contactId ,
                             Guid aP2_groupId ,
                             Guid aP3_referenceGroupId ,
                             string aP4_toastType ,
                             string aP5_title ,
                             string aP6_text )
      {
         execute(aP0_kind, aP1_contactId, aP2_groupId, aP3_referenceGroupId, aP4_toastType, aP5_title, aP6_text, out aP7_index);
         return AV10index ;
      }

      public void executeSubmit( string aP0_kind ,
                                 Guid aP1_contactId ,
                                 Guid aP2_groupId ,
                                 Guid aP3_referenceGroupId ,
                                 string aP4_toastType ,
                                 string aP5_title ,
                                 string aP6_text ,
                                 out int aP7_index )
      {
         this.AV11kind = aP0_kind;
         this.AV8contactId = aP1_contactId;
         this.AV9groupId = aP2_groupId;
         this.AV20referenceGroupId = aP3_referenceGroupId;
         this.AV19toastType = aP4_toastType;
         this.AV18title = aP5_title;
         this.AV17text = aP6_text;
         this.AV10index = 0 ;
         SubmitImpl();
         aP7_index=this.AV10index;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13targets.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "notifications.enc", out  AV16error), null);
         AV10index = 0;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV13targets.Count )
         {
            AV12target = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV13targets.Item(AV21GXV1));
            if ( AV12target.gxTpr_Index > AV10index )
            {
               AV10index = AV12target.gxTpr_Index;
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         AV10index = (int)(AV10index+1);
         AV12target = new GeneXus.Programs.wallet.registered.SdtNotificationTarget(context);
         AV12target.gxTpr_Index = AV10index;
         AV12target.gxTpr_Kind = StringUtil.Trim( AV11kind);
         AV12target.gxTpr_Contactid = AV8contactId;
         AV12target.gxTpr_Groupid = AV9groupId;
         AV12target.gxTpr_Referencegroupid = AV20referenceGroupId;
         AV12target.gxTpr_Datetime = DateTimeUtil.Now( context);
         AV12target.gxTpr_Toasttype = AV19toastType;
         AV12target.gxTpr_Title = AV18title;
         AV12target.gxTpr_Text = AV17text;
         AV12target.gxTpr_Isread = false;
         AV13targets.Add(AV12target, 0);
         while ( AV13targets.Count > 200 )
         {
            AV13targets.RemoveItem(1);
         }
         GXt_char1 = AV16error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "notifications.enc",  AV13targets.ToJSonString(false), out  GXt_char1) ;
         AV16error = GXt_char1;
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
         AV13targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV16error = "";
         AV12target = new GeneXus.Programs.wallet.registered.SdtNotificationTarget(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV10index ;
      private int AV21GXV1 ;
      private string AV18title ;
      private string AV17text ;
      private string AV16error ;
      private string GXt_char1 ;
      private string AV11kind ;
      private string AV19toastType ;
      private Guid AV8contactId ;
      private Guid AV9groupId ;
      private Guid AV20referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV13targets ;
      private GeneXus.Programs.wallet.registered.SdtNotificationTarget AV12target ;
      private int aP7_index ;
   }

}
