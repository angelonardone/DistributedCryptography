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
namespace GeneXus.Programs.googleauthenticator {
   [Serializable]
   public class SdtSetupCode : GxUserType, IGxExternalObject
   {
      public SdtSetupCode( )
      {
         /* Constructor for serialization */
      }

      public SdtSetupCode( IGxContext context )
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

      public string gxTpr_Account
      {
         get {
            if ( GoogleAuthenticator_SetupCode_externalReference == null )
            {
               GoogleAuthenticator_SetupCode_externalReference = new Google.Authenticator.SetupCode();
            }
            return GoogleAuthenticator_SetupCode_externalReference.Account ;
         }

      }

      public string gxTpr_Manualentrykey
      {
         get {
            if ( GoogleAuthenticator_SetupCode_externalReference == null )
            {
               GoogleAuthenticator_SetupCode_externalReference = new Google.Authenticator.SetupCode();
            }
            return GoogleAuthenticator_SetupCode_externalReference.ManualEntryKey ;
         }

      }

      public string gxTpr_Qrcodesetupimageurl
      {
         get {
            if ( GoogleAuthenticator_SetupCode_externalReference == null )
            {
               GoogleAuthenticator_SetupCode_externalReference = new Google.Authenticator.SetupCode();
            }
            return GoogleAuthenticator_SetupCode_externalReference.QrCodeSetupImageUrl ;
         }

      }

      public Object ExternalInstance
      {
         get {
            if ( GoogleAuthenticator_SetupCode_externalReference == null )
            {
               GoogleAuthenticator_SetupCode_externalReference = new Google.Authenticator.SetupCode();
            }
            return GoogleAuthenticator_SetupCode_externalReference ;
         }

         set {
            GoogleAuthenticator_SetupCode_externalReference = (Google.Authenticator.SetupCode)(value);
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

      protected Google.Authenticator.SetupCode GoogleAuthenticator_SetupCode_externalReference=null ;
   }

}
