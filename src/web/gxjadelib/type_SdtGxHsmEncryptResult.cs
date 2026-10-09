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
   public class SdtGxHsmEncryptResult : GxUserType, IGxExternalObject
   {
      public SdtGxHsmEncryptResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxHsmEncryptResult( IGxContext context )
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

      public string gxTpr_Ciphertext
      {
         get {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            return GxJadeLib_GxHsmEncryptResult_externalReference.Ciphertext ;
         }

         set {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            GxJadeLib_GxHsmEncryptResult_externalReference.Ciphertext = value;
            SetDirty("Ciphertext");
         }

      }

      public string gxTpr_Nonce
      {
         get {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            return GxJadeLib_GxHsmEncryptResult_externalReference.Nonce ;
         }

         set {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            GxJadeLib_GxHsmEncryptResult_externalReference.Nonce = value;
            SetDirty("Nonce");
         }

      }

      public string gxTpr_Tag
      {
         get {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            return GxJadeLib_GxHsmEncryptResult_externalReference.Tag ;
         }

         set {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            GxJadeLib_GxHsmEncryptResult_externalReference.Tag = value;
            SetDirty("Tag");
         }

      }

      public string gxTpr_Ephemeralpubkey
      {
         get {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            return GxJadeLib_GxHsmEncryptResult_externalReference.EphemeralPubkey ;
         }

         set {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            GxJadeLib_GxHsmEncryptResult_externalReference.EphemeralPubkey = value;
            SetDirty("Ephemeralpubkey");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxHsmEncryptResult_externalReference == null )
            {
               GxJadeLib_GxHsmEncryptResult_externalReference = new GxJadeLib.Models.GxHsmEncryptResult();
            }
            return GxJadeLib_GxHsmEncryptResult_externalReference ;
         }

         set {
            GxJadeLib_GxHsmEncryptResult_externalReference = (GxJadeLib.Models.GxHsmEncryptResult)(value);
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

      protected GxJadeLib.Models.GxHsmEncryptResult GxJadeLib_GxHsmEncryptResult_externalReference=null ;
   }

}
