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
   public class decryptreceivedfile : GXProcedure
   {
      public decryptreceivedfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptreceivedfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_uploadedFile ,
                           out string aP1_token ,
                           out string aP2_error )
      {
         this.AV15uploadedFile = aP0_uploadedFile;
         this.AV14token = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_token=this.AV14token;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_uploadedFile ,
                                out string aP1_token )
      {
         execute(aP0_uploadedFile, out aP1_token, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_uploadedFile ,
                                 out string aP1_token ,
                                 out string aP2_error )
      {
         this.AV15uploadedFile = aP0_uploadedFile;
         this.AV14token = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_token=this.AV14token;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         AV14token = "";
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         GXt_char2 = AV12privateDir;
         new GeneXus.Programs.wallet.getprivatetempdir(context ).execute( out  GXt_char2) ;
         AV12privateDir = GXt_char2;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         GXt_boolean4 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
         AV11outputFile = StringUtil.Trim( AV12privateDir) + (GXt_boolean4 ? "/" : "\\") + Guid.NewGuid( ).ToString();
         GXt_char2 = AV8error;
         new GeneXus.Programs.distributedcrypto.filedecryptv2(context ).execute(  AV15uploadedFile,  AV11outputFile,  StringUtil.Trim( AV9externalUser.gxTpr_Keyinfo.gxTpr_Privatekey), out  AV13senderName, out  GXt_char2) ;
         AV8error = GXt_char2;
         AV10file.Source = AV15uploadedFile;
         if ( AV10file.Exists() )
         {
            AV10file.Delete();
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_char2 = AV14token;
            GXt_char5 = AV14token;
            new GeneXus.Programs.wallet.safefilename(context ).execute(  AV13senderName, out  GXt_char5) ;
            new GeneXus.Programs.wallet.stagedownload(context ).execute(  AV11outputFile,  GXt_char5, out  GXt_char2) ;
            AV14token = GXt_char2;
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
         AV14token = "";
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV12privateDir = "";
         AV11outputFile = "";
         AV13senderName = "";
         AV10file = new GxFile(context.GetPhysicalPath());
         GXt_char2 = "";
         GXt_char5 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string GXt_char5 ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean4 ;
      private string AV15uploadedFile ;
      private string AV14token ;
      private string AV8error ;
      private string AV12privateDir ;
      private string AV11outputFile ;
      private string AV13senderName ;
      private GxFile AV10file ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP1_token ;
      private string aP2_error ;
   }

}
