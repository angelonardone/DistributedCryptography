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
   public class transactiondescription : GXProcedure
   {
      public transactiondescription( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public transactiondescription( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_transactionId ,
                           long aP2_n ,
                           bool aP3_update ,
                           ref string aP4_description ,
                           out string aP5_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV18transactionId = aP1_transactionId;
         this.AV13n = aP2_n;
         this.AV20update = aP3_update;
         this.AV9description = aP4_description;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP4_description=this.AV9description;
         aP5_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_transactionId ,
                                long aP2_n ,
                                bool aP3_update ,
                                ref string aP4_description )
      {
         execute(aP0_groupId, aP1_transactionId, aP2_n, aP3_update, ref aP4_description, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_transactionId ,
                                 long aP2_n ,
                                 bool aP3_update ,
                                 ref string aP4_description ,
                                 out string aP5_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV18transactionId = aP1_transactionId;
         this.AV13n = aP2_n;
         this.AV20update = aP3_update;
         this.AV9description = aP4_description;
         this.AV10error = "" ;
         SubmitImpl();
         aP4_description=this.AV9description;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV12groupId, out  AV17transactionFileName, out  AV15requestsFileName, out  AV14receivingType, out  AV8changeType) ;
         AV16StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV17transactionFileName, out  AV10error), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV11found = false;
            AV21GXV1 = 1;
            while ( AV21GXV1 <= AV16StoredTransactions.gxTpr_Transaction.Count )
            {
               AV19TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV16StoredTransactions.gxTpr_Transaction.Item(AV21GXV1));
               if ( ( StringUtil.StrCmp(StringUtil.Trim( AV19TransactionItem.gxTpr_Transactionid), StringUtil.Trim( AV18transactionId)) == 0 ) && ( AV19TransactionItem.gxTpr_N == AV13n ) )
               {
                  AV11found = true;
                  if ( AV20update )
                  {
                     AV19TransactionItem.gxTpr_Description = StringUtil.Trim( AV9description);
                  }
                  else
                  {
                     AV9description = StringUtil.Trim( AV19TransactionItem.gxTpr_Description);
                  }
                  if (true) break;
               }
               AV21GXV1 = (int)(AV21GXV1+1);
            }
            if ( ! AV11found )
            {
               AV10error = "The transaction is not in this wallet";
            }
            else
            {
               if ( AV20update )
               {
                  GXt_char1 = AV10error;
                  new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV17transactionFileName,  AV16StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
                  AV10error = GXt_char1;
               }
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
         AV10error = "";
         AV17transactionFileName = "";
         AV15requestsFileName = "";
         AV16StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV19TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private short AV14receivingType ;
      private short AV8changeType ;
      private int AV21GXV1 ;
      private long AV13n ;
      private string AV18transactionId ;
      private string AV10error ;
      private string AV17transactionFileName ;
      private string AV15requestsFileName ;
      private string GXt_char1 ;
      private bool AV20update ;
      private bool AV11found ;
      private string AV9description ;
      private Guid AV12groupId ;
      private string aP4_description ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV16StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV19TransactionItem ;
      private string aP5_error ;
   }

}
