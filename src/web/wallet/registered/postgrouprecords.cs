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
   public class postgrouprecords : GXProcedure
   {
      public postgrouprecords( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public postgrouprecords( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_method ,
                           GeneXus.Programs.wallet.registered.SdtGroupRecordRequest aP1_body ,
                           out GeneXus.Programs.wallet.registered.SdtGroupRecordResponse aP2_response ,
                           out string aP3_error )
      {
         this.AV19method = aP0_method;
         this.AV8body = aP1_body;
         this.AV20response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context) ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_response=this.AV20response;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_method ,
                                GeneXus.Programs.wallet.registered.SdtGroupRecordRequest aP1_body ,
                                out GeneXus.Programs.wallet.registered.SdtGroupRecordResponse aP2_response )
      {
         execute(aP0_method, aP1_body, out aP2_response, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_method ,
                                 GeneXus.Programs.wallet.registered.SdtGroupRecordRequest aP1_body ,
                                 out GeneXus.Programs.wallet.registered.SdtGroupRecordResponse aP2_response ,
                                 out string aP3_error )
      {
         this.AV19method = aP0_method;
         this.AV8body = aP1_body;
         this.AV20response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context) ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_response=this.AV20response;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         GXt_SdtExternalUser1 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV10externalUser = GXt_SdtExternalUser1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10externalUser.gxTpr_Externaltoken)) )
         {
            AV9error = "You are not logged into Distributed Cryptography main site";
            cleanup();
            if (true) return;
         }
         AV14localVarPath = "/" + StringUtil.Trim( AV19method);
         AV13localVarHeaders.Set("Authorization", new GeneXus.Programs.openapicommon.varchartojsonformat(context).executeUdp(  AV10externalUser.gxTpr_Externaltoken));
         GXt_SdtApiResponse2 = AV17localVarResponse;
         GXt_char3 = "";
         GXt_char4 = "";
         new GeneXus.Programs.distcrypt.getserverbaseurl(context ).execute( out  GXt_char4) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char4+"//DesktopApp/services/rest",  AV21ServerUrlTemplatingVar, out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.callapi(context ).execute(  "POST",  GXt_char3+AV14localVarPath,  AV13localVarHeaders,  AV15localVarPathParams,  AV16localVarQueryParams,  AV11localFileFormParams,  AV12localVarFormParams,  AV8body.ToJSonString(false, true),  "",  0,  0, out  GXt_SdtApiResponse2) ;
         AV17localVarResponse = GXt_SdtApiResponse2;
         AV18localVarStatusCode = (short)(AV17localVarResponse.gxTpr_Statuscode);
         if ( AV18localVarStatusCode == 200 )
         {
            AV20response.FromJSonString(AV17localVarResponse.gxTpr_Content, null);
            AV9error = StringUtil.Trim( AV20response.gxTpr_Error);
         }
         else
         {
            if ( AV18localVarStatusCode == 404 )
            {
               AV9error = "The server does not know the shared records yet (" + StringUtil.Trim( AV19method) + ")";
            }
            else
            {
               AV9error = AV17localVarResponse.gxTpr_Errormessage;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV9error))) )
               {
                  AV9error = "The server answered " + StringUtil.Trim( StringUtil.Str( (decimal)(AV18localVarStatusCode), 4, 0));
               }
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
         AV20response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context);
         AV9error = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV14localVarPath = "";
         AV13localVarHeaders = new GXProperties();
         AV17localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse2 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV21ServerUrlTemplatingVar = new GXProperties();
         AV15localVarPathParams = new GXProperties();
         AV16localVarQueryParams = new GXProperties();
         AV11localFileFormParams = new GXProperties();
         AV12localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV18localVarStatusCode ;
      private string AV9error ;
      private string AV14localVarPath ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private string AV19method ;
      private GXProperties AV13localVarHeaders ;
      private GXProperties AV21ServerUrlTemplatingVar ;
      private GXProperties AV15localVarPathParams ;
      private GXProperties AV16localVarQueryParams ;
      private GXProperties AV11localFileFormParams ;
      private GXProperties AV12localVarFormParams ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordRequest AV8body ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse AV20response ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV17localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse2 ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse aP2_response ;
      private string aP3_error ;
   }

}
