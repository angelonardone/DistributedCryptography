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
   public class getselectedcoins : GXProcedure
   {
      public getselectedcoins( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getselectedcoins( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GxSimpleCollection<string> aP0_outpoints )
      {
         this.AV8outpoints = new GxSimpleCollection<string>() ;
         initialize();
         ExecuteImpl();
         aP0_outpoints=this.AV8outpoints;
      }

      public GxSimpleCollection<string> executeUdp( )
      {
         execute(out aP0_outpoints);
         return AV8outpoints ;
      }

      public void executeSubmit( out GxSimpleCollection<string> aP0_outpoints )
      {
         this.AV8outpoints = new GxSimpleCollection<string>() ;
         SubmitImpl();
         aP0_outpoints=this.AV8outpoints;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8outpoints.FromJSonString(AV9WebSession.Get("CoinControlSelection"), null);
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
         AV8outpoints = new GxSimpleCollection<string>();
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GxSimpleCollection<string> AV8outpoints ;
      private GxSimpleCollection<string> aP0_outpoints ;
   }

}
