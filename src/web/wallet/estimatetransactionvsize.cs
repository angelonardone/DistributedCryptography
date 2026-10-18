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
namespace GeneXus.Programs.wallet {
   public class estimatetransactionvsize : GXProcedure
   {
      public estimatetransactionvsize( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public estimatetransactionvsize( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_sendAllCoins ,
                           string aP1_networkType ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                           string aP3_sendTo ,
                           string aP4_returnTo ,
                           out long aP5_virtualSize ,
                           out string aP6_error )
      {
         this.AV14sendAllCoins = aP0_sendAllCoins;
         this.AV10networkType = aP1_networkType;
         this.AV16transactionsToSend = aP2_transactionsToSend;
         this.AV15sendTo = aP3_sendTo;
         this.AV13returnTo = aP4_returnTo;
         this.AV17virtualSize = 0 ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP5_virtualSize=this.AV17virtualSize;
         aP6_error=this.AV8error;
      }

      public string executeUdp( bool aP0_sendAllCoins ,
                                string aP1_networkType ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                string aP3_sendTo ,
                                string aP4_returnTo ,
                                out long aP5_virtualSize )
      {
         execute(aP0_sendAllCoins, aP1_networkType, aP2_transactionsToSend, aP3_sendTo, aP4_returnTo, out aP5_virtualSize, out aP6_error);
         return AV8error ;
      }

      public void executeSubmit( bool aP0_sendAllCoins ,
                                 string aP1_networkType ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                 string aP3_sendTo ,
                                 string aP4_returnTo ,
                                 out long aP5_virtualSize ,
                                 out string aP6_error )
      {
         this.AV14sendAllCoins = aP0_sendAllCoins;
         this.AV10networkType = aP1_networkType;
         this.AV16transactionsToSend = aP2_transactionsToSend;
         this.AV15sendTo = aP3_sendTo;
         this.AV13returnTo = aP4_returnTo;
         this.AV17virtualSize = 0 ;
         this.AV8error = "" ;
         SubmitImpl();
         aP5_virtualSize=this.AV17virtualSize;
         aP6_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network = NBitcoin.Network.Main;
         /* User Code */
          int baseSize = 0;
         /* User Code */
          int witSize = 0;
         /* User Code */
          int nonWitnessInputs = 0;
         /* User Code */
          bool hasWitness = false;
         /* User Code */
          int nIn = 0;
         /* User Code */
          int nOut = 0;
         /* User Code */
          NBitcoin.Script firstInputScript = null;
         if ( StringUtil.StrCmp(AV10networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV10networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV10networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV8error = "Network Type not sopported";
            cleanup();
            if (true) return;
         }
         /* User Code */
          try
         /* User Code */
          {
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV16transactionsToSend.Count )
         {
            AV11oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV16transactionsToSend.Item(AV18GXV1));
            AV9hexTransaction = AV11oneAddressHistory.gxTpr_Receivedtransactionhex;
            AV12receivedN = AV11oneAddressHistory.gxTpr_Recivedn;
            /* User Code */
             var prevTx = NBitcoin.Transaction.Parse(AV9hexTransaction, network);
            /* User Code */
             var spk = prevTx.Outputs[(int)AV12receivedN].ScriptPubKey;
            /* User Code */
             if (firstInputScript == null) firstInputScript = spk;
            /* User Code */
             nIn++;
            /* User Code */
             baseSize += 41;
            /* User Code */
             if (NBitcoin.PayToTaprootTemplate.Instance.CheckScriptPubKey(spk)) { witSize += 1 + 1 + 65; hasWitness = true; }
            /* User Code */
             else if (NBitcoin.PayToWitPubKeyHashTemplate.Instance.CheckScriptPubKey(spk)) { witSize += 1 + 73 + 34; hasWitness = true; }
            /* User Code */
             else if (NBitcoin.PayToScriptHashTemplate.Instance.CheckScriptPubKey(spk)) { baseSize += 23; witSize += 1 + 73 + 34; hasWitness = true; }
            /* User Code */
             else if (NBitcoin.PayToPubkeyHashTemplate.Instance.CheckScriptPubKey(spk)) { baseSize += 107; nonWitnessInputs++; }
            /* User Code */
             else { AV8error = "A coin to spend has a script this wallet does not know: " + spk.ToString(); }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            /* User Code */
             var destination = NBitcoin.BitcoinAddress.Create(AV15sendTo.Trim(), network).ScriptPubKey;
            /* User Code */
             baseSize += 8 + 1 + destination.Length;
            /* User Code */
             nOut++;
            if ( ! AV14sendAllCoins )
            {
               /* User Code */
                var change = firstInputScript;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13returnTo)) )
               {
                  /* User Code */
                   change = NBitcoin.BitcoinAddress.Create(AV13returnTo.Trim(), network).ScriptPubKey;
               }
               /* User Code */
                if (change != null) { baseSize += 8 + 1 + change.Length; nOut++; }
            }
            /* User Code */
             baseSize += 4 + 4 + (nIn < 0xfd ? 1 : (nIn <= 0xffff ? 3 : 5)) + (nOut < 0xfd ? 1 : (nOut <= 0xffff ? 3 : 5));
            /* User Code */
             if (hasWitness) witSize += 2 + nonWitnessInputs;
            /* User Code */
             int weight = baseSize * 4 + witSize;
            /* User Code */
             AV17virtualSize = (weight + 3) / 4;
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
         AV8error = "";
         AV11oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV9hexTransaction = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private long AV17virtualSize ;
      private long AV12receivedN ;
      private string AV10networkType ;
      private string AV15sendTo ;
      private string AV13returnTo ;
      private string AV8error ;
      private bool AV14sendAllCoins ;
      private string AV9hexTransaction ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV16transactionsToSend ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV11oneAddressHistory ;
      private long aP5_virtualSize ;
      private string aP6_error ;
   }

}
