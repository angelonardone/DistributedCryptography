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
   public class getexchangedir : GXProcedure
   {
      public getexchangedir( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getexchangedir( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_dir ,
                           out string aP1_error )
      {
         this.AV9dir = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP0_dir=this.AV9dir;
         aP1_error=this.AV10error;
      }

      public string executeUdp( out string aP0_dir )
      {
         execute(out aP0_dir, out aP1_error);
         return AV10error ;
      }

      public void executeSubmit( out string aP0_dir ,
                                 out string aP1_error )
      {
         this.AV9dir = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP0_dir=this.AV9dir;
         aP1_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         AV9dir = "";
         GXt_SdtWallet1 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV12wallet = GXt_SdtWallet1;
         AV15walletName = StringUtil.Trim( AV12wallet.gxTpr_Walletname);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15walletName)) )
         {
            AV10error = "Open a wallet first";
         }
         else
         {
            GXt_char2 = AV11stored;
            new GeneXus.Programs.wallet.readjsonencfile(context ).execute(  "exchange.conf", out  AV16readError, out  GXt_char2) ;
            AV11stored = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16readError)) )
            {
               AV11stored = "";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               GXt_char2 = AV8dataDir;
               new GeneXus.Programs.wallet.getappdatadir(context ).execute( out  GXt_char2) ;
               AV8dataDir = GXt_char2;
               AV13webDir.Source = ".";
               AV14webPath = AV13webDir.GetAbsoluteName();
               /* User Code */
                string d = (AV11stored ?? "").Trim();
               /* User Code */
                string def = System.IO.Path.Combine(AV8dataDir.Trim(), "Exchange", AV15walletName.Trim());
               /* User Code */
                string web = System.IO.Path.GetFullPath(AV14webPath.Trim()).TrimEnd('\\', '/');
               /* User Code */
                if (d.Length == 0 || !System.IO.Path.IsPathRooted(d)) d = def;
               /* User Code */
                d = System.IO.Path.GetFullPath(d);
               /* User Code */
                if (d.TrimEnd('\\', '/').Equals(web, System.StringComparison.OrdinalIgnoreCase) || d.StartsWith(web + System.IO.Path.DirectorySeparatorChar, System.StringComparison.OrdinalIgnoreCase)) d = def;
               /* User Code */
                try
               /* User Code */
                {
               /* User Code */
                    foreach (var sub in new[] { "Received", "Decrypted", "To encrypt", "To send" }) System.IO.Directory.CreateDirectory(System.IO.Path.Combine(d, sub));
               /* User Code */
                    AV9dir = d;
               /* User Code */
                }
               /* User Code */
                catch (System.Exception ex) { AV10error = "The exchange folder cannot be used (" + d + "): " + ex.Message; }
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
         AV9dir = "";
         AV10error = "";
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV15walletName = "";
         AV11stored = "";
         AV16readError = "";
         AV8dataDir = "";
         GXt_char2 = "";
         AV13webDir = new GxDirectory(context.GetPhysicalPath());
         AV14webPath = "";
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string AV11stored ;
      private string AV9dir ;
      private string AV10error ;
      private string AV15walletName ;
      private string AV16readError ;
      private string AV8dataDir ;
      private string AV14webPath ;
      private GxDirectory AV13webDir ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP0_dir ;
      private string aP1_error ;
   }

}
