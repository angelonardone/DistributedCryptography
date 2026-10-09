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
   public class SdtGxXpubResult : GxUserType, IGxExternalObject
   {
      public SdtGxXpubResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxXpubResult( IGxContext context )
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
            if ( GxJadeLib_GxXpubResult_externalReference == null )
            {
               GxJadeLib_GxXpubResult_externalReference = new GxJadeLib.Models.GxXpubResult();
            }
            return GxJadeLib_GxXpubResult_externalReference.Xpub ;
         }

         set {
            if ( GxJadeLib_GxXpubResult_externalReference == null )
            {
               GxJadeLib_GxXpubResult_externalReference = new GxJadeLib.Models.GxXpubResult();
            }
            GxJadeLib_GxXpubResult_externalReference.Xpub = value;
            SetDirty("Xpub");
         }

      }

      public string gxTpr_Path
      {
         get {
            if ( GxJadeLib_GxXpubResult_externalReference == null )
            {
               GxJadeLib_GxXpubResult_externalReference = new GxJadeLib.Models.GxXpubResult();
            }
            return GxJadeLib_GxXpubResult_externalReference.Path ;
         }

         set {
            if ( GxJadeLib_GxXpubResult_externalReference == null )
            {
               GxJadeLib_GxXpubResult_externalReference = new GxJadeLib.Models.GxXpubResult();
            }
            GxJadeLib_GxXpubResult_externalReference.Path = value;
            SetDirty("Path");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxXpubResult_externalReference == null )
            {
               GxJadeLib_GxXpubResult_externalReference = new GxJadeLib.Models.GxXpubResult();
            }
            return GxJadeLib_GxXpubResult_externalReference ;
         }

         set {
            GxJadeLib_GxXpubResult_externalReference = (GxJadeLib.Models.GxXpubResult)(value);
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

      protected GxJadeLib.Models.GxXpubResult GxJadeLib_GxXpubResult_externalReference=null ;
   }

}
