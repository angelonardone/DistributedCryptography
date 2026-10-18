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
namespace GeneXus.Programs.electrum {
   public class saveelectrumconfig : GXProcedure
   {
      public saveelectrumconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public saveelectrumconfig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ,
                           out string aP1_error )
      {
         this.AV13walletConnParameter = aP0_walletConnParameter;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter )
      {
         execute(aP0_walletConnParameter, out aP1_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem aP0_walletConnParameter ,
                                 out string aP1_error )
      {
         this.AV13walletConnParameter = aP0_walletConnParameter;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         if ( ! new GeneXus.Programs.wallet.iswalletopen(context).executeUdp( ) )
         {
            AV10error = "Open a wallet first";
         }
         else
         {
            GXt_SdtWallet1 = AV12wallet;
            new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
            AV12wallet = GXt_SdtWallet1;
            AV11toSave = (GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)(AV13walletConnParameter.Clone());
            AV11toSave.gxTpr_Hostname = StringUtil.Trim( AV13walletConnParameter.gxTpr_Hostname);
            AV11toSave.gxTpr_Networktype = AV12wallet.gxTpr_Networktype;
            if ( ( StringUtil.StrCmp(AV11toSave.gxTpr_Connectiontype, "tcp") != 0 ) && ( StringUtil.StrCmp(AV11toSave.gxTpr_Connectiontype, "ws") != 0 ) )
            {
               AV10error = "Choose the connection type of the Electrum server";
            }
            else if ( ! GxRegex.IsMatch(StringUtil.Trim( AV11toSave.gxTpr_Hostname),"^[A-Za-z0-9._-]+$") )
            {
               AV10error = "Type the host name of the Electrum server (letters, numbers, dots and hyphens)";
            }
            else if ( ( AV11toSave.gxTpr_Port < 1 ) || ( AV11toSave.gxTpr_Port > 65535 ) )
            {
               AV10error = "The port of the Electrum server must be between 1 and 65535";
            }
            else if ( AV11toSave.gxTpr_Timeoutmiliseconds < 100 )
            {
               AV10error = "The time out must be at least 100 miliseconds";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               GXt_char2 = AV10error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "electrum.conf",  AV11toSave.ToJSonString(false, true), out  GXt_char2) ;
               AV10error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  GXt_char2 = AV8auxError;
                  new GeneXus.Programs.electrum.disconnect(context ).execute( out  GXt_char2) ;
                  AV8auxError = GXt_char2;
                  AV9Connection = new GeneXus.Programs.electrum.SdtConnection(context);
                  new GeneXus.Programs.electrum.setelectrumconnid(context ).execute(  AV9Connection) ;
               }
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
         AV10error = "";
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11toSave = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV8auxError = "";
         GXt_char2 = "";
         AV9Connection = new GeneXus.Programs.electrum.SdtConnection(context);
         /* GeneXus formulas. */
      }

      private string AV10error ;
      private string AV8auxError ;
      private string GXt_char2 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV13walletConnParameter ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV11toSave ;
      private GeneXus.Programs.electrum.SdtConnection AV9Connection ;
      private string aP1_error ;
   }

}
