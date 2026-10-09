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
   public class SdtGxHsmXpubResult : GxUserType, IGxExternalObject
   {
      public SdtGxHsmXpubResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxHsmXpubResult( IGxContext context )
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

      public string gxTpr_Xpub
      {
         get {
            if ( GxJadeLib_GxHsmXpubResult_externalReference == null )
            {
               GxJadeLib_GxHsmXpubResult_externalReference = new GxJadeLib.Models.GxHsmXpubResult();
            }
            return GxJadeLib_GxHsmXpubResult_externalReference.Xpub ;
         }

         set {
            if ( GxJadeLib_GxHsmXpubResult_externalReference == null )
            {
               GxJadeLib_GxHsmXpubResult_externalReference = new GxJadeLib.Models.GxHsmXpubResult();
            }
            GxJadeLib_GxHsmXpubResult_externalReference.Xpub = value;
            SetDirty("Xpub");
         }

      }

      public string gxTpr_Path
      {
         get {
            if ( GxJadeLib_GxHsmXpubResult_externalReference == null )
            {
               GxJadeLib_GxHsmXpubResult_externalReference = new GxJadeLib.Models.GxHsmXpubResult();
            }
            return GxJadeLib_GxHsmXpubResult_externalReference.Path ;
         }

         set {
            if ( GxJadeLib_GxHsmXpubResult_externalReference == null )
            {
               GxJadeLib_GxHsmXpubResult_externalReference = new GxJadeLib.Models.GxHsmXpubResult();
            }
            GxJadeLib_GxHsmXpubResult_externalReference.Path = value;
            SetDirty("Path");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxHsmXpubResult_externalReference == null )
            {
               GxJadeLib_GxHsmXpubResult_externalReference = new GxJadeLib.Models.GxHsmXpubResult();
            }
            return GxJadeLib_GxHsmXpubResult_externalReference ;
         }

         set {
            GxJadeLib_GxHsmXpubResult_externalReference = (GxJadeLib.Models.GxHsmXpubResult)(value);
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

      protected GxJadeLib.Models.GxHsmXpubResult GxJadeLib_GxHsmXpubResult_externalReference=null ;
   }

}
