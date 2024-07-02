using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.nbitcoin {
   public class createextkey : GXProcedure
   {
      public createextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createextkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nbitcoin.SdtExtKeyCreate aP0_extKeyCreate ,
                           string aP1_password ,
                           out GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP2_keyInfo ,
                           out string aP3_error )
      {
         this.AV11extKeyCreate = aP0_extKeyCreate;
         this.AV16password = aP1_password;
         this.AV12keyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context) ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_keyInfo=this.AV12keyInfo;
         aP3_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.nbitcoin.SdtExtKeyCreate aP0_extKeyCreate ,
                                string aP1_password ,
                                out GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP2_keyInfo )
      {
         execute(aP0_extKeyCreate, aP1_password, out aP2_keyInfo, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.nbitcoin.SdtExtKeyCreate aP0_extKeyCreate ,
                                 string aP1_password ,
                                 out GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP2_keyInfo ,
                                 out string aP3_error )
      {
         this.AV11extKeyCreate = aP0_extKeyCreate;
         this.AV16password = aP1_password;
         this.AV12keyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context) ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_keyInfo=this.AV12keyInfo;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13keyPath = StringUtil.Trim( AV11extKeyCreate.gxTpr_Keypath);
         AV12keyInfo.gxTpr_Extended.gxTpr_Keypath = AV13keyPath;
         AV15networkType = AV11extKeyCreate.gxTpr_Networktype;
         /* User Code */
          NBitcoin.Network network;
         if ( StringUtil.StrCmp(AV15networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV15networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV15networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV9error = "Network Type not sopported";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
            	NBitcoin.Key privateKey;
            /* User Code */
            	NBitcoin.ExtKey privateExtKey;
            /* User Code */
            	NBitcoin.ExtPubKey masterPubKey;
            /* User Code */
            	NBitcoin.Mnemonic mnemo;
            /* User Code */
            	NBitcoin.KeyPath keyPath;
            /* User Code */
              byte[] chainCode;
            /* User Code */
              byte[] seed;
            /* User Code */
             	string val;
            /* User Code */
            	string password = AV16password;
            /* User Code */
            	byte[] data;
            /* User Code */
            	byte[] tempbytes;
            /* User Code */
            	byte[] version;
            if ( AV11extKeyCreate.gxTpr_Createextkeytype == 10 )
            {
               /* User Code */
                privateExtKey = new NBitcoin.ExtKey();
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 50 )
            {
               AV17seed = AV11extKeyCreate.gxTpr_Seed;
               /* User Code */
                string tempSeed = AV17seed;
               /* User Code */
                seed = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(tempSeed);
               /* User Code */
                privateExtKey = NBitcoin.ExtKey.CreateFromSeed(seed);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 20 )
            {
               AV19WordCount = AV11extKeyCreate.gxTpr_Mnemonicnumberwords;
               if ( AV11extKeyCreate.gxTpr_Mnemoniclanguage == 20 )
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(NBitcoin.Wordlist.Spanish, (NBitcoin.WordCount)AV19WordCount);
               }
               else if ( AV11extKeyCreate.gxTpr_Mnemoniclanguage == 10 )
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(NBitcoin.Wordlist.English, (NBitcoin.WordCount)AV19WordCount);
               }
               else
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(NBitcoin.Wordlist.English, (NBitcoin.WordCount)AV19WordCount);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16password)) )
               {
                  /* User Code */
                   privateExtKey = mnemo.DeriveExtKey();
               }
               else
               {
                  /* User Code */
                   privateExtKey = mnemo.DeriveExtKey(password);
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
               /* User Code */
                AV14Mnemonic = mnemo.ToString();
               AV12keyInfo.gxTpr_Mnemonic = AV14Mnemonic;
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 40 )
            {
               AV8createText = AV11extKeyCreate.gxTpr_Privatekey;
               /* User Code */
                string privateKeyStr = AV8createText;
               /* User Code */
                byte[] pKbytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(privateKeyStr);
               /* User Code */
                privateKey = new NBitcoin.Key(pKbytes);
               AV8createText = AV11extKeyCreate.gxTpr_Chaincode;
               /* User Code */
                string chainCodeStr = AV8createText;
               /* User Code */
                chainCode = NBitcoin.DataEncoders.Encoders.Base64.DecodeData(chainCodeStr);
               /* User Code */
                privateExtKey = new NBitcoin.ExtKey(privateKey, chainCode);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 80 )
            {
               AV8createText = AV11extKeyCreate.gxTpr_Encryptedwif;
               /* User Code */
                string encWifString = AV8createText;
               /* User Code */
                privateKey = NBitcoin.Key.Parse(encWifString, password, network);
               AV8createText = AV11extKeyCreate.gxTpr_Chaincode;
               /* User Code */
                string chainCodeStr = AV8createText;
               /* User Code */
                chainCode = NBitcoin.DataEncoders.Encoders.Base64.DecodeData(chainCodeStr);
               /* User Code */
                privateExtKey = new NBitcoin.ExtKey(privateKey, chainCode);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 30 )
            {
               AV14Mnemonic = AV11extKeyCreate.gxTpr_Createtext;
               /* User Code */
                val = AV14Mnemonic;
               if ( AV11extKeyCreate.gxTpr_Mnemoniclanguage == 20 )
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(val,NBitcoin.Wordlist.Spanish);
               }
               else if ( AV11extKeyCreate.gxTpr_Mnemoniclanguage == 10 )
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(val,NBitcoin.Wordlist.English);
               }
               else
               {
                  /* User Code */
                   mnemo = new NBitcoin.Mnemonic(val,NBitcoin.Wordlist.English);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16password)) )
               {
                  /* User Code */
                   privateExtKey = mnemo.DeriveExtKey();
               }
               else
               {
                  /* User Code */
                   privateExtKey = mnemo.DeriveExtKey(password);
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
            }
            else if ( AV11extKeyCreate.gxTpr_Createextkeytype == 70 )
            {
               AV10ExtendedPrivateKey = AV11extKeyCreate.gxTpr_Extendedprivatekey;
               /* User Code */
                string tempExtPrivKey = AV10ExtendedPrivateKey;
               /* User Code */
                privateExtKey = NBitcoin.ExtKey.Parse(tempExtPrivKey, network);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13keyPath)) )
               {
                  /* User Code */
                   keyPath = new NBitcoin.KeyPath(AV13keyPath);
                  /* User Code */
                   privateExtKey = privateExtKey.Derive(keyPath);
               }
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
            }
            else
            {
               /* User Code */
                privateExtKey = new NBitcoin.ExtKey();
               /* User Code */
                privateKey = privateExtKey.PrivateKey;
               /* User Code */
                chainCode = privateExtKey.ChainCode;
            }
            /* User Code */
             	val = Convert.ToBase64String(chainCode);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Chaincode = AV18val;
            /* User Code */
             	val = privateKey.ToHex().ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Privatekey = AV18val;
            /* User Code */
             	NBitcoin.PubKey publicKey = privateKey.PubKey;
            /* User Code */
             	val = publicKey.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Publickey = AV18val;
            /* User Code */
              var netPrivateKey = privateKey.GetBitcoinSecret(network);
            /* User Code */
              val = netPrivateKey.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Wif = AV18val;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16password)) )
            {
               /* User Code */
                NBitcoin.BitcoinEncryptedSecret encryptedNetPrivateKey = netPrivateKey.Encrypt(password);
               /* User Code */
                	AV18val = encryptedNetPrivateKey.ToString();
               AV12keyInfo.gxTpr_Encryptedwif = AV18val;
            }
            /* User Code */
              val = privateExtKey.GetWif(network).ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Privatekey = AV18val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot = AV18val;
            /* User Code */
            	data = privateExtKey.ToBytes();
            /* User Code */
            	version = (network == NBitcoin.Network.Main)
            /* User Code */
            		? new byte[] { (0x04), (0x9D), (0x78), (0x78) }
            /* User Code */
            		: new byte[] { (0x04), (0x4A), (0x4E), (0x28) };
            /* User Code */
             tempbytes = new byte[version.Length + data.Length];
            /* User Code */
             System.Buffer.BlockCopy(version, 0, tempbytes, 0, version.Length);
            /* User Code */
             System.Buffer.BlockCopy(data, 0, tempbytes, version.Length, data.Length);
            /* User Code */
            	val =  NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(tempbytes);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Privatekeysegwitp2sh = AV18val;
            /* User Code */
            	data = privateExtKey.ToBytes();
            /* User Code */
            	version = (network == NBitcoin.Network.Main)
            /* User Code */
            		? new byte[] { (0x04), (0xB2), (0x43), (0x0C) }
            /* User Code */
            		: new byte[] { (0x04), (0x5F), (0x18), (0xBC) };
            /* User Code */
             tempbytes = new byte[version.Length + data.Length];
            /* User Code */
             System.Buffer.BlockCopy(version, 0, tempbytes, 0, version.Length);
            /* User Code */
             System.Buffer.BlockCopy(data, 0, tempbytes, version.Length, data.Length);
            /* User Code */
            	val =  NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(tempbytes);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Privatekeysegwit = AV18val;
            /* User Code */
             masterPubKey = privateExtKey.Neuter();
            /* User Code */
             val = masterPubKey.ToString(network);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Nuterpublickey = AV18val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Nuterpublickeytaproot = AV18val;
            /* User Code */
            	data = masterPubKey.ToBytes();
            /* User Code */
            	version = (network == NBitcoin.Network.Main)
            /* User Code */
            		? new byte[] { (0x04), (0x9D), (0x7C), (0xB2) }
            /* User Code */
            		: new byte[] { (0x04), (0x4A), (0x52), (0x62) };
            /* User Code */
             tempbytes = new byte[version.Length + data.Length];
            /* User Code */
             System.Buffer.BlockCopy(version, 0, tempbytes, 0, version.Length);
            /* User Code */
             System.Buffer.BlockCopy(data, 0, tempbytes, version.Length, data.Length);
            /* User Code */
            	val =  NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(tempbytes);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh = AV18val;
            /* User Code */
            	data = masterPubKey.ToBytes();
            /* User Code */
            	version = (network == NBitcoin.Network.Main)
            /* User Code */
            		? new byte[] { (0x04), (0xB2), (0x47), (0x46) }
            /* User Code */
            		: new byte[] { (0x04), (0x5F), (0x1C), (0xF6) };
            /* User Code */
             tempbytes = new byte[version.Length + data.Length];
            /* User Code */
             System.Buffer.BlockCopy(version, 0, tempbytes, 0, version.Length);
            /* User Code */
             System.Buffer.BlockCopy(data, 0, tempbytes, version.Length, data.Length);
            /* User Code */
            	val =  NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(tempbytes);
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwit = AV18val;
            /* User Code */
             val = privateExtKey.Depth.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Depth = (short)(Math.Round(NumberUtil.Val( AV18val, "."), 18, MidpointRounding.ToEven));
            /* User Code */
             val = privateExtKey.Child.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Child = (long)(Math.Round(NumberUtil.Val( AV18val, "."), 18, MidpointRounding.ToEven));
            /* User Code */
             val = privateExtKey.IsHardened.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Ishardended = ((StringUtil.StrCmp(StringUtil.Lower( AV18val), "true")==0) ? true : false);
            /* User Code */
             val = privateExtKey.PrivateKey.PubKey.GetHDFingerPrint().ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Fingerprint = AV18val;
            /* User Code */
             val = privateExtKey.ParentFingerprint.ToString();
            /* User Code */
             	AV18val = val;
            AV12keyInfo.gxTpr_Extended.gxTpr_Parentfingerprint = AV18val;
            /* User Code */
            	}
            /* User Code */
                catch (Exception ex)
            /* User Code */
                {
            /* User Code */
                 AV9error = ex.Message.ToString();
            /* User Code */
                }
         }
         this.cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV12keyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV9error = "";
         AV13keyPath = "";
         AV15networkType = "";
         AV17seed = "";
         AV14Mnemonic = "";
         AV8createText = "";
         AV10ExtendedPrivateKey = "";
         AV18val = "";
         /* GeneXus formulas. */
      }

      private short AV19WordCount ;
      private string AV16password ;
      private string AV9error ;
      private string AV13keyPath ;
      private string AV15networkType ;
      private string AV17seed ;
      private string AV10ExtendedPrivateKey ;
      private string AV18val ;
      private string AV14Mnemonic ;
      private string AV8createText ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP2_keyInfo ;
      private string aP3_error ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV11extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12keyInfo ;
   }

}
