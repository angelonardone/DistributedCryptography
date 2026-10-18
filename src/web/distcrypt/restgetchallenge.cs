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
   public class restgetchallenge : GXProcedure
   {
      public restgetchallenge( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public restgetchallenge( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           out SdtgetChallenge__postOutput aP1_getChallenge__postOutputOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP2_HttpMessage ,
                           out bool aP3_IsSuccess )
      {
         this.AV19ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV9getChallenge__postOutputOUT = new SdtgetChallenge__postOutput(context) ;
         this.AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV11IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP1_getChallenge__postOutputOUT=this.AV9getChallenge__postOutputOUT;
         aP2_HttpMessage=this.AV10HttpMessage;
         aP3_IsSuccess=this.AV11IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              out SdtgetChallenge__postOutput aP1_getChallenge__postOutputOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP2_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, out aP1_getChallenge__postOutputOUT, out aP2_HttpMessage, out aP3_IsSuccess);
         return AV11IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 out SdtgetChallenge__postOutput aP1_getChallenge__postOutputOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP2_HttpMessage ,
                                 out bool aP3_IsSuccess )
      {
         this.AV19ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV9getChallenge__postOutputOUT = new SdtgetChallenge__postOutput(context) ;
         this.AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV11IsSuccess = false ;
         SubmitImpl();
         aP1_getChallenge__postOutputOUT=this.AV9getChallenge__postOutputOUT;
         aP2_HttpMessage=this.AV10HttpMessage;
         aP3_IsSuccess=this.AV11IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15localVarPath = "/getChallenge";
         GXt_SdtApiResponse1 = AV18localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new GeneXus.Programs.distcrypt.getserverbaseurl(context ).execute( out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3+"//DesktopApp/services/rest",  AV19ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapionce(context ).execute(  "POST",  GXt_char2+AV15localVarPath,  AV14localVarHeaders,  AV16localVarPathParams,  AV17localVarQueryParams,  AV12localFileFormParams,  AV13localVarFormParams,  "{}",  "",  0,  0, out  GXt_SdtApiResponse1) ;
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
            AV9getChallenge__postOutputOUT.FromJSonString(AV18localVarResponse.gxTpr_Content, null);
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
         AV9getChallenge__postOutputOUT = new SdtgetChallenge__postOutput(context);
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
      private GXProperties AV19ServerUrlTemplatingVar ;
      private GXProperties AV14localVarHeaders ;
      private GXProperties AV16localVarPathParams ;
      private GXProperties AV17localVarQueryParams ;
      private GXProperties AV12localFileFormParams ;
      private GXProperties AV13localVarFormParams ;
      private SdtgetChallenge__postOutput AV9getChallenge__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV10HttpMessage ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV18localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private SdtgetChallenge__postOutput aP1_getChallenge__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message aP2_HttpMessage ;
      private bool aP3_IsSuccess ;
   }

}
