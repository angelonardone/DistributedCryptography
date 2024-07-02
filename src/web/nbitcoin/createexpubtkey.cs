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
   public class createexpubtkey : GXProcedure
   {
      public createexpubtkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createexpubtkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_ExtendedPublicKey ,
                           string aP1_networkType ,
                           string aP2_keyPath ,
                           out GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo aP3_keyInfo ,
                           out string aP4_error )
      {
         this.AV9ExtendedPublicKey = aP0_ExtendedPublicKey;
         this.AV12networkType = aP1_networkType;
         this.AV11keyPath = aP2_keyPath;
         this.AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context) ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_keyInfo=this.AV10keyInfo;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_ExtendedPublicKey ,
                                string aP1_networkType ,
                                string aP2_keyPath ,
                                out GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo aP3_keyInfo )
      {
         execute(aP0_ExtendedPublicKey, aP1_networkType, aP2_keyPath, out aP3_keyInfo, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_ExtendedPublicKey ,
                                 string aP1_networkType ,
                                 string aP2_keyPath ,
                                 out GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo aP3_keyInfo ,
                                 out string aP4_error )
      {
         this.AV9ExtendedPublicKey = aP0_ExtendedPublicKey;
         this.AV12networkType = aP1_networkType;
         this.AV11keyPath = aP2_keyPath;
         this.AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context) ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_keyInfo=this.AV10keyInfo;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10keyInfo.gxTpr_Keypath = AV11keyPath;
         /* User Code */
          NBitcoin.Network network;
         if ( StringUtil.StrCmp(AV12networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV12networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV12networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV8error = "Network Type not sopported";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
            	NBitcoin.ExtPubKey masterPubKey;
            /* User Code */
            	NBitcoin.KeyPath keyPath;
            /* User Code */
             	string val;
            /* User Code */
            	byte[] data;
            /* User Code */
            	byte[] tempbytes;
            /* User Code */
            	byte[] version;
            /* User Code */
             string tempExtPubKey = AV9ExtendedPublicKey;
            /* User Code */
             masterPubKey = NBitcoin.ExtPubKey.Parse(tempExtPubKey, network);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11keyPath)) )
            {
               /* User Code */
                keyPath = new NBitcoin.KeyPath(AV11keyPath);
               /* User Code */
                masterPubKey = masterPubKey.Derive(keyPath);
            }
            /* User Code */
             val = masterPubKey.ToString(network);
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Publickey = AV13val;
            AV10keyInfo.gxTpr_Publickeytaproot = AV13val;
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
             	AV13val = val;
            AV10keyInfo.gxTpr_Publickeysegwitp2sh = AV13val;
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
             	AV13val = val;
            AV10keyInfo.gxTpr_Publickeysegwit = AV13val;
            /* User Code */
            	val = masterPubKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Legacy, network).ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Addresslegacy = AV13val;
            /* User Code */
            	val = masterPubKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.SegwitP2SH, network).ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Addresssegwitp2sh = AV13val;
            /* User Code */
            	val = masterPubKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Segwit, network).ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Addresssegwit = AV13val;
            /* User Code */
            	val = masterPubKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.TaprootBIP86, network).ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Addresstaproot = AV13val;
            /* User Code */
            	val = masterPubKey.PubKey.ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Ec_publickey = AV13val;
            /* User Code */
             val = masterPubKey.Depth.ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Depth = (short)(Math.Round(NumberUtil.Val( AV13val, "."), 18, MidpointRounding.ToEven));
            /* User Code */
             val = masterPubKey.Child.ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Child = (long)(Math.Round(NumberUtil.Val( AV13val, "."), 18, MidpointRounding.ToEven));
            /* User Code */
             val = masterPubKey.IsHardened.ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Ishardended = ((StringUtil.StrCmp(StringUtil.Lower( AV13val), "true")==0) ? true : false);
            /* User Code */
             val = masterPubKey.PubKey.GetHDFingerPrint().ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Fingerprint = AV13val;
            /* User Code */
             val = masterPubKey.ParentFingerprint.ToString();
            /* User Code */
             	AV13val = val;
            AV10keyInfo.gxTpr_Parentfingerprint = AV13val;
            /* User Code */
            	}
            /* User Code */
                catch (Exception ex)
            /* User Code */
                {
            /* User Code */
                 AV8error = ex.Message.ToString();
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
         AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV8error = "";
         AV13val = "";
         /* GeneXus formulas. */
      }

      private string AV9ExtendedPublicKey ;
      private string AV12networkType ;
      private string AV11keyPath ;
      private string AV8error ;
      private string AV13val ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo aP3_keyInfo ;
      private string aP4_error ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV10keyInfo ;
   }

}
