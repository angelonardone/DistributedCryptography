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
   public class getrawtransfromcoinstosend : GXProcedure
   {
      public getrawtransfromcoinstosend( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getrawtransfromcoinstosend( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP0_transactionsToSend ,
                           out string aP1_error )
      {
         this.AV8transactionsToSend = aP0_transactionsToSend;
         this.AV15error = "" ;
         initialize();
         ExecuteImpl();
         aP0_transactionsToSend=this.AV8transactionsToSend;
         aP1_error=this.AV15error;
      }

      public string executeUdp( ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP0_transactionsToSend )
      {
         execute(ref aP0_transactionsToSend, out aP1_error);
         return AV15error ;
      }

      public void executeSubmit( ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP0_transactionsToSend ,
                                 out string aP1_error )
      {
         this.AV8transactionsToSend = aP0_transactionsToSend;
         this.AV15error = "" ;
         SubmitImpl();
         aP0_transactionsToSend=this.AV8transactionsToSend;
         aP1_error=this.AV15error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV8transactionsToSend.Count )
         {
            AV9oneAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV8transactionsToSend.Item(AV16GXV1));
            new GeneXus.Programs.debug.consolewriteline(context ).execute(  AV9oneAddressHistory.ToJSonString(false, true)) ;
            GXt_char1 = AV15error;
            GXt_char2 = AV9oneAddressHistory.gxTpr_Receivedtransactionhex;
            new GeneXus.Programs.sudodb.getrawtransactionfromdb(context ).execute(  StringUtil.Trim( AV9oneAddressHistory.gxTpr_Receivedtransactionid), out  GXt_char2, out  GXt_char1) ;
            AV9oneAddressHistory.gxTpr_Receivedtransactionhex = GXt_char2;
            AV15error = GXt_char1;
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
         AV15error = "";
         AV9oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         GXt_char1 = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private string AV15error ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV8transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP0_transactionsToSend ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV9oneAddressHistory ;
      private string aP1_error ;
   }

}
