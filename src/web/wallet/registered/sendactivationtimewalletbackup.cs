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
namespace GeneXus.Programs.wallet.registered {
   public class sendactivationtimewalletbackup : GXProcedure
   {
      public sendactivationtimewalletbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendactivationtimewalletbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_secret1 ,
                           string aP2_encryptedSecret2 ,
                           out string aP3_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV22secret1 = aP1_secret1;
         this.AV10encryptedSecret2 = aP2_encryptedSecret2;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_secret1 ,
                                string aP2_encryptedSecret2 )
      {
         execute(aP0_groupId, aP1_secret1, aP2_encryptedSecret2, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_secret1 ,
                                 string aP2_encryptedSecret2 ,
                                 out string aP3_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV22secret1 = aP1_secret1;
         this.AV10encryptedSecret2 = aP2_encryptedSecret2;
         this.AV11error = "" ;
         SubmitImpl();
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV16groupId, out  GXt_SdtGroup_SDT1) ;
         AV13group_sdt = GXt_SdtGroup_SDT1;
         AV18hasContactEmptyShares = false;
         AV24totalUserShares = 0;
         AV26GXV1 = 1;
         while ( AV26GXV1 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV15groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV26GXV1));
            AV24totalUserShares = (short)(AV24totalUserShares+(AV15groupContact.gxTpr_Numshares));
            if ( AV15groupContact.gxTpr_Numshares == 0 )
            {
               AV18hasContactEmptyShares = true;
            }
            AV26GXV1 = (int)(AV26GXV1+1);
         }
         if ( AV18hasContactEmptyShares || ( AV24totalUserShares < AV13group_sdt.gxTpr_Minimumshares ) )
         {
            AV11error = "There is a problem with the number of shares ";
         }
         GXt_char2 = AV11error;
         new GeneXus.Programs.shamirss.createshares(context ).execute(  AV22secret1,  AV24totalUserShares,  AV13group_sdt.gxTpr_Minimumshares, out  AV23shares, ref  GXt_char2) ;
         AV11error += GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV8assignShares = 1;
            AV27GXV2 = 1;
            while ( AV27GXV2 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV15groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV27GXV2));
               AV25userShares.Clear();
               AV19i = 1;
               while ( AV19i <= AV15groupContact.gxTpr_Numshares )
               {
                  AV25userShares.Add(((string)AV23shares.Item(AV8assignShares)), 0);
                  AV8assignShares = (short)(AV8assignShares+1);
                  AV19i = (short)(AV19i+1);
               }
               GXt_char2 = AV11error;
               GXt_char3 = AV15groupContact.gxTpr_Contactencryptedkey;
               GXt_char4 = AV15groupContact.gxTpr_Contactencryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV25userShares.ToJSonString(false),  StringUtil.Trim( AV15groupContact.gxTpr_Contactuserpubkey), out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
               AV15groupContact.gxTpr_Contactencryptedkey = GXt_char3;
               AV15groupContact.gxTpr_Contactencryptedtext = GXt_char4;
               AV11error = GXt_char2;
               AV27GXV2 = (int)(AV27GXV2+1);
            }
            AV13group_sdt.gxTpr_Encryptedtextshare = AV10encryptedSecret2;
            GXt_SdtExternalUser5 = AV12externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser5) ;
            AV12externalUser = GXt_SdtExternalUser5;
            AV14group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV14group_sdt_temp.gxTpr_Groupname = AV13group_sdt.gxTpr_Groupname;
            AV14group_sdt_temp.gxTpr_Grouptype = AV13group_sdt.gxTpr_Grouptype;
            AV14group_sdt_temp.gxTpr_Minimumshares = AV13group_sdt.gxTpr_Minimumshares;
            AV14group_sdt_temp.gxTpr_Encryptedtextshare = AV13group_sdt.gxTpr_Encryptedtextshare;
            AV14group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV12externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV14group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV14group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
            AV20message_signature.gxTpr_Username = StringUtil.Trim( AV12externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV20message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV12externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            GXt_char4 = AV11error;
            GXt_char3 = AV20message_signature.gxTpr_Signature;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV12externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV20message_signature.gxTpr_Username)+StringUtil.Trim( AV20message_signature.gxTpr_Grouppubkey), out  GXt_char3, out  GXt_char4) ;
            AV20message_signature.gxTpr_Signature = GXt_char3;
            AV11error += GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV14group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV20message_signature.gxTpr_Signature);
               AV28GXV3 = 1;
               while ( AV28GXV3 <= AV13group_sdt.gxTpr_Contact.Count )
               {
                  AV15groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV28GXV3));
                  AV15groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
                  AV15groupContact.gxTpr_Contactinvisent = true;
                  AV21sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int6 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int6) ;
                  AV21sdt_message.gxTpr_Datetimeunix = GXt_int6;
                  AV21sdt_message.gxTpr_Messagetype = 90;
                  AV21sdt_message.gxTpr_Message = AV14group_sdt_temp.ToJSonString(false, true);
                  AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV9contact.gxTpr_Username = StringUtil.Trim( AV15groupContact.gxTpr_Contactusername);
                  AV9contact.gxTpr_Messagepubkey = StringUtil.Trim( AV15groupContact.gxTpr_Contactuserpubkey);
                  if ( ! AV13group_sdt.gxTpr_Isactive )
                  {
                     GXt_char4 = AV11error;
                     new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV9contact,  AV21sdt_message, out  GXt_char4) ;
                     AV11error += GXt_char4;
                  }
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV11error = "There was a problem sending the Activation to the Group: " + AV11error;
                     cleanup();
                     if (true) return;
                  }
                  AV28GXV3 = (int)(AV28GXV3+1);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV13group_sdt.gxTpr_Isactive = true;
                  GXt_char4 = AV11error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV13group_sdt,  StringUtil.Trim( AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV17grpupId, out  GXt_char4) ;
                  AV11error += GXt_char4;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     GXt_char4 = AV11error;
                     new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV13group_sdt, out  GXt_char4) ;
                     AV11error = GXt_char4;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
               }
            }
            else
            {
               AV11error = "There was a problem Signing the invitation: " + AV11error;
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
         AV11error = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV23shares = new GxSimpleCollection<string>();
         AV25userShares = new GxSimpleCollection<string>();
         GXt_char2 = "";
         AV12externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser5 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV14group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char3 = "";
         AV21sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV17grpupId = Guid.Empty;
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV24totalUserShares ;
      private short AV8assignShares ;
      private short AV19i ;
      private int AV26GXV1 ;
      private int AV27GXV2 ;
      private int AV28GXV3 ;
      private long GXt_int6 ;
      private string AV11error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV18hasContactEmptyShares ;
      private string AV22secret1 ;
      private string AV10encryptedSecret2 ;
      private Guid AV16groupId ;
      private Guid AV17grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15groupContact ;
      private GxSimpleCollection<string> AV23shares ;
      private GxSimpleCollection<string> AV25userShares ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV12externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser5 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV20message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV21sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private string aP3_error ;
   }

}
