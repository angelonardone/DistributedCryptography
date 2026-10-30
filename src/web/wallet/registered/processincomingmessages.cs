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
   public class processincomingmessages : GXProcedure
   {
      public processincomingmessages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public processincomingmessages( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_isChatScreen ,
                           bool aP1_isGroupScreen ,
                           ref Guid aP2_lastMessageId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP3_notifications ,
                           out bool aP4_refreshChat )
      {
         this.AV15isChatScreen = aP0_isChatScreen;
         this.AV50isGroupScreen = aP1_isGroupScreen;
         this.AV19lastMessageId = aP2_lastMessageId;
         this.AV23notifications = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification>( context, "IncomingNotification", "distributedcryptography") ;
         this.AV31refreshChat = false ;
         initialize();
         ExecuteImpl();
         aP2_lastMessageId=this.AV19lastMessageId;
         aP3_notifications=this.AV23notifications;
         aP4_refreshChat=this.AV31refreshChat;
      }

      public bool executeUdp( bool aP0_isChatScreen ,
                              bool aP1_isGroupScreen ,
                              ref Guid aP2_lastMessageId ,
                              out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP3_notifications )
      {
         execute(aP0_isChatScreen, aP1_isGroupScreen, ref aP2_lastMessageId, out aP3_notifications, out aP4_refreshChat);
         return AV31refreshChat ;
      }

      public void executeSubmit( bool aP0_isChatScreen ,
                                 bool aP1_isGroupScreen ,
                                 ref Guid aP2_lastMessageId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP3_notifications ,
                                 out bool aP4_refreshChat )
      {
         this.AV15isChatScreen = aP0_isChatScreen;
         this.AV50isGroupScreen = aP1_isGroupScreen;
         this.AV19lastMessageId = aP2_lastMessageId;
         this.AV23notifications = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification>( context, "IncomingNotification", "distributedcryptography") ;
         this.AV31refreshChat = false ;
         SubmitImpl();
         aP2_lastMessageId=this.AV19lastMessageId;
         aP3_notifications=this.AV23notifications;
         aP4_refreshChat=this.AV31refreshChat;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV23notifications.Clear();
         AV31refreshChat = false;
         GXt_SdtExternalUser1 = AV14externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV14externalUser = GXt_SdtExternalUser1;
         GXt_guid2 = AV42connectionId;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid2) ;
         AV42connectionId = GXt_guid2;
         AV43queueCleaned = AV44websession.Get("COMQUEUE_CLEANED");
         if ( ! (Guid.Empty==AV42connectionId) && String.IsNullOrEmpty(StringUtil.RTrim( AV43queueCleaned)) )
         {
            new GeneXus.Programs.nostr.cleanqueue(context ).execute(  AV42connectionId,  false) ;
            AV44websession.Set("COMQUEUE_CLEANED", "1");
         }
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         GXt_boolean4 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
         AV28queueDirectory.Source = "Comqueue"+(GXt_boolean4 ? "/" : "\\")+StringUtil.Trim( AV42connectionId.ToString());
         GXt_char5 = AV13error;
         new GeneXus.Programs.wallet.registered.getmessages(context ).execute( out  AV21messages, out  GXt_char5) ;
         AV13error = GXt_char5;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV59GXV1 = 1;
            while ( AV59GXV1 <= AV21messages.gxTpr_Message.Count )
            {
               AV20message = ((SdtDesktopApp_services_SDT_Messages_Message_MessageItem)AV21messages.gxTpr_Message.Item(AV59GXV1));
               AV18json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
               AV18json_enc.gxTpr_Encryptedkey = AV20message.gxTpr_Messageencryptedkey;
               AV18json_enc.gxTpr_Encryptedtext = AV20message.gxTpr_Messageencrypted;
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S131 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV59GXV1 = (int)(AV59GXV1+1);
            }
         }
         else
         {
            AV26nType = "error";
            AV25nTitle = "getMessages: ";
            AV24nText = AV13error;
            /* Execute user subroutine: 'NOTIFY' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV20message = new SdtDesktopApp_services_SDT_Messages_Message_MessageItem(context);
         AV58queuedEvents.Clear();
         if ( AV28queueDirectory.Exists() )
         {
            AV61GXV3 = 1;
            AV60GXV2 = AV28queueDirectory.GetFiles(".queue");
            while ( AV61GXV3 <= AV60GXV2.ItemCount )
            {
               AV29queueFile = AV60GXV2.Item(AV61GXV3);
               AV22notificationInfo.FromJSonFile(AV29queueFile, null);
               AV29queueFile.Delete();
               new GeneXus.Programs.nostr.processrecivedfromnostr(context ).execute(  AV22notificationInfo, out  AV30recFromNostr) ;
               AV57queuedEvent = new GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent(context);
               AV57queuedEvent.gxTpr_Createdat = AV30recFromNostr.gxTpr_Event.gxTpr_Created_at;
               AV57queuedEvent.gxTpr_Notification = AV22notificationInfo.ToJSonString(false, true);
               AV58queuedEvents.Add(AV57queuedEvent, 0);
               AV61GXV3 = (int)(AV61GXV3+1);
            }
         }
         AV58queuedEvents.Sort("createdAt");
         AV62GXV4 = 1;
         while ( AV62GXV4 <= AV58queuedEvents.Count )
         {
            AV57queuedEvent = ((GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent)AV58queuedEvents.Item(AV62GXV4));
            AV22notificationInfo.FromJSonString(AV57queuedEvent.gxTpr_Notification, null);
            new GeneXus.Programs.nostr.processrecivedfromnostr(context ).execute(  AV22notificationInfo, out  AV30recFromNostr) ;
            if ( ( StringUtil.StrCmp(AV30recFromNostr.gxTpr_Responsetype, "EVENT") == 0 ) && ( ( AV30recFromNostr.gxTpr_Event.gxTpr_Kind == 4 ) || ( AV30recFromNostr.gxTpr_Event.gxTpr_Kind == 20004 ) ) )
            {
               GXt_char5 = AV12encText;
               new GeneXus.Programs.distcrypt.hextotext(context ).execute(  StringUtil.Trim( AV30recFromNostr.gxTpr_Event.gxTpr_Content), out  GXt_char5) ;
               AV12encText = GXt_char5;
               AV18json_enc.FromJSonString(AV12encText, null);
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S131 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
            }
            else
            {
               if ( StringUtil.StrCmp(AV30recFromNostr.gxTpr_Responsetype, "EOSE") == 0 )
               {
               }
               else
               {
                  if ( StringUtil.StrCmp(AV30recFromNostr.gxTpr_Responsetype, "OK") == 0 )
                  {
                  }
                  else
                  {
                     AV26nType = "error";
                     AV25nTitle = "Nostr response: ";
                     AV24nText = AV30recFromNostr.ToJSonString(false, true);
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if ( returnInSub )
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
               }
            }
            AV62GXV4 = (int)(AV62GXV4+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'NOTIFY' Routine */
         returnInSub = false;
         AV27oneNotification = new GeneXus.Programs.wallet.registered.SdtIncomingNotification(context);
         AV27oneNotification.gxTpr_Toasttype = AV26nType;
         AV27oneNotification.gxTpr_Title = AV25nTitle;
         AV27oneNotification.gxTpr_Text = AV24nText;
         AV27oneNotification.gxTpr_Sticky = true;
         GXt_int6 = AV39nIndex;
         new GeneXus.Programs.wallet.registered.addnotificationtarget(context ).execute(  AV40nKind,  AV37nContactId,  AV38nGroupId,  AV41nReferenceGroupId,  AV26nType,  AV25nTitle,  AV24nText, out  GXt_int6) ;
         AV39nIndex = GXt_int6;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV40nKind)) )
         {
            AV27oneNotification.gxTpr_Url = formatLink("wallet.registered.gotonotification", new object[] {GXUtil.UrlEncode(StringUtil.LTrimStr(AV39nIndex,8,0))}, new string[] {"index"}) ;
         }
         AV23notifications.Add(AV27oneNotification, 0);
         AV40nKind = "";
         AV37nContactId = Guid.Empty;
         AV38nGroupId = Guid.Empty;
         AV41nReferenceGroupId = Guid.Empty;
      }

      protected void S121( )
      {
         /* 'GROUP TARGET' Routine */
         returnInSub = false;
         GXt_guid2 = AV38nGroupId;
         new GeneXus.Programs.wallet.registered.getgroupidbyreference(context ).execute(  AV41nReferenceGroupId, out  GXt_guid2) ;
         AV38nGroupId = GXt_guid2;
         AV55nGroupName = StringUtil.Trim( AV35groupForLink.gxTpr_Groupname);
         if ( ! (Guid.Empty==AV38nGroupId) )
         {
            GXt_SdtGroup_SDT7 = AV54localGroup;
            new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV38nGroupId, out  GXt_SdtGroup_SDT7) ;
            AV54localGroup = GXt_SdtGroup_SDT7;
            AV55nGroupName = StringUtil.Trim( AV54localGroup.gxTpr_Groupname);
         }
         if ( (Guid.Empty==AV38nGroupId) )
         {
            AV40nKind = "groups";
         }
         else
         {
            AV40nKind = "group";
         }
      }

      protected void S131( )
      {
         /* 'DISPATCH ONE MESSAGE' Routine */
         returnInSub = false;
         GXt_char5 = AV13error;
         new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Chatkeyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char5) ;
         AV13error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char5 = AV13error;
            new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char5) ;
            AV13error = GXt_char5;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char5) ;
               AV13error = GXt_char5;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV33sdt_message.FromJSonString(AV9clearText, null);
            if ( AV33sdt_message.gxTpr_Messagetype == 50 )
            {
               AV32sdt_chat.FromJSonString(AV33sdt_message.gxTpr_Message, null);
               GXt_guid2 = AV11contactId;
               new GeneXus.Programs.wallet.registered.getcontactid(context ).execute(  StringUtil.Trim( AV32sdt_chat.gxTpr_From), out  AV34userPrivateName, out  GXt_guid2) ;
               AV11contactId = GXt_guid2;
               if ( (Guid.Empty==AV11contactId) )
               {
               }
               else
               {
                  if ( ! new GeneXus.Programs.wallet.registered.ischatstored(context).executeUdp(  AV11contactId,  AV32sdt_chat.gxTpr_Id) )
                  {
                     new GeneXus.Programs.wallet.registered.appendcontactchat(context ).execute(  AV11contactId,  AV32sdt_chat) ;
                     AV8chatNotice = true;
                     if ( AV15isChatScreen )
                     {
                        GXt_SdtContact_SDT8 = AV10contact;
                        new GeneXus.Programs.wallet.getcontact(context ).execute( out  GXt_SdtContact_SDT8) ;
                        AV10contact = GXt_SdtContact_SDT8;
                        if ( AV10contact.gxTpr_Contactrid == AV11contactId )
                        {
                           AV31refreshChat = true;
                           AV8chatNotice = false;
                        }
                     }
                     if ( AV8chatNotice )
                     {
                        AV26nType = "info";
                        AV25nTitle = "New Chat";
                        AV24nText = "you've received a chat from: " + AV34userPrivateName;
                        AV40nKind = "chat";
                        AV37nContactId = AV11contactId;
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                     }
                  }
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 30 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.insertinvitationoncontact(context ).execute(  AV33sdt_message, out  AV16isContactDeclined, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting invitation";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        if ( ! AV16isContactDeclined )
                        {
                           AV26nType = "info";
                           AV25nTitle = "Contact invitation";
                           AV24nText = "Please go to Contacts to accept or decline";
                           AV40nKind = "contacts";
                           /* Execute user subroutine: 'NOTIFY' */
                           S111 ();
                           if (returnInSub) return;
                        }
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 40 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitation(context ).execute(  AV33sdt_message, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting confirmation: ";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        AV26nType = "info";
                        AV25nTitle = "Contact accepted";
                        AV24nText = "One of you contact invitation was accepted";
                        AV40nKind = "contacts";
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "error";
                  AV25nTitle = "Error updating User invitation: ";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 70 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.insertinvitationongroup(context ).execute(  AV33sdt_message, out  AV16isContactDeclined, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group invitation";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        if ( ! AV16isContactDeclined )
                        {
                           AV26nType = "info";
                           AV25nTitle = "Group invitation";
                           AV24nText = "Please go to SmartGroups to accept or decline";
                           AV35groupForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                           AV41nReferenceGroupId = AV35groupForLink.gxTpr_Othergroup.gxTpr_Referencegroupid;
                           /* Execute user subroutine: 'GROUP TARGET' */
                           S121 ();
                           if (returnInSub) return;
                           GXt_char5 = AV24nText;
                           new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV35groupForLink.gxTpr_Othergroup.gxTpr_Referenceusernname, out  GXt_char5) ;
                           AV24nText = GXt_char5 + " invited you to the group " + AV55nGroupName + ". Accept or decline it in Smart Groups";
                           /* Execute user subroutine: 'NOTIFY' */
                           S111 ();
                           if (returnInSub) return;
                        }
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received a group invitaton";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 80 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitationongroup(context ).execute(  AV33sdt_message, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group invitation";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        AV26nType = "info";
                        AV25nTitle = "Group Confirmation";
                        AV24nText = "Please go to SmartGroups to see which user group accepted the invitation";
                        AV35groupForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                        AV41nReferenceGroupId = AV35groupForLink.gxTpr_Othergroup.gxTpr_Referencegroupid;
                        /* Execute user subroutine: 'GROUP TARGET' */
                        S121 ();
                        if (returnInSub) return;
                        GXt_char5 = AV24nText;
                        new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV35groupForLink.gxTpr_Othergroup.gxTpr_Referenceusernname, out  GXt_char5) ;
                        AV24nText = GXt_char5 + " accepted the invitation to the group " + AV55nGroupName;
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received a group acceptance";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 90 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.updateactivatedgroup(context ).execute(  AV33sdt_message, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group message";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        AV26nType = "info";
                        AV25nTitle = "Group activated";
                        AV24nText = "Please go to SmartGroups to see which user group was activated";
                        AV35groupForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                        AV41nReferenceGroupId = AV35groupForLink.gxTpr_Othergroup.gxTpr_Referencegroupid;
                        /* Execute user subroutine: 'GROUP TARGET' */
                        S121 ();
                        if (returnInSub) return;
                        AV24nText = "The group " + AV55nGroupName + " was activated";
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received a group activation";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 100 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.updaterestoreongroup(context ).execute(  AV33sdt_message, out  AV17isOwnerAlert, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group message";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        if ( AV17isOwnerAlert )
                        {
                           AV26nType = "warning";
                           AV25nTitle = "Your Wallet Backup is being RESTORED";
                           AV24nText = "A member of your backup group signed a restore. If you did NOT authorize it, go to SmartGroups > your group > Signatures and STOP the restore";
                        }
                        else
                        {
                           AV26nType = "info";
                           AV25nTitle = "Group restore";
                           AV24nText = "Please go to SmartGroups to see which user group it is restoring";
                        }
                        AV35groupForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                        AV41nReferenceGroupId = AV35groupForLink.gxTpr_Othergroup.gxTpr_Referencegroupid;
                        /* Execute user subroutine: 'GROUP TARGET' */
                        S121 ();
                        if (returnInSub) return;
                        if ( AV17isOwnerAlert )
                        {
                           AV24nText = "A member of your backup group " + AV55nGroupName + " signed a restore. If you did NOT authorize it, open the group, tab Signatures, and STOP the restore";
                        }
                        else
                        {
                           AV24nText = "The group " + AV55nGroupName + " is being restored";
                        }
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received a group restore";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 110 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.receivedonemusign(context ).execute(  AV33sdt_message, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group message";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        AV26nType = "info";
                        AV25nTitle = "Group signature";
                        AV24nText = "Please go to SmartGroups to see which user group has received a signature";
                        AV36muSigForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                        AV41nReferenceGroupId = AV36muSigForLink.gxTpr_Referencegroupid;
                        /* Execute user subroutine: 'GROUP TARGET' */
                        S121 ();
                        if (returnInSub) return;
                        GXt_char5 = AV24nText;
                        new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV36muSigForLink.gxTpr_Senderusername, out  GXt_char5) ;
                        AV24nText = GXt_char5 + " signed a payment of the group " + AV55nGroupName;
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received one partial signature";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 120 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.wallet.registered.receivedfinishedmusig(context ).execute(  AV33sdt_message, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV26nType = "error";
                     AV25nTitle = "Error deleting group message";
                     AV24nText = AV13error;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                     {
                        AV26nType = "info";
                        AV25nTitle = "Group payment completed";
                        AV24nText = "Please go to SmartGroups to see the completed transaction";
                        AV36muSigForLink.FromJSonString(AV33sdt_message.gxTpr_Message, null);
                        AV41nReferenceGroupId = AV36muSigForLink.gxTpr_Referencegroupid;
                        /* Execute user subroutine: 'GROUP TARGET' */
                        S121 ();
                        if (returnInSub) return;
                        AV24nText = "A payment of the group " + AV55nGroupName + " was completed";
                        /* Execute user subroutine: 'NOTIFY' */
                        S111 ();
                        if (returnInSub) return;
                        AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                     }
                  }
               }
               else
               {
                  AV26nType = "warning";
                  AV25nTitle = "We've received a signature confirmation";
                  AV24nText = AV13error;
                  /* Execute user subroutine: 'NOTIFY' */
                  S111 ();
                  if (returnInSub) return;
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 140 )
            {
               new GeneXus.Programs.wallet.registered.updatedeclinedinvitationongroup(context ).execute(  AV33sdt_message, out  AV51declinedBy, out  AV52declinedGroupId, out  AV53declinedGroupName, out  AV13error) ;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( ! ( AV19lastMessageId == AV33sdt_message.gxTpr_Id ) )
                  {
                     AV26nType = "warning";
                     AV25nTitle = "Group invitation declined";
                     GXt_char5 = AV24nText;
                     new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV51declinedBy, out  GXt_char5) ;
                     AV24nText = GXt_char5 + " declined the invitation to the group " + StringUtil.Trim( AV53declinedGroupName);
                     AV40nKind = "group";
                     AV38nGroupId = AV52declinedGroupId;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                     AV19lastMessageId = AV33sdt_message.gxTpr_Id;
                  }
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 130 )
            {
               new GeneXus.Programs.wallet.registered.receivegroupchat(context ).execute(  AV33sdt_message, out  AV46chatGroupId, out  AV48chatReferenceGroupId, out  AV47chatGroupName, out  AV45chatAdded, out  AV13error) ;
               if ( AV45chatAdded )
               {
                  AV8chatNotice = true;
                  if ( AV50isGroupScreen )
                  {
                     AV49groupOnScreen.FromJSonString(AV44websession.Get("Group_EDIT"), null);
                     if ( AV49groupOnScreen.gxTpr_Othergroup.gxTpr_Referencegroupid == AV48chatReferenceGroupId )
                     {
                        AV31refreshChat = true;
                        AV8chatNotice = false;
                     }
                  }
                  if ( AV8chatNotice )
                  {
                     AV26nType = "info";
                     AV25nTitle = "New group message";
                     AV24nText = "There is a new message in the group " + StringUtil.Trim( AV47chatGroupName);
                     AV40nKind = "group";
                     AV38nGroupId = AV46chatGroupId;
                     AV41nReferenceGroupId = AV48chatReferenceGroupId;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 150 )
            {
               new GeneXus.Programs.wallet.registered.receivemusig2room(context ).execute(  AV33sdt_message, out  AV46chatGroupId, out  AV48chatReferenceGroupId, out  AV47chatGroupName, out  AV56roomNotice, out  AV45chatAdded, out  AV13error) ;
               if ( AV45chatAdded )
               {
                  AV8chatNotice = true;
                  if ( AV50isGroupScreen )
                  {
                     AV49groupOnScreen.FromJSonString(AV44websession.Get("Group_EDIT"), null);
                     if ( AV49groupOnScreen.gxTpr_Othergroup.gxTpr_Referencegroupid == AV48chatReferenceGroupId )
                     {
                        AV31refreshChat = true;
                        AV8chatNotice = false;
                     }
                  }
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56roomNotice)) )
                  {
                     AV26nType = "info";
                     AV25nTitle = "Payment session to sign";
                     AV24nText = StringUtil.Trim( AV56roomNotice);
                     AV40nKind = "group";
                     AV38nGroupId = AV46chatGroupId;
                     AV41nReferenceGroupId = AV48chatReferenceGroupId;
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
               }
            }
            else
            {
            }
         }
         else
         {
         }
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
         AV23notifications = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification>( context, "IncomingNotification", "distributedcryptography");
         AV14externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV42connectionId = Guid.Empty;
         AV43queueCleaned = "";
         AV44websession = context.GetSession();
         AV28queueDirectory = new GxDirectory(context.GetPhysicalPath());
         AV13error = "";
         AV21messages = new SdtDesktopApp_services_SDT_Messages(context);
         AV20message = new SdtDesktopApp_services_SDT_Messages_Message_MessageItem(context);
         AV18json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         AV26nType = "";
         AV25nTitle = "";
         AV24nText = "";
         AV58queuedEvents = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent>( context, "QueuedNostrEvent", "distributedcryptography");
         AV60GXV2 = new GxFileCollection();
         AV29queueFile = new GxFile(context.GetPhysicalPath());
         AV22notificationInfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
         AV30recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
         AV57queuedEvent = new GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent(context);
         AV12encText = "";
         AV27oneNotification = new GeneXus.Programs.wallet.registered.SdtIncomingNotification(context);
         AV40nKind = "";
         AV37nContactId = Guid.Empty;
         AV38nGroupId = Guid.Empty;
         AV41nReferenceGroupId = Guid.Empty;
         AV55nGroupName = "";
         AV35groupForLink = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV54localGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT7 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV9clearText = "";
         AV33sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV32sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         AV11contactId = Guid.Empty;
         GXt_guid2 = Guid.Empty;
         AV34userPrivateName = "";
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_SdtContact_SDT8 = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV36muSigForLink = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV51declinedBy = "";
         AV52declinedGroupId = Guid.Empty;
         AV53declinedGroupName = "";
         GXt_char5 = "";
         AV46chatGroupId = Guid.Empty;
         AV48chatReferenceGroupId = Guid.Empty;
         AV47chatGroupName = "";
         AV49groupOnScreen = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV56roomNotice = "";
         /* GeneXus formulas. */
      }

      private int AV59GXV1 ;
      private int AV61GXV3 ;
      private int AV62GXV4 ;
      private int AV39nIndex ;
      private int GXt_int6 ;
      private string AV13error ;
      private string AV25nTitle ;
      private string AV24nText ;
      private string AV34userPrivateName ;
      private string GXt_char5 ;
      private bool AV15isChatScreen ;
      private bool AV50isGroupScreen ;
      private bool AV31refreshChat ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean4 ;
      private bool returnInSub ;
      private bool AV8chatNotice ;
      private bool AV16isContactDeclined ;
      private bool AV17isOwnerAlert ;
      private bool AV45chatAdded ;
      private string AV12encText ;
      private string AV9clearText ;
      private string AV43queueCleaned ;
      private string AV26nType ;
      private string AV40nKind ;
      private string AV55nGroupName ;
      private string AV51declinedBy ;
      private string AV53declinedGroupName ;
      private string AV47chatGroupName ;
      private string AV56roomNotice ;
      private Guid AV19lastMessageId ;
      private Guid AV42connectionId ;
      private Guid AV37nContactId ;
      private Guid AV38nGroupId ;
      private Guid AV41nReferenceGroupId ;
      private Guid AV11contactId ;
      private Guid GXt_guid2 ;
      private Guid AV52declinedGroupId ;
      private Guid AV46chatGroupId ;
      private Guid AV48chatReferenceGroupId ;
      private IGxSession AV44websession ;
      private GxFile AV29queueFile ;
      private GxDirectory AV28queueDirectory ;
      private GxFileCollection AV60GXV2 ;
      private Guid aP2_lastMessageId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> AV23notifications ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV14externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtDesktopApp_services_SDT_Messages AV21messages ;
      private SdtDesktopApp_services_SDT_Messages_Message_MessageItem AV20message ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV18json_enc ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent> AV58queuedEvents ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV22notificationInfo ;
      private GeneXus.Programs.nostr.SdtRecFromNostr AV30recFromNostr ;
      private GeneXus.Programs.wallet.registered.SdtQueuedNostrEvent AV57queuedEvent ;
      private GeneXus.Programs.wallet.registered.SdtIncomingNotification AV27oneNotification ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV35groupForLink ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV54localGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT7 ;
      private GeneXus.Programs.nostr.SdtSDT_message AV33sdt_message ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV32sdt_chat ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT GXt_SdtContact_SDT8 ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV36muSigForLink ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV49groupOnScreen ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP3_notifications ;
      private bool aP4_refreshChat ;
   }

}
