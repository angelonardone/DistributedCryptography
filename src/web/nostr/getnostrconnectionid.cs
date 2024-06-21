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
namespace GeneXus.Programs.nostr {
   public class getnostrconnectionid : GXProcedure
   {
      public getnostrconnectionid( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getnostrconnectionid( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out Guid aP0_connectionID )
      {
         this.AV9connectionID = Guid.Empty ;
         initialize();
         ExecuteImpl();
         aP0_connectionID=this.AV9connectionID;
      }

      public Guid executeUdp( )
      {
         execute(out aP0_connectionID);
         return AV9connectionID ;
      }

      public void executeSubmit( out Guid aP0_connectionID )
      {
         this.AV9connectionID = Guid.Empty ;
         SubmitImpl();
         aP0_connectionID=this.AV9connectionID;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9connectionID = StringUtil.StrToGuid( AV8WebSession.Get("ConnectionID"));
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
         AV9connectionID = Guid.Empty;
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private Guid AV9connectionID ;
      private IGxSession AV8WebSession ;
      private Guid aP0_connectionID ;
   }

}
