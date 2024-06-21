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
namespace GeneXus.Programs.nostr {
   public class publish : GXProcedure
   {
      public publish( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public publish( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_topic ,
                           string aP1_message ,
                           out string aP2_error )
      {
         this.AV11topic = aP0_topic;
         this.AV13message = aP1_message;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_topic ,
                                string aP1_message )
      {
         execute(aP0_topic, aP1_message, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_topic ,
                                 string aP1_message ,
                                 out string aP2_error )
      {
         this.AV11topic = aP0_topic;
         this.AV13message = aP1_message;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV19externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV19externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19externalUser.gxTpr_Externaltoken)) )
         {
            AV17nostrEvent.gxTpr_Pubkey = StringUtil.Trim( AV19externalUser.gxTpr_Keyinfo.gxTpr_Taprootpubkey);
            GXt_int2 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimesecondsutc(context ).execute( out  GXt_int2) ;
            AV17nostrEvent.gxTpr_Created_at = GXt_int2;
            AV17nostrEvent.gxTpr_Kind = 4;
            AV18tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
            AV18tagsoftags.gxTpr_Tag.Add("p", 0);
            AV18tagsoftags.gxTpr_Tag.Add(StringUtil.Trim( AV11topic), 0);
            AV17nostrEvent.gxTpr_Tags.Add(AV18tagsoftags, 0);
            AV17nostrEvent.gxTpr_Content = AV24HexaEncoder.tohexa(AV13message);
            GXt_char3 = AV8error;
            new GeneXus.Programs.nostr.serializeevent(context ).execute( ref  AV17nostrEvent,  AV19externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV20messageSend, out  GXt_char3) ;
            AV8error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_guid4 = AV12Key;
               new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid4) ;
               AV12Key = GXt_guid4;
               AV22WebSocketOperationResult = GxNostrLib.sendmessageasync(AV12Key, AV20messageSend);
               if ( ! AV22WebSocketOperationResult.gxTpr_Success )
               {
                  GX_msglist.addItem("error sending message: "+AV22WebSocketOperationResult.gxTpr_Errormessage);
               }
            }
            else
            {
               GX_msglist.addItem("error: "+AV8error);
            }
         }
         else
         {
            AV8error = "You are not legged in";
         }
         this.cleanup();
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
         AV8error = "";
         AV19externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV17nostrEvent = new GeneXus.Programs.nostr.SdtEvent(context);
         AV18tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
         AV24HexaEncoder = new GeneXus.Programs.securityapicommons.SdtHexaEncoder(context);
         GXt_char3 = "";
         AV20messageSend = "";
         AV12Key = Guid.Empty;
         GXt_guid4 = Guid.Empty;
         AV22WebSocketOperationResult = new GeneXus.Programs.nostr.SdtWebSocketOperationResult(context);
         GxNostrLib = new GeneXus.Programs.nostr.SdtGxNostrLib(context);
         /* GeneXus formulas. */
      }

      private long GXt_int2 ;
      private string AV11topic ;
      private string AV8error ;
      private string GXt_char3 ;
      private string AV13message ;
      private string AV20messageSend ;
      private Guid AV12Key ;
      private Guid GXt_guid4 ;
      private GeneXus.Programs.nostr.SdtGxNostrLib GxNostrLib ;
      private GeneXus.Programs.securityapicommons.SdtHexaEncoder AV24HexaEncoder ;
      private string aP2_error ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV19externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.nostr.SdtWebSocketOperationResult AV22WebSocketOperationResult ;
      private GeneXus.Programs.nostr.SdtEvent AV17nostrEvent ;
      private GeneXus.Programs.nostr.SdtTagsOfTags AV18tagsoftags ;
   }

}
