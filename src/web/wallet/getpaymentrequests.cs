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
         if ( (Guid.Empty==AV17groupId) )
         {
            GXt_char1 = AV13stored;
            new GeneXus.Programs.wallet.readjsonencfile(context ).execute(  AV19requestsFileName, out  AV9error, out  GXt_char1) ;
            AV13stored = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV13stored)) )
            {
               AV12requests.FromJSonString(AV13stored, null);
            }
         }
         else
         {
            GXt_SdtGroup_SDT2 = AV24group_sdt;
            new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV17groupId, out  GXt_SdtGroup_SDT2) ;
            AV24group_sdt = GXt_SdtGroup_SDT2;
            AV25isOwner = AV24group_sdt.gxTpr_Amigroupowner;
            GXt_SdtExternalUser3 = AV23externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
            AV23externalUser = GXt_SdtExternalUser3;
            AV26myUserName = StringUtil.Trim( AV23externalUser.gxTpr_Userinfo.gxTpr_Username);
            new GeneXus.Programs.wallet.registered.listsharedrecords(context ).execute(  AV17groupId,  "reservation", out  AV22sharedRecords, out  AV9error) ;
            AV27GXV1 = 1;
            while ( AV27GXV1 <= AV22sharedRecords.Count )
            {
               AV21oneShared = ((GeneXus.Programs.wallet.registered.SdtGroupSharedRecord)AV22sharedRecords.Item(AV27GXV1));
               AV11oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
               AV11oneRequest.FromJSonString(AV21oneShared.gxTpr_Cleartext, null);
               AV12requests.Add(AV11oneRequest, 0);
               AV27GXV1 = (int)(AV27GXV1+1);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) && ( AV12requests.Count > 0 ) )
         {
            AV14StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV20transactionFileName, out  AV8auxError), null);
            AV10currentTime = DateTimeUtil.Now( context);
            AV28GXV2 = 1;
            while ( AV28GXV2 <= AV12requests.Count )
            {
               AV11oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV12requests.Item(AV28GXV2));
               AV11oneRequest.gxTpr_Receivedamount = (decimal)(0);
               AV29GXV3 = 1;
               while ( AV29GXV3 <= AV14StoredTransactions.gxTpr_Transaction.Count )
               {
                  AV15TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV14StoredTransactions.gxTpr_Transaction.Item(AV29GXV3));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV15TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV11oneRequest.gxTpr_Address)) == 0 )
                  {
                     AV11oneRequest.gxTpr_Receivedamount = (decimal)(AV11oneRequest.gxTpr_Receivedamount+(AV15TransactionItem.gxTpr_Value));
                  }
                  AV29GXV3 = (int)(AV29GXV3+1);
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
               AV11oneRequest.gxTpr_Cancancel = false;
               if ( StringUtil.StrCmp(AV11oneRequest.gxTpr_Status, "Pending") == 0 )
               {
                  if ( (Guid.Empty==AV17groupId) || AV25isOwner || ( StringUtil.StrCmp(StringUtil.Trim( AV11oneRequest.gxTpr_Createdby), AV26myUserName) == 0 ) )
                  {
                     AV11oneRequest.gxTpr_Cancancel = true;
                  }
               }
               GXt_char1 = "";
               new GeneXus.Programs.wallet.buildpaymenturi(context ).execute(  AV11oneRequest.gxTpr_Address,  AV11oneRequest.gxTpr_Amount,  AV11oneRequest.gxTpr_Label, out  GXt_char1) ;
               AV11oneRequest.gxTpr_Uri = GXt_char1;
               AV28GXV2 = (int)(AV28GXV2+1);
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
         AV24group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV23externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser3 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV26myUserName = "";
         AV22sharedRecords = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography");
         AV21oneShared = new GeneXus.Programs.wallet.registered.SdtGroupSharedRecord(context);
         AV11oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV14StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV8auxError = "";
         AV10currentTime = (DateTime)(DateTime.MinValue);
         AV15TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV18receivingType ;
      private short AV16changeType ;
      private int AV27GXV1 ;
      private int AV28GXV2 ;
      private int AV29GXV3 ;
      private string AV9error ;
      private string AV20transactionFileName ;
      private string AV19requestsFileName ;
      private string AV8auxError ;
      private string GXt_char1 ;
      private DateTime AV10currentTime ;
      private bool AV25isOwner ;
      private string AV13stored ;
      private string AV26myUserName ;
      private Guid AV17groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV12requests ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV24group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV23externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser3 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> AV22sharedRecords ;
      private GeneXus.Programs.wallet.registered.SdtGroupSharedRecord AV21oneShared ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV11oneRequest ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV14StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV15TransactionItem ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> aP1_requests ;
      private string aP2_error ;
   }

}
