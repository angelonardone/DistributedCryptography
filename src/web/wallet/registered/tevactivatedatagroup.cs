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
   public class tevactivatedatagroup : GXProcedure
   {
      public tevactivatedatagroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public tevactivatedatagroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_secret1 ,
                           string aP2_encryptedSecret2 ,
                           out string aP3_error )
      {
         this.AV18groupId = aP0_groupId;
         this.AV23secret1 = aP1_secret1;
         this.AV12encryptedSecret2 = aP2_encryptedSecret2;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV13error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_secret1 ,
                                string aP2_encryptedSecret2 )
      {
         execute(aP0_groupId, aP1_secret1, aP2_encryptedSecret2, out aP3_error);
         return AV13error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_secret1 ,
                                 string aP2_encryptedSecret2 ,
                                 out string aP3_error )
      {
         this.AV18groupId = aP0_groupId;
         this.AV23secret1 = aP1_secret1;
         this.AV12encryptedSecret2 = aP2_encryptedSecret2;
         this.AV13error = "" ;
         SubmitImpl();
         aP3_error=this.AV13error;
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
            AV13error = "We couldn't find the restore group of the vault";
            cleanup();
            if (true) return;
         }
         AV20hasContactEmptyShares = false;
         AV10totalUserShares = 0;
         AV26GXV1 = 1;
         while ( AV26GXV1 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV26GXV1));
            AV10totalUserShares = (short)(AV10totalUserShares+(AV17groupContact.gxTpr_Numshares));
            if ( AV17groupContact.gxTpr_Numshares == 0 )
            {
               AV20hasContactEmptyShares = true;
            }
            AV26GXV1 = (int)(AV26GXV1+1);
         }
         if ( AV20hasContactEmptyShares || ( AV10totalUserShares < AV15group_sdt.gxTpr_Minimumshares ) || ( AV15group_sdt.gxTpr_Minimumshares < 2 ) )
         {
            AV13error = "There is a problem with the number of votes of the restore group";
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV13error;
         new GeneXus.Programs.shamirss.createshares(context ).execute(  AV23secret1,  AV10totalUserShares,  AV15group_sdt.gxTpr_Minimumshares, out  AV24shares, ref  GXt_char2) ;
         AV13error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8assignShares = 1;
         AV27GXV2 = 1;
         while ( AV27GXV2 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV27GXV2));
            AV25userShares.Clear();
            AV9i = 1;
            while ( AV9i <= AV17groupContact.gxTpr_Numshares )
            {
               AV25userShares.Add(((string)AV24shares.Item(AV8assignShares)), 0);
               AV8assignShares = (short)(AV8assignShares+1);
               AV9i = (short)(AV9i+1);
            }
            GXt_char2 = AV13error;
            GXt_char3 = AV17groupContact.gxTpr_Contactencryptedkey;
            GXt_char4 = AV17groupContact.gxTpr_Contactencryptedtext;
            new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV25userShares.ToJSonString(false),  StringUtil.Trim( AV17groupContact.gxTpr_Contactuserpubkey), out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
            AV17groupContact.gxTpr_Contactencryptedkey = GXt_char3;
            AV17groupContact.gxTpr_Contactencryptedtext = GXt_char4;
            AV13error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               cleanup();
               if (true) return;
            }
            AV27GXV2 = (int)(AV27GXV2+1);
         }
         AV15group_sdt.gxTpr_Encryptedtextshare = AV12encryptedSecret2;
         GXt_SdtExternalUserPublic5 = AV14externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic5) ;
         AV14externalUserPublic = GXt_SdtExternalUserPublic5;
         AV16group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16group_sdt_temp.gxTpr_Groupname = AV15group_sdt.gxTpr_Groupname;
         AV16group_sdt_temp.gxTpr_Grouptype = AV15group_sdt.gxTpr_Grouptype;
         AV16group_sdt_temp.gxTpr_Minimumshares = AV15group_sdt.gxTpr_Minimumshares;
         AV16group_sdt_temp.gxTpr_Encryptedtextshare = AV15group_sdt.gxTpr_Encryptedtextshare;
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV15group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV15group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
         AV21message_signature.gxTpr_Username = StringUtil.Trim( AV14externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV21message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV14externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char4 = AV13error;
         GXt_char3 = AV21message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV21message_signature.gxTpr_Username)+StringUtil.Trim( AV21message_signature.gxTpr_Grouppubkey), out  GXt_char3, out  GXt_char4) ;
         AV21message_signature.gxTpr_Signature = GXt_char3;
         AV13error = GXt_char4;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV13error = "There was a problem Signing the invitation: " + AV13error;
            cleanup();
            if (true) return;
         }
         AV16group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV21message_signature.gxTpr_Signature);
         AV28GXV3 = 1;
         while ( AV28GXV3 <= AV15group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV28GXV3));
            if ( (DateTime.MinValue==AV17groupContact.gxTpr_Contactinvitationsent) )
            {
               AV17groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
            }
            AV17groupContact.gxTpr_Contactinvisent = true;
            if ( ! AV15group_sdt.gxTpr_Isactive )
            {
               AV22sdt_message.gxTpr_Id = Guid.NewGuid( );
               GXt_int6 = 0;
               new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int6) ;
               AV22sdt_message.gxTpr_Datetimeunix = GXt_int6;
               AV22sdt_message.gxTpr_Messagetype = 90;
               AV22sdt_message.gxTpr_Message = AV16group_sdt_temp.ToJSonString(false, true);
               AV11contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
               AV11contact.gxTpr_Username = StringUtil.Trim( AV17groupContact.gxTpr_Contactusername);
               AV11contact.gxTpr_Messagepubkey = StringUtil.Trim( AV17groupContact.gxTpr_Contactuserpubkey);
               GXt_char4 = AV13error;
               new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV11contact,  AV22sdt_message, out  GXt_char4) ;
               AV13error = GXt_char4;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV13error = "There was a problem sending the Activation to the Group: " + AV13error;
                  cleanup();
                  if (true) return;
               }
            }
            AV28GXV3 = (int)(AV28GXV3+1);
         }
         AV15group_sdt.gxTpr_Isactive = true;
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV15group_sdt,  StringUtil.Trim( AV15group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV19grpupId, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char4 = AV13error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV15group_sdt, out  GXt_char4) ;
            AV13error = GXt_char4;
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
         AV13error = "";
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV24shares = new GxSimpleCollection<string>();
         AV25userShares = new GxSimpleCollection<string>();
         GXt_char2 = "";
         AV14externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic5 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV16group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char3 = "";
         AV22sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV11contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV19grpupId = Guid.Empty;
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV10totalUserShares ;
      private short AV8assignShares ;
      private short AV9i ;
      private int AV26GXV1 ;
      private int AV27GXV2 ;
      private int AV28GXV3 ;
      private long GXt_int6 ;
      private string AV13error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV20hasContactEmptyShares ;
      private string AV23secret1 ;
      private string AV12encryptedSecret2 ;
      private Guid AV18groupId ;
      private Guid AV19grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17groupContact ;
      private GxSimpleCollection<string> AV24shares ;
      private GxSimpleCollection<string> AV25userShares ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV14externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic5 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV21message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV22sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV11contact ;
      private string aP3_error ;
   }

}
