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
   public class finalizelegacypsbt : GXProcedure
   {
      public finalizelegacypsbt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public finalizelegacypsbt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_psbtBase64 ,
                           string aP1_networkType ,
                           out GeneXus.Programs.wallet.registered.SdtLegacyFinalResult aP2_result )
      {
         this.AV10psbtBase64 = aP0_psbtBase64;
         this.AV9networkType = aP1_networkType;
         this.AV13result = new GeneXus.Programs.wallet.registered.SdtLegacyFinalResult(context) ;
         initialize();
         ExecuteImpl();
         aP2_result=this.AV13result;
      }

      public GeneXus.Programs.wallet.registered.SdtLegacyFinalResult executeUdp( string aP0_psbtBase64 ,
                                                                                 string aP1_networkType )
      {
         execute(aP0_psbtBase64, aP1_networkType, out aP2_result);
         return AV13result ;
      }

      public void executeSubmit( string aP0_psbtBase64 ,
                                 string aP1_networkType ,
                                 out GeneXus.Programs.wallet.registered.SdtLegacyFinalResult aP2_result )
      {
         this.AV10psbtBase64 = aP0_psbtBase64;
         this.AV9networkType = aP1_networkType;
         this.AV13result = new GeneXus.Programs.wallet.registered.SdtLegacyFinalResult(context) ;
         SubmitImpl();
         aP2_result=this.AV13result;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12jsonText = AV8groupProcessor.finalizelegacypsbt(AV10psbtBase64, AV9networkType);
         AV13result.FromJSonString(AV12jsonText, null);
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
         AV13result = new GeneXus.Programs.wallet.registered.SdtLegacyFinalResult(context);
         AV12jsonText = "";
         AV8groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         /* GeneXus formulas. */
      }

      private string AV9networkType ;
      private string AV10psbtBase64 ;
      private string AV12jsonText ;
      private GeneXus.Programs.wallet.registered.SdtLegacyFinalResult AV13result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV8groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtLegacyFinalResult aP2_result ;
   }

}
