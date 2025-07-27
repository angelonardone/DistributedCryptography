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
   public class disconnect : GXProcedure
   {
      public disconnect( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public disconnect( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV16error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV16error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV16error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV16error = "" ;
         SubmitImpl();
         aP0_error=this.AV16error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtConnection1 = AV15Connection;
         new GeneXus.Programs.electrum.getelectrumconnid(context ).execute( out  GXt_SdtConnection1) ;
         AV15Connection = GXt_SdtConnection1;
         AV19OperationResult = GxInternetLibTcp.disconnect(AV15Connection.gxTpr_Connectionid);
         if ( AV19OperationResult.gxTpr_Success )
         {
            AV15Connection = new GeneXus.Programs.electrum.SdtConnection(context);
            new GeneXus.Programs.electrum.setelectrumconnid(context ).execute(  AV15Connection) ;
         }
         else
         {
            AV16error = AV19OperationResult.gxTpr_Errormessage;
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
         AV16error = "";
         AV15Connection = new GeneXus.Programs.electrum.SdtConnection(context);
         GXt_SdtConnection1 = new GeneXus.Programs.electrum.SdtConnection(context);
         AV19OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibTcp = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp(context);
         /* GeneXus formulas. */
      }

      private string AV16error ;
      private GeneXus.Programs.electrum.SdtConnection AV15Connection ;
      private GeneXus.Programs.electrum.SdtConnection GXt_SdtConnection1 ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV19OperationResult ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp GxInternetLibTcp ;
      private string aP0_error ;
   }

}
