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
namespace GeneXus.Programs.sudodb {
   public class savevins : GXProcedure
   {
      public savevins( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savevins( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                           out string aP1_error )
      {
         this.AV9electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId )
      {
         execute(aP0_electrumRespGetTransactionId, out aP1_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                                 out string aP1_error )
      {
         this.AV9electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13transactionId = StringUtil.Trim( AV9electrumRespGetTransactionId.gxTpr_Result.gxTpr_Txid);
         AV8DBvINs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvIN.db", out  AV10error), null);
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV9electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Count )
         {
            AV12oneVin = ((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem)AV9electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Item(AV15GXV1));
            AV11oneDBvIN = new GeneXus.Programs.sudodb.SdtvIN(context);
            AV11oneDBvIN.gxTpr_Transactionid = AV13transactionId;
            AV11oneDBvIN.gxTpr_Vintransactionid = StringUtil.Trim( AV12oneVin.gxTpr_Txid);
            AV11oneDBvIN.gxTpr_Vinn = (long)(Math.Round(AV12oneVin.gxTpr_Vout, 18, MidpointRounding.ToEven));
            AV16GXV2 = 1;
            while ( AV16GXV2 <= AV8DBvINs.Count )
            {
               AV14tempDBvIN = ((GeneXus.Programs.sudodb.SdtvIN)AV8DBvINs.Item(AV16GXV2));
               if ( ( StringUtil.StrCmp(AV14tempDBvIN.gxTpr_Vintransactionid, AV11oneDBvIN.gxTpr_Vintransactionid) == 0 ) && ( AV14tempDBvIN.gxTpr_Vinn == AV11oneDBvIN.gxTpr_Vinn ) )
               {
                  AV8DBvINs.RemoveItem(AV8DBvINs.IndexOf(AV14tempDBvIN));
                  if (true) break;
               }
               AV16GXV2 = (int)(AV16GXV2+1);
            }
            AV8DBvINs.Add(AV11oneDBvIN, 0);
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         AV8DBvINs.Sort("vINTransactionId,vINn");
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "DBvIN.db",  AV8DBvINs.ToJSonString(false), out  GXt_char1) ;
         AV10error = GXt_char1;
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
         AV13transactionId = "";
         AV8DBvINs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN>( context, "vIN", "distributedcryptography");
         AV12oneVin = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem(context);
         AV11oneDBvIN = new GeneXus.Programs.sudodb.SdtvIN(context);
         AV14tempDBvIN = new GeneXus.Programs.sudodb.SdtvIN(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private int AV16GXV2 ;
      private string AV10error ;
      private string AV13transactionId ;
      private string GXt_char1 ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV9electrumRespGetTransactionId ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvIN> AV8DBvINs ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem AV12oneVin ;
      private GeneXus.Programs.sudodb.SdtvIN AV11oneDBvIN ;
      private GeneXus.Programs.sudodb.SdtvIN AV14tempDBvIN ;
      private string aP1_error ;
   }

}
