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
   public class gxexplorerservicesrestgettransactionspost : GXProcedure
   {
      public gxexplorerservicesrestgettransactionspost( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gxexplorerservicesrestgettransactionspost( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXProperties aP0_ServerUrlTemplatingVar ,
                           SdtGetTransactions__postInput aP1_body ,
                           out SdtGxExplorer_services_TxoutFromAddresses aP2_GxExplorer_services_TxoutFromAddressesOUT ,
                           out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                           out bool aP4_IsSuccess )
      {
         this.AV10ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8body = aP1_body;
         this.GxExplorer_services_TxoutFromAddressesOUT = new SdtGxExplorer_services_TxoutFromAddresses(context) ;
         this.AV20HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV19IsSuccess = false ;
         initialize();
         ExecuteImpl();
         aP2_GxExplorer_services_TxoutFromAddressesOUT=this.GxExplorer_services_TxoutFromAddressesOUT;
         aP3_HttpMessage=this.AV20HttpMessage;
         aP4_IsSuccess=this.AV19IsSuccess;
      }

      public bool executeUdp( GXProperties aP0_ServerUrlTemplatingVar ,
                              SdtGetTransactions__postInput aP1_body ,
                              out SdtGxExplorer_services_TxoutFromAddresses aP2_GxExplorer_services_TxoutFromAddressesOUT ,
                              out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage )
      {
         execute(aP0_ServerUrlTemplatingVar, aP1_body, out aP2_GxExplorer_services_TxoutFromAddressesOUT, out aP3_HttpMessage, out aP4_IsSuccess);
         return AV19IsSuccess ;
      }

      public void executeSubmit( GXProperties aP0_ServerUrlTemplatingVar ,
                                 SdtGetTransactions__postInput aP1_body ,
                                 out SdtGxExplorer_services_TxoutFromAddresses aP2_GxExplorer_services_TxoutFromAddressesOUT ,
                                 out GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ,
                                 out bool aP4_IsSuccess )
      {
         this.AV10ServerUrlTemplatingVar = aP0_ServerUrlTemplatingVar;
         this.AV8body = aP1_body;
         this.GxExplorer_services_TxoutFromAddressesOUT = new SdtGxExplorer_services_TxoutFromAddresses(context) ;
         this.AV20HttpMessage = new GeneXus.Utils.SdtMessages_Message(context) ;
         this.AV19IsSuccess = false ;
         SubmitImpl();
         aP2_GxExplorer_services_TxoutFromAddressesOUT=this.GxExplorer_services_TxoutFromAddressesOUT;
         aP3_HttpMessage=this.AV20HttpMessage;
         aP4_IsSuccess=this.AV19IsSuccess;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11localVarPath = "/GetTransactions";
         GXt_SdtApiResponse1 = AV14localVarResponse;
         GXt_char2 = "";
         GXt_char3 = "";
         new apibaseurllocalhost(context ).execute(  StringUtil.Format( "%1 - %2", StringUtil.Trim( AV11localVarPath), "POST", "", "", "", "", "", "", ""), out  GXt_char3) ;
         new GeneXus.Programs.openapicommon.processserver(context ).execute(  GXt_char3,  AV10ServerUrlTemplatingVar, out  GXt_char2) ;
         new GeneXus.Programs.openapicommon.callapi(context ).execute(  "POST",  GXt_char2+AV11localVarPath,  AV18localVarHeaders,  AV12localVarPathParams,  AV13localVarQueryParams,  AV17localFileFormParams,  AV16localVarFormParams,  AV8body.ToJSonString(false, true),  "",  0,  0, out  GXt_SdtApiResponse1) ;
         AV14localVarResponse = GXt_SdtApiResponse1;
         AV15localVarStatusCode = (short)(AV14localVarResponse.gxTpr_Statuscode);
         if ( ( AV15localVarStatusCode >= 200 ) && ( AV15localVarStatusCode < 300 ) )
         {
            AV19IsSuccess = true;
         }
         else
         {
            AV19IsSuccess = false;
            AV20HttpMessage.gxTpr_Type = 1;
         }
         if ( AV15localVarStatusCode == 200 )
         {
            AV20HttpMessage.gxTpr_Id = "200";
            AV20HttpMessage.gxTpr_Description = "Successful operation";
            GxExplorer_services_TxoutFromAddressesOUT.FromJSonString(AV14localVarResponse.gxTpr_Content, null);
         }
         else if ( AV15localVarStatusCode == 404 )
         {
            AV20HttpMessage.gxTpr_Id = "404";
            AV20HttpMessage.gxTpr_Description = "Not found";
         }
         else
         {
            AV20HttpMessage.gxTpr_Description = AV14localVarResponse.gxTpr_Errormessage;
            AV20HttpMessage.gxTpr_Id = StringUtil.Str( (decimal)(AV15localVarStatusCode), 4, 0);
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
         GxExplorer_services_TxoutFromAddressesOUT = new SdtGxExplorer_services_TxoutFromAddresses(context);
         AV20HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV11localVarPath = "";
         AV14localVarResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         GXt_char2 = "";
         GXt_char3 = "";
         AV18localVarHeaders = new GXProperties();
         AV12localVarPathParams = new GXProperties();
         AV13localVarQueryParams = new GXProperties();
         AV17localFileFormParams = new GXProperties();
         AV16localVarFormParams = new GXProperties();
         /* GeneXus formulas. */
      }

      private short AV15localVarStatusCode ;
      private string AV11localVarPath ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private bool AV19IsSuccess ;
      private SdtGxExplorer_services_TxoutFromAddresses aP2_GxExplorer_services_TxoutFromAddressesOUT ;
      private GeneXus.Utils.SdtMessages_Message aP3_HttpMessage ;
      private bool aP4_IsSuccess ;
      private GXProperties AV10ServerUrlTemplatingVar ;
      private GXProperties AV18localVarHeaders ;
      private GXProperties AV12localVarPathParams ;
      private GXProperties AV13localVarQueryParams ;
      private GXProperties AV17localFileFormParams ;
      private GXProperties AV16localVarFormParams ;
      private SdtGetTransactions__postInput AV8body ;
      private SdtGxExplorer_services_TxoutFromAddresses GxExplorer_services_TxoutFromAddressesOUT ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV14localVarResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private GeneXus.Utils.SdtMessages_Message AV20HttpMessage ;
   }

}
