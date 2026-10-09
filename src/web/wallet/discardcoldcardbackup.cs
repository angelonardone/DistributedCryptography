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
   public class discardcoldcardbackup : GXProcedure
   {
      public discardcoldcardbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public discardcoldcardbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9stagedFile = AV10webSession.Get("ColdcardStagedFile");
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9stagedFile)) )
         {
            AV8file.Source = AV9stagedFile;
            if ( AV8file.Exists() )
            {
               AV8file.Delete();
            }
            AV10webSession.Remove("ColdcardStagedFile");
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
         AV9stagedFile = "";
         AV10webSession = context.GetSession();
         AV8file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV9stagedFile ;
      private IGxSession AV10webSession ;
      private GxFile AV8file ;
   }

}
