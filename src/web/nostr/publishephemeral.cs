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
namespace GeneXus.Programs.nostr {
   public class publishephemeral : GXProcedure
   {
      public publishephemeral( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public publishephemeral( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_topic ,
                           string aP1_message ,
                           out string aP2_error )
      {
         this.AV17topic = aP0_topic;
         this.AV12message = aP1_message;
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
         this.AV17topic = aP0_topic;
         this.AV12message = aP1_message;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUser.gxTpr_Externaltoken)) )
         {
            AV14nostrEvent.gxTpr_Pubkey = StringUtil.Trim( AV9externalUser.gxTpr_Keyinfo.gxTpr_Taprootpubkey);
            GXt_int2 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimesecondsutc(context ).execute( out  GXt_int2) ;
            AV14nostrEvent.gxTpr_Created_at = GXt_int2;
            AV14nostrEvent.gxTpr_Kind = 20004;
            AV16tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
            AV16tagsoftags.gxTpr_Tag.Add("p", 0);
            AV16tagsoftags.gxTpr_Tag.Add(StringUtil.Trim( AV17topic), 0);
            AV14nostrEvent.gxTpr_Tags.Add(AV16tagsoftags, 0);
            GXt_char3 = "";
            new GeneXus.Programs.distcrypt.texttohex(context ).execute(  StringUtil.Trim( AV12message), out  GXt_char3) ;
            AV14nostrEvent.gxTpr_Content = GXt_char3;
            GXt_char3 = AV8error;
            new GeneXus.Programs.nostr.serializeevent(context ).execute( ref  AV14nostrEvent,  AV9externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV13messageSend, out  GXt_char3) ;
            AV8error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_guid4 = AV11Key;
               new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid4) ;
               AV11Key = GXt_guid4;
               AV15OperationResult = GxInternetLibWs.sendmessage(AV11Key, AV13messageSend);
               if ( ! AV15OperationResult.gxTpr_Success )
               {
                  GX_msglist.addItem("error sending message: "+AV15OperationResult.gxTpr_Errormessage);
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
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV14nostrEvent = new GeneXus.Programs.nostr.SdtEvent(context);
         AV16tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
         GXt_char3 = "";
         AV13messageSend = "";
         AV11Key = Guid.Empty;
         GXt_guid4 = Guid.Empty;
         AV15OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         /* GeneXus formulas. */
      }

      private long GXt_int2 ;
      private string AV17topic ;
      private string AV8error ;
      private string GXt_char3 ;
      private string AV12message ;
      private string AV13messageSend ;
      private Guid AV11Key ;
      private Guid GXt_guid4 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.nostr.SdtEvent AV14nostrEvent ;
      private GeneXus.Programs.nostr.SdtTagsOfTags AV16tagsoftags ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV15OperationResult ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP2_error ;
   }

}
