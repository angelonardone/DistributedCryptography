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
   public class safefilename : GXProcedure
   {
      public safefilename( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public safefilename( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_name ,
                           out string aP1_safeName )
      {
         this.AV8name = aP0_name;
         this.AV9safeName = "" ;
         initialize();
         ExecuteImpl();
         aP1_safeName=this.AV9safeName;
      }

      public string executeUdp( string aP0_name )
      {
         execute(aP0_name, out aP1_safeName);
         return AV9safeName ;
      }

      public void executeSubmit( string aP0_name ,
                                 out string aP1_safeName )
      {
         this.AV8name = aP0_name;
         this.AV9safeName = "" ;
         SubmitImpl();
         aP1_safeName=this.AV9safeName;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          string s = AV8name ?? "";
         /* User Code */
          int cut = System.Math.Max(s.LastIndexOf('/'), s.LastIndexOf('\\'));
         /* User Code */
          if (cut >= 0) s = s.Substring(cut + 1);
         /* User Code */
          var sb = new System.Text.StringBuilder();
         /* User Code */
          foreach (char c in s) { if (c < 32 || c == 127 || "<>:\"/\\|?*".IndexOf(c) >= 0) sb.Append('_'); else sb.Append(c); }
         /* User Code */
          s = sb.ToString().Trim().TrimEnd('.', ' ');
         /* User Code */
          if (s.Length == 0) s = "file";
         /* User Code */
          string stem = s.IndexOf('.') >= 0 ? s.Substring(0, s.IndexOf('.')) : s;
         /* User Code */
          string[] reserved = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
         /* User Code */
          if (System.Array.IndexOf(reserved, stem.Trim().ToUpperInvariant()) >= 0) s = "_" + s;
         /* User Code */
          if (s.Length > 200) { string ext = System.IO.Path.GetExtension(s); if (ext.Length > 20) ext = ""; s = s.Substring(0, 200 - ext.Length) + ext; }
         /* User Code */
          AV9safeName = s;
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
         AV9safeName = "";
         /* GeneXus formulas. */
      }

      private string AV8name ;
      private string AV9safeName ;
      private string aP1_safeName ;
   }

}
