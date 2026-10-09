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
   public class listexchangefiles : GXProcedure
   {
      public listexchangefiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public listexchangefiles( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_folderKind ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> aP1_files )
      {
         this.AV11folderKind = aP0_folderKind;
         this.AV10files = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP1_files=this.AV10files;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> executeUdp( string aP0_folderKind )
      {
         execute(aP0_folderKind, out aP1_files);
         return AV10files ;
      }

      public void executeSubmit( string aP0_folderKind ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> aP1_files )
      {
         this.AV11folderKind = aP0_folderKind;
         this.AV10files = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography") ;
         SubmitImpl();
         aP1_files=this.AV10files;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10files.Clear();
         new GeneXus.Programs.wallet.getexchangedir(context ).execute( out  AV8dir, out  AV9error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             string sub = AV11folderKind.Trim() == "ToEncrypt" ? "To encrypt" : "Received";
            /* User Code */
             var items = new System.Collections.Generic.List<object>();
            /* User Code */
             var di = new System.IO.DirectoryInfo(System.IO.Path.Combine(AV8dir.Trim(), sub));
            /* User Code */
             if (di.Exists)
            /* User Code */
             {
            /* User Code */
                 foreach (var f in System.Linq.Enumerable.OrderBy(di.GetFiles(), x => x.Name, System.StringComparer.OrdinalIgnoreCase))
            /* User Code */
                 {
            /* User Code */
                     if (f.Name.EndsWith(".partial", System.StringComparison.OrdinalIgnoreCase)) continue;
            /* User Code */
                     double b = f.Length; string[] u = { "bytes", "KB", "MB", "GB", "TB" }; int k = 0;
            /* User Code */
                     while (b >= 1024 && k < u.Length - 1) { b /= 1024; k++; }
            /* User Code */
                     items.Add(new System.Collections.Generic.Dictionary<string, object> {
            /* User Code */
                         ["FileName"] = f.Name,
            /* User Code */
                         ["FileSize"] = k == 0 ? f.Length + " bytes" : b.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + " " + u[k],
            /* User Code */
                         ["Modified"] = f.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:ss") });
            /* User Code */
                 }
            /* User Code */
             }
            /* User Code */
             AV12json = System.Text.Json.JsonSerializer.Serialize(items);
            AV10files.FromJSonString(AV12json, null);
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
         AV10files = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography");
         AV8dir = "";
         AV9error = "";
         AV12json = "";
         /* GeneXus formulas. */
      }

      private string AV12json ;
      private string AV11folderKind ;
      private string AV8dir ;
      private string AV9error ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> AV10files ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> aP1_files ;
   }

}
