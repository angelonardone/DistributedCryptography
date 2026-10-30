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
   public class receivegroupchat : GXProcedure
   {
      public receivegroupchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public receivegroupchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out Guid aP1_groupId ,
                           out Guid aP2_referenceGroupId ,
                           out string aP3_groupName ,
                           out bool aP4_added ,
                           out string aP5_error )
      {
         this.AV20sdt_message = aP0_sdt_message;
         this.AV15groupId = Guid.Empty ;
         this.AV19referenceGroupId = Guid.Empty ;
         this.AV16groupName = "" ;
         this.AV8added = false ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_groupId=this.AV15groupId;
         aP2_referenceGroupId=this.AV19referenceGroupId;
         aP3_groupName=this.AV16groupName;
         aP4_added=this.AV8added;
         aP5_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out Guid aP1_groupId ,
                                out Guid aP2_referenceGroupId ,
                                out string aP3_groupName ,
                                out bool aP4_added )
      {
         execute(aP0_sdt_message, out aP1_groupId, out aP2_referenceGroupId, out aP3_groupName, out aP4_added, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out Guid aP1_groupId ,
                                 out Guid aP2_referenceGroupId ,
                                 out string aP3_groupName ,
                                 out bool aP4_added ,
                                 out string aP5_error )
      {
         this.AV20sdt_message = aP0_sdt_message;
         this.AV15groupId = Guid.Empty ;
         this.AV19referenceGroupId = Guid.Empty ;
         this.AV16groupName = "" ;
         this.AV8added = false ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_groupId=this.AV15groupId;
         aP2_referenceGroupId=this.AV19referenceGroupId;
         aP3_groupName=this.AV16groupName;
         aP4_added=this.AV8added;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8added = false;
         AV18message.FromJSonString(AV20sdt_message.gxTpr_Message, null);
         AV19referenceGroupId = AV18message.gxTpr_Referencegroupid;
         GXt_SdtExternalUserPublic1 = AV11externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV11externalUserPublic = GXt_SdtExternalUserPublic1;
         if ( StringUtil.StrCmp(StringUtil.Trim( AV18message.gxTpr_Fromusername), StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
         {
            cleanup();
            if (true) return;
         }
         AV9all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV10error), null);
         AV12found = false;
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV9all_groups_sdt.Count )
         {
            AV13group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV9all_groups_sdt.Item(AV23GXV1));
            if ( ( AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid == AV18message.gxTpr_Referencegroupid ) && ( AV13group_sdt.gxTpr_Grouptype == 70 ) && AV13group_sdt.gxTpr_Isactive )
            {
               AV12found = true;
               if (true) break;
            }
            AV23GXV1 = (int)(AV23GXV1+1);
         }
         if ( ! AV12found )
         {
            AV10error = "a group chat message for a group I do not have";
            cleanup();
            if (true) return;
         }
         AV15groupId = AV13group_sdt.gxTpr_Groupid;
         AV16groupName = StringUtil.Trim( AV13group_sdt.gxTpr_Groupname);
         AV21senderPubKey = "";
         AV24GXV2 = 1;
         while ( AV24GXV2 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV24GXV2));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Contactusername), StringUtil.Trim( AV18message.gxTpr_Fromusername)) == 0 )
            {
               AV21senderPubKey = StringUtil.Trim( AV14groupContact.gxTpr_Contactuserpubkey);
               if (true) break;
            }
            AV24GXV2 = (int)(AV24GXV2+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV21senderPubKey)) )
         {
            AV10error = "a group chat message from somebody who is not a member";
            cleanup();
            if (true) return;
         }
         AV22signed = StringUtil.Trim( AV18message.gxTpr_Id.ToString()) + StringUtil.Trim( AV18message.gxTpr_Referencegroupid.ToString()) + StringUtil.Trim( StringUtil.Str( (decimal)(AV18message.gxTpr_Datetimeunix), 18, 0)) + StringUtil.Trim( AV18message.gxTpr_Text);
         GXt_char2 = AV10error;
         new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  AV21senderPubKey,  AV22signed,  StringUtil.Trim( AV18message.gxTpr_Signature), out  AV17isOk, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) || ! AV17isOk )
         {
            AV10error = "a group chat message with a bad signature";
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.appendgroupchat(context ).execute(  AV18message.gxTpr_Referencegroupid,  AV18message, out  AV8added) ;
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
         AV15groupId = Guid.Empty;
         AV19referenceGroupId = Guid.Empty;
         AV16groupName = "";
         AV10error = "";
         AV18message = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         AV11externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV9all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21senderPubKey = "";
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV22signed = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private string AV10error ;
      private string GXt_char2 ;
      private bool AV8added ;
      private bool AV12found ;
      private bool AV17isOk ;
      private string AV22signed ;
      private string AV16groupName ;
      private string AV21senderPubKey ;
      private Guid AV15groupId ;
      private Guid AV19referenceGroupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV20sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV18message ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV11externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV9all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private Guid aP1_groupId ;
      private Guid aP2_referenceGroupId ;
      private string aP3_groupName ;
      private bool aP4_added ;
      private string aP5_error ;
   }

}
