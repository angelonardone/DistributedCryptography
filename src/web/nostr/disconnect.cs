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
namespace GeneXus.Programs.nostr {
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
         GXt_char1 = AV11error;
         new GeneXus.Programs.nostr.stopsubscription(context ).execute( out  GXt_char1) ;
         AV11error = GXt_char1;
         GXt_guid2 = AV8Key;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid2) ;
         AV8Key = GXt_guid2;
         AV15OperationResult = GxInternetLibWs.disconnect(AV8Key);
         if ( AV15OperationResult.gxTpr_Success )
         {
            AV8Key = Guid.Empty;
            new GeneXus.Programs.nostr.settnostrconnectionid(context ).execute(  AV8Key) ;
         }
         else
         {
            AV11error = AV15OperationResult.gxTpr_Errormessage;
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
         GXt_char1 = "";
         AV8Key = Guid.Empty;
         GXt_guid2 = Guid.Empty;
         AV15OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         /* GeneXus formulas. */
      }

      private string AV11error ;
      private string GXt_char1 ;
      private Guid AV8Key ;
      private Guid GXt_guid2 ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP0_error ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV15OperationResult ;
   }

}
