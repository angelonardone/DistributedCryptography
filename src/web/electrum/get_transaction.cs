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
namespace GeneXus.Programs.electrum {
   public class get_transaction : GXProcedure
   {
      public get_transaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public get_transaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionId ,
                           short aP1_messageType ,
                           out string aP2_messageResponse ,
                           out string aP3_error )
      {
         this.AV10transactionId = aP0_transactionId;
         this.AV11messageType = aP1_messageType;
         this.AV12messageResponse = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_messageResponse=this.AV12messageResponse;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_transactionId ,
                                short aP1_messageType ,
                                out string aP2_messageResponse )
      {
         execute(aP0_transactionId, aP1_messageType, out aP2_messageResponse, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_transactionId ,
                                 short aP1_messageType ,
                                 out string aP2_messageResponse ,
                                 out string aP3_error )
      {
         this.AV10transactionId = aP0_transactionId;
         this.AV11messageType = aP1_messageType;
         this.AV12messageResponse = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_messageResponse=this.AV12messageResponse;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9sendMessage = "{\"jsonrpc\": \"2.0\",\"id\": \"blockchain.transaction.get\",\"method\": \"blockchain.transaction.get\",\"params\": [\"" + StringUtil.Trim( AV10transactionId) + "\",true]}";
         GXt_char1 = AV8error;
         new GeneXus.Programs.electrum.sendmessage(context ).execute(  AV9sendMessage,  AV11messageType, out  AV12messageResponse, out  GXt_char1) ;
         AV8error = GXt_char1;
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
         AV12messageResponse = "";
         AV8error = "";
         AV9sendMessage = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV11messageType ;
      private string AV10transactionId ;
      private string AV8error ;
      private string GXt_char1 ;
      private string AV12messageResponse ;
      private string AV9sendMessage ;
      private string aP2_messageResponse ;
      private string aP3_error ;
   }

}
