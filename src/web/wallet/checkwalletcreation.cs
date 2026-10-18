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
   public class checkwalletcreation : GXProcedure
   {
      public checkwalletcreation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public checkwalletcreation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletName ,
                           out string aP1_error )
      {
         this.AV10walletName = aP0_walletName;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( string aP0_walletName )
      {
         execute(aP0_walletName, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_walletName ,
                                 out string aP1_error )
      {
         this.AV10walletName = aP0_walletName;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         if ( ! new GeneXus.Programs.wallet.islocalrequest(context).executeUdp( ) )
         {
            AV8error = "Wallets are created and restored only on the computer where the application runs, at http://localhost";
         }
         else if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV10walletName))) )
         {
            AV8error = "The Wallet Name is mandatory";
         }
         else if ( StringUtil.StrCmp(new GeneXus.Programs.wallet.safefilename(context).executeUdp(  AV10walletName), StringUtil.Trim( AV10walletName)) != 0 )
         {
            AV8error = "Invalid wallet name";
         }
         else
         {
            GXt_char1 = "";
            new GeneXus.Programs.wallet.getwalletsdir(context ).execute( out  GXt_char1) ;
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            GXt_boolean4 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
            GXt_boolean5 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
            AV9walletFile.Source = GXt_char1+(GXt_boolean3 ? "/" : "\\")+StringUtil.Trim( AV10walletName)+(GXt_boolean5 ? "/" : "\\")+StringUtil.Trim( AV10walletName)+".json";
            if ( AV9walletFile.Exists() )
            {
               AV8error = "A wallet with this name already exists on this computer. Nothing was created: choose another name.";
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
         AV8error = "";
         AV9walletFile = new GxFile(context.GetPhysicalPath());
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV10walletName ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean4 ;
      private bool GXt_boolean5 ;
      private GxFile AV9walletFile ;
      private string aP1_error ;
   }

}
