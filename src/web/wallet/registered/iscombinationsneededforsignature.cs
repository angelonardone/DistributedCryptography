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
namespace GeneXus.Programs.wallet.registered {
   public class iscombinationsneededforsignature : GXProcedure
   {
      public iscombinationsneededforsignature( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public iscombinationsneededforsignature( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                           GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination ,
                           out GeneXus.Programs.wallet.SdtMultiSigSignatureData aP2_returnMultiSigSignatureData ,
                           out bool aP3_isNeeded )
      {
         this.AV10oneAddressHistory = aP0_oneAddressHistory;
         this.AV8finalOneCombination = aP1_finalOneCombination;
         this.AV15returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context) ;
         this.AV9isNeeded = false ;
         initialize();
         ExecuteImpl();
         aP2_returnMultiSigSignatureData=this.AV15returnMultiSigSignatureData;
         aP3_isNeeded=this.AV9isNeeded;
      }

      public bool executeUdp( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                              GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination ,
                              out GeneXus.Programs.wallet.SdtMultiSigSignatureData aP2_returnMultiSigSignatureData )
      {
         execute(aP0_oneAddressHistory, aP1_finalOneCombination, out aP2_returnMultiSigSignatureData, out aP3_isNeeded);
         return AV9isNeeded ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                                 GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination ,
                                 out GeneXus.Programs.wallet.SdtMultiSigSignatureData aP2_returnMultiSigSignatureData ,
                                 out bool aP3_isNeeded )
      {
         this.AV10oneAddressHistory = aP0_oneAddressHistory;
         this.AV8finalOneCombination = aP1_finalOneCombination;
         this.AV15returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context) ;
         this.AV9isNeeded = false ;
         SubmitImpl();
         aP2_returnMultiSigSignatureData=this.AV15returnMultiSigSignatureData;
         aP3_isNeeded=this.AV9isNeeded;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9isNeeded = false;
         AV15returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV22selecteMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV21previousTotalCombination = 0;
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV10oneAddressHistory.gxTpr_Multisignaturedata.Count )
         {
            AV11oneMultiSigSignatureData = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData)AV10oneAddressHistory.gxTpr_Multisignaturedata.Item(AV23GXV1));
            AV20totalCombinations = (short)(AV11oneMultiSigSignatureData.gxTpr_Data.Count);
            if ( AV21previousTotalCombination == 0 )
            {
               AV21previousTotalCombination = AV20totalCombinations;
               AV22selecteMultiSigSignatureData = AV11oneMultiSigSignatureData;
            }
            else
            {
               if ( AV20totalCombinations < AV21previousTotalCombination )
               {
                  AV21previousTotalCombination = AV20totalCombinations;
                  AV22selecteMultiSigSignatureData = AV11oneMultiSigSignatureData;
               }
            }
            AV23GXV1 = (int)(AV23GXV1+1);
         }
         AV24GXV2 = 1;
         while ( AV24GXV2 <= AV22selecteMultiSigSignatureData.gxTpr_Data.Count )
         {
            AV13oneDataItem = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV22selecteMultiSigSignatureData.gxTpr_Data.Item(AV24GXV2));
            AV17countItems = 0;
            AV18finalOneCombination1 = AV13oneDataItem.gxTpr_Finalcombination;
            AV25GXV3 = 1;
            while ( AV25GXV3 <= AV8finalOneCombination.gxTpr_Items.Count )
            {
               AV12oneItem = AV8finalOneCombination.gxTpr_Items.GetString(AV25GXV3);
               AV26GXV4 = 1;
               while ( AV26GXV4 <= AV18finalOneCombination1.gxTpr_Items.Count )
               {
                  AV19oneItem1 = AV18finalOneCombination1.gxTpr_Items.GetString(AV26GXV4);
                  if ( StringUtil.StrCmp(AV19oneItem1, AV12oneItem) == 0 )
                  {
                     AV17countItems = (short)(AV17countItems+1);
                  }
                  AV26GXV4 = (int)(AV26GXV4+1);
               }
               if ( AV17countItems == AV8finalOneCombination.gxTpr_Items.Count )
               {
                  AV9isNeeded = true;
                  AV14returnDataItem = (GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)(AV13oneDataItem.Clone());
                  AV15returnMultiSigSignatureData.gxTpr_Data.Add(AV14returnDataItem, 0);
                  cleanup();
                  if (true) return;
               }
               AV25GXV3 = (int)(AV25GXV3+1);
            }
            AV24GXV2 = (int)(AV24GXV2+1);
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
         AV15returnMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV22selecteMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV11oneMultiSigSignatureData = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV13oneDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV18finalOneCombination1 = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV12oneItem = "";
         AV19oneItem1 = "";
         AV14returnDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         /* GeneXus formulas. */
      }

      private short AV21previousTotalCombination ;
      private short AV20totalCombinations ;
      private short AV17countItems ;
      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int AV25GXV3 ;
      private int AV26GXV4 ;
      private string AV12oneItem ;
      private string AV19oneItem1 ;
      private bool AV9isNeeded ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneAddressHistory ;
      private GeneXus.Programs.math.SdtfinalCombination AV8finalOneCombination ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV15returnMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV22selecteMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV11oneMultiSigSignatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV13oneDataItem ;
      private GeneXus.Programs.math.SdtfinalCombination AV18finalOneCombination1 ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV14returnDataItem ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData aP2_returnMultiSigSignatureData ;
      private bool aP3_isNeeded ;
   }

}
