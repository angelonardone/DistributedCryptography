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
            new gxexplorerservicesrestgetrawtransactionget(context ).execute(  AV11ServerUrlTemplatingVar,  StringUtil.Trim( AV9oneAddressHistory.gxTpr_Receivedtransactionid), out  GxExplorer_services_resultGetRawTransactionOUT, out  AV13HttpMessage, out  AV14IsSuccess) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( GxExplorer_services_resultGetRawTransactionOUT.gxTpr_Error)) )
            {
               AV9oneAddressHistory.gxTpr_Receivedtransactionhex = GxExplorer_services_resultGetRawTransactionOUT.gxTpr_Hextransaction;
            }
            else
            {
               AV15error = GxExplorer_services_resultGetRawTransactionOUT.gxTpr_Error;
            }
            AV16GXV1 = (int)(AV16GXV1+1);
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
         AV15error = "";
         AV9oneAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV11ServerUrlTemplatingVar = new GXProperties();
         GxExplorer_services_resultGetRawTransactionOUT = new SdtGxExplorer_services_resultGetRawTransaction(context);
         AV13HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private string AV15error ;
      private bool AV14IsSuccess ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP0_transactionsToSend ;
      private string aP1_error ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV8transactionsToSend ;
      private GXProperties AV11ServerUrlTemplatingVar ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV9oneAddressHistory ;
      private SdtGxExplorer_services_resultGetRawTransaction GxExplorer_services_resultGetRawTransactionOUT ;
      private GeneXus.Utils.SdtMessages_Message AV13HttpMessage ;
   }

}
