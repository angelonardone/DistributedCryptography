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
   public class SdtTimeConstrainItem : GxUserType, IGxExternalObject
   {
      public SdtTimeConstrainItem( )
      {
         /* Constructor for serialization */
      }

      public SdtTimeConstrainItem( IGxContext context )
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

      public int gxTpr_Sequence
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference.Sequence ;
         }

         set {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            DistributedCryptographyLib_TimeConstrainItem_externalReference.Sequence = value;
            SetDirty("Sequence");
         }

      }

      public string gxTpr_Address
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference.Address ;
         }

         set {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            DistributedCryptographyLib_TimeConstrainItem_externalReference.Address = value;
            SetDirty("Address");
         }

      }

      public DateTime gxTpr_Date
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference.Date ;
         }

         set {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            DistributedCryptographyLib_TimeConstrainItem_externalReference.Date = value;
            SetDirty("Date");
         }

      }

      public string gxTpr_Encryptedsecret
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference.EncryptedSecret ;
         }

         set {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            DistributedCryptographyLib_TimeConstrainItem_externalReference.EncryptedSecret = value;
            SetDirty("Encryptedsecret");
         }

      }

      public string gxTpr_Encryptedkey
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference.EncryptedKey ;
         }

         set {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            DistributedCryptographyLib_TimeConstrainItem_externalReference.EncryptedKey = value;
            SetDirty("Encryptedkey");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_TimeConstrainItem_externalReference == null )
            {
               DistributedCryptographyLib_TimeConstrainItem_externalReference = new DistricutedCryptographyLib.TimeConstrainItem();
            }
            return DistributedCryptographyLib_TimeConstrainItem_externalReference ;
         }

         set {
            DistributedCryptographyLib_TimeConstrainItem_externalReference = (DistricutedCryptographyLib.TimeConstrainItem)(value);
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

      protected DistricutedCryptographyLib.TimeConstrainItem DistributedCryptographyLib_TimeConstrainItem_externalReference=null ;
   }

}
