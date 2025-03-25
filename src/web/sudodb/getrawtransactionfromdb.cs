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
namespace GeneXus.Programs.sudodb {
   public class getrawtransactionfromdb : GXProcedure
   {
      public getrawtransactionfromdb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getrawtransactionfromdb( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionId ,
                           out string aP1_hex ,
                           out string aP2_error )
      {
         this.AV10transactionId = aP0_transactionId;
         this.AV12hex = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_hex=this.AV12hex;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_transactionId ,
                                out string aP1_hex )
      {
         execute(aP0_transactionId, out aP1_hex, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_transactionId ,
                                 out string aP1_hex ,
                                 out string aP2_error )
      {
         this.AV10transactionId = aP0_transactionId;
         this.AV12hex = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_hex=this.AV12hex;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11transactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV8error), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV13GXV1 = 1;
            while ( AV13GXV1 <= AV11transactions.Count )
            {
               AV9oneTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV11transactions.Item(AV13GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV9oneTransaction.gxTpr_Transactionid), StringUtil.Trim( AV10transactionId)) == 0 )
               {
                  AV12hex = StringUtil.Trim( AV9oneTransaction.gxTpr_Hex);
                  if (true) break;
               }
               AV13GXV1 = (int)(AV13GXV1+1);
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
         AV12hex = "";
         AV8error = "";
         AV11transactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV9oneTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV10transactionId ;
      private string AV8error ;
      private string AV12hex ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV11transactions ;
      private GeneXus.Programs.sudodb.SdtTransaction AV9oneTransaction ;
      private string aP1_hex ;
      private string aP2_error ;
   }

}
