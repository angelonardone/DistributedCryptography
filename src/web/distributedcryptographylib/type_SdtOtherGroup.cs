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
   public class SdtOtherGroup : GxUserType, IGxExternalObject
   {
      public SdtOtherGroup( )
      {
         /* Constructor for serialization */
      }

      public SdtOtherGroup( IGxContext context )
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

      public object gxTpr_Referencegroupid
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            object intValue;
            System.Guid externalParm0;
            externalParm0 = DistributedCryptographyLib_OtherGroup_externalReference.ReferenceGroupId;
            intValue = (object)(externalParm0);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.ReferenceGroupId = (System.Guid)(value);
            SetDirty("Referencegroupid");
         }

      }

      public bool gxTpr_Invitationdeclined
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.InvitationDeclined ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.InvitationDeclined = value;
            SetDirty("Invitationdeclined");
         }

      }

      public string gxTpr_Encpassword
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.EncPassword ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.EncPassword = value;
            SetDirty("Encpassword");
         }

      }

      public string gxTpr_Referenceusername
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.ReferenceUserName ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.ReferenceUserName = value;
            SetDirty("Referenceusername");
         }

      }

      public string gxTpr_Signature
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.Signature ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.Signature = value;
            SetDirty("Signature");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            DistributedCryptographyLib_OtherGroup_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_OtherGroup_externalReference == null )
            {
               DistributedCryptographyLib_OtherGroup_externalReference = new DistricutedCryptographyLib.OtherGroup();
            }
            return DistributedCryptographyLib_OtherGroup_externalReference ;
         }

         set {
            DistributedCryptographyLib_OtherGroup_externalReference = (DistricutedCryptographyLib.OtherGroup)(value);
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

      protected DistricutedCryptographyLib.OtherGroup DistributedCryptographyLib_OtherGroup_externalReference=null ;
   }

}
