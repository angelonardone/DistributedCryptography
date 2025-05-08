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
   public class savetransaction : GXProcedure
   {
      public savetransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savetransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                           out string aP1_error )
      {
         this.AV8electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId )
      {
         execute(aP0_electrumRespGetTransactionId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                                 out string aP1_error )
      {
         this.AV8electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11transaction.gxTpr_Transactionid = StringUtil.Trim( AV8electrumRespGetTransactionId.gxTpr_Result.gxTpr_Txid);
         AV11transaction.gxTpr_Blockid = StringUtil.Trim( AV8electrumRespGetTransactionId.gxTpr_Result.gxTpr_Blockhash);
         AV11transaction.gxTpr_Confirmations = (long)(Math.Round(AV8electrumRespGetTransactionId.gxTpr_Result.gxTpr_Confirmations, 18, MidpointRounding.ToEven));
         GXt_dtime1 = (DateTime)(DateTime.MinValue);
         new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetimeseconds(context ).execute(  (long)(Math.Round(AV8electrumRespGetTransactionId.gxTpr_Result.gxTpr_Blocktime, 18, MidpointRounding.ToEven)), out  GXt_dtime1) ;
         AV11transaction.gxTpr_Blockdatetime = GXt_dtime1;
         AV11transaction.gxTpr_Hex = StringUtil.Trim( AV8electrumRespGetTransactionId.gxTpr_Result.gxTpr_Hex);
         AV12transactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBTrransaction.db", out  AV9error), null);
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV12transactions.Count )
         {
            AV10oneTransaction = ((GeneXus.Programs.sudodb.SdtTransaction)AV12transactions.Item(AV13GXV1));
            if ( StringUtil.StrCmp(AV10oneTransaction.gxTpr_Transactionid, AV11transaction.gxTpr_Transactionid) == 0 )
            {
               AV12transactions.RemoveItem(AV12transactions.IndexOf(AV10oneTransaction));
               if (true) break;
            }
            AV13GXV1 = (int)(AV13GXV1+1);
         }
         AV12transactions.Add(AV11transaction, 0);
         AV12transactions.Sort("TransactionId");
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "DBTrransaction.db",  AV12transactions.ToJSonString(false), out  GXt_char2) ;
         AV9error = GXt_char2;
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
         AV9error = "";
         AV11transaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         GXt_dtime1 = (DateTime)(DateTime.MinValue);
         AV12transactions = new GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction>( context, "Transaction", "distributedcryptography");
         AV10oneTransaction = new GeneXus.Programs.sudodb.SdtTransaction(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV9error ;
      private string GXt_char2 ;
      private DateTime GXt_dtime1 ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV8electrumRespGetTransactionId ;
      private GeneXus.Programs.sudodb.SdtTransaction AV11transaction ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtTransaction> AV12transactions ;
      private GeneXus.Programs.sudodb.SdtTransaction AV10oneTransaction ;
      private string aP1_error ;
   }

}
