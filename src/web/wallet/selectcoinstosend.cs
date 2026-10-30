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
   public class selectcoinstosend : GXProcedure
   {
      public selectcoinstosend( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public selectcoinstosend( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_description ,
                           decimal aP1_amountToSend ,
                           decimal aP2_transactionFee ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend )
      {
         this.AV14description = aP0_description;
         this.AV8amountToSend = aP1_amountToSend;
         this.AV15transactionFee = aP2_transactionFee;
         this.AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP3_transactionsToSend=this.AV12transactionsToSend;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> executeUdp( string aP0_description ,
                                                                                        decimal aP1_amountToSend ,
                                                                                        decimal aP2_transactionFee )
      {
         execute(aP0_description, aP1_amountToSend, aP2_transactionFee, out aP3_transactionsToSend);
         return AV12transactionsToSend ;
      }

      public void executeSubmit( string aP0_description ,
                                 decimal aP1_amountToSend ,
                                 decimal aP2_transactionFee ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend )
      {
         this.AV14description = aP0_description;
         this.AV8amountToSend = aP1_amountToSend;
         this.AV15transactionFee = aP2_transactionFee;
         this.AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         SubmitImpl();
         aP3_transactionsToSend=this.AV12transactionsToSend;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDTAddressHistory1 = AV9historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory1) ;
         AV9historyWithBalance = GXt_objcol_SdtSDTAddressHistory1;
         AV16neededAmount = (decimal)(AV8amountToSend+AV15transactionFee);
         GXt_objcol_svchar2 = AV22selected;
         new GeneXus.Programs.wallet.getselectedcoins(context ).execute( out  GXt_objcol_svchar2) ;
         AV22selected = GXt_objcol_svchar2;
         if ( AV22selected.Count > 0 )
         {
            AV23GXV1 = 1;
            while ( AV23GXV1 <= AV9historyWithBalance.Count )
            {
               AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV23GXV1));
               if ( ( AV10oneAddressHistory.gxTpr_Balance > Convert.ToDecimal( 0 )) )
               {
                  AV21outpoint = StringUtil.Trim( AV10oneAddressHistory.gxTpr_Receivedtransactionid) + ":" + StringUtil.Trim( StringUtil.Str( (decimal)(AV10oneAddressHistory.gxTpr_Recivedn), 10, 0));
                  AV24GXV2 = 1;
                  while ( AV24GXV2 <= AV22selected.Count )
                  {
                     AV20oneOutpoint = ((string)AV22selected.Item(AV24GXV2));
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV20oneOutpoint), StringUtil.Trim( AV21outpoint)) == 0 )
                     {
                        AV10oneAddressHistory.gxTpr_Description = StringUtil.Trim( AV14description);
                        AV12transactionsToSend.Add(AV10oneAddressHistory, 0);
                        if (true) break;
                     }
                     AV24GXV2 = (int)(AV24GXV2+1);
                  }
               }
               AV23GXV1 = (int)(AV23GXV1+1);
            }
            cleanup();
            if (true) return;
         }
         AV9historyWithBalance.Sort("Balance");
         AV17bestSingleIndex = 0;
         AV18bestSingleBalance = 0;
         AV19currentIndex = 0;
         AV25GXV3 = 1;
         while ( AV25GXV3 <= AV9historyWithBalance.Count )
         {
            AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV25GXV3));
            AV19currentIndex = (long)(AV19currentIndex+1);
            if ( AV10oneAddressHistory.gxTpr_Balance >= AV16neededAmount )
            {
               AV17bestSingleIndex = AV19currentIndex;
               AV18bestSingleBalance = AV10oneAddressHistory.gxTpr_Balance;
               if (true) break;
            }
            AV25GXV3 = (int)(AV25GXV3+1);
         }
         if ( AV17bestSingleIndex > 0 )
         {
            AV19currentIndex = 0;
            AV26GXV4 = 1;
            while ( AV26GXV4 <= AV9historyWithBalance.Count )
            {
               AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV26GXV4));
               AV19currentIndex = (long)(AV19currentIndex+1);
               if ( AV19currentIndex == AV17bestSingleIndex )
               {
                  AV10oneAddressHistory.gxTpr_Description = StringUtil.Trim( AV14description);
                  AV12transactionsToSend.Add(AV10oneAddressHistory, 0);
                  if (true) break;
               }
               AV26GXV4 = (int)(AV26GXV4+1);
            }
         }
         else
         {
            AV9historyWithBalance.Sort("[Balance]");
            AV11totalBalance = 0;
            AV27GXV5 = 1;
            while ( AV27GXV5 <= AV9historyWithBalance.Count )
            {
               AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV27GXV5));
               AV10oneAddressHistory.gxTpr_Description = StringUtil.Trim( AV14description);
               AV11totalBalance = (decimal)(AV11totalBalance+(AV10oneAddressHistory.gxTpr_Balance));
               AV12transactionsToSend.Add(AV10oneAddressHistory, 0);
               if ( AV11totalBalance >= AV16neededAmount )
               {
                  if (true) break;
               }
               AV27GXV5 = (int)(AV27GXV5+1);
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
         AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV9historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV22selected = new GxSimpleCollection<string>();
         GXt_objcol_svchar2 = new GxSimpleCollection<string>();
         AV10oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV21outpoint = "";
         AV20oneOutpoint = "";
         /* GeneXus formulas. */
      }

      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int AV25GXV3 ;
      private int AV26GXV4 ;
      private int AV27GXV5 ;
      private long AV17bestSingleIndex ;
      private long AV19currentIndex ;
      private decimal AV8amountToSend ;
      private decimal AV15transactionFee ;
      private decimal AV16neededAmount ;
      private decimal AV18bestSingleBalance ;
      private decimal AV11totalBalance ;
      private string AV14description ;
      private string AV21outpoint ;
      private string AV20oneOutpoint ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV12transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9historyWithBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private GxSimpleCollection<string> AV22selected ;
      private GxSimpleCollection<string> GXt_objcol_svchar2 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ;
   }

}
