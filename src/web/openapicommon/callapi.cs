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
namespace GeneXus.Programs.openapicommon {
   public class callapi : GXProcedure
   {
      public callapi( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public callapi( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_Method ,
                           string aP1_Url ,
                           GXProperties aP2_VarHeaders ,
                           GXProperties aP3_VarPathParams ,
                           GXProperties aP4_VarQueryParams ,
                           GXProperties aP5_FileFormParams ,
                           GXProperties aP6_VarFormParams ,
                           string aP7_PostData ,
                           string aP8_ContentType ,
                           short aP9_RetryCount ,
                           short aP10_RequestSecondsTimeout ,
                           out GeneXus.Programs.openapicommon.SdtApiResponse aP11_ApiResponse )
      {
         this.AV12Method = aP0_Method;
         this.AV20Url = aP1_Url;
         this.AV28VarHeaders = aP2_VarHeaders;
         this.AV22VarPathParams = aP3_VarPathParams;
         this.AV24VarQueryParams = aP4_VarQueryParams;
         this.AV26FileFormParams = aP5_FileFormParams;
         this.AV13VarFormParams = aP6_VarFormParams;
         this.AV14PostData = aP7_PostData;
         this.AV11ContentType = aP8_ContentType;
         this.AV19RetryCount = aP9_RetryCount;
         this.AV18RequestSecondsTimeout = aP10_RequestSecondsTimeout;
         this.AV8ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context) ;
         initialize();
         ExecuteImpl();
         aP11_ApiResponse=this.AV8ApiResponse;
      }

      public GeneXus.Programs.openapicommon.SdtApiResponse executeUdp( string aP0_Method ,
                                                                       string aP1_Url ,
                                                                       GXProperties aP2_VarHeaders ,
                                                                       GXProperties aP3_VarPathParams ,
                                                                       GXProperties aP4_VarQueryParams ,
                                                                       GXProperties aP5_FileFormParams ,
                                                                       GXProperties aP6_VarFormParams ,
                                                                       string aP7_PostData ,
                                                                       string aP8_ContentType ,
                                                                       short aP9_RetryCount ,
                                                                       short aP10_RequestSecondsTimeout )
      {
         execute(aP0_Method, aP1_Url, aP2_VarHeaders, aP3_VarPathParams, aP4_VarQueryParams, aP5_FileFormParams, aP6_VarFormParams, aP7_PostData, aP8_ContentType, aP9_RetryCount, aP10_RequestSecondsTimeout, out aP11_ApiResponse);
         return AV8ApiResponse ;
      }

      public void executeSubmit( string aP0_Method ,
                                 string aP1_Url ,
                                 GXProperties aP2_VarHeaders ,
                                 GXProperties aP3_VarPathParams ,
                                 GXProperties aP4_VarQueryParams ,
                                 GXProperties aP5_FileFormParams ,
                                 GXProperties aP6_VarFormParams ,
                                 string aP7_PostData ,
                                 string aP8_ContentType ,
                                 short aP9_RetryCount ,
                                 short aP10_RequestSecondsTimeout ,
                                 out GeneXus.Programs.openapicommon.SdtApiResponse aP11_ApiResponse )
      {
         this.AV12Method = aP0_Method;
         this.AV20Url = aP1_Url;
         this.AV28VarHeaders = aP2_VarHeaders;
         this.AV22VarPathParams = aP3_VarPathParams;
         this.AV24VarQueryParams = aP4_VarQueryParams;
         this.AV26FileFormParams = aP5_FileFormParams;
         this.AV13VarFormParams = aP6_VarFormParams;
         this.AV14PostData = aP7_PostData;
         this.AV11ContentType = aP8_ContentType;
         this.AV19RetryCount = aP9_RetryCount;
         this.AV18RequestSecondsTimeout = aP10_RequestSecondsTimeout;
         this.AV8ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context) ;
         SubmitImpl();
         aP11_ApiResponse=this.AV8ApiResponse;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtApiResponse1 = AV8ApiResponse;
         new GeneXus.Programs.openapicommon.callapionce(context ).execute(  AV12Method,  AV20Url,  AV28VarHeaders,  AV22VarPathParams,  AV24VarQueryParams,  AV26FileFormParams,  AV13VarFormParams,  AV14PostData,  AV11ContentType,  AV19RetryCount,  AV18RequestSecondsTimeout, out  GXt_SdtApiResponse1) ;
         AV8ApiResponse = GXt_SdtApiResponse1;
         if ( ( AV8ApiResponse.gxTpr_Statuscode == 401 ) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV28VarHeaders.Get("Authorization"))) )
         {
            GXt_char2 = AV32loginError;
            new GeneXus.Programs.distcrypt.logintoserver(context ).execute( out  GXt_char2) ;
            AV32loginError = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV32loginError)) )
            {
               GXt_SdtExternalUser3 = AV31externalUser;
               new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
               AV31externalUser = GXt_SdtExternalUser3;
               AV29VarProperty = AV28VarHeaders.GetFirst();
               while ( ! AV28VarHeaders.Eof() )
               {
                  AV33retryHeaders.Set(AV29VarProperty.Key, AV29VarProperty.Value);
                  AV29VarProperty = AV28VarHeaders.GetNext();
               }
               AV33retryHeaders.Set("Authorization", StringUtil.Trim( AV31externalUser.gxTpr_Externaltoken));
               GXt_SdtApiResponse1 = AV8ApiResponse;
               new GeneXus.Programs.openapicommon.callapionce(context ).execute(  AV12Method,  AV20Url,  AV33retryHeaders,  AV22VarPathParams,  AV24VarQueryParams,  AV26FileFormParams,  AV13VarFormParams,  AV14PostData,  AV11ContentType,  AV19RetryCount,  AV18RequestSecondsTimeout, out  GXt_SdtApiResponse1) ;
               AV8ApiResponse = GXt_SdtApiResponse1;
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
         AV8ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         AV32loginError = "";
         GXt_char2 = "";
         AV31externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser3 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV29VarProperty = new GxKeyValuePair();
         AV33retryHeaders = new GXProperties();
         GXt_SdtApiResponse1 = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         /* GeneXus formulas. */
      }

      private short AV19RetryCount ;
      private short AV18RequestSecondsTimeout ;
      private string AV12Method ;
      private string AV11ContentType ;
      private string AV32loginError ;
      private string GXt_char2 ;
      private string AV20Url ;
      private string AV14PostData ;
      private GXProperties AV28VarHeaders ;
      private GXProperties AV22VarPathParams ;
      private GXProperties AV24VarQueryParams ;
      private GXProperties AV26FileFormParams ;
      private GXProperties AV13VarFormParams ;
      private GXProperties AV33retryHeaders ;
      private GxKeyValuePair AV29VarProperty ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV8ApiResponse ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV31externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser3 ;
      private GeneXus.Programs.openapicommon.SdtApiResponse GXt_SdtApiResponse1 ;
      private GeneXus.Programs.openapicommon.SdtApiResponse aP11_ApiResponse ;
   }

}
