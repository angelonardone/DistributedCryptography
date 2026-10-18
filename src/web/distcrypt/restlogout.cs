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
   public class restlogout : GXProcedure
   {
      public restlogout( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public restlogout( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           string aP1_oAuthGXGAM ,
                           out Sdtlogout__postOutput aP2_logout__postOutputOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                           out bool aP4_IsSuccess )
      {
         this.AV20ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV19oAuthGXGAM = aP1_oAuthGXGAM;
         this.AV18logout__postOutputOUT = new Sdtlogout__postOutput(context) ;
         this.AV9HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV10IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP2_logout__postOutputOUT=this.AV18logout__postOutputOUT;
         aP3_HttpMessage=this.AV9HttpMessage;
         aP4_IsSuccess=this.AV10IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              string aP1_oAuthGXGAM ,
                              out Sdtlogout__postOutput aP2_logout__postOutputOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, aP1_oAuthGXGAM, out aP2_logout__postOutputOUT, out aP3_HttpMessage, out aP4_IsSuccess);
         return AV10IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 string aP1_oAuthGXGAM ,
                                 out Sdtlogout__postOutput aP2_logout__postOutputOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                                 out bool aP4_IsSuccess )
      {
         this.AV20ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV19oAuthGXGAM = aP1_oAuthGXGAM;
         this.AV18logout__postOutputOUT = new Sdtlogout__postOutput(context) ;
         this.AV9HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV10IsSuccess = false ;
         SubmitImpl();
         aP2_logout__postOutputOUT=this.AV18logout__postOutputOUT;
         aP3_HttpMessage=this.AV9HttpMessage;
         aP4_IsSuccess=this.AV10IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14localVarPath = "/logout";
         AV13localVarHeaders.Set("Authorization", StringUtil.Trim( AV19oAuthGXGAM));
         GXt_SdtApiResponse1 = AV17localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new GeneXus.Programs.distcrypt.getserverbaseurl(context ).execute( out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3+"//DesktopApp/services/rest",  AV20ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapionce(context ).execute(  "POST",  GXt_char2+AV14localVarPath,  AV13localVarHeaders,  AV15localVarPathParams,  AV16localVarQueryParams,  AV11localFileFormParams,  AV12localVarFormParams,  "{}",  "",  0,  0, out  GXt_SdtApiResponse1) ;
         AV17localVarResponse = GXt_SdtApiResponse1;
         AV8localVarStatusCode = (short)(AV17localVarResponse.gxTpr_Statuscode);
         if ( ( AV8localVarStatusCode >= 200 ) && ( AV8localVarStatusCode < 300 ) )
         {
            AV10IsSuccess = true;
         }
         else
         {
            AV10IsSuccess = false;
            AV9HttpMessage.gxTpr_Type = 1;
         }
         if ( AV8localVarStatusCode == 200 )
         {
            AV9HttpMessage.gxTpr_Id = "200";
            AV9HttpMessage.gxTpr_Description = "Successful operation";
            AV18logout__postOutputOUT.FromJSonString(AV17localVarResponse.gxTpr_Content, null);
         }
         else
         {
            AV9HttpMessage.gxTpr_Description = AV17localVarResponse.gxTpr_Errormessage;
            AV9HttpMessage.gxTpr_Id = StringUtil.Str( (decimal)(AV8localVarStatusCode), 4, 0);
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
         AV18logout__postOutputOUT = new Sdtlogout__postOutput(context);
         AV9HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV14localVarPath = "";
         AV13localVarHeaders = new GXProperties();
         AV17localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char2 = "";
         GXt_char3 = "";
         AV15localVarPathParams = new GXProperties();
         AV16localVarQueryParams = new GXProperties();
         AV11localFileFormParams = new GXProperties();
         AV12localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV8localVarStatusCode ;
      private string AV14localVarPath ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private bool AV10IsSuccess ;
      private string AV19oAuthGXGAM ;
      private GXProperties AV20ServerUrlTemplatingVar ;
      private GXProperties AV13localVarHeaders ;
      private GXProperties AV15localVarPathParams ;
      private GXProperties AV16localVarQueryParams ;
      private GXProperties AV11localFileFormParams ;
      private GXProperties AV12localVarFormParams ;
      private Sdtlogout__postOutput AV18logout__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV9HttpMessage ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV17localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private Sdtlogout__postOutput aP2_logout__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ;
      private bool aP4_IsSuccess ;
   }

}
