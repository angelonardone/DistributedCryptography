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
   public class combinelegacypsbts : GXProcedure
   {
      public combinelegacypsbts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public combinelegacypsbts( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_psbts ,
                           string aP1_networkType ,
                           out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP2_result )
      {
         this.AV13psbts = aP0_psbts;
         this.AV9networkType = aP1_networkType;
         this.AV14result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         initialize();
         ExecuteImpl();
         aP2_result=this.AV14result;
      }

      public GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult executeUdp( GxSimpleCollection<string> aP0_psbts ,
                                                                                string aP1_networkType )
      {
         execute(aP0_psbts, aP1_networkType, out aP2_result);
         return AV14result ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_psbts ,
                                 string aP1_networkType ,
                                 out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP2_result )
      {
         this.AV13psbts = aP0_psbts;
         this.AV9networkType = aP1_networkType;
         this.AV14result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         SubmitImpl();
         aP2_result=this.AV14result;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12jsonText = AV8groupProcessor.combinelegacypsbts(AV13psbts.ToJSonString(false), AV9networkType);
         AV14result.FromJSonString(AV12jsonText, null);
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
         AV14result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV12jsonText = "";
         AV8groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         /* GeneXus formulas. */
      }

      private string AV9networkType ;
      private string AV12jsonText ;
      private GxSimpleCollection<string> AV13psbts ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV14result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV8groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP2_result ;
   }

}
