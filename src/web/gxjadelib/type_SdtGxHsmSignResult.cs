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
   public class SdtGxHsmSignResult : GxUserType, IGxExternalObject
   {
      public SdtGxHsmSignResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxHsmSignResult( IGxContext context )
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

      public string gxTpr_Signature
      {
         get {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            return GxJadeLib_GxHsmSignResult_externalReference.Signature ;
         }

         set {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            GxJadeLib_GxHsmSignResult_externalReference.Signature = value;
            SetDirty("Signature");
         }

      }

      public string gxTpr_Pubkey
      {
         get {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            return GxJadeLib_GxHsmSignResult_externalReference.Pubkey ;
         }

         set {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            GxJadeLib_GxHsmSignResult_externalReference.Pubkey = value;
            SetDirty("Pubkey");
         }

      }

      public string gxTpr_Algorithm
      {
         get {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            return GxJadeLib_GxHsmSignResult_externalReference.Algorithm ;
         }

         set {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            GxJadeLib_GxHsmSignResult_externalReference.Algorithm = value;
            SetDirty("Algorithm");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxHsmSignResult_externalReference == null )
            {
               GxJadeLib_GxHsmSignResult_externalReference = new GxJadeLib.Models.GxHsmSignResult();
            }
            return GxJadeLib_GxHsmSignResult_externalReference ;
         }

         set {
            GxJadeLib_GxHsmSignResult_externalReference = (GxJadeLib.Models.GxHsmSignResult)(value);
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

      protected GxJadeLib.Models.GxHsmSignResult GxJadeLib_GxHsmSignResult_externalReference=null ;
   }

}
