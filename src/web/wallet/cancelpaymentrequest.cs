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
   public class cancelpaymentrequest : GXProcedure
   {
      public cancelpaymentrequest( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public cancelpaymentrequest( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_requestId ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV11requestId = aP1_requestId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_requestId )
      {
         execute(aP0_groupId, aP1_requestId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_requestId ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV11requestId = aP1_requestId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV14groupId, out  AV17transactionFileName, out  AV16requestsFileName, out  AV15receivingType, out  AV13changeType) ;
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV14groupId, out  AV12requests, out  AV8error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV9found = false;
            AV23GXV1 = 1;
            while ( AV23GXV1 <= AV12requests.Count )
            {
               AV10oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV12requests.Item(AV23GXV1));
               if ( AV10oneRequest.gxTpr_Requestid == AV11requestId )
               {
                  AV9found = true;
                  if ( StringUtil.StrCmp(AV10oneRequest.gxTpr_Status, "Pending") != 0 )
                  {
                     AV8error = "Only a Pending request can be cancelled";
                  }
                  else
                  {
                     AV10oneRequest.gxTpr_Cancelled = true;
                     AV10oneRequest.gxTpr_Status = "Cancelled";
                  }
                  if (true) break;
               }
               AV23GXV1 = (int)(AV23GXV1+1);
            }
            if ( ! AV9found )
            {
               AV8error = "The payment request was not found";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && (Guid.Empty==AV14groupId) )
            {
               GXt_char1 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV16requestsFileName,  AV12requests.ToJSonString(false), out  GXt_char1) ;
               AV8error = GXt_char1;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && ! (Guid.Empty==AV14groupId) )
         {
            AV22tries = 0;
            while ( AV22tries < 2 )
            {
               AV22tries = (short)(AV22tries+1);
               new GeneXus.Programs.wallet.registered.listsharedrecords(context ).execute(  AV14groupId,  "reservation", out  AV20sharedRecords, out  AV8error) ;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  if (true) break;
               }
               AV19recordFound = false;
               AV24GXV2 = 1;
               while ( AV24GXV2 <= AV20sharedRecords.Count )
               {
                  AV18oneShared = ((GeneXus.Programs.wallet.registered.SdtGroupSharedRecord)AV20sharedRecords.Item(AV24GXV2));
                  AV21sharedRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
                  AV21sharedRequest.FromJSonString(AV18oneShared.gxTpr_Cleartext, null);
                  if ( AV21sharedRequest.gxTpr_Requestid == AV11requestId )
                  {
                     AV19recordFound = true;
                     AV21sharedRequest.gxTpr_Cancelled = true;
                     new GeneXus.Programs.wallet.registered.updatesharedrecord(context ).execute(  AV14groupId,  "reservation",  AV18oneShared.gxTpr_Recordkey,  AV21sharedRequest.ToJSonString(false, true),  AV18oneShared.gxTpr_Version, out  AV8error) ;
                     if (true) break;
                  }
                  AV24GXV2 = (int)(AV24GXV2+1);
               }
               if ( ! AV19recordFound )
               {
                  AV8error = "The payment request was not found";
               }
               if ( StringUtil.StrCmp(AV8error, "conflict") != 0 )
               {
                  if (true) break;
               }
            }
            if ( StringUtil.StrCmp(AV8error, "conflict") == 0 )
            {
               AV8error = "Another member changed this payment request at the same time: reload the list and try again";
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
         AV8error = "";
         AV17transactionFileName = "";
         AV16requestsFileName = "";
         AV12requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV10oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         GXt_char1 = "";
         AV20sharedRecords = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography");
         AV18oneShared = new GeneXus.Programs.wallet.registered.SdtGroupSharedRecord(context);
         AV21sharedRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         /* GeneXus formulas. */
      }

      private short AV15receivingType ;
      private short AV13changeType ;
      private short AV22tries ;
      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private string AV8error ;
      private string AV17transactionFileName ;
      private string AV16requestsFileName ;
      private string GXt_char1 ;
      private bool AV9found ;
      private bool AV19recordFound ;
      private Guid AV14groupId ;
      private Guid AV11requestId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV12requests ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV10oneRequest ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> AV20sharedRecords ;
      private GeneXus.Programs.wallet.registered.SdtGroupSharedRecord AV18oneShared ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV21sharedRequest ;
      private string aP2_error ;
   }

}
