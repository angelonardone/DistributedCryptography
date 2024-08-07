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
   public class SdtOperationResult : GxUserType, IGxExternalObject
   {
      public SdtOperationResult( )
      {
         /* Constructor for serialization */
      }

      public SdtOperationResult( IGxContext context )
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
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            return GxInternetLib_OperationResult_externalReference.Success ;
         }

         set {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            GxInternetLib_OperationResult_externalReference.Success = value;
            SetDirty("Success");
         }

      }

      public string gxTpr_Errormessage
      {
         get {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            return GxInternetLib_OperationResult_externalReference.ErrorMessage ;
         }

         set {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            GxInternetLib_OperationResult_externalReference.ErrorMessage = value;
            SetDirty("Errormessage");
         }

      }

      public Guid gxTpr_Connectionid
      {
         get {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            return GxInternetLib_OperationResult_externalReference.ConnectionId ;
         }

         set {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            GxInternetLib_OperationResult_externalReference.ConnectionId = value;
            SetDirty("Connectionid");
         }

      }

      public string gxTpr_Responsemessage
      {
         get {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            return GxInternetLib_OperationResult_externalReference.ResponseMessage ;
         }

         set {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            GxInternetLib_OperationResult_externalReference.ResponseMessage = value;
            SetDirty("Responsemessage");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxInternetLib_OperationResult_externalReference == null )
            {
               GxInternetLib_OperationResult_externalReference = new OperationResult();
            }
            return GxInternetLib_OperationResult_externalReference ;
         }

         set {
            GxInternetLib_OperationResult_externalReference = (OperationResult)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected OperationResult GxInternetLib_OperationResult_externalReference=null ;
   }

}
