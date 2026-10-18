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

      public bool fromsdt( GeneXus.Programs.distributedcryptographylib.SdtGroupSDT gxTp_group )
      {
         bool returnfromsdt;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnfromsdt = false;
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = (DistricutedCryptographyLib.GroupSDT)(gxTp_group.ExternalInstance);
         returnfromsdt = (bool)(DistributedCryptographyLib_GroupProcessor_externalReference.FromSDT(externalParm0));
         return returnfromsdt ;
      }

      public GeneXus.Programs.distributedcryptographylib.SdtGroupSDT tosdt( )
      {
         GeneXus.Programs.distributedcryptographylib.SdtGroupSDT returntosdt;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returntosdt = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = DistributedCryptographyLib_GroupProcessor_externalReference.ToSDT();
         returntosdt.ExternalInstance = externalParm0;
         return returntosdt ;
      }

      public string createlegacyaddress( int gxTp_sequence ,
                                         bool gxTp_isChange ,
                                         string gxTp_networkType )
      {
         string returncreatelegacyaddress;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returncreatelegacyaddress = "";
         returncreatelegacyaddress = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.CreateLegacyAddress(gxTp_sequence, gxTp_isChange, gxTp_networkType));
         return returncreatelegacyaddress ;
      }

      public string buildlegacyspend( string gxTp_utxosJson ,
                                      string gxTp_sendTo ,
                                      string gxTp_amountBtc ,
                                      string gxTp_changeAddr ,
                                      string gxTp_feeBtc ,
                                      string gxTp_percentages ,
                                      bool gxTp_sendAll ,
                                      string gxTp_networkType )
      {
         string returnbuildlegacyspend;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnbuildlegacyspend = "";
         returnbuildlegacyspend = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.BuildLegacySpend(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_feeBtc, gxTp_percentages, gxTp_sendAll, gxTp_networkType));
         return returnbuildlegacyspend ;
      }

      public string signlegacyspend( string gxTp_bundleJson ,
                                     string gxTp_receivingExtPrivKey ,
                                     string gxTp_changeExtPrivKey ,
                                     int gxTp_finalPercent ,
                                     string gxTp_networkType )
      {
         string returnsignlegacyspend;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnsignlegacyspend = "";
         returnsignlegacyspend = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.SignLegacySpend(gxTp_bundleJson, gxTp_receivingExtPrivKey, gxTp_changeExtPrivKey, gxTp_finalPercent, gxTp_networkType));
         return returnsignlegacyspend ;
      }

      public string finalizelegacyspend( string gxTp_bundleJson ,
                                         int gxTp_percent ,
                                         string gxTp_networkType )
      {
         string returnfinalizelegacyspend;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnfinalizelegacyspend = "";
         returnfinalizelegacyspend = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.FinalizeLegacySpend(gxTp_bundleJson, gxTp_percent, gxTp_networkType));
         return returnfinalizelegacyspend ;
      }

      public string describelegacyspend( string gxTp_bundleJson ,
                                         string gxTp_myReceivingExtPubKey ,
                                         string gxTp_myChangeExtPubKey ,
                                         string gxTp_networkType )
      {
         string returndescribelegacyspend;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returndescribelegacyspend = "";
         returndescribelegacyspend = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.DescribeLegacySpend(gxTp_bundleJson, gxTp_myReceivingExtPubKey, gxTp_myChangeExtPubKey, gxTp_networkType));
         return returndescribelegacyspend ;
      }

      public string getlegacydescriptor( bool gxTp_isChange ,
                                         string gxTp_networkType )
      {
         string returngetlegacydescriptor;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returngetlegacydescriptor = "";
         returngetlegacydescriptor = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.GetLegacyDescriptor(gxTp_isChange, gxTp_networkType));
         return returngetlegacydescriptor ;
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
