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

      public Guid gxTpr_Groupid
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.GroupId ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.GroupId = value;
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

      public string gxTpr_Encryptedtextshare
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.EncryptedTextShare ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.EncryptedTextShare = value;
            SetDirty("Encryptedtextshare");
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

      public short gxTpr_Subgrouptype
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.SubGroupType ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.SubGroupType = value;
            SetDirty("Subgrouptype");
         }

      }

      public Guid gxTpr_Bountygroupid
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.BountyGroupId ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.BountyGroupId = value;
            SetDirty("Bountygroupid");
         }

      }

      public Guid gxTpr_Datagroupid
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.DataGroupId ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.DataGroupId = value;
            SetDirty("Datagroupid");
         }

      }

      public string gxTpr_Extpubkeytimebountyreceiving
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            return DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyTimeBountyReceiving ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            DistributedCryptographyLib_GroupSDT_externalReference.ExtPubKeyTimeBountyReceiving = value;
            SetDirty("Extpubkeytimebountyreceiving");
         }

      }

      public GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtTimeConstrainItem> gxTpr_Timeconstrain
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtTimeConstrainItem> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtTimeConstrainItem>( context, "GeneXus.Programs.distributedcryptographylib.SdtTimeConstrainItem", "GeneXus.Programs");
            System.Collections.Generic.List< DistricutedCryptographyLib.TimeConstrainItem> externalParm0;
            externalParm0 = DistributedCryptographyLib_GroupSDT_externalReference.TimeConstrain;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.TimeConstrainItem>), externalParm0);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtTimeConstrainItem> intValue;
            System.Collections.Generic.List< DistricutedCryptographyLib.TimeConstrainItem> externalParm1;
            intValue = value;
            externalParm1 = (System.Collections.Generic.List< DistricutedCryptographyLib.TimeConstrainItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.TimeConstrainItem>), intValue.ExternalInstance);
            DistributedCryptographyLib_GroupSDT_externalReference.TimeConstrain = externalParm1;
            SetDirty("Timeconstrain");
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
            System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem> externalParm2;
            externalParm2 = DistributedCryptographyLib_GroupSDT_externalReference.Contact;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>), externalParm2);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtContactItem> intValue;
            System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem> externalParm3;
            intValue = value;
            externalParm3 = (System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.ContactItem>), intValue.ExternalInstance);
            DistributedCryptographyLib_GroupSDT_externalReference.Contact = externalParm3;
            SetDirty("Contact");
         }

      }

      public GeneXus.Programs.distributedcryptographylib.SdtOtherGroup gxTpr_Othergroup
      {
         get {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GeneXus.Programs.distributedcryptographylib.SdtOtherGroup intValue;
            intValue = new GeneXus.Programs.distributedcryptographylib.SdtOtherGroup(context);
            DistricutedCryptographyLib.OtherGroup externalParm4;
            externalParm4 = DistributedCryptographyLib_GroupSDT_externalReference.OtherGroup;
            intValue.ExternalInstance = externalParm4;
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_GroupSDT_externalReference == null )
            {
               DistributedCryptographyLib_GroupSDT_externalReference = new DistricutedCryptographyLib.GroupSDT();
            }
            GeneXus.Programs.distributedcryptographylib.SdtOtherGroup intValue;
            DistricutedCryptographyLib.OtherGroup externalParm5;
            intValue = value;
            externalParm5 = (DistricutedCryptographyLib.OtherGroup)(intValue.ExternalInstance);
            DistributedCryptographyLib_GroupSDT_externalReference.OtherGroup = externalParm5;
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
