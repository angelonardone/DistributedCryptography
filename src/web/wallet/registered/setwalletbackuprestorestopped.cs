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
   public class setwalletbackuprestorestopped : GXProcedure
   {
      public setwalletbackuprestorestopped( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setwalletbackuprestorestopped( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           bool aP1_stop ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV18stop = aP1_stop;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                bool aP1_stop )
      {
         execute(aP0_groupId, aP1_stop, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 bool aP1_stop ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV18stop = aP1_stop;
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
            AV9error = "Only the owner of the group can stop or allow the restore";
            cleanup();
            if (true) return;
         }
         AV11group_sdt.gxTpr_Restorestopped = AV18stop;
         AV11group_sdt.gxTpr_Restorestoppeddatetime = DateTimeUtil.Now( context);
         if ( ! AV18stop )
         {
            AV19GXV1 = 1;
            while ( AV19GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
            {
               AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV19GXV1));
               AV13groupContact.gxTpr_Restoresigneddatetime = (DateTime)(DateTime.MinValue);
               AV13groupContact.gxTpr_Numofsharesreached = false;
               AV19GXV1 = (int)(AV19GXV1+1);
            }
         }
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char2) ;
         AV9error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV11group_sdt, out  GXt_char2) ;
            AV9error = GXt_char2;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic3 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic3) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic3;
         AV12group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12group_sdt_temp.gxTpr_Groupname = AV11group_sdt.gxTpr_Groupname;
         AV12group_sdt_temp.gxTpr_Grouptype = AV11group_sdt.gxTpr_Grouptype;
         AV12group_sdt_temp.gxTpr_Restorestopped = AV11group_sdt.gxTpr_Restorestopped;
         AV12group_sdt_temp.gxTpr_Restorestoppeddatetime = AV11group_sdt.gxTpr_Restorestoppeddatetime;
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV16message_signature.gxTpr_Username = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV16message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV10externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char2 = AV9error;
         GXt_char4 = AV16message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey), out  GXt_char4, out  GXt_char2) ;
         AV16message_signature.gxTpr_Signature = GXt_char4;
         AV9error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "There was a problem Signing the message: " + AV9error;
            cleanup();
            if (true) return;
         }
         AV12group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV16message_signature.gxTpr_Signature);
         AV20GXV2 = 1;
         while ( AV20GXV2 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV20GXV2));
            AV17sdt_message.gxTpr_Id = Guid.NewGuid( );
            GXt_int5 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
            AV17sdt_message.gxTpr_Datetimeunix = GXt_int5;
            AV17sdt_message.gxTpr_Messagetype = 100;
            AV17sdt_message.gxTpr_Message = AV12group_sdt_temp.ToJSonString(false, true);
            AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
            AV8contact.gxTpr_Username = StringUtil.Trim( AV13groupContact.gxTpr_Contactusername);
            AV8contact.gxTpr_Messagepubkey = StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey);
            GXt_char4 = AV9error;
            new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV8contact,  AV17sdt_message, out  GXt_char4) ;
            AV9error = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV9error = "There was a problem notifying the group members: " + AV9error;
               cleanup();
               if (true) return;
            }
            AV20GXV2 = (int)(AV20GXV2+1);
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
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15grpupId = Guid.Empty;
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic3 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char2 = "";
         AV17sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private int AV19GXV1 ;
      private int AV20GXV2 ;
      private long GXt_int5 ;
      private string AV9error ;
      private string GXt_char2 ;
      private string GXt_char4 ;
      private bool AV18stop ;
      private Guid AV14groupId ;
      private Guid AV15grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV17sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV8contact ;
      private string aP2_error ;
   }

}
