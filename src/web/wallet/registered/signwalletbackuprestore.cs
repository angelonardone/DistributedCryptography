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
   public class signwalletbackuprestore : GXProcedure
   {
      public signwalletbackuprestore( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signwalletbackuprestore( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_restored ,
                           out string aP2_warning ,
                           out string aP3_error )
      {
         this.AV21groupId = aP0_groupId;
         this.AV29restored = false ;
         this.AV35warning = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_restored=this.AV29restored;
         aP2_warning=this.AV35warning;
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out bool aP1_restored ,
                                out string aP2_warning )
      {
         execute(aP0_groupId, out aP1_restored, out aP2_warning, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_restored ,
                                 out string aP2_warning ,
                                 out string aP3_error )
      {
         this.AV21groupId = aP0_groupId;
         this.AV29restored = false ;
         this.AV35warning = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_restored=this.AV29restored;
         aP2_warning=this.AV35warning;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV29restored = false;
         AV24numOfSharedWasReach = false;
         AV32sharesToRecover.Clear();
         GXt_SdtExternalUserPublic1 = AV14externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV14externalUserPublic = GXt_SdtExternalUserPublic1;
         GXt_SdtGroup_SDT2 = AV16group_sdt_my;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV21groupId, out  GXt_SdtGroup_SDT2) ;
         AV16group_sdt_my = GXt_SdtGroup_SDT2;
         if ( (Guid.Empty==AV16group_sdt_my.gxTpr_Groupid) )
         {
            AV11error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV11error;
         new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Encpassword, out  AV15group_sdt, out  GXt_char3) ;
         AV11error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( AV15group_sdt.gxTpr_Restorestopped )
         {
            AV11error = "The owner of this wallet STOPPED the restore on " + context.localUtil.TToC( AV15group_sdt.gxTpr_Restorestoppeddatetime, 8, 5, 1, 2, "/", ":", " ") + ". Your share was NOT revealed.";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'LOOK FOR OTHERS SHARES' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            /* Execute user subroutine: 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' */
            S131 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            /* Execute user subroutine: 'SEND COMBINED SHARES TO GROUP MEMBERS' */
            S151 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV29restored = AV24numOfSharedWasReach;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOOK FOR OTHERS SHARES' Routine */
         returnInSub = false;
         AV36GXV1 = 1;
         while ( AV36GXV1 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV19groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV36GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV19groupContact.gxTpr_Contactusername), StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) != 0 )
            {
               AV10contactFound = false;
               AV37GXV2 = 1;
               while ( AV37GXV2 <= AV16group_sdt_my.gxTpr_Contact.Count )
               {
                  AV20groupContactMy = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV16group_sdt_my.gxTpr_Contact.Item(AV37GXV2));
                  if ( StringUtil.StrCmp(AV20groupContactMy.gxTpr_Contactusername, AV19groupContact.gxTpr_Contactusername) == 0 )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20groupContactMy.gxTpr_Cleartextshare)) )
                     {
                        if ( AV20groupContactMy.gxTpr_Numofsharesreached )
                        {
                           AV16group_sdt_my.gxTpr_Cleartextshare = AV20groupContactMy.gxTpr_Cleartextshare;
                           AV16group_sdt_my.gxTpr_Numofsharesreached = AV20groupContactMy.gxTpr_Numofsharesreached;
                           AV24numOfSharedWasReach = true;
                           /* Execute user subroutine: 'UPDATE MY GROUP' */
                           S121 ();
                           if (returnInSub) return;
                        }
                        else
                        {
                           AV33sharesToRecoverTemp.Clear();
                           AV33sharesToRecoverTemp.FromJSonString(AV20groupContactMy.gxTpr_Cleartextshare, null);
                           AV38GXV3 = 1;
                           while ( AV38GXV3 <= AV33sharesToRecoverTemp.Count )
                           {
                              AV26oneShare = ((string)AV33sharesToRecoverTemp.Item(AV38GXV3));
                              AV32sharesToRecover.Add(AV26oneShare, 0);
                              AV38GXV3 = (int)(AV38GXV3+1);
                           }
                        }
                        AV10contactFound = true;
                        if (true) break;
                     }
                  }
                  AV37GXV2 = (int)(AV37GXV2+1);
               }
               if ( ! AV10contactFound )
               {
                  GXt_char3 = AV12errorContact;
                  new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV19groupContact.gxTpr_Contactgroupid,  AV19groupContact.gxTpr_Contactgroupencpassword, out  AV18group_sdt_temp, out  GXt_char3) ;
                  AV12errorContact = GXt_char3;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12errorContact)) )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18group_sdt_temp.gxTpr_Cleartextshare)) )
                     {
                        AV33sharesToRecoverTemp.Clear();
                        AV33sharesToRecoverTemp.FromJSonString(AV18group_sdt_temp.gxTpr_Cleartextshare, null);
                        AV39GXV4 = 1;
                        while ( AV39GXV4 <= AV33sharesToRecoverTemp.Count )
                        {
                           AV26oneShare = ((string)AV33sharesToRecoverTemp.Item(AV39GXV4));
                           AV32sharesToRecover.Add(AV26oneShare, 0);
                           AV39GXV4 = (int)(AV39GXV4+1);
                        }
                     }
                  }
                  else
                  {
                     AV35warning = AV12errorContact;
                  }
               }
            }
            AV36GXV1 = (int)(AV36GXV1+1);
         }
      }

      protected void S131( )
      {
         /* 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' Routine */
         returnInSub = false;
         AV40GXV5 = 1;
         while ( AV40GXV5 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV19groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV40GXV5));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV19groupContact.gxTpr_Contactusername), StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               GXt_char3 = AV11error;
               new GeneXus.Programs.distcrypt.decryptwithgroupskey(context ).execute(  AV19groupContact.gxTpr_Contactencryptedtext,  AV19groupContact.gxTpr_Contactencryptedkey, out  AV31share, out  GXt_char3) ;
               AV11error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV34userShares.FromJSonString(AV31share, null);
                  AV16group_sdt_my.gxTpr_Cleartextshare = AV34userShares.ToJSonString(false);
                  AV16group_sdt_my.gxTpr_Restoresigneddatetime = DateTimeUtil.Now( context);
                  /* Execute user subroutine: 'RECORD MY SIGNATURE' */
                  S141 ();
                  if (returnInSub) return;
                  AV33sharesToRecoverTemp.Clear();
                  AV33sharesToRecoverTemp.FromJSonString(AV16group_sdt_my.gxTpr_Cleartextshare, null);
                  AV41GXV6 = 1;
                  while ( AV41GXV6 <= AV33sharesToRecoverTemp.Count )
                  {
                     AV26oneShare = ((string)AV33sharesToRecoverTemp.Item(AV41GXV6));
                     AV32sharesToRecover.Add(AV26oneShare, 0);
                     AV41GXV6 = (int)(AV41GXV6+1);
                  }
                  if ( ( AV15group_sdt.gxTpr_Minimumshares > 1 ) && ( AV32sharesToRecover.Count >= AV15group_sdt.gxTpr_Minimumshares ) )
                  {
                     GXt_char3 = AV11error;
                     new GeneXus.Programs.shamirss.combineshares(context ).execute(  AV32sharesToRecover, out  AV28recoveredSecret, out  GXt_char3) ;
                     AV11error = GXt_char3;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV28recoveredSecret)) )
                     {
                        AV16group_sdt_my.gxTpr_Numofsharesreached = true;
                        AV16group_sdt_my.gxTpr_Cleartextshare = AV28recoveredSecret;
                        AV24numOfSharedWasReach = true;
                     }
                  }
               }
            }
            AV40GXV5 = (int)(AV40GXV5+1);
         }
      }

      protected void S141( )
      {
         /* 'RECORD MY SIGNATURE' Routine */
         returnInSub = false;
         AV10contactFound = false;
         AV42GXV7 = 1;
         while ( AV42GXV7 <= AV16group_sdt_my.gxTpr_Contact.Count )
         {
            AV20groupContactMy = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV16group_sdt_my.gxTpr_Contact.Item(AV42GXV7));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV20groupContactMy.gxTpr_Contactusername), StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               AV20groupContactMy.gxTpr_Restoresigneddatetime = AV16group_sdt_my.gxTpr_Restoresigneddatetime;
               AV10contactFound = true;
            }
            AV42GXV7 = (int)(AV42GXV7+1);
         }
         if ( ! AV10contactFound )
         {
            AV25oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
            AV25oneGroupContact.gxTpr_Contactusername = StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
            AV25oneGroupContact.gxTpr_Restoresigneddatetime = AV16group_sdt_my.gxTpr_Restoresigneddatetime;
            AV16group_sdt_my.gxTpr_Contact.Add(AV25oneGroupContact, 0);
         }
      }

      protected void S151( )
      {
         /* 'SEND COMBINED SHARES TO GROUP MEMBERS' Routine */
         returnInSub = false;
         AV18group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18group_sdt_temp.gxTpr_Groupname = AV16group_sdt_my.gxTpr_Groupname;
         AV18group_sdt_temp.gxTpr_Grouptype = AV16group_sdt_my.gxTpr_Grouptype;
         AV18group_sdt_temp.gxTpr_Cleartextshare = AV16group_sdt_my.gxTpr_Cleartextshare;
         AV18group_sdt_temp.gxTpr_Numofsharesreached = AV16group_sdt_my.gxTpr_Numofsharesreached;
         AV18group_sdt_temp.gxTpr_Restoresigneddatetime = AV16group_sdt_my.gxTpr_Restoresigneddatetime;
         AV18group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV18group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV23message_signature.gxTpr_Username = StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV23message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV14externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char3 = AV11error;
         GXt_char4 = AV23message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV23message_signature.gxTpr_Username)+StringUtil.Trim( AV23message_signature.gxTpr_Grouppubkey), out  GXt_char4, out  GXt_char3) ;
         AV23message_signature.gxTpr_Signature = GXt_char4;
         AV11error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV18group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV23message_signature.gxTpr_Signature);
            AV43GXV8 = 1;
            while ( AV43GXV8 <= AV15group_sdt.gxTpr_Contact.Count )
            {
               AV19groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV43GXV8));
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV19groupContact.gxTpr_Contactusername), StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 ) )
               {
                  AV30sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int5 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
                  AV30sdt_message.gxTpr_Datetimeunix = GXt_int5;
                  AV30sdt_message.gxTpr_Messagetype = 100;
                  AV30sdt_message.gxTpr_Message = AV18group_sdt_temp.ToJSonString(false, true);
                  AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV9contact.gxTpr_Username = StringUtil.Trim( AV19groupContact.gxTpr_Contactusername);
                  AV9contact.gxTpr_Messagepubkey = StringUtil.Trim( AV19groupContact.gxTpr_Contactuserpubkey);
                  GXt_char4 = AV11error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV9contact,  AV30sdt_message, out  GXt_char4) ;
                  AV11error = GXt_char4;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV11error = "There was a problem sending the Message to the Group: " + AV11error;
                     if (true) break;
                  }
               }
               AV43GXV8 = (int)(AV43GXV8+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               /* Execute user subroutine: 'NOTIFY OWNER' */
               S161 ();
               if (returnInSub) return;
               /* Execute user subroutine: 'UPDATE MY GROUP' */
               S121 ();
               if (returnInSub) return;
            }
         }
         else
         {
            AV11error = "There was a problem Signing the invitation: " + AV11error;
         }
      }

      protected void S161( )
      {
         /* 'NOTIFY OWNER' Routine */
         returnInSub = false;
         AV17group_sdt_owner = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV18group_sdt_temp.Clone());
         AV17group_sdt_owner.gxTpr_Cleartextshare = "";
         AV8allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV13errorOwner), null);
         AV27ownerContact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV44GXV9 = 1;
         while ( AV44GXV9 <= AV8allContacts.Count )
         {
            AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV8allContacts.Item(AV44GXV9));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV9contact.gxTpr_Username), StringUtil.Trim( AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
            {
               AV27ownerContact = (GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV9contact.Clone());
               if (true) break;
            }
            AV44GXV9 = (int)(AV44GXV9+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV27ownerContact.gxTpr_Username)) )
         {
            AV35warning = "The owner of the wallet is not on your contact list: he was NOT notified of the restore";
         }
         else
         {
            AV30sdt_message.gxTpr_Id = Guid.NewGuid( );
            GXt_int5 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
            AV30sdt_message.gxTpr_Datetimeunix = GXt_int5;
            AV30sdt_message.gxTpr_Messagetype = 100;
            AV30sdt_message.gxTpr_Message = AV17group_sdt_owner.ToJSonString(false, true);
            AV27ownerContact.gxTpr_Messagepubkey = StringUtil.Trim( AV27ownerContact.gxTpr_Grouppubkey);
            GXt_char4 = AV13errorOwner;
            new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV27ownerContact,  AV30sdt_message, out  GXt_char4) ;
            AV13errorOwner = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13errorOwner)) )
            {
               AV35warning = "There was a problem notifying the owner of the wallet: " + AV13errorOwner;
            }
         }
      }

      protected void S121( )
      {
         /* 'UPDATE MY GROUP' Routine */
         returnInSub = false;
         GXt_char4 = AV11error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV16group_sdt_my,  StringUtil.Trim( AV16group_sdt_my.gxTpr_Encpassword), out  AV22grpupId, out  GXt_char4) ;
         AV11error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char4 = AV11error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV16group_sdt_my, out  GXt_char4) ;
            AV11error = GXt_char4;
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
         AV35warning = "";
         AV11error = "";
         AV32sharesToRecover = new GxSimpleCollection<string>();
         AV14externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV16group_sdt_my = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV20groupContactMy = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV33sharesToRecoverTemp = new GxSimpleCollection<string>();
         AV26oneShare = "";
         AV12errorContact = "";
         AV18group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV31share = "";
         AV34userShares = new GxSimpleCollection<string>();
         AV28recoveredSecret = "";
         AV25oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV23message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char3 = "";
         AV30sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV17group_sdt_owner = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV13errorOwner = "";
         AV27ownerContact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV22grpupId = Guid.Empty;
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private int AV36GXV1 ;
      private int AV37GXV2 ;
      private int AV38GXV3 ;
      private int AV39GXV4 ;
      private int AV40GXV5 ;
      private int AV41GXV6 ;
      private int AV42GXV7 ;
      private int AV43GXV8 ;
      private int AV44GXV9 ;
      private long GXt_int5 ;
      private string AV35warning ;
      private string AV11error ;
      private string AV12errorContact ;
      private string AV28recoveredSecret ;
      private string GXt_char3 ;
      private string AV13errorOwner ;
      private string GXt_char4 ;
      private bool AV29restored ;
      private bool AV24numOfSharedWasReach ;
      private bool returnInSub ;
      private bool AV10contactFound ;
      private string AV26oneShare ;
      private string AV31share ;
      private Guid AV21groupId ;
      private Guid AV22grpupId ;
      private GxSimpleCollection<string> AV32sharesToRecover ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV14externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_my ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV19groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV20groupContactMy ;
      private GxSimpleCollection<string> AV33sharesToRecoverTemp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV18group_sdt_temp ;
      private GxSimpleCollection<string> AV34userShares ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV25oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV23message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV30sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt_owner ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV8allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV27ownerContact ;
      private bool aP1_restored ;
      private string aP2_warning ;
      private string aP3_error ;
   }

}
