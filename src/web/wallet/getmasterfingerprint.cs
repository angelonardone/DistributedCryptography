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
namespace GeneXus.Programs.wallet {
   public class getmasterfingerprint : GXProcedure
   {
      public getmasterfingerprint( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getmasterfingerprint( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_fingerprint )
      {
         this.AV8fingerprint = "" ;
         initialize();
         ExecuteImpl();
         aP0_fingerprint=this.AV8fingerprint;
      }

      public string executeUdp( )
      {
         execute(out aP0_fingerprint);
         return AV8fingerprint ;
      }

      public void executeSubmit( out string aP0_fingerprint )
      {
         this.AV8fingerprint = "" ;
         SubmitImpl();
         aP0_fingerprint=this.AV8fingerprint;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8fingerprint = AV9WebSession.Get("MasterFingerprint");
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
         AV8fingerprint = "";
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV8fingerprint ;
      private IGxSession AV9WebSession ;
      private string aP0_fingerprint ;
   }

}
