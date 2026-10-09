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
   public class activatelegacygroup : GXProcedure
   {
      public activatelegacygroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public activatelegacygroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV14group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV17groupId, out  GXt_SdtGroup_SDT1) ;
         AV14group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV14group_sdt.gxTpr_Groupid) || ! AV14group_sdt.gxTpr_Amigroupowner )
         {
            AV9error = "Only the owner of the group can activate it";
            cleanup();
            if (true) return;
         }
         if ( AV14group_sdt.gxTpr_Isactive )
         {
            AV9error = "The group is already active";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic2;
         GXt_SdtWalletInfo3 = AV22walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV22walletInfo = GXt_SdtWalletInfo3;
         if ( AV14group_sdt.gxTpr_Grouptype == 30 )
         {
            GXt_SdtExtKeyInfo4 = AV24extKeyInfoRoot;
            new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo4) ;
            AV24extKeyInfoRoot = GXt_SdtExtKeyInfo4;
            GXt_char5 = AV9error;
            new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV24extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV22walletInfo.gxTpr_Networktype,  "2", out  AV12extPubKeyInfo, out  GXt_char5) ;
            AV9error = GXt_char5;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV14group_sdt.gxTpr_Extpubkeymultisigreceiving = AV12extPubKeyInfo.gxTpr_Publickeytaproot;
               GXt_char5 = AV9error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV24extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV22walletInfo.gxTpr_Networktype,  "3", out  AV12extPubKeyInfo, out  GXt_char5) ;
               AV9error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV14group_sdt.gxTpr_Extpubkeymultisigchange = AV12extPubKeyInfo.gxTpr_Publickeytaproot;
               }
               else
               {
                  AV9error = "We couldn't create the Change Extended Public Key for the multisignature: " + AV9error;
                  cleanup();
                  if (true) return;
               }
            }
            else
            {
               AV9error = "We couldn't create the Receiving Extended Public Key for the multisignature: " + AV9error;
               cleanup();
               if (true) return;
            }
         }
         else
         {
            GXt_SdtExtKeyInfo4 = AV11extKeyInfoRootBIP48;
            new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo4) ;
            AV11extKeyInfoRootBIP48 = GXt_SdtExtKeyInfo4;
            GXt_char5 = AV9error;
            new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV11extKeyInfoRootBIP48.gxTpr_Extended.gxTpr_Nuterpublickey,  AV22walletInfo.gxTpr_Networktype,  "0", out  AV12extPubKeyInfo, out  GXt_char5) ;
            AV9error = GXt_char5;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV14group_sdt.gxTpr_Extpubkeymultisigreceiving = AV12extPubKeyInfo.gxTpr_Publickey;
               GXt_char5 = AV9error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV11extKeyInfoRootBIP48.gxTpr_Extended.gxTpr_Nuterpublickey,  AV22walletInfo.gxTpr_Networktype,  "1", out  AV12extPubKeyInfo, out  GXt_char5) ;
               AV9error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV14group_sdt.gxTpr_Extpubkeymultisigchange = AV12extPubKeyInfo.gxTpr_Publickey;
               }
               else
               {
                  AV9error = "We couldn't create the Change Extended Public Key for the Legacy multisignature: " + AV9error;
                  cleanup();
                  if (true) return;
               }
            }
            else
            {
               AV9error = "We couldn't create the Receiving Extended Public Key for the Legacy multisignature: " + AV9error;
               cleanup();
               if (true) return;
            }
         }
         AV14group_sdt.gxTpr_Isactive = true;
         AV14group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving = AV14group_sdt.gxTpr_Extpubkeymultisigreceiving;
         AV14group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange = AV14group_sdt.gxTpr_Extpubkeymultisigchange;
         AV14group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         if ( AV14group_sdt.gxTpr_Grouptype == 30 )
         {
            GXt_SdtDelegatedAddressResult6 = AV23addressResult;
            new GeneXus.Programs.wallet.registered.deriveaddressdelegated(context ).execute(  AV14group_sdt,  AV22walletInfo.gxTpr_Networktype,  0,  false, out  GXt_SdtDelegatedAddressResult6) ;
            AV23addressResult = GXt_SdtDelegatedAddressResult6;
            if ( ! AV23addressResult.gxTpr_Success )
            {
               AV9error = AV23addressResult.gxTpr_Error;
               cleanup();
               if (true) return;
            }
         }
         AV15group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15group_sdt_temp.gxTpr_Groupname = AV14group_sdt.gxTpr_Groupname;
         AV15group_sdt_temp.gxTpr_Grouptype = AV14group_sdt.gxTpr_Grouptype;
         AV15group_sdt_temp.gxTpr_Minimumshares = AV14group_sdt.gxTpr_Minimumshares;
         AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV14group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving = AV14group_sdt.gxTpr_Extpubkeymultisigreceiving;
         AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange = AV14group_sdt.gxTpr_Extpubkeymultisigchange;
         AV13firstInvitationSent = (DateTime)(DateTime.MinValue);
         AV25GXV1 = 1;
         while ( AV25GXV1 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV25GXV1));
            if ( (DateTime.MinValue==AV16groupContact.gxTpr_Contactinvitationsent) )
            {
               AV16groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
            }
            AV16groupContact.gxTpr_Contactinvisent = true;
            if ( (DateTime.MinValue==AV13firstInvitationSent) || ( AV16groupContact.gxTpr_Contactinvitationsent < AV13firstInvitationSent ) )
            {
               AV13firstInvitationSent = AV16groupContact.gxTpr_Contactinvitationsent;
            }
            AV25GXV1 = (int)(AV25GXV1+1);
         }
         AV20oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV20oneGroupContact.gxTpr_Contactid = AV14group_sdt.gxTpr_Groupid;
         AV20oneGroupContact.gxTpr_Contactgroupid = AV14group_sdt.gxTpr_Groupid;
         AV20oneGroupContact.gxTpr_Contactusername = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV20oneGroupContact.gxTpr_Contactuserpubkey = StringUtil.Trim( AV10externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         AV20oneGroupContact.gxTpr_Extpubkeymultisigreceiving = AV14group_sdt.gxTpr_Extpubkeymultisigreceiving;
         AV20oneGroupContact.gxTpr_Extpubkeymultisigchange = AV14group_sdt.gxTpr_Extpubkeymultisigchange;
         AV20oneGroupContact.gxTpr_Contactinvitationsent = AV13firstInvitationSent;
         AV20oneGroupContact.gxTpr_Contactinvitacionaccepted = DateTimeUtil.Now( context);
         AV20oneGroupContact.gxTpr_Contactinvisent = true;
         AV14group_sdt.gxTpr_Contact.Add(AV20oneGroupContact, 0);
         AV26GXV2 = 1;
         while ( AV26GXV2 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV20oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV26GXV2));
            AV15group_sdt_temp.gxTpr_Contact.Add((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV20oneGroupContact.Clone()), 0);
            AV26GXV2 = (int)(AV26GXV2+1);
         }
         AV19message_signature.gxTpr_Username = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV19message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV10externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char5 = AV9error;
         GXt_char7 = AV19message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV19message_signature.gxTpr_Username)+StringUtil.Trim( AV19message_signature.gxTpr_Grouppubkey), out  GXt_char7, out  GXt_char5) ;
         AV19message_signature.gxTpr_Signature = GXt_char7;
         AV9error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "There was a problem Signing the invitation: " + AV9error;
            cleanup();
            if (true) return;
         }
         AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV19message_signature.gxTpr_Signature);
         AV27GXV3 = 1;
         while ( AV27GXV3 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV27GXV3));
            if ( ! ( AV16groupContact.gxTpr_Contactid == AV16groupContact.gxTpr_Contactgroupid ) )
            {
               AV21sdt_message.gxTpr_Id = Guid.NewGuid( );
               GXt_int8 = 0;
               new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int8) ;
               AV21sdt_message.gxTpr_Datetimeunix = GXt_int8;
               AV21sdt_message.gxTpr_Messagetype = 90;
               AV21sdt_message.gxTpr_Message = AV15group_sdt_temp.ToJSonString(false, true);
               AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
               AV8contact.gxTpr_Username = StringUtil.Trim( AV16groupContact.gxTpr_Contactusername);
               AV8contact.gxTpr_Messagepubkey = StringUtil.Trim( AV16groupContact.gxTpr_Contactuserpubkey);
               GXt_char7 = AV9error;
               new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV8contact,  AV21sdt_message, out  GXt_char7) ;
               AV9error = GXt_char7;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV9error = "There was a problem sending the Activation to the Group: " + AV9error;
                  cleanup();
                  if (true) return;
               }
            }
            AV27GXV3 = (int)(AV27GXV3+1);
         }
         GXt_char7 = AV9error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  StringUtil.Trim( AV14group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV18grpupId, out  GXt_char7) ;
         AV9error = GXt_char7;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char7 = AV9error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV14group_sdt, out  GXt_char7) ;
            AV9error = GXt_char7;
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
         AV14group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV22walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV24extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV12extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV11extKeyInfoRootBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo4 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV23addressResult = new GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult(context);
         GXt_SdtDelegatedAddressResult6 = new GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult(context);
         AV15group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13firstInvitationSent = (DateTime)(DateTime.MinValue);
         AV16groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV20oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV19message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char5 = "";
         AV21sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV8contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV18grpupId = Guid.Empty;
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private int AV25GXV1 ;
      private int AV26GXV2 ;
      private int AV27GXV3 ;
      private long GXt_int8 ;
      private string AV9error ;
      private string GXt_char5 ;
      private string GXt_char7 ;
      private DateTime AV13firstInvitationSent ;
      private Guid AV17groupId ;
      private Guid AV18grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV22walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV24extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV12extPubKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV11extKeyInfoRootBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo4 ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult AV23addressResult ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult GXt_SdtDelegatedAddressResult6 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV20oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV19message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV21sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV8contact ;
      private string aP1_error ;
   }

}
