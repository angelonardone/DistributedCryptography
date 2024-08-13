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
   public class getbalancefromhistorywithbalance : GXProcedure
   {
      public getbalancefromhistorywithbalance( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getbalancefromhistorywithbalance( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out decimal aP0_totalBalance )
      {
         this.AV8totalBalance = 0 ;
         initialize();
         ExecuteImpl();
         aP0_totalBalance=this.AV8totalBalance;
      }

      public decimal executeUdp( )
      {
         execute(out aP0_totalBalance);
         return AV8totalBalance ;
      }

      public void executeSubmit( out decimal aP0_totalBalance )
      {
         this.AV8totalBalance = 0 ;
         SubmitImpl();
         aP0_totalBalance=this.AV8totalBalance;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDTAddressHistory1 = AV10historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory1) ;
         AV10historyWithBalance = GXt_objcol_SdtSDTAddressHistory1;
         AV11GXV1 = 1;
         while ( AV11GXV1 <= AV10historyWithBalance.Count )
         {
            AV9oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV10historyWithBalance.Item(AV11GXV1));
            AV8totalBalance = (decimal)(AV8totalBalance+(AV9oneAddressHistory.gxTpr_Balance));
            AV11GXV1 = (int)(AV11GXV1+1);
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
         AV10historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV9oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         /* GeneXus formulas. */
      }

      private int AV11GXV1 ;
      private decimal AV8totalBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV10historyWithBalance ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory1 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV9oneAddressHistory ;
      private decimal aP0_totalBalance ;
   }

}
