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
   public class walletfilepath : GXProcedure
   {
      public walletfilepath( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public walletfilepath( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fileName ,
                           out string aP1_fullPath ,
                           out string aP2_error )
      {
         this.AV10fileName = aP0_fileName;
         this.AV11fullPath = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_fullPath=this.AV11fullPath;
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_fileName ,
                                out string aP1_fullPath )
      {
         execute(aP0_fileName, out aP1_fullPath, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_fileName ,
                                 out string aP1_fullPath ,
                                 out string aP2_error )
      {
         this.AV10fileName = aP0_fileName;
         this.AV11fullPath = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_fullPath=this.AV11fullPath;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11fullPath = "";
         AV9error = "";
         GXt_SdtWallet1 = AV13wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV13wallet = GXt_SdtWallet1;
         AV8baseDirectory = StringUtil.Trim( AV13wallet.gxTpr_Walletbasedirectory);
         AV12name = StringUtil.Trim( AV10fileName);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8baseDirectory)) )
         {
            AV9error = "The wallet is not open";
         }
         else
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12name)) )
            {
               AV9error = "No file name";
            }
            else
            {
               /* User Code */
                try { char sep = System.IO.Path.DirectorySeparatorChar; string b = System.IO.Path.GetFullPath(AV8baseDirectory); if (!b.EndsWith(sep.ToString())) b += sep; string n = AV12name; if (sep == '/') n = n.Replace('\\', '/'); string f = System.IO.Path.GetFullPath(System.IO.Path.IsPathRooted(n) ? n : System.IO.Path.Combine(b, n)); bool inside = f.Length > b.Length && f.StartsWith(b, System.OperatingSystem.IsWindows() ? System.StringComparison.OrdinalIgnoreCase : System.StringComparison.Ordinal); AV11fullPath = inside ? f : ""; } catch (System.Exception) { AV11fullPath = ""; }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11fullPath)) )
               {
                  AV9error = "Invalid file name";
               }
            }
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
         AV11fullPath = "";
         AV9error = "";
         AV13wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8baseDirectory = "";
         AV12name = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string AV10fileName ;
      private string AV11fullPath ;
      private string AV8baseDirectory ;
      private string AV12name ;
      private GeneXus.Programs.wallet.SdtWallet AV13wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP1_fullPath ;
      private string aP2_error ;
   }

}
