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
   public class SdtTwoFactorAuthenticator : GxUserType, IGxExternalObject
   {
      public SdtTwoFactorAuthenticator( )
      {
         /* Constructor for serialization */
      }

      public SdtTwoFactorAuthenticator( IGxContext context )
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

      public GeneXus.Programs.googleauthenticator.SdtSetupCode generatesetupcode( string gxTp_issuer ,
                                                                                  string gxTp_accountTitleNoSpaces ,
                                                                                  string gxTp_accountSecretKey ,
                                                                                  bool gxTp_secretIsBase32 ,
                                                                                  int gxTp_qrPixelsPerModule )
      {
         GeneXus.Programs.googleauthenticator.SdtSetupCode returngeneratesetupcode;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngeneratesetupcode = new GeneXus.Programs.googleauthenticator.SdtSetupCode(context);
         Google.Authenticator.SetupCode externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GenerateSetupCode(gxTp_issuer, gxTp_accountTitleNoSpaces, gxTp_accountSecretKey, gxTp_secretIsBase32, gxTp_qrPixelsPerModule);
         returngeneratesetupcode.ExternalInstance = externalParm0;
         return returngeneratesetupcode ;
      }

      public GeneXus.Programs.googleauthenticator.SdtSetupCode generatesetupcode1( string gxTp_issuer ,
                                                                                   string gxTp_accountTitleNoSpaces ,
                                                                                   object gxTp_accountSecretKey ,
                                                                                   int gxTp_qrPixelsPerModule ,
                                                                                   bool gxTp_generateQrCode )
      {
         GeneXus.Programs.googleauthenticator.SdtSetupCode returngeneratesetupcode1;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngeneratesetupcode1 = new GeneXus.Programs.googleauthenticator.SdtSetupCode(context);
         Google.Authenticator.SetupCode externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GenerateSetupCode(gxTp_issuer, gxTp_accountTitleNoSpaces, (System.Byte[])(gxTp_accountSecretKey), gxTp_qrPixelsPerModule, gxTp_generateQrCode);
         returngeneratesetupcode1.ExternalInstance = externalParm0;
         return returngeneratesetupcode1 ;
      }

      public string generatepinatinterval( string gxTp_accountSecretKey ,
                                           long gxTp_counter ,
                                           int gxTp_digits ,
                                           bool gxTp_secretIsBase32 )
      {
         string returngeneratepinatinterval;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngeneratepinatinterval = "";
         returngeneratepinatinterval = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GeneratePINAtInterval(gxTp_accountSecretKey, gxTp_counter, gxTp_digits, gxTp_secretIsBase32));
         return returngeneratepinatinterval ;
      }

      public string generatepinatinterval1( object gxTp_accountSecretKey ,
                                            long gxTp_counter ,
                                            int gxTp_digits )
      {
         string returngeneratepinatinterval1;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngeneratepinatinterval1 = "";
         returngeneratepinatinterval1 = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GeneratePINAtInterval((System.Byte[])(gxTp_accountSecretKey), gxTp_counter, gxTp_digits));
         return returngeneratepinatinterval1 ;
      }

      public bool validatetwofactorpin( string gxTp_accountSecretKey ,
                                        string gxTp_twoFactorCodeFromClient ,
                                        bool gxTp_secretIsBase32 )
      {
         bool returnvalidatetwofactorpin;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returnvalidatetwofactorpin = false;
         returnvalidatetwofactorpin = (bool)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.ValidateTwoFactorPIN(gxTp_accountSecretKey, gxTp_twoFactorCodeFromClient, gxTp_secretIsBase32));
         return returnvalidatetwofactorpin ;
      }

      public bool validatetwofactorpin1( string gxTp_accountSecretKey ,
                                         string gxTp_twoFactorCodeFromClient ,
                                         object gxTp_timeTolerance ,
                                         bool gxTp_secretIsBase32 )
      {
         bool returnvalidatetwofactorpin1;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returnvalidatetwofactorpin1 = false;
         returnvalidatetwofactorpin1 = (bool)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.ValidateTwoFactorPIN(gxTp_accountSecretKey, gxTp_twoFactorCodeFromClient, (System.TimeSpan)(gxTp_timeTolerance), gxTp_secretIsBase32));
         return returnvalidatetwofactorpin1 ;
      }

      public bool validatetwofactorpin2( object gxTp_accountSecretKey ,
                                         string gxTp_twoFactorCodeFromClient )
      {
         bool returnvalidatetwofactorpin2;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returnvalidatetwofactorpin2 = false;
         returnvalidatetwofactorpin2 = (bool)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.ValidateTwoFactorPIN((System.Byte[])(gxTp_accountSecretKey), gxTp_twoFactorCodeFromClient));
         return returnvalidatetwofactorpin2 ;
      }

      public bool validatetwofactorpin3( object gxTp_accountSecretKey ,
                                         string gxTp_twoFactorCodeFromClient ,
                                         object gxTp_timeTolerance )
      {
         bool returnvalidatetwofactorpin3;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returnvalidatetwofactorpin3 = false;
         returnvalidatetwofactorpin3 = (bool)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.ValidateTwoFactorPIN((System.Byte[])(gxTp_accountSecretKey), gxTp_twoFactorCodeFromClient, (System.TimeSpan)(gxTp_timeTolerance)));
         return returnvalidatetwofactorpin3 ;
      }

      public string getcurrentpin( string gxTp_accountSecretKey ,
                                   bool gxTp_secretIsBase32 )
      {
         string returngetcurrentpin;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngetcurrentpin = "";
         returngetcurrentpin = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPIN(gxTp_accountSecretKey, gxTp_secretIsBase32));
         return returngetcurrentpin ;
      }

      public string getcurrentpin1( string gxTp_accountSecretKey ,
                                    DateTime gxTp_now ,
                                    bool gxTp_secretIsBase32 )
      {
         string returngetcurrentpin1;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngetcurrentpin1 = "";
         returngetcurrentpin1 = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPIN(gxTp_accountSecretKey, gxTp_now, gxTp_secretIsBase32));
         return returngetcurrentpin1 ;
      }

      public string getcurrentpin2( object gxTp_accountSecretKey )
      {
         string returngetcurrentpin2;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngetcurrentpin2 = "";
         returngetcurrentpin2 = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPIN((System.Byte[])(gxTp_accountSecretKey)));
         return returngetcurrentpin2 ;
      }

      public string getcurrentpin3( object gxTp_accountSecretKey ,
                                    DateTime gxTp_now )
      {
         string returngetcurrentpin3;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngetcurrentpin3 = "";
         returngetcurrentpin3 = (string)(GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPIN((System.Byte[])(gxTp_accountSecretKey), gxTp_now));
         return returngetcurrentpin3 ;
      }

      public GxSimpleCollection<string> getcurrentpins( string gxTp_accountSecretKey ,
                                                        bool gxTp_secretIsBase32 )
      {
         GxSimpleCollection<string> returngetcurrentpins;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         returngetcurrentpins = new GxSimpleCollection<string>();
         System.String[] externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPINs(gxTp_accountSecretKey, gxTp_secretIsBase32);
         returngetcurrentpins.ExternalInstance = (IList)CollectionUtils.ConvertToInternal( typeof(System.String[]), externalParm0);
         return returngetcurrentpins ;
      }

      public object getcurrentpins1( string gxTp_accountSecretKey ,
                                     object gxTp_timeTolerance ,
                                     bool gxTp_secretIsBase32 )
      {
         object returngetcurrentpins1;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         System.String[] externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPINs(gxTp_accountSecretKey, (System.TimeSpan)(gxTp_timeTolerance), gxTp_secretIsBase32);
         returngetcurrentpins1 = (object)(externalParm0);
         return returngetcurrentpins1 ;
      }

      public object getcurrentpins2( object gxTp_accountSecretKey )
      {
         object returngetcurrentpins2;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         System.String[] externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPINs((System.Byte[])(gxTp_accountSecretKey));
         returngetcurrentpins2 = (object)(externalParm0);
         return returngetcurrentpins2 ;
      }

      public object getcurrentpins3( object gxTp_accountSecretKey ,
                                     object gxTp_timeTolerance )
      {
         object returngetcurrentpins3;
         if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
         {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
         }
         System.String[] externalParm0;
         externalParm0 = GoogleAuthenticator_TwoFactorAuthenticator_externalReference.GetCurrentPINs((System.Byte[])(gxTp_accountSecretKey), (System.TimeSpan)(gxTp_timeTolerance));
         returngetcurrentpins3 = (object)(externalParm0);
         return returngetcurrentpins3 ;
      }

      public Object ExternalInstance
      {
         get {
            if ( GoogleAuthenticator_TwoFactorAuthenticator_externalReference == null )
            {
               GoogleAuthenticator_TwoFactorAuthenticator_externalReference = new Google.Authenticator.TwoFactorAuthenticator();
            }
            return GoogleAuthenticator_TwoFactorAuthenticator_externalReference ;
         }

         set {
            GoogleAuthenticator_TwoFactorAuthenticator_externalReference = (Google.Authenticator.TwoFactorAuthenticator)(value);
         }

      }

      public void initialize( )
      {
         return  ;
      }

      protected Google.Authenticator.TwoFactorAuthenticator GoogleAuthenticator_TwoFactorAuthenticator_externalReference=null ;
   }

}
