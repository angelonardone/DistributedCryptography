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
   public class getreservedaddresses : GXProcedure
   {
      public getreservedaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getreservedaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GxSimpleCollection<string> aP1_addresses )
      {
         this.AV12groupId = aP0_groupId;
         this.AV8addresses = new GxSimpleCollection<string>() ;
         initialize();
         ExecuteImpl();
         aP1_addresses=this.AV8addresses;
      }

      public GxSimpleCollection<string> executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_addresses);
         return AV8addresses ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GxSimpleCollection<string> aP1_addresses )
      {
         this.AV12groupId = aP0_groupId;
         this.AV8addresses = new GxSimpleCollection<string>() ;
         SubmitImpl();
         aP1_addresses=this.AV8addresses;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8addresses = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV12groupId, out  AV11requests, out  AV9error) ;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV11requests.Count )
         {
            AV10oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV11requests.Item(AV13GXV1));
            if ( StringUtil.StrCmp(AV10oneRequest.gxTpr_Status, "Pending") == 0 )
            {
               AV8addresses.Add(StringUtil.Trim( AV10oneRequest.gxTpr_Address), 0);
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV8addresses = new GxSimpleCollection<string>();
         AV11requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV9error = "";
         AV10oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV9error ;
      private Guid AV12groupId ;
      private GxSimpleCollection<string> AV8addresses ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV11requests ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV10oneRequest ;
      private GxSimpleCollection<string> aP1_addresses ;
   }

}
