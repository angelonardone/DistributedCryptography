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
         GXt_SdtExternalUser1 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV13externalUser = GXt_SdtExternalUser1;
         GXt_guid2 = AV17key;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid2) ;
         AV17key = GXt_guid2;
         if ( (Guid.Empty==AV17key) )
         {
            AV21WebSocketOperationResult = GxNostrLib.connectasync("ws://192.168.10.21:4848", "receiveFromNoster", 15000);
            if ( AV21WebSocketOperationResult.gxTpr_Success )
            {
               AV17key = AV21WebSocketOperationResult.gxTpr_Connectionid;
               new GeneXus.Programs.nostr.settnostrconnectionid(context ).execute(  AV17key) ;
            }
            else
            {
               AV11error = AV21WebSocketOperationResult.gxTpr_Errormessage;
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
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV17key = Guid.Empty;
         GXt_guid2 = Guid.Empty;
         AV21WebSocketOperationResult = new GeneXus.Programs.nostr.SdtWebSocketOperationResult(context);
         GxNostrLib = new GeneXus.Programs.nostr.SdtGxNostrLib(context);
         /* GeneXus formulas. */
      }

      private string AV11error ;
      private Guid AV17key ;
      private Guid GXt_guid2 ;
      private GeneXus.Programs.nostr.SdtGxNostrLib GxNostrLib ;
      private string aP0_error ;
      private GeneXus.Programs.nostr.SdtWebSocketOperationResult AV21WebSocketOperationResult ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
   }

}
