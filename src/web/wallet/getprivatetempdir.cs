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
   public class getprivatetempdir : GXProcedure
   {
      public getprivatetempdir( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getprivatetempdir( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_dir )
      {
         this.AV8dir = "" ;
         initialize();
         ExecuteImpl();
         aP0_dir=this.AV8dir;
      }

      public string executeUdp( )
      {
         execute(out aP0_dir);
         return AV8dir ;
      }

      public void executeSubmit( out string aP0_dir )
      {
         this.AV8dir = "" ;
         SubmitImpl();
         aP0_dir=this.AV8dir;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          string dir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DistCryptPrivate");
         /* User Code */
          System.IO.Directory.CreateDirectory(dir);
         /* User Code */
          foreach (var f in System.IO.Directory.GetFiles(dir))
         /* User Code */
          {
         /* User Code */
              try { if (System.IO.File.GetLastWriteTimeUtc(f) < System.DateTime.UtcNow.AddMinutes(-15)) System.IO.File.Delete(f); } catch { }
         /* User Code */
          }
         /* User Code */
          AV8dir = dir;
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
         AV8dir = "";
         /* GeneXus formulas. */
      }

      private string AV8dir ;
      private string aP0_dir ;
   }

}
