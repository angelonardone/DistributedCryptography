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
using GeneXus.Http.Server;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.hsm {
   public class getcredentials : GXProcedure
   {
      public getcredentials( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcredentials( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_credentials )
      {
         this.AV8credentials = "" ;
         initialize();
         ExecuteImpl();
         aP0_credentials=this.AV8credentials;
      }

      public string executeUdp( )
      {
         execute(out aP0_credentials);
         return AV8credentials ;
      }

      public void executeSubmit( out string aP0_credentials )
      {
         this.AV8credentials = "" ;
         SubmitImpl();
         aP0_credentials=this.AV8credentials;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9HSMcredentialsSDT.gxTpr_Authorization = AV10HttpRequest.GetHeader("Authorization");
         AV9HSMcredentialsSDT.gxTpr_Timestamp = AV10HttpRequest.GetHeader("X-HSM-Timestamp");
         AV9HSMcredentialsSDT.gxTpr_Nonce = AV10HttpRequest.GetHeader("X-HSM-Nonce");
         AV9HSMcredentialsSDT.gxTpr_Signature = AV10HttpRequest.GetHeader("X-HSM-Signature");
         AV8credentials = AV9HSMcredentialsSDT.ToJSonString(false, true);
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
         AV8credentials = "";
         AV9HSMcredentialsSDT = new GeneXus.Programs.hsm.SdtHSMcredentialsSDT(context);
         AV10HttpRequest = new GxHttpRequest( context);
         /* GeneXus formulas. */
      }

      private string AV8credentials ;
      private GxHttpRequest AV10HttpRequest ;
      private GeneXus.Programs.hsm.SdtHSMcredentialsSDT AV9HSMcredentialsSDT ;
      private string aP0_credentials ;
   }

}
