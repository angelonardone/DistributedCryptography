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
namespace GeneXus.Programs.distributedcryptographylib {
   [Serializable]
   public class SdtEncryptionService : GxUserType, IGxExternalObject
   {
      public SdtEncryptionService( )
      {
         /* Constructor for serialization */
      }

      public SdtEncryptionService( IGxContext context )
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

      public GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult encrypt( string gxTp_plainText ,
                                                                                      string gxTp_base64Key )
      {
         GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult returnencrypt;
         if ( DistributedCryptographyLib_EncryptionService_externalReference == null )
         {
            DistributedCryptographyLib_EncryptionService_externalReference = new DistricutedCryptographyLib.EncryptionService();
         }
         returnencrypt = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         DistricutedCryptographyLib.EncryptionResult externalParm0;
         externalParm0 = DistributedCryptographyLib_EncryptionService_externalReference.Encrypt(gxTp_plainText, gxTp_base64Key);
         returnencrypt.ExternalInstance = externalParm0;
         return returnencrypt ;
      }

      public GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult decrypt( string gxTp_base64EncryptedPayload ,
                                                                                      string gxTp_base64Key )
      {
         GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult returndecrypt;
         if ( DistributedCryptographyLib_EncryptionService_externalReference == null )
         {
            DistributedCryptographyLib_EncryptionService_externalReference = new DistricutedCryptographyLib.EncryptionService();
         }
         returndecrypt = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         DistricutedCryptographyLib.DecryptionResult externalParm0;
         externalParm0 = DistributedCryptographyLib_EncryptionService_externalReference.Decrypt(gxTp_base64EncryptedPayload, gxTp_base64Key);
         returndecrypt.ExternalInstance = externalParm0;
         return returndecrypt ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_EncryptionService_externalReference == null )
            {
               DistributedCryptographyLib_EncryptionService_externalReference = new DistricutedCryptographyLib.EncryptionService();
            }
            return DistributedCryptographyLib_EncryptionService_externalReference ;
         }

         set {
            DistributedCryptographyLib_EncryptionService_externalReference = (DistricutedCryptographyLib.EncryptionService)(value);
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

      protected DistricutedCryptographyLib.EncryptionService DistributedCryptographyLib_EncryptionService_externalReference=null ;
   }

}
