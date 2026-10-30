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
   public class sendmusig2roommessage : GXProcedure
   {
      public sendmusig2roommessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendmusig2roommessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           string aP2_kind ,
                           string aP3_data ,
                           out string aP4_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20roomId = aP1_roomId;
         this.AV15kind = aP2_kind;
         this.AV8data = aP3_data;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP4_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_roomId ,
                                string aP2_kind ,
                                string aP3_data )
      {
         execute(aP0_groupId, aP1_roomId, aP2_kind, aP3_data, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_roomId ,
                                 string aP2_kind ,
                                 string aP3_data ,
                                 out string aP4_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20roomId = aP1_roomId;
         this.AV15kind = aP2_kind;
         this.AV8data = aP3_data;
         this.AV9error = "" ;
         SubmitImpl();
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV13groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) || ! AV11group_sdt.gxTpr_Isactive )
         {
            AV9error = "This group is not active";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic2;
         GXt_SdtWalletInfo3 = AV25walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV25walletInfo = GXt_SdtWalletInfo3;
         AV16message = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage(context);
         AV16message.gxTpr_Id = Guid.NewGuid( );
         AV16message.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV16message.gxTpr_Roomid = AV20roomId;
         AV16message.gxTpr_Fromusername = StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         GXt_int4 = 0;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int4) ;
         AV16message.gxTpr_Datetimeunix = GXt_int4;
         AV16message.gxTpr_Kind = StringUtil.Trim( AV15kind);
         AV16message.gxTpr_Data = AV8data;
         AV23signed = StringUtil.Trim( AV16message.gxTpr_Id.ToString()) + StringUtil.Trim( AV16message.gxTpr_Referencegroupid.ToString()) + StringUtil.Trim( AV16message.gxTpr_Roomid.ToString()) + StringUtil.Trim( StringUtil.Str( (decimal)(AV16message.gxTpr_Datetimeunix), 18, 0)) + StringUtil.Trim( AV16message.gxTpr_Kind) + AV16message.gxTpr_Data;
         GXt_char5 = AV9error;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  AV23signed, out  AV22signature, out  GXt_char5) ;
         AV9error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV9error = "The message could not be signed: " + AV9error;
            cleanup();
            if (true) return;
         }
         AV16message.gxTpr_Signature = StringUtil.Trim( AV22signature);
         new GeneXus.Programs.wallet.registered.applymusig2roommessage(context ).execute(  AV11group_sdt,  AV16message, out  AV19notice, out  AV18mustSendNonces, out  AV17mustFinalize, out  AV9error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            cleanup();
            if (true) return;
         }
         AV21sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV21sdt_message.gxTpr_Id = AV16message.gxTpr_Id;
         AV21sdt_message.gxTpr_Datetimeunix = AV16message.gxTpr_Datetimeunix;
         AV21sdt_message.gxTpr_Messagetype = 150;
         AV21sdt_message.gxTpr_Message = AV16message.ToJSonString(false, true);
         AV26GXV1 = 1;
         while ( AV26GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV12groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV26GXV1));
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV12groupContact.gxTpr_Contactusername), AV16message.gxTpr_Fromusername) == 0 ) )
            {
               AV14json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
               GXt_char5 = AV9error;
               GXt_char6 = AV14json_enc.gxTpr_Encryptedkey;
               GXt_char7 = AV14json_enc.gxTpr_Encryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV21sdt_message.ToJSonString(false, true),  StringUtil.Trim( AV12groupContact.gxTpr_Contactuserpubkey), out  GXt_char6, out  GXt_char7, out  GXt_char5) ;
               AV14json_enc.gxTpr_Encryptedkey = GXt_char6;
               AV14json_enc.gxTpr_Encryptedtext = GXt_char7;
               AV9error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  GXt_char7 = AV9error;
                  new GeneXus.Programs.nbitcoin.tapprootaddresstoxonlypubkey(context ).execute(  StringUtil.Trim( AV12groupContact.gxTpr_Contactusername),  AV25walletInfo.gxTpr_Networktype, out  AV24taprootPubKey, out  GXt_char7) ;
                  AV9error = GXt_char7;
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  if ( ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "here") == 0 ) || ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "start") == 0 ) || ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "nonces") == 0 ) || ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "partials") == 0 ) )
                  {
                     GXt_char7 = AV9error;
                     new GeneXus.Programs.nostr.publishephemeral(context ).execute(  AV24taprootPubKey,  AV14json_enc.ToJSonString(false, true), out  GXt_char7) ;
                     AV9error = GXt_char7;
                  }
                  else
                  {
                     GXt_char7 = AV9error;
                     new GeneXus.Programs.nostr.publish(context ).execute(  AV24taprootPubKey,  AV14json_enc.ToJSonString(false, true), out  GXt_char7) ;
                     AV9error = GXt_char7;
                  }
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV9error = "The message did not reach " + StringUtil.Trim( AV12groupContact.gxTpr_Contactusername) + ": " + AV9error;
                  cleanup();
                  if (true) return;
               }
            }
            AV26GXV1 = (int)(AV26GXV1+1);
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
         AV25walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV16message = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage(context);
         AV23signed = "";
         AV22signature = "";
         AV19notice = "";
         AV21sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV12groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GXt_char5 = "";
         GXt_char6 = "";
         AV24taprootPubKey = "";
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private int AV26GXV1 ;
      private long GXt_int4 ;
      private string AV9error ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string AV24taprootPubKey ;
      private string GXt_char7 ;
      private bool AV18mustSendNonces ;
      private bool AV17mustFinalize ;
      private string AV8data ;
      private string AV23signed ;
      private string AV15kind ;
      private string AV22signature ;
      private string AV19notice ;
      private Guid AV13groupId ;
      private Guid AV20roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV25walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage AV16message ;
      private GeneXus.Programs.nostr.SdtSDT_message AV21sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV12groupContact ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV14json_enc ;
      private string aP4_error ;
   }

}
