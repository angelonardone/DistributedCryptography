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
   public class SdtBase32Encoding : GxUserType, IGxExternalObject
   {
      public SdtBase32Encoding( )
      {
         /* Constructor for serialization */
      }

      public SdtBase32Encoding( IGxContext context )
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

      public object tobytes( string gxTp_input )
      {
         object returntobytes;
         System.Byte[] externalParm0;
         externalParm0 = Google.Authenticator.Base32Encoding.ToBytes(gxTp_input);
         returntobytes = (object)(externalParm0);
         return returntobytes ;
      }

      public string tostring( object gxTp_input )
      {
         string returntostring;
         returntostring = "";
         returntostring = (string)(Google.Authenticator.Base32Encoding.ToString((System.Byte[])(gxTp_input)));
         return returntostring ;
      }

      public Object ExternalInstance
      {
         get {
            if ( GoogleAuthenticator_Base32Encoding_externalReference == null )
            {
               GoogleAuthenticator_Base32Encoding_externalReference = new Google.Authenticator.Base32Encoding();
            }
            return GoogleAuthenticator_Base32Encoding_externalReference ;
         }

         set {
            GoogleAuthenticator_Base32Encoding_externalReference = (Google.Authenticator.Base32Encoding)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected Google.Authenticator.Base32Encoding GoogleAuthenticator_Base32Encoding_externalReference=null ;
   }

}
