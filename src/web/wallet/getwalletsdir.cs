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
   public class getwalletsdir : GXProcedure
   {
      public getwalletsdir( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwalletsdir( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_walletsDir )
      {
         this.AV11walletsDir = "" ;
         initialize();
         ExecuteImpl();
         aP0_walletsDir=this.AV11walletsDir;
      }

      public string executeUdp( )
      {
         execute(out aP0_walletsDir);
         return AV11walletsDir ;
      }

      public void executeSubmit( out string aP0_walletsDir )
      {
         this.AV11walletsDir = "" ;
         SubmitImpl();
         aP0_walletsDir=this.AV11walletsDir;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8dataDir;
         new GeneXus.Programs.wallet.getappdatadir(context ).execute( out  GXt_char1) ;
         AV8dataDir = GXt_char1;
         /* User Code */
          string nw = System.IO.Path.Combine(AV8dataDir.Trim(), "Wallets");
         /* User Code */
          System.IO.Directory.CreateDirectory(nw);
         /* User Code */
          AV11walletsDir = nw;
         AV9oldDir.Source = "Wallets";
         if ( AV9oldDir.Exists() )
         {
            AV10oldPath = AV9oldDir.GetAbsoluteName();
            /* User Code */
             string oldP = System.IO.Path.GetFullPath(AV10oldPath.Trim()).TrimEnd('\\', '/');
            /* User Code */
             if (!oldP.Equals(nw.TrimEnd('\\', '/'), System.StringComparison.OrdinalIgnoreCase))
            /* User Code */
             {
            /* User Code */
                 foreach (var src in System.IO.Directory.GetFileSystemEntries(oldP))
            /* User Code */
                 {
            /* User Code */
                     string dst = System.IO.Path.Combine(nw, System.IO.Path.GetFileName(src));
            /* User Code */
                     try
            /* User Code */
                     {
            /* User Code */
                         if (System.IO.Directory.Exists(src)) { if (!System.IO.Directory.Exists(dst)) MoveDirectory(src, dst); }
            /* User Code */
                         else if (!System.IO.File.Exists(dst)) System.IO.File.Move(src, dst);
            /* User Code */
                     }
            /* User Code */
                     catch (System.Exception ex) { System.Console.Error.WriteLine("Wallets migration, " + src + ": " + ex.Message); }
            /* User Code */
                 }
            /* User Code */
                 try { if (System.IO.Directory.GetFileSystemEntries(oldP).Length == 0) System.IO.Directory.Delete(oldP); } catch { }
            /* User Code */
             }
         }
         /* User Code */
          static void MoveDirectory(string src, string dst)
         /* User Code */
          {
         /* User Code */
              if (string.Equals(System.IO.Path.GetPathRoot(src), System.IO.Path.GetPathRoot(dst), System.StringComparison.OrdinalIgnoreCase))
         /* User Code */
              {
         /* User Code */
                  System.IO.Directory.Move(src, dst);
         /* User Code */
                  return;
         /* User Code */
              }
         /* User Code */
              CopyDirectory(src, dst);
         /* User Code */
              try { System.IO.Directory.Delete(src, true); }
         /* User Code */
              catch { try { System.IO.Directory.Delete(dst, true); } catch { } throw; }
         /* User Code */
          }
         /* User Code */
          static void CopyDirectory(string src, string dst)
         /* User Code */
          {
         /* User Code */
              System.IO.Directory.CreateDirectory(dst);
         /* User Code */
              foreach (var f in System.IO.Directory.GetFiles(src)) System.IO.File.Copy(f, System.IO.Path.Combine(dst, System.IO.Path.GetFileName(f)), false);
         /* User Code */
              foreach (var sub in System.IO.Directory.GetDirectories(src)) CopyDirectory(sub, System.IO.Path.Combine(dst, System.IO.Path.GetFileName(sub)));
         /* User Code */
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
         AV11walletsDir = "";
         AV8dataDir = "";
         GXt_char1 = "";
         AV9oldDir = new GxDirectory(context.GetPhysicalPath());
         AV10oldPath = "";
         /* GeneXus formulas. */
      }

      private string GXt_char1 ;
      private string AV11walletsDir ;
      private string AV8dataDir ;
      private string AV10oldPath ;
      private GxDirectory AV9oldDir ;
      private string aP0_walletsDir ;
   }

}
