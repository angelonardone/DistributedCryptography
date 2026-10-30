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
   public class getpaymentrequests : GXProcedure
   {
      public getpaymentrequests( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getpaymentrequests( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> aP1_requests ,
                           out string aP2_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV12requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography") ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_requests=this.AV12requests;
         aP2_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> aP1_requests )
      {
         execute(aP0_groupId, out aP1_requests, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> aP1_requests ,
                                 out string aP2_error )
      {
         this.AV17groupId = aP0_groupId;
         this.AV12requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography") ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_requests=this.AV12requests;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV17groupId, out  AV20transactionFileName, out  AV19requestsFileName, out  AV18receivingType, out  AV16changeType) ;
         AV12requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         GXt_char1 = AV13stored;
         new GeneXus.Programs.wallet.readjsonencfile(context ).execute(  AV19requestsFileName, out  AV9error, out  GXt_char1) ;
         AV13stored = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV13stored)) )
         {
            AV12requests.FromJSonString(AV13stored, null);
            AV14StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV20transactionFileName, out  AV8auxError), null);
            AV10currentTime = DateTimeUtil.Now( context);
            AV21GXV1 = 1;
            while ( AV21GXV1 <= AV12requests.Count )
            {
               AV11oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV12requests.Item(AV21GXV1));
               AV11oneRequest.gxTpr_Receivedamount = (decimal)(0);
               AV22GXV2 = 1;
               while ( AV22GXV2 <= AV14StoredTransactions.gxTpr_Transaction.Count )
               {
                  AV15TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV14StoredTransactions.gxTpr_Transaction.Item(AV22GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV15TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV11oneRequest.gxTpr_Address)) == 0 )
                  {
                     AV11oneRequest.gxTpr_Receivedamount = (decimal)(AV11oneRequest.gxTpr_Receivedamount+(AV15TransactionItem.gxTpr_Value));
                  }
                  AV22GXV2 = (int)(AV22GXV2+1);
               }
               if ( ( AV11oneRequest.gxTpr_Receivedamount > Convert.ToDecimal( 0 )) && ( AV11oneRequest.gxTpr_Receivedamount >= AV11oneRequest.gxTpr_Amount ) )
               {
                  AV11oneRequest.gxTpr_Status = "Paid";
               }
               else if ( ( AV11oneRequest.gxTpr_Receivedamount > Convert.ToDecimal( 0 )) )
               {
                  AV11oneRequest.gxTpr_Status = "Partially paid";
               }
               else if ( AV11oneRequest.gxTpr_Cancelled )
               {
                  AV11oneRequest.gxTpr_Status = "Cancelled";
               }
               else if ( ! (DateTime.MinValue==AV11oneRequest.gxTpr_Expiresat) && ( AV11oneRequest.gxTpr_Expiresat < AV10currentTime ) )
               {
                  AV11oneRequest.gxTpr_Status = "Expired";
               }
               else
               {
                  AV11oneRequest.gxTpr_Status = "Pending";
               }
               GXt_char1 = "";
               new GeneXus.Programs.wallet.buildpaymenturi(context ).execute(  AV11oneRequest.gxTpr_Address,  AV11oneRequest.gxTpr_Amount,  AV11oneRequest.gxTpr_Label, out  GXt_char1) ;
               AV11oneRequest.gxTpr_Uri = GXt_char1;
               AV21GXV1 = (int)(AV21GXV1+1);
            }
            AV12requests.Sort("[createdAt]");
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
         AV12requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV9error = "";
         AV20transactionFileName = "";
         AV19requestsFileName = "";
         AV13stored = "";
         AV14StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV8auxError = "";
         AV10currentTime = (DateTime)(DateTime.MinValue);
         AV11oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV15TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV18receivingType ;
      private short AV16changeType ;
      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private string AV9error ;
      private string AV20transactionFileName ;
      private string AV19requestsFileName ;
      private string AV8auxError ;
      private string GXt_char1 ;
      private DateTime AV10currentTime ;
      private string AV13stored ;
      private Guid AV17groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV12requests ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV14StoredTransactions ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV11oneRequest ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV15TransactionItem ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> aP1_requests ;
      private string aP2_error ;
   }

}
