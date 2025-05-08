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
   public class sendmessage : GXProcedure
   {
      public sendmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                           GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message ,
                           out string aP2_error )
      {
         this.AV21contact = aP0_contact;
         this.AV10sdt_message = aP1_sdt_message;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message )
      {
         execute(aP0_contact, aP1_sdt_message, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                 GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message ,
                                 out string aP2_error )
      {
         this.AV21contact = aP0_contact;
         this.AV10sdt_message = aP1_sdt_message;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV17externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV17externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17externalUser.gxTpr_Externaltoken)) )
         {
            GXt_char2 = AV9error;
            GXt_char3 = AV13body.gxTpr_Messageencryptedkey;
            GXt_char4 = AV13body.gxTpr_Messageencrypted;
            new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV10sdt_message.ToJSonString(false, true),  StringUtil.Trim( AV21contact.gxTpr_Messagepubkey), out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
            AV13body.gxTpr_Messageencryptedkey = GXt_char3;
            AV13body.gxTpr_Messageencrypted = GXt_char4;
            AV9error = GXt_char2;
            AV13body.gxTpr_Username = StringUtil.Trim( AV21contact.gxTpr_Username);
            new desktopappservicesrestsendmesagepost(context ).execute(  AV14ServerUrlTemplatingVar,  AV13body,  StringUtil.Trim( AV17externalUser.gxTpr_Externaltoken), out  AV16sendMesage__postOutputOUT, out  AV11HttpMessage, out  AV12IsSuccess) ;
            if ( ! AV12IsSuccess )
            {
               AV9error = AV11HttpMessage.gxTpr_Description;
            }
            else
            {
               AV9error = AV16sendMesage__postOutputOUT.gxTpr_Error;
               AV19MessageId = AV16sendMesage__postOutputOUT.gxTpr_Messageid;
            }
         }
         else
         {
            AV9error = "You are not logged into Distributed Cryptography main site";
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
         AV17externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         AV13body = new SdtsendMesage__postInput(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV14ServerUrlTemplatingVar = new GXProperties();
         AV16sendMesage__postOutputOUT = new SdtsendMesage__postOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV19MessageId = Guid.Empty;
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV12IsSuccess ;
      private Guid AV19MessageId ;
      private GXProperties AV14ServerUrlTemplatingVar ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV21contact ;
      private GeneXus.Programs.nostr.SdtSDT_message AV10sdt_message ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV17externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtsendMesage__postInput AV13body ;
      private SdtsendMesage__postOutput AV16sendMesage__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private string aP2_error ;
   }

}
