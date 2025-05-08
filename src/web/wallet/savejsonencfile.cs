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
   public class savejsonencfile : GXProcedure
   {
      public savejsonencfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savejsonencfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fileName ,
                           string aP1_clearText ,
                           out string aP2_error )
      {
         this.AV9fileName = aP0_fileName;
         this.AV8clearText = aP1_clearText;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV13error;
      }

      public string executeUdp( string aP0_fileName ,
                                string aP1_clearText )
      {
         execute(aP0_fileName, aP1_clearText, out aP2_error);
         return AV13error ;
      }

      public void executeSubmit( string aP0_fileName ,
                                 string aP1_clearText ,
                                 out string aP2_error )
      {
         this.AV9fileName = aP0_fileName;
         this.AV8clearText = aP1_clearText;
         this.AV13error = "" ;
         SubmitImpl();
         aP2_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV17wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV17wallet = GXt_SdtWallet1;
         GXt_char2 = AV13error;
         GXt_char3 = AV16json_enc.gxTpr_Encryptedkey;
         GXt_char4 = AV16json_enc.gxTpr_Encryptedtext;
         new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV8clearText,  "", out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
         AV16json_enc.gxTpr_Encryptedkey = GXt_char3;
         AV16json_enc.gxTpr_Encryptedtext = GXt_char4;
         AV13error = GXt_char2;
         GXt_boolean5 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
         GXt_boolean6 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean6) ;
         AV18file.Source = AV17wallet.gxTpr_Walletbasedirectory+(GXt_boolean6 ? "/" : "\\")+StringUtil.Trim( AV9fileName);
         AV18file.WriteAllText(AV16json_enc.ToJSonString(false, true), "");
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
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char2 = "";
         AV16json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV18file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV9fileName ;
      private string AV13error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool GXt_boolean5 ;
      private bool GXt_boolean6 ;
      private string AV8clearText ;
      private GxFile AV18file ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV16json_enc ;
      private string aP2_error ;
   }

}
