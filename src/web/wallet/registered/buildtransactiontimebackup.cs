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
   public class buildtransactiontimebackup : GXProcedure
   {
      public buildtransactiontimebackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildtransactiontimebackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           decimal aP1_transactionFee ,
                           string aP2_networkType ,
                           decimal aP3_inSendCoins ,
                           string aP4_sendTo ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                           out long aP6_virtualSize ,
                           out string aP7_hexTransaction ,
                           out bool aP8_verified ,
                           out string aP9_error )
      {
         this.AV17group_sdt = aP0_group_sdt;
         this.AV43transactionFee = aP1_transactionFee;
         this.AV23networkType = aP2_networkType;
         this.AV20inSendCoins = aP3_inSendCoins;
         this.AV37sendTo = aP4_sendTo;
         this.AV44transactionsToSend = aP5_transactionsToSend;
         this.AV46virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV45verified = false ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP5_transactionsToSend=this.AV44transactionsToSend;
         aP6_virtualSize=this.AV46virtualSize;
         aP7_hexTransaction=this.AV18hexTransaction;
         aP8_verified=this.AV45verified;
         aP9_error=this.AV11error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                decimal aP1_transactionFee ,
                                string aP2_networkType ,
                                decimal aP3_inSendCoins ,
                                string aP4_sendTo ,
                                ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                                out long aP6_virtualSize ,
                                out string aP7_hexTransaction ,
                                out bool aP8_verified )
      {
         execute(aP0_group_sdt, aP1_transactionFee, aP2_networkType, aP3_inSendCoins, aP4_sendTo, ref aP5_transactionsToSend, out aP6_virtualSize, out aP7_hexTransaction, out aP8_verified, out aP9_error);
         return AV11error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 decimal aP1_transactionFee ,
                                 string aP2_networkType ,
                                 decimal aP3_inSendCoins ,
                                 string aP4_sendTo ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ,
                                 out long aP6_virtualSize ,
                                 out string aP7_hexTransaction ,
                                 out bool aP8_verified ,
                                 out string aP9_error )
      {
         this.AV17group_sdt = aP0_group_sdt;
         this.AV43transactionFee = aP1_transactionFee;
         this.AV23networkType = aP2_networkType;
         this.AV20inSendCoins = aP3_inSendCoins;
         this.AV37sendTo = aP4_sendTo;
         this.AV44transactionsToSend = aP5_transactionsToSend;
         this.AV46virtualSize = 0 ;
         this.AV18hexTransaction = "" ;
         this.AV45verified = false ;
         this.AV11error = "" ;
         SubmitImpl();
         aP5_transactionsToSend=this.AV44transactionsToSend;
         aP6_virtualSize=this.AV46virtualSize;
         aP7_hexTransaction=this.AV18hexTransaction;
         aP8_verified=this.AV45verified;
         aP9_error=this.AV11error;
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
          var spentAllOutputsIn = new System.Collections.Generic.List<NBitcoin.TxOut>();
         /* User Code */
          var AllTreeInfo = new System.Collections.Generic.List<NBitcoin.TaprootSpendInfo>();
         /* User Code */
          var AllScripts = new System.Collections.Generic.List<NBitcoin.TapScript>();
         /* User Code */
          System.Collections.Generic.List<NBitcoin.Op> ops = new System.Collections.Generic.List<NBitcoin.Op>();
         AV36sendCoins = (decimal)(AV20inSendCoins-AV43transactionFee);
         GXt_SdtExternalUser1 = AV14externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV14externalUser = GXt_SdtExternalUser1;
         if ( ! AV17group_sdt.gxTpr_Amigroupowner )
         {
            GXt_char2 = AV11error;
            new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV17group_sdt.gxTpr_Encryptedtextshare,  AV17group_sdt.gxTpr_Encpassword,  AV14externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV35secret, out  GXt_char2) ;
            AV11error = GXt_char2;
         }
         /* User Code */
          try
         /* User Code */
          {
         if ( StringUtil.StrCmp(AV23networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV23networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV23networkType, "RegTest") == 0 )
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
            AV41signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
            AV47GXV1 = 1;
            while ( AV47GXV1 <= AV44transactionsToSend.Count )
            {
               AV26oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV44transactionsToSend.Item(AV47GXV1));
               AV16generatedType = AV26oneAddressHistory.gxTpr_Addressgeneratedtype;
               AV39sequence = AV26oneAddressHistory.gxTpr_Addresscreationsequence;
               AV25numPeers = (short)(AV17group_sdt.gxTpr_Contact.Count);
               AV48GXV2 = 1;
               while ( AV48GXV2 <= AV17group_sdt.gxTpr_Timeconstrain.Count )
               {
                  AV29oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV17group_sdt.gxTpr_Timeconstrain.Item(AV48GXV2));
                  if ( AV29oneTimeConstrain.gxTpr_Sequence == AV39sequence )
                  {
                     AV10bountyRestoreDate = AV29oneTimeConstrain.gxTpr_Date;
                     GXt_int3 = AV9bountyResotreUnixTime;
                     GXt_dtime4 = DateTimeUtil.ResetTime( AV10bountyRestoreDate ) ;
                     new GeneXus.Programs.distributedcrypto.datetimetounixtime(context ).execute(  GXt_dtime4, out  GXt_int3) ;
                     AV9bountyResotreUnixTime = GXt_int3;
                     if ( AV17group_sdt.gxTpr_Amigroupowner )
                     {
                        GXt_char2 = AV11error;
                        new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV29oneTimeConstrain.gxTpr_Encryptedsecret,  AV29oneTimeConstrain.gxTpr_Encryptedkey,  AV14externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV35secret, out  GXt_char2) ;
                        AV11error = GXt_char2;
                     }
                     if (true) break;
                  }
                  AV48GXV2 = (int)(AV48GXV2+1);
               }
               AV21items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               AV49GXV3 = 1;
               while ( AV49GXV3 <= AV17group_sdt.gxTpr_Contact.Count )
               {
                  AV27oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV49GXV3));
                  if ( ( AV17group_sdt.gxTpr_Grouptype == 20 ) && ! ( AV27oneContact.gxTpr_Contactid == AV27oneContact.gxTpr_Contactgroupid ) )
                  {
                     if ( (Convert.ToDecimal( AV16generatedType ) == NumberUtil.Val( "4", ".") ) )
                     {
                        AV13extendedPublicKey = AV27oneContact.gxTpr_Extpubkeytimebountyreceiving;
                        AV30ownerPublicKey = AV17group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving;
                     }
                     else
                     {
                        AV11error = "The generated Type is not a TimeBountyReceiving Type";
                        cleanup();
                        if (true) return;
                     }
                     GXt_char2 = AV11error;
                     new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV13extendedPublicKey,  AV23networkType,  AV8base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV39sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char2) ;
                     AV11error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        AV21items.Add(StringUtil.Trim( AV15extPubKeyInfo.gxTpr_Ec_publickey), 0);
                        if ( ! AV17group_sdt.gxTpr_Amigroupowner )
                        {
                           if ( StringUtil.StrCmp(StringUtil.Trim( AV27oneContact.gxTpr_Contactusername), StringUtil.Trim( AV14externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 )
                           {
                              AV22my_EC_PublicKey = StringUtil.Trim( AV15extPubKeyInfo.gxTpr_Ec_publickey);
                           }
                        }
                     }
                     else
                     {
                        if (true) break;
                     }
                  }
                  AV49GXV3 = (int)(AV49GXV3+1);
               }
               GXt_char2 = AV11error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV30ownerPublicKey,  AV23networkType,  AV8base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV39sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char2) ;
               AV11error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV30ownerPublicKey = AV15extPubKeyInfo.gxTpr_Ec_publickey;
               }
               else
               {
                  cleanup();
                  if (true) return;
               }
               AV21items.Sort("");
               AV24numOfScripts = (short)(AV21items.Count);
               /* User Code */
                var howManyScripts = AV24numOfScripts;
               /* User Code */
                var Scripts = new NBitcoin.TapScript[howManyScripts];
               /* User Code */
                string pubKeyString;
               /* User Code */
                int i;
               /* User Code */
                var secret = Convert.FromBase64String(AV35secret);
               /* User Code */
                var secret_sha256 = NBitcoin.Crypto.Hashes.DoubleSHA256(secret);
               /* User Code */
                NBitcoin.LockTime target = (int) AV9bountyResotreUnixTime;
               /* User Code */
                var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
               /* User Code */
                var probability = (uint)(100 / howManyScripts);
               AV19i = 0;
               AV50GXV4 = 1;
               while ( AV50GXV4 <= AV21items.Count )
               {
                  AV28oneItem = AV21items.GetString(AV50GXV4);
                  /* User Code */
                   ops.Clear();
                  /* User Code */
                   pubKeyString = AV28oneItem;
                  if ( ! AV17group_sdt.gxTpr_Amigroupowner )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV22my_EC_PublicKey), StringUtil.Trim( AV28oneItem)) == 0 )
                     {
                        AV34s = AV19i;
                     }
                  }
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
                   AllScripts.Add(Scripts[i]);
                  /* User Code */
                   scriptWeightsList.Add((probability, Scripts[i]));
                  AV19i = (int)(AV19i+1);
                  AV50GXV4 = (int)(AV50GXV4+1);
               }
               /* User Code */
                var scriptWeights = scriptWeightsList.ToArray();
               /* User Code */
                var ownerKeyString = AV30ownerPublicKey;
               /* User Code */
                var ec_PubKey = NBitcoin.Secp256k1.ECPubKey.Create(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(ownerKeyString));
               /* User Code */
                var xOnlyFromPubkey = ec_PubKey.ToXOnlyPubKey();
               /* User Code */
                var tapIntFromEC = new NBitcoin.TaprootInternalPubKey(xOnlyFromPubkey.ToBytes());
               /* User Code */
                var treeInfo = NBitcoin.TaprootSpendInfo.WithHuffmanTree(tapIntFromEC, scriptWeights);
               /* User Code */
                AllTreeInfo.Add(treeInfo);
               GXt_char2 = AV31privateKey;
               new GeneXus.Programs.wallet.getprivatekeyfromaddresshistory(context ).execute(  AV26oneAddressHistory, out  GXt_char2) ;
               AV31privateKey = GXt_char2;
               /* User Code */
                string hexPrivateKey = AV31privateKey;
               /* User Code */
                byte[] Keybytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
               /* User Code */
               	privateKey = new NBitcoin.Key(Keybytes);
               /* User Code */
                all_keys.Add(privateKey);
               AV33receivedTransactionHex = AV26oneAddressHistory.gxTpr_Receivedtransactionhex;
               /* User Code */
                tx_hex = AV33receivedTransactionHex;
               /* User Code */
                tx = NBitcoin.Transaction.Parse(tx_hex, network);
               AV32receivedIn = 0;
               /* User Code */
                foreach (var output in tx.Outputs.AsIndexedOutputs())
               /* User Code */
                	{
               if ( AV32receivedIn == AV26oneAddressHistory.gxTpr_Recivedn )
               {
                  /* User Code */
                   		spender.Inputs.Add(new NBitcoin.OutPoint(tx, output.N));
                  /* User Code */
                   		spentAllOutputsIn.Add(output.TxOut);
               }
               AV32receivedIn = (long)(AV32receivedIn+1);
               /* User Code */
                	}
               AV42totalInUTXOs = (decimal)(AV42totalInUTXOs+(AV26oneAddressHistory.gxTpr_Balance));
               AV41signatureContacts.Add(AV40signatureContact, 0);
               AV47GXV1 = (int)(AV47GXV1+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               /* User Code */
                var destination = NBitcoin.BitcoinAddress.Create(AV37sendTo, network);
               /* User Code */
                NBitcoin.TxOut[] spentOutputsIn = spentAllOutputsIn.ToArray();
               AV38sendTotal = (decimal)(AV42totalInUTXOs-AV43transactionFee);
               /* User Code */
                spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV38sendTotal), destination);
               /* User Code */
                var sighash = NBitcoin.TaprootSigHash.All | NBitcoin.TaprootSigHash.AnyoneCanPay;
               /* User Code */
                var allTreeInfoArray = AllTreeInfo.ToArray();
               /* User Code */
                var allkeysarray = all_keys.ToArray();
               /* User Code */
                var allScriptsArray = AllScripts.ToArray();
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
                  /* User Code */
                   var s = AV34s;
                  /* User Code */
                   NBitcoin.LockTime target = (int) AV9bountyResotreUnixTime;
                  /* User Code */
                   spender.LockTime = target;
                  /* User Code */
                   var secret = Convert.FromBase64String(AV35secret);
                  /* User Code */
                   for (int i = 0; i < spender.Inputs.Count; i++)
                  /* User Code */
                   {
                  /* User Code */
                   spender.Inputs[i].Sequence = 1;
                  /* User Code */
                   var extectionDataScriptSpend = new NBitcoin.TaprootExecutionData(i, allScriptsArray[s].LeafHash) { SigHash = sighash };
                  /* User Code */
                   var hashScriptSpend = spender.GetSignatureHashTaproot(spentOutputsIn, extectionDataScriptSpend);
                  /* User Code */
                   var sig = allkeysarray[i].SignTaprootScriptSpend(hashScriptSpend, sighash);
                  /* User Code */
                   spender.Inputs[i].WitScript = new NBitcoin.WitScript(NBitcoin.Op.GetPushOp(sig.ToBytes()), NBitcoin.Op.GetPushOp(secret), NBitcoin.Op.GetPushOp(allScriptsArray[s].Script.ToBytes()), NBitcoin.Op.GetPushOp(allTreeInfoArray[i].GetControlBlock(allScriptsArray[s]).ToBytes()));
                  /* User Code */
                   }
               }
               /* User Code */
                var validator = spender.CreateValidator(spentOutputsIn);
               /* User Code */
                var result = validator.ValidateInput(0);
               /* User Code */
                var success = result.Error is null;
               /* User Code */
                AV45verified = success;
               /* User Code */
                AV46virtualSize = spender.GetVirtualSize();
               /* User Code */
                AV18hexTransaction = spender.ToHex();
               if ( ! AV45verified )
               {
                  AV11error = "Transaction is not Verified";
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
         AV14externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV35secret = "";
         AV41signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
         AV26oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV29oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV10bountyRestoreDate = DateTime.MinValue;
         GXt_dtime4 = (DateTime)(DateTime.MinValue);
         AV21items = new GxSimpleCollection<string>();
         AV27oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV13extendedPublicKey = "";
         AV30ownerPublicKey = "";
         AV8base_char = "";
         AV15extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV22my_EC_PublicKey = "";
         AV28oneItem = "";
         AV31privateKey = "";
         GXt_char2 = "";
         AV33receivedTransactionHex = "";
         AV40signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         /* GeneXus formulas. */
      }

      private short AV16generatedType ;
      private short AV25numPeers ;
      private short AV24numOfScripts ;
      private int AV47GXV1 ;
      private int AV48GXV2 ;
      private int AV49GXV3 ;
      private int AV19i ;
      private int AV50GXV4 ;
      private int AV34s ;
      private long AV46virtualSize ;
      private long AV39sequence ;
      private long AV9bountyResotreUnixTime ;
      private long GXt_int3 ;
      private long AV32receivedIn ;
      private decimal AV43transactionFee ;
      private decimal AV20inSendCoins ;
      private decimal AV36sendCoins ;
      private decimal AV42totalInUTXOs ;
      private decimal AV38sendTotal ;
      private string AV23networkType ;
      private string AV37sendTo ;
      private string AV11error ;
      private string AV35secret ;
      private string AV13extendedPublicKey ;
      private string AV30ownerPublicKey ;
      private string AV8base_char ;
      private string AV22my_EC_PublicKey ;
      private string AV28oneItem ;
      private string AV31privateKey ;
      private string GXt_char2 ;
      private DateTime GXt_dtime4 ;
      private DateTime AV10bountyRestoreDate ;
      private bool AV45verified ;
      private string AV18hexTransaction ;
      private string AV33receivedTransactionHex ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV44transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV14externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> AV41signatureContacts ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV26oneAddressHistory ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV29oneTimeConstrain ;
      private GxSimpleCollection<string> AV21items ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV27oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV15extPubKeyInfo ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV40signatureContact ;
      private long aP6_virtualSize ;
      private string aP7_hexTransaction ;
      private bool aP8_verified ;
      private string aP9_error ;
   }

}
