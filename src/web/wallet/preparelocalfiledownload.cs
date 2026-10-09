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
   public class preparelocalfiledownload : GXProcedure
   {
      public preparelocalfiledownload( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public preparelocalfiledownload( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_fullFileName ,
                           out string aP1_token ,
                           out string aP2_error )
      {
         this.AV12fullFileName = aP0_fullFileName;
         this.AV18token = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_token=this.AV18token;
         aP2_error=this.AV10error;
      }

      public string executeUdp( string aP0_fullFileName ,
                                out string aP1_token )
      {
         execute(aP0_fullFileName, out aP1_token, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_fullFileName ,
                                 out string aP1_token ,
                                 out string aP2_error )
      {
         this.AV12fullFileName = aP0_fullFileName;
         this.AV18token = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_token=this.AV18token;
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         AV18token = "";
         AV11found = false;
         GXt_objcol_SdtEncryptedFile1 = AV9encryptedFiles;
         new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile1) ;
         AV9encryptedFiles = GXt_objcol_SdtEncryptedFile1;
         AV19GXV1 = 1;
         while ( AV19GXV1 <= AV9encryptedFiles.Count )
         {
            AV8encryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV9encryptedFiles.Item(AV19GXV1));
            if ( ! AV11found && ( StringUtil.StrCmp(StringUtil.Trim( AV8encryptedFile.gxTpr_Fullfilename), StringUtil.Trim( AV12fullFileName)) == 0 ) )
            {
               AV11found = true;
               AV17stored = AV8encryptedFile;
            }
            AV19GXV1 = (int)(AV19GXV1+1);
         }
         if ( ! AV11found )
         {
            AV10error = "The file is not in this wallet's list";
         }
         else
         {
            GXt_SdtKeyInfo2 = AV14keyInfo;
            new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo2) ;
            AV14keyInfo = GXt_SdtKeyInfo2;
            GXt_char3 = AV16privateDir;
            new GeneXus.Programs.wallet.getprivatetempdir(context ).execute( out  GXt_char3) ;
            AV16privateDir = GXt_char3;
            GXt_boolean4 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
            GXt_boolean5 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
            AV15outputFile = StringUtil.Trim( AV16privateDir) + (GXt_boolean5 ? "/" : "\\") + Guid.NewGuid( ).ToString();
            GXt_char3 = AV10error;
            new GeneXus.Programs.distributedcrypto.filedecryptv2(context ).execute(  AV17stored.gxTpr_Fullfilename,  AV15outputFile,  StringUtil.Trim( AV14keyInfo.gxTpr_Privatekey), out  AV13innerName, out  GXt_char3) ;
            AV10error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               GXt_char3 = AV18token;
               new GeneXus.Programs.wallet.stagedownload(context ).execute(  AV15outputFile,  AV17stored.gxTpr_Filename, out  GXt_char3) ;
               AV18token = GXt_char3;
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
         AV18token = "";
         AV10error = "";
         AV9encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         GXt_objcol_SdtEncryptedFile1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV8encryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         AV17stored = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         AV14keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV16privateDir = "";
         AV15outputFile = "";
         AV13innerName = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV19GXV1 ;
      private string AV12fullFileName ;
      private string GXt_char3 ;
      private bool AV11found ;
      private bool GXt_boolean4 ;
      private bool GXt_boolean5 ;
      private string AV18token ;
      private string AV10error ;
      private string AV16privateDir ;
      private string AV15outputFile ;
      private string AV13innerName ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> AV9encryptedFiles ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> GXt_objcol_SdtEncryptedFile1 ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV8encryptedFile ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV17stored ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV14keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private string aP1_token ;
      private string aP2_error ;
   }

}
