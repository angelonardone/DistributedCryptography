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
   public class gettransactiondetail : GXProcedure
   {
      public gettransactiondetail( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettransactiondetail( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_transactionId ,
                           string aP1_transactionFileName ,
                           out GeneXus.Programs.wallet.SdtWalletTransactionItem aP2_header ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP3_inputs ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP4_outputs ,
                           out string aP5_error )
      {
         this.AV26transactionId = aP0_transactionId;
         this.AV25transactionFileName = aP1_transactionFileName;
         this.AV14header = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context) ;
         this.AV15inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography") ;
         this.AV20outputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography") ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_header=this.AV14header;
         aP3_inputs=this.AV15inputs;
         aP4_outputs=this.AV20outputs;
         aP5_error=this.AV13error;
      }

      public string executeUdp( string aP0_transactionId ,
                                string aP1_transactionFileName ,
                                out GeneXus.Programs.wallet.SdtWalletTransactionItem aP2_header ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP3_inputs ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP4_outputs )
      {
         execute(aP0_transactionId, aP1_transactionFileName, out aP2_header, out aP3_inputs, out aP4_outputs, out aP5_error);
         return AV13error ;
      }

      public void executeSubmit( string aP0_transactionId ,
                                 string aP1_transactionFileName ,
                                 out GeneXus.Programs.wallet.SdtWalletTransactionItem aP2_header ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP3_inputs ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP4_outputs ,
                                 out string aP5_error )
      {
         this.AV26transactionId = aP0_transactionId;
         this.AV25transactionFileName = aP1_transactionFileName;
         this.AV14header = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context) ;
         this.AV15inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography") ;
         this.AV20outputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography") ;
         this.AV13error = "" ;
         SubmitImpl();
         aP2_header=this.AV14header;
         aP3_inputs=this.AV15inputs;
         aP4_outputs=this.AV20outputs;
         aP5_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         AV20outputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         AV14header = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
         new GeneXus.Programs.wallet.getwallettransactions(context ).execute(  AV25transactionFileName, out  AV8allTransactions, out  AV10confirmedBalance, out  AV21pendingBalance, out  AV13error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV8allTransactions.Count )
         {
            AV19oneTransaction = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV8allTransactions.Item(AV28GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV19oneTransaction.gxTpr_Transactionid), StringUtil.Trim( AV26transactionId)) == 0 )
            {
               AV14header = AV19oneTransaction;
               if (true) break;
            }
            AV28GXV1 = (int)(AV28GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14header.gxTpr_Transactionid)) )
         {
            AV13error = "The transaction is not in this wallet";
            cleanup();
            if (true) return;
         }
         AV24StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV25transactionFileName, out  AV9auxError), null);
         AV11dbvINs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvIN.db", out  AV9auxError), null);
         AV12dbvOUTs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvOUT.db", out  AV9auxError), null);
         AV29GXV2 = 1;
         while ( AV29GXV2 <= AV11dbvINs.Count )
         {
            AV16oneDBvIN = ((GeneXus.Programs.sudodb.SdtvIN)AV11dbvINs.Item(AV29GXV2));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV16oneDBvIN.gxTpr_Transactionid), StringUtil.Trim( AV26transactionId)) == 0 )
            {
               AV18oneLine = new GeneXus.Programs.wallet.SdtTransactionLine(context);
               AV18oneLine.gxTpr_N = AV16oneDBvIN.gxTpr_Vinn;
               AV18oneLine.gxTpr_Outpoint = StringUtil.Trim( AV16oneDBvIN.gxTpr_Vintransactionid)+":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV16oneDBvIN.gxTpr_Vinn), 10, 0));
               AV18oneLine.gxTpr_Valuetext = "unknown";
               AV30GXV3 = 1;
               while ( AV30GXV3 <= AV12dbvOUTs.Count )
               {
                  AV17oneDBvOUT = ((GeneXus.Programs.sudodb.SdtvOUT)AV12dbvOUTs.Item(AV30GXV3));
                  if ( ( StringUtil.StrCmp(StringUtil.Trim( AV17oneDBvOUT.gxTpr_Transactionid), StringUtil.Trim( AV16oneDBvIN.gxTpr_Vintransactionid)) == 0 ) && ( AV17oneDBvOUT.gxTpr_N == AV16oneDBvIN.gxTpr_Vinn ) )
                  {
                     AV18oneLine.gxTpr_Address = StringUtil.Trim( AV17oneDBvOUT.gxTpr_Scriptpubkey_address);
                     AV18oneLine.gxTpr_Value = AV17oneDBvOUT.gxTpr_Value;
                     AV18oneLine.gxTpr_Valuetext = StringUtil.Trim( StringUtil.Str( AV17oneDBvOUT.gxTpr_Value, 17, 8));
                     if (true) break;
                  }
                  AV30GXV3 = (int)(AV30GXV3+1);
               }
               AV23previousTransactionId = StringUtil.Trim( AV16oneDBvIN.gxTpr_Vintransactionid);
               AV22previousN = AV16oneDBvIN.gxTpr_Vinn;
               /* Execute user subroutine: 'OWNER' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV15inputs.Add(AV18oneLine, 0);
            }
            AV29GXV2 = (int)(AV29GXV2+1);
         }
         AV31GXV4 = 1;
         while ( AV31GXV4 <= AV12dbvOUTs.Count )
         {
            AV17oneDBvOUT = ((GeneXus.Programs.sudodb.SdtvOUT)AV12dbvOUTs.Item(AV31GXV4));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV17oneDBvOUT.gxTpr_Transactionid), StringUtil.Trim( AV26transactionId)) == 0 )
            {
               AV18oneLine = new GeneXus.Programs.wallet.SdtTransactionLine(context);
               AV18oneLine.gxTpr_N = AV17oneDBvOUT.gxTpr_N;
               AV18oneLine.gxTpr_Outpoint = StringUtil.Trim( AV26transactionId)+":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV17oneDBvOUT.gxTpr_N), 10, 0));
               AV18oneLine.gxTpr_Address = StringUtil.Trim( AV17oneDBvOUT.gxTpr_Scriptpubkey_address);
               AV18oneLine.gxTpr_Value = AV17oneDBvOUT.gxTpr_Value;
               AV18oneLine.gxTpr_Valuetext = StringUtil.Trim( StringUtil.Str( AV17oneDBvOUT.gxTpr_Value, 17, 8));
               AV23previousTransactionId = StringUtil.Trim( AV26transactionId);
               AV22previousN = AV17oneDBvOUT.gxTpr_N;
               /* Execute user subroutine: 'OWNER' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV20outputs.Add(AV18oneLine, 0);
            }
            AV31GXV4 = (int)(AV31GXV4+1);
         }
         AV20outputs.Sort("n");
         cleanup();
      }

      protected void S111( )
      {
         /* 'OWNER' Routine */
         returnInSub = false;
         AV18oneLine.gxTpr_Owner = "Other";
         AV32GXV5 = 1;
         while ( AV32GXV5 <= AV24StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV24StoredTransactions.gxTpr_Transaction.Item(AV32GXV5));
            if ( ( StringUtil.StrCmp(StringUtil.Trim( AV27TransactionItem.gxTpr_Transactionid), StringUtil.Trim( AV23previousTransactionId)) == 0 ) && ( AV27TransactionItem.gxTpr_N == AV22previousN ) )
            {
               AV18oneLine.gxTpr_Owner = "This wallet";
               if (true) break;
            }
            AV32GXV5 = (int)(AV32GXV5+1);
         }
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
         AV14header = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
         AV15inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         AV20outputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         AV13error = "";
         AV8allTransactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography");
         AV19oneTransaction = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
         AV24StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV9auxError = "";
         AV11dbvINs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN>( context, "vIN", "distributedcryptography");
         AV12dbvOUTs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT>( context, "vOUT", "distributedcryptography");
         AV16oneDBvIN = new GeneXus.Programs.sudodb.SdtvIN(context);
         AV18oneLine = new GeneXus.Programs.wallet.SdtTransactionLine(context);
         AV17oneDBvOUT = new GeneXus.Programs.sudodb.SdtvOUT(context);
         AV23previousTransactionId = "";
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         /* GeneXus formulas. */
      }

      private int AV28GXV1 ;
      private int AV29GXV2 ;
      private int AV30GXV3 ;
      private int AV31GXV4 ;
      private int AV32GXV5 ;
      private long AV22previousN ;
      private decimal AV10confirmedBalance ;
      private decimal AV21pendingBalance ;
      private string AV26transactionId ;
      private string AV25transactionFileName ;
      private string AV13error ;
      private string AV9auxError ;
      private bool returnInSub ;
      private string AV23previousTransactionId ;
      private GeneXus.Programs.wallet.SdtWalletTransactionItem AV14header ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> AV15inputs ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> AV20outputs ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> AV8allTransactions ;
      private GeneXus.Programs.wallet.SdtWalletTransactionItem AV19oneTransaction ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV24StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN> AV11dbvINs ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT> AV12dbvOUTs ;
      private GeneXus.Programs.sudodb.SdtvIN AV16oneDBvIN ;
      private GeneXus.Programs.wallet.SdtTransactionLine AV18oneLine ;
      private GeneXus.Programs.sudodb.SdtvOUT AV17oneDBvOUT ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.wallet.SdtWalletTransactionItem aP2_header ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP3_inputs ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> aP4_outputs ;
      private string aP5_error ;
   }

}
