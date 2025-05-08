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
   public class savevouts : GXProcedure
   {
      public savevouts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savevouts( IGxContext context )
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
         AV8DBvOUTs.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "DBvOUT.db", out  AV10error), null);
         AV8DBvOUTs.Sort("TransactionId,n");
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV9electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Count )
         {
            AV12oneVout = ((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_voutItem)AV9electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Item(AV15GXV1));
            AV11oneDBvOUT = new GeneXus.Programs.sudodb.SdtvOUT(context);
            AV11oneDBvOUT.gxTpr_Transactionid = AV13transactionId;
            AV11oneDBvOUT.gxTpr_N = (long)(Math.Round(AV12oneVout.gxTpr_N, 18, MidpointRounding.ToEven));
            AV11oneDBvOUT.gxTpr_Value = AV12oneVout.gxTpr_Value;
            AV11oneDBvOUT.gxTpr_Scriptpubkey_address = StringUtil.Trim( AV12oneVout.gxTpr_Scriptpubkey.gxTpr_Address);
            AV11oneDBvOUT.gxTpr_Type = StringUtil.Trim( AV12oneVout.gxTpr_Scriptpubkey.gxTpr_Type);
            AV16GXV2 = 1;
            while ( AV16GXV2 <= AV8DBvOUTs.Count )
            {
               AV14tempDBvOUT = ((GeneXus.Programs.sudodb.SdtvOUT)AV8DBvOUTs.Item(AV16GXV2));
               if ( ( StringUtil.StrCmp(AV14tempDBvOUT.gxTpr_Transactionid, AV11oneDBvOUT.gxTpr_Transactionid) == 0 ) && ( AV14tempDBvOUT.gxTpr_N == AV11oneDBvOUT.gxTpr_N ) )
               {
                  AV8DBvOUTs.RemoveItem(AV8DBvOUTs.IndexOf(AV14tempDBvOUT));
                  if (true) break;
               }
               AV16GXV2 = (int)(AV16GXV2+1);
            }
            AV8DBvOUTs.Add(AV11oneDBvOUT, 0);
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         AV8DBvOUTs.Sort("scriptPubKey_address");
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "DBvOUT.db",  AV8DBvOUTs.ToJSonString(false), out  GXt_char1) ;
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
         AV8DBvOUTs = new GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT>( context, "vOUT", "distributedcryptography");
         AV12oneVout = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_voutItem(context);
         AV11oneDBvOUT = new GeneXus.Programs.sudodb.SdtvOUT(context);
         AV14tempDBvOUT = new GeneXus.Programs.sudodb.SdtvOUT(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private int AV16GXV2 ;
      private string AV10error ;
      private string AV13transactionId ;
      private string GXt_char1 ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV9electrumRespGetTransactionId ;
      private GXBaseCollection<GeneXus.Programs.sudodb.SdtvOUT> AV8DBvOUTs ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_voutItem AV12oneVout ;
      private GeneXus.Programs.sudodb.SdtvOUT AV11oneDBvOUT ;
      private GeneXus.Programs.sudodb.SdtvOUT AV14tempDBvOUT ;
      private string aP1_error ;
   }

}
