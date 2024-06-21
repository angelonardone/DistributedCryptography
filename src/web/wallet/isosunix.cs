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
namespace GeneXus.Programs.wallet {
   public class isosunix : GXProcedure
   {
      public isosunix( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public isosunix( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out bool aP0_isUnix )
      {
         this.AV8isUnix = false ;
         initialize();
         ExecuteImpl();
         aP0_isUnix=this.AV8isUnix;
      }

      public bool executeUdp( )
      {
         execute(out aP0_isUnix);
         return AV8isUnix ;
      }

      public void executeSubmit( out bool aP0_isUnix )
      {
         this.AV8isUnix = false ;
         SubmitImpl();
         aP0_isUnix=this.AV8isUnix;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          OperatingSystem os = Environment.OSVersion;
         /* User Code */
          PlatformID     pid = os.Platform;
         /* User Code */
          bool isUnix = pid == PlatformID.Unix;
         /* User Code */
          AV8isUnix = isUnix;
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
         /* GeneXus formulas. */
      }

      private bool AV8isUnix ;
      private bool aP0_isUnix ;
   }

}
