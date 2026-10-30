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
            AV18GXV1 = 1;
            while ( AV18GXV1 <= AV12requests.Count )
            {
               AV10oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV12requests.Item(AV18GXV1));
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
               AV18GXV1 = (int)(AV18GXV1+1);
            }
            if ( ! AV9found )
            {
               AV8error = "The payment request was not found";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_char1 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV16requestsFileName,  AV12requests.ToJSonString(false), out  GXt_char1) ;
               AV8error = GXt_char1;
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
         /* GeneXus formulas. */
      }

      private short AV15receivingType ;
      private short AV13changeType ;
      private int AV18GXV1 ;
      private string AV8error ;
      private string AV17transactionFileName ;
      private string AV16requestsFileName ;
      private string GXt_char1 ;
      private bool AV9found ;
      private Guid AV14groupId ;
      private Guid AV11requestId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV12requests ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV10oneRequest ;
      private string aP2_error ;
   }

}
