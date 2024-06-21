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
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend )
      {
         this.AV14description = aP0_description;
         this.AV8amountToSend = aP1_amountToSend;
         this.AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP2_transactionsToSend=this.AV12transactionsToSend;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> executeUdp( string aP0_description ,
                                                                                        decimal aP1_amountToSend )
      {
         execute(aP0_description, aP1_amountToSend, out aP2_transactionsToSend);
         return AV12transactionsToSend ;
      }

      public void executeSubmit( string aP0_description ,
                                 decimal aP1_amountToSend ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend )
      {
         this.AV14description = aP0_description;
         this.AV8amountToSend = aP1_amountToSend;
         this.AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         SubmitImpl();
         aP2_transactionsToSend=this.AV12transactionsToSend;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13feeUsedForEstimate = NumberUtil.Val( "0.00001000", ".");
         GXt_objcol_SdtSDTAddressHistory1 = AV9historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory1) ;
         AV9historyWithBalance = GXt_objcol_SdtSDTAddressHistory1;
         AV9historyWithBalance.Sort("Balance");
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV9historyWithBalance.Count )
         {
            AV10oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9historyWithBalance.Item(AV15GXV1));
            AV10oneAddressHistory.gxTpr_Description = StringUtil.Trim( AV14description);
            AV11totalBalance = (decimal)(AV11totalBalance+(AV10oneAddressHistory.gxTpr_Balance));
            AV12transactionsToSend.Add(AV10oneAddressHistory, 0);
            if ( AV11totalBalance > AV8amountToSend + AV13feeUsedForEstimate )
            {
               if (true) break;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
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
         AV12transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV9historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV10oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private decimal AV8amountToSend ;
      private decimal AV13feeUsedForEstimate ;
      private decimal AV11totalBalance ;
      private string AV14description ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV12transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9historyWithBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
   }

}
