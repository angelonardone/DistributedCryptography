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
   public class callapionce : GXProcedure
   {
      public callapionce( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public callapionce( IGxContext context )
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
         this.AV16Method = aP0_Method;
         this.AV19Url = aP1_Url;
         this.AV22VarHeaders = aP2_VarHeaders;
         this.AV23VarPathParams = aP3_VarPathParams;
         this.AV25VarQueryParams = aP4_VarQueryParams;
         this.AV14FileFormParams = aP5_FileFormParams;
         this.AV21VarFormParams = aP6_VarFormParams;
         this.AV17PostData = aP7_PostData;
         this.AV11ContentType = aP8_ContentType;
         this.AV9RetryCount = aP9_RetryCount;
         this.AV8RequestSecondsTimeout = aP10_RequestSecondsTimeout;
         this.AV10ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context) ;
         initialize();
         ExecuteImpl();
         aP11_ApiResponse=this.AV10ApiResponse;
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
         return AV10ApiResponse ;
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
         this.AV16Method = aP0_Method;
         this.AV19Url = aP1_Url;
         this.AV22VarHeaders = aP2_VarHeaders;
         this.AV23VarPathParams = aP3_VarPathParams;
         this.AV25VarQueryParams = aP4_VarQueryParams;
         this.AV14FileFormParams = aP5_FileFormParams;
         this.AV21VarFormParams = aP6_VarFormParams;
         this.AV17PostData = aP7_PostData;
         this.AV11ContentType = aP8_ContentType;
         this.AV9RetryCount = aP9_RetryCount;
         this.AV8RequestSecondsTimeout = aP10_RequestSecondsTimeout;
         this.AV10ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context) ;
         SubmitImpl();
         aP11_ApiResponse=this.AV10ApiResponse;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | System.Net.SecurityProtocolType.Tls12;
         AV15httpClient.Timeout = AV8RequestSecondsTimeout;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11ContentType)) )
         {
            AV15httpClient.AddHeader("Content-Type", "application/json; char-set=utf-8");
         }
         else
         {
            AV15httpClient.AddHeader("Content-Type", AV11ContentType);
         }
         AV24VarProperty = AV22VarHeaders.GetFirst();
         while ( ! AV22VarHeaders.Eof() )
         {
            AV15httpClient.AddHeader(AV24VarProperty.Key, AV24VarProperty.Value);
            AV24VarProperty = AV22VarHeaders.GetNext();
         }
         AV24VarProperty = AV21VarFormParams.GetFirst();
         while ( ! AV21VarFormParams.Eof() )
         {
            AV15httpClient.AddVariable(AV24VarProperty.Key, AV24VarProperty.Value);
            AV24VarProperty = AV21VarFormParams.GetNext();
         }
         if ( StringUtil.StrCmp(AV17PostData, AV12EmptyPostData.ToJSonString()) != 0 )
         {
            AV15httpClient.AddString(AV17PostData);
         }
         AV13File = AV14FileFormParams.GetFirst();
         while ( ! AV14FileFormParams.Eof() )
         {
            AV15httpClient.AddFile(AV13File.Value, AV13File.Key);
            AV13File = AV14FileFormParams.GetNext();
         }
         AV20UrlWithParms = AV19Url;
         AV24VarProperty = AV23VarPathParams.GetFirst();
         while ( ! AV23VarPathParams.Eof() )
         {
            AV20UrlWithParms = StringUtil.StringReplace( AV20UrlWithParms, "{"+AV24VarProperty.Key+"}", AV24VarProperty.Value);
            AV24VarProperty = AV23VarPathParams.GetNext();
         }
         AV26VarQueryValue = AV25VarQueryParams.GetFirst();
         while ( ! AV25VarQueryParams.Eof() )
         {
            AV18QueryParams += StringUtil.Format( "%1=%2&", AV26VarQueryValue.Key, AV26VarQueryValue.Value, "", "", "", "", "", "", "");
            AV26VarQueryValue = AV25VarQueryParams.GetNext();
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18QueryParams)) )
         {
            AV20UrlWithParms += "?" + AV18QueryParams;
         }
         AV15httpClient.Execute(AV16Method, AV20UrlWithParms);
         AV10ApiResponse.gxTpr_Content = AV15httpClient.ToString();
         AV10ApiResponse.gxTpr_Statuscode = AV15httpClient.StatusCode;
         AV10ApiResponse.gxTpr_Errorcode = AV15httpClient.ErrCode;
         AV10ApiResponse.gxTpr_Errormessage = AV15httpClient.ReasonLine;
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
         AV10ApiResponse = new GeneXus.Programs.openapicommon.SdtApiResponse(context);
         AV15httpClient = new GxHttpClient( context);
         AV24VarProperty = new GxKeyValuePair();
         AV12EmptyPostData = new GXProperties();
         AV13File = new GxKeyValuePair();
         AV20UrlWithParms = "";
         AV26VarQueryValue = new GxKeyValuePair();
         AV18QueryParams = "";
         /* GeneXus formulas. */
      }

      private short AV9RetryCount ;
      private short AV8RequestSecondsTimeout ;
      private string AV16Method ;
      private string AV11ContentType ;
      private string AV18QueryParams ;
      private string AV19Url ;
      private string AV17PostData ;
      private string AV20UrlWithParms ;
      private GXProperties AV12EmptyPostData ;
      private GxHttpClient AV15httpClient ;
      private GXProperties AV22VarHeaders ;
      private GXProperties AV23VarPathParams ;
      private GXProperties AV25VarQueryParams ;
      private GXProperties AV14FileFormParams ;
      private GXProperties AV21VarFormParams ;
      private GxKeyValuePair AV24VarProperty ;
      private GxKeyValuePair AV13File ;
      private GxKeyValuePair AV26VarQueryValue ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV10ApiResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse aP11_ApiResponse ;
   }

}
