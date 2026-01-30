using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.hsm {
   [ApiController]
   [Route("HSM/rest")]
   public class rest_services : GxRestService
   {
      [HttpGet]
      [Route("getPubKey")]
      public async Task<ActionResult> gxep_getpubkey( [FromQuery(Name="Indexkey")]short AV5indexKey )
      {
         string AV7error = "";
         string AV6publicKey = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_getpubkey(AV5indexKey,out AV6publicKey,out AV7error );
            worker.cleanup( );
            rest_getpubkey_ResponseData data = new rest_getpubkey_ResponseData();
            data.publicKey = AV6publicKey;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("getPubKey")]
      public async Task<ActionResult> GetOptionsgetPubKey( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,GET");
         return EmptyResult();
      }

      [HttpPost]
      [Route("encrypt")]
      public async Task<ActionResult> gxep_encrypt( rest_encrypt_RequestData entity )
      {
         entity ??= new rest_encrypt_RequestData();
         string AV9message = entity.message;
         short AV5indexKey = entity.indexKey;
         string AV7error = "";
         string AV10encryptedMessage = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_encrypt(AV9message,AV5indexKey,out AV10encryptedMessage,out AV7error );
            worker.cleanup( );
            rest_encrypt_ResponseData data = new rest_encrypt_ResponseData();
            data.encryptedMessage = AV10encryptedMessage;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("encrypt")]
      public async Task<ActionResult> GetOptionsencrypt( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,POST");
         return EmptyResult();
      }

      [HttpPost]
      [Route("encryptToPubKey")]
      public async Task<ActionResult> gxep_encrypttopubkey( rest_encrypttopubkey_RequestData entity )
      {
         entity ??= new rest_encrypttopubkey_RequestData();
         string AV9message = entity.message;
         string AV6publicKey = entity.publicKey;
         string AV7error = "";
         string AV10encryptedMessage = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_encrypttopubkey(AV9message,AV6publicKey,out AV10encryptedMessage,out AV7error );
            worker.cleanup( );
            rest_encrypttopubkey_ResponseData data = new rest_encrypttopubkey_ResponseData();
            data.encryptedMessage = AV10encryptedMessage;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("encryptToPubKey")]
      public async Task<ActionResult> GetOptionsencryptToPubKey( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,POST");
         return EmptyResult();
      }

      [HttpPost]
      [Route("decrypt")]
      public async Task<ActionResult> gxep_decrypt( rest_decrypt_RequestData entity )
      {
         entity ??= new rest_decrypt_RequestData();
         string AV10encryptedMessage = entity.encryptedMessage;
         short AV5indexKey = entity.indexKey;
         string AV7error = "";
         string AV9message = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_decrypt(AV10encryptedMessage,AV5indexKey,out AV9message,out AV7error );
            worker.cleanup( );
            rest_decrypt_ResponseData data = new rest_decrypt_ResponseData();
            data.message = AV9message;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("decrypt")]
      public async Task<ActionResult> GetOptionsdecrypt( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,POST");
         return EmptyResult();
      }

      [HttpPost]
      [Route("sign")]
      public async Task<ActionResult> gxep_sign( rest_sign_RequestData entity )
      {
         entity ??= new rest_sign_RequestData();
         string AV9message = entity.message;
         short AV5indexKey = entity.indexKey;
         string AV7error = "";
         string AV11signature = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_sign(AV9message,AV5indexKey,out AV11signature,out AV7error );
            worker.cleanup( );
            rest_sign_ResponseData data = new rest_sign_ResponseData();
            data.signature = AV11signature;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("sign")]
      public async Task<ActionResult> GetOptionssign( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,POST");
         return EmptyResult();
      }

      [HttpPost]
      [Route("SignSchnorr")]
      public async Task<ActionResult> gxep_signschnorr( rest_signschnorr_RequestData entity )
      {
         entity ??= new rest_signschnorr_RequestData();
         string AV9message = entity.message;
         short AV5indexKey = entity.indexKey;
         string AV7error = "";
         string AV11signature = "";
         try
         {
            permissionPrefix = "";
            if ( ! IsAuthenticated() )
            {
               return Unauthenticated(NoContent()) ;
            }
            if ( ! ProcessHeaders("hsm.rest") )
            {
               return GetNotModified() ;
            }
            rest worker = new rest(context);
            worker.IsMain = RunAsMain ;
            worker.gxep_signschnorr(AV9message,AV5indexKey,out AV11signature,out AV7error );
            worker.cleanup( );
            rest_signschnorr_ResponseData data = new rest_signschnorr_ResponseData();
            data.signature = AV11signature;
            data.error = AV7error;
            return GetResponse(data) ;
         }
         catch ( Exception e )
         {
            return HandleException(e);
         }
         finally
         {
            Cleanup();
         }
         return NoContent() ;
      }

      [HttpOptions]
      [Route("SignSchnorr")]
      public async Task<ActionResult> GetOptionsSignSchnorr( )
      {
         Response.Headers.Add("Allow","OPTIONS,HEAD,POST");
         return EmptyResult();
      }

   }

   public class rest_getpubkey_ResponseData
   {
      string _AV6publicKey;
      [JsonPropertyName("publicKey")]

      public string publicKey
      {
         get {
            if ( StringUtil.StrCmp(_AV6publicKey, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV6publicKey ;
            }
         }

         set {
            if ( value == null )
            {
               _AV6publicKey = "";
            }
            else
            {
               _AV6publicKey= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

   public class rest_encrypt_RequestData
   {
      string _AV9message;
      [JsonPropertyName("message")]

      public string message
      {
         get {
            if ( StringUtil.StrCmp(_AV9message, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV9message ;
            }
         }

         set {
            if ( value == null )
            {
               _AV9message = "";
            }
            else
            {
               _AV9message= value;
            }
         }

      }

      short _AV5indexKey;
      [JsonPropertyName("indexKey")]

      public short indexKey
      {
         get {
            return _AV5indexKey ;
         }

         set {
            _AV5indexKey= value;
         }

      }

   }

   public class rest_encrypt_ResponseData
   {
      string _AV10encryptedMessage;
      [JsonPropertyName("encryptedMessage")]

      public string encryptedMessage
      {
         get {
            if ( StringUtil.StrCmp(_AV10encryptedMessage, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV10encryptedMessage ;
            }
         }

         set {
            if ( value == null )
            {
               _AV10encryptedMessage = "";
            }
            else
            {
               _AV10encryptedMessage= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

   public class rest_encrypttopubkey_RequestData
   {
      string _AV9message;
      [JsonPropertyName("message")]

      public string message
      {
         get {
            if ( StringUtil.StrCmp(_AV9message, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV9message ;
            }
         }

         set {
            if ( value == null )
            {
               _AV9message = "";
            }
            else
            {
               _AV9message= value;
            }
         }

      }

      string _AV6publicKey;
      [JsonPropertyName("publicKey")]

      public string publicKey
      {
         get {
            if ( StringUtil.StrCmp(_AV6publicKey, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV6publicKey ;
            }
         }

         set {
            if ( value == null )
            {
               _AV6publicKey = "";
            }
            else
            {
               _AV6publicKey= value;
            }
         }

      }

   }

   public class rest_encrypttopubkey_ResponseData
   {
      string _AV10encryptedMessage;
      [JsonPropertyName("encryptedMessage")]

      public string encryptedMessage
      {
         get {
            if ( StringUtil.StrCmp(_AV10encryptedMessage, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV10encryptedMessage ;
            }
         }

         set {
            if ( value == null )
            {
               _AV10encryptedMessage = "";
            }
            else
            {
               _AV10encryptedMessage= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

   public class rest_decrypt_RequestData
   {
      string _AV10encryptedMessage;
      [JsonPropertyName("encryptedMessage")]

      public string encryptedMessage
      {
         get {
            if ( StringUtil.StrCmp(_AV10encryptedMessage, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV10encryptedMessage ;
            }
         }

         set {
            if ( value == null )
            {
               _AV10encryptedMessage = "";
            }
            else
            {
               _AV10encryptedMessage= value;
            }
         }

      }

      short _AV5indexKey;
      [JsonPropertyName("indexKey")]

      public short indexKey
      {
         get {
            return _AV5indexKey ;
         }

         set {
            _AV5indexKey= value;
         }

      }

   }

   public class rest_decrypt_ResponseData
   {
      string _AV9message;
      [JsonPropertyName("message")]

      public string message
      {
         get {
            if ( StringUtil.StrCmp(_AV9message, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV9message ;
            }
         }

         set {
            if ( value == null )
            {
               _AV9message = "";
            }
            else
            {
               _AV9message= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

   public class rest_sign_RequestData
   {
      string _AV9message;
      [JsonPropertyName("message")]

      public string message
      {
         get {
            if ( StringUtil.StrCmp(_AV9message, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV9message ;
            }
         }

         set {
            if ( value == null )
            {
               _AV9message = "";
            }
            else
            {
               _AV9message= value;
            }
         }

      }

      short _AV5indexKey;
      [JsonPropertyName("indexKey")]

      public short indexKey
      {
         get {
            return _AV5indexKey ;
         }

         set {
            _AV5indexKey= value;
         }

      }

   }

   public class rest_sign_ResponseData
   {
      string _AV11signature;
      [JsonPropertyName("signature")]

      public string signature
      {
         get {
            if ( StringUtil.StrCmp(_AV11signature, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV11signature ;
            }
         }

         set {
            if ( value == null )
            {
               _AV11signature = "";
            }
            else
            {
               _AV11signature= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

   public class rest_signschnorr_RequestData
   {
      string _AV9message;
      [JsonPropertyName("message")]

      public string message
      {
         get {
            if ( StringUtil.StrCmp(_AV9message, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV9message ;
            }
         }

         set {
            if ( value == null )
            {
               _AV9message = "";
            }
            else
            {
               _AV9message= value;
            }
         }

      }

      short _AV5indexKey;
      [JsonPropertyName("indexKey")]

      public short indexKey
      {
         get {
            return _AV5indexKey ;
         }

         set {
            _AV5indexKey= value;
         }

      }

   }

   public class rest_signschnorr_ResponseData
   {
      string _AV11signature;
      [JsonPropertyName("signature")]

      public string signature
      {
         get {
            if ( StringUtil.StrCmp(_AV11signature, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV11signature ;
            }
         }

         set {
            if ( value == null )
            {
               _AV11signature = "";
            }
            else
            {
               _AV11signature= value;
            }
         }

      }

      string _AV7error;
      [JsonPropertyName("error")]

      public string error
      {
         get {
            if ( StringUtil.StrCmp(_AV7error, null) == 0 )
            {
               return "" ;
            }
            else
            {
               return _AV7error ;
            }
         }

         set {
            if ( value == null )
            {
               _AV7error = "";
            }
            else
            {
               _AV7error= value;
            }
         }

      }

   }

}
