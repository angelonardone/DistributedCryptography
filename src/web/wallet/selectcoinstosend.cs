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
         AV9historyWithBalance.Sort("Balance");
         AV11totalBalance = 0;
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV9historyWithBalance.Count )
         {
            AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV16GXV1));
            AV10oneAddressHistory.gxTpr_Description = StringUtil.Trim( AV14description);
            AV11totalBalance = (decimal)(AV11totalBalance+(AV10oneAddressHistory.gxTpr_Balance));
            AV12transactionsToSend.Add(AV10oneAddressHistory, 0);
            if ( AV11totalBalance >= AV8amountToSend + AV15transactionFee )
            {
               if (true) break;
            }
            AV16GXV1 = (int)(AV16GXV1+1);
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
         AV10oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private decimal AV8amountToSend ;
      private decimal AV15transactionFee ;
      private decimal AV11totalBalance ;
      private string AV14description ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV12transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9historyWithBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP3_transactionsToSend ;
   }

}
