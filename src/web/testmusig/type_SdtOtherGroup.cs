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

      public Guid gxTpr_Referencegroupid
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.ReferenceGroupId ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.ReferenceGroupId = value;
            SetDirty("Referencegroupid");
         }

      }

      public bool gxTpr_Invitationdeclined
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.InvitationDeclined ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.InvitationDeclined = value;
            SetDirty("Invitationdeclined");
         }

      }

      public string gxTpr_Encpassword
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.EncPassword ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.EncPassword = value;
            SetDirty("Encpassword");
         }

      }

      public string gxTpr_Referenceusername
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.ReferenceUserName ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.ReferenceUserName = value;
            SetDirty("Referenceusername");
         }

      }

      public string gxTpr_Signature
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.Signature ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.Signature = value;
            SetDirty("Signature");
         }

      }

      public string gxTpr_Extpubkeymultisigreceiving
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.ExtPubKeyMultiSigReceiving ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.ExtPubKeyMultiSigReceiving = value;
            SetDirty("Extpubkeymultisigreceiving");
         }

      }

      public string gxTpr_Extpubkeymultisigchange
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference.ExtPubKeyMultiSigChange ;
         }

         set {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            testMuSig_OtherGroup_externalReference.ExtPubKeyMultiSigChange = value;
            SetDirty("Extpubkeymultisigchange");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( testMuSig_OtherGroup_externalReference == null )
            {
               testMuSig_OtherGroup_externalReference = new MultiSignature.Group.OtherGroup();
            }
            return testMuSig_OtherGroup_externalReference ;
         }

         set {
            testMuSig_OtherGroup_externalReference = (MultiSignature.Group.OtherGroup)(value);
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

      protected MultiSignature.Group.OtherGroup testMuSig_OtherGroup_externalReference=null ;
   }

}
