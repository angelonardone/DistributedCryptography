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
   public class ordersignaturestomatchscript : GXProcedure
   {
      public ordersignaturestomatchscript( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public ordersignaturestomatchscript( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_estimateFeeOnly ,
                           string aP1_script ,
                           GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP2_signatureData ,
                           out GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP3_signatureDataOut )
      {
         this.AV17estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV12script = aP1_script;
         this.AV14signatureData = aP2_signatureData;
         this.AV15signatureDataOut = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context) ;
         initialize();
         ExecuteImpl();
         aP3_signatureDataOut=this.AV15signatureDataOut;
      }

      public GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem executeUdp( bool aP0_estimateFeeOnly ,
                                                                                   string aP1_script ,
                                                                                   GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP2_signatureData )
      {
         execute(aP0_estimateFeeOnly, aP1_script, aP2_signatureData, out aP3_signatureDataOut);
         return AV15signatureDataOut ;
      }

      public void executeSubmit( bool aP0_estimateFeeOnly ,
                                 string aP1_script ,
                                 GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP2_signatureData ,
                                 out GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP3_signatureDataOut )
      {
         this.AV17estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV12script = aP1_script;
         this.AV14signatureData = aP2_signatureData;
         this.AV15signatureDataOut = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context) ;
         SubmitImpl();
         aP3_signatureDataOut=this.AV15signatureDataOut;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15signatureDataOut.FromJSonString(AV14signatureData.ToJSonString(false, true), null);
         if ( AV17estimateFeeOnly )
         {
            cleanup();
            if (true) return;
         }
         AV15signatureDataOut.gxTpr_Signatures.Clear();
         AV11retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV12script,"\\s\\d+\\sOP_NUMEQUAL"));
         AV8filtered = ((string)AV11retSplit.Item(1));
         AV11retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV8filtered," OP_CHECKSIG "));
         AV8filtered = ((string)AV11retSplit.Item(2));
         AV18orderedSignatures.Add(((string)AV11retSplit.Item(1)), 0);
         AV11retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV8filtered," OP_CHECKSIGADD"));
         AV19I = 1;
         while ( AV19I <= AV11retSplit.Count )
         {
            AV18orderedSignatures.Add(((string)AV11retSplit.Item(AV19I)), 0);
            AV19I = (short)(AV19I+1);
         }
         AV16totalSignatures = (short)(AV11retSplit.Count+1);
         AV13signatureCount = 1;
         while ( AV13signatureCount <= AV16totalSignatures )
         {
            AV20GXV1 = 1;
            while ( AV20GXV1 <= AV14signatureData.gxTpr_Signatures.Count )
            {
               AV10oneSignature = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem)AV14signatureData.gxTpr_Signatures.Item(AV20GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV10oneSignature.gxTpr_Xonlypubkey), StringUtil.Trim( ((string)AV18orderedSignatures.Item(AV13signatureCount)))) == 0 )
               {
                  AV9newOneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
                  AV9newOneSignature.gxTpr_Signature = StringUtil.Trim( AV10oneSignature.gxTpr_Signature);
                  AV9newOneSignature.gxTpr_Xonlypubkey = StringUtil.Trim( AV10oneSignature.gxTpr_Xonlypubkey);
                  AV15signatureDataOut.gxTpr_Signatures.Add(AV9newOneSignature, 0);
                  if (true) break;
               }
               AV20GXV1 = (int)(AV20GXV1+1);
            }
            AV13signatureCount = (short)(AV13signatureCount+1);
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
         AV15signatureDataOut = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV11retSplit = new GxSimpleCollection<string>();
         AV8filtered = "";
         AV18orderedSignatures = new GxSimpleCollection<string>();
         AV10oneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
         AV9newOneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
         /* GeneXus formulas. */
      }

      private short AV19I ;
      private short AV16totalSignatures ;
      private short AV13signatureCount ;
      private int AV20GXV1 ;
      private bool AV17estimateFeeOnly ;
      private string AV12script ;
      private string AV8filtered ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV14signatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV15signatureDataOut ;
      private GxSimpleCollection<string> AV11retSplit ;
      private GxSimpleCollection<string> AV18orderedSignatures ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem AV10oneSignature ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem AV9newOneSignature ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP3_signatureDataOut ;
   }

}
