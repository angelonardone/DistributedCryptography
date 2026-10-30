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
   public class opennotificationtarget : GXProcedure
   {
      public opennotificationtarget( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public opennotificationtarget( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( int aP0_index ,
                           out string aP1_kind ,
                           out Guid aP2_groupId )
      {
         this.AV12index = aP0_index;
         this.AV13kind = "" ;
         this.AV11groupId = Guid.Empty ;
         initialize();
         ExecuteImpl();
         aP1_kind=this.AV13kind;
         aP2_groupId=this.AV11groupId;
      }

      public Guid executeUdp( int aP0_index ,
                              out string aP1_kind )
      {
         execute(aP0_index, out aP1_kind, out aP2_groupId);
         return AV11groupId ;
      }

      public void executeSubmit( int aP0_index ,
                                 out string aP1_kind ,
                                 out Guid aP2_groupId )
      {
         this.AV12index = aP0_index;
         this.AV13kind = "" ;
         this.AV11groupId = Guid.Empty ;
         SubmitImpl();
         aP1_kind=this.AV13kind;
         aP2_groupId=this.AV11groupId;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13kind = "";
         AV11groupId = Guid.Empty;
         AV15targets.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "notifications.enc", out  AV10error), null);
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV15targets.Count )
         {
            AV14target = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV15targets.Item(AV20GXV1));
            if ( AV14target.gxTpr_Index == AV12index )
            {
               AV13kind = StringUtil.Trim( AV14target.gxTpr_Kind);
               AV11groupId = AV14target.gxTpr_Groupid;
               AV18contactId = AV14target.gxTpr_Contactid;
               AV19referenceGroupId = AV14target.gxTpr_Referencegroupid;
               if ( ! AV14target.gxTpr_Isread )
               {
                  AV14target.gxTpr_Isread = true;
                  AV17changed = true;
               }
               if (true) break;
            }
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         if ( AV17changed )
         {
            GXt_char1 = AV10error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "notifications.enc",  AV15targets.ToJSonString(false), out  GXt_char1) ;
            AV10error = GXt_char1;
         }
         if ( StringUtil.StrCmp(AV13kind, "group") == 0 )
         {
            new GeneXus.Programs.wallet.registered.setgroupedit(context ).execute(  AV11groupId,  AV19referenceGroupId) ;
         }
         if ( StringUtil.StrCmp(AV13kind, "chat") == 0 )
         {
            AV13kind = "";
            AV8allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV10error), null);
            AV21GXV2 = 1;
            while ( AV21GXV2 <= AV8allContacts.Count )
            {
               AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV8allContacts.Item(AV21GXV2));
               if ( AV9contact.gxTpr_Contactrid == AV18contactId )
               {
                  new GeneXus.Programs.wallet.setcontact(context ).execute(  AV9contact) ;
                  AV13kind = "chat";
                  if (true) break;
               }
               AV21GXV2 = (int)(AV21GXV2+1);
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
         AV13kind = "";
         AV11groupId = Guid.Empty;
         AV15targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         AV10error = "";
         AV14target = new GeneXus.Programs.wallet.registered.SdtNotificationTarget(context);
         AV18contactId = Guid.Empty;
         AV19referenceGroupId = Guid.Empty;
         GXt_char1 = "";
         AV8allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV12index ;
      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private string AV10error ;
      private string GXt_char1 ;
      private bool AV17changed ;
      private string AV13kind ;
      private Guid AV11groupId ;
      private Guid AV18contactId ;
      private Guid AV19referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV15targets ;
      private GeneXus.Programs.wallet.registered.SdtNotificationTarget AV14target ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV8allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private string aP1_kind ;
      private Guid aP2_groupId ;
   }

}
