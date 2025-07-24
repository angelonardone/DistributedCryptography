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
   public class SdtMuSigSignaturesItem : GxUserType, IGxExternalObject
   {
      public SdtMuSigSignaturesItem( )
      {
         /* Constructor for serialization */
      }

      public SdtMuSigSignaturesItem( IGxContext context )
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
            if ( DistributedCryptographyLib_MuSigSignaturesItem_externalReference == null )
            {
               DistributedCryptographyLib_MuSigSignaturesItem_externalReference = new DistricutedCryptographyLib.MuSigSignaturesItem();
            }
            return DistributedCryptographyLib_MuSigSignaturesItem_externalReference.Signature ;
         }

         set {
            if ( DistributedCryptographyLib_MuSigSignaturesItem_externalReference == null )
            {
               DistributedCryptographyLib_MuSigSignaturesItem_externalReference = new DistricutedCryptographyLib.MuSigSignaturesItem();
            }
            DistributedCryptographyLib_MuSigSignaturesItem_externalReference.Signature = value;
            SetDirty("Signature");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_MuSigSignaturesItem_externalReference == null )
            {
               DistributedCryptographyLib_MuSigSignaturesItem_externalReference = new DistricutedCryptographyLib.MuSigSignaturesItem();
            }
            return DistributedCryptographyLib_MuSigSignaturesItem_externalReference ;
         }

         set {
            DistributedCryptographyLib_MuSigSignaturesItem_externalReference = (DistricutedCryptographyLib.MuSigSignaturesItem)(value);
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

      protected DistricutedCryptographyLib.MuSigSignaturesItem DistributedCryptographyLib_MuSigSignaturesItem_externalReference=null ;
   }

}
