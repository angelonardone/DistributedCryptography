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
   public class deletemessage : GXProcedure
   {
      public deletemessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletemessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_messageId ,
                           out string aP1_error )
      {
         this.AV8messageId = aP0_messageId;
         this.AV15error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV15error;
      }

      public string executeUdp( Guid aP0_messageId )
      {
         execute(aP0_messageId, out aP1_error);
         return AV15error ;
      }

      public void executeSubmit( Guid aP0_messageId ,
                                 out string aP1_error )
      {
         this.AV8messageId = aP0_messageId;
         this.AV15error = "" ;
         SubmitImpl();
         aP1_error=this.AV15error;
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
            AV10body.gxTpr_Messageid = AV8messageId;
            new desktopappservicesrestdeletemesagepost(context ).execute(  AV14ServerUrlTemplatingVar,  AV10body,  AV9externalUser.gxTpr_Externaltoken, out  AV11deleteMesage__postOutputOUT, out  AV12HttpMessage, out  AV13IsSuccess) ;
            if ( ! AV13IsSuccess )
            {
               AV15error = AV12HttpMessage.gxTpr_Description;
            }
            else
            {
               AV15error = AV11deleteMesage__postOutputOUT.gxTpr_Error;
               if ( AV8messageId != AV11deleteMesage__postOutputOUT.gxTpr_Deletemessageid )
               {
                  AV15error = "The deleted message Id is diferent from the requested id : " + AV15error;
               }
            }
         }
         else
         {
            AV15error = "You are not logged into Distributed Cryptography main site";
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
         AV15error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV10body = new SdtdeleteMesage__postInput(context);
         AV14ServerUrlTemplatingVar = new GXProperties();
         AV11deleteMesage__postOutputOUT = new SdtdeleteMesage__postOutput(context);
         AV12HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV15error ;
      private bool AV13IsSuccess ;
      private Guid AV8messageId ;
      private GXProperties AV14ServerUrlTemplatingVar ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtdeleteMesage__postInput AV10body ;
      private SdtdeleteMesage__postOutput AV11deleteMesage__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV12HttpMessage ;
      private string aP1_error ;
   }

}
