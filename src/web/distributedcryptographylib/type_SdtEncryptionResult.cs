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
namespace GeneXus.Programs.distributedcryptographylib {
   [Serializable]
   public class SdtEncryptionResult : GxUserType, IGxExternalObject
   {
      public SdtEncryptionResult( )
      {
         /* Constructor for serialization */
      }

      public SdtEncryptionResult( IGxContext context )
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
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            return DistributedCryptographyLib_EncryptionResult_externalReference.CipherText ;
         }

         set {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            DistributedCryptographyLib_EncryptionResult_externalReference.CipherText = value;
            SetDirty("Ciphertext");
         }

      }

      public string gxTpr_Generatedkey
      {
         get {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            return DistributedCryptographyLib_EncryptionResult_externalReference.GeneratedKey ;
         }

         set {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            DistributedCryptographyLib_EncryptionResult_externalReference.GeneratedKey = value;
            SetDirty("Generatedkey");
         }

      }

      public string gxTpr_Errormessage
      {
         get {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            return DistributedCryptographyLib_EncryptionResult_externalReference.ErrorMessage ;
         }

         set {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            DistributedCryptographyLib_EncryptionResult_externalReference.ErrorMessage = value;
            SetDirty("Errormessage");
         }

      }

      public bool gxTpr_Success
      {
         get {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            return DistributedCryptographyLib_EncryptionResult_externalReference.Success ;
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_EncryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionResult_externalReference = new DistricutedCryptographyLib.EncryptionResult();
            }
            return DistributedCryptographyLib_EncryptionResult_externalReference ;
         }

         set {
            DistributedCryptographyLib_EncryptionResult_externalReference = (DistricutedCryptographyLib.EncryptionResult)(value);
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

      protected DistricutedCryptographyLib.EncryptionResult DistributedCryptographyLib_EncryptionResult_externalReference=null ;
   }

}
