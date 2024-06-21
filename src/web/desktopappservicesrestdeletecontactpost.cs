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
namespace GeneXus.Programs {
   public class desktopappservicesrestdeletecontactpost : GXProcedure
   {
      public desktopappservicesrestdeletecontactpost( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public desktopappservicesrestdeletecontactpost( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           SdtdeleteContact__postInput aP1_body ,
                           string aP2_oAuthGXGAM ,
                           out SdtdeleteContact__postOutput aP3_deleteContact__postOutputOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ,
                           out bool aP5_IsSuccess )
      {
         this.AV11ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8body = aP1_body;
         this.AV9oAuthGXGAM = aP2_oAuthGXGAM;
         this.AV10deleteContact__postOutputOUT = new SdtdeleteContact__postOutput(context) ;
         this.AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV20IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP3_deleteContact__postOutputOUT=this.AV10deleteContact__postOutputOUT;
         aP4_HttpMessage=this.AV21HttpMessage;
         aP5_IsSuccess=this.AV20IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              SdtdeleteContact__postInput aP1_body ,
                              string aP2_oAuthGXGAM ,
                              out SdtdeleteContact__postOutput aP3_deleteContact__postOutputOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, aP1_body, aP2_oAuthGXGAM, out aP3_deleteContact__postOutputOUT, out aP4_HttpMessage, out aP5_IsSuccess);
         return AV20IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 SdtdeleteContact__postInput aP1_body ,
                                 string aP2_oAuthGXGAM ,
                                 out SdtdeleteContact__postOutput aP3_deleteContact__postOutputOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ,
                                 out bool aP5_IsSuccess )
      {
         this.AV11ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8body = aP1_body;
         this.AV9oAuthGXGAM = aP2_oAuthGXGAM;
         this.AV10deleteContact__postOutputOUT = new SdtdeleteContact__postOutput(context) ;
         this.AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV20IsSuccess = false ;
         SubmitImpl();
         aP3_deleteContact__postOutputOUT=this.AV10deleteContact__postOutputOUT;
         aP4_HttpMessage=this.AV21HttpMessage;
         aP5_IsSuccess=this.AV20IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12localVarPath = "/deleteContact";
         AV19localVarHeaders.Set("Authorization", new GeneXus.Programs.openapicommon.varchartojsonformat(context).executeUdp(  AV9oAuthGXGAM));
         GXt_SdtApiResponse1 = AV15localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new apibaseurlauth(context ).execute(  StringUtil.Format( "%1 - %2", StringUtil.Trim( AV12localVarPath), "POST", "", "", "", "", "", "", ""), out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3,  AV11ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapi(context ).execute(  "POST",  GXt_char2+AV12localVarPath,  AV19localVarHeaders,  AV13localVarPathParams,  AV14localVarQueryParams,  AV18localFileFormParams,  AV17localVarFormParams,  AV8body.ToJSonString(false, true),  "",  0,  0, out  GXt_SdtApiResponse1) ;
         AV15localVarResponse = GXt_SdtApiResponse1;
         AV16localVarStatusCode = (short)(AV15localVarResponse.gxTpr_Statuscode);
         if ( ( AV16localVarStatusCode >= 200 ) && ( AV16localVarStatusCode < 300 ) )
         {
            AV20IsSuccess = true;
         }
         else
         {
            AV20IsSuccess = false;
            AV21HttpMessage.gxTpr_Type = 1;
         }
         if ( AV16localVarStatusCode == 200 )
         {
            AV21HttpMessage.gxTpr_Id = "200";
            AV21HttpMessage.gxTpr_Description = "Successful operation";
            AV10deleteContact__postOutputOUT.FromJSonString(AV15localVarResponse.gxTpr_Content, null);
         }
         else if ( AV16localVarStatusCode == 404 )
         {
            AV21HttpMessage.gxTpr_Id = "404";
            AV21HttpMessage.gxTpr_Description = "Not found";
         }
         else
         {
            AV21HttpMessage.gxTpr_Description = AV15localVarResponse.gxTpr_Errormessage;
            AV21HttpMessage.gxTpr_Id = StringUtil.Str( (decimal)(AV16localVarStatusCode), 4, 0);
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
         AV10deleteContact__postOutputOUT = new SdtdeleteContact__postOutput(context);
         AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV12localVarPath = "";
         AV19localVarHeaders = new GXProperties();
         AV15localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char2 = "";
         GXt_char3 = "";
         AV13localVarPathParams = new GXProperties();
         AV14localVarQueryParams = new GXProperties();
         AV18localFileFormParams = new GXProperties();
         AV17localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV16localVarStatusCode ;
      private string AV9oAuthGXGAM ;
      private string AV12localVarPath ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private bool AV20IsSuccess ;
      private SdtdeleteContact__postOutput aP3_deleteContact__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ;
      private bool aP5_IsSuccess ;
      private GXProperties AV11ServerUrlTemplatingVar ;
      private GXProperties AV19localVarHeaders ;
      private GXProperties AV13localVarPathParams ;
      private GXProperties AV14localVarQueryParams ;
      private GXProperties AV18localFileFormParams ;
      private GXProperties AV17localVarFormParams ;
      private SdtdeleteContact__postInput AV8body ;
      private SdtdeleteContact__postOutput AV10deleteContact__postOutputOUT ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV15localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private GeneXus.Utils.SdtMessages_Message AV21HttpMessage ;
   }

}
