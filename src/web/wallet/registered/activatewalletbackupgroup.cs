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
   public class activatewalletbackupgroup : GXProcedure
   {
      public activatewalletbackupgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public activatewalletbackupgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           Guid aP1_groupId ,
                           out string aP2_error )
      {
         this.AV17password = aP0_password;
         this.AV14groupId = aP1_groupId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_password ,
                                Guid aP1_groupId )
      {
         execute(aP0_password, aP1_groupId, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_password ,
                                 Guid aP1_groupId ,
                                 out string aP2_error )
      {
         this.AV17password = aP0_password;
         this.AV14groupId = aP1_groupId;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) || ! AV11group_sdt.gxTpr_Amigroupowner )
         {
            AV9error = "Only the owner of the group can activate it";
            cleanup();
            if (true) return;
         }
         if ( AV11group_sdt.gxTpr_Isactive )
         {
            AV9error = "The group is already active";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.registered.creategroupsharesfrommaster(context ).execute(  AV17password, ref  AV11group_sdt, out  GXt_char2) ;
         AV9error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            cleanup();
            if (true) return;
         }
         AV11group_sdt.gxTpr_Isactive = true;
         GXt_SdtExternalUserPublic3 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic3) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic3;
         AV12group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12group_sdt_temp.gxTpr_Groupname = AV11group_sdt.gxTpr_Groupname;
         AV12group_sdt_temp.gxTpr_Grouptype = AV11group_sdt.gxTpr_Grouptype;
         AV12group_sdt_temp.gxTpr_Minimumshares = AV11group_sdt.gxTpr_Minimumshares;
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
         AV16message_signature.gxTpr_Username = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV16message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV10externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char2 = AV9error;
         GXt_char4 = AV16message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey), out  GXt_char4, out  GXt_char2) ;
         AV16message_signature.gxTpr_Signature = GXt_char4;
         AV9error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "There was a problem Signing the invitation: " + AV9error;
            cleanup();
            if (true) return;
         }
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV16message_signature.gxTpr_Signature);
         AV19GXV1 = 1;
         while ( AV19GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV19GXV1));
            if ( (DateTime.MinValue==AV13groupContact.gxTpr_Contactinvitationsent) )
            {
               AV13groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
            }
            AV13groupContact.gxTpr_Contactinvisent = true;
            AV18sdt_message.gxTpr_Id = Guid.NewGuid( );
            GXt_int5 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
            AV18sdt_message.gxTpr_Datetimeunix = GXt_int5;
            AV18sdt_message.gxTpr_Messagetype = 90;
            AV18sdt_message.gxTpr_Message = AV12group_sdt_temp.ToJSonString(false, true);
            AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
            AV8contact.gxTpr_Username = StringUtil.Trim( AV13groupContact.gxTpr_Contactusername);
            AV8contact.gxTpr_Messagepubkey = StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey);
            GXt_char4 = AV9error;
            new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV8contact,  AV18sdt_message, out  GXt_char4) ;
            AV9error = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV9error = "There was a problem sending the Invitation to the Group: " + AV9error;
               cleanup();
               if (true) return;
            }
            AV19GXV1 = (int)(AV19GXV1+1);
         }
         GXt_char4 = AV9error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char4) ;
         AV9error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char4 = AV9error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV11group_sdt, out  GXt_char4) ;
            AV9error = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
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
         AV9error = "";
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic3 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char2 = "";
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV18sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV15grpupId = Guid.Empty;
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private int AV19GXV1 ;
      private long GXt_int5 ;
      private string AV17password ;
      private string AV9error ;
      private string GXt_char2 ;
      private string GXt_char4 ;
      private Guid AV14groupId ;
      private Guid AV15grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.nostr.SdtSDT_message AV18sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV8contact ;
      private string aP2_error ;
   }

}
