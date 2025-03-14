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
namespace GeneXus.Programs.wallet.registered {
   public class deriveoneaddressfordelegationmusig2 : GXProcedure
   {
      public deriveoneaddressfordelegationmusig2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveoneaddressfordelegationmusig2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           string aP1_networkType ,
                           short aP2_generatedType ,
                           long aP3_sequence ,
                           out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP4_sdt_oneAddress ,
                           out string aP5_error )
      {
         this.AV21group_sdt = aP0_group_sdt;
         this.AV17networkType = aP1_networkType;
         this.AV41generatedType = aP2_generatedType;
         this.AV40sequence = aP3_sequence;
         this.AV39sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP4_sdt_oneAddress=this.AV39sdt_oneAddress;
         aP5_error=this.AV13error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                string aP1_networkType ,
                                short aP2_generatedType ,
                                long aP3_sequence ,
                                out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP4_sdt_oneAddress )
      {
         execute(aP0_group_sdt, aP1_networkType, aP2_generatedType, aP3_sequence, out aP4_sdt_oneAddress, out aP5_error);
         return AV13error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 string aP1_networkType ,
                                 short aP2_generatedType ,
                                 long aP3_sequence ,
                                 out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP4_sdt_oneAddress ,
                                 out string aP5_error )
      {
         this.AV21group_sdt = aP0_group_sdt;
         this.AV17networkType = aP1_networkType;
         this.AV41generatedType = aP2_generatedType;
         this.AV40sequence = aP3_sequence;
         this.AV39sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         this.AV13error = "" ;
         SubmitImpl();
         aP4_sdt_oneAddress=this.AV39sdt_oneAddress;
         aP5_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV22numPeers = (short)(AV21group_sdt.gxTpr_Contact.Count);
         AV23numMinSignatures = AV21group_sdt.gxTpr_Minimumshares;
         AV10base_char = "/";
         if ( AV22numPeers < 2 )
         {
            AV13error = "There are not enough contacts on the group";
            cleanup();
            if (true) return;
         }
         /* User Code */
          NBitcoin.Network network;
         if ( StringUtil.StrCmp(AV17networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV17networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV17networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV13error = "Network Type not sopported";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            AV27items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
            AV42GXV1 = 1;
            while ( AV42GXV1 <= AV21group_sdt.gxTpr_Contact.Count )
            {
               AV24oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV21group_sdt.gxTpr_Contact.Item(AV42GXV1));
               if ( ( AV21group_sdt.gxTpr_Grouptype == 800 ) && ! ( AV24oneContact.gxTpr_Contactid == AV24oneContact.gxTpr_Contactgroupid ) )
               {
                  if ( (Convert.ToDecimal( AV41generatedType ) == NumberUtil.Val( "2", ".") ) )
                  {
                     AV15extendedPublicKey = AV24oneContact.gxTpr_Extpubkeymultisigreceiving;
                     AV36ownerPublicKey = AV21group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
                  }
                  else
                  {
                     AV15extendedPublicKey = AV24oneContact.gxTpr_Extpubkeymultisigchange;
                     AV36ownerPublicKey = AV21group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
                  }
                  GXt_char1 = AV13error;
                  new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV15extendedPublicKey,  AV17networkType,  AV10base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV40sequence), 10, 0)), out  AV16extPubKeyInfo, out  GXt_char1) ;
                  AV13error = GXt_char1;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV27items.Add(StringUtil.Trim( AV16extPubKeyInfo.gxTpr_Ec_publickey), 0);
                  }
                  else
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               AV42GXV1 = (int)(AV42GXV1+1);
            }
            AV27items.Sort("");
            AV29currentCombination = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
            AV28finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
            new GeneXus.Programs.math.generatecombinations(context ).execute(  AV27items,  AV23numMinSignatures,  1, ref  AV29currentCombination, ref  AV28finalCombinations) ;
            AV35numOfScripts = (short)(AV28finalCombinations.Count);
            /* User Code */
             var peers = AV23numMinSignatures;
            /* User Code */
             var howManyScripts = AV35numOfScripts;
            /* User Code */
             var combPubKeysForScripts = new NBitcoin.Secp256k1.ECPubKey[howManyScripts];
            /* User Code */
             var Scripts = new NBitcoin.TapScript[howManyScripts];
            /* User Code */
             string pubKeyString;
            /* User Code */
             int p;
            /* User Code */
             int i;
            /* User Code */
             var ctx = NBitcoin.Secp256k1.Context.Instance;
            AV34i = 0;
            AV43GXV2 = 1;
            while ( AV43GXV2 <= AV28finalCombinations.Count )
            {
               AV30finalOneCombination = ((GeneXus.Programs.math.SdtfinalCombination)AV28finalCombinations.Item(AV43GXV2));
               AV32p = 0;
               /* User Code */
                var combinationOfPubKeys = new NBitcoin.Secp256k1.ECPubKey[peers];
               AV44GXV3 = 1;
               while ( AV44GXV3 <= AV30finalOneCombination.gxTpr_Items.Count )
               {
                  AV31oneItem = AV30finalOneCombination.gxTpr_Items.GetString(AV44GXV3);
                  /* User Code */
                   p = AV32p;
                  /* User Code */
                   pubKeyString = AV31oneItem;
                  /* User Code */
                   combinationOfPubKeys[p] = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(pubKeyString));
                  AV32p = (short)(AV32p+1);
                  AV44GXV3 = (int)(AV44GXV3+1);
               }
               /* User Code */
                i = AV34i;
               /* User Code */
                combPubKeysForScripts[i] = NBitcoin.Secp256k1.ECPubKey.MusigAggregate(combinationOfPubKeys);
               AV34i = (short)(AV34i+1);
               AV43GXV2 = (int)(AV43GXV2+1);
            }
            /* User Code */
             var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
            /* User Code */
             var probability = (uint)(100 / howManyScripts);
            /* User Code */
             for (int j = 0; j < howManyScripts; j++)
            /* User Code */
             {
            /* User Code */
             Scripts[j] = new NBitcoin.Script(NBitcoin.Op.GetPushOp(combPubKeysForScripts[j].ToXOnlyPubKey().ToBytes()), NBitcoin.OpcodeType.OP_CHECKSIG).ToTapScript(NBitcoin.TapLeafVersion.C0);
            /* User Code */
             scriptWeightsList.Add((probability, Scripts[j]));
            /* User Code */
             }
            /* User Code */
             var scriptWeights = scriptWeightsList.ToArray();
            GXt_char1 = AV13error;
            new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV36ownerPublicKey,  AV17networkType,  AV10base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV40sequence), 10, 0)), out  AV16extPubKeyInfo, out  GXt_char1) ;
            AV13error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               AV36ownerPublicKey = AV16extPubKeyInfo.gxTpr_Ec_publickey;
               /* User Code */
                var ownerKeyString = AV36ownerPublicKey;
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
                AV18one_address = final_address;
               AV39sdt_oneAddress.gxTpr_Address = AV18one_address;
               AV39sdt_oneAddress.gxTpr_Generatedtype = AV41generatedType;
               AV39sdt_oneAddress.gxTpr_Isused = false;
               AV39sdt_oneAddress.gxTpr_Creationsequence = AV40sequence;
            }
            else
            {
               cleanup();
               if (true) return;
            }
            /* User Code */
            	}
            /* User Code */
            	catch (Exception ex)
            /* User Code */
            	{
            /* User Code */
            		AV13error = ex.Message.ToString();
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
         AV39sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV13error = "";
         AV10base_char = "";
         AV27items = new GxSimpleCollection<string>();
         AV24oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15extendedPublicKey = "";
         AV36ownerPublicKey = "";
         AV16extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV29currentCombination = new GxSimpleCollection<string>();
         AV28finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
         AV30finalOneCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV31oneItem = "";
         GXt_char1 = "";
         AV18one_address = "";
         /* GeneXus formulas. */
      }

      private short AV41generatedType ;
      private short AV22numPeers ;
      private short AV23numMinSignatures ;
      private short AV35numOfScripts ;
      private short AV34i ;
      private short AV32p ;
      private int AV42GXV1 ;
      private int AV43GXV2 ;
      private int AV44GXV3 ;
      private long AV40sequence ;
      private string AV17networkType ;
      private string AV13error ;
      private string AV10base_char ;
      private string AV15extendedPublicKey ;
      private string AV36ownerPublicKey ;
      private string AV31oneItem ;
      private string GXt_char1 ;
      private string AV18one_address ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV21group_sdt ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV39sdt_oneAddress ;
      private GxSimpleCollection<string> AV27items ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV24oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV16extPubKeyInfo ;
      private GxSimpleCollection<string> AV29currentCombination ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> AV28finalCombinations ;
      private GeneXus.Programs.math.SdtfinalCombination AV30finalOneCombination ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP4_sdt_oneAddress ;
      private string aP5_error ;
   }

}
