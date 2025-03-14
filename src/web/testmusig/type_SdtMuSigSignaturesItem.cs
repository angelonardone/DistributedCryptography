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
            if ( testMuSig_MuSigSignaturesItem_externalReference == null )
            {
               testMuSig_MuSigSignaturesItem_externalReference = new MultiSignature.Group.MuSigSignaturesItem();
            }
            return testMuSig_MuSigSignaturesItem_externalReference.Signature ;
         }

         set {
            if ( testMuSig_MuSigSignaturesItem_externalReference == null )
            {
               testMuSig_MuSigSignaturesItem_externalReference = new MultiSignature.Group.MuSigSignaturesItem();
            }
            testMuSig_MuSigSignaturesItem_externalReference.Signature = value;
            SetDirty("Signature");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( testMuSig_MuSigSignaturesItem_externalReference == null )
            {
               testMuSig_MuSigSignaturesItem_externalReference = new MultiSignature.Group.MuSigSignaturesItem();
            }
            return testMuSig_MuSigSignaturesItem_externalReference ;
         }

         set {
            testMuSig_MuSigSignaturesItem_externalReference = (MultiSignature.Group.MuSigSignaturesItem)(value);
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

      protected MultiSignature.Group.MuSigSignaturesItem testMuSig_MuSigSignaturesItem_externalReference=null ;
   }

}
