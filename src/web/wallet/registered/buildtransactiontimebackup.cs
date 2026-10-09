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
         this.AV19group_sdt = aP0_group_sdt;
         this.AV66transactionFee = aP1_transactionFee;
         this.AV25networkType = aP2_networkType;
         this.AV22inSendCoins = aP3_inSendCoins;
         this.AV56sendTo = aP4_sendTo;
         this.AV67transactionsToSend = aP5_transactionsToSend;
         this.AV69virtualSize = 0 ;
         this.AV20hexTransaction = "" ;
         this.AV68verified = false ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP5_transactionsToSend=this.AV67transactionsToSend;
         aP6_virtualSize=this.AV69virtualSize;
         aP7_hexTransaction=this.AV20hexTransaction;
         aP8_verified=this.AV68verified;
         aP9_error=this.AV12error;
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
         return AV12error ;
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
         this.AV19group_sdt = aP0_group_sdt;
         this.AV66transactionFee = aP1_transactionFee;
         this.AV25networkType = aP2_networkType;
         this.AV22inSendCoins = aP3_inSendCoins;
         this.AV56sendTo = aP4_sendTo;
         this.AV67transactionsToSend = aP5_transactionsToSend;
         this.AV69virtualSize = 0 ;
         this.AV20hexTransaction = "" ;
         this.AV68verified = false ;
         this.AV12error = "" ;
         SubmitImpl();
         aP5_transactionsToSend=this.AV67transactionsToSend;
         aP6_virtualSize=this.AV69virtualSize;
         aP7_hexTransaction=this.AV20hexTransaction;
         aP8_verified=this.AV68verified;
         aP9_error=this.AV12error;
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
         AV55sendCoins = (decimal)(AV22inSendCoins-AV66transactionFee);
         GXt_SdtExternalUser1 = AV72externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV72externalUser = GXt_SdtExternalUser1;
         if ( ! AV19group_sdt.gxTpr_Amigroupowner )
         {
            GXt_char2 = AV12error;
            new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV19group_sdt.gxTpr_Encryptedtextshare,  AV19group_sdt.gxTpr_Encpassword,  AV72externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV71secret, out  GXt_char2) ;
            AV12error = GXt_char2;
         }
         /* User Code */
          try
         /* User Code */
          {
         if ( StringUtil.StrCmp(AV25networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV25networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV25networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV12error = "Network Type not sopported";
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            /* User Code */
             var spender = network.CreateTransaction();
            AV61signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
            AV108GXV1 = 1;
            while ( AV108GXV1 <= AV67transactionsToSend.Count )
            {
               AV34oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV67transactionsToSend.Item(AV108GXV1));
               AV18generatedType = AV34oneAddressHistory.gxTpr_Addressgeneratedtype;
               AV58sequence = AV34oneAddressHistory.gxTpr_Addresscreationsequence;
               AV31numPeers = (short)(AV19group_sdt.gxTpr_Contact.Count);
               AV109GXV2 = 1;
               while ( AV109GXV2 <= AV19group_sdt.gxTpr_Timeconstrain.Count )
               {
                  AV73oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV19group_sdt.gxTpr_Timeconstrain.Item(AV109GXV2));
                  if ( AV73oneTimeConstrain.gxTpr_Sequence == AV58sequence )
                  {
                     AV74bountyRestoreDate = AV73oneTimeConstrain.gxTpr_Date;
                     GXt_int3 = AV83bountyResotreUnixTime;
                     GXt_dtime4 = DateTimeUtil.ResetTime( AV74bountyRestoreDate ) ;
                     new GeneXus.Programs.distributedcrypto.datetimetounixtime(context ).execute(  GXt_dtime4, out  GXt_int3) ;
                     AV83bountyResotreUnixTime = GXt_int3;
                     if ( AV19group_sdt.gxTpr_Amigroupowner )
                     {
                        GXt_char2 = AV12error;
                        new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV73oneTimeConstrain.gxTpr_Encryptedsecret,  AV73oneTimeConstrain.gxTpr_Encryptedkey,  AV72externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV71secret, out  GXt_char2) ;
                        AV12error = GXt_char2;
                     }
                     if (true) break;
                  }
                  AV109GXV2 = (int)(AV109GXV2+1);
               }
               AV24items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               AV110GXV3 = 1;
               while ( AV110GXV3 <= AV19group_sdt.gxTpr_Contact.Count )
               {
                  AV35oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV19group_sdt.gxTpr_Contact.Item(AV110GXV3));
                  if ( ( AV19group_sdt.gxTpr_Grouptype == 20 ) && ! ( AV35oneContact.gxTpr_Contactid == AV35oneContact.gxTpr_Contactgroupid ) )
                  {
                     if ( (Convert.ToDecimal( AV18generatedType ) == NumberUtil.Val( "4", ".") ) )
                     {
                        AV14extendedPublicKey = AV35oneContact.gxTpr_Extpubkeytimebountyreceiving;
                        AV42ownerPublicKey = AV19group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving;
                     }
                     else
                     {
                        AV12error = "The generated Type is not a TimeBountyReceiving Type";
                        cleanup();
                        if (true) return;
                     }
                     GXt_char2 = AV12error;
                     new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV14extendedPublicKey,  AV25networkType,  AV9base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV58sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char2) ;
                     AV12error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        AV24items.Add(StringUtil.Trim( AV15extPubKeyInfo.gxTpr_Ec_publickey), 0);
                        if ( ! AV19group_sdt.gxTpr_Amigroupowner )
                        {
                           if ( StringUtil.StrCmp(StringUtil.Trim( AV35oneContact.gxTpr_Contactusername), StringUtil.Trim( AV72externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 )
                           {
                              AV107my_EC_PublicKey = StringUtil.Trim( AV15extPubKeyInfo.gxTpr_Ec_publickey);
                           }
                        }
                     }
                     else
                     {
                        if (true) break;
                     }
                  }
                  AV110GXV3 = (int)(AV110GXV3+1);
               }
               GXt_char2 = AV12error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV42ownerPublicKey,  AV25networkType,  AV9base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV58sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char2) ;
               AV12error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV42ownerPublicKey = AV15extPubKeyInfo.gxTpr_Ec_publickey;
               }
               else
               {
                  cleanup();
                  if (true) return;
               }
               AV24items.Sort("");
               AV30numOfScripts = (short)(AV24items.Count);
               /* User Code */
                var howManyScripts = AV30numOfScripts;
               /* User Code */
                var Scripts = new NBitcoin.TapScript[howManyScripts];
               /* User Code */
                string pubKeyString;
               /* User Code */
                int i;
               /* User Code */
                var secret = Convert.FromBase64String(AV71secret);
               /* User Code */
                var secret_sha256 = NBitcoin.Crypto.Hashes.DoubleSHA256(secret);
               /* User Code */
                NBitcoin.LockTime target = (int) AV83bountyResotreUnixTime;
               /* User Code */
                var scriptWeightsList = new System.Collections.Generic.List<(UInt32, NBitcoin.TapScript)>();
               /* User Code */
                var probability = (uint)(100 / howManyScripts);
               AV21i = 0;
               AV111GXV4 = 1;
               while ( AV111GXV4 <= AV24items.Count )
               {
                  AV36oneItem = AV24items.GetString(AV111GXV4);
                  /* User Code */
                   ops.Clear();
                  /* User Code */
                   pubKeyString = AV36oneItem;
                  if ( ! AV19group_sdt.gxTpr_Amigroupowner )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV107my_EC_PublicKey), StringUtil.Trim( AV36oneItem)) == 0 )
                     {
                        AV51s = AV21i;
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
                   i = AV21i;
                  /* User Code */
                   Scripts[i] = new NBitcoin.Script(ops).ToTapScript(NBitcoin.TapLeafVersion.C0);
                  /* User Code */
                   AllScripts.Add(Scripts[i]);
                  /* User Code */
                   scriptWeightsList.Add((probability, Scripts[i]));
                  AV21i = (int)(AV21i+1);
                  AV111GXV4 = (int)(AV111GXV4+1);
               }
               /* User Code */
                var scriptWeights = scriptWeightsList.ToArray();
               /* User Code */
                var ownerKeyString = AV42ownerPublicKey;
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
               GXt_char2 = AV44privateKey;
               new GeneXus.Programs.wallet.getprivatekeyfromaddresshistory(context ).execute(  AV34oneAddressHistory, out  GXt_char2) ;
               AV44privateKey = GXt_char2;
               /* User Code */
                string hexPrivateKey = AV44privateKey;
               /* User Code */
                byte[] Keybytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
               /* User Code */
               	privateKey = new NBitcoin.Key(Keybytes);
               /* User Code */
                all_keys.Add(privateKey);
               AV46receivedTransactionHex = AV34oneAddressHistory.gxTpr_Receivedtransactionhex;
               /* User Code */
                tx_hex = AV46receivedTransactionHex;
               /* User Code */
                tx = NBitcoin.Transaction.Parse(tx_hex, network);
               AV45receivedIn = 0;
               /* User Code */
                foreach (var output in tx.Outputs.AsIndexedOutputs())
               /* User Code */
                	{
               if ( AV45receivedIn == AV34oneAddressHistory.gxTpr_Recivedn )
               {
                  /* User Code */
                   		spender.Inputs.Add(new NBitcoin.OutPoint(tx, output.N));
                  /* User Code */
                   		spentAllOutputsIn.Add(output.TxOut);
               }
               AV45receivedIn = (long)(AV45receivedIn+1);
               /* User Code */
                	}
               AV65totalInUTXOs = (decimal)(AV65totalInUTXOs+(AV34oneAddressHistory.gxTpr_Balance));
               AV61signatureContacts.Add(AV59signatureContact, 0);
               AV108GXV1 = (int)(AV108GXV1+1);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               /* User Code */
                var destination = NBitcoin.BitcoinAddress.Create(AV56sendTo, network);
               /* User Code */
                NBitcoin.TxOut[] spentOutputsIn = spentAllOutputsIn.ToArray();
               AV57sendTotal = (decimal)(AV65totalInUTXOs-AV66transactionFee);
               /* User Code */
                spender.Outputs.Add(NBitcoin.Money.Coins((decimal)AV57sendTotal), destination);
               /* User Code */
                var sighash = NBitcoin.TaprootSigHash.All | NBitcoin.TaprootSigHash.AnyoneCanPay;
               /* User Code */
                var allTreeInfoArray = AllTreeInfo.ToArray();
               /* User Code */
                var allkeysarray = all_keys.ToArray();
               /* User Code */
                var allScriptsArray = AllScripts.ToArray();
               if ( AV19group_sdt.gxTpr_Amigroupowner )
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
                   var s = AV51s;
                  /* User Code */
                   NBitcoin.LockTime target = (int) AV83bountyResotreUnixTime;
                  /* User Code */
                   spender.LockTime = target;
                  /* User Code */
                   var secret = Convert.FromBase64String(AV71secret);
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
                AV68verified = success;
               /* User Code */
                AV69virtualSize = spender.GetVirtualSize();
               /* User Code */
                AV20hexTransaction = spender.ToHex();
               if ( ! AV68verified )
               {
                  AV12error = "Transaction is not Verified";
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
         		AV12error = ex.Message.ToString();
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
         AV20hexTransaction = "";
         AV12error = "";
         AV72externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV71secret = "";
         AV61signatureContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData>( context, "MultiSigSignatureData", "distributedcryptography");
         AV34oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV73oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV74bountyRestoreDate = DateTime.MinValue;
         GXt_dtime4 = (DateTime)(DateTime.MinValue);
         AV24items = new GxSimpleCollection<string>();
         AV35oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14extendedPublicKey = "";
         AV42ownerPublicKey = "";
         AV9base_char = "";
         AV15extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV107my_EC_PublicKey = "";
         AV36oneItem = "";
         AV44privateKey = "";
         GXt_char2 = "";
         AV46receivedTransactionHex = "";
         AV59signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         /* GeneXus formulas. */
      }

      private short AV18generatedType ;
      private short AV31numPeers ;
      private short AV30numOfScripts ;
      private int AV108GXV1 ;
      private int AV109GXV2 ;
      private int AV110GXV3 ;
      private int AV21i ;
      private int AV111GXV4 ;
      private int AV51s ;
      private long AV69virtualSize ;
      private long AV58sequence ;
      private long AV83bountyResotreUnixTime ;
      private long GXt_int3 ;
      private long AV45receivedIn ;
      private decimal AV66transactionFee ;
      private decimal AV22inSendCoins ;
      private decimal AV55sendCoins ;
      private decimal AV65totalInUTXOs ;
      private decimal AV57sendTotal ;
      private string AV25networkType ;
      private string AV56sendTo ;
      private string AV12error ;
      private string AV71secret ;
      private string AV14extendedPublicKey ;
      private string AV42ownerPublicKey ;
      private string AV9base_char ;
      private string AV107my_EC_PublicKey ;
      private string AV36oneItem ;
      private string AV44privateKey ;
      private string GXt_char2 ;
      private DateTime GXt_dtime4 ;
      private DateTime AV74bountyRestoreDate ;
      private bool AV68verified ;
      private string AV20hexTransaction ;
      private string AV46receivedTransactionHex ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV67transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP5_transactionsToSend ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV72externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> AV61signatureContacts ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV34oneAddressHistory ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV73oneTimeConstrain ;
      private GxSimpleCollection<string> AV24items ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV35oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV15extPubKeyInfo ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV59signatureContact ;
      private long aP6_virtualSize ;
      private string aP7_hexTransaction ;
      private bool aP8_verified ;
      private string aP9_error ;
   }

}
