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
namespace GeneXus.Programs.wallet {
   public class buildtransactionmusig : GXProcedure
   {
      public buildtransactionmusig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildtransactionmusig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_isFinalCall ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           bool aP2_sendAllCoins ,
                           decimal aP3_transactionFee ,
                           string aP4_networkType ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                           decimal aP6_inSendCoins ,
                           string aP7_sendTo ,
                           string aP8_returnTo ,
                           out long aP9_virtualSize ,
                           out string aP10_hexTransaction ,
                           out bool aP11_verified ,
                           out string aP12_error )
      {
         this.AV78isFinalCall = aP0_isFinalCall;
         this.AV39group_sdt = aP1_group_sdt;
         this.AV28sendAllCoins = aP2_sendAllCoins;
         this.AV23transactionFee = aP3_transactionFee;
         this.AV13networkType = aP4_networkType;
         this.AV24transactionsToSend = aP5_transactionsToSend;
         this.AV65inSendCoins = aP6_inSendCoins;
         this.AV20sendTo = aP7_sendTo;
         this.AV18returnTo = aP8_returnTo;
         this.AV27virtualSize = 0 ;
         this.AV9hexTransaction = "" ;
         this.AV26verified = false ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP5_transactionsToSend=this.AV24transactionsToSend;
         aP9_virtualSize=this.AV27virtualSize;
         aP10_hexTransaction=this.AV9hexTransaction;
         aP11_verified=this.AV26verified;
         aP12_error=this.AV8error;
      }

      public string executeUdp( bool aP0_isFinalCall ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                bool aP2_sendAllCoins ,
                                decimal aP3_transactionFee ,
                                string aP4_networkType ,
                                ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                                decimal aP6_inSendCoins ,
                                string aP7_sendTo ,
                                string aP8_returnTo ,
                                out long aP9_virtualSize ,
                                out string aP10_hexTransaction ,
                                out bool aP11_verified )
      {
         execute(aP0_isFinalCall, aP1_group_sdt, aP2_sendAllCoins, aP3_transactionFee, aP4_networkType, ref aP5_transactionsToSend, aP6_inSendCoins, aP7_sendTo, aP8_returnTo, out aP9_virtualSize, out aP10_hexTransaction, out aP11_verified, out aP12_error);
         return AV8error ;
      }

