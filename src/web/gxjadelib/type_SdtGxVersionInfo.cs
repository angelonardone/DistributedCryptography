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
   public class SdtGxVersionInfo : GxUserType, IGxExternalObject
   {
      public SdtGxVersionInfo( )
      {
         /* Constructor for serialization */
      }

      public SdtGxVersionInfo( IGxContext context )
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

      public string gxTpr_Jadeversion
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.JadeVersion ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.JadeVersion = value;
            SetDirty("Jadeversion");
         }

      }

      public int gxTpr_Otamaxchunk
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.OtaMaxChunk ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.OtaMaxChunk = value;
            SetDirty("Otamaxchunk");
         }

      }

      public string gxTpr_Config
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.Config ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.Config = value;
            SetDirty("Config");
         }

      }

      public string gxTpr_Boardtype
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.BoardType ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.BoardType = value;
            SetDirty("Boardtype");
         }

      }

      public string gxTpr_Features
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.Features ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.Features = value;
            SetDirty("Features");
         }

      }

      public string gxTpr_Efusemac
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.EfuseMac ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.EfuseMac = value;
            SetDirty("Efusemac");
         }

      }

      public string gxTpr_State
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.State ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.State = value;
            SetDirty("State");
         }

      }

      public string gxTpr_Networks
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.Networks ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.Networks = value;
            SetDirty("Networks");
         }

      }

      public bool gxTpr_Haspin
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.HasPin ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.HasPin = value;
            SetDirty("Haspin");
         }

      }

      public bool gxTpr_Haswallet
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.HasWallet ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.HasWallet = value;
            SetDirty("Haswallet");
         }

      }

      public bool gxTpr_Isunlocked
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference.IsUnlocked ;
         }

         set {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            GxJadeLib_GxVersionInfo_externalReference.IsUnlocked = value;
            SetDirty("Isunlocked");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxVersionInfo_externalReference == null )
            {
               GxJadeLib_GxVersionInfo_externalReference = new GxJadeLib.Models.GxVersionInfo();
            }
            return GxJadeLib_GxVersionInfo_externalReference ;
         }

         set {
            GxJadeLib_GxVersionInfo_externalReference = (GxJadeLib.Models.GxVersionInfo)(value);
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

      protected GxJadeLib.Models.GxVersionInfo GxJadeLib_GxVersionInfo_externalReference=null ;
   }

}
