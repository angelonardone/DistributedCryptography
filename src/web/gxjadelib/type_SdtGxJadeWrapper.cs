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
   public class SdtGxJadeWrapper : GxUserType, IGxExternalObject
   {
      public SdtGxJadeWrapper( )
      {
         /* Constructor for serialization */
      }

      public SdtGxJadeWrapper( IGxContext context )
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

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult connect( string gxTp_portName )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnconnect;
         returnconnect = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.Connect(gxTp_portName);
         returnconnect.ExternalInstance = externalParm0;
         return returnconnect ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult connectauto( )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnconnectauto;
         returnconnectauto = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.ConnectAuto();
         returnconnectauto.ExternalInstance = externalParm0;
         return returnconnectauto ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult connecttcp( string gxTp_host ,
                                                                           int gxTp_port )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnconnecttcp;
         returnconnecttcp = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.ConnectTcp(gxTp_host, gxTp_port);
         returnconnecttcp.ExternalInstance = externalParm0;
         return returnconnecttcp ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult connectqemu( )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnconnectqemu;
         returnconnectqemu = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.ConnectQemu();
         returnconnectqemu.ExternalInstance = externalParm0;
         return returnconnectqemu ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult disconnect( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returndisconnect;
         returndisconnect = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.Disconnect(gxTp_connectionId);
         returndisconnect.ExternalInstance = externalParm0;
         return returndisconnect ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult isconnected( Guid gxTp_connectionId ,
                                                                            out bool gxTp_isConnected )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnisconnected;
         gxTp_isConnected = false;
         returnisconnected = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.IsConnected(gxTp_connectionId, out gxTp_isConnected);
         returnisconnected.ExternalInstance = externalParm0;
         return returnisconnected ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult listports( out string gxTp_ports )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnlistports;
         gxTp_ports = "";
         returnlistports = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.ListPorts(out gxTp_ports);
         returnlistports.ExternalInstance = externalParm0;
         return returnlistports ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult drain( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returndrain;
         returndrain = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.Drain(gxTp_connectionId);
         returndrain.ExternalInstance = externalParm0;
         return returndrain ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult getversioninfo( Guid gxTp_connectionId ,
                                                                               out GeneXus.Programs.gxjadelib.SdtGxVersionInfo gxTp_info )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returngetversioninfo;
         gxTp_info = new GeneXus.Programs.gxjadelib.SdtGxVersionInfo(context);
         returngetversioninfo = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxVersionInfo externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.GetVersionInfo(gxTp_connectionId, out externalParm1);
         returngetversioninfo.ExternalInstance = externalParm0;
         gxTp_info.ExternalInstance = externalParm1;
         return returngetversioninfo ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult addentropy( Guid gxTp_connectionId ,
                                                                           string gxTp_entropyHex )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnaddentropy;
         returnaddentropy = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.AddEntropy(gxTp_connectionId, gxTp_entropyHex);
         returnaddentropy.ExternalInstance = externalParm0;
         return returnaddentropy ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult setmnemonic( Guid gxTp_connectionId ,
                                                                            string gxTp_mnemonic ,
                                                                            string gxTp_passphrase )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnsetmnemonic;
         returnsetmnemonic = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.SetMnemonic(gxTp_connectionId, gxTp_mnemonic, gxTp_passphrase);
         returnsetmnemonic.ExternalInstance = externalParm0;
         return returnsetmnemonic ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult authuser( Guid gxTp_connectionId ,
                                                                         string gxTp_network )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnauthuser;
         returnauthuser = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.AuthUser(gxTp_connectionId, gxTp_network);
         returnauthuser.ExternalInstance = externalParm0;
         return returnauthuser ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult authuserwithserver( Guid gxTp_connectionId ,
                                                                                   string gxTp_network ,
                                                                                   string gxTp_pinServerUrl )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnauthuserwithserver;
         returnauthuserwithserver = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.AuthUserWithServer(gxTp_connectionId, gxTp_network, gxTp_pinServerUrl);
         returnauthuserwithserver.ExternalInstance = externalParm0;
         return returnauthuserwithserver ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult logout( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnlogout;
         returnlogout = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.Logout(gxTp_connectionId);
         returnlogout.ExternalInstance = externalParm0;
         return returnlogout ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult getxpub( Guid gxTp_connectionId ,
                                                                        string gxTp_network ,
                                                                        string gxTp_pathString ,
                                                                        out GeneXus.Programs.gxjadelib.SdtGxXpubResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returngetxpub;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxXpubResult(context);
         returngetxpub = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxXpubResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.GetXpub(gxTp_connectionId, gxTp_network, gxTp_pathString, out externalParm1);
         returngetxpub.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returngetxpub ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult getreceiveaddress( Guid gxTp_connectionId ,
                                                                                  string gxTp_network ,
                                                                                  string gxTp_pathString ,
                                                                                  string gxTp_variant ,
                                                                                  out GeneXus.Programs.gxjadelib.SdtGxAddressResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returngetreceiveaddress;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxAddressResult(context);
         returngetreceiveaddress = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxAddressResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.GetReceiveAddress(gxTp_connectionId, gxTp_network, gxTp_pathString, gxTp_variant, out externalParm1);
         returngetreceiveaddress.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returngetreceiveaddress ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult updatepinserver( Guid gxTp_connectionId ,
                                                                                string gxTp_urlA ,
                                                                                string gxTp_urlB ,
                                                                                string gxTp_pubkeyHex )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnupdatepinserver;
         returnupdatepinserver = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.UpdatePinServer(gxTp_connectionId, gxTp_urlA, gxTp_urlB, gxTp_pubkeyHex);
         returnupdatepinserver.ExternalInstance = externalParm0;
         return returnupdatepinserver ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult resetpinserver( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnresetpinserver;
         returnresetpinserver = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.ResetPinServer(gxTp_connectionId);
         returnresetpinserver.ExternalInstance = externalParm0;
         return returnresetpinserver ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmgetinfo( Guid gxTp_connectionId ,
                                                                           out object gxTp_info )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmgetinfo;
         returnhsmgetinfo = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxHsmInfo externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmGetInfo(gxTp_connectionId, out externalParm1);
         returnhsmgetinfo.ExternalInstance = externalParm0;
         gxTp_info = (object)(externalParm1);
         return returnhsmgetinfo ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmgetpubkey( Guid gxTp_connectionId ,
                                                                             string gxTp_network ,
                                                                             long gxTp_index ,
                                                                             out GeneXus.Programs.gxjadelib.SdtGxHsmPubkeyResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmgetpubkey;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxHsmPubkeyResult(context);
         returnhsmgetpubkey = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxHsmPubkeyResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmGetPubkey(gxTp_connectionId, gxTp_network, (System.UInt32)(gxTp_index), out externalParm1);
         returnhsmgetpubkey.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returnhsmgetpubkey ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmgetxpub( Guid gxTp_connectionId ,
                                                                           string gxTp_network ,
                                                                           out GeneXus.Programs.gxjadelib.SdtGxHsmXpubResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmgetxpub;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxHsmXpubResult(context);
         returnhsmgetxpub = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxHsmXpubResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmGetXpub(gxTp_connectionId, gxTp_network, out externalParm1);
         returnhsmgetxpub.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returnhsmgetxpub ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmsign( Guid gxTp_connectionId ,
                                                                        string gxTp_network ,
                                                                        long gxTp_index ,
                                                                        string gxTp_hashHex ,
                                                                        string gxTp_algorithm ,
                                                                        out GeneXus.Programs.gxjadelib.SdtGxHsmSignResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmsign;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxHsmSignResult(context);
         returnhsmsign = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxHsmSignResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmSign(gxTp_connectionId, gxTp_network, (System.UInt32)(gxTp_index), gxTp_hashHex, gxTp_algorithm, out externalParm1);
         returnhsmsign.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returnhsmsign ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmecdh( Guid gxTp_connectionId ,
                                                                        string gxTp_network ,
                                                                        long gxTp_index ,
                                                                        string gxTp_theirPubkeyHex ,
                                                                        out string gxTp_secretHex )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmecdh;
         gxTp_secretHex = "";
         returnhsmecdh = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmEcdh(gxTp_connectionId, gxTp_network, (System.UInt32)(gxTp_index), gxTp_theirPubkeyHex, out gxTp_secretHex);
         returnhsmecdh.ExternalInstance = externalParm0;
         return returnhsmecdh ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmencrypt( Guid gxTp_connectionId ,
                                                                           string gxTp_network ,
                                                                           long gxTp_index ,
                                                                           string gxTp_plaintextHex ,
                                                                           string gxTp_theirPubkeyHex ,
                                                                           string gxTp_aadHex ,
                                                                           out GeneXus.Programs.gxjadelib.SdtGxHsmEncryptResult gxTp_result )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmencrypt;
         gxTp_result = new GeneXus.Programs.gxjadelib.SdtGxHsmEncryptResult(context);
         returnhsmencrypt = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         GxJadeLib.Models.GxHsmEncryptResult externalParm1;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmEncrypt(gxTp_connectionId, gxTp_network, (System.UInt32)(gxTp_index), gxTp_plaintextHex, gxTp_theirPubkeyHex, gxTp_aadHex, out externalParm1);
         returnhsmencrypt.ExternalInstance = externalParm0;
         gxTp_result.ExternalInstance = externalParm1;
         return returnhsmencrypt ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmdecrypt( Guid gxTp_connectionId ,
                                                                           string gxTp_network ,
                                                                           long gxTp_index ,
                                                                           string gxTp_ciphertextHex ,
                                                                           string gxTp_nonceHex ,
                                                                           string gxTp_tagHex ,
                                                                           string gxTp_ephemeralPubkeyHex ,
                                                                           string gxTp_aadHex ,
                                                                           out string gxTp_plaintextHex )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmdecrypt;
         gxTp_plaintextHex = "";
         returnhsmdecrypt = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmDecrypt(gxTp_connectionId, gxTp_network, (System.UInt32)(gxTp_index), gxTp_ciphertextHex, gxTp_nonceHex, gxTp_tagHex, gxTp_ephemeralPubkeyHex, gxTp_aadHex, out gxTp_plaintextHex);
         returnhsmdecrypt.ExternalInstance = externalParm0;
         return returnhsmdecrypt ;
      }

      public GeneXus.Programs.gxjadelib.SdtJadeOperationResult hsmlock( Guid gxTp_connectionId )
      {
         GeneXus.Programs.gxjadelib.SdtJadeOperationResult returnhsmlock;
         returnhsmlock = new GeneXus.Programs.gxjadelib.SdtJadeOperationResult(context);
         GxJadeLib.Models.JadeOperationResult externalParm0;
         externalParm0 = GxJadeLib.GxJadeWrapper.HsmLock(gxTp_connectionId);
         returnhsmlock.ExternalInstance = externalParm0;
         return returnhsmlock ;
      }

      public int getactiveconnectioncount( )
      {
         int returngetactiveconnectioncount;
         returngetactiveconnectioncount = 0;
         returngetactiveconnectioncount = (int)(GxJadeLib.GxJadeWrapper.GetActiveConnectionCount());
         return returngetactiveconnectioncount ;
      }

      public void disconnectall( )
      {
         GxJadeLib.GxJadeWrapper.DisconnectAll() ;
         return  ;
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
