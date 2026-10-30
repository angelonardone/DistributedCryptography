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
   public class receivemusig2room : GXProcedure
   {
      public receivemusig2room( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public receivemusig2room( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out Guid aP1_groupId ,
                           out Guid aP2_referenceGroupId ,
                           out string aP3_groupName ,
                           out string aP4_notice ,
                           out bool aP5_applied ,
                           out string aP6_error )
      {
         this.AV23sdt_message = aP0_sdt_message;
         this.AV15groupId = Guid.Empty ;
         this.AV22referenceGroupId = Guid.Empty ;
         this.AV16groupName = "" ;
         this.AV21notice = "" ;
         this.AV9applied = false ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_groupId=this.AV15groupId;
         aP2_referenceGroupId=this.AV22referenceGroupId;
         aP3_groupName=this.AV16groupName;
         aP4_notice=this.AV21notice;
         aP5_applied=this.AV9applied;
         aP6_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out Guid aP1_groupId ,
                                out Guid aP2_referenceGroupId ,
                                out string aP3_groupName ,
                                out string aP4_notice ,
                                out bool aP5_applied )
      {
         execute(aP0_sdt_message, out aP1_groupId, out aP2_referenceGroupId, out aP3_groupName, out aP4_notice, out aP5_applied, out aP6_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out Guid aP1_groupId ,
                                 out Guid aP2_referenceGroupId ,
                                 out string aP3_groupName ,
                                 out string aP4_notice ,
                                 out bool aP5_applied ,
                                 out string aP6_error )
      {
         this.AV23sdt_message = aP0_sdt_message;
         this.AV15groupId = Guid.Empty ;
         this.AV22referenceGroupId = Guid.Empty ;
         this.AV16groupName = "" ;
         this.AV21notice = "" ;
         this.AV9applied = false ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_groupId=this.AV15groupId;
         aP2_referenceGroupId=this.AV22referenceGroupId;
         aP3_groupName=this.AV16groupName;
         aP4_notice=this.AV21notice;
         aP5_applied=this.AV9applied;
         aP6_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9applied = false;
         AV18message.FromJSonString(AV23sdt_message.gxTpr_Message, null);
         AV22referenceGroupId = AV18message.gxTpr_Referencegroupid;
         GXt_SdtExternalUserPublic1 = AV11externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV11externalUserPublic = GXt_SdtExternalUserPublic1;
         if ( StringUtil.StrCmp(StringUtil.Trim( AV18message.gxTpr_Fromusername), StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
         {
            cleanup();
            if (true) return;
         }
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV10error), null);
         AV12found = false;
         AV26GXV1 = 1;
         while ( AV26GXV1 <= AV8all_groups_sdt.Count )
         {
            AV13group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV26GXV1));
            if ( ( AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid == AV18message.gxTpr_Referencegroupid ) && ( ( AV13group_sdt.gxTpr_Grouptype == 80 ) || ( AV13group_sdt.gxTpr_Grouptype == 90 ) ) && AV13group_sdt.gxTpr_Isactive )
            {
               AV12found = true;
               if (true) break;
            }
            AV26GXV1 = (int)(AV26GXV1+1);
         }
         if ( ! AV12found )
         {
            AV10error = "a signing room message for a group I do not have";
            cleanup();
            if (true) return;
         }
         AV15groupId = AV13group_sdt.gxTpr_Groupid;
         AV16groupName = StringUtil.Trim( AV13group_sdt.gxTpr_Groupname);
         AV24senderPubKey = "";
         AV27GXV2 = 1;
         while ( AV27GXV2 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV27GXV2));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Contactusername), StringUtil.Trim( AV18message.gxTpr_Fromusername)) == 0 )
            {
               AV24senderPubKey = StringUtil.Trim( AV14groupContact.gxTpr_Contactuserpubkey);
               if (true) break;
            }
            AV27GXV2 = (int)(AV27GXV2+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV24senderPubKey)) )
         {
            AV10error = "a signing room message from somebody who is not a member";
            cleanup();
            if (true) return;
         }
         AV25signed = StringUtil.Trim( AV18message.gxTpr_Id.ToString()) + StringUtil.Trim( AV18message.gxTpr_Referencegroupid.ToString()) + StringUtil.Trim( AV18message.gxTpr_Roomid.ToString()) + StringUtil.Trim( StringUtil.Str( (decimal)(AV18message.gxTpr_Datetimeunix), 18, 0)) + StringUtil.Trim( AV18message.gxTpr_Kind) + AV18message.gxTpr_Data;
         GXt_char2 = AV10error;
         new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  AV24senderPubKey,  AV25signed,  StringUtil.Trim( AV18message.gxTpr_Signature), out  AV17isOk, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) || ! AV17isOk )
         {
            AV10error = "a signing room message with a bad signature";
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.applymusig2roommessage(context ).execute(  AV13group_sdt,  AV18message, out  AV21notice, out  AV20mustSendNonces, out  AV19mustFinalize, out  AV10error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV9applied = true;
         if ( AV20mustSendNonces )
         {
            GXt_char2 = AV10error;
            new GeneXus.Programs.wallet.registered.musig2roomsendnonces(context ).execute(  AV15groupId,  AV18message.gxTpr_Roomid, out  GXt_char2) ;
            AV10error = GXt_char2;
         }
         if ( AV19mustFinalize )
         {
            GXt_char2 = AV10error;
            new GeneXus.Programs.wallet.registered.finalizemusig2room(context ).execute(  AV15groupId,  AV18message.gxTpr_Roomid, out  GXt_char2) ;
            AV10error = GXt_char2;
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
         AV15groupId = Guid.Empty;
         AV22referenceGroupId = Guid.Empty;
         AV16groupName = "";
         AV21notice = "";
         AV10error = "";
         AV18message = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage(context);
         AV11externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV24senderPubKey = "";
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV25signed = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV26GXV1 ;
      private int AV27GXV2 ;
      private string AV10error ;
      private string AV24senderPubKey ;
      private string GXt_char2 ;
      private bool AV9applied ;
      private bool AV12found ;
      private bool AV17isOk ;
      private bool AV20mustSendNonces ;
      private bool AV19mustFinalize ;
      private string AV25signed ;
      private string AV16groupName ;
      private string AV21notice ;
      private Guid AV15groupId ;
      private Guid AV22referenceGroupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV23sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage AV18message ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV11externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private Guid aP1_groupId ;
      private Guid aP2_referenceGroupId ;
      private string aP3_groupName ;
      private string aP4_notice ;
      private bool aP5_applied ;
      private string aP6_error ;
   }

}
