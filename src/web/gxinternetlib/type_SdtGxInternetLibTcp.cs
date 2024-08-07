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
namespace GeneXus.Programs.gxinternetlib {
   [Serializable]
   public class SdtGxInternetLibTcp : GxUserType, IGxExternalObject
   {
      public SdtGxInternetLibTcp( )
      {
         /* Constructor for serialization */
      }

      public SdtGxInternetLibTcp( IGxContext context )
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

      public GeneXus.Programs.gxinternetlib.SdtOperationResult connect( string gxTp_hostname ,
                                                                        int gxTp_port ,
                                                                        string gxTp_genexusProc ,
                                                                        bool gxTp_useSsl ,
                                                                        int gxTp_timeoutMilliseconds )
      {
         GeneXus.Programs.gxinternetlib.SdtOperationResult returnconnect;
         if ( GxInternetLib_GxInternetLibTcp_externalReference == null )
         {
            GxInternetLib_GxInternetLibTcp_externalReference = new GxInternetLibTcp();
         }
         returnconnect = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         OperationResult externalParm0;
         externalParm0 = GxInternetLib_GxInternetLibTcp_externalReference.Connect(gxTp_hostname, gxTp_port, gxTp_genexusProc, gxTp_useSsl, gxTp_timeoutMilliseconds);
         returnconnect.ExternalInstance = externalParm0;
         return returnconnect ;
      }

      public GeneXus.Programs.gxinternetlib.SdtOperationResult sendmessage( Guid gxTp_connectionId ,
                                                                            string gxTp_message )
      {
         GeneXus.Programs.gxinternetlib.SdtOperationResult returnsendmessage;
         if ( GxInternetLib_GxInternetLibTcp_externalReference == null )
         {
            GxInternetLib_GxInternetLibTcp_externalReference = new GxInternetLibTcp();
         }
         returnsendmessage = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         OperationResult externalParm0;
         externalParm0 = GxInternetLib_GxInternetLibTcp_externalReference.SendMessage(gxTp_connectionId, gxTp_message);
         returnsendmessage.ExternalInstance = externalParm0;
         return returnsendmessage ;
      }

      public GeneXus.Programs.gxinternetlib.SdtOperationResult disconnect( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxinternetlib.SdtOperationResult returndisconnect;
         if ( GxInternetLib_GxInternetLibTcp_externalReference == null )
         {
            GxInternetLib_GxInternetLibTcp_externalReference = new GxInternetLibTcp();
         }
         returndisconnect = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         OperationResult externalParm0;
         externalParm0 = GxInternetLib_GxInternetLibTcp_externalReference.Disconnect(gxTp_connectionId);
         returndisconnect.ExternalInstance = externalParm0;
         return returndisconnect ;
      }

      public GeneXus.Programs.gxinternetlib.SdtOperationResult sendmessageandwaitforresponse( Guid gxTp_connectionId ,
                                                                                              string gxTp_message ,
                                                                                              int gxTp_timeoutMilliseconds )
      {
         GeneXus.Programs.gxinternetlib.SdtOperationResult returnsendmessageandwaitforresponse;
         if ( GxInternetLib_GxInternetLibTcp_externalReference == null )
         {
            GxInternetLib_GxInternetLibTcp_externalReference = new GxInternetLibTcp();
         }
         returnsendmessageandwaitforresponse = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         OperationResult externalParm0;
         externalParm0 = GxInternetLib_GxInternetLibTcp_externalReference.SendMessageAndWaitForResponse(gxTp_connectionId, gxTp_message, gxTp_timeoutMilliseconds);
         returnsendmessageandwaitforresponse.ExternalInstance = externalParm0;
         return returnsendmessageandwaitforresponse ;
      }

      public Object ExternalInstance
      {
         get {
            if ( GxInternetLib_GxInternetLibTcp_externalReference == null )
            {
               GxInternetLib_GxInternetLibTcp_externalReference = new GxInternetLibTcp();
            }
            return GxInternetLib_GxInternetLibTcp_externalReference ;
         }

         set {
            GxInternetLib_GxInternetLibTcp_externalReference = (GxInternetLibTcp)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected GxInternetLibTcp GxInternetLib_GxInternetLibTcp_externalReference=null ;
   }

}
