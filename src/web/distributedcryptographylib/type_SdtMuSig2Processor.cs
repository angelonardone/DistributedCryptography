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
   public class SdtMuSig2Processor : GxUserType, IGxExternalObject
   {
      public SdtMuSig2Processor( )
      {
         /* Constructor for serialization */
      }

      public SdtMuSig2Processor( IGxContext context )
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
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnfromsdt = false;
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = (DistricutedCryptographyLib.GroupSDT)(gxTp_group.ExternalInstance);
         returnfromsdt = (bool)(DistributedCryptographyLib_MuSig2Processor_externalReference.FromSDT(externalParm0));
         return returnfromsdt ;
      }

      public GeneXus.Programs.distributedcryptographylib.SdtGroupSDT tosdt( )
      {
         GeneXus.Programs.distributedcryptographylib.SdtGroupSDT returntosdt;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returntosdt = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         DistricutedCryptographyLib.GroupSDT externalParm0;
         externalParm0 = DistributedCryptographyLib_MuSig2Processor_externalReference.ToSDT();
         returntosdt.ExternalInstance = externalParm0;
         return returntosdt ;
      }

      public string createaddress( int gxTp_sequence ,
                                   bool gxTp_isChange ,
                                   string gxTp_networkType )
      {
         string returncreateaddress;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returncreateaddress = "";
         returncreateaddress = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.CreateAddress(gxTp_sequence, gxTp_isChange, gxTp_networkType));
         return returncreateaddress ;
      }

      public string getdescriptor( bool gxTp_isChange ,
                                   string gxTp_networkType )
      {
         string returngetdescriptor;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returngetdescriptor = "";
         returngetdescriptor = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.GetDescriptor(gxTp_isChange, gxTp_networkType));
         return returngetdescriptor ;
      }

      public string estimatevsize( string gxTp_utxosJson ,
                                   string gxTp_sendTo ,
                                   string gxTp_changeAddr ,
                                   bool gxTp_sendAll ,
                                   string gxTp_path ,
                                   string gxTp_networkType )
      {
         string returnestimatevsize;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnestimatevsize = "";
         returnestimatevsize = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.EstimateVsize(gxTp_utxosJson, gxTp_sendTo, gxTp_changeAddr, gxTp_sendAll, gxTp_path, gxTp_networkType));
         return returnestimatevsize ;
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
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnownerspend = "";
         returnownerspend = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.OwnerSpend(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_feeBtc, gxTp_sendAll, gxTp_receivingExtPrivKey, gxTp_changeExtPrivKey, gxTp_networkType));
         return returnownerspend ;
      }

      public string buildsession( string gxTp_utxosJson ,
                                  string gxTp_sendTo ,
                                  string gxTp_amountBtc ,
                                  string gxTp_changeAddr ,
                                  int gxTp_changeSequence ,
                                  string gxTp_feeBtc ,
                                  bool gxTp_sendAll ,
                                  string gxTp_signersJson ,
                                  string gxTp_networkType )
      {
         string returnbuildsession;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnbuildsession = "";
         returnbuildsession = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.BuildSession(gxTp_utxosJson, gxTp_sendTo, gxTp_amountBtc, gxTp_changeAddr, gxTp_changeSequence, gxTp_feeBtc, gxTp_sendAll, gxTp_signersJson, gxTp_networkType));
         return returnbuildsession ;
      }

      public string describesession( string gxTp_sessionJson ,
                                     string gxTp_myReceivingExtPubKey ,
                                     string gxTp_myChangeExtPubKey ,
                                     string gxTp_networkType )
      {
         string returndescribesession;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returndescribesession = "";
         returndescribesession = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.DescribeSession(gxTp_sessionJson, gxTp_myReceivingExtPubKey, gxTp_myChangeExtPubKey, gxTp_networkType));
         return returndescribesession ;
      }

      public string createnonces( string gxTp_sessionJson ,
                                  string gxTp_myReceivingExtPubKey ,
                                  string gxTp_myChangeExtPubKey ,
                                  string gxTp_networkType )
      {
         string returncreatenonces;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returncreatenonces = "";
         returncreatenonces = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.CreateNonces(gxTp_sessionJson, gxTp_myReceivingExtPubKey, gxTp_myChangeExtPubKey, gxTp_networkType));
         return returncreatenonces ;
      }

      public string sign( string gxTp_sessionJson ,
                          string gxTp_publicNoncesJson ,
                          string gxTp_receivingExtPrivKey ,
                          string gxTp_changeExtPrivKey ,
                          string gxTp_networkType )
      {
         string returnsign;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnsign = "";
         returnsign = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.Sign(gxTp_sessionJson, gxTp_publicNoncesJson, gxTp_receivingExtPrivKey, gxTp_changeExtPrivKey, gxTp_networkType));
         return returnsign ;
      }

      public string finalize( string gxTp_sessionJson ,
                              string gxTp_publicNoncesJson ,
                              string gxTp_partialSignaturesJson ,
                              string gxTp_networkType )
      {
         string returnfinalize;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnfinalize = "";
         returnfinalize = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.Finalize(gxTp_sessionJson, gxTp_publicNoncesJson, gxTp_partialSignaturesJson, gxTp_networkType));
         return returnfinalize ;
      }

      public string verifycoins( string gxTp_sessionJson ,
                                 string gxTp_prevTxsJson ,
                                 string gxTp_networkType )
      {
         string returnverifycoins;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnverifycoins = "";
         returnverifycoins = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.VerifyCoins(gxTp_sessionJson, gxTp_prevTxsJson, gxTp_networkType));
         return returnverifycoins ;
      }

      public string abort( string gxTp_sessionId )
      {
         string returnabort;
         if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
         {
            DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
         }
         returnabort = "";
         returnabort = (string)(DistributedCryptographyLib_MuSig2Processor_externalReference.Abort(gxTp_sessionId));
         return returnabort ;
      }

      public Object ExternalInstance
      {
         get {
            if ( DistributedCryptographyLib_MuSig2Processor_externalReference == null )
            {
               DistributedCryptographyLib_MuSig2Processor_externalReference = new DistricutedCryptographyLib.MuSig2Processor();
            }
            return DistributedCryptographyLib_MuSig2Processor_externalReference ;
         }

         set {
            DistributedCryptographyLib_MuSig2Processor_externalReference = (DistricutedCryptographyLib.MuSig2Processor)(value);
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

      protected DistricutedCryptographyLib.MuSig2Processor DistributedCryptographyLib_MuSig2Processor_externalReference=null ;
   }

}
