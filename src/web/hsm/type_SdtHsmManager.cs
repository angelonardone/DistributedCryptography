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
namespace GeneXus.Programs.hsm {
   [Serializable]
   public class SdtHsmManager : GxUserType, IGxExternalObject
   {
      public SdtHsmManager( )
      {
         /* Constructor for serialization */
      }

      public SdtHsmManager( IGxContext context )
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

      public bool initialize( string gxTp_extPrivKey ,
                              string gxTp_networkType )
      {
         bool returninitialize;
         returninitialize = false;
         returninitialize = (bool)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.Initialize(gxTp_extPrivKey, gxTp_networkType));
         return returninitialize ;
      }

      public string getpublickey( int gxTp_index )
      {
         string returngetpublickey;
         returngetpublickey = "";
         returngetpublickey = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.GetPublicKey(gxTp_index));
         return returngetpublickey ;
      }

      public string sign( string gxTp_message ,
                          int gxTp_index )
      {
         string returnsign;
         returnsign = "";
         returnsign = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.Sign(gxTp_message, gxTp_index));
         return returnsign ;
      }

      public string signschnorr( string gxTp_message ,
                                 int gxTp_index )
      {
         string returnsignschnorr;
         returnsignschnorr = "";
         returnsignschnorr = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.SignSchnorr(gxTp_message, gxTp_index));
         return returnsignschnorr ;
      }

      public string encrypttopubkey( string gxTp_message ,
                                     string gxTp_recipientPubKey )
      {
         string returnencrypttopubkey;
         returnencrypttopubkey = "";
         returnencrypttopubkey = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.EncryptToPubKey(gxTp_message, gxTp_recipientPubKey));
         return returnencrypttopubkey ;
      }

      public string encrypt( string gxTp_message ,
                             int gxTp_index )
      {
         string returnencrypt;
         returnencrypt = "";
         returnencrypt = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.Encrypt(gxTp_message, gxTp_index));
         return returnencrypt ;
      }

      public string decrypt( string gxTp_cipher ,
                             int gxTp_index )
      {
         string returndecrypt;
         returndecrypt = "";
         returndecrypt = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.Decrypt(gxTp_cipher, gxTp_index));
         return returndecrypt ;
      }

      public void clear( )
      {
         DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.Clear() ;
         return  ;
      }

      public string getlasterror( )
      {
         string returngetlasterror;
         returngetlasterror = "";
         returngetlasterror = (string)(DistricutedCryptographyLib.DistricutedCryptographyLib.HsmManager.GetLastError());
         return returngetlasterror ;
      }

      public Object ExternalInstance
      {
         get {
            return null ;
         }

         set {
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

   }

}
