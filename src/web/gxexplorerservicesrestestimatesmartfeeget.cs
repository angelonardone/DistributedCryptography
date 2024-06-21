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
   public class gxexplorerservicesrestestimatesmartfeeget : GXProcedure
   {
      public gxexplorerservicesrestestimatesmartfeeget( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gxexplorerservicesrestestimatesmartfeeget( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           int aP1_Estbasedonblocks ,
                           string aP2_Estimate_Mode ,
                           out SdtEstimateSmartFee__getOutput aP3_EstimateSmartFee__getOutputOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ,
                           out bool aP5_IsSuccess )
      {
         this.AV11ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8Estbasedonblocks = aP1_Estbasedonblocks;
         this.AV9Estimate_Mode = aP2_Estimate_Mode;
         this.AV10EstimateSmartFee__getOutputOUT = new SdtEstimateSmartFee__getOutput(context) ;
         this.AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV20IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP3_EstimateSmartFee__getOutputOUT=this.AV10EstimateSmartFee__getOutputOUT;
         aP4_HttpMessage=this.AV21HttpMessage;
         aP5_IsSuccess=this.AV20IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              int aP1_Estbasedonblocks ,
                              string aP2_Estimate_Mode ,
                              out SdtEstimateSmartFee__getOutput aP3_EstimateSmartFee__getOutputOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, aP1_Estbasedonblocks, aP2_Estimate_Mode, out aP3_EstimateSmartFee__getOutputOUT, out aP4_HttpMessage, out aP5_IsSuccess);
         return AV20IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 int aP1_Estbasedonblocks ,
                                 string aP2_Estimate_Mode ,
                                 out SdtEstimateSmartFee__getOutput aP3_EstimateSmartFee__getOutputOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ,
                                 out bool aP5_IsSuccess )
      {
         this.AV11ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8Estbasedonblocks = aP1_Estbasedonblocks;
         this.AV9Estimate_Mode = aP2_Estimate_Mode;
         this.AV10EstimateSmartFee__getOutputOUT = new SdtEstimateSmartFee__getOutput(context) ;
         this.AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV20IsSuccess = false ;
         SubmitImpl();
         aP3_EstimateSmartFee__getOutputOUT=this.AV10EstimateSmartFee__getOutputOUT;
         aP4_HttpMessage=this.AV21HttpMessage;
         aP5_IsSuccess=this.AV20IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12localVarPath = "/EstimateSmartFee";
         AV14localVarQueryParams.Set("Estbasedonblocks", new GeneXus.Programs.openapicommon.numerictojsonformat(context).executeUdp(  (short)(AV8Estbasedonblocks)));
         AV14localVarQueryParams.Set("Estimate_Mode", new GeneXus.Programs.openapicommon.varchartojsonformat(context).executeUdp(  AV9Estimate_Mode));
         GXt_SdtApiResponse1 = AV15localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new apibaseurllocalhost(context ).execute(  StringUtil.Format( "%1 - %2", StringUtil.Trim( AV12localVarPath), "GET", "", "", "", "", "", "", ""), out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3,  AV11ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapi(context ).execute(  "GET",  GXt_char2+AV12localVarPath,  AV19localVarHeaders,  AV13localVarPathParams,  AV14localVarQueryParams,  AV18localFileFormParams,  AV17localVarFormParams,  "",  "",  0,  0, out  GXt_SdtApiResponse1) ;
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
            AV10EstimateSmartFee__getOutputOUT.FromJSonString(AV15localVarResponse.gxTpr_Content, null);
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
         AV10EstimateSmartFee__getOutputOUT = new SdtEstimateSmartFee__getOutput(context);
         AV21HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV12localVarPath = "";
         AV14localVarQueryParams = new GXProperties();
         AV15localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char2 = "";
         GXt_char3 = "";
         AV19localVarHeaders = new GXProperties();
         AV13localVarPathParams = new GXProperties();
         AV18localFileFormParams = new GXProperties();
         AV17localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV16localVarStatusCode ;
      private int AV8Estbasedonblocks ;
      private string AV12localVarPath ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private bool AV20IsSuccess ;
      private string AV9Estimate_Mode ;
      private SdtEstimateSmartFee__getOutput aP3_EstimateSmartFee__getOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message aP4_HttpMessage ;
      private bool aP5_IsSuccess ;
      private GXProperties AV11ServerUrlTemplatingVar ;
      private GXProperties AV14localVarQueryParams ;
      private GXProperties AV19localVarHeaders ;
      private GXProperties AV13localVarPathParams ;
      private GXProperties AV18localFileFormParams ;
      private GXProperties AV17localVarFormParams ;
      private SdtEstimateSmartFee__getOutput AV10EstimateSmartFee__getOutputOUT ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV15localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private GeneXus.Utils.SdtMessages_Message AV21HttpMessage ;
   }

}
