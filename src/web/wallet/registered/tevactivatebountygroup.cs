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
   public class tevactivatebountygroup : GXProcedure
   {
      public tevactivatebountygroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public tevactivatebountygroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_base64_secret ,
                           out string aP2_error )
      {
         this.AV18groupId = aP0_groupId;
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
         this.AV18groupId = aP0_groupId;
         this.AV8base64_secret = aP1_base64_secret;
         this.AV10error = "" ;
         SubmitImpl();
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV15group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV18groupId, out  GXt_SdtGroup_SDT1) ;
         AV15group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV15group_sdt.gxTpr_Groupid) )
         {
            AV10error = "We couldn't find the bounty group of the vault";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV11externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV11externalUserPublic = GXt_SdtExternalUserPublic2;
         GXt_SdtExtKeyInfo3 = AV12extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo3) ;
         AV12extKeyInfoRoot = GXt_SdtExtKeyInfo3;
         GXt_SdtWalletInfo4 = AV23walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo4) ;
         AV23walletInfo = GXt_SdtWalletInfo4;
         GXt_char5 = AV10error;
         new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV23walletInfo.gxTpr_Networktype,  "4", out  AV13extPubKeyInfo, out  GXt_char5) ;
         AV10error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV10error = "We couldn't create the Receiving Extended Public Key for the Bounty: " + AV10error;
            cleanup();
            if (true) return;
         }
         AV15group_sdt.gxTpr_Extpubkeytimebountyreceiving = AV13extPubKeyInfo.gxTpr_Publickeytaproot;
         GXt_char5 = AV10error;
         GXt_char6 = AV15group_sdt.gxTpr_Encpassword;
         GXt_char7 = AV15group_sdt.gxTpr_Encryptedtextshare;
         new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  StringUtil.Trim( AV8base64_secret),  StringUtil.Trim( AV11externalUserPublic.gxTpr_Keyinfo.gxTpr_Publickey), out  GXt_char6, out  GXt_char7, out  GXt_char5) ;
         AV15group_sdt.gxTpr_Encpassword = GXt_char6;
         AV15group_sdt.gxTpr_Encryptedtextshare = GXt_char7;
         AV10error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV15group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving = AV15group_sdt.gxTpr_Extpubkeytimebountyreceiving;
         AV15group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV24GXV1));
            GXt_char7 = AV10error;
            GXt_char6 = AV17groupContact.gxTpr_Contactencryptedkey;
            GXt_char5 = AV17groupContact.gxTpr_Contactencryptedtext;
            new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  StringUtil.Trim( AV8base64_secret),  StringUtil.Trim( AV17groupContact.gxTpr_Contactuserpubkey), out  GXt_char6, out  GXt_char5, out  GXt_char7) ;
            AV17groupContact.gxTpr_Contactencryptedkey = GXt_char6;
            AV17groupContact.gxTpr_Contactencryptedtext = GXt_char5;
            AV10error = GXt_char7;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               cleanup();
               if (true) return;
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         AV16group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16group_sdt_temp.gxTpr_Groupname = AV15group_sdt.gxTpr_Groupname;
         AV16group_sdt_temp.gxTpr_Grouptype = AV15group_sdt.gxTpr_Grouptype;
         AV16group_sdt_temp.gxTpr_Minimumshares = AV15group_sdt.gxTpr_Minimumshares;
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV15group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV15group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving = AV15group_sdt.gxTpr_Extpubkeytimebountyreceiving;
         AV14firstInvitationSent = (DateTime)(DateTime.MinValue);
         AV25GXV2 = 1;
         while ( AV25GXV2 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV25GXV2));
            if ( (DateTime.MinValue==AV17groupContact.gxTpr_Contactinvitationsent) )
            {
               AV17groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
            }
            AV17groupContact.gxTpr_Contactinvisent = true;
            if ( (DateTime.MinValue==AV14firstInvitationSent) || ( AV17groupContact.gxTpr_Contactinvitationsent < AV14firstInvitationSent ) )
            {
               AV14firstInvitationSent = AV17groupContact.gxTpr_Contactinvitationsent;
            }
            AV25GXV2 = (int)(AV25GXV2+1);
         }
         if ( ! AV15group_sdt.gxTpr_Isactive )
         {
            AV21oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
            AV21oneGroupContact.gxTpr_Contactid = AV15group_sdt.gxTpr_Groupid;
            AV21oneGroupContact.gxTpr_Contactgroupid = AV15group_sdt.gxTpr_Groupid;
            AV21oneGroupContact.gxTpr_Contactusername = StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
            AV21oneGroupContact.gxTpr_Contactuserpubkey = StringUtil.Trim( AV11externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            AV21oneGroupContact.gxTpr_Extpubkeytimebountyreceiving = AV15group_sdt.gxTpr_Extpubkeytimebountyreceiving;
            AV21oneGroupContact.gxTpr_Contactinvitationsent = AV14firstInvitationSent;
            AV21oneGroupContact.gxTpr_Contactinvitacionaccepted = DateTimeUtil.Now( context);
            AV21oneGroupContact.gxTpr_Contactinvisent = true;
            AV15group_sdt.gxTpr_Contact.Add(AV21oneGroupContact, 0);
         }
         AV26GXV3 = 1;
         while ( AV26GXV3 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV21oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV26GXV3));
            AV16group_sdt_temp.gxTpr_Contact.Add((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV21oneGroupContact.Clone()), 0);
            AV26GXV3 = (int)(AV26GXV3+1);
         }
         AV20message_signature.gxTpr_Username = StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV20message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV11externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char7 = AV10error;
         GXt_char6 = AV20message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV20message_signature.gxTpr_Username)+StringUtil.Trim( AV20message_signature.gxTpr_Grouppubkey), out  GXt_char6, out  GXt_char7) ;
         AV20message_signature.gxTpr_Signature = GXt_char6;
         AV10error = GXt_char7;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV10error = "There was a problem Signing the invitation: " + AV10error;
            cleanup();
            if (true) return;
         }
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV20message_signature.gxTpr_Signature);
         if ( ! AV15group_sdt.gxTpr_Isactive )
         {
            AV27GXV4 = 1;
            while ( AV27GXV4 <= AV15group_sdt.gxTpr_Contact.Count )
            {
               AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV27GXV4));
               if ( ! ( AV17groupContact.gxTpr_Contactid == AV17groupContact.gxTpr_Contactgroupid ) )
               {
                  AV22sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int8 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int8) ;
                  AV22sdt_message.gxTpr_Datetimeunix = GXt_int8;
                  AV22sdt_message.gxTpr_Messagetype = 90;
                  AV22sdt_message.gxTpr_Message = AV16group_sdt_temp.ToJSonString(false, true);
                  AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV9contact.gxTpr_Username = StringUtil.Trim( AV17groupContact.gxTpr_Contactusername);
                  AV9contact.gxTpr_Messagepubkey = StringUtil.Trim( AV17groupContact.gxTpr_Contactuserpubkey);
                  GXt_char7 = AV10error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV9contact,  AV22sdt_message, out  GXt_char7) ;
                  AV10error = GXt_char7;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                  {
                     AV10error = "There was a problem sending the Activation to the Group: " + AV10error;
                     cleanup();
                     if (true) return;
                  }
               }
               AV27GXV4 = (int)(AV27GXV4+1);
            }
         }
         AV15group_sdt.gxTpr_Isactive = true;
         GXt_char7 = AV10error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV15group_sdt,  StringUtil.Trim( AV15group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV19grpupId, out  GXt_char7) ;
         AV10error = GXt_char7;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GXt_char7 = AV10error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV15group_sdt, out  GXt_char7) ;
            AV10error = GXt_char7;
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
         AV10error = "";
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo3 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV23walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo4 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV17groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_char5 = "";
         AV16group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14firstInvitationSent = (DateTime)(DateTime.MinValue);
         AV21oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV20message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char6 = "";
         AV22sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV19grpupId = Guid.Empty;
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private int AV26GXV3 ;
      private int AV27GXV4 ;
      private long GXt_int8 ;
      private string AV8base64_secret ;
      private string AV10error ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string GXt_char7 ;
      private DateTime AV14firstInvitationSent ;
      private Guid AV18groupId ;
      private Guid AV19grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV11externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo3 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV23walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo4 ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV13extPubKeyInfo ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV21oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV20message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV22sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private string aP2_error ;
   }

}
