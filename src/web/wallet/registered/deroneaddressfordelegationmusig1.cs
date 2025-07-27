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
   public class deroneaddressfordelegationmusig1 : GXProcedure
   {
      public deroneaddressfordelegationmusig1( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deroneaddressfordelegationmusig1( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP0_finalCombinations ,
                           string aP1_ownerPublicKey ,
                           string aP2_networkType ,
                           out string aP3_one_address ,
                           out string aP4_error )
      {
         this.AV13finalCombinations = aP0_finalCombinations;
         this.AV26ownerPublicKey = aP1_ownerPublicKey;
         this.AV19networkType = aP2_networkType;
         this.AV23one_address = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP3_one_address=this.AV23one_address;
         aP4_error=this.AV10error;
      }

      public string executeUdp( GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP0_finalCombinations ,
                                string aP1_ownerPublicKey ,
                                string aP2_networkType ,
                                out string aP3_one_address )
      {
         execute(aP0_finalCombinations, aP1_ownerPublicKey, aP2_networkType, out aP3_one_address, out aP4_error);
         return AV10error ;
      }

      public void executeSubmit( GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP0_finalCombinations ,
                                 string aP1_ownerPublicKey ,
                                 string aP2_networkType ,
                                 out string aP3_one_address ,
                                 out string aP4_error )
      {
         this.AV13finalCombinations = aP0_finalCombinations;
         this.AV26ownerPublicKey = aP1_ownerPublicKey;
         this.AV19networkType = aP2_networkType;
         this.AV23one_address = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP3_one_address=this.AV23one_address;
         aP4_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
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
            AV21numOfScripts = (short)(AV13finalCombinations.Count);
            AV20numMinSignatures = (short)(((GeneXus.Programs.math.SdtfinalCombination)AV13finalCombinations.Item(1)).gxTpr_Items.Count);
            /* User Code */
             var peers = AV20numMinSignatures;
            /* User Code */
             var howManyScripts = AV21numOfScripts;
            /* User Code */
             var Scripts = new NBitcoin.TapScript[howManyScripts];
            /* User Code */
             string pubKeyString;
            /* User Code */
             int i;
            /* User Code */
             System.Collections.Generic.List<NBitcoin.Op> ops = new System.Collections.Generic.List<NBitcoin.Op>();
            /* User Code */
             var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
            /* User Code */
             var probability = (uint)(100 / howManyScripts);
            AV17i = 0;
            AV30GXV1 = 1;
            while ( AV30GXV1 <= AV13finalCombinations.Count )
            {
               AV14finalOneCombination = ((GeneXus.Programs.math.SdtfinalCombination)AV13finalCombinations.Item(AV30GXV1));
               AV27p = 0;
               /* User Code */
                ops.Clear();
               AV31GXV2 = 1;
               while ( AV31GXV2 <= AV14finalOneCombination.gxTpr_Items.Count )
               {
                  AV25oneItem = AV14finalOneCombination.gxTpr_Items.GetString(AV31GXV2);
                  /* User Code */
                   pubKeyString = AV25oneItem;
                  /* User Code */
                   var xonlypubk = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(pubKeyString)).ToXOnlyPubKey();
                  /* User Code */
                   ops.Add(NBitcoin.Op.GetPushOp(xonlypubk.ToBytes()));
                  if ( AV27p == 0 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_CHECKSIG);
                  }
                  else
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_CHECKSIGADD);
                  }
                  AV27p = (short)(AV27p+1);
                  AV31GXV2 = (int)(AV31GXV2+1);
               }
               if ( AV20numMinSignatures == 2 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_2);
               }
               else if ( AV20numMinSignatures == 3 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_3);
               }
               else if ( AV20numMinSignatures == 4 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_4);
               }
               else if ( AV20numMinSignatures == 5 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_5);
               }
               else if ( AV20numMinSignatures == 6 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_6);
               }
               else if ( AV20numMinSignatures == 7 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_7);
               }
               else if ( AV20numMinSignatures == 8 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_8);
               }
               else if ( AV20numMinSignatures == 9 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_9);
               }
               else if ( AV20numMinSignatures == 10 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_10);
               }
               else if ( AV20numMinSignatures == 11 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_11);
               }
               else if ( AV20numMinSignatures == 12 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_12);
               }
               else if ( AV20numMinSignatures == 13 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_13);
               }
               else if ( AV20numMinSignatures == 14 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_14);
               }
               else if ( AV20numMinSignatures == 15 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_15);
               }
               else if ( AV20numMinSignatures == 16 )
               {
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_16);
               }
               else
               {
                  AV10error = "The following number of signatures is not supported: " + StringUtil.Str( (decimal)(AV20numMinSignatures), 4, 0);
                  cleanup();
                  if (true) return;
               }
               /* User Code */
                ops.Add(NBitcoin.OpcodeType.OP_NUMEQUAL);
               /* User Code */
                i = AV17i;
               /* User Code */
                Scripts[i] = new NBitcoin.Script(ops).ToTapScript(NBitcoin.TapLeafVersion.C0);
               /* User Code */
                scriptWeightsList.Add((probability, Scripts[i]));
               AV17i = (short)(AV17i+1);
               AV30GXV1 = (int)(AV30GXV1+1);
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
         AV14finalOneCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV25oneItem = "";
         /* GeneXus formulas. */
      }

      private short AV21numOfScripts ;
      private short AV20numMinSignatures ;
      private short AV17i ;
      private short AV27p ;
      private int AV30GXV1 ;
      private int AV31GXV2 ;
      private string AV26ownerPublicKey ;
      private string AV19networkType ;
      private string AV23one_address ;
      private string AV10error ;
      private string AV25oneItem ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> AV13finalCombinations ;
      private GeneXus.Programs.math.SdtfinalCombination AV14finalOneCombination ;
      private string aP3_one_address ;
      private string aP4_error ;
   }

}
