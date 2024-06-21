using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.nostr {
   [Serializable]
   public class SdtWebSocketOperationResult : GxUserType, IGxExternalObject
   {
      public SdtWebSocketOperationResult( )
      {
         /* Constructor for serialization */
      }

      public SdtWebSocketOperationResult( IGxContext context )
      {
         this.context = context;
         initialize();
      }

      private static Hashtable mapper;
      public override string JsonMap( string value )
      {
         if ( mapper == null )
         {
            mapper = new Hashtable();
         }
         return (string)mapper[value]; ;
      }

      public bool gxTpr_Success
      {
         get {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            return Nostr_WebSocketOperationResult_externalReference.Success ;
         }

         set {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            Nostr_WebSocketOperationResult_externalReference.Success = value;
            SetDirty("Success");
         }

      }

      public string gxTpr_Errormessage
      {
         get {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            return Nostr_WebSocketOperationResult_externalReference.ErrorMessage ;
         }

         set {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            Nostr_WebSocketOperationResult_externalReference.ErrorMessage = value;
            SetDirty("Errormessage");
         }

      }

      public Guid gxTpr_Connectionid
      {
         get {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            return Nostr_WebSocketOperationResult_externalReference.ConnectionId ;
         }

         set {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            Nostr_WebSocketOperationResult_externalReference.ConnectionId = value;
            SetDirty("Connectionid");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( Nostr_WebSocketOperationResult_externalReference == null )
            {
               Nostr_WebSocketOperationResult_externalReference = new WebSocketOperationResult();
            }
            return Nostr_WebSocketOperationResult_externalReference ;
         }

         set {
            Nostr_WebSocketOperationResult_externalReference = (WebSocketOperationResult)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected WebSocketOperationResult Nostr_WebSocketOperationResult_externalReference=null ;
   }

}
