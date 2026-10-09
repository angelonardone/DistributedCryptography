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
   public class SdtDelegatedProcessor : GxUserType, IGxExternalObject
   {
      public SdtDelegatedProcessor( )
      {
         /* Constructor for serialization */
      }

      public SdtDelegatedProcessor( IGxContext context )
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
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnfromsdt = false;
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = (DistricutedCryptographyLib.GroupSDT)(gxTp_group.ExternalInstance);
         returnfromsdt = (bool)(DistributedCryptographyLib_DelegatedProcessor_externalReference.FromSDT(externalParm0));
         return returnfromsdt ;
      }

      public GeneXus.Programs.distributedcryptographylib.SdtGroupSDT tosdt( )
      {
         GeneXus.Programs.distributedcryptographylib.SdtGroupSDT returntosdt;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returntosdt = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = DistributedCryptographyLib_DelegatedProcessor_externalReference.ToSDT();
         returntosdt.ExternalInstance = externalParm0;
         return returntosdt ;
      }

      public string createaddress( int gxTp_sequence ,
                                   bool gxTp_isChange ,
                                   string gxTp_networkType )
      {
         string returncreateaddress;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returncreateaddress = "";
         returncreateaddress = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.CreateAddress(gxTp_sequence, gxTp_isChange, gxTp_networkType));
         return returncreateaddress ;
      }

      public string getdescriptor( bool gxTp_isChange ,
                                   string gxTp_networkType )
      {
         string returngetdescriptor;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returngetdescriptor = "";
         returngetdescriptor = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.GetDescriptor(gxTp_isChange, gxTp_networkType));
         return returngetdescriptor ;
      }

      public string estimatevsize( string gxTp_utxosJson ,
                                   string gxTp_sendTo ,
                                   string gxTp_changeAddr ,
                                   bool gxTp_sendAll ,
                                   bool gxTp_asOwner ,
                                   string gxTp_networkType )
      {
         string returnestimatevsize;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnestimatevsize = "";
         returnestimatevsize = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.EstimateVsize(gxTp_utxosJson, gxTp_sendTo, gxTp_changeAddr, gxTp_sendAll, gxTp_asOwner, gxTp_networkType));
         return returnestimatevsize ;
      }

      public string buildspend( string gxTp_utxosJson ,
                                string gxTp_sendTo ,
                                string gxTp_amountBtc ,
                                string gxTp_changeAddr ,
                                string gxTp_feeBtc ,
                                string gxTp_percentages ,
                                bool gxTp_sendAll ,
                                string gxTp_networkType )
      {
         string returnbuildspend;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnbuildspend = "";
         returnbuildspend = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.BuildSpend(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_feeBtc, gxTp_percentages, gxTp_sendAll, gxTp_networkType));
         return returnbuildspend ;
      }

      public string signspend( string gxTp_bundleJson ,
                               string gxTp_receivingExtPrivKey ,
                               string gxTp_changeExtPrivKey ,
                               int gxTp_finalPercent ,
                               string gxTp_networkType )
      {
         string returnsignspend;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnsignspend = "";
         returnsignspend = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.SignSpend(gxTp_bundleJson, gxTp_receivingExtPrivKey, gxTp_changeExtPrivKey, gxTp_finalPercent, gxTp_networkType));
         return returnsignspend ;
      }

      public string finalizespend( string gxTp_bundleJson ,
                                   int gxTp_percent ,
                                   string gxTp_networkType )
      {
         string returnfinalizespend;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnfinalizespend = "";
         returnfinalizespend = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.FinalizeSpend(gxTp_bundleJson, gxTp_percent, gxTp_networkType));
         return returnfinalizespend ;
      }

      public string describespend( string gxTp_bundleJson ,
                                   string gxTp_myReceivingExtPubKey ,
                                   string gxTp_myChangeExtPubKey ,
                                   string gxTp_networkType )
      {
         string returndescribespend;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returndescribespend = "";
         returndescribespend = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.DescribeSpend(gxTp_bundleJson, gxTp_myReceivingExtPubKey, gxTp_myChangeExtPubKey, gxTp_networkType));
         return returndescribespend ;
      }

      public string ownerspend( string gxTp_utxosJson ,
                                string gxTp_sendTo ,
                                string gxTp_amountBtc ,
                                string gxTp_changeAddr ,
                                string gxTp_feeBtc ,
                                bool gxTp_sendAll ,
                                string gxTp_receivingExtPrivKey ,
                                string gxTp_changeExtPrivKey ,
                                string gxTp_networkType )
      {
         string returnownerspend;
         if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
         {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
         }
         returnownerspend = "";
         returnownerspend = (string)(DistributedCryptographyLib_DelegatedProcessor_externalReference.OwnerSpend(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_feeBtc, gxTp_sendAll, gxTp_receivingExtPrivKey, gxTp_changeExtPrivKey, gxTp_networkType));
         return returnownerspend ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_DelegatedProcessor_externalReference == null )
            {
               DistributedCryptographyLib_DelegatedProcessor_externalReference = new DistricutedCryptographyLib.DelegatedProcessor();
            }
            return DistributedCryptographyLib_DelegatedProcessor_externalReference ;
         }

         set {
            DistributedCryptographyLib_DelegatedProcessor_externalReference = (DistricutedCryptographyLib.DelegatedProcessor)(value);
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

      protected DistricutedCryptographyLib.DelegatedProcessor DistributedCryptographyLib_DelegatedProcessor_externalReference=null ;
   }

}
