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
   public class setexchangedir : GXProcedure
   {
      public setexchangedir( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setexchangedir( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_newDir ,
                           out string aP1_error )
      {
         this.AV9newDir = aP0_newDir;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( string aP0_newDir )
      {
         execute(aP0_newDir, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_newDir ,
                                 out string aP1_error )
      {
         this.AV9newDir = aP0_newDir;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         GXt_SdtWallet1 = AV11wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV11wallet = GXt_SdtWallet1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV11wallet.gxTpr_Walletname))) )
         {
            AV8error = "Open a wallet first";
         }
         else
         {
            AV10toStore = "default";
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV9newDir))) )
            {
               AV12webDir.Source = ".";
               AV13webPath = AV12webDir.GetAbsoluteName();
               /* User Code */
                string d = AV9newDir.Trim(), msg = "";
               /* User Code */
                if (!System.IO.Path.IsPathRooted(d)) msg = "Type a full path (for example C:\\Exchange or /data/exchange)";
               /* User Code */
                else
               /* User Code */
                {
               /* User Code */
                    d = System.IO.Path.GetFullPath(d);
               /* User Code */
                    string web = System.IO.Path.GetFullPath(AV13webPath.Trim()).TrimEnd('\\', '/');
               /* User Code */
                    if (d.TrimEnd('\\', '/').Equals(web, System.StringComparison.OrdinalIgnoreCase) || d.StartsWith(web + System.IO.Path.DirectorySeparatorChar, System.StringComparison.OrdinalIgnoreCase))
               /* User Code */
                        msg = "The folder cannot be inside the web folder of the application";
               /* User Code */
                    else
               /* User Code */
                    {
               /* User Code */
                        try { foreach (var sub in new[] { "Received", "Decrypted", "To encrypt", "To send" }) System.IO.Directory.CreateDirectory(System.IO.Path.Combine(d, sub)); }
               /* User Code */
                        catch (System.Exception ex) { msg = "The folder cannot be used: " + ex.Message; }
               /* User Code */
                    }
               /* User Code */
                }
               /* User Code */
                AV8error = msg;
               /* User Code */
                AV10toStore = d;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_char2 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "exchange.conf",  AV10toStore, out  GXt_char2) ;
               AV8error = GXt_char2;
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
         AV8error = "";
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV10toStore = "";
         AV12webDir = new GxDirectory(context.GetPhysicalPath());
         AV13webPath = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string AV9newDir ;
      private string AV8error ;
      private string AV10toStore ;
      private string AV13webPath ;
      private GxDirectory AV12webDir ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP1_error ;
   }

}
