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
   public class readallfiles : GXProcedure
   {
      public readallfiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readallfiles( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> aP0_encryptedFiles )
      {
         this.AV13encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_encryptedFiles=this.AV13encryptedFiles;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> executeUdp( )
      {
         execute(out aP0_encryptedFiles);
         return AV13encryptedFiles ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> aP0_encryptedFiles )
      {
         this.AV13encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography") ;
         SubmitImpl();
         aP0_encryptedFiles=this.AV13encryptedFiles;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV11wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV11wallet = GXt_SdtWallet1;
         AV9directory.Source = AV11wallet.gxTpr_Walletbasedirectory+"Files";
         if ( ! AV9directory.Exists() )
         {
            AV9directory.Create();
         }
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV14all_file_name = StringUtil.Trim( AV9directory.Source) + (GXt_boolean3 ? "/" : "\\") + "files.json";
         AV13encryptedFiles.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV14all_file_name, out  AV15error), null);
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
         AV13encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV14all_file_name = "";
         AV15error = "";
         /* GeneXus formulas. */
      }

      private string AV14all_file_name ;
      private string AV15error ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> aP0_encryptedFiles ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> AV13encryptedFiles ;
      private GxDirectory AV9directory ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
   }

}
