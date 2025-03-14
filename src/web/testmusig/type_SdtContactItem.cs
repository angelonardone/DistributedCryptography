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
   public class SdtContactItem : GxUserType, IGxExternalObject
   {
      public SdtContactItem( )
      {
         /* Constructor for serialization */
      }

      public SdtContactItem( IGxContext context )
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

      public Guid gxTpr_Contactid
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactId ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactId = value;
            SetDirty("Contactid");
         }

      }

      public short gxTpr_Numshares
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.NumShares ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.NumShares = value;
            SetDirty("Numshares");
         }

      }

      public string gxTpr_Contactprivatename
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactPrivateName ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactPrivateName = value;
            SetDirty("Contactprivatename");
         }

      }

      public string gxTpr_Contactusername
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactUserName ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactUserName = value;
            SetDirty("Contactusername");
         }

      }

      public string gxTpr_Contactuserpubkey
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactUserPubKey ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactUserPubKey = value;
            SetDirty("Contactuserpubkey");
         }

      }

      public string gxTpr_Contactencryptedkey
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactEncryptedKey ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactEncryptedKey = value;
            SetDirty("Contactencryptedkey");
         }

      }

      public string gxTpr_Contactiv
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactIV ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactIV = value;
            SetDirty("Contactiv");
         }

      }

      public string gxTpr_Contactencryptedtext
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactEncryptedText ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactEncryptedText = value;
            SetDirty("Contactencryptedtext");
         }

      }

      public GxSimpleCollection<DateTime> gxTpr_Contactinvitationsent
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GxSimpleCollection<DateTime> intValue;
            intValue = new GxSimpleCollection<DateTime>();
            System.Nullable< System.DateTime> externalParm0;
            externalParm0 = testMuSig_ContactItem_externalReference.ContactInvitationSent;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Nullable< System.DateTime>), externalParm0);
            return intValue ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GxSimpleCollection<DateTime> intValue;
            System.Nullable< System.DateTime> externalParm1;
            intValue = value;
            externalParm1 = (System.Nullable< System.DateTime>)CollectionUtils.ConvertToExternal( typeof(System.Nullable< System.DateTime>), intValue.ExternalInstance);
            testMuSig_ContactItem_externalReference.ContactInvitationSent = externalParm1;
            SetDirty("Contactinvitationsent");
         }

      }

      public GxSimpleCollection<DateTime> gxTpr_Contactinvitationaccepted
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GxSimpleCollection<DateTime> intValue;
            intValue = new GxSimpleCollection<DateTime>();
            System.Nullable< System.DateTime> externalParm2;
            externalParm2 = testMuSig_ContactItem_externalReference.ContactInvitationAccepted;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Nullable< System.DateTime>), externalParm2);
            return intValue ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GxSimpleCollection<DateTime> intValue;
            System.Nullable< System.DateTime> externalParm3;
            intValue = value;
            externalParm3 = (System.Nullable< System.DateTime>)CollectionUtils.ConvertToExternal( typeof(System.Nullable< System.DateTime>), intValue.ExternalInstance);
            testMuSig_ContactItem_externalReference.ContactInvitationAccepted = externalParm3;
            SetDirty("Contactinvitationaccepted");
         }

      }

      public bool gxTpr_Contactinvitationdeclined
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactInvitationDeclined ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactInvitationDeclined = value;
            SetDirty("Contactinvitationdeclined");
         }

      }

      public bool gxTpr_Contactinvisent
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactInviSent ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactInviSent = value;
            SetDirty("Contactinvisent");
         }

      }

      public bool gxTpr_Contactinvrec
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactInvRec ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactInvRec = value;
            SetDirty("Contactinvrec");
         }

      }

      public Guid gxTpr_Contactgroupid
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactGroupId ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactGroupId = value;
            SetDirty("Contactgroupid");
         }

      }

      public string gxTpr_Contactgroupencpassword
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ContactGroupEncPassword ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ContactGroupEncPassword = value;
            SetDirty("Contactgroupencpassword");
         }

      }

      public string gxTpr_Cleartextshare
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ClearTextShare ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ClearTextShare = value;
            SetDirty("Cleartextshare");
         }

      }

      public bool gxTpr_Numofsharesreached
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.NumOfSharesReached ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.NumOfSharesReached = value;
            SetDirty("Numofsharesreached");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            testMuSig_ContactItem_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public GXExternalCollection<GeneXus.Programs.testmusig.SdtMuSigSignaturesItem> gxTpr_Musigsignatures
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtMuSigSignaturesItem> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.testmusig.SdtMuSigSignaturesItem>( context, "GeneXus.Programs.testmusig.SdtMuSigSignaturesItem", "GeneXus.Programs");
            System.Collections.Generic.List< MultiSignature.Group.MuSigSignaturesItem> externalParm4;
            externalParm4 = testMuSig_ContactItem_externalReference.MuSigSignatures;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< MultiSignature.Group.MuSigSignaturesItem>), externalParm4);
            return intValue ;
         }

         set {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            GXExternalCollection<GeneXus.Programs.testmusig.SdtMuSigSignaturesItem> intValue;
            System.Collections.Generic.List< MultiSignature.Group.MuSigSignaturesItem> externalParm5;
            intValue = value;
            externalParm5 = (System.Collections.Generic.List< MultiSignature.Group.MuSigSignaturesItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< MultiSignature.Group.MuSigSignaturesItem>), intValue.ExternalInstance);
            testMuSig_ContactItem_externalReference.MuSigSignatures = externalParm5;
            SetDirty("Musigsignatures");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( testMuSig_ContactItem_externalReference == null )
            {
               testMuSig_ContactItem_externalReference = new MultiSignature.Group.ContactItem();
            }
            return testMuSig_ContactItem_externalReference ;
         }

         set {
            testMuSig_ContactItem_externalReference = (MultiSignature.Group.ContactItem)(value);
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

      protected MultiSignature.Group.ContactItem testMuSig_ContactItem_externalReference=null ;
   }

}
