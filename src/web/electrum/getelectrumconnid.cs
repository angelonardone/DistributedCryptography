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
namespace GeneXus.Programs.electrum {
   public class getelectrumconnid : GXProcedure
   {
      public getelectrumconnid( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getelectrumconnid( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.electrum.SdtConnection aP0_Connection )
      {
         this.AV10Connection = new GeneXus.Programs.electrum.SdtConnection(context) ;
         initialize();
         ExecuteImpl();
         aP0_Connection=this.AV10Connection;
      }

      public GeneXus.Programs.electrum.SdtConnection executeUdp( )
      {
         execute(out aP0_Connection);
         return AV10Connection ;
      }

      public void executeSubmit( out GeneXus.Programs.electrum.SdtConnection aP0_Connection )
      {
         this.AV10Connection = new GeneXus.Programs.electrum.SdtConnection(context) ;
         SubmitImpl();
         aP0_Connection=this.AV10Connection;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10Connection.FromJSonString(AV8WebSession.Get("ELECTRUM_CONNECTION_ID"), null);
         this.cleanup();
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
         AV10Connection = new GeneXus.Programs.electrum.SdtConnection(context);
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV8WebSession ;
      private GeneXus.Programs.electrum.SdtConnection aP0_Connection ;
      private GeneXus.Programs.electrum.SdtConnection AV10Connection ;
   }

}
