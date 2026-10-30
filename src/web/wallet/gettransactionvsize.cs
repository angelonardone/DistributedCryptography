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
   public class gettransactionvsize : GXProcedure
   {
      public gettransactionvsize( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettransactionvsize( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_hex ,
                           out long aP1_virtualSize ,
                           out string aP2_error )
      {
         this.AV9hex = aP0_hex;
         this.AV10virtualSize = 0 ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_virtualSize=this.AV10virtualSize;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_hex ,
                                out long aP1_virtualSize )
      {
         execute(aP0_hex, out aP1_virtualSize, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_hex ,
                                 out long aP1_virtualSize ,
                                 out string aP2_error )
      {
         this.AV9hex = aP0_hex;
         this.AV10virtualSize = 0 ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_virtualSize=this.AV10virtualSize;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         AV10virtualSize = 0;
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          NBitcoin.Transaction tx = NBitcoin.Transaction.Parse(AV9hex.Trim(), NBitcoin.Network.Main);
         /* User Code */
          AV10virtualSize = tx.GetVirtualSize();
         /* User Code */
          }
         /* User Code */
          catch (Exception ex)
         /* User Code */
          {
         /* User Code */
          AV8error = ex.Message.ToString();
         /* User Code */
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
         /* GeneXus formulas. */
      }

      private long AV10virtualSize ;
      private string AV8error ;
      private string AV9hex ;
      private long aP1_virtualSize ;
      private string aP2_error ;
   }

}
