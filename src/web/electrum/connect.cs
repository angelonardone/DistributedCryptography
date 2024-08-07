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
namespace GeneXus.Programs.electrum {
   public class connect : GXProcedure
   {
      public connect( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public connect( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV11error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV11error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV11error = "" ;
         SubmitImpl();
         aP0_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtConnection1 = AV15Connection;
         new GeneXus.Programs.electrum.getelectrumconnid(context ).execute( out  GXt_SdtConnection1) ;
         AV15Connection = GXt_SdtConnection1;
         if ( (Guid.Empty==AV15Connection.gxTpr_Connectionid) )
         {
            GXt_SdtConnectionParameters_ConnectionParametersItem2 = AV16walletConnParameter;
            new GeneXus.Programs.electrum.getelectrumconfig(context ).execute( out  GXt_SdtConnectionParameters_ConnectionParametersItem2) ;
            AV16walletConnParameter = GXt_SdtConnectionParameters_ConnectionParametersItem2;
            if ( ( StringUtil.StrCmp(AV16walletConnParameter.gxTpr_Connectiontype, "ws") == 0 ) && ! AV16walletConnParameter.gxTpr_Secure )
            {
               AV17wsUrl = "ws://" + StringUtil.Trim( AV16walletConnParameter.gxTpr_Hostname) + ":" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16walletConnParameter.gxTpr_Port), 6, 0));
               AV13OperationResult = GxInternetLibWs.connect(AV17wsUrl, "receiveFromElectrum", AV16walletConnParameter.gxTpr_Timeoutmiliseconds);
            }
            else if ( ( StringUtil.StrCmp(AV16walletConnParameter.gxTpr_Connectiontype, "ws") == 0 ) && AV16walletConnParameter.gxTpr_Secure )
            {
               AV17wsUrl = "wss://" + StringUtil.Trim( AV16walletConnParameter.gxTpr_Hostname) + ":" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16walletConnParameter.gxTpr_Port), 6, 0));
               AV13OperationResult = GxInternetLibWs.connect(AV17wsUrl, "receiveFromElectrum", AV16walletConnParameter.gxTpr_Timeoutmiliseconds);
            }
            else if ( StringUtil.StrCmp(AV16walletConnParameter.gxTpr_Connectiontype, "tcp") == 0 )
            {
               AV13OperationResult = GxInternetLibTcp.connect(StringUtil.Trim( AV16walletConnParameter.gxTpr_Hostname), AV16walletConnParameter.gxTpr_Port, "receiveFromElectrum", AV16walletConnParameter.gxTpr_Secure, AV16walletConnParameter.gxTpr_Timeoutmiliseconds);
            }
            else
            {
               AV11error = "We couldn't recognize the Connection Type on the Electrum config file";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               if ( AV13OperationResult.gxTpr_Success )
               {
                  AV15Connection.gxTpr_Connectionid = AV13OperationResult.gxTpr_Connectionid;
                  AV15Connection.gxTpr_Connectiontype = AV16walletConnParameter.gxTpr_Connectiontype;
                  new GeneXus.Programs.electrum.setelectrumconnid(context ).execute(  AV15Connection) ;
               }
               else
               {
                  AV11error = AV13OperationResult.gxTpr_Errormessage;
               }
            }
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
         AV11error = "";
         AV15Connection = new GeneXus.Programs.electrum.SdtConnection(context);
         GXt_SdtConnection1 = new GeneXus.Programs.electrum.SdtConnection(context);
         AV16walletConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         GXt_SdtConnectionParameters_ConnectionParametersItem2 = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV17wsUrl = "";
         AV13OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         GxInternetLibTcp = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp(context);
         /* GeneXus formulas. */
      }

      private string AV11error ;
      private string AV17wsUrl ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp GxInternetLibTcp ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP0_error ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV13OperationResult ;
      private GeneXus.Programs.electrum.SdtConnection AV15Connection ;
      private GeneXus.Programs.electrum.SdtConnection GXt_SdtConnection1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV16walletConnParameter ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem GXt_SdtConnectionParameters_ConnectionParametersItem2 ;
   }

}
