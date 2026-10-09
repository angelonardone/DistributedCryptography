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
   public class getappdatadir : GXProcedure
   {
      public getappdatadir( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getappdatadir( IGxContext context )
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
         AV9webDir.Source = ".";
         AV10webPath = AV9webDir.GetAbsoluteName();
         /* User Code */
          string d = System.Environment.GetEnvironmentVariable("DISTCRYPT_DATA_DIR");
         /* User Code */
          string home = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
         /* User Code */
          if (string.IsNullOrWhiteSpace(home)) home = System.Environment.GetEnvironmentVariable("HOME");
         /* User Code */
          if (string.IsNullOrWhiteSpace(home)) home = System.OperatingSystem.IsWindows() ? "C:\\" : "/data";
         /* User Code */
          string def = System.IO.Path.Combine(home, "DistributedCryptography");
         /* User Code */
          if (string.IsNullOrWhiteSpace(d)) d = def;
         /* User Code */
          d = System.IO.Path.GetFullPath(d.Trim());
         /* User Code */
          string web = System.IO.Path.GetFullPath(AV10webPath.Trim()).TrimEnd('\\', '/');
         /* User Code */
          if (d.TrimEnd('\\', '/').Equals(web, System.StringComparison.OrdinalIgnoreCase) || d.StartsWith(web + System.IO.Path.DirectorySeparatorChar, System.StringComparison.OrdinalIgnoreCase))
         /* User Code */
          {
         /* User Code */
              System.Console.Error.WriteLine("DISTCRYPT_DATA_DIR points inside the web folder (" + d + "): ignored, using " + def);
         /* User Code */
              d = def;
         /* User Code */
          }
         /* User Code */
          System.IO.Directory.CreateDirectory(d);
         /* User Code */
          AV8dir = d;
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
         AV9webDir = new GxDirectory(context.GetPhysicalPath());
         AV10webPath = "";
         /* GeneXus formulas. */
      }

      private string AV8dir ;
      private string AV10webPath ;
      private GxDirectory AV9webDir ;
      private string aP0_dir ;
   }

}
