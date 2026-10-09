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
                           ref Guid aP1_lastMessageId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP2_notifications ,
                           out bool aP3_refreshChat )
      {
         this.AV15isChatScreen = aP0_isChatScreen;
         this.AV19lastMessageId = aP1_lastMessageId;
         this.AV23notifications = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification>( context, "IncomingNotification", "distributedcryptography") ;
         this.AV31refreshChat = false ;
         initialize();
         ExecuteImpl();
         aP1_lastMessageId=this.AV19lastMessageId;
         aP2_notifications=this.AV23notifications;
         aP3_refreshChat=this.AV31refreshChat;
      }

      public bool executeUdp( bool aP0_isChatScreen ,
                              ref Guid aP1_lastMessageId ,
                              out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP2_notifications )
      {
         execute(aP0_isChatScreen, ref aP1_lastMessageId, out aP2_notifications, out aP3_refreshChat);
         return AV31refreshChat ;
      }

      public void executeSubmit( bool aP0_isChatScreen ,
                                 ref Guid aP1_lastMessageId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP2_notifications ,
                                 out bool aP3_refreshChat )
      {
         this.AV15isChatScreen = aP0_isChatScreen;
         this.AV19lastMessageId = aP1_lastMessageId;
         this.AV23notifications = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification>( context, "IncomingNotification", "distributedcryptography") ;
         this.AV31refreshChat = false ;
         SubmitImpl();
         aP1_lastMessageId=this.AV19lastMessageId;
         aP2_notifications=this.AV23notifications;
         aP3_refreshChat=this.AV31refreshChat;
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
         AV28queueDirectory.Source = "Comqueue";
         AV36GXV2 = 1;
         AV35GXV1 = AV28queueDirectory.GetFiles(".queue");
         while ( AV36GXV2 <= AV35GXV1.ItemCount )
         {
            AV29queueFile = AV35GXV1.Item(AV36GXV2);
            AV22notificationInfo.FromJSonFile(AV29queueFile, null);
            AV29queueFile.Delete();
            new GeneXus.Programs.nostr.processrecivedfromnostr(context ).execute(  AV22notificationInfo, out  AV30recFromNostr) ;
            if ( ( StringUtil.StrCmp(AV30recFromNostr.gxTpr_Responsetype, "EVENT") == 0 ) && ( AV30recFromNostr.gxTpr_Event.gxTpr_Kind == 4 ) )
            {
               GXt_char2 = AV12encText;
               new GeneXus.Programs.distcrypt.hextotext(context ).execute(  StringUtil.Trim( AV30recFromNostr.gxTpr_Event.gxTpr_Content), out  GXt_char2) ;
               AV12encText = GXt_char2;
               AV18json_enc.FromJSonString(AV12encText, null);
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S121 ();
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
            AV36GXV2 = (int)(AV36GXV2+1);
         }
         GXt_char2 = AV13error;
         new GeneXus.Programs.wallet.registered.getmessages(context ).execute( out  AV21messages, out  GXt_char2) ;
         AV13error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV37GXV3 = 1;
            while ( AV37GXV3 <= AV21messages.gxTpr_Message.Count )
            {
               AV20message = ((SdtDesktopApp_services_SDT_Messages_Message_MessageItem)AV21messages.gxTpr_Message.Item(AV37GXV3));
               AV18json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
               AV18json_enc.gxTpr_Encryptedkey = AV20message.gxTpr_Messageencryptedkey;
               AV18json_enc.gxTpr_Encryptedtext = AV20message.gxTpr_Messageencrypted;
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S121 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV37GXV3 = (int)(AV37GXV3+1);
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
         AV23notifications.Add(AV27oneNotification, 0);
      }

      protected void S121( )
      {
         /* 'DISPATCH ONE MESSAGE' Routine */
         returnInSub = false;
         GXt_char2 = AV13error;
         new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Chatkeyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char2) ;
         AV13error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char2 = AV13error;
            new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char2) ;
            AV13error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               GXt_char2 = AV13error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18json_enc.gxTpr_Encryptedtext,  AV18json_enc.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV9clearText, out  GXt_char2) ;
               AV13error = GXt_char2;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV33sdt_message.FromJSonString(AV9clearText, null);
            if ( AV33sdt_message.gxTpr_Messagetype == 50 )
            {
               AV32sdt_chat.FromJSonString(AV33sdt_message.gxTpr_Message, null);
               GXt_guid3 = AV11contactId;
               new GeneXus.Programs.wallet.registered.getcontactid(context ).execute(  StringUtil.Trim( AV32sdt_chat.gxTpr_From), out  AV34userPrivateName, out  GXt_guid3) ;
               AV11contactId = GXt_guid3;
               if ( (Guid.Empty==AV11contactId) )
               {
               }
               else
               {
                  new GeneXus.Programs.wallet.registered.appendcontactchat(context ).execute(  AV11contactId,  AV32sdt_chat) ;
                  AV8chatNotice = true;
                  if ( AV15isChatScreen )
                  {
                     GXt_SdtContact_SDT4 = AV10contact;
                     new GeneXus.Programs.wallet.getcontact(context ).execute( out  GXt_SdtContact_SDT4) ;
                     AV10contact = GXt_SdtContact_SDT4;
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
                     /* Execute user subroutine: 'NOTIFY' */
                     S111 ();
                     if (returnInSub) return;
                  }
               }
            }
            else if ( AV33sdt_message.gxTpr_Messagetype == 30 )
            {
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.insertinvitationoncontact(context ).execute(  AV33sdt_message, out  AV16isContactDeclined, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                           AV25nTitle = "There is a new Contact invitation";
                           AV24nText = "Please go to Contacts to accept or decline";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitation(context ).execute(  AV33sdt_message, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                        AV25nTitle = "There is a new Confirmation";
                        AV24nText = "One of you contact invitation was accepted";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.insertinvitationongroup(context ).execute(  AV33sdt_message, out  AV16isContactDeclined, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                           AV25nTitle = "There is a new Group invitation";
                           AV24nText = "Please go to SmartGroups to accept or decline";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitationongroup(context ).execute(  AV33sdt_message, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                        AV25nTitle = "There is a new Group Confirmation";
                        AV24nText = "Please go to SmartGroups to see which user group accepted the invitation";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.updateactivatedgroup(context ).execute(  AV33sdt_message, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                        AV25nTitle = "There is a new Group Activation";
                        AV24nText = "Please go to SmartGroups to see which user group was activated";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.updaterestoreongroup(context ).execute(  AV33sdt_message, out  AV17isOwnerAlert, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                           AV25nTitle = "There is a new Group Restore";
                           AV24nText = "Please go to SmartGroups to see which user group it is restoring";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.receivedonemusign(context ).execute(  AV33sdt_message, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                        AV25nTitle = "There is a new MultiSignature Group event";
                        AV24nText = "Please go to SmartGroups to see which user group has received a signature";
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
               GXt_char2 = AV13error;
               new GeneXus.Programs.wallet.registered.receivedfinishedmusig(context ).execute(  AV33sdt_message, out  GXt_char2) ;
               AV13error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  GXt_char2 = AV13error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV20message.gxTpr_Messageid, out  GXt_char2) ;
                  AV13error = GXt_char2;
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
                        AV25nTitle = "There is a new MultiSignature Group transaction";
                        AV24nText = "Please go to SmartGroups to see the completed transaction";
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
         AV28queueDirectory = new GxDirectory(context.GetPhysicalPath());
         AV35GXV1 = new GxFileCollection();
         AV29queueFile = new GxFile(context.GetPhysicalPath());
         AV22notificationInfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
         AV30recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
         AV12encText = "";
         AV18json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         AV26nType = "";
         AV25nTitle = "";
         AV24nText = "";
         AV13error = "";
         AV21messages = new SdtDesktopApp_services_SDT_Messages(context);
         AV20message = new SdtDesktopApp_services_SDT_Messages_Message_MessageItem(context);
         AV27oneNotification = new GeneXus.Programs.wallet.registered.SdtIncomingNotification(context);
         AV9clearText = "";
         AV33sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV32sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         AV11contactId = Guid.Empty;
         GXt_guid3 = Guid.Empty;
         AV34userPrivateName = "";
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_SdtContact_SDT4 = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV36GXV2 ;
      private int AV37GXV3 ;
      private string AV25nTitle ;
      private string AV24nText ;
      private string AV13error ;
      private string AV34userPrivateName ;
      private string GXt_char2 ;
      private bool AV15isChatScreen ;
      private bool AV31refreshChat ;
      private bool returnInSub ;
      private bool AV8chatNotice ;
      private bool AV16isContactDeclined ;
      private bool AV17isOwnerAlert ;
      private string AV12encText ;
      private string AV9clearText ;
      private string AV26nType ;
      private Guid AV19lastMessageId ;
      private Guid AV11contactId ;
      private Guid GXt_guid3 ;
      private GxFile AV29queueFile ;
      private GxDirectory AV28queueDirectory ;
      private GxFileCollection AV35GXV1 ;
      private Guid aP1_lastMessageId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> AV23notifications ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV14externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV22notificationInfo ;
      private GeneXus.Programs.nostr.SdtRecFromNostr AV30recFromNostr ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV18json_enc ;
      private SdtDesktopApp_services_SDT_Messages AV21messages ;
      private SdtDesktopApp_services_SDT_Messages_Message_MessageItem AV20message ;
      private GeneXus.Programs.wallet.registered.SdtIncomingNotification AV27oneNotification ;
      private GeneXus.Programs.nostr.SdtSDT_message AV33sdt_message ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV32sdt_chat ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT GXt_SdtContact_SDT4 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtIncomingNotification> aP2_notifications ;
      private bool aP3_refreshChat ;
   }

}
