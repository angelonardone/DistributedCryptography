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
   public class encryptexchangefile : GXProcedure
   {
      public encryptexchangefile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptexchangefile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fileName ,
                           string aP1_recipientUserName ,
                           out string aP2_resultPath ,
                           out string aP3_error )
      {
         this.AV10fileName = aP0_fileName;
         this.AV13recipientUserName = aP1_recipientUserName;
         this.AV14resultPath = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_resultPath=this.AV14resultPath;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_fileName ,
                                string aP1_recipientUserName ,
                                out string aP2_resultPath )
      {
         execute(aP0_fileName, aP1_recipientUserName, out aP2_resultPath, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_fileName ,
                                 string aP1_recipientUserName ,
                                 out string aP2_resultPath ,
                                 out string aP3_error )
      {
         this.AV10fileName = aP0_fileName;
         this.AV13recipientUserName = aP1_recipientUserName;
         this.AV14resultPath = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_resultPath=this.AV14resultPath;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         AV14resultPath = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV13recipientUserName))) )
         {
            AV9error = "Type the user name of the recipient";
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            new GeneXus.Programs.wallet.getexchangedir(context ).execute( out  AV8dir, out  AV9error) ;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             string src = System.IO.Path.Combine(AV8dir.Trim(), "To encrypt", System.IO.Path.GetFileName((AV10fileName ?? "").Trim()));
            /* User Code */
             AV16source = src;
            /* User Code */
             AV11partial = System.IO.Path.Combine(AV8dir.Trim(), "To send", System.Guid.NewGuid().ToString("N") + ".partial");
            /* User Code */
             if (!System.IO.File.Exists(src)) AV9error = "The file is not in the To encrypt folder any more: " + src;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.registered.getuserpubkey(context ).execute(  StringUtil.Trim( AV13recipientUserName), out  AV12recipientPubKey, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) && String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12recipientPubKey))) )
            {
               AV9error = "The user " + StringUtil.Trim( AV13recipientUserName) + " was not found";
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV15safeName;
            new GeneXus.Programs.wallet.safefilename(context ).execute(  AV10fileName, out  GXt_char1) ;
            AV15safeName = GXt_char1;
            GXt_char1 = AV9error;
            new GeneXus.Programs.distributedcrypto.fileencryptv2(context ).execute(  AV16source,  AV11partial,  AV12recipientPubKey,  AV15safeName, out  GXt_char1) ;
            AV9error = GXt_char1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             string folder = System.IO.Path.Combine(AV8dir.Trim(), "To send"), name = AV15safeName.Trim() + ".dcf";
            /* User Code */
             string target = System.IO.Path.Combine(folder, name);
            /* User Code */
             for (int i = 2; System.IO.File.Exists(target); i++) target = System.IO.Path.Combine(folder, AV15safeName.Trim() + " (" + i + ").dcf");
            /* User Code */
             try { System.IO.File.Move(AV11partial.Trim(), target); AV14resultPath = target; }
            /* User Code */
             catch (System.Exception ex) { try { System.IO.File.Delete(AV11partial.Trim()); } catch { } AV9error = "The encrypted file could not be saved: " + ex.Message; }
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
         AV14resultPath = "";
         AV9error = "";
         AV8dir = "";
         AV16source = "";
         AV11partial = "";
         AV12recipientPubKey = "";
         AV15safeName = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV12recipientPubKey ;
      private string GXt_char1 ;
      private string AV10fileName ;
      private string AV13recipientUserName ;
      private string AV14resultPath ;
      private string AV9error ;
      private string AV8dir ;
      private string AV16source ;
      private string AV11partial ;
      private string AV15safeName ;
      private string aP2_resultPath ;
      private string aP3_error ;
   }

}
