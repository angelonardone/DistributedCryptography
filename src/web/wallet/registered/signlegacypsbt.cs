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
   public class signlegacypsbt : GXProcedure
   {
      public signlegacypsbt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signlegacypsbt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_psbtBase64 ,
                           long aP1_sequence ,
                           string aP2_signerExtPrivKey ,
                           string aP3_networkType ,
                           out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP4_result )
      {
         this.AV10psbtBase64 = aP0_psbtBase64;
         this.AV12sequence = aP1_sequence;
         this.AV13signerExtPrivKey = aP2_signerExtPrivKey;
         this.AV9networkType = aP3_networkType;
         this.AV15result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         initialize();
         ExecuteImpl();
         aP4_result=this.AV15result;
      }

      public GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult executeUdp( string aP0_psbtBase64 ,
                                                                                long aP1_sequence ,
                                                                                string aP2_signerExtPrivKey ,
                                                                                string aP3_networkType )
      {
         execute(aP0_psbtBase64, aP1_sequence, aP2_signerExtPrivKey, aP3_networkType, out aP4_result);
         return AV15result ;
      }

      public void executeSubmit( string aP0_psbtBase64 ,
                                 long aP1_sequence ,
                                 string aP2_signerExtPrivKey ,
                                 string aP3_networkType ,
                                 out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP4_result )
      {
         this.AV10psbtBase64 = aP0_psbtBase64;
         this.AV12sequence = aP1_sequence;
         this.AV13signerExtPrivKey = aP2_signerExtPrivKey;
         this.AV9networkType = aP3_networkType;
         this.AV15result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         SubmitImpl();
         aP4_result=this.AV15result;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14jsonText = AV8groupProcessor.signlegacypsbt(AV10psbtBase64, (int)(AV12sequence), StringUtil.Trim( AV13signerExtPrivKey), AV9networkType);
         AV15result.FromJSonString(AV14jsonText, null);
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
         AV15result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV14jsonText = "";
         AV8groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         /* GeneXus formulas. */
      }

      private long AV12sequence ;
      private string AV13signerExtPrivKey ;
      private string AV9networkType ;
      private string AV10psbtBase64 ;
      private string AV14jsonText ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV15result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV8groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP4_result ;
   }

}
