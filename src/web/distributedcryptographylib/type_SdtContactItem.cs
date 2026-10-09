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
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactId ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactId = value;
            SetDirty("Contactid");
         }

      }

      public short gxTpr_Numshares
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.NumShares ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.NumShares = value;
            SetDirty("Numshares");
         }

      }

      public string gxTpr_Contactprivatename
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactPrivateName ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactPrivateName = value;
            SetDirty("Contactprivatename");
         }

      }

      public string gxTpr_Contactusername
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactUserName ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactUserName = value;
            SetDirty("Contactusername");
         }

      }

      public string gxTpr_Contactuserpubkey
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactUserPubKey ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactUserPubKey = value;
            SetDirty("Contactuserpubkey");
         }

      }

      public string gxTpr_Contactencryptedkey
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactEncryptedKey ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactEncryptedKey = value;
            SetDirty("Contactencryptedkey");
         }

      }

      public string gxTpr_Contactencryptedtext
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactEncryptedText ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactEncryptedText = value;
            SetDirty("Contactencryptedtext");
         }

      }

      public DateTime gxTpr_Contactinvitationsent
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationSent ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationSent = value;
            SetDirty("Contactinvitationsent");
         }

      }

      public DateTime gxTpr_Contactinvitationaccepted
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationAccepted ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationAccepted = value;
            SetDirty("Contactinvitationaccepted");
         }

      }

      public bool gxTpr_Contactinvitationdeclined
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationDeclined ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactInvitationDeclined = value;
            SetDirty("Contactinvitationdeclined");
         }

      }

      public bool gxTpr_Contactinvisent
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactInviSent ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactInviSent = value;
            SetDirty("Contactinvisent");
         }

      }

      public bool gxTpr_Contactinvrec
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactInvRec ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactInvRec = value;
            SetDirty("Contactinvrec");
         }

      }

      public Guid gxTpr_Contactgroupid
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactGroupId ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactGroupId = value;
            SetDirty("Contactgroupid");
         }

      }

      public string gxTpr_Contactgroupencpassword
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ContactGroupEncPassword ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ContactGroupEncPassword = value;
            SetDirty("Contactgroupencpassword");
         }

      }

      public string gxTpr_Cleartextshare
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ClearTextShare ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ClearTextShare = value;
            SetDirty("Cleartextshare");
         }

      }

      public bool gxTpr_Numofsharesreached
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.NumOfSharesReached ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.NumOfSharesReached = value;
            SetDirty("Numofsharesreached");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public string gxTpr_Extpubkeytimebountyreceiving
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyTimeBountyReceiving ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            DistributedCryptographyLib_ContactItem_externalReference.ExtPubKeyTimeBountyReceiving = value;
            SetDirty("Extpubkeytimebountyreceiving");
         }

      }

      public GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtMuSigSignaturesItem> gxTpr_Musigsignatures
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtMuSigSignaturesItem> intValue;
            intValue = new GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtMuSigSignaturesItem>( context, "GeneXus.Programs.distributedcryptographylib.SdtMuSigSignaturesItem", "GeneXus.Programs");
            System.Collections.Generic.List< DistricutedCryptographyLib.MuSigSignaturesItem> externalParm0;
            externalParm0 = DistributedCryptographyLib_ContactItem_externalReference.MuSigSignatures;
            intValue.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.MuSigSignaturesItem>), externalParm0);
            return intValue ;
         }

         set {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            GXExternalCollection<GeneXus.Programs.distributedcryptographylib.SdtMuSigSignaturesItem> intValue;
            System.Collections.Generic.List< DistricutedCryptographyLib.MuSigSignaturesItem> externalParm1;
            intValue = value;
            externalParm1 = (System.Collections.Generic.List< DistricutedCryptographyLib.MuSigSignaturesItem>)CollectionUtils.ConvertToExternal( typeof(System.Collections.Generic.List< DistricutedCryptographyLib.MuSigSignaturesItem>), intValue.ExternalInstance);
            DistributedCryptographyLib_ContactItem_externalReference.MuSigSignatures = externalParm1;
            SetDirty("Musigsignatures");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_ContactItem_externalReference == null )
            {
               DistributedCryptographyLib_ContactItem_externalReference = new DistricutedCryptographyLib.ContactItem();
            }
            return DistributedCryptographyLib_ContactItem_externalReference ;
         }

         set {
            DistributedCryptographyLib_ContactItem_externalReference = (DistricutedCryptographyLib.ContactItem)(value);
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

      protected DistricutedCryptographyLib.ContactItem DistributedCryptographyLib_ContactItem_externalReference=null ;
   }

}
