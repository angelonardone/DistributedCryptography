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

      public string buildlegacypsbt( string gxTp_utxosJson ,
                                     string gxTp_sendTo ,
                                     string gxTp_amountBtc ,
                                     string gxTp_changeAddr ,
                                     string gxTp_feeBtc ,
                                     bool gxTp_sendAll ,
                                     int gxTp_sequence ,
                                     bool gxTp_isChange ,
                                     string gxTp_networkType )
      {
         string returnbuildlegacypsbt;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnbuildlegacypsbt = "";
         returnbuildlegacypsbt = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.BuildLegacyPsbt(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_feeBtc, gxTp_sendAll, gxTp_sequence, gxTp_isChange, gxTp_networkType));
         return returnbuildlegacypsbt ;
      }

      public string signlegacypsbt( string gxTp_psbtBase64 ,
                                    int gxTp_sequence ,
                                    string gxTp_signerExtPrivKey ,
                                    string gxTp_networkType )
      {
         string returnsignlegacypsbt;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnsignlegacypsbt = "";
         returnsignlegacypsbt = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.SignLegacyPsbt(gxTp_psbtBase64, gxTp_sequence, gxTp_signerExtPrivKey, gxTp_networkType));
         return returnsignlegacypsbt ;
      }

      public string combinelegacypsbts( string gxTp_psbtsJson ,
                                        string gxTp_networkType )
      {
         string returncombinelegacypsbts;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returncombinelegacypsbts = "";
         returncombinelegacypsbts = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.CombineLegacyPsbts(gxTp_psbtsJson, gxTp_networkType));
         return returncombinelegacypsbts ;
      }

      public string finalizelegacypsbt( string gxTp_psbtBase64 ,
                                        string gxTp_networkType )
      {
         string returnfinalizelegacypsbt;
         if ( DistributedCryptographyLib_GroupProcessor_externalReference == null )
         {
            DistributedCryptographyLib_GroupProcessor_externalReference = new DistricutedCryptographyLib.GroupProcessor();
         }
         returnfinalizelegacypsbt = "";
         returnfinalizelegacypsbt = (string)(DistributedCryptographyLib_GroupProcessor_externalReference.FinalizeLegacyPsbt(gxTp_psbtBase64, gxTp_networkType));
         return returnfinalizelegacypsbt ;
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
