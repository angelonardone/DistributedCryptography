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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.nbitcoin {
   public class createkey : GXProcedure
   {
      public createkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nbitcoin.SdtKeyCreate aP0_keyCreate ,
                           string aP1_password ,
                           out GeneXus.Programs.nbitcoin.SdtKeyInfo aP2_keyInfo ,
                           out string aP3_error )
      {
         this.AV10keyCreate = aP0_keyCreate;
         this.AV12password = aP1_password;
         this.AV11keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_keyInfo=this.AV11keyInfo;
         aP3_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.nbitcoin.SdtKeyCreate aP0_keyCreate ,
                                string aP1_password ,
                                out GeneXus.Programs.nbitcoin.SdtKeyInfo aP2_keyInfo )
      {
         execute(aP0_keyCreate, aP1_password, out aP2_keyInfo, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.nbitcoin.SdtKeyCreate aP0_keyCreate ,
                                 string aP1_password ,
                                 out GeneXus.Programs.nbitcoin.SdtKeyInfo aP2_keyInfo ,
                                 out string aP3_error )
      {
         this.AV10keyCreate = aP0_keyCreate;
         this.AV12password = aP1_password;
         this.AV11keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_keyInfo=this.AV11keyInfo;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network;
         /* User Code */
          network = NBitcoin.Network.Main;
         if ( StringUtil.StrCmp(AV10keyCreate.gxTpr_Networktype, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV10keyCreate.gxTpr_Networktype, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV10keyCreate.gxTpr_Networktype, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV9error = "Network Type not sopported";
         }
         /* User Code */
          NBitcoin.ScriptPubKeyType addressType;
         /* User Code */
          addressType = NBitcoin.ScriptPubKeyType.Legacy;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            if ( AV10keyCreate.gxTpr_Addresstype == 0 )
            {
               /* User Code */
                addressType = NBitcoin.ScriptPubKeyType.Legacy;
            }
            else if ( AV10keyCreate.gxTpr_Addresstype == 1 )
            {
               /* User Code */
                addressType = NBitcoin.ScriptPubKeyType.Segwit;
            }
            else if ( AV10keyCreate.gxTpr_Addresstype == 2 )
            {
               /* User Code */
                addressType = NBitcoin.ScriptPubKeyType.SegwitP2SH;
            }
            else if ( AV10keyCreate.gxTpr_Addresstype == 3 )
            {
               /* User Code */
                addressType = NBitcoin.ScriptPubKeyType.TaprootBIP86;
            }
            else
            {
               AV9error = "Address Type not supported";
            }
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
             string password = AV12password;
            if ( AV10keyCreate.gxTpr_Createkeytype == 10 )
            {
               /* User Code */
               	privateKey = new NBitcoin.Key();
            }
            else if ( AV10keyCreate.gxTpr_Createkeytype == 20 )
            {
               AV8createText = AV10keyCreate.gxTpr_Createtext;
               /* User Code */
                string brain_text = AV8createText;
               /* User Code */
                byte[] bytes = NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(brain_text);
               /* User Code */
                var HashedText =  NBitcoin.Crypto.Hashes.SHA256(bytes);
               /* User Code */
               	privateKey = new NBitcoin.Key(HashedText);
            }
            else if ( AV10keyCreate.gxTpr_Createkeytype == 30 )
            {
               AV8createText = AV10keyCreate.gxTpr_Createtext;
               /* User Code */
                string hexPrivateKey = AV8createText;
               /* User Code */
                byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
               /* User Code */
               	privateKey = new NBitcoin.Key(bytes);
            }
            else if ( AV10keyCreate.gxTpr_Createkeytype == 100 )
            {
               AV8createText = AV10keyCreate.gxTpr_Createtext;
               /* User Code */
                string wif = AV8createText;
               /* User Code */
                privateKey = new NBitcoin.BitcoinSecret(wif, network).PrivateKey;
            }
            else if ( AV10keyCreate.gxTpr_Createkeytype == 110 )
            {
               AV8createText = AV10keyCreate.gxTpr_Createtext;
               /* User Code */
                string encWifString = AV8createText;
               /* User Code */
                privateKey = NBitcoin.Key.Parse(encWifString, password, network);
            }
            else
            {
               /* User Code */
               	privateKey = new NBitcoin.Key();
            }
            /* User Code */
             	NBitcoin.PubKey publicKey = privateKey.PubKey;
            /* User Code */
             	string val = publicKey.ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Publickey = AV13val;
            /* User Code */
             val = privateKey.PubKey.GetTaprootFullPubKey().ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Taprootpubkey = AV13val;
            /* User Code */
             	val = privateKey.ToHex().ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Privatekey = AV13val;
            /* User Code */
              var netPrivateKey = privateKey.GetBitcoinSecret(network);
            /* User Code */
              val = netPrivateKey.ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Wif = AV13val;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12password)) )
            {
               /* User Code */
                NBitcoin.BitcoinEncryptedSecret encryptedNetPrivateKey = netPrivateKey.Encrypt(password);
               /* User Code */
                	AV13val = encryptedNetPrivateKey.ToString();
               AV11keyInfo.gxTpr_Encryptedwif = AV13val;
            }
            /* User Code */
             var publicKeyHash = publicKey.Hash;
            /* User Code */
             	val = publicKeyHash.ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Publickeyhash = AV13val;
            /* User Code */
             var netAddress = publicKey.GetAddress(addressType,network);
            /* User Code */
             	val = netAddress.ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Address = AV13val;
            /* User Code */
             	val = netAddress.ScriptPubKey.ToString();
            /* User Code */
             	AV13val = val;
            AV11keyInfo.gxTpr_Scriptpublickey = AV13val;
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
         cleanup();
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
         AV11keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV9error = "";
         AV8createText = "";
         AV13val = "";
         /* GeneXus formulas. */
      }

      private string AV12password ;
      private string AV9error ;
      private string AV13val ;
      private string AV8createText ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV10keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV11keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo aP2_keyInfo ;
      private string aP3_error ;
   }

}
