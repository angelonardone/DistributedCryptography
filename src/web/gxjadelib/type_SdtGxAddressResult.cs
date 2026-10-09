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
   public class SdtGxAddressResult : GxUserType, IGxExternalObject
   {
      public SdtGxAddressResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxAddressResult( IGxContext context )
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

      public string gxTpr_Address
      {
         get {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            return GxJadeLib_GxAddressResult_externalReference.Address ;
         }

         set {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            GxJadeLib_GxAddressResult_externalReference.Address = value;
            SetDirty("Address");
         }

      }

      public string gxTpr_Path
      {
         get {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            return GxJadeLib_GxAddressResult_externalReference.Path ;
         }

         set {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            GxJadeLib_GxAddressResult_externalReference.Path = value;
            SetDirty("Path");
         }

      }

      public string gxTpr_Variant
      {
         get {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            return GxJadeLib_GxAddressResult_externalReference.Variant ;
         }

         set {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            GxJadeLib_GxAddressResult_externalReference.Variant = value;
            SetDirty("Variant");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxAddressResult_externalReference == null )
            {
               GxJadeLib_GxAddressResult_externalReference = new GxJadeLib.Models.GxAddressResult();
            }
            return GxJadeLib_GxAddressResult_externalReference ;
         }

         set {
            GxJadeLib_GxAddressResult_externalReference = (GxJadeLib.Models.GxAddressResult)(value);
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

      protected GxJadeLib.Models.GxAddressResult GxJadeLib_GxAddressResult_externalReference=null ;
   }

}