      public void executeSubmit( bool aP0_isFinalCall ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 bool aP2_sendAllCoins ,
                                 decimal aP3_transactionFee ,
                                 string aP4_networkType ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                                 decimal aP6_inSendCoins ,
                                 string aP7_sendTo ,
                                 string aP8_returnTo ,
                                 out long aP9_virtualSize ,
                                 out string aP10_hexTransaction ,
                                 out bool aP11_verified ,
                                 out string aP12_error )
      {
         this.AV78isFinalCall = aP0_isFinalCall;
         this.AV39group_sdt = aP1_group_sdt;
         this.AV28sendAllCoins = aP2_sendAllCoins;
         this.AV23transactionFee = aP3_transactionFee;
         this.AV13networkType = aP4_networkType;
         this.AV24transactionsToSend = aP5_transactionsToSend;
         this.AV65inSendCoins = aP6_inSendCoins;
         this.AV20sendTo = aP7_sendTo;
         this.AV18returnTo = aP8_returnTo;
         this.AV27virtualSize = 0 ;
         this.AV9hexTransaction = "" ;
         this.AV26verified = false ;
         this.AV8error = "" ;
         SubmitImpl();
         aP5_transactionsToSend=this.AV24transactionsToSend;
         aP9_virtualSize=this.AV27virtualSize;
         aP10_hexTransaction=this.AV9hexTransaction;
         aP11_verified=this.AV26verified;
         aP12_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network;
         /* User Code */
          network = NBitcoin.Network.Main;
         /* User Code */
          string tx_hex;
         /* User Code */
          NBitcoin.Transaction tx;
         /* User Code */
          NBitcoin.Key privateKey;
         /* User Code */
          NBitcoin.Secp256k1.ECPrivKey ecPrivateKey;
         /* User Code */
          var all_keys = new System.Collections.Generic.List<NBitcoin.Key>();
         /* User Code */
          var all_EcKeys = new System.Collections.Generic.List<NBitcoin.Secp256k1.ECPrivKey>();
         /* User Code */
          var spentAllOutputsIn = new System.Collections.Generic.List<NBitcoin.TxOut>();
         /* User Code */
          var AllTreeInfo = new System.Collections.Generic.List<NBitcoin.TaprootSpendInfo>();
         /* User Code */
          var AllScripts = new System.Collections.Generic.List<NBitcoin.TapScript>();
         /* User Code */
          var ctx = NBitcoin.Secp256k1.Context.Instance;
         /* User Code */
          System.Collections.Generic.List<NBitcoin.Secp256k1.ECPubKey[]> AllcombinationOfPubKeys = new System.Collections.Generic.List<NBitcoin.Secp256k1.ECPubKey[]>();
         if ( AV28sendAllCoins )
         {
            AV19sendCoins = (decimal)(AV65inSendCoins-AV23transactionFee);
         }
         else
         {
            AV19sendCoins = AV65inSendCoins;
         }
         /* User Code */
          try
         /* User Code */
          {
         if ( StringUtil.StrCmp(AV13networkType, "Main") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV13networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV13networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV8error = "Network Type not sopported";
         }
         /* User Code */
          var destination = NBitcoin.BitcoinAddress.Create(AV20sendTo, network);
         /* User Code */
          var returnto = NBitcoin.BitcoinAddress.Create(AV18returnTo, network);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            /* User Code */
             var spender = network.CreateTransaction();
            AV64signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
            AV80GXV1 = 1;
            while ( AV80GXV1 <= AV24transactionsToSend.Count )
            {
               AV14oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV24transactionsToSend.Item(AV80GXV1));
               AV38generatedType = AV14oneAddressHistory.gxTpr_Addressgeneratedtype;
               AV51sequence = AV14oneAddressHistory.gxTpr_Addresscreationsequence;
               AV44numPeers = (short)(AV39group_sdt.gxTpr_Contact.Count);
               AV42numMinSignatures = AV39group_sdt.gxTpr_Minimumshares;
               AV32base_char = "/";
               if ( AV44numPeers < 2 )
               {
                  AV8error = "There are not enough contacts on the group";
                  this.cleanup();
                  if (true) return;
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  AV41items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
                  AV58signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
                  AV81GXV2 = 1;
                  while ( AV81GXV2 <= AV39group_sdt.gxTpr_Contact.Count )
                  {
                     AV46oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV39group_sdt.gxTpr_Contact.Item(AV81GXV2));
                     if ( ( AV39group_sdt.gxTpr_Grouptype == 200 ) && ! ( AV46oneContact.gxTpr_Contactid == AV46oneContact.gxTpr_Contactgroupid ) )
                     {
                        if ( (Convert.ToDecimal( AV38generatedType ) == NumberUtil.Val( "2", ".") ) )
                        {
                           AV34extendedPublicKey = AV46oneContact.gxTpr_Extpubkeymultisigreceiving;
                           AV48ownerPublicKey = AV39group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
                           if ( ! AV39group_sdt.gxTpr_Amigroupowner && ( StringUtil.StrCmp(AV39group_sdt.gxTpr_Extpubkeymultisigreceiving, AV46oneContact.gxTpr_Extpubkeymultisigreceiving) == 0 ) )
                           {
                              AV52notOwnerMyExtendePubKey = AV34extendedPublicKey;
                              AV58signatureContact.gxTpr_Contactusername = StringUtil.Trim( AV46oneContact.gxTpr_Contactusername);
                           }
                        }
                        else
                        {
                           AV34extendedPublicKey = AV46oneContact.gxTpr_Extpubkeymultisigchange;
                           AV48ownerPublicKey = AV39group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
                           if ( ! AV39group_sdt.gxTpr_Amigroupowner && ( StringUtil.StrCmp(AV39group_sdt.gxTpr_Extpubkeymultisigchange, AV46oneContact.gxTpr_Extpubkeymultisigchange) == 0 ) )
                           {
                              AV52notOwnerMyExtendePubKey = AV34extendedPublicKey;
                              AV58signatureContact.gxTpr_Contactusername = StringUtil.Trim( AV46oneContact.gxTpr_Contactusername);
                           }
                        }
                        GXt_char1 = AV8error;
                        new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV34extendedPublicKey,  AV13networkType,  AV32base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV51sequence), 10, 0)), out  AV35extPubKeyInfo, out  GXt_char1) ;
                        AV8error = GXt_char1;
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                        {
                           AV41items.Add(StringUtil.Trim( AV35extPubKeyInfo.gxTpr_Ec_publickey), 0);
                           if ( StringUtil.StrCmp(StringUtil.Trim( AV52notOwnerMyExtendePubKey), StringUtil.Trim( AV34extendedPublicKey)) == 0 )
                           {
                              AV53one_EC_PublicKey = StringUtil.Trim( AV35extPubKeyInfo.gxTpr_Ec_publickey);
                           }
                        }
                        else
                        {
                           this.cleanup();
                           if (true) return;
                        }
                     }
                     AV81GXV2 = (int)(AV81GXV2+1);
                  }
                  AV41items.Sort("");
                  AV33currentCombination = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
                  AV36finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
                  new GeneXus.Programs.math.generatecombinations(context ).execute(  AV41items,  AV42numMinSignatures,  1, ref  AV33currentCombination, ref  AV36finalCombinations) ;
                  AV43numOfScripts = (short)(AV36finalCombinations.Count);
                  /* User Code */
                   var peers = AV42numMinSignatures;
                  /* User Code */
                   var howManyScripts = AV43numOfScripts;
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
                  AV40i = 0;
                  GXt_objcol_char2 = AV79all_EC_PublicKey;
                  new GeneXus.Programs.wallet.registered.getec_pubkeysfrommusigdata(context ).execute(  AV14oneAddressHistory, out  GXt_objcol_char2) ;
                  AV79all_EC_PublicKey = GXt_objcol_char2;
                  AV82GXV3 = 1;
                  while ( AV82GXV3 <= AV36finalCombinations.Count )
                  {
                     AV37finalOneCombination = ((GeneXus.Programs.math.SdtfinalCombination)AV36finalCombinations.Item(AV82GXV3));
                     if ( AV14oneAddressHistory.gxTpr_Multisignaturedata.Count > 0 )
                     {
                        GXt_boolean3 = AV70isCombNeeded;
                        new GeneXus.Programs.wallet.registered.iscombinationsneeded(context ).execute(  AV79all_EC_PublicKey,  AV37finalOneCombination, out  GXt_boolean3) ;
                        AV70isCombNeeded = GXt_boolean3;
                     }
                     else
                     {
                        AV70isCombNeeded = true;
                     }
                     AV49p = 0;
                     /* User Code */
                      var combinationOfPubKeys = new NBitcoin.Secp256k1.ECPubKey[peers];
                     AV83GXV4 = 1;
                     while ( AV83GXV4 <= AV37finalOneCombination.gxTpr_Items.Count )
                     {
                        AV47oneItem = AV37finalOneCombination.gxTpr_Items.GetString(AV83GXV4);
                        /* User Code */
                         p = AV49p;
                        /* User Code */
                         pubKeyString = AV47oneItem;
                        /* User Code */
                         combinationOfPubKeys[p] = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(pubKeyString));
                        if ( ( StringUtil.StrCmp(StringUtil.Trim( AV47oneItem), StringUtil.Trim( AV53one_EC_PublicKey)) == 0 ) && AV70isCombNeeded )
                        {
                           AV59signatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
                           AV59signatureData.gxTpr_I = AV40i;
                           AV59signatureData.gxTpr_P = AV49p;
                           AV59signatureData.gxTpr_Transactionid = StringUtil.Trim( AV14oneAddressHistory.gxTpr_Receivedtransactionid);
                           AV59signatureData.gxTpr_N = AV14oneAddressHistory.gxTpr_Recivedn;
                           AV59signatureData.gxTpr_Ec_pubkey = StringUtil.Trim( AV53one_EC_PublicKey);
                           AV59signatureData.gxTpr_Finalcombination = AV37finalOneCombination;
                           AV58signatureContact.gxTpr_Data.Add(AV59signatureData, 0);
                        }
                        AV49p = (int)(AV49p+1);
                        AV83GXV4 = (int)(AV83GXV4+1);
                     }
                     /* User Code */
                      AllcombinationOfPubKeys.Add(combinationOfPubKeys);
                     /* User Code */
                      i = AV40i;
                     /* User Code */
                      combPubKeysForScripts[i] = NBitcoin.Secp256k1.ECPubKey.MusigAggregate(combinationOfPubKeys);
                     AV40i = (int)(AV40i+1);
                     AV82GXV3 = (int)(AV82GXV3+1);
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
                   AllScripts.Add(Scripts[j]);
                  /* User Code */
                   scriptWeightsList.Add((probability, Scripts[j]));
                  /* User Code */
                   }
                  /* User Code */
                   var scriptWeights = scriptWeightsList.ToArray();
                  GXt_char1 = AV8error;
                  new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV48ownerPublicKey,  AV13networkType,  AV32base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV51sequence), 10, 0)), out  AV35extPubKeyInfo, out  GXt_char1) ;
                  AV8error = GXt_char1;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV48ownerPublicKey = AV35extPubKeyInfo.gxTpr_Ec_publickey;
                     /* User Code */
                      var ownerKeyString = AV48ownerPublicKey;
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
                      AllTreeInfo.Add(treeInfo);
                     /* User Code */
                      var addr = taprootPubKey.GetAddress(network);
                     /* User Code */
                      var final_address = addr.ToString();
                     /* User Code */
                      AV45one_address = final_address;
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV45one_address), StringUtil.Trim( AV14oneAddressHistory.gxTpr_Receivedaddress)) != 0 )
                     {
                        AV8error = "Generated address and UTXO address don't match";
                        if (true) break;
                     }
                  }
               }
               GXt_char1 = AV16privateKey;
               new GeneXus.Programs.wallet.getprivatekeyfromaddresshistory(context ).execute(  AV14oneAddressHistory, out  GXt_char1) ;
               AV16privateKey = GXt_char1;
               /* User Code */
                string hexPrivateKey = AV16privateKey;
               /* User Code */
                byte[] Keybytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
               /* User Code */
               	privateKey = new NBitcoin.Key(Keybytes);
               /* User Code */
                all_keys.Add(privateKey);
               /* User Code */
                ecPrivateKey = ctx.CreateECPrivKey(Keybytes);
               /* User Code */
                all_EcKeys.Add(ecPrivateKey);
               AV77receivedTransactionHex = AV14oneAddressHistory.gxTpr_Receivedtransactionhex;
               /* User Code */
                tx_hex = AV77receivedTransactionHex;
               /* User Code */
                tx = NBitcoin.Transaction.Parse(tx_hex, network);
               AV17receivedIn = 0;
               /* User Code */
                foreach (var output in tx.Outputs.AsIndexedOutputs())
               /* User Code */
                	{
               if ( AV17receivedIn == AV14oneAddressHistory.gxTpr_Recivedn )
               {
                  /* User Code */
                   		spender.Inputs.Add(new NBitcoin.OutPoint(tx, output.N));
                  /* User Code */
                   		spentAllOutputsIn.Add(output.TxOut);
               }
               AV17receivedIn = (long)(AV17receivedIn+1);
               /* User Code */
                	}
               AV29totalInUTXOs = (decimal)(AV29totalInUTXOs+(AV14oneAddressHistory.gxTpr_Balance));
               AV64signatureContacts.Add(AV58signatureContact, 0);
               AV80GXV1 = (int)(AV80GXV1+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               /* User Code */
                NBitcoin.TxOut[] spentOutputsIn = spentAllOutputsIn.ToArray();
               if ( AV28sendAllCoins )
               {
                  AV31sendTotal = (decimal)(AV29totalInUTXOs-AV23transactionFee);
                  /* User Code */
                   spender.Outputs.Add(NBitcoin.Money.Coins(AV31sendTotal), destination);
               }
               else
               {
                  AV30returnTotal = (decimal)(AV29totalInUTXOs-AV19sendCoins-AV23transactionFee);
                  /* User Code */
                   spender.Outputs.Add(NBitcoin.Money.Coins(AV19sendCoins), destination);
                  /* User Code */
                   spender.Outputs.Add(NBitcoin.Money.Coins(AV30returnTotal), returnto);
               }
               /* User Code */
                var sighash = NBitcoin.TaprootSigHash.All | NBitcoin.TaprootSigHash.AnyoneCanPay;
               /* User Code */
                var allTreeInfoArray = AllTreeInfo.ToArray();
               /* User Code */
                var allkeysarray = all_keys.ToArray();
               /* User Code */
                var allScriptsArray = AllScripts.ToArray();
               /* User Code */
                int s;
               /* User Code */
                var allEcKeysarray = all_EcKeys.ToArray();
               if ( AV39group_sdt.gxTpr_Amigroupowner )
               {
                  /* User Code */
                   for (int i = 0; i < spender.Inputs.Count; i++)
                  /* User Code */
                   {
                  /* User Code */
                   var extectionDataKeySpend = new NBitcoin.TaprootExecutionData(i) { SigHash = sighash };
                  /* User Code */
                   var hashKeySpend = spender.GetSignatureHashTaproot(spentOutputsIn, extectionDataKeySpend);
                  /* User Code */
                   var sig = allkeysarray[i].SignTaprootKeySpend(hashKeySpend, allTreeInfoArray[i].MerkleRoot, sighash);
                  /* User Code */
                   spender.Inputs[i].WitScript = new NBitcoin.WitScript(NBitcoin.Op.GetPushOp(sig.ToBytes()));
                  /* User Code */
                   }
               }
               else
               {
                  AV40i = 0;
                  AV68pubKeyInexes = 0;
                  AV84GXV5 = 1;
                  while ( AV84GXV5 <= AV24transactionsToSend.Count )
                  {
                     AV14oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV24transactionsToSend.Item(AV84GXV5));
                     /* User Code */
                      var i = AV40i;
                     AV85GXV6 = 1;
                     while ( AV85GXV6 <= AV64signatureContacts.Count )
                     {
                        AV58signatureContact = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData)AV64signatureContacts.Item(AV85GXV6));
                        AV86GXV7 = 1;
                        while ( AV86GXV7 <= AV58signatureContact.gxTpr_Data.Count )
                        {
                           AV59signatureData = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV58signatureContact.gxTpr_Data.Item(AV86GXV7));
                           if ( ( StringUtil.StrCmp(AV59signatureData.gxTpr_Transactionid, AV14oneAddressHistory.gxTpr_Receivedtransactionid) == 0 ) && ( AV59signatureData.gxTpr_N == AV14oneAddressHistory.gxTpr_Recivedn ) )
                           {
                              AV56s = AV59signatureData.gxTpr_I;
                              /* User Code */
                               s = AV56s;
                              /* User Code */
                               var pubKeyInexes = AV68pubKeyInexes + s;
                              /* User Code */
                               var extectionDataScriptSpend = new NBitcoin.TaprootExecutionData(i, allScriptsArray[pubKeyInexes].LeafHash) { SigHash = sighash };
                              /* User Code */
                               var hashScriptSpend = spender.GetSignatureHashTaproot(spentOutputsIn, extectionDataScriptSpend);
                              /* User Code */
                               var musig = new NBitcoin.Secp256k1.Musig.MusigContext(AllcombinationOfPubKeys[pubKeyInexes], hashScriptSpend.ToBytes());
                              /* User Code */
                               var nonces = new NBitcoin.Secp256k1.Musig.MusigPrivNonce[AllcombinationOfPubKeys[pubKeyInexes].Length];
                              /* User Code */
                               for (int n = 0; n < AllcombinationOfPubKeys[pubKeyInexes].Length; n++)
                              /* User Code */
                               {
                              /* User Code */
                               nonces[n] = musig.GenerateNonce(AllcombinationOfPubKeys[pubKeyInexes][n]);
                              /* User Code */
                               }
                              AV49p = AV59signatureData.gxTpr_P;
                              /* User Code */
                               var p = AV49p;
                              /* User Code */
                               var pubNonces = new NBitcoin.Secp256k1.Musig.MusigPubNonce[nonces.Length];
                              /* User Code */
                               for (int n = 0; n < nonces.Length; n++)
                              /* User Code */
                               {
                              /* User Code */
                               pubNonces[n] = nonces[n].CreatePubNonce();
                              /* User Code */
                               var strOnePubNonce = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(pubNonces[n].ToBytes());
                              /* User Code */
                               AV60pubNonce = strOnePubNonce;
                              AV59signatureData.gxTpr_Pubnonces.Add(AV60pubNonce, 0);
                              /* User Code */
                               }
                              /* User Code */
                               musig.ProcessNonces(pubNonces);
                              /* User Code */
                               var sigs = new NBitcoin.Secp256k1.Musig.MusigPartialSignature[nonces.Length];
                              /* User Code */
                               for (int n = 0; n < nonces.Length; n++)
                              /* User Code */
                               {
                              /* User Code */
                               if (n == p)
                              /* User Code */
                               {
                              /* User Code */
                               sigs[n] = musig.Sign(allEcKeysarray[i], nonces[n]);
                              /* User Code */
                               AV61oneSignature = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(sigs[n].ToBytes());
                              AV59signatureData.gxTpr_Signatures.Add(AV61oneSignature, 0);
                              /* User Code */
                               }
                              /* User Code */
                               }
                              if ( AV14oneAddressHistory.gxTpr_Multisignaturedata.Count > 0 )
                              {
                                 if ( new GeneXus.Programs.wallet.registered.iscombinationsneededforsignature(context).executeUdp(  AV14oneAddressHistory,  AV59signatureData.gxTpr_Finalcombination, out  AV74returnMultiSigSignatureData) )
                                 {
                                    if ( ! ( AV42numMinSignatures == AV59signatureData.gxTpr_Finalcombination.gxTpr_Items.Count ) )
                                    {
                                       AV8error = "Combination does not much amount of Signatures";
                                       this.cleanup();
                                       if (true) return;
                                    }
                                    else
                                    {
                                       AV87GXV8 = 1;
                                       while ( AV87GXV8 <= AV74returnMultiSigSignatureData.gxTpr_Data.Count )
                                       {
                                          AV73returnDataItem = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV74returnMultiSigSignatureData.gxTpr_Data.Item(AV87GXV8));
                                          AV88GXV9 = 1;
                                          while ( AV88GXV9 <= AV73returnDataItem.gxTpr_Signatures.Count )
                                          {
                                             AV61oneSignature = AV73returnDataItem.gxTpr_Signatures.GetString(AV88GXV9);
                                             AV59signatureData.gxTpr_Signatures.Add(AV61oneSignature, 0);
                                             AV88GXV9 = (int)(AV88GXV9+1);
                                          }
                                          AV87GXV8 = (int)(AV87GXV8+1);
                                       }
                                       AV76numberPubNonces = (short)(AV59signatureData.gxTpr_Pubnonces.Count);
                                       AV89Numbersignatures = AV59signatureData.gxTpr_Signatures.Count;
                                       /* User Code */
                                        var numberPubNonces = AV76numberPubNonces;
                                       /* User Code */
                                        var numberSignatures = AV89Numbersignatures;
                                       /* User Code */
                                        pubNonces = new NBitcoin.Secp256k1.Musig.MusigPubNonce[numberPubNonces];
                                       /* User Code */
                                        sigs = new NBitcoin.Secp256k1.Musig.MusigPartialSignature[numberSignatures];
                                       /* User Code */
                                        var musig2 = new NBitcoin.Secp256k1.Musig.MusigContext(AllcombinationOfPubKeys[pubKeyInexes], hashScriptSpend.ToBytes());
                                       /* User Code */
                                        string noceString;
                                       /* User Code */
                                        byte[] backToByptes;
                                       /* User Code */
                                        var countSignatures = 0;
                                       AV90GXV10 = 1;
                                       while ( AV90GXV10 <= AV59signatureData.gxTpr_Pubnonces.Count )
                                       {
                                          AV60pubNonce = AV59signatureData.gxTpr_Pubnonces.GetString(AV90GXV10);
                                          /* User Code */
                                           noceString = AV60pubNonce;
                                          /* User Code */
                                           backToByptes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(noceString);
                                          /* User Code */
                                           pubNonces[countSignatures] = new NBitcoin.Secp256k1.Musig.MusigPubNonce(backToByptes);
                                          /* User Code */
                                           countSignatures += 1;
                                          AV90GXV10 = (int)(AV90GXV10+1);
                                       }
                                       /* User Code */
                                        musig2.ProcessNonces(pubNonces);
                                       /* User Code */
                                        string sigString;
                                       /* User Code */
                                        countSignatures = 0;
                                       AV91GXV11 = 1;
                                       while ( AV91GXV11 <= AV59signatureData.gxTpr_Signatures.Count )
                                       {
                                          AV61oneSignature = AV59signatureData.gxTpr_Signatures.GetString(AV91GXV11);
                                          /* User Code */
                                           sigString = AV61oneSignature;
                                          /* User Code */
                                           backToByptes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(sigString);
                                          /* User Code */
                                           sigs[countSignatures] = new NBitcoin.Secp256k1.Musig.MusigPartialSignature(backToByptes);
                                          /* User Code */
                                           countSignatures += 1;
                                          AV91GXV11 = (int)(AV91GXV11+1);
                                       }
                                       /* User Code */
                                        var signature = musig2.AggregateSignatures(sigs);
                                       /* User Code */
                                        var schnorrSig = new NBitcoin.Crypto.SchnorrSignature(signature.ToBytes());
                                       /* User Code */
                                        var trSign = new NBitcoin.TaprootSignature(schnorrSig, sighash);
                                       /* User Code */
                                        spender.Inputs[i].WitScript = new NBitcoin.WitScript(NBitcoin.Op.GetPushOp(trSign.ToBytes()), NBitcoin.Op.GetPushOp(allScriptsArray[pubKeyInexes].Script.ToBytes()), NBitcoin.Op.GetPushOp(allTreeInfoArray[i].GetControlBlock(allScriptsArray[pubKeyInexes]).ToBytes()));
                                    }
                                 }
                              }
                           }
                           AV86GXV7 = (int)(AV86GXV7+1);
                        }
                        AV85GXV6 = (int)(AV85GXV6+1);
                     }
                     AV40i = (int)(AV40i+1);
                     AV68pubKeyInexes = (short)(AV68pubKeyInexes+AV43numOfScripts);
                     AV84GXV5 = (int)(AV84GXV5+1);
                  }
               }
               /* User Code */
                var validator = spender.CreateValidator(spentOutputsIn);
               /* User Code */
                var result = validator.ValidateInput(0);
               /* User Code */
                var success = result.Error is null;
               /* User Code */
                AV26verified = success;
               if ( AV39group_sdt.gxTpr_Amigroupowner )
               {
                  /* User Code */
                   AV27virtualSize = spender.GetVirtualSize();
                  /* User Code */
                   AV9hexTransaction = spender.ToHex();
                  if ( ! AV26verified )
                  {
                     AV8error = "Transaction is not Verified";
                  }
               }
               else
               {
                  if ( ! AV26verified && String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     new GeneXus.Programs.wallet.addmusigdatatohistory(context ).execute(  AV64signatureContacts, ref  AV24transactionsToSend) ;
                  }
                  else
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        if ( AV78isFinalCall )
                        {
                           new GeneXus.Programs.wallet.addmusigdatatohistory(context ).execute(  AV64signatureContacts, ref  AV24transactionsToSend) ;
                        }
                        /* User Code */
                         AV27virtualSize = spender.GetVirtualSize();
                        /* User Code */
                         AV9hexTransaction = spender.ToHex();
                     }
                  }
               }
            }
         }
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
         AV9hexTransaction = "";
         AV8error = "";
         AV64signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
         AV14oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV32base_char = "";
         AV41items = new GxSimpleCollection<string>();
         AV58signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV46oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV34extendedPublicKey = "";
         AV48ownerPublicKey = "";
         AV52notOwnerMyExtendePubKey = "";
         AV35extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV53one_EC_PublicKey = "";
         AV33currentCombination = new GxSimpleCollection<string>();
         AV36finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
         AV79all_EC_PublicKey = new GxSimpleCollection<string>();
         GXt_objcol_char2 = new GxSimpleCollection<string>();
         AV37finalOneCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV47oneItem = "";
         AV59signatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV45one_address = "";
         AV16privateKey = "";
         GXt_char1 = "";
         AV77receivedTransactionHex = "";
         AV60pubNonce = "";
         AV61oneSignature = "";
         AV74returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV73returnDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         /* GeneXus formulas. */
      }

      private short AV38generatedType ;
      private short AV44numPeers ;
      private short AV42numMinSignatures ;
      private short AV43numOfScripts ;
      private short AV68pubKeyInexes ;
      private short AV76numberPubNonces ;
      private int AV80GXV1 ;
      private int AV81GXV2 ;
      private int AV40i ;
      private int AV82GXV3 ;
      private int AV49p ;
      private int AV83GXV4 ;
      private int AV84GXV5 ;
      private int AV85GXV6 ;
      private int AV86GXV7 ;
      private int AV56s ;
      private int AV87GXV8 ;
      private int AV88GXV9 ;
      private int AV89Numbersignatures ;
      private int AV90GXV10 ;
      private int AV91GXV11 ;
      private long AV27virtualSize ;
      private long AV51sequence ;
      private long AV17receivedIn ;
      private decimal AV23transactionFee ;
      private decimal AV65inSendCoins ;
      private decimal AV19sendCoins ;
      private decimal AV29totalInUTXOs ;
      private decimal AV31sendTotal ;
      private decimal AV30returnTotal ;
      private string AV13networkType ;
      private string AV20sendTo ;
      private string AV18returnTo ;
      private string AV8error ;
      private string AV32base_char ;
      private string AV34extendedPublicKey ;
      private string AV48ownerPublicKey ;
      private string AV52notOwnerMyExtendePubKey ;
      private string AV53one_EC_PublicKey ;
      private string AV47oneItem ;
      private string AV45one_address ;
      private string AV16privateKey ;
      private string GXt_char1 ;
      private string AV60pubNonce ;
      private string AV61oneSignature ;
      private bool AV78isFinalCall ;
      private bool AV28sendAllCoins ;
      private bool AV26verified ;
      private bool AV70isCombNeeded ;
      private bool GXt_boolean3 ;
      private string AV9hexTransaction ;
      private string AV77receivedTransactionHex ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ;
      private long aP9_virtualSize ;
      private string aP10_hexTransaction ;
      private bool aP11_verified ;
      private string aP12_error ;
      private GxSimpleCollection<string> AV41items ;
      private GxSimpleCollection<string> AV33currentCombination ;
      private GxSimpleCollection<string> AV79all_EC_PublicKey ;
      private GxSimpleCollection<string> GXt_objcol_char2 ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> AV36finalCombinations ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV24transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> AV64signatureContacts ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV35extPubKeyInfo ;
      private GeneXus.Programs.math.SdtfinalCombination AV37finalOneCombination ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV39group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV46oneContact ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV14oneAddressHistory ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV58signatureContact ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV74returnMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV59signatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV73returnDataItem ;
   }

}
