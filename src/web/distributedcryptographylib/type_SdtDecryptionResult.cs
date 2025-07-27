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
   public class SdtDecryptionResult : GxUserType, IGxExternalObject
   {
      public SdtDecryptionResult( )
      {
         /* Constructor for serialization */
      }

      public SdtDecryptionResult( IGxContext context )
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

      public string gxTpr_Decryptedtext
      {
         get {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            return DistributedCryptographyLib_DecryptionResult_externalReference.DecryptedText ;
         }

         set {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            DistributedCryptographyLib_DecryptionResult_externalReference.DecryptedText = value;
            SetDirty("Decryptedtext");
         }

      }

      public string gxTpr_Errormessage
      {
         get {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            return DistributedCryptographyLib_DecryptionResult_externalReference.ErrorMessage ;
         }

         set {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            DistributedCryptographyLib_DecryptionResult_externalReference.ErrorMessage = value;
            SetDirty("Errormessage");
         }

      }

      public bool gxTpr_Success
      {
         get {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            return DistributedCryptographyLib_DecryptionResult_externalReference.Success ;
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_DecryptionResult_externalReference == null )
            {
               DistributedCryptographyLib_DecryptionResult_externalReference = new DistricutedCryptographyLib.DecryptionResult();
            }
            return DistributedCryptographyLib_DecryptionResult_externalReference ;
         }

         set {
            DistributedCryptographyLib_DecryptionResult_externalReference = (DistricutedCryptographyLib.DecryptionResult)(value);
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

      protected DistricutedCryptographyLib.DecryptionResult DistributedCryptographyLib_DecryptionResult_externalReference=null ;
   }

}
