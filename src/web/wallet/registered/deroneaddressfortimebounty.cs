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
namespace GeneXus.Programs.wallet.registered {
   public class deroneaddressfortimebounty : GXProcedure
   {
      public deroneaddressfortimebounty( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deroneaddressfortimebounty( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_items ,
                           string aP1_ownerPublicKey ,
                           string aP2_networkType ,
                           DateTime aP3_bountyRestoreDate ,
                           string aP4_secret ,
                           out string aP5_one_address ,
                           out string aP6_error )
      {
         this.AV20items = aP0_items;
         this.AV28ownerPublicKey = aP1_ownerPublicKey;
         this.AV21networkType = aP2_networkType;
         this.AV10bountyRestoreDate = aP3_bountyRestoreDate;
         this.AV32secret = aP4_secret;
         this.AV25one_address = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP5_one_address=this.AV25one_address;
         aP6_error=this.AV12error;
      }

      public string executeUdp( GxSimpleCollection<string> aP0_items ,
                                string aP1_ownerPublicKey ,
                                string aP2_networkType ,
                                DateTime aP3_bountyRestoreDate ,
                                string aP4_secret ,
                                out string aP5_one_address )
      {
         execute(aP0_items, aP1_ownerPublicKey, aP2_networkType, aP3_bountyRestoreDate, aP4_secret, out aP5_one_address, out aP6_error);
         return AV12error ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_items ,
                                 string aP1_ownerPublicKey ,
                                 string aP2_networkType ,
                                 DateTime aP3_bountyRestoreDate ,
                                 string aP4_secret ,
                                 out string aP5_one_address ,
                                 out string aP6_error )
      {
         this.AV20items = aP0_items;
         this.AV28ownerPublicKey = aP1_ownerPublicKey;
         this.AV21networkType = aP2_networkType;
         this.AV10bountyRestoreDate = aP3_bountyRestoreDate;
         this.AV32secret = aP4_secret;
         this.AV25one_address = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP5_one_address=this.AV25one_address;
         aP6_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_int1 = AV9bountyResotreUnixTime;
         GXt_dtime2 = DateTimeUtil.ResetTime( AV10bountyRestoreDate ) ;
         new GeneXus.Programs.distributedcrypto.datetimetounixtime(context ).execute(  GXt_dtime2, out  GXt_int1) ;
         AV9bountyResotreUnixTime = GXt_int1;
         /* User Code */
          NBitcoin.Network network;
         if ( StringUtil.StrCmp(AV21networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV21networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV21networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV12error = "Network Type not sopported";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            AV20items.Sort("");
            AV23numOfScripts = (short)(AV20items.Count);
            /* User Code */
             var howManyScripts = AV23numOfScripts;
            /* User Code */
             var Scripts = new NBitcoin.TapScript[howManyScripts];
            /* User Code */
             string pubKeyString;
            /* User Code */
             int i;
            /* User Code */
             var secret = Convert.FromBase64String(AV32secret);
            /* User Code */
             var secret_sha256 = NBitcoin.Crypto.Hashes.DoubleSHA256(secret);
            /* User Code */
             NBitcoin.LockTime target = (int) AV9bountyResotreUnixTime;
            /* User Code */
             System.Collections.Generic.List<NBitcoin.Op> ops = new System.Collections.Generic.List<NBitcoin.Op>();
            /* User Code */
             var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
            /* User Code */
             var probability = (uint)(100 / howManyScripts);
            AV19i = 0;
            AV33GXV1 = 1;
            while ( AV33GXV1 <= AV20items.Count )
            {
               AV27oneItem = AV20items.GetString(AV33GXV1);
               /* User Code */
                ops.Clear();
               /* User Code */
                pubKeyString = AV27oneItem;
               /* User Code */
                var xonlypubk = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(pubKeyString)).ToXOnlyPubKey();
               /* User Code */
                ops.Add(NBitcoin.Op.GetPushOp(target.Value));
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_CHECKLOCKTIMEVERIFY);
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_DROP);
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_HASH256);
               /* User Code */
                ops.Add(NBitcoin.Op.GetPushOp(secret_sha256.ToBytes()));
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_EQUALVERIFY);
               /* User Code */
                ops.Add(NBitcoin.Op.GetPushOp(xonlypubk.ToBytes()));
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_CHECKSIG);
               /* User Code */
                i = AV19i;
               /* User Code */
                Scripts[i] = new NBitcoin.Script(ops).ToTapScript(NBitcoin.TapLeafVersion.C0);
               /* User Code */
                scriptWeightsList.Add((probability, Scripts[i]));
               AV19i = (short)(AV19i+1);
               AV33GXV1 = (int)(AV33GXV1+1);
            }
            /* User Code */
             var scriptWeights = scriptWeightsList.ToArray();
            /* User Code */
             var ownerKeyString = AV28ownerPublicKey;
            /* User Code */
             var ec_PubKey = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(ownerKeyString));
            /* User Code */
             var xOnlyFromPubkey = ec_PubKey.ToXOnlyPubKey();
            /* User Code */
             var tapIntFromEC = new NBitcoin.TaprootInternalPubKey(xOnlyFromPubkey.ToBytes());
            /* User Code */
             var treeInfo = NBitcoin.TaprootSpendInfo.WithHuffmanTree(tapIntFromEC, scriptWeights);
            /* User Code */
             var taprootPubKey = treeInfo.OutputPubKey.OutputKey;
            /* User Code */
             var addr = taprootPubKey.GetAddress(network);
            /* User Code */
             var final_address = addr.ToString();
            /* User Code */
             AV25one_address = final_address;
            /* User Code */
            	}
            /* User Code */
            	catch (Exception ex)
            /* User Code */
            	{
            /* User Code */
            		AV12error = ex.Message.ToString();
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
         AV25one_address = "";
         AV12error = "";
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         AV27oneItem = "";
         /* GeneXus formulas. */
      }

      private short AV23numOfScripts ;
      private short AV19i ;
      private int AV33GXV1 ;
      private long AV9bountyResotreUnixTime ;
      private long GXt_int1 ;
      private string AV28ownerPublicKey ;
      private string AV21networkType ;
      private string AV32secret ;
      private string AV25one_address ;
      private string AV12error ;
      private string AV27oneItem ;
      private DateTime GXt_dtime2 ;
      private DateTime AV10bountyRestoreDate ;
      private GxSimpleCollection<string> AV20items ;
      private string aP5_one_address ;
      private string aP6_error ;
   }

}
