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
         GXt_SdtWallet1 = AV17wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV17wallet = GXt_SdtWallet1;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV18file.Source = AV17wallet.gxTpr_Walletbasedirectory+(GXt_boolean3 ? "/" : "\\")+StringUtil.Trim( AV9fileName);
         if ( AV18file.Exists() )
         {
            AV16json_enc.FromJSonFile(AV18file, null);
            GXt_char4 = AV13error;
            new GeneXus.Programs.distributedcrypto.decryptjson(context ).execute(  AV16json_enc.gxTpr_Encryptedtext,  AV16json_enc.gxTpr_Encryptedkey,  AV16json_enc.gxTpr_Iv, out  AV8clearText, out  GXt_char4) ;
            AV13error = GXt_char4;
            if ( StringUtil.StrCmp(AV8clearText, "_empty_") == 0 )
            {
               AV8clearText = "";
            }
         }
         else
         {
            GXt_char4 = AV13error;
            GXt_char5 = AV16json_enc.gxTpr_Encryptedkey;
            GXt_char6 = AV16json_enc.gxTpr_Iv;
            GXt_char7 = AV16json_enc.gxTpr_Encryptedtext;
            new GeneXus.Programs.distributedcrypto.encryptjson(context ).execute(  "_empty_",  "", out  GXt_char5, out  GXt_char6, out  GXt_char7, out  GXt_char4) ;
            AV16json_enc.gxTpr_Encryptedkey = GXt_char5;
            AV16json_enc.gxTpr_Iv = GXt_char6;
            AV16json_enc.gxTpr_Encryptedtext = GXt_char7;
            AV13error = GXt_char4;
            AV18file.WriteAllText(AV16json_enc.ToJSonString(false, true), "");
         }
         this.cleanup();
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
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV18file = new GxFile(context.GetPhysicalPath());
         AV16json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GXt_char4 = "";
         GXt_char5 = "";
         GXt_char6 = "";
         GXt_char7 = "";
         /* GeneXus formulas. */
      }

      private string AV9fileName ;
      private string AV13error ;
      private string GXt_char4 ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string GXt_char7 ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private string AV8clearText ;
      private string aP1_error ;
      private string aP2_clearText ;
      private GxFile AV18file ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV16json_enc ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
   }

}
