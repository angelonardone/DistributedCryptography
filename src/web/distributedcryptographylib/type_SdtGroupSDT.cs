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
   public class SdtGroupSDT : GxUserType, IGxExternalObject
   {
      public SdtGroupSDT( )
      {
         /* Constructor for serialization */
      }

      public SdtGroupSDT( IGxContext context )
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

      public object gxTpr_Groupid
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            object intValue;
            System.Guid externalParm0;
            externalParm0 = DistributedCryptographyLib_GroupSDT_externalReference.GroupId;
            intValue = (object)(externalParm0);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.GroupId = (System.Guid)(value);
            SetDirty("Groupid");
         }

      }

      public short gxTpr_Grouptype
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.GroupType ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.GroupType = value;
            SetDirty("Grouptype");
         }

      }

      public string gxTpr_Groupname
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.GroupName ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.GroupName = value;
            SetDirty("Groupname");
         }

      }

      public bool gxTpr_Amigroupowner
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.AmIGroupOwner ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.AmIGroupOwner = value;
            SetDirty("Amigroupowner");
         }

      }

      public bool gxTpr_Isactive
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.IsActive ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.IsActive = value;
            SetDirty("Isactive");
         }

      }

      public short gxTpr_Minimumshares
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.MinimumShares ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.MinimumShares = value;
            SetDirty("Minimumshares");
         }

      }

      public string gxTpr_Encpassword
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.EncPassword ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.EncPassword = value;
            SetDirty("Encpassword");
         }

      }

      public string gxTpr_Cleartextshare
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.ClearTextShare ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.ClearTextShare = value;
            SetDirty("Cleartextshare");
         }

      }

      public bool gxTpr_Numofsharesreached
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.NumOfSharesReached ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.NumOfSharesReached = value;
            SetDirty("Numofsharesreached");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtContactItem> gxTpr_Contact
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtContactItem> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtContactItem>( context, "GeneXus.Programs.distributedcryptographylib.SdtContactItem", "GeneXus.Programs");
            System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem> externalParm1;
            externalParm1 = DistributedCryptographyLib_GroupSDT_externalReference.Contact;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>), externalParm1);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtContactItem> intValue;
            System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem> externalParm2;
            intValue = value;
            externalParm2 = (System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>), intValue.ExternalInstance);
            DistributedCryptographyLib_GroupSDT_externalReference.Contact = externalParm2;
            SetDirty("Contact");
         }

      }

      public GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtOtherGroup> gxTpr_Othergroup
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtOtherGroup> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtOtherGroup>( context, "GeneXus.Programs.distributedcryptographylib.SdtOtherGroup", "GeneXus.Programs");
            System.Collections.Generic.List< DistricutedCryptographyLib.OtherGroup> externalParm3;
            externalParm3 = DistributedCryptographyLib_GroupSDT_externalReference.OtherGroup;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.OtherGroup>), externalParm3);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtOtherGroup> intValue;
            System.Collections.Generic.List< DistricutedCryptographyLib.OtherGroup> externalParm4;
            intValue = value;
            externalParm4 = (System.Collections.Generic.List< DistricutedCryptographyLib.OtherGroup>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.OtherGroup>), intValue.ExternalInstance);
            DistributedCryptographyLib_GroupSDT_externalReference.OtherGroup = externalParm4;
            SetDirty("Othergroup");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference ;
         }

         set {
            DistributedCryptographyLib_GroupSDT_externalReference = (DistricutedCryptographyLib.GroupSDT)(value);
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

      protected DistricutedCryptographyLib.GroupSDT DistributedCryptographyLib_GroupSDT_externalReference=null ;
   }

}
