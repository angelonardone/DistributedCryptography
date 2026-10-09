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
namespace GeneXus.Programs.gxjadelib {
   [Serializable]
   public class SdtGxHsmInfo : GxUserType, IGxExternalObject
   {
      public SdtGxHsmInfo( )
      {
         /* Constructor for serialization */
      }

      public SdtGxHsmInfo( IGxContext context )
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

      public bool gxTpr_Active
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.Active ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.Active = value;
            SetDirty("Active");
         }

      }

      public string gxTpr_Networks
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.Networks ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.Networks = value;
            SetDirty("Networks");
         }

      }

      public string gxTpr_Mainnetrootpath
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.MainnetRootPath ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.MainnetRootPath = value;
            SetDirty("Mainnetrootpath");
         }

      }

      public string gxTpr_Testnetrootpath
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.TestnetRootPath ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.TestnetRootPath = value;
            SetDirty("Testnetrootpath");
         }

      }

      public string gxTpr_Mainnetrootpubkey
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.MainnetRootPubkey ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.MainnetRootPubkey = value;
            SetDirty("Mainnetrootpubkey");
         }

      }

      public string gxTpr_Testnetrootpubkey
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.TestnetRootPubkey ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.TestnetRootPubkey = value;
            SetDirty("Testnetrootpubkey");
         }

      }

      public long gxTpr_Operationscount
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.OperationsCount ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.OperationsCount = value;
            SetDirty("Operationscount");
         }

      }

      public long gxTpr_Autolocktimeout
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.AutoLockTimeout ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.AutoLockTimeout = value;
            SetDirty("Autolocktimeout");
         }

      }

      public long gxTpr_Autolockremaining
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference.AutoLockRemaining ;
         }

         set {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            GxJadeLib_GxHsmInfo_externalReference.AutoLockRemaining = value;
            SetDirty("Autolockremaining");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxHsmInfo_externalReference == null )
            {
               GxJadeLib_GxHsmInfo_externalReference = new GxJadeLib.Models.GxHsmInfo();
            }
            return GxJadeLib_GxHsmInfo_externalReference ;
         }

         set {
            GxJadeLib_GxHsmInfo_externalReference = (GxJadeLib.Models.GxHsmInfo)(value);
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

      protected GxJadeLib.Models.GxHsmInfo GxJadeLib_GxHsmInfo_externalReference=null ;
   }

}
