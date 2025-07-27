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

      public bool processgroup( GeneXus.Programs.distributedcryptographylib.SdtGroupSDT gxTp_group )
      {
         bool returnprocessgroup;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnprocessgroup = false;
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = (DistricutedCryptographyLib.GroupSDT)(gxTp_group.ExternalInstance);
         returnprocessgroup = (bool)(DistributedCryptographyLib_GroupProcessor_externalReference.ProcessGroup(externalParm0));
         return returnprocessgroup ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
            {
               DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
            }
            return DistributedCryptographyLib_GroupProcessor_externalReference ;
         }

         set {
            DistributedCryptographyLib_GroupProcessor_externalReference = (DistricutedCryptographyLib.GroupProcessor)(value);
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

      protected DistricutedCryptographyLib.GroupProcessor DistributedCryptographyLib_GroupProcessor_externalReference=null ;
   }

}
