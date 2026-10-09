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
   public class decryptexchangefile : GXProcedure
   {
      public decryptexchangefile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptexchangefile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fileName ,
                           out string aP1_resultPath ,
                           out string aP2_error )
      {
         this.AV11fileName = aP0_fileName;
         this.AV13resultPath = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_resultPath=this.AV13resultPath;
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_fileName ,
                                out string aP1_resultPath )
      {
         execute(aP0_fileName, out aP1_resultPath, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_fileName ,
                                 out string aP1_resultPath ,
                                 out string aP2_error )
      {
         this.AV11fileName = aP0_fileName;
         this.AV13resultPath = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_resultPath=this.AV13resultPath;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         AV13resultPath = "";
         new GeneXus.Programs.wallet.getexchangedir(context ).execute( out  AV8dir, out  AV9error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             string src = System.IO.Path.Combine(AV8dir.Trim(), "Received", System.IO.Path.GetFileName((AV11fileName ?? "").Trim()));
            /* User Code */
             AV16source = src;
            /* User Code */
             AV12partial = System.IO.Path.Combine(AV8dir.Trim(), "Decrypted", System.Guid.NewGuid().ToString("N") + ".partial");
            /* User Code */
             if (!System.IO.File.Exists(src)) AV9error = "The file is not in the Received folder any more: " + src;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_SdtExternalUser1 = AV10externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
            AV10externalUser = GXt_SdtExternalUser1;
            GXt_char2 = AV9error;
            new GeneXus.Programs.distributedcrypto.filedecryptv2(context ).execute(  AV16source,  AV12partial,  StringUtil.Trim( AV10externalUser.gxTpr_Keyinfo.gxTpr_Privatekey), out  AV15senderName, out  GXt_char2) ;
            AV9error = GXt_char2;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char2 = AV14safeName;
            new GeneXus.Programs.wallet.safefilename(context ).execute(  AV15senderName, out  GXt_char2) ;
            AV14safeName = GXt_char2;
            /* User Code */
             string folder = System.IO.Path.Combine(AV8dir.Trim(), "Decrypted"), name = AV14safeName.Trim();
            /* User Code */
             string target = System.IO.Path.Combine(folder, name);
            /* User Code */
             for (int i = 2; System.IO.File.Exists(target); i++) target = System.IO.Path.Combine(folder, System.IO.Path.GetFileNameWithoutExtension(name) + " (" + i + ")" + System.IO.Path.GetExtension(name));
            /* User Code */
             try { System.IO.File.Move(AV12partial.Trim(), target); AV13resultPath = target; }
            /* User Code */
             catch (System.Exception ex) { try { System.IO.File.Delete(AV12partial.Trim()); } catch { } AV9error = "The decrypted file could not be saved: " + ex.Message; }
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
         AV13resultPath = "";
         AV9error = "";
         AV8dir = "";
         AV16source = "";
         AV12partial = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV15senderName = "";
         AV14safeName = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string AV11fileName ;
      private string AV13resultPath ;
      private string AV9error ;
      private string AV8dir ;
      private string AV16source ;
      private string AV12partial ;
      private string AV15senderName ;
      private string AV14safeName ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP1_resultPath ;
      private string aP2_error ;
   }

}
