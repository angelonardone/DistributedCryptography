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
   public class SdtCodex32Processor : GxUserType, IGxExternalObject
   {
      public SdtCodex32Processor( )
      {
         /* Constructor for serialization */
      }

      public SdtCodex32Processor( IGxContext context )
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

      public string splitseed( string gxTp_seedHex ,
                               int gxTp_threshold ,
                               int gxTp_count )
      {
         string returnsplitseed;
         if ( DistributedCryptographyLib_Codex32Processor_externalReference == null )
         {
            DistributedCryptographyLib_Codex32Processor_externalReference = new DistricutedCryptographyLib.Codex32Processor();
         }
         returnsplitseed = "";
         returnsplitseed = (string)(DistributedCryptographyLib_Codex32Processor_externalReference.SplitSeed(gxTp_seedHex, gxTp_threshold, gxTp_count));
         return returnsplitseed ;
      }

      public string combineseed( string gxTp_sharesJson )
      {
         string returncombineseed;
         if ( DistributedCryptographyLib_Codex32Processor_externalReference == null )
         {
            DistributedCryptographyLib_Codex32Processor_externalReference = new DistricutedCryptographyLib.Codex32Processor();
         }
         returncombineseed = "";
         returncombineseed = (string)(DistributedCryptographyLib_Codex32Processor_externalReference.CombineSeed(gxTp_sharesJson));
         return returncombineseed ;
      }

      public string maskseed( string gxTp_seedHex )
      {
         string returnmaskseed;
         if ( DistributedCryptographyLib_Codex32Processor_externalReference == null )
         {
            DistributedCryptographyLib_Codex32Processor_externalReference = new DistricutedCryptographyLib.Codex32Processor();
         }
         returnmaskseed = "";
         returnmaskseed = (string)(DistributedCryptographyLib_Codex32Processor_externalReference.MaskSeed(gxTp_seedHex));
         return returnmaskseed ;
      }

      public string unmaskseed( string gxTp_padHex ,
                                string gxTp_maskedHex )
      {
         string returnunmaskseed;
         if ( DistributedCryptographyLib_Codex32Processor_externalReference == null )
         {
            DistributedCryptographyLib_Codex32Processor_externalReference = new DistricutedCryptographyLib.Codex32Processor();
         }
         returnunmaskseed = "";
         returnunmaskseed = (string)(DistributedCryptographyLib_Codex32Processor_externalReference.UnmaskSeed(gxTp_padHex, gxTp_maskedHex));
         return returnunmaskseed ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_Codex32Processor_externalReference == null )
            {
               DistributedCryptographyLib_Codex32Processor_externalReference = new DistricutedCryptographyLib.Codex32Processor();
            }
            return DistributedCryptographyLib_Codex32Processor_externalReference ;
         }

         set {
            DistributedCryptographyLib_Codex32Processor_externalReference = (DistricutedCryptographyLib.Codex32Processor)(value);
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

      protected DistricutedCryptographyLib.Codex32Processor DistributedCryptographyLib_Codex32Processor_externalReference=null ;
   }

}
