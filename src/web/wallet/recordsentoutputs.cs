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
   public class recordsentoutputs : GXProcedure
   {
      public recordsentoutputs( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public recordsentoutputs( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionFileName ,
                           string aP1_transactionId ,
                           string aP2_hexTransaction ,
                           out string aP3_error )
      {
         this.AV23transactionFileName = aP0_transactionFileName;
         this.AV24transactionId = aP1_transactionId;
         this.AV11hexTransaction = aP2_hexTransaction;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_transactionFileName ,
                                string aP1_transactionId ,
                                string aP2_hexTransaction )
      {
         execute(aP0_transactionFileName, aP1_transactionId, aP2_hexTransaction, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_transactionFileName ,
                                 string aP1_transactionId ,
                                 string aP2_hexTransaction ,
                                 out string aP3_error )
      {
         this.AV23transactionFileName = aP0_transactionFileName;
         this.AV24transactionId = aP1_transactionId;
         this.AV11hexTransaction = aP2_hexTransaction;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWalletInfo1 = AV27walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV27walletInfo = GXt_SdtWalletInfo1;
         AV13networkName = AV27walletInfo.gxTpr_Networktype;
         AV20outputsText = "";
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          string netName = AV13networkName.Trim();
         /* User Code */
          NBitcoin.Network net = netName == "MainNet" ? NBitcoin.Network.Main : (netName == "TestNet" ? NBitcoin.Network.TestNet : NBitcoin.Network.RegTest);
         /* User Code */
          NBitcoin.Transaction tx = NBitcoin.Transaction.Parse(AV11hexTransaction.Trim(), net);
         /* User Code */
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
         /* User Code */
          for (int i = 0; i < tx.Outputs.Count; i++)
         /* User Code */
          {
         /* User Code */
          NBitcoin.BitcoinAddress dest = tx.Outputs[i].ScriptPubKey.GetDestinationAddress(net);
         /* User Code */
          sb.Append(i).Append(',').Append(tx.Outputs[i].Value.ToDecimal(NBitcoin.MoneyUnit.BTC).ToString(System.Globalization.CultureInfo.InvariantCulture)).Append(',').Append(dest == null ? "" : dest.ToString()).Append(';');
         /* User Code */
          }
         /* User Code */
          AV20outputsText = sb.ToString();
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
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         AV22StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV23transactionFileName, out  AV8error), null);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         AV12label = "";
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV22StoredTransactions.gxTpr_Transaction.Count )
         {
            AV25TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV22StoredTransactions.gxTpr_Transaction.Item(AV28GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV25TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.Trim( AV24transactionId)) == 0 )
            {
               AV12label = StringUtil.Trim( AV25TransactionItem.gxTpr_Description);
            }
            AV28GXV1 = (int)(AV28GXV1+1);
         }
         AV19outputRows = (GxSimpleCollection<string>)(GxRegex.Split(AV20outputsText,";"));
         AV29GXV2 = 1;
         while ( AV29GXV2 <= AV19outputRows.Count )
         {
            AV18outputRow = ((string)AV19outputRows.Item(AV29GXV2));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV18outputRow))) )
            {
               AV9fields = (GxSimpleCollection<string>)(GxRegex.Split(AV18outputRow,","));
               AV17outputN = (long)(Math.Round(NumberUtil.Val( ((string)AV9fields.Item(1)), "."), 18, MidpointRounding.ToEven));
               AV21outputValue = NumberUtil.Val( ((string)AV9fields.Item(2)), ".");
               AV16outputAddress = "";
               if ( AV9fields.Count >= 3 )
               {
                  AV16outputAddress = ((string)AV9fields.Item(3));
                  AV16outputAddress = StringUtil.Trim( AV16outputAddress);
               }
               AV30GXV3 = 1;
               while ( AV30GXV3 <= AV22StoredTransactions.gxTpr_Transaction.Count )
               {
                  AV25TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV22StoredTransactions.gxTpr_Transaction.Item(AV30GXV3));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV25TransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.Trim( AV24transactionId)) == 0 )
                  {
                     AV10found = false;
                     AV31GXV4 = 1;
                     while ( AV31GXV4 <= AV25TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count )
                     {
                        AV26usedTo = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV25TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV31GXV4));
                        if ( AV26usedTo.gxTpr_N == AV17outputN )
                        {
                           AV10found = true;
                           if (true) break;
                        }
                        AV31GXV4 = (int)(AV31GXV4+1);
                     }
                     if ( ! AV10found )
                     {
                        AV26usedTo = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
                        AV26usedTo.gxTpr_N = AV17outputN;
                        AV26usedTo.gxTpr_Value = AV21outputValue;
                        AV26usedTo.gxTpr_Scriptpubkey_address = AV16outputAddress;
                        AV25TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Add(AV26usedTo, 0);
                     }
                  }
                  AV30GXV3 = (int)(AV30GXV3+1);
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16outputAddress)) )
               {
                  GXt_SdtSDT_Addressess_SDT_AddressessItem2 = AV15one_sdt_address;
                  new GeneXus.Programs.wallet.searchinalladdress(context ).execute(  AV16outputAddress, out  GXt_SdtSDT_Addressess_SDT_AddressessItem2) ;
                  AV15one_sdt_address = GXt_SdtSDT_Addressess_SDT_AddressessItem2;
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV15one_sdt_address.gxTpr_Address), AV16outputAddress) == 0 )
                  {
                     AV10found = false;
                     AV32GXV5 = 1;
                     while ( AV32GXV5 <= AV22StoredTransactions.gxTpr_Transaction.Count )
                     {
                        AV25TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV22StoredTransactions.gxTpr_Transaction.Item(AV32GXV5));
                        if ( ( StringUtil.StrCmp(StringUtil.Trim( AV25TransactionItem.gxTpr_Transactionid), StringUtil.Trim( AV24transactionId)) == 0 ) && ( AV25TransactionItem.gxTpr_N == AV17outputN ) )
                        {
                           AV10found = true;
                           if (true) break;
                        }
                        AV32GXV5 = (int)(AV32GXV5+1);
                     }
                     if ( ! AV10found )
                     {
                        AV14newItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
                        AV14newItem.gxTpr_Transactionid = StringUtil.Trim( AV24transactionId);
                        AV14newItem.gxTpr_N = AV17outputN;
                        AV14newItem.gxTpr_Value = AV21outputValue;
                        AV14newItem.gxTpr_Scriptpubkey_address = AV16outputAddress;
                        AV14newItem.gxTpr_Datetime = DateTimeUtil.Now( context);
                        AV14newItem.gxTpr_Confirmations = 0;
                        AV14newItem.gxTpr_Addresscreationsequence = (short)(AV15one_sdt_address.gxTpr_Creationsequence);
                        AV14newItem.gxTpr_Addressgeneratedtype = AV15one_sdt_address.gxTpr_Generatedtype;
                        AV14newItem.gxTpr_Description = AV12label;
                        AV22StoredTransactions.gxTpr_Transaction.Add(AV14newItem, 0);
                     }
                  }
               }
            }
            AV29GXV2 = (int)(AV29GXV2+1);
         }
         GXt_char3 = AV8error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV23transactionFileName,  AV22StoredTransactions.ToJSonString(false, true), out  GXt_char3) ;
         AV8error = GXt_char3;
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
         AV27walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13networkName = "";
         AV20outputsText = "";
         AV22StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV12label = "";
         AV25TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV19outputRows = new GxSimpleCollection<string>();
         AV18outputRow = "";
         AV9fields = new GxSimpleCollection<string>();
         AV16outputAddress = "";
         AV26usedTo = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(context);
         AV15one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         GXt_SdtSDT_Addressess_SDT_AddressessItem2 = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV14newItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV28GXV1 ;
      private int AV29GXV2 ;
      private int AV30GXV3 ;
      private int AV31GXV4 ;
      private int AV32GXV5 ;
      private long AV17outputN ;
      private decimal AV21outputValue ;
      private string AV23transactionFileName ;
      private string AV8error ;
      private string AV13networkName ;
      private string GXt_char3 ;
      private bool AV10found ;
      private string AV11hexTransaction ;
      private string AV20outputsText ;
      private string AV24transactionId ;
      private string AV12label ;
      private string AV18outputRow ;
      private string AV16outputAddress ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV27walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV22StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV25TransactionItem ;
      private GxSimpleCollection<string> AV19outputRows ;
      private GxSimpleCollection<string> AV9fields ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem AV26usedTo ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_sdt_address ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem GXt_SdtSDT_Addressess_SDT_AddressessItem2 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV14newItem ;
      private string aP3_error ;
   }

}
