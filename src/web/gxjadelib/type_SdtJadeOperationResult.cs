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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.gxjadelib {
   [Serializable]
   public class SdtJadeOperationResult : GxUserType, IGxExternalObject
   {
      public SdtJadeOperationResult( )
      {
         /* Constructor for serialization */
      }

      public SdtJadeOperationResult( IGxContext context )
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

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult ok( Guid gxTp_connectionId ,
                                                                   string gxTp_response )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnok;
         returnok = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.Models.JadeOperationResult.Ok(gxTp_connectionId, gxTp_response);
         returnok.ExternalInstance = externalParm0;
         return returnok ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult fail( Guid gxTp_connectionId ,
                                                                     string gxTp_error ,
                                                                     int gxTp_code )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnfail;
         returnfail = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.Models.JadeOperationResult.Fail(gxTp_connectionId, gxTp_error, gxTp_code);
         returnfail.ExternalInstance = externalParm0;
         return returnfail ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult fail1( string gxTp_error ,
                                                                      int gxTp_code )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnfail1;
         returnfail1 = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.Models.JadeOperationResult.Fail(gxTp_error, gxTp_code);
         returnfail1.ExternalInstance = externalParm0;
         return returnfail1 ;
      }

      public bool gxTpr_Success
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference.Success ;
         }

         set {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            GxJadeLib_JadeOperationResult_externalReference.Success = value;
            SetDirty("Success");
         }

      }

      public string gxTpr_Errormessage
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference.ErrorMessage ;
         }

         set {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            GxJadeLib_JadeOperationResult_externalReference.ErrorMessage = value;
            SetDirty("Errormessage");
         }

      }

      public Guid gxTpr_Connectionid
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference.ConnectionId ;
         }

         set {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            GxJadeLib_JadeOperationResult_externalReference.ConnectionId = value;
            SetDirty("Connectionid");
         }

      }

      public string gxTpr_Responsemessage
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference.ResponseMessage ;
         }

         set {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            GxJadeLib_JadeOperationResult_externalReference.ResponseMessage = value;
            SetDirty("Responsemessage");
         }

      }

      public int gxTpr_Errorcode
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference.ErrorCode ;
         }

         set {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            GxJadeLib_JadeOperationResult_externalReference.ErrorCode = value;
            SetDirty("Errorcode");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_JadeOperationResult_externalReference == null )
            {
               GxJadeLib_JadeOperationResult_externalReference = new GxJadeLib.Models.JadeOperationResult();
            }
            return GxJadeLib_JadeOperationResult_externalReference ;
         }

         set {
            GxJadeLib_JadeOperationResult_externalReference = (GxJadeLib.Models.JadeOperationResult)(value);
         }

      }

      [XmlIgnore]
      private static GXTypeInfo _typeProps;
      protected override GXTypeInfo TypeInfo
      {
         get {
            return _typeProps ;
         }

         set {
            _typeProps = value ;
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected GxJadeLib.Models.JadeOperationResult GxJadeLib_JadeOperationResult_externalReference=null ;
   }

}
