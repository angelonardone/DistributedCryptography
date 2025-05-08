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
namespace GeneXus.Programs.distributedcrypto {
   public class getunixtimemilisecondsutc : GXProcedure
   {
      public getunixtimemilisecondsutc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getunixtimemilisecondsutc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out long aP0_miliseconds )
      {
         this.AV8miliseconds = 0 ;
         initialize();
         ExecuteImpl();
         aP0_miliseconds=this.AV8miliseconds;
      }

      public long executeUdp( )
      {
         execute(out aP0_miliseconds);
         return AV8miliseconds ;
      }

      public void executeSubmit( out long aP0_miliseconds )
      {
         this.AV8miliseconds = 0 ;
         SubmitImpl();
         aP0_miliseconds=this.AV8miliseconds;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          long seconds = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000);
         /* User Code */
          AV8miliseconds = seconds;
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

      private long AV8miliseconds ;
      private long aP0_miliseconds ;
   }

}
