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
   public class storelocalfile : GXProcedure
   {
      public storelocalfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public storelocalfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_uploadedFile ,
                           string aP1_originalName ,
                           out string aP2_error )
      {
         this.AV16uploadedFile = aP0_uploadedFile;
         this.AV14originalName = aP1_originalName;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV11error;
      }

      public string executeUdp( string aP0_uploadedFile ,
                                string aP1_originalName )
      {
         execute(aP0_uploadedFile, aP1_originalName, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_uploadedFile ,
                                 string aP1_originalName ,
                                 out string aP2_error )
      {
         this.AV16uploadedFile = aP0_uploadedFile;
         this.AV14originalName = aP1_originalName;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11error = "";
         GXt_SdtWallet1 = AV17wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV17wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV13keyInfo;
         new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV13keyInfo = GXt_SdtKeyInfo2;
         GXt_char3 = AV15safeName;
         new GeneXus.Programs.wallet.safefilename(context ).execute(  AV14originalName, out  GXt_char3) ;
         AV15safeName = GXt_char3;
         AV8directory.Source = AV17wallet.gxTpr_Walletbasedirectory+"Files";
         if ( ! AV8directory.Exists() )
         {
            AV8directory.Create();
         }
         GXt_boolean4 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
         GXt_boolean5 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
         AV9encDestination = AV8directory.GetAbsoluteName() + (GXt_boolean5 ? "/" : "\\") + Guid.NewGuid( ).ToString();
         GXt_char3 = AV11error;
         new GeneXus.Programs.distributedcrypto.fileencryptv2(context ).execute(  AV16uploadedFile,  AV9encDestination,  AV13keyInfo.gxTpr_Publickey,  AV15safeName, out  GXt_char3) ;
         AV11error = GXt_char3;
         AV12file.Source = AV16uploadedFile;
         if ( AV12file.Exists() )
         {
            AV12file.Delete();
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV10encryptedFile.gxTpr_Filename = AV15safeName;
            AV10encryptedFile.gxTpr_Fullfilename = AV9encDestination;
            AV10encryptedFile.gxTpr_Create = DateTimeUtil.Now( context);
            GXt_char3 = AV11error;
            new GeneXus.Programs.wallet.insertintoallfiles(context ).execute(  AV10encryptedFile, out  GXt_char3) ;
            AV11error = GXt_char3;
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
         AV11error = "";
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV15safeName = "";
         AV8directory = new GxDirectory(context.GetPhysicalPath());
         AV9encDestination = "";
         AV12file = new GxFile(context.GetPhysicalPath());
         AV10encryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char3 ;
      private bool GXt_boolean4 ;
      private bool GXt_boolean5 ;
      private string AV16uploadedFile ;
      private string AV14originalName ;
      private string AV11error ;
      private string AV15safeName ;
      private string AV9encDestination ;
      private GxFile AV12file ;
      private GxDirectory AV8directory ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV13keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV10encryptedFile ;
      private string aP2_error ;
   }

}
