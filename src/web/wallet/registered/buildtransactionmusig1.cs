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
   public class buildtransactionmusig1 : GXProcedure
   {
      public buildtransactionmusig1( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildtransactionmusig1( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_estimateFeeOnly ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           bool aP2_sendAllCoins ,
                           decimal aP3_transactionFee ,
                           string aP4_networkType ,
                           decimal aP5_inSendCoins ,
                           string aP6_sendTo ,
                           string aP7_returnTo ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                           out long aP9_virtualSize ,
                           out string aP10_hexTransaction ,
                           out bool aP11_verified ,
                           out string aP12_error )
      {
         this.AV68estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV17group_sdt = aP1_group_sdt;
         this.AV49sendAllCoins = aP2_sendAllCoins;
         this.AV60transactionFee = aP3_transactionFee;
         this.AV24networkType = aP4_networkType;
         this.AV20inSendCoins = aP5_inSendCoins;
         this.AV51sendTo = aP6_sendTo;
         this.AV46returnTo = aP7_returnTo;
         this.AV61transactionsToSend = aP8_transactionsToSend;
         this.AV63virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV62verified = false ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP8_transactionsToSend=this.AV61transactionsToSend;
         aP9_virtualSize=this.AV63virtualSize;
         aP10_hexTransaction=this.AV18hexTransaction;
         aP11_verified=this.AV62verified;
         aP12_error=this.AV11error;
      }

      public string executeUdp( bool aP0_estimateFeeOnly ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                bool aP2_sendAllCoins ,
                                decimal aP3_transactionFee ,
                                string aP4_networkType ,
                                decimal aP5_inSendCoins ,
                                string aP6_sendTo ,
                                string aP7_returnTo ,
                                ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                                out long aP9_virtualSize ,
                                out string aP10_hexTransaction ,
                                out bool aP11_verified )
      {
         execute(aP0_estimateFeeOnly, aP1_group_sdt, aP2_sendAllCoins, aP3_transactionFee, aP4_networkType, aP5_inSendCoins, aP6_sendTo, aP7_returnTo, ref aP8_transactionsToSend, out aP9_virtualSize, out aP10_hexTransaction, out aP11_verified, out aP12_error);
         return AV11error ;
      }

      public void executeSubmit( bool aP0_estimateFeeOnly ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 bool aP2_sendAllCoins ,
                                 decimal aP3_transactionFee ,
                                 string aP4_networkType ,
                                 decimal aP5_inSendCoins ,
                                 string aP6_sendTo ,
                                 string aP7_returnTo ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ,
                                 out long aP9_virtualSize ,
                                 out string aP10_hexTransaction ,
                                 out bool aP11_verified ,
                                 out string aP12_error )
      {
         this.AV68estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV17group_sdt = aP1_group_sdt;
         this.AV49sendAllCoins = aP2_sendAllCoins;
         this.AV60transactionFee = aP3_transactionFee;
         this.AV24networkType = aP4_networkType;
         this.AV20inSendCoins = aP5_inSendCoins;
         this.AV51sendTo = aP6_sendTo;
         this.AV46returnTo = aP7_returnTo;
         this.AV61transactionsToSend = aP8_transactionsToSend;
         this.AV63virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV62verified = false ;
         this.AV11error = "" ;
         SubmitImpl();
         aP8_transactionsToSend=this.AV61transactionsToSend;
         aP9_virtualSize=this.AV63virtualSize;
         aP10_hexTransaction=this.AV18hexTransaction;
         aP11_verified=this.AV62verified;
         aP12_error=this.AV11error;
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
          var all_keys = new System.Collections.Generic.List<NBitcoin.Key>();
         /* User Code */
          var all_XOnly = new System.Collections.Generic.List<NBitcoin.Secp256k1.ECXOnlyPubKey>();
         /* User Code */
          var spentAllOutputsIn = new System.Collections.Generic.List<NBitcoin.TxOut>();
         /* User Code */
          var AllTreeInfo = new System.Collections.Generic.List<NBitcoin.TaprootSpendInfo>();
         /* User Code */
          var AllScripts = new System.Collections.Generic.List<NBitcoin.TapScript>();
         /* User Code */
          System.Collections.Generic.List<NBitcoin.Op> ops = new System.Collections.Generic.List<NBitcoin.Op>();
         if ( AV49sendAllCoins )
         {
            AV50sendCoins = (decimal)(AV20inSendCoins-AV60transactionFee);
         }
         else
         {
            AV50sendCoins = AV20inSendCoins;
         }
         /* User Code */
          try
         /* User Code */
          {
         if ( StringUtil.StrCmp(AV24networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV24networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV24networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV11error = "Network Type not sopported";
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            /* User Code */
             var spender = network.CreateTransaction();
            AV56signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
            AV80GXV1 = 1;
            while ( AV80GXV1 <= AV61transactionsToSend.Count )
            {
               AV33oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV61transactionsToSend.Item(AV80GXV1));
               AV16generatedType = AV33oneAddressHistory.gxTpr_Addressgeneratedtype;
               AV53sequence = AV33oneAddressHistory.gxTpr_Addresscreationsequence;
               AV30numPeers = (short)(AV17group_sdt.gxTpr_Contact.Count);
               AV28numMinSignatures = AV17group_sdt.gxTpr_Minimumshares;
               AV9base_char = "/";
               if ( AV30numPeers < 2 )
               {
                  AV11error = "There are not enough contacts on the group";
                  cleanup();
                  if (true) return;
               }
               if ( AV30numPeers > 16 )
               {
                  AV11error = "There are to many approvers on the group the maximum is 16";
                  cleanup();
                  if (true) return;
               }
               AV23items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               AV55signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
               AV81GXV2 = 1;
               while ( AV81GXV2 <= AV17group_sdt.gxTpr_Contact.Count )
               {
                  AV34oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV81GXV2));
                  if ( ( AV17group_sdt.gxTpr_Grouptype == 30 ) && ! ( AV34oneContact.gxTpr_Contactid == AV34oneContact.gxTpr_Contactgroupid ) )
                  {
                     if ( (Convert.ToDecimal( AV16generatedType ) == NumberUtil.Val( "2", ".") ) )
                     {
                        AV12extendedPublicKey = AV34oneContact.gxTpr_Extpubkeymultisigreceiving;
                        AV37ownerPublicKey = AV17group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
                        if ( ! AV17group_sdt.gxTpr_Amigroupowner && ( StringUtil.StrCmp(AV17group_sdt.gxTpr_Extpubkeymultisigreceiving, AV34oneContact.gxTpr_Extpubkeymultisigreceiving) == 0 ) )
                        {
                           AV25notOwnerMyExtendePubKey = AV12extendedPublicKey;
                           AV55signatureContact.gxTpr_Contactusername = StringUtil.Trim( AV34oneContact.gxTpr_Contactusername);
                        }
                     }
                     else
                     {
                        AV12extendedPublicKey = AV34oneContact.gxTpr_Extpubkeymultisigchange;
                        AV37ownerPublicKey = AV17group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
                        if ( ! AV17group_sdt.gxTpr_Amigroupowner && ( StringUtil.StrCmp(AV17group_sdt.gxTpr_Extpubkeymultisigchange, AV34oneContact.gxTpr_Extpubkeymultisigchange) == 0 ) )
                        {
                           AV25notOwnerMyExtendePubKey = AV12extendedPublicKey;
                           AV55signatureContact.gxTpr_Contactusername = StringUtil.Trim( AV34oneContact.gxTpr_Contactusername);
                        }
                     }
                     GXt_char1 = AV11error;
                     new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV12extendedPublicKey,  AV24networkType,  AV9base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV53sequence), 10, 0)), out  AV13extPubKeyInfo, out  GXt_char1) ;
                     AV11error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        AV23items.Add(StringUtil.Trim( AV13extPubKeyInfo.gxTpr_Ec_publickey), 0);
                        if ( StringUtil.StrCmp(StringUtil.Trim( AV25notOwnerMyExtendePubKey), StringUtil.Trim( AV12extendedPublicKey)) == 0 )
                        {
                           AV32one_EC_PublicKey = StringUtil.Trim( AV13extPubKeyInfo.gxTpr_Ec_publickey);
                        }
                     }
                     else
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
                  AV81GXV2 = (int)(AV81GXV2+1);
               }
               AV23items.Sort("");
               AV10currentCombination = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               AV14finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
               new GeneXus.Programs.math.generatecombinations(context ).execute(  AV23items,  AV28numMinSignatures,  1, ref  AV10currentCombination, ref  AV14finalCombinations) ;
               AV29numOfScripts = (short)(AV14finalCombinations.Count);
               /* User Code */
                var peers = AV28numMinSignatures;
               /* User Code */
                var howManyScripts = AV29numOfScripts;
               /* User Code */
                var Scripts = new NBitcoin.TapScript[howManyScripts];
               /* User Code */
                string pubKeyString;
               /* User Code */
                int p;
               /* User Code */
                int i;
               /* User Code */
                var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
               /* User Code */
                var probability = (uint)(100 / howManyScripts);
               AV19i = 0;
               GXt_objcol_char2 = AV8all_EC_PublicKey;
               new GeneXus.Programs.wallet.registered.getec_pubkeysfrommusigdata(context ).execute(  AV33oneAddressHistory, out  GXt_objcol_char2) ;
               AV8all_EC_PublicKey = GXt_objcol_char2;
               AV82GXV3 = 1;
               while ( AV82GXV3 <= AV14finalCombinations.Count )
               {
                  AV15finalOneCombination = ((GeneXus.Programs.math.SdtfinalCombination)AV14finalCombinations.Item(AV82GXV3));
                  if ( AV33oneAddressHistory.gxTpr_Multisignaturedata.Count > 0 )
                  {
                     GXt_boolean3 = AV21isCombNeeded;
                     new GeneXus.Programs.wallet.registered.iscombinationsneeded(context ).execute(  AV8all_EC_PublicKey,  AV15finalOneCombination, out  GXt_boolean3) ;
                     AV21isCombNeeded = GXt_boolean3;
                  }
                  else
                  {
                     AV21isCombNeeded = true;
                  }
                  AV38p = 0;
                  /* User Code */
                   ops.Clear();
                  AV83GXV4 = 1;
                  while ( AV83GXV4 <= AV15finalOneCombination.gxTpr_Items.Count )
                  {
                     AV35oneItem = AV15finalOneCombination.gxTpr_Items.GetString(AV83GXV4);
                     /* User Code */
                      p = AV38p;
                     /* User Code */
                      pubKeyString = AV35oneItem;
                     /* User Code */
                      var xonlypubk = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(pubKeyString)).ToXOnlyPubKey();
                     /* User Code */
                      ops.Add(NBitcoin.Op.GetPushOp(xonlypubk.ToBytes()));
                     if ( AV38p == 0 )
                     {
                        /* User Code */
                         ops.Add(NBitcoin.OpcodeType.OP_CHECKSIG);
                     }
                     else
                     {
                        /* User Code */
                         ops.Add(NBitcoin.OpcodeType.OP_CHECKSIGADD);
                     }
                     if ( ( StringUtil.StrCmp(StringUtil.Trim( AV35oneItem), StringUtil.Trim( AV32one_EC_PublicKey)) == 0 ) && AV21isCombNeeded )
                     {
                        AV57signatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
                        AV57signatureData.gxTpr_I = AV19i;
                        AV57signatureData.gxTpr_P = AV38p;
                        AV57signatureData.gxTpr_Transactionid = StringUtil.Trim( AV33oneAddressHistory.gxTpr_Receivedtransactionid);
                        AV57signatureData.gxTpr_N = AV33oneAddressHistory.gxTpr_Recivedn;
                        AV57signatureData.gxTpr_Ec_pubkey = StringUtil.Trim( AV32one_EC_PublicKey);
                        AV57signatureData.gxTpr_Finalcombination = AV15finalOneCombination;
                        AV55signatureContact.gxTpr_Data.Add(AV57signatureData, 0);
                     }
                     AV38p = (int)(AV38p+1);
                     AV83GXV4 = (int)(AV83GXV4+1);
                  }
                  if ( AV28numMinSignatures == 2 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_2);
                  }
                  else if ( AV28numMinSignatures == 3 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_3);
                  }
                  else if ( AV28numMinSignatures == 4 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_4);
                  }
                  else if ( AV28numMinSignatures == 5 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_5);
                  }
                  else if ( AV28numMinSignatures == 6 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_6);
                  }
                  else if ( AV28numMinSignatures == 7 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_7);
                  }
                  else if ( AV28numMinSignatures == 8 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_8);
                  }
                  else if ( AV28numMinSignatures == 9 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_9);
                  }
                  else if ( AV28numMinSignatures == 10 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_10);
                  }
                  else if ( AV28numMinSignatures == 11 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_11);
                  }
                  else if ( AV28numMinSignatures == 12 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_12);
                  }
                  else if ( AV28numMinSignatures == 13 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_13);
                  }
                  else if ( AV28numMinSignatures == 14 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_14);
                  }
                  else if ( AV28numMinSignatures == 15 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_15);
                  }
                  else if ( AV28numMinSignatures == 16 )
                  {
                     /* User Code */
                      ops.Add(NBitcoin.OpcodeType.OP_16);
                  }
                  else
                  {
                     AV11error = "The following number of signatures is not supported: " + StringUtil.Str( (decimal)(AV28numMinSignatures), 4, 0);
                  }
                  /* User Code */
                   ops.Add(NBitcoin.OpcodeType.OP_NUMEQUAL);
                  /* User Code */
                   i = AV19i;
                  /* User Code */
                   Scripts[i] = new NBitcoin.Script(ops).ToTapScript(NBitcoin.TapLeafVersion.C0);
                  /* User Code */
                   AllScripts.Add(Scripts[i]);
                  /* User Code */
                   scriptWeightsList.Add((probability, Scripts[i]));
                  AV19i = (int)(AV19i+1);
                  AV82GXV3 = (int)(AV82GXV3+1);
               }
               /* User Code */
                var scriptWeights = scriptWeightsList.ToArray();
               GXt_char1 = AV11error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV37ownerPublicKey,  AV24networkType,  AV9base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV53sequence), 10, 0)), out  AV13extPubKeyInfo, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV37ownerPublicKey = AV13extPubKeyInfo.gxTpr_Ec_publickey;
                  /* User Code */
                   var ownerKeyString = AV37ownerPublicKey;
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
                   AV31one_address = final_address;
                  AV65sdt_oneAddress.gxTpr_Address = AV31one_address;
                  AV65sdt_oneAddress.gxTpr_Generatedtype = AV16generatedType;
                  AV65sdt_oneAddress.gxTpr_Isused = false;
                  AV65sdt_oneAddress.gxTpr_Creationsequence = AV53sequence;
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV31one_address), StringUtil.Trim( AV33oneAddressHistory.gxTpr_Receivedaddress)) != 0 )
                  {
                     AV11error = "Generated address and UTXO address don't match";
                     if (true) break;
                  }
               }
               GXt_char1 = AV39privateKey;
               new GeneXus.Programs.wallet.getprivatekeyfromaddresshistory(context ).execute(  AV33oneAddressHistory, out  GXt_char1) ;
               AV39privateKey = GXt_char1;
               /* User Code */
                string hexPrivateKey = AV39privateKey;
               /* User Code */
                byte[] Keybytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
               /* User Code */
               	privateKey = new NBitcoin.Key(Keybytes);
               /* User Code */
                all_keys.Add(privateKey);
               /* User Code */
                var ec_pkey = NBitcoin.Secp256k1.ECPrivKey.Create(Keybytes);
               /* User Code */
                var ec_xonly = ec_pkey.CreatePubKey().ToXOnlyPubKey();
               /* User Code */
                all_XOnly.Add(ec_xonly);
               AV43receivedTransactionHex = AV33oneAddressHistory.gxTpr_Receivedtransactionhex;
               /* User Code */
                tx_hex = AV43receivedTransactionHex;
               /* User Code */
                tx = NBitcoin.Transaction.Parse(tx_hex, network);
               AV42receivedIn = 0;
               /* User Code */
                foreach (var output in tx.Outputs.AsIndexedOutputs())
               /* User Code */
                	{
               if ( AV42receivedIn == AV33oneAddressHistory.gxTpr_Recivedn )
               {
                  /* User Code */
                   		spender.Inputs.Add(new NBitcoin.OutPoint(tx, output.N));
                  /* User Code */
                   		spentAllOutputsIn.Add(output.TxOut);
               }
               AV42receivedIn = (long)(AV42receivedIn+1);
               /* User Code */
                	}
               AV59totalInUTXOs = (decimal)(AV59totalInUTXOs+(AV33oneAddressHistory.gxTpr_Balance));
               AV56signatureContacts.Add(AV55signatureContact, 0);
               AV80GXV1 = (int)(AV80GXV1+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               if ( AV68estimateFeeOnly )
               {
                  AV78signatureContactFeeOnly.FromJSonString(AV55signatureContact.ToJSonString(false, true), null);
                  AV84GXV5 = 1;
                  while ( AV84GXV5 <= AV55signatureContact.gxTpr_Data.Count )
                  {
                     AV57signatureData = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV55signatureContact.gxTpr_Data.Item(AV84GXV5));
                     AV71scriptIndexes.Add(AV57signatureData.gxTpr_I, 0);
                     AV84GXV5 = (int)(AV84GXV5+1);
                  }
               }
               AV70conditionDoWhile = true;
               while ( AV70conditionDoWhile )
               {
                  if ( AV68estimateFeeOnly )
                  {
                     AV55signatureContact.FromJSonString(AV78signatureContactFeeOnly.ToJSonString(false, true), null);
                  }
                  /* User Code */
                   var destination = NBitcoin.BitcoinAddress.Create(AV51sendTo, network);
                  /* User Code */
                   var returnto = NBitcoin.BitcoinAddress.Create(AV46returnTo, network);
                  /* User Code */
                   NBitcoin.TxOut[] spentOutputsIn = spentAllOutputsIn.ToArray();
                  if ( AV68estimateFeeOnly )
                  {
                     if ( ! AV79outputsAdded )
                     {
                        if ( AV49sendAllCoins )
                        {
                           AV52sendTotal = (decimal)(AV59totalInUTXOs-AV60transactionFee);
                           /* User Code */
                            spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV52sendTotal), destination);
                        }
                        else
                        {
                           AV47returnTotal = (decimal)(AV59totalInUTXOs-AV50sendCoins-AV60transactionFee);
                           /* User Code */
                            spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV50sendCoins), destination);
                           /* User Code */
                            spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV47returnTotal), returnto);
                        }
                        AV79outputsAdded = true;
                     }
                  }
                  else
                  {
                     if ( AV49sendAllCoins )
                     {
                        AV52sendTotal = (decimal)(AV59totalInUTXOs-AV60transactionFee);
                        /* User Code */
                         spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV52sendTotal), destination);
                     }
                     else
                     {
                        AV47returnTotal = (decimal)(AV59totalInUTXOs-AV50sendCoins-AV60transactionFee);
                        /* User Code */
                         spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV50sendCoins), destination);
                        /* User Code */
                         spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV47returnTotal), returnto);
                     }
                  }
                  /* User Code */
                   var sighash = NBitcoin.TaprootSigHash.All | NBitcoin.TaprootSigHash.AnyoneCanPay;
                  /* User Code */
                   var allTreeInfoArray = AllTreeInfo.ToArray();
                  /* User Code */
                   var allkeysarray = all_keys.ToArray();
                  /* User Code */
                   var allXonlyPubKeys = all_XOnly.ToArray();
                  /* User Code */
                   var allScriptsArray = AllScripts.ToArray();
                  /* User Code */
                   int s;
                  /* User Code */
                   int t;
                  if ( AV17group_sdt.gxTpr_Amigroupowner )
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
                     AV19i = 0;
                     AV85GXV6 = 1;
                     while ( AV85GXV6 <= AV61transactionsToSend.Count )
                     {
                        AV33oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV61transactionsToSend.Item(AV85GXV6));
                        /* User Code */
                         var i = AV19i;
                        AV86GXV7 = 1;
                        while ( AV86GXV7 <= AV56signatureContacts.Count )
                        {
                           AV55signatureContact = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData)AV56signatureContacts.Item(AV86GXV7));
                           AV87GXV8 = 1;
                           while ( AV87GXV8 <= AV55signatureContact.gxTpr_Data.Count )
                           {
                              AV57signatureData = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV55signatureContact.gxTpr_Data.Item(AV87GXV8));
                              if ( ( StringUtil.StrCmp(AV57signatureData.gxTpr_Transactionid, AV33oneAddressHistory.gxTpr_Receivedtransactionid) == 0 ) && ( AV57signatureData.gxTpr_N == AV33oneAddressHistory.gxTpr_Recivedn ) )
                              {
                                 AV48s = AV57signatureData.gxTpr_I;
                                 AV66t = AV57signatureData.gxTpr_P;
                                 /* User Code */
                                  s = AV48s;
                                 /* User Code */
                                  t = AV66t;
                                 /* User Code */
                                  var extectionDataScriptSpend = new NBitcoin.TaprootExecutionData(i, allScriptsArray[s].LeafHash) { SigHash = sighash };
                                 /* User Code */
                                  var hashScriptSpend = spender.GetSignatureHashTaproot(spentOutputsIn, extectionDataScriptSpend);
                                 /* User Code */
                                  var sig = allkeysarray[i].SignTaprootScriptSpend(hashScriptSpend, sighash);
                                 /* User Code */
                                  var oneXonly = allXonlyPubKeys[i];
                                 /* User Code */
                                  AV76oneXonlyPubKey = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(oneXonly.ToBytes());
                                 /* User Code */
                                  AV75oneSignatureTxt = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(sig.ToBytes());
                                 if ( AV68estimateFeeOnly )
                                 {
                                    GXt_char1 = AV75oneSignatureTxt;
                                    new GeneXus.Programs.wallet.registered.getdummysignature(context ).execute( out  GXt_char1) ;
                                    AV75oneSignatureTxt = GXt_char1;
                                 }
                                 AV36oneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
                                 AV36oneSignature.gxTpr_Signature = StringUtil.Trim( AV75oneSignatureTxt);
                                 AV36oneSignature.gxTpr_Xonlypubkey = AV76oneXonlyPubKey;
                                 AV57signatureData.gxTpr_Signatures.Add(AV36oneSignature, 0);
                                 if ( AV68estimateFeeOnly )
                                 {
                                    AV69numDumySignatures = 1;
                                    while ( AV69numDumySignatures < AV28numMinSignatures )
                                    {
                                       AV57signatureData.gxTpr_Signatures.Add(AV36oneSignature, 0);
                                       AV69numDumySignatures = (short)(AV69numDumySignatures+1);
                                    }
                                 }
                                 if ( AV68estimateFeeOnly || ( AV33oneAddressHistory.gxTpr_Multisignaturedata.Count > 0 ) )
                                 {
                                    if ( ( AV68estimateFeeOnly && ( AV57signatureData.gxTpr_I == AV71scriptIndexes.GetNumeric(1) ) ) || new GeneXus.Programs.wallet.registered.iscombinationsneededforsignature(context).executeUdp(  AV33oneAddressHistory,  AV57signatureData.gxTpr_Finalcombination, out  AV45returnMultiSigSignatureData) )
                                    {
                                       if ( ! ( AV28numMinSignatures == AV57signatureData.gxTpr_Finalcombination.gxTpr_Items.Count ) )
                                       {
                                          AV11error = "Combination does not match amount of Signatures";
                                          cleanup();
                                          if (true) return;
                                       }
                                       else
                                       {
                                          AV88GXV9 = 1;
                                          while ( AV88GXV9 <= AV45returnMultiSigSignatureData.gxTpr_Data.Count )
                                          {
                                             AV44returnDataItem = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV45returnMultiSigSignatureData.gxTpr_Data.Item(AV88GXV9));
                                             AV89GXV10 = 1;
                                             while ( AV89GXV10 <= AV44returnDataItem.gxTpr_Signatures.Count )
                                             {
                                                AV36oneSignature = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem)AV44returnDataItem.gxTpr_Signatures.Item(AV89GXV10));
                                                AV57signatureData.gxTpr_Signatures.Add(AV36oneSignature, 0);
                                                AV89GXV10 = (int)(AV89GXV10+1);
                                             }
                                             AV88GXV9 = (int)(AV88GXV9+1);
                                          }
                                          AV27numberSignatures = (short)(AV57signatureData.gxTpr_Signatures.Count);
                                          /* User Code */
                                           var numberSignatures = AV27numberSignatures;
                                          if ( ( AV27numberSignatures == AV28numMinSignatures ) || AV68estimateFeeOnly )
                                          {
                                             /* User Code */
                                              ops.Clear();
                                             /* User Code */
                                              var strScript = allScriptsArray[s].Script.ToString();
                                             /* User Code */
                                              AV67strScript = strScript;
                                             new GeneXus.Programs.wallet.registered.ordersignaturestomatchscript(context ).execute(  AV68estimateFeeOnly,  AV67strScript,  AV57signatureData, out  AV77orderedSignatureData) ;
                                             /* User Code */
                                              var countSignatures = 0;
                                             /* User Code */
                                              string sigString;
                                             /* User Code */
                                              var sigs = new NBitcoin.TaprootSignature[numberSignatures];
                                             AV90GXV11 = 1;
                                             while ( AV90GXV11 <= AV77orderedSignatureData.gxTpr_Signatures.Count )
                                             {
                                                AV36oneSignature = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem)AV77orderedSignatureData.gxTpr_Signatures.Item(AV90GXV11));
                                                AV75oneSignatureTxt = AV36oneSignature.gxTpr_Signature;
                                                /* User Code */
                                                 sigString = AV75oneSignatureTxt;
                                                /* User Code */
                                                 sigs[countSignatures] = NBitcoin.TaprootSignature.Parse(sigString);
                                                /* User Code */
                                                 countSignatures += 1;
                                                AV90GXV11 = (int)(AV90GXV11+1);
                                             }
                                             /* User Code */
                                              for (var r = numberSignatures-1; r >= 0; r--)
                                             /* User Code */
                                              {
                                             /* User Code */
                                              ops.Add(NBitcoin.Op.GetPushOp(sigs[r].ToBytes()));
                                             /* User Code */
                                              }
                                             /* User Code */
                                              ops.Add(NBitcoin.Op.GetPushOp(allScriptsArray[s].Script.ToBytes()));
                                             /* User Code */
                                              ops.Add(NBitcoin.Op.GetPushOp(allTreeInfoArray[i].GetControlBlock(allScriptsArray[s]).ToBytes()));
                                             /* User Code */
                                              spender.Inputs[i].WitScript = new NBitcoin.WitScript(ops.ToArray());
                                          }
                                       }
                                    }
                                 }
                              }
                              AV87GXV8 = (int)(AV87GXV8+1);
                           }
                           AV86GXV7 = (int)(AV86GXV7+1);
                        }
                        AV19i = (int)(AV19i+1);
                        AV85GXV6 = (int)(AV85GXV6+1);
                     }
                  }
                  /* User Code */
                   var validator = spender.CreateValidator(spentOutputsIn);
                  /* User Code */
                   var result = validator.ValidateInput(0);
                  /* User Code */
                   var success = result.Error is null;
                  /* User Code */
                   AV62verified = success;
                  /* User Code */
                   AV63virtualSize = spender.GetVirtualSize();
                  /* User Code */
                   AV18hexTransaction = spender.ToHex();
                  if ( AV17group_sdt.gxTpr_Amigroupowner )
                  {
                     if ( ! AV62verified )
                     {
                        AV11error = "Transaction is not Verified";
                     }
                  }
                  else
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        if ( ! AV68estimateFeeOnly )
                        {
                           new GeneXus.Programs.wallet.addmusigdatatohistory(context ).execute(  AV56signatureContacts, ref  AV61transactionsToSend) ;
                        }
                     }
                     else
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
                  if ( ! AV68estimateFeeOnly )
                  {
                     AV70conditionDoWhile = false;
                  }
                  else
                  {
                     AV72virtualSizes.Add(AV63virtualSize, 0);
                     AV71scriptIndexes.RemoveItem(1);
                     if ( AV71scriptIndexes.Count == 0 )
                     {
                        AV70conditionDoWhile = false;
                        AV72virtualSizes.Sort("");
                        AV63virtualSize = (long)(AV72virtualSizes.GetNumeric(AV72virtualSizes.Count));
                     }
                     else
                     {
                        AV70conditionDoWhile = true;
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
         		AV11error = ex.Message.ToString();
         /* User Code */
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
         AV18hexTransaction = "";
         AV11error = "";
         AV56signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
         AV33oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV9base_char = "";
         AV23items = new GxSimpleCollection<string>();
         AV55signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV34oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV12extendedPublicKey = "";
         AV37ownerPublicKey = "";
         AV25notOwnerMyExtendePubKey = "";
         AV13extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV32one_EC_PublicKey = "";
         AV10currentCombination = new GxSimpleCollection<string>();
         AV14finalCombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "distributedcryptography");
         AV8all_EC_PublicKey = new GxSimpleCollection<string>();
         GXt_objcol_char2 = new GxSimpleCollection<string>();
         AV15finalOneCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV35oneItem = "";
         AV57signatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV31one_address = "";
         AV65sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV39privateKey = "";
         AV43receivedTransactionHex = "";
         AV78signatureContactFeeOnly = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV71scriptIndexes = new GxSimpleCollection<int>();
         AV76oneXonlyPubKey = "";
         AV75oneSignatureTxt = "";
         GXt_char1 = "";
         AV36oneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
         AV45returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV44returnDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV67strScript = "";
         AV77orderedSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV72virtualSizes = new GxSimpleCollection<long>();
         /* GeneXus formulas. */
      }

      private short AV16generatedType ;
      private short AV30numPeers ;
      private short AV28numMinSignatures ;
      private short AV29numOfScripts ;
      private short AV69numDumySignatures ;
      private short AV27numberSignatures ;
      private int AV80GXV1 ;
      private int AV81GXV2 ;
      private int AV19i ;
      private int AV82GXV3 ;
      private int AV38p ;
      private int AV83GXV4 ;
      private int AV84GXV5 ;
      private int AV85GXV6 ;
      private int AV86GXV7 ;
      private int AV87GXV8 ;
      private int AV48s ;
      private int AV66t ;
      private int AV88GXV9 ;
      private int AV89GXV10 ;
      private int AV90GXV11 ;
      private long AV63virtualSize ;
      private long AV53sequence ;
      private long AV42receivedIn ;
      private decimal AV60transactionFee ;
      private decimal AV20inSendCoins ;
      private decimal AV50sendCoins ;
      private decimal AV59totalInUTXOs ;
      private decimal AV52sendTotal ;
      private decimal AV47returnTotal ;
      private string AV24networkType ;
      private string AV51sendTo ;
      private string AV46returnTo ;
      private string AV11error ;
      private string AV9base_char ;
      private string AV12extendedPublicKey ;
      private string AV37ownerPublicKey ;
      private string AV25notOwnerMyExtendePubKey ;
      private string AV32one_EC_PublicKey ;
      private string AV35oneItem ;
      private string AV31one_address ;
      private string AV39privateKey ;
      private string AV76oneXonlyPubKey ;
      private string AV75oneSignatureTxt ;
      private string GXt_char1 ;
      private bool AV68estimateFeeOnly ;
      private bool AV49sendAllCoins ;
      private bool AV62verified ;
      private bool AV21isCombNeeded ;
      private bool GXt_boolean3 ;
      private bool AV70conditionDoWhile ;
      private bool AV79outputsAdded ;
      private string AV18hexTransaction ;
      private string AV43receivedTransactionHex ;
      private string AV67strScript ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV61transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP8_transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> AV56signatureContacts ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV33oneAddressHistory ;
      private GxSimpleCollection<string> AV23items ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV55signatureContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV34oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV13extPubKeyInfo ;
      private GxSimpleCollection<string> AV10currentCombination ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> AV14finalCombinations ;
      private GxSimpleCollection<string> AV8all_EC_PublicKey ;
      private GxSimpleCollection<string> GXt_objcol_char2 ;
      private GeneXus.Programs.math.SdtfinalCombination AV15finalOneCombination ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV57signatureData ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV65sdt_oneAddress ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV78signatureContactFeeOnly ;
      private GxSimpleCollection<int> AV71scriptIndexes ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem AV36oneSignature ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV45returnMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV44returnDataItem ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV77orderedSignatureData ;
      private GxSimpleCollection<long> AV72virtualSizes ;
      private long aP9_virtualSize ;
      private string aP10_hexTransaction ;
      private bool aP11_verified ;
      private string aP12_error ;
   }

}
