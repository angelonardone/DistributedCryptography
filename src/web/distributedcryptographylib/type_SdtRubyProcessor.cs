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
   public class SdtRubyProcessor : GxUserType, IGxExternalObject
   {
      public SdtRubyProcessor( )
      {
         /* Constructor for serialization */
      }

      public SdtRubyProcessor( IGxContext context )
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

      public string finddevices( )
      {
         string returnfinddevices;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnfinddevices = "";
         returnfinddevices = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.FindDevices());
         return returnfinddevices ;
      }

      public string status( string gxTp_device )
      {
         string returnstatus;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnstatus = "";
         returnstatus = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.Status(gxTp_device));
         return returnstatus ;
      }

      public string logout( string gxTp_device )
      {
         string returnlogout;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnlogout = "";
         returnlogout = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.Logout(gxTp_device));
         return returnlogout ;
      }

      public string createoracle( )
      {
         string returncreateoracle;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returncreateoracle = "";
         returncreateoracle = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.CreateOracle());
         return returncreateoracle ;
      }

      public string pair( string gxTp_device ,
                          string gxTp_oracleKey ,
                          string gxTp_encryptedOracle )
      {
         string returnpair;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnpair = "";
         returnpair = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.Pair(gxTp_device, gxTp_oracleKey, gxTp_encryptedOracle));
         return returnpair ;
      }

      public string unlock( string gxTp_device ,
                            string gxTp_networkType ,
                            string gxTp_oracleKey ,
                            string gxTp_encryptedOracle )
      {
         string returnunlock;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnunlock = "";
         returnunlock = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.Unlock(gxTp_device, gxTp_networkType, gxTp_oracleKey, gxTp_encryptedOracle));
         return returnunlock ;
      }

      public string getpublicinfo( string gxTp_device ,
                                   string gxTp_networkType )
      {
         string returngetpublicinfo;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returngetpublicinfo = "";
         returngetpublicinfo = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.GetPublicInfo(gxTp_device, gxTp_networkType));
         return returngetpublicinfo ;
      }

      public string getutilitykeys( string gxTp_device ,
                                    string gxTp_networkType )
      {
         string returngetutilitykeys;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returngetutilitykeys = "";
         returngetutilitykeys = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.GetUtilityKeys(gxTp_device, gxTp_networkType));
         return returngetutilitykeys ;
      }

      public string signtransaction( string gxTp_device ,
                                     string gxTp_networkType ,
                                     string gxTp_inputsJson ,
                                     string gxTp_sendTo ,
                                     string gxTp_amountBtc ,
                                     string gxTp_changeTo ,
                                     int gxTp_changeChain ,
                                     int gxTp_changeIndex ,
                                     string gxTp_feeBtc ,
                                     bool gxTp_sendAll )
      {
         string returnsigntransaction;
         if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
         {
            DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
         }
         returnsigntransaction = "";
         returnsigntransaction = (string)(DistributedCryptographyLib_RubyProcessor_externalReference.SignTransaction(gxTp_device, gxTp_networkType, gxTp_inputsJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeTo, gxTp_changeChain, gxTp_changeIndex, gxTp_feeBtc, gxTp_sendAll));
         return returnsigntransaction ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_RubyProcessor_externalReference == null )
            {
               DistributedCryptographyLib_RubyProcessor_externalReference = new DistricutedCryptographyLib.RubyProcessor();
            }
            return DistributedCryptographyLib_RubyProcessor_externalReference ;
         }

         set {
            DistributedCryptographyLib_RubyProcessor_externalReference = (DistricutedCryptographyLib.RubyProcessor)(value);
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

      protected DistricutedCryptographyLib.RubyProcessor DistributedCryptographyLib_RubyProcessor_externalReference=null ;
   }

}
