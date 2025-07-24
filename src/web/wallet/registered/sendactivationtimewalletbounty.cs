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
   public class sendactivationtimewalletbounty : GXProcedure
   {
      public sendactivationtimewalletbounty( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendactivationtimewalletbounty( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_base64_secret ,
                           out string aP2_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV8base64_secret = aP1_base64_secret;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_base64_secret )
      {
         execute(aP0_groupId, aP1_base64_secret, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_base64_secret ,
                                 out string aP2_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV8base64_secret = aP1_base64_secret;
         this.AV10error = "" ;
         SubmitImpl();
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV14group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV17groupId, out  GXt_SdtGroup_SDT1) ;
         AV14group_sdt = GXt_SdtGroup_SDT1;
         GXt_SdtExternalUser2 = AV11externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV11externalUser = GXt_SdtExternalUser2;
         GXt_SdtExtKeyInfo3 = AV12extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo3) ;
         AV12extKeyInfoRoot = GXt_SdtExtKeyInfo3;
         GXt_SdtWallet4 = AV22wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet4) ;
         AV22wallet = GXt_SdtWallet4;
         GXt_char5 = AV10error;
         new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV22wallet.gxTpr_Networktype,  "4", out  AV13extPubKeyInfo, out  GXt_char5) ;
         AV10error = GXt_char5;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV14group_sdt.gxTpr_Extpubkeytimebountyreceiving = AV13extPubKeyInfo.gxTpr_Publickeytaproot;
         }
         else
         {
            AV10error = "We couldn't create the Receiving Extended Public Key for the Bounty: " + AV10error;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GXt_char5 = AV10error;
            GXt_char6 = AV14group_sdt.gxTpr_Encpassword;
            GXt_char7 = AV14group_sdt.gxTpr_Encryptedtextshare;
            new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  StringUtil.Trim( AV8base64_secret),  StringUtil.Trim( AV11externalUser.gxTpr_Keyinfo.gxTpr_Publickey), out  GXt_char6, out  GXt_char7, out  GXt_char5) ;
            AV14group_sdt.gxTpr_Encpassword = GXt_char6;
            AV14group_sdt.gxTpr_Encryptedtextshare = GXt_char7;
            AV10error = GXt_char5;
            AV14group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving = AV14group_sdt.gxTpr_Extpubkeytimebountyreceiving;
            AV14group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV11externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV23GXV1 = 1;
            while ( AV23GXV1 <= AV14group_sdt.gxTpr_Contact.Count )
            {
               AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV23GXV1));
               GXt_char7 = AV10error;
               GXt_char6 = AV16groupContact.gxTpr_Contactencryptedkey;
               GXt_char5 = AV16groupContact.gxTpr_Contactencryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  StringUtil.Trim( AV8base64_secret),  StringUtil.Trim( AV16groupContact.gxTpr_Contactuserpubkey), out  GXt_char6, out  GXt_char5, out  GXt_char7) ;
               AV16groupContact.gxTpr_Contactencryptedkey = GXt_char6;
               AV16groupContact.gxTpr_Contactencryptedtext = GXt_char5;
               AV10error = GXt_char7;
               AV23GXV1 = (int)(AV23GXV1+1);
            }
            /* Execute user subroutine: 'SEND ACTIVATION TO GROUP MEMBERS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else
         {
            GX_msglist.addItem(AV10error);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'SEND ACTIVATION TO GROUP MEMBERS' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV15group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV15group_sdt_temp.gxTpr_Groupname = AV14group_sdt.gxTpr_Groupname;
            AV15group_sdt_temp.gxTpr_Grouptype = AV14group_sdt.gxTpr_Grouptype;
            AV15group_sdt_temp.gxTpr_Minimumshares = AV14group_sdt.gxTpr_Minimumshares;
            AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV11externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV14group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV14group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
            AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving = AV14group_sdt.gxTpr_Extpubkeytimebountyreceiving;
            AV24GXV2 = 1;
            while ( AV24GXV2 <= AV14group_sdt.gxTpr_Contact.Count )
            {
               AV20oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV24GXV2));
               AV15group_sdt_temp.gxTpr_Contact.Add(AV20oneGroupContact, 0);
               AV24GXV2 = (int)(AV24GXV2+1);
            }
            if ( ! AV14group_sdt.gxTpr_Isactive )
            {
               if ( AV14group_sdt.gxTpr_Grouptype == 20 )
               {
                  AV20oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
                  AV20oneGroupContact.gxTpr_Contactid = AV14group_sdt.gxTpr_Groupid;
                  AV20oneGroupContact.gxTpr_Contactgroupid = AV14group_sdt.gxTpr_Groupid;
                  AV20oneGroupContact.gxTpr_Contactusername = StringUtil.Trim( AV11externalUser.gxTpr_Userinfo.gxTpr_Username);
                  AV20oneGroupContact.gxTpr_Contactuserpubkey = StringUtil.Trim( AV11externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
                  AV20oneGroupContact.gxTpr_Extpubkeytimebountyreceiving = AV14group_sdt.gxTpr_Extpubkeytimebountyreceiving;
                  AV15group_sdt_temp.gxTpr_Contact.Add(AV20oneGroupContact, 0);
                  AV14group_sdt.gxTpr_Contact.Add(AV20oneGroupContact, 0);
               }
            }
            AV19message_signature.gxTpr_Username = StringUtil.Trim( AV11externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV19message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV11externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            GXt_char7 = AV10error;
            GXt_char6 = AV19message_signature.gxTpr_Signature;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV11externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV19message_signature.gxTpr_Username)+StringUtil.Trim( AV19message_signature.gxTpr_Grouppubkey), out  GXt_char6, out  GXt_char7) ;
            AV19message_signature.gxTpr_Signature = GXt_char6;
            AV10error = GXt_char7;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV19message_signature.gxTpr_Signature);
               AV25GXV3 = 1;
               while ( AV25GXV3 <= AV14group_sdt.gxTpr_Contact.Count )
               {
                  AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV25GXV3));
                  AV16groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
                  AV16groupContact.gxTpr_Contactinvisent = true;
                  AV21sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int8 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int8) ;
                  AV21sdt_message.gxTpr_Datetimeunix = GXt_int8;
                  AV21sdt_message.gxTpr_Messagetype = 90;
                  AV21sdt_message.gxTpr_Message = AV15group_sdt_temp.ToJSonString(false, true);
                  AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV9contact.gxTpr_Username = StringUtil.Trim( AV16groupContact.gxTpr_Contactusername);
                  AV9contact.gxTpr_Messagepubkey = StringUtil.Trim( AV16groupContact.gxTpr_Contactuserpubkey);
                  if ( ( AV14group_sdt.gxTpr_Grouptype == 20 ) && ! ( AV16groupContact.gxTpr_Contactid == AV16groupContact.gxTpr_Contactgroupid ) )
                  {
                     if ( ! AV14group_sdt.gxTpr_Isactive )
                     {
                        GXt_char7 = AV10error;
                        new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV9contact,  AV21sdt_message, out  GXt_char7) ;
                        AV10error = GXt_char7;
                     }
                  }
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                  {
                     GX_msglist.addItem("There was a problem sending the Activation to the Group: "+AV10error);
                     if (true) break;
                  }
                  AV25GXV3 = (int)(AV25GXV3+1);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  AV14group_sdt.gxTpr_Isactive = true;
                  GXt_char7 = AV10error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  StringUtil.Trim( AV14group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV18grpupId, out  GXt_char7) ;
                  AV10error = GXt_char7;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                  {
                     GXt_char7 = AV10error;
                     new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV14group_sdt, out  GXt_char7) ;
                     AV10error = GXt_char7;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                     {
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        GX_msglist.addItem(AV10error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV10error);
                  }
               }
            }
            else
            {
               GX_msglist.addItem("There was a problem Signing the invitation: "+AV10error);
            }
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
         AV10error = "";
         AV14group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo3 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV22wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet4 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV16groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_char5 = "";
         AV15group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV19message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char6 = "";
         AV21sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV18grpupId = Guid.Empty;
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int AV25GXV3 ;
      private long GXt_int8 ;
      private string AV8base64_secret ;
      private string AV10error ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string GXt_char7 ;
      private bool returnInSub ;
      private Guid AV17groupId ;
      private Guid AV18grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV11externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo3 ;
      private GeneXus.Programs.wallet.SdtWallet AV22wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet4 ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV13extPubKeyInfo ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV20oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV19message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV21sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private string aP2_error ;
   }

}
