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
   public class createwalletfiles : GXProcedure
   {
      public createwalletfiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createwalletfiles( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtWallet aP0_wallet )
      {
         this.AV9wallet = aP0_wallet;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtWallet aP0_wallet )
      {
         this.AV9wallet = aP0_wallet;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( StringUtil.StrCmp(new GeneXus.Programs.wallet.safefilename(context).executeUdp(  AV9wallet.gxTpr_Walletname), StringUtil.Trim( AV9wallet.gxTpr_Walletname)) != 0 )
         {
            GX_msglist.addItem("Invalid wallet name");
         }
         else
         {
            GXt_char1 = "";
            new GeneXus.Programs.wallet.getwalletsdir(context ).execute( out  GXt_char1) ;
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            AV11walletDirectory.Source = GXt_char1+(GXt_boolean3 ? "/" : "\\")+StringUtil.Trim( AV9wallet.gxTpr_Walletname);
            if ( ! AV11walletDirectory.Exists() )
            {
               AV11walletDirectory.Create();
            }
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            AV9wallet.gxTpr_Walletbasedirectory = AV11walletDirectory.GetAbsoluteName()+(GXt_boolean2 ? "/" : "\\");
            AV12walletFileName = StringUtil.Trim( AV9wallet.gxTpr_Walletbasedirectory) + StringUtil.Trim( AV9wallet.gxTpr_Walletname) + ".json";
            AV9wallet.gxTpr_Walletfilename = AV12walletFileName;
            AV10walletFile.Source = AV12walletFileName;
            if ( AV10walletFile.Exists() )
            {
               GX_msglist.addItem("Wallet already exist");
            }
            else
            {
               AV10walletFile.WriteAllText(AV9wallet.ToJSonString(false, true), "");
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
         AV11walletDirectory = new GxDirectory(context.GetPhysicalPath());
         GXt_char1 = "";
         AV12walletFileName = "";
         AV10walletFile = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string GXt_char1 ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean2 ;
      private string AV12walletFileName ;
      private GxFile AV10walletFile ;
      private GxDirectory AV11walletDirectory ;
      private GeneXus.Programs.wallet.SdtWallet AV9wallet ;
   }

}
