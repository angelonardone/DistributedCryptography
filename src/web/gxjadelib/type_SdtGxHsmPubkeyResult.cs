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
   public class SdtGxHsmPubkeyResult : GxUserType, IGxExternalObject
   {
      public SdtGxHsmPubkeyResult( )
      {
         /* Constructor for serialization */
      }

      public SdtGxHsmPubkeyResult( IGxContext context )
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

      public string gxTpr_Pubkey
      {
         get {
            if ( GxJadeLib_GxHsmPubkeyResult_externalReference == null )
            {
               GxJadeLib_GxHsmPubkeyResult_externalReference = new GxJadeLib.Models.GxHsmPubkeyResult();
            }
            return GxJadeLib_GxHsmPubkeyResult_externalReference.Pubkey ;
         }

         set {
            if ( GxJadeLib_GxHsmPubkeyResult_externalReference == null )
            {
               GxJadeLib_GxHsmPubkeyResult_externalReference = new GxJadeLib.Models.GxHsmPubkeyResult();
            }
            GxJadeLib_GxHsmPubkeyResult_externalReference.Pubkey = value;
            SetDirty("Pubkey");
         }

      }

      public string gxTpr_Path
      {
         get {
            if ( GxJadeLib_GxHsmPubkeyResult_externalReference == null )
            {
               GxJadeLib_GxHsmPubkeyResult_externalReference = new GxJadeLib.Models.GxHsmPubkeyResult();
            }
            return GxJadeLib_GxHsmPubkeyResult_externalReference.Path ;
         }

         set {
            if ( GxJadeLib_GxHsmPubkeyResult_externalReference == null )
            {
               GxJadeLib_GxHsmPubkeyResult_externalReference = new GxJadeLib.Models.GxHsmPubkeyResult();
            }
            GxJadeLib_GxHsmPubkeyResult_externalReference.Path = value;
            SetDirty("Path");
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GxJadeLib_GxHsmPubkeyResult_externalReference == null )
            {
               GxJadeLib_GxHsmPubkeyResult_externalReference = new GxJadeLib.Models.GxHsmPubkeyResult();
            }
            return GxJadeLib_GxHsmPubkeyResult_externalReference ;
         }

         set {
            GxJadeLib_GxHsmPubkeyResult_externalReference = (GxJadeLib.Models.GxHsmPubkeyResult)(value);
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

      protected GxJadeLib.Models.GxHsmPubkeyResult GxJadeLib_GxHsmPubkeyResult_externalReference=null ;
   }

}
