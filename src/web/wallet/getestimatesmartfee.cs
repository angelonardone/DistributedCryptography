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
   public class getestimatesmartfee : GXProcedure
   {
      public getestimatesmartfee( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getestimatesmartfee( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( long aP0_virtualSize ,
                           short aP1_numBlocks ,
                           string aP2_estimateSmartFeeType ,
                           out decimal aP3_estimatedFee ,
                           out short aP4_returnNumBlocks ,
                           out string aP5_error )
      {
         this.AV15virtualSize = aP0_virtualSize;
         this.AV14numBlocks = aP1_numBlocks;
         this.AV12estimateSmartFeeType = aP2_estimateSmartFeeType;
         this.AV18estimatedFee = 0 ;
         this.AV17returnNumBlocks = 0 ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_estimatedFee=this.AV18estimatedFee;
         aP4_returnNumBlocks=this.AV17returnNumBlocks;
         aP5_error=this.AV8error;
      }

      public string executeUdp( long aP0_virtualSize ,
                                short aP1_numBlocks ,
                                string aP2_estimateSmartFeeType ,
                                out decimal aP3_estimatedFee ,
                                out short aP4_returnNumBlocks )
      {
         execute(aP0_virtualSize, aP1_numBlocks, aP2_estimateSmartFeeType, out aP3_estimatedFee, out aP4_returnNumBlocks, out aP5_error);
         return AV8error ;
      }

      public void executeSubmit( long aP0_virtualSize ,
                                 short aP1_numBlocks ,
                                 string aP2_estimateSmartFeeType ,
                                 out decimal aP3_estimatedFee ,
                                 out short aP4_returnNumBlocks ,
                                 out string aP5_error )
      {
         this.AV15virtualSize = aP0_virtualSize;
         this.AV14numBlocks = aP1_numBlocks;
         this.AV12estimateSmartFeeType = aP2_estimateSmartFeeType;
         this.AV18estimatedFee = 0 ;
         this.AV17returnNumBlocks = 0 ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_estimatedFee=this.AV18estimatedFee;
         aP4_returnNumBlocks=this.AV17returnNumBlocks;
         aP5_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8error;
         new GeneXus.Programs.electrum.get_estimatefee(context ).execute(  AV14numBlocks, out  AV16feeRate, out  GXt_char1) ;
         AV8error = GXt_char1;
         AV17returnNumBlocks = AV14numBlocks;
         if ( ( AV16feeRate == Convert.ToDecimal( 0 )) )
         {
            AV16feeRate = NumberUtil.Val( "0.00001000", ".");
         }
         AV18estimatedFee = (decimal)(AV16feeRate/ (decimal)(1000)*AV15virtualSize);
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
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV14numBlocks ;
      private short AV17returnNumBlocks ;
      private long AV15virtualSize ;
      private decimal AV18estimatedFee ;
      private decimal AV16feeRate ;
      private string AV12estimateSmartFeeType ;
      private string AV8error ;
      private string GXt_char1 ;
      private decimal aP3_estimatedFee ;
      private short aP4_returnNumBlocks ;
      private string aP5_error ;
   }

}
