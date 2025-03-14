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
   public class SdtGroupProcessor : GxUserType, IGxExternalObject
   {
      public SdtGroupProcessor( )
      {
         /* Constructor for serialization */
      }

      public SdtGroupProcessor( IGxContext context )
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

      public bool processgroup( GeneXus.Programs.testmusig.SdtGroupSDT gxTp_group )
      {
         bool returnprocessgroup;
         if ( testMuSig_GroupProcessor_externalReference == null )
         {
            testMuSig_GroupProcessor_externalReference = new MultiSignature.Services.GroupProcessor();
         }
         returnprocessgroup = false;
         MultiSignature.Group.GroupSDT externalParm0;
         externalParm0 = (MultiSignature.Group.GroupSDT)(gxTp_group.ExternalInstance);
         returnprocessgroup = (bool)(testMuSig_GroupProcessor_externalReference.ProcessGroup(externalParm0));
         return returnprocessgroup ;
      }

      public Object ExternalInstance
      {
         get {
            if ( testMuSig_GroupProcessor_externalReference == null )
            {
               testMuSig_GroupProcessor_externalReference = new MultiSignature.Services.GroupProcessor();
            }
            return testMuSig_GroupProcessor_externalReference ;
         }

         set {
            testMuSig_GroupProcessor_externalReference = (MultiSignature.Services.GroupProcessor)(value);
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

      protected MultiSignature.Services.GroupProcessor testMuSig_GroupProcessor_externalReference=null ;
   }

}
