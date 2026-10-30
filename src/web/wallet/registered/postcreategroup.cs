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
   public class postcreategroup : GXProcedure
   {
      public postcreategroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public postcreategroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( SdtcreateGroup__postInput aP0_body ,
                           out Guid aP1_groupId ,
                           out string aP2_error )
      {
         this.AV9body = aP0_body;
         this.AV19groupId = Guid.Empty ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_groupId=this.AV19groupId;
         aP2_error=this.AV10error;
      }

      public string executeUdp( SdtcreateGroup__postInput aP0_body ,
                                out Guid aP1_groupId )
      {
         execute(aP0_body, out aP1_groupId, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( SdtcreateGroup__postInput aP0_body ,
                                 out Guid aP1_groupId ,
                                 out string aP2_error )
      {
         this.AV9body = aP0_body;
         this.AV19groupId = Guid.Empty ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_groupId=this.AV19groupId;
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV11externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV11externalUser = GXt_SdtExternalUser1;
         AV15localVarPath = "/createGroup";
         AV14localVarHeaders.Set("Authorization", new GeneXus.Programs.openapicommon.varchartojsonformat(context).executeUdp(  AV11externalUser.gxTpr_Externaltoken));
         GXt_SdtApiResponse2 = AV18localVarResponse;
         GXt_char3 = "";
         GXt_char4 = "";
         new GeneXus.Programs.distcrypt.getserverbaseurl(context ).execute( out  GXt_char4) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char4+"//DesktopApp/services/rest",  AV20ServerUrlTemplatingVar, out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.callapi(context ).execute(  "POST",  GXt_char3+AV15localVarPath,  AV14localVarHeaders,  AV16localVarPathParams,  AV17localVarQueryParams,  AV12localFileFormParams,  AV13localVarFormParams,  AV9body.ToJSonString(false, true),  "",  0,  0, out  GXt_SdtApiResponse2) ;
         AV18localVarResponse = GXt_SdtApiResponse2;
         AV8localVarStatusCode = (short)(AV18localVarResponse.gxTpr_Statuscode);
         if ( AV8localVarStatusCode == 200 )
         {
            AV21createGroup__postOutputOUT.FromJSonString(AV18localVarResponse.gxTpr_Content, null);
            AV10error = AV21createGroup__postOutputOUT.gxTpr_Error;
            AV19groupId = AV21createGroup__postOutputOUT.gxTpr_Groupid;
         }
         else
         {
            if ( AV8localVarStatusCode == 404 )
            {
               AV10error = "Not found";
            }
            else
            {
               AV10error = AV18localVarResponse.gxTpr_Errormessage;
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
         AV19groupId = Guid.Empty;
         AV10error = "";
         AV11externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV15localVarPath = "";
         AV14localVarHeaders = new GXProperties();
         AV18localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse2 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV20ServerUrlTemplatingVar = new GXProperties();
         AV16localVarPathParams = new GXProperties();
         AV17localVarQueryParams = new GXProperties();
         AV12localFileFormParams = new GXProperties();
         AV13localVarFormParams = new GXProperties();
         AV21createGroup__postOutputOUT = new SdtcreateGroup__postOutput(context);
         /* GeneXus formulas. */
      }

      private short AV8localVarStatusCode ;
      private string AV10error ;
      private string AV15localVarPath ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private Guid AV19groupId ;
      private GXProperties AV14localVarHeaders ;
      private GXProperties AV20ServerUrlTemplatingVar ;
      private GXProperties AV16localVarPathParams ;
      private GXProperties AV17localVarQueryParams ;
      private GXProperties AV12localFileFormParams ;
      private GXProperties AV13localVarFormParams ;
      private SdtcreateGroup__postInput AV9body ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV11externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV18localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse2 ;
      private SdtcreateGroup__postOutput AV21createGroup__postOutputOUT ;
      private Guid aP1_groupId ;
      private string aP2_error ;
   }

}
