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
         this.AV22estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV10script = aP1_script;
         this.AV11signatureData = aP2_signatureData;
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
         this.AV22estimateFeeOnly = aP0_estimateFeeOnly;
         this.AV10script = aP1_script;
         this.AV11signatureData = aP2_signatureData;
         this.AV15signatureDataOut = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context) ;
         SubmitImpl();
         aP3_signatureDataOut=this.AV15signatureDataOut;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15signatureDataOut.FromJSonString(AV11signatureData.ToJSonString(false, true), null);
         if ( AV22estimateFeeOnly )
         {
            cleanup();
            if (true) return;
         }
         AV15signatureDataOut.gxTpr_Signatures.Clear();
         AV16retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV10script,"\\s\\d+\\sOP_NUMEQUAL"));
         AV17filtered = ((string)AV16retSplit.Item(1));
         AV16retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV17filtered," OP_CHECKSIG "));
         AV17filtered = ((string)AV16retSplit.Item(2));
         AV23orderedSignatures.Add(((string)AV16retSplit.Item(1)), 0);
         AV16retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV17filtered," OP_CHECKSIGADD"));
         AV24I = 1;
         while ( AV24I <= AV16retSplit.Count )
         {
            AV23orderedSignatures.Add(((string)AV16retSplit.Item(AV24I)), 0);
            AV24I = (short)(AV24I+1);
         }
         AV19totalSignatures = (short)(AV16retSplit.Count+1);
         AV18signatureCount = 1;
         while ( AV18signatureCount <= AV19totalSignatures )
         {
            AV25GXV1 = 1;
            while ( AV25GXV1 <= AV11signatureData.gxTpr_Signatures.Count )
            {
               AV20oneSignature = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem)AV11signatureData.gxTpr_Signatures.Item(AV25GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV20oneSignature.gxTpr_Xonlypubkey), StringUtil.Trim( ((string)AV23orderedSignatures.Item(AV18signatureCount)))) == 0 )
               {
                  AV21newOneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
                  AV21newOneSignature.gxTpr_Signature = StringUtil.Trim( AV20oneSignature.gxTpr_Signature);
                  AV21newOneSignature.gxTpr_Xonlypubkey = StringUtil.Trim( AV20oneSignature.gxTpr_Xonlypubkey);
                  AV15signatureDataOut.gxTpr_Signatures.Add(AV21newOneSignature, 0);
                  if (true) break;
               }
               AV25GXV1 = (int)(AV25GXV1+1);
            }
            AV18signatureCount = (short)(AV18signatureCount+1);
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
         AV16retSplit = new GxSimpleCollection<string>();
         AV17filtered = "";
         AV23orderedSignatures = new GxSimpleCollection<string>();
         AV20oneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
         AV21newOneSignature = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem(context);
         /* GeneXus formulas. */
      }

      private short AV24I ;
      private short AV19totalSignatures ;
      private short AV18signatureCount ;
      private int AV25GXV1 ;
      private bool AV22estimateFeeOnly ;
      private string AV10script ;
      private string AV17filtered ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV11signatureData ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV15signatureDataOut ;
      private GxSimpleCollection<string> AV16retSplit ;
      private GxSimpleCollection<string> AV23orderedSignatures ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem AV20oneSignature ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem_signaturesItem AV21newOneSignature ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem aP3_signatureDataOut ;
   }

}
