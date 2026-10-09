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
   public class deletewalletnote : GXProcedure
   {
      public deletewalletnote( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletewalletnote( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_noteFileName ,
                           out string aP1_error )
      {
         this.AV10noteFileName = aP0_noteFileName;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( string aP0_noteFileName )
      {
         execute(aP0_noteFileName, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_noteFileName ,
                                 out string aP1_error )
      {
         this.AV10noteFileName = aP0_noteFileName;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         /* User Code */
          AV11plainName = System.IO.Path.GetFileName(AV10noteFileName.Trim());
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV11plainName))) || ( StringUtil.StringSearch( AV11plainName, "..", 1) > 0 ) )
         {
            AV8error = "Invalid note file name";
         }
         else
         {
            GXt_SdtWallet1 = AV12wallet;
            new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
            AV12wallet = GXt_SdtWallet1;
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            AV9file.Source = AV12wallet.gxTpr_Walletbasedirectory+"Notes"+(GXt_boolean3 ? "/" : "\\")+StringUtil.Trim( AV11plainName);
            if ( AV9file.Exists() )
            {
               AV9file.Delete();
            }
            else
            {
               AV8error = "The file does not exist";
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
         AV11plainName = "";
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV10noteFileName ;
      private string AV8error ;
      private string AV11plainName ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private GxFile AV9file ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP1_error ;
   }

}
