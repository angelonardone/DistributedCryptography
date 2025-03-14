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
namespace GeneXus.Programs.testmusig {
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
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.GroupId ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.GroupId = value;
            SetDirty("Groupid");
         }

      }

      public short gxTpr_Grouptype
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.GroupType ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.GroupType = value;
            SetDirty("Grouptype");
         }

      }

      public string gxTpr_Groupname
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.GroupName ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.GroupName = value;
            SetDirty("Groupname");
         }

      }

      public bool gxTpr_Amigroupowner
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.AmIGroupOwner ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.AmIGroupOwner = value;
            SetDirty("Amigroupowner");
         }

      }

      public bool gxTpr_Isactive
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.IsActive ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.IsActive = value;
            SetDirty("Isactive");
         }

      }

      public short gxTpr_Minimumshares
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.MinimumShares ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.MinimumShares = value;
            SetDirty("Minimumshares");
         }

      }

      public string gxTpr_Encpassword
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.EncPassword ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.EncPassword = value;
            SetDirty("Encpassword");
         }

      }

      public string gxTpr_Cleartextshare
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.ClearTextShare ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.ClearTextShare = value;
            SetDirty("Cleartextshare");
         }

      }

      public bool gxTpr_Numofsharesreached
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.NumOfSharesReached ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.NumOfSharesReached = value;
            SetDirty("Numofsharesreached");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            testMuSig_GroupSDT_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public GXExternalCollection<GeneXus.Programs.testmusig.SdtContactItem> gxTpr_Contact
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtContactItem> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.testmusig.SdtContactItem>( context, "GeneXus.Programs.testmusig.SdtContactItem", "GeneXus.Programs");
            System.Collections.Generic.List< MultiSignature.Group.ContactItem> externalParm0;
            externalParm0 = testMuSig_GroupSDT_externalReference.Contact;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< MultiSignature.Group.ContactItem>), externalParm0);
            return intValue ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtContactItem> intValue;
            System.Collections.Generic.List< MultiSignature.Group.ContactItem> externalParm1;
            intValue = value;
            externalParm1 = (System.Collections.Generic.List< MultiSignature.Group.ContactItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< MultiSignature.Group.ContactItem>), intValue.ExternalInstance);
            testMuSig_GroupSDT_externalReference.Contact = externalParm1;
            SetDirty("Contact");
         }

      }

      public GXExternalCollection<GeneXus.Programs.testmusig.SdtOtherGroup> gxTpr_Othergroup
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtOtherGroup> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.testmusig.SdtOtherGroup>( context, "GeneXus.Programs.testmusig.SdtOtherGroup", "GeneXus.Programs");
            System.Collections.Generic.List< MultiSignature.Group.OtherGroup> externalParm2;
            externalParm2 = testMuSig_GroupSDT_externalReference.OtherGroup;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< MultiSignature.Group.OtherGroup>), externalParm2);
            return intValue ;
         }

         set {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtOtherGroup> intValue;
            System.Collections.Generic.List< MultiSignature.Group.OtherGroup> externalParm3;
            intValue = value;
            externalParm3 = (System.Collections.Generic.List< MultiSignature.Group.OtherGroup>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< MultiSignature.Group.OtherGroup>), intValue.ExternalInstance);
            testMuSig_GroupSDT_externalReference.OtherGroup = externalParm3;
            SetDirty("Othergroup");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( testMuSig_GroupSDT_externalReference == null )
            {
               testMuSig_GroupSDT_externalReference = new MultiSignature.Group.GroupSDT();
            }
            return testMuSig_GroupSDT_externalReference ;
         }

         set {
            testMuSig_GroupSDT_externalReference = (MultiSignature.Group.GroupSDT)(value);
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

      protected MultiSignature.Group.GroupSDT testMuSig_GroupSDT_externalReference=null ;
   }

}
