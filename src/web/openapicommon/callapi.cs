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
         /* User Code */
          System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | System.Net.SecurityProtocolType.Tls12;
         AV9httpClient.Timeout = AV18RequestSecondsTimeout;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11ContentType)) )
         {
            AV9httpClient.AddHeader("Content-Type", "application/json; char-set=utf-8");
         }
         else
         {
            AV9httpClient.AddHeader("Content-Type", AV11ContentType);
         }
         AV29VarProperty = AV28VarHeaders.GetFirst();
         while ( ! AV28VarHeaders.Eof() )
         {
            AV9httpClient.AddHeader(AV29VarProperty.Key, AV29VarProperty.Value);
            AV29VarProperty = AV28VarHeaders.GetNext();
         }
         AV29VarProperty = AV13VarFormParams.GetFirst();
         while ( ! AV13VarFormParams.Eof() )
         {
            AV9httpClient.AddVariable(AV29VarProperty.Key, AV29VarProperty.Value);
            AV29VarProperty = AV13VarFormParams.GetNext();
         }
         if ( StringUtil.StrCmp(AV14PostData, AV15EmptyPostData.ToJSonString()) != 0 )
         {
            AV9httpClient.AddString(AV14PostData);
         }
         AV27File = AV26FileFormParams.GetFirst();
         while ( ! AV26FileFormParams.Eof() )
         {
            AV9httpClient.AddFile(AV27File.Value, AV27File.Key);
            AV27File = AV26FileFormParams.GetNext();
         }
         AV21UrlWithParms = AV20Url;
         AV17RegularExpression = "(\\{(\\w+?)\\})";
         AV16RegExMatchCollection = GxRegex.Matches(AV21UrlWithParms,AV17RegularExpression);
         AV31GXV1 = 1;
         while ( AV31GXV1 <= AV16RegExMatchCollection.Count )
         {
            AV10RegExMatch = ((GxRegexMatch)AV16RegExMatchCollection.Item(AV31GXV1));
            AV23VarPathValue = AV22VarPathParams.Get(((string)AV10RegExMatch.Groups.Item(2)));
            AV21UrlWithParms = StringUtil.StringReplace( AV21UrlWithParms, AV10RegExMatch.Value, AV23VarPathValue);
            AV31GXV1 = (int)(AV31GXV1+1);
         }
         AV25VarQueryValue = AV24VarQueryParams.GetFirst();
         while ( ! AV24VarQueryParams.Eof() )
         {
            AV30QueryParams += StringUtil.Format( "%1=%2&", AV25VarQueryValue.Key, AV25VarQueryValue.Value, "", "", "", "", "", "", "");
            AV25VarQueryValue = AV24VarQueryParams.GetNext();
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV30QueryParams)) )
         {
            AV21UrlWithParms += "?" + AV30QueryParams;
         }
         AV9httpClient.Execute(AV12Method, AV21UrlWithParms);
         AV8ApiResponse.gxTpr_Content = AV9httpClient.ToString();
         AV8ApiResponse.gxTpr_Statuscode = AV9httpClient.StatusCode;
         AV8ApiResponse.gxTpr_Errorcode = AV9httpClient.ErrCode;
         AV8ApiResponse.gxTpr_Errormessage = AV9httpClient.ReasonLine;
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
         AV9httpClient = new GxHttpClient( context);
         AV29VarProperty = new GxKeyValuePair();
         AV15EmptyPostData = new GXProperties();
         AV27File = new GxKeyValuePair();
         AV21UrlWithParms = "";
         AV17RegularExpression = "";
         AV16RegExMatchCollection = new GxUnknownObjectCollection();
         AV10RegExMatch = new GxRegexMatch();
         AV23VarPathValue = "";
         AV25VarQueryValue = new GxKeyValuePair();
         AV30QueryParams = "";
         /* GeneXus formulas. */
      }

      private short AV19RetryCount ;
      private short AV18RequestSecondsTimeout ;
      private int AV31GXV1 ;
      private string AV12Method ;
      private string AV11ContentType ;
      private string AV17RegularExpression ;
      private string AV23VarPathValue ;
      private string AV30QueryParams ;
      private string AV20Url ;
      private string AV14PostData ;
      private string AV21UrlWithParms ;
      private GXProperties AV15EmptyPostData ;
      private GxHttpClient AV9httpClient ;
      private GXProperties AV28VarHeaders ;
      private GXProperties AV22VarPathParams ;
      private GXProperties AV24VarQueryParams ;
      private GXProperties AV26FileFormParams ;
      private GXProperties AV13VarFormParams ;
      private GxKeyValuePair AV29VarProperty ;
      private GxKeyValuePair AV27File ;
      private GxKeyValuePair AV25VarQueryValue ;
      private GxRegexMatch AV10RegExMatch ;
      private GxUnknownObjectCollection AV16RegExMatchCollection ;
      private GeneXus.Programs.openapicommon.SdtApiResponse AV8ApiResponse ;
      private GeneXus.Programs.openapicommon.SdtApiResponse aP11_ApiResponse ;
   }

}
