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
namespace GeneXus.Programs.distcrypt {
   public class restlogin : GXProcedure
   {
      public restlogin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public restlogin( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           Sdtlogin__postInput aP1_body ,
                           out Sdtlogin__postOutput aP2_login__postOutputOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                           out bool aP4_IsSuccess )
      {
         this.AV20ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV9body = aP1_body;
         this.AV19login__postOutputOUT = new Sdtlogin__postOutput(context) ;
         this.AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV11IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP2_login__postOutputOUT=this.AV19login__postOutputOUT;
         aP3_HttpMessage=this.AV10HttpMessage;
         aP4_IsSuccess=this.AV11IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              Sdtlogin__postInput aP1_body ,
                              out Sdtlogin__postOutput aP2_login__postOutputOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, aP1_body, out aP2_login__postOutputOUT, out aP3_HttpMessage, out aP4_IsSuccess);
         return AV11IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 Sdtlogin__postInput aP1_body ,
                                 out Sdtlogin__postOutput aP2_login__postOutputOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                                 out bool aP4_IsSuccess )
      {
         this.AV20ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV9body = aP1_body;
         this.AV19login__postOutputOUT = new Sdtlogin__postOutput(context) ;
         this.AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV11IsSuccess = false ;
         SubmitImpl();
         aP2_login__postOutputOUT=this.AV19login__postOutputOUT;
         aP3_HttpMessage=this.AV10HttpMessage;
         aP4_IsSuccess=this.AV11IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15localVarPath = "/login";
         GXt_SdtApiResponse1 = AV18localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new GeneXus.Programs.distcrypt.getserverbaseurl(context ).execute( out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3+"//DesktopApp/services/rest",  AV20ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapionce(context ).execute(  "POST",  GXt_char2+AV15localVarPath,  AV14localVarHeaders,  AV16localVarPathParams,  AV17localVarQueryParams,  AV12localFileFormParams,  AV13localVarFormParams,  AV9body.ToJSonString(false, true),  "",  0,  0, out  GXt_SdtApiResponse1) ;
         AV18localVarResponse = GXt_SdtApiResponse1;
         AV8localVarStatusCode = (short)(AV18localVarResponse.gxTpr_Statuscode);
         if ( ( AV8localVarStatusCode >= 200 ) && ( AV8localVarStatusCode < 300 ) )
         {
            AV11IsSuccess = true;
         }
         else
         {
            AV11IsSuccess = false;
            AV10HttpMessage.gxTpr_Type = 1;
         }
         if ( AV8localVarStatusCode == 200 )
         {
            AV10HttpMessage.gxTpr_Id = "200";
            AV10HttpMessage.gxTpr_Description = "Successful operation";
            AV19login__postOutputOUT.FromJSonString(AV18localVarResponse.gxTpr_Content, null);
         }
         else
         {
            AV10HttpMessage.gxTpr_Description = AV18localVarResponse.gxTpr_Errormessage;
            AV10HttpMessage.gxTpr_Id = StringUtil.Str( (decimal)(AV8localVarStatusCode), 4, 0);
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
         AV19login__postOutputOUT = new Sdtlogin__postOutput(context);
         AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV15localVarPath = "";
         AV18localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char2 = "";
         GXt_char3 = "";
         AV14localVarHeaders = new GXProperties();
         AV16localVarPathParams = new GXProperties();
         AV17localVarQueryParams = new GXProperties();
         AV12localFileFormParams = new GXProperties();
         AV13localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV8localVarStatusCode ;
      private string AV15localVarPath ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private bool AV11IsSuccess ;
      private GXProperties AV20ServerUrlTemplatingVar ;
      private GXProperties AV14localVarHeaders ;
      private GXProperties AV16localVarPathParams ;
      private GXProperties AV17localVarQueryParams ;
      private GXProperties AV12localFileFormParams ;
      private GXProperties AV13localVarFormParams ;
      private Sdtlogin__postInput AV9body ;
      private Sdtlogin__postOutput AV19login__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV10HttpMessage ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV18localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private Sdtlogin__postOutput aP2_login__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ;
      private bool aP4_IsSuccess ;
   }

}
