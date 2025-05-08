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
namespace GeneXus.Programs.wallet.registered {
   public class getec_pubkeysfrommusigdata : GXProcedure
   {
      public getec_pubkeysfrommusigdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getec_pubkeysfrommusigdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                           out GxSimpleCollection<string> aP1_all_EC_PublicKey )
      {
         this.AV10oneAddressHistory = aP0_oneAddressHistory;
         this.AV19all_EC_PublicKey = new GxSimpleCollection<string>() ;
         initialize();
         ExecuteImpl();
         aP1_all_EC_PublicKey=this.AV19all_EC_PublicKey;
      }

      public GxSimpleCollection<string> executeUdp( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory )
      {
         execute(aP0_oneAddressHistory, out aP1_all_EC_PublicKey);
         return AV19all_EC_PublicKey ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                                 out GxSimpleCollection<string> aP1_all_EC_PublicKey )
      {
         this.AV10oneAddressHistory = aP0_oneAddressHistory;
         this.AV19all_EC_PublicKey = new GxSimpleCollection<string>() ;
         SubmitImpl();
         aP1_all_EC_PublicKey=this.AV19all_EC_PublicKey;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV19all_EC_PublicKey = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV10oneAddressHistory.gxTpr_Multisignaturedata.Count )
         {
            AV13oneMultiSigSignatureData = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData)AV10oneAddressHistory.gxTpr_Multisignaturedata.Item(AV20GXV1));
            AV21GXV2 = 1;
            while ( AV21GXV2 <= AV13oneMultiSigSignatureData.gxTpr_Data.Count )
            {
               AV11oneDataItem = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV13oneMultiSigSignatureData.gxTpr_Data.Item(AV21GXV2));
               AV19all_EC_PublicKey.Add(AV11oneDataItem.gxTpr_Ec_pubkey, 0);
               if (true) break;
               AV21GXV2 = (int)(AV21GXV2+1);
            }
            AV20GXV1 = (int)(AV20GXV1+1);
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
         AV19all_EC_PublicKey = new GxSimpleCollection<string>();
         AV13oneMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV11oneDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         /* GeneXus formulas. */
      }

      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
      private GxSimpleCollection<string> AV19all_EC_PublicKey ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV13oneMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV11oneDataItem ;
      private GxSimpleCollection<string> aP1_all_EC_PublicKey ;
   }

}
