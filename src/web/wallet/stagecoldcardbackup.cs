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
   public class stagecoldcardbackup : GXProcedure
   {
      public stagecoldcardbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public stagecoldcardbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_coldCardPassword ,
                           string aP1_uploadedFile ,
                           out string aP2_error )
      {
         this.AV8coldCardPassword = aP0_coldCardPassword;
         this.AV12uploadedFile = aP1_uploadedFile;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_coldCardPassword ,
                                string aP1_uploadedFile )
      {
         execute(aP0_coldCardPassword, aP1_uploadedFile, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_coldCardPassword ,
                                 string aP1_uploadedFile ,
                                 out string aP2_error )
      {
         this.AV8coldCardPassword = aP0_coldCardPassword;
         this.AV12uploadedFile = aP1_uploadedFile;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.discardcoldcardbackup(context ).execute( ) ;
         GXt_char1 = AV9error;
         new GeneXus.Programs.distributedcrypto.decryptcoldcardbackup(context ).execute(  StringUtil.Trim( AV8coldCardPassword),  AV12uploadedFile, out  AV10mnemonic, out  GXt_char1) ;
         AV9error = GXt_char1;
         AV10mnemonic = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
                 string staged = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dc-coldcard-" + System.Guid.NewGuid().ToString("N") + ".7z");
            /* User Code */
                 System.IO.File.Copy(AV12uploadedFile, staged, true);
            /* User Code */
                 AV11stagedFile = staged;
            /* User Code */
             }
            /* User Code */
             catch (Exception ex)
            /* User Code */
             {
            /* User Code */
                 AV9error = ex.Message.ToString();
            /* User Code */
             }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV13webSession.Set("ColdcardStagedFile", AV11stagedFile);
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
         AV9error = "";
         GXt_char1 = "";
         AV10mnemonic = "";
         AV11stagedFile = "";
         AV13webSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char1 ;
      private string AV8coldCardPassword ;
      private string AV10mnemonic ;
      private string AV12uploadedFile ;
      private string AV11stagedFile ;
      private IGxSession AV13webSession ;
      private string aP2_error ;
   }

}
