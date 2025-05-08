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
namespace GeneXus.Programs.electrum {
   public class get_estimatefee : GXProcedure
   {
      public get_estimatefee( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public get_estimatefee( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_inNumBlocks ,
                           out decimal aP1_estimateFee ,
                           out string aP2_error )
      {
         this.AV14inNumBlocks = aP0_inNumBlocks;
         this.AV8estimateFee = 0 ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_estimateFee=this.AV8estimateFee;
         aP2_error=this.AV12error;
      }

      public string executeUdp( short aP0_inNumBlocks ,
                                out decimal aP1_estimateFee )
      {
         execute(aP0_inNumBlocks, out aP1_estimateFee, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( short aP0_inNumBlocks ,
                                 out decimal aP1_estimateFee ,
                                 out string aP2_error )
      {
         this.AV14inNumBlocks = aP0_inNumBlocks;
         this.AV8estimateFee = 0 ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_estimateFee=this.AV8estimateFee;
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( AV14inNumBlocks == 0 )
         {
            AV9numBlocks = 1;
         }
         else
         {
            AV9numBlocks = AV14inNumBlocks;
         }
         AV11sendMessage = "{\"jsonrpc\": \"2.0\",\"id\": \"blockchain.estimatefee\",\"method\": \"blockchain.estimatefee\",\"params\": [\"" + StringUtil.Trim( StringUtil.Str( (decimal)(AV9numBlocks), 4, 0)) + "\"]}";
         GXt_char1 = AV12error;
         new GeneXus.Programs.electrum.sendmessage(context ).execute(  AV11sendMessage,  20, out  AV13messageResponse, out  GXt_char1) ;
         AV12error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV10electrumRespEstimateFee.FromJSonString(AV13messageResponse, null);
            AV8estimateFee = AV10electrumRespEstimateFee.gxTpr_Result;
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
         AV12error = "";
         AV11sendMessage = "";
         GXt_char1 = "";
         AV13messageResponse = "";
         AV10electrumRespEstimateFee = new GeneXus.Programs.electrum.SdtelectrumRespEstimateFee(context);
         /* GeneXus formulas. */
      }

      private short AV14inNumBlocks ;
      private short AV9numBlocks ;
      private decimal AV8estimateFee ;
      private string AV12error ;
      private string GXt_char1 ;
      private string AV11sendMessage ;
      private string AV13messageResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespEstimateFee AV10electrumRespEstimateFee ;
      private decimal aP1_estimateFee ;
      private string aP2_error ;
   }

}
