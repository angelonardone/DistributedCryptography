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
   public class saverubyoracle : GXProcedure
   {
      public saverubyoracle( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public saverubyoracle( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletFileName ,
                           string aP1_encryptedOracle ,
                           out string aP2_error )
      {
         this.AV12walletFileName = aP0_walletFileName;
         this.AV8encryptedOracle = aP1_encryptedOracle;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_walletFileName ,
                                string aP1_encryptedOracle )
      {
         execute(aP0_walletFileName, aP1_encryptedOracle, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_walletFileName ,
                                 string aP1_encryptedOracle ,
                                 out string aP2_error )
      {
         this.AV12walletFileName = aP0_walletFileName;
         this.AV8encryptedOracle = aP1_encryptedOracle;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         GXt_SdtWallet1 = AV10wallet;
         new GeneXus.Programs.wallet.readwallet(context ).execute(  AV12walletFileName, out  GXt_SdtWallet1) ;
         AV10wallet = GXt_SdtWallet1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10wallet.gxTpr_Walletfilename)) )
         {
            AV9error = "The wallet file could not be read";
         }
         else
         {
            AV10wallet.gxTpr_Encryptedoracle = AV8encryptedOracle;
            AV11walletFile.Source = StringUtil.Trim( AV10wallet.gxTpr_Walletfilename);
            AV11walletFile.WriteAllText(AV10wallet.ToJSonString(false, true), "");
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
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11walletFile = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string AV8encryptedOracle ;
      private string AV12walletFileName ;
      private GxFile AV11walletFile ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP2_error ;
   }

}
