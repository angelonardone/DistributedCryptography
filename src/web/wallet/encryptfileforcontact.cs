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
   public class encryptfileforcontact : GXProcedure
   {
      public encryptfileforcontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptfileforcontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_uploadedFile ,
                           string aP1_originalName ,
                           string aP2_recipientPubKey ,
                           out string aP3_token ,
                           out string aP4_error )
      {
         this.AV16uploadedFile = aP0_uploadedFile;
         this.AV10originalName = aP1_originalName;
         this.AV13recipientPubKey = aP2_recipientPubKey;
         this.AV15token = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_token=this.AV15token;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_uploadedFile ,
                                string aP1_originalName ,
                                string aP2_recipientPubKey ,
                                out string aP3_token )
      {
         execute(aP0_uploadedFile, aP1_originalName, aP2_recipientPubKey, out aP3_token, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_uploadedFile ,
                                 string aP1_originalName ,
                                 string aP2_recipientPubKey ,
                                 out string aP3_token ,
                                 out string aP4_error )
      {
         this.AV16uploadedFile = aP0_uploadedFile;
         this.AV10originalName = aP1_originalName;
         this.AV13recipientPubKey = aP2_recipientPubKey;
         this.AV15token = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_token=this.AV15token;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         AV15token = "";
         GXt_char1 = AV14safeName;
         new GeneXus.Programs.wallet.safefilename(context ).execute(  AV10originalName, out  GXt_char1) ;
         AV14safeName = GXt_char1;
         GXt_char1 = AV12privateDir;
         new GeneXus.Programs.wallet.getprivatetempdir(context ).execute( out  GXt_char1) ;
         AV12privateDir = GXt_char1;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV11outputFile = StringUtil.Trim( AV12privateDir) + (GXt_boolean3 ? "/" : "\\") + Guid.NewGuid( ).ToString();
         GXt_char1 = AV8error;
         new GeneXus.Programs.distributedcrypto.fileencryptv2(context ).execute(  AV16uploadedFile,  AV11outputFile,  AV13recipientPubKey,  AV14safeName, out  GXt_char1) ;
         AV8error = GXt_char1;
         AV9file.Source = AV16uploadedFile;
         if ( AV9file.Exists() )
         {
            AV9file.Delete();
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_char1 = AV15token;
            new GeneXus.Programs.wallet.stagedownload(context ).execute(  AV11outputFile,  StringUtil.Trim( AV14safeName)+".dcf", out  GXt_char1) ;
            AV15token = GXt_char1;
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
         AV15token = "";
         AV8error = "";
         AV14safeName = "";
         AV12privateDir = "";
         AV11outputFile = "";
         AV9file = new GxFile(context.GetPhysicalPath());
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char1 ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private string AV16uploadedFile ;
      private string AV10originalName ;
      private string AV13recipientPubKey ;
      private string AV15token ;
      private string AV8error ;
      private string AV14safeName ;
      private string AV12privateDir ;
      private string AV11outputFile ;
      private GxFile AV9file ;
      private string aP3_token ;
      private string aP4_error ;
   }

}
