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
         this.AV18items = aP0_items;
         this.AV26ownerPublicKey = aP1_ownerPublicKey;
         this.AV19networkType = aP2_networkType;
         this.AV30bountyRestoreDate = aP3_bountyRestoreDate;
         this.AV32secret = aP4_secret;
         this.AV23one_address = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP5_one_address=this.AV23one_address;
         aP6_error=this.AV10error;
      }

      public string executeUdp( GxSimpleCollection<string> aP0_items ,
                                string aP1_ownerPublicKey ,
                                string aP2_networkType ,
                                DateTime aP3_bountyRestoreDate ,
                                string aP4_secret ,
                                out string aP5_one_address )
      {
         execute(aP0_items, aP1_ownerPublicKey, aP2_networkType, aP3_bountyRestoreDate, aP4_secret, out aP5_one_address, out aP6_error);
         return AV10error ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_items ,
                                 string aP1_ownerPublicKey ,
                                 string aP2_networkType ,
                                 DateTime aP3_bountyRestoreDate ,
                                 string aP4_secret ,
                                 out string aP5_one_address ,
                                 out string aP6_error )
      {
         this.AV18items = aP0_items;
         this.AV26ownerPublicKey = aP1_ownerPublicKey;
         this.AV19networkType = aP2_networkType;
         this.AV30bountyRestoreDate = aP3_bountyRestoreDate;
         this.AV32secret = aP4_secret;
         this.AV23one_address = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP5_one_address=this.AV23one_address;
         aP6_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_int1 = AV31bountyResotreUnixTime;
         GXt_dtime2 = DateTimeUtil.ResetTime( AV30bountyRestoreDate ) ;
         new GeneXus.Programs.distributedcrypto.datetimetounixtime(context ).execute(  GXt_dtime2, out  GXt_int1) ;
         AV31bountyResotreUnixTime = GXt_int1;
         /* User Code */
          NBitcoin.Network network;
         if ( StringUtil.StrCmp(AV19networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV19networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV19networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV10error = "Network Type not sopported";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            AV18items.Sort("");
            AV21numOfScripts = (short)(AV18items.Count);
            /* User Code */
             var howManyScripts = AV21numOfScripts;
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
             NBitcoin.LockTime target = (int) AV31bountyResotreUnixTime;
            /* User Code */
             System.Collections.Generic.List<NBitcoin.Op> ops = new System.Collections.Generic.List<NBitcoin.Op>();
            /* User Code */
             var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
            /* User Code */
             var probability = (uint)(100 / howManyScripts);
            AV17i = 0;
            AV33GXV1 = 1;
            while ( AV33GXV1 <= AV18items.Count )
            {
               AV25oneItem = AV18items.GetString(AV33GXV1);
               /* User Code */
                ops.Clear();
               /* User Code */
                pubKeyString = AV25oneItem;
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
                i = AV17i;
               /* User Code */
                Scripts[i] = new NBitcoin.Script(ops).ToTapScript(NBitcoin.TapLeafVersion.C0);
               /* User Code */
                scriptWeightsList.Add((probability, Scripts[i]));
               AV17i = (short)(AV17i+1);
               AV33GXV1 = (int)(AV33GXV1+1);
            }
            /* User Code */
             var scriptWeights = scriptWeightsList.ToArray();
            /* User Code */
             var ownerKeyString = AV26ownerPublicKey;
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
             AV23one_address = final_address;
            /* User Code */
            	}
            /* User Code */
            	catch (Exception ex)
            /* User Code */
            	{
            /* User Code */
            		AV10error = ex.Message.ToString();
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
         AV23one_address = "";
         AV10error = "";
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         AV25oneItem = "";
         /* GeneXus formulas. */
      }

      private short AV21numOfScripts ;
      private short AV17i ;
      private int AV33GXV1 ;
      private long AV31bountyResotreUnixTime ;
      private long GXt_int1 ;
      private string AV26ownerPublicKey ;
      private string AV19networkType ;
      private string AV32secret ;
      private string AV23one_address ;
      private string AV10error ;
      private string AV25oneItem ;
      private DateTime GXt_dtime2 ;
      private DateTime AV30bountyRestoreDate ;
      private GxSimpleCollection<string> AV18items ;
      private string aP5_one_address ;
      private string aP6_error ;
   }

}
