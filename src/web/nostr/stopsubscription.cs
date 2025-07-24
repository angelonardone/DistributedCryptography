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
namespace GeneXus.Programs.nostr {
   public class stopsubscription : GXProcedure
   {
      public stopsubscription( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public stopsubscription( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV9error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV9error = "" ;
         SubmitImpl();
         aP0_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_guid1 = AV11Key;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid1) ;
         AV11Key = GXt_guid1;
         GXt_SdtExternalUser2 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV10externalUser = GXt_SdtExternalUser2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10externalUser.gxTpr_Externaltoken)) )
         {
            GXt_int3 = AV12lastChatTimeInSeconds;
            new GeneXus.Programs.wallet.registered.getlastchattime(context ).execute( out  GXt_int3) ;
            AV12lastChatTimeInSeconds = GXt_int3;
            AV13message = "[\"CLOSE\", \"" + AV10externalUser.gxTpr_Keyinfo.gxTpr_Address + "\"]";
            AV17OperationResult = GxInternetLibWs.sendmessage(AV11Key, AV13message);
            if ( ! AV17OperationResult.gxTpr_Success )
            {
               GX_msglist.addItem("error sending message: "+AV17OperationResult.gxTpr_Errormessage);
            }
         }
         else
         {
            AV9error = "You are not legged in";
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
         AV9error = "";
         AV11Key = Guid.Empty;
         GXt_guid1 = Guid.Empty;
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13message = "";
         AV17OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         /* GeneXus formulas. */
      }

      private long AV12lastChatTimeInSeconds ;
      private long GXt_int3 ;
      private string AV9error ;
      private string AV13message ;
      private Guid AV11Key ;
      private Guid GXt_guid1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV17OperationResult ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP0_error ;
   }

}
