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
   public class estimatelegacyvsize : GXProcedure
   {
      public estimatelegacyvsize( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public estimatelegacyvsize( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( long aP0_numInputs ,
                           long aP1_numOutputs ,
                           long aP2_minSignatures ,
                           long aP3_numKeys ,
                           out long aP4_virtualSize )
      {
         this.AV9numInputs = aP0_numInputs;
         this.AV11numOutputs = aP1_numOutputs;
         this.AV8minSignatures = aP2_minSignatures;
         this.AV10numKeys = aP3_numKeys;
         this.AV12virtualSize = 0 ;
         initialize();
         ExecuteImpl();
         aP4_virtualSize=this.AV12virtualSize;
      }

      public long executeUdp( long aP0_numInputs ,
                              long aP1_numOutputs ,
                              long aP2_minSignatures ,
                              long aP3_numKeys )
      {
         execute(aP0_numInputs, aP1_numOutputs, aP2_minSignatures, aP3_numKeys, out aP4_virtualSize);
         return AV12virtualSize ;
      }

      public void executeSubmit( long aP0_numInputs ,
                                 long aP1_numOutputs ,
                                 long aP2_minSignatures ,
                                 long aP3_numKeys ,
                                 out long aP4_virtualSize )
      {
         this.AV9numInputs = aP0_numInputs;
         this.AV11numOutputs = aP1_numOutputs;
         this.AV8minSignatures = aP2_minSignatures;
         this.AV10numKeys = aP3_numKeys;
         this.AV12virtualSize = 0 ;
         SubmitImpl();
         aP4_virtualSize=this.AV12virtualSize;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          int nIn = (int)AV9numInputs;
         /* User Code */
          int nOut = (int)AV11numOutputs;
         /* User Code */
          int M = (int)AV8minSignatures;
         /* User Code */
          int N = (int)AV10numKeys;
         /* User Code */
          if (nIn < 1) nIn = 1;
         /* User Code */
          if (nOut < 1) nOut = 1;
         /* User Code */
          if (M < 1) M = 1;
         /* User Code */
          if (N < M) N = M;
         /* User Code */
          int witnessScript = 3 + 34 * N;
         /* User Code */
          int scriptPushPrefix = (witnessScript > 75) ? 2 : 1;
         /* User Code */
          int witnessPerInput = 1 + 1 + M * 73 + scriptPushPrefix + witnessScript;
         /* User Code */
          int baseBytes = 10 + 76 * nIn + 34 * nOut;
         /* User Code */
          int witnessBytes = 2 + nIn * witnessPerInput;
         /* User Code */
          int weight = baseBytes * 4 + witnessBytes;
         /* User Code */
          int vsize = (weight + 3) / 4;
         /* User Code */
          AV12virtualSize = vsize;
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
         /* GeneXus formulas. */
      }

      private long AV9numInputs ;
      private long AV11numOutputs ;
      private long AV8minSignatures ;
      private long AV10numKeys ;
      private long AV12virtualSize ;
      private long aP4_virtualSize ;
   }

}
