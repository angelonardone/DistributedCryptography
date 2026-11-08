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
   public class readjsonencfile : GXProcedure
   {
      public readjsonencfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readjsonencfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fileName ,
                           out string aP1_error ,
                           out string aP2_clearText )
      {
         this.AV9fileName = aP0_fileName;
         this.AV13error = "" ;
         this.AV8clearText = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV13error;
         aP2_clearText=this.AV8clearText;
      }

      public string executeUdp( string aP0_fileName ,
                                out string aP1_error )
      {
         execute(aP0_fileName, out aP1_error, out aP2_clearText);
         return AV8clearText ;
      }

      public void executeSubmit( string aP0_fileName ,
                                 out string aP1_error ,
                                 out string aP2_clearText )
      {
         this.AV9fileName = aP0_fileName;
         this.AV13error = "" ;
         this.AV8clearText = "" ;
         SubmitImpl();
         aP1_error=this.AV13error;
         aP2_clearText=this.AV8clearText;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.walletfilepath(context ).execute(  AV9fileName, out  AV19fullPath, out  AV13error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV8clearText = "";
            cleanup();
            if (true) return;
         }
         AV18file.Source = AV19fullPath;
         if ( AV18file.Exists() )
         {
            AV16json_enc.FromJSonFile(AV18file, null);
            GXt_char1 = AV13error;
            new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV16json_enc.gxTpr_Encryptedtext,  AV16json_enc.gxTpr_Encryptedkey, out  AV8clearText, out  GXt_char1) ;
            AV13error = GXt_char1;
            if ( StringUtil.StrCmp(AV8clearText, "_empty_") == 0 )
            {
               AV8clearText = "";
            }
         }
         else
         {
            GXt_char1 = AV13error;
            GXt_char2 = AV16json_enc.gxTpr_Encryptedkey;
            GXt_char3 = AV16json_enc.gxTpr_Encryptedtext;
            new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  "_empty_",  "", out  GXt_char2, out  GXt_char3, out  GXt_char1) ;
            AV16json_enc.gxTpr_Encryptedkey = GXt_char2;
            AV16json_enc.gxTpr_Encryptedtext = GXt_char3;
            AV13error = GXt_char1;
            AV18file.WriteAllText(AV16json_enc.ToJSonString(false, true), "");
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
         AV13error = "";
         AV8clearText = "";
         AV19fullPath = "";
         AV18file = new GxFile(context.GetPhysicalPath());
         AV16json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GXt_char1 = "";
         GXt_char2 = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private string AV9fileName ;
      private string AV13error ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string AV8clearText ;
      private string AV19fullPath ;
      private GxFile AV18file ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV16json_enc ;
      private string aP1_error ;
      private string aP2_clearText ;
   }

}
