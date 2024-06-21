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
   public class SdtGxNostrLib : GxUserType, IGxExternalObject
   {
      public SdtGxNostrLib( )
      {
         /* Constructor for serialization */
      }

      public SdtGxNostrLib( IGxContext context )
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

      public GeneXus.Programs.nostr.SdtWebSocketOperationResult connectasync( string gxTp_uri ,
                                                                              string gxTp_genexusProc ,
                                                                              int gxTp_timeoutMilliseconds )
      {
         GeneXus.Programs.nostr.SdtWebSocketOperationResult returnconnectasync;
         if ( Nostr_GxNostrLib_externalReference == null )
         {
            Nostr_GxNostrLib_externalReference = new GxNostrLib();
         }
         returnconnectasync = new GeneXus.Programs.nostr.SdtWebSocketOperationResult(context);
         WebSocketOperationResult externalParm0;
         externalParm0 = Nostr_GxNostrLib_externalReference.ConnectAsync(gxTp_uri, gxTp_genexusProc, gxTp_timeoutMilliseconds);
         returnconnectasync.ExternalInstance = externalParm0;
         return returnconnectasync ;
      }

      public GeneXus.Programs.nostr.SdtWebSocketOperationResult sendmessageasync( Guid gxTp_connectionId ,
                                                                                  string gxTp_message )
      {
         GeneXus.Programs.nostr.SdtWebSocketOperationResult returnsendmessageasync;
         if ( Nostr_GxNostrLib_externalReference == null )
         {
            Nostr_GxNostrLib_externalReference = new GxNostrLib();
         }
         returnsendmessageasync = new GeneXus.Programs.nostr.SdtWebSocketOperationResult(context);
         WebSocketOperationResult externalParm0;
         externalParm0 = Nostr_GxNostrLib_externalReference.SendMessageAsync(gxTp_connectionId, gxTp_message);
         returnsendmessageasync.ExternalInstance = externalParm0;
         return returnsendmessageasync ;
      }

      public GeneXus.Programs.nostr.SdtWebSocketOperationResult disconnectasync( Guid gxTp_connectionId )
      {
         GeneXus.Programs.nostr.SdtWebSocketOperationResult returndisconnectasync;
         if ( Nostr_GxNostrLib_externalReference == null )
         {
            Nostr_GxNostrLib_externalReference = new GxNostrLib();
         }
         returndisconnectasync = new GeneXus.Programs.nostr.SdtWebSocketOperationResult(context);
         WebSocketOperationResult externalParm0;
         externalParm0 = Nostr_GxNostrLib_externalReference.DisconnectAsync(gxTp_connectionId);
         returndisconnectasync.ExternalInstance = externalParm0;
         return returndisconnectasync ;
      }

      public Object ExternalInstance
      {
         get {
            if ( Nostr_GxNostrLib_externalReference == null )
            {
               Nostr_GxNostrLib_externalReference = new GxNostrLib();
            }
            return Nostr_GxNostrLib_externalReference ;
         }

         set {
            Nostr_GxNostrLib_externalReference = (GxNostrLib)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected GxNostrLib Nostr_GxNostrLib_externalReference=null ;
   }

}
