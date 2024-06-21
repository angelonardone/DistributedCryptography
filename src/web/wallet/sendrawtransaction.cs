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
   public class sendrawtransaction : GXProcedure
   {
      public sendrawtransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendrawtransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_hexTransaction ,
                           out string aP1_TransactionId ,
                           out string aP2_error )
      {
         this.AV13hexTransaction = aP0_hexTransaction;
         this.AV16TransactionId = "" ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP1_TransactionId=this.AV16TransactionId;
         aP2_error=this.AV14error;
      }

      public string executeUdp( string aP0_hexTransaction ,
                                out string aP1_TransactionId )
      {
         execute(aP0_hexTransaction, out aP1_TransactionId, out aP2_error);
         return AV14error ;
      }

      public void executeSubmit( string aP0_hexTransaction ,
                                 out string aP1_TransactionId ,
                                 out string aP2_error )
      {
         this.AV13hexTransaction = aP0_hexTransaction;
         this.AV16TransactionId = "" ;
         this.AV14error = "" ;
         SubmitImpl();
         aP1_TransactionId=this.AV16TransactionId;
         aP2_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12rawTransaction_postInput.gxTpr_Hextransaction = AV13hexTransaction;
         new gxexplorerservicesrestsendrawtransactionpost(context ).execute(  AV10ServerUrlTemplatingVar,  AV12rawTransaction_postInput, out  AV11RawTransaction__postOutputOUT, out  AV8HttpMessage, out  AV9IsSuccess) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11RawTransaction__postOutputOUT.gxTpr_Error)) )
         {
            AV15resultSendRawTransaction.FromJSonString(AV11RawTransaction__postOutputOUT.gxTpr_Sdt_sendblock_result.gxTpr_Error.gxTpr_Message, null);
            AV14error = AV15resultSendRawTransaction.gxTpr_Error;
            AV16TransactionId = StringUtil.Trim( AV15resultSendRawTransaction.gxTpr_Result);
         }
         else
         {
            AV14error = AV11RawTransaction__postOutputOUT.gxTpr_Error;
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
         AV16TransactionId = "";
         AV14error = "";
         AV12rawTransaction_postInput = new SdtSendRawTransaction__postInput(context);
         AV10ServerUrlTemplatingVar = new GXProperties();
         AV11RawTransaction__postOutputOUT = new SdtSendRawTransaction__postOutput(context);
         AV8HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV15resultSendRawTransaction = new GeneXus.Programs.wallet.SdtresultSendRawTransaction(context);
         /* GeneXus formulas. */
      }

      private string AV16TransactionId ;
      private string AV14error ;
      private bool AV9IsSuccess ;
      private string AV13hexTransaction ;
      private string aP1_TransactionId ;
      private string aP2_error ;
      private GXProperties AV10ServerUrlTemplatingVar ;
      private GeneXus.Utils.SdtMessages_Message AV8HttpMessage ;
      private SdtSendRawTransaction__postOutput AV11RawTransaction__postOutputOUT ;
      private SdtSendRawTransaction__postInput AV12rawTransaction_postInput ;
      private GeneXus.Programs.wallet.SdtresultSendRawTransaction AV15resultSendRawTransaction ;
   }

}
