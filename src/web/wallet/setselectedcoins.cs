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
   public class setselectedcoins : GXProcedure
   {
      public setselectedcoins( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setselectedcoins( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_outpoints )
      {
         this.AV8outpoints = aP0_outpoints;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_outpoints )
      {
         this.AV8outpoints = aP0_outpoints;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( AV8outpoints.Count == 0 )
         {
            AV9WebSession.Set("CoinControlSelection", "");
         }
         else
         {
            AV9WebSession.Set("CoinControlSelection", AV8outpoints.ToJSonString(false));
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
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GxSimpleCollection<string> AV8outpoints ;
   }

}
