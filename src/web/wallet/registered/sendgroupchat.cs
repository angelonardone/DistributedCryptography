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
   public class sendgroupchat : GXProcedure
   {
      public sendgroupchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendgroupchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_text ,
                           out string aP2_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20text = aP1_text;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_text )
      {
         execute(aP0_groupId, aP1_text, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_text ,
                                 out string aP2_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20text = aP1_text;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV13groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) || ! ( AV11group_sdt.gxTpr_Grouptype == 70 ) || ! AV11group_sdt.gxTpr_Isactive )
         {
            AV9error = "This is not an active Group Chat";
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV20text))) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic2;
         GXt_SdtWalletInfo3 = AV21walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV21walletInfo = GXt_SdtWalletInfo3;
         AV15message = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         AV15message.gxTpr_Id = Guid.NewGuid( );
         AV15message.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV15message.gxTpr_Fromusername = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         GXt_int4 = 0;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int4) ;
         AV15message.gxTpr_Datetimeunix = GXt_int4;
         AV15message.gxTpr_Text = StringUtil.Trim( AV20text);
         AV18signed = StringUtil.Trim( AV15message.gxTpr_Id.ToString()) + StringUtil.Trim( AV15message.gxTpr_Referencegroupid.ToString()) + StringUtil.Trim( StringUtil.Str( (decimal)(AV15message.gxTpr_Datetimeunix), 18, 0)) + StringUtil.Trim( AV15message.gxTpr_Text);
         GXt_char5 = AV9error;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  AV18signed, out  AV17signature, out  GXt_char5) ;
         AV9error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "The message could not be signed: " + AV9error;
            cleanup();
            if (true) return;
         }
         AV15message.gxTpr_Signature = StringUtil.Trim( AV17signature);
         new GeneXus.Programs.wallet.registered.appendgroupchat(context ).execute(  AV15message.gxTpr_Referencegroupid,  AV15message, out  AV8added) ;
         AV16sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV16sdt_message.gxTpr_Id = AV15message.gxTpr_Id;
         AV16sdt_message.gxTpr_Datetimeunix = AV15message.gxTpr_Datetimeunix;
         AV16sdt_message.gxTpr_Messagetype = 130;
         AV16sdt_message.gxTpr_Message = AV15message.ToJSonString(false, true);
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV12groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV22GXV1));
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV12groupContact.gxTpr_Contactusername), AV15message.gxTpr_Fromusername) == 0 ) )
            {
               AV14json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
               GXt_char5 = AV9error;
               GXt_char6 = AV14json_enc.gxTpr_Encryptedkey;
               GXt_char7 = AV14json_enc.gxTpr_Encryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV16sdt_message.ToJSonString(false, true),  StringUtil.Trim( AV12groupContact.gxTpr_Contactuserpubkey), out  GXt_char6, out  GXt_char7, out  GXt_char5) ;
               AV14json_enc.gxTpr_Encryptedkey = GXt_char6;
               AV14json_enc.gxTpr_Encryptedtext = GXt_char7;
               AV9error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  GXt_char7 = AV9error;
                  new GeneXus.Programs.nbitcoin.tapprootaddresstoxonlypubkey(context ).execute(  StringUtil.Trim( AV12groupContact.gxTpr_Contactusername),  AV21walletInfo.gxTpr_Networktype, out  AV19taprootPubKey, out  GXt_char7) ;
                  AV9error = GXt_char7;
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  GXt_char7 = AV9error;
                  new GeneXus.Programs.nostr.publish(context ).execute(  AV19taprootPubKey,  AV14json_enc.ToJSonString(false, true), out  GXt_char7) ;
                  AV9error = GXt_char7;
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV9error = "The message did not reach " + StringUtil.Trim( AV12groupContact.gxTpr_Contactusername) + ": " + AV9error;
                  cleanup();
                  if (true) return;
               }
            }
            AV22GXV1 = (int)(AV22GXV1+1);
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
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV21walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV15message = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         AV18signed = "";
         AV17signature = "";
         AV16sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV12groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GXt_char5 = "";
         GXt_char6 = "";
         AV19taprootPubKey = "";
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private long GXt_int4 ;
      private string AV9error ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string AV19taprootPubKey ;
      private string GXt_char7 ;
      private bool AV8added ;
      private string AV20text ;
      private string AV18signed ;
      private string AV17signature ;
      private Guid AV13groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV21walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV15message ;
      private GeneXus.Programs.nostr.SdtSDT_message AV16sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV12groupContact ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV14json_enc ;
      private string aP2_error ;
   }

}
