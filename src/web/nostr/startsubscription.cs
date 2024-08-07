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
   public class startsubscription : GXProcedure
   {
      public startsubscription( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public startsubscription( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV10error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV10error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV10error = "" ;
         SubmitImpl();
         aP0_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_guid1 = AV12Key;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid1) ;
         AV12Key = GXt_guid1;
         GXt_SdtExternalUser2 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV13externalUser = GXt_SdtExternalUser2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13externalUser.gxTpr_Externaltoken)) )
         {
            GXt_int3 = AV20lastChatTimeInSeconds;
            new GeneXus.Programs.wallet.registered.getlastchattime(context ).execute( out  GXt_int3) ;
            AV20lastChatTimeInSeconds = GXt_int3;
            AV18message = "[\"REQ\", \"" + AV13externalUser.gxTpr_Keyinfo.gxTpr_Address + "\" , " + "{" + "\"kinds\": [4]," + "\"#p\": [\"" + StringUtil.Trim( AV13externalUser.gxTpr_Keyinfo.gxTpr_Taprootpubkey) + "\"]," + "\"since\": " + StringUtil.Trim( StringUtil.Str( (decimal)(AV20lastChatTimeInSeconds), 18, 0)) + "}" + "]";
            AV23OperationResult = GxInternetLibWs.sendmessage(AV12Key, AV18message);
            if ( ! AV23OperationResult.gxTpr_Success )
            {
               GX_msglist.addItem("error sending message: "+AV23OperationResult.gxTpr_Errormessage);
            }
         }
         else
         {
            AV10error = "You are not legged in";
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
         AV10error = "";
         AV12Key = Guid.Empty;
         GXt_guid1 = Guid.Empty;
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV18message = "";
         AV23OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         /* GeneXus formulas. */
      }

      private long AV20lastChatTimeInSeconds ;
      private long GXt_int3 ;
      private string AV10error ;
      private string AV18message ;
      private Guid AV12Key ;
      private Guid GXt_guid1 ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP0_error ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV23OperationResult ;
   }

}
