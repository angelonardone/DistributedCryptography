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
namespace GeneXus.Programs.wallet.registered {
   public class grouprecordeditsecret : GXProcedure
   {
      public grouprecordeditsecret( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public grouprecordeditsecret( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_sharedGroupId ,
                           string aP1_kind ,
                           string aP2_recordKey ,
                           out string aP3_editSecret ,
                           out string aP4_error )
      {
         this.AV13sharedGroupId = aP0_sharedGroupId;
         this.AV11kind = aP1_kind;
         this.AV12recordKey = aP2_recordKey;
         this.AV8editSecret = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP3_editSecret=this.AV8editSecret;
         aP4_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_sharedGroupId ,
                                string aP1_kind ,
                                string aP2_recordKey ,
                                out string aP3_editSecret )
      {
         execute(aP0_sharedGroupId, aP1_kind, aP2_recordKey, out aP3_editSecret, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_sharedGroupId ,
                                 string aP1_kind ,
                                 string aP2_recordKey ,
                                 out string aP3_editSecret ,
                                 out string aP4_error )
      {
         this.AV13sharedGroupId = aP0_sharedGroupId;
         this.AV11kind = aP1_kind;
         this.AV12recordKey = aP2_recordKey;
         this.AV8editSecret = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP3_editSecret=this.AV8editSecret;
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV10externalUser = GXt_SdtExternalUser1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV10externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey))) )
         {
            AV9error = "Open the wallet again: its groups key is not loaded";
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.hmacsha256hex(context ).execute(  StringUtil.Trim( AV10externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey),  "DistributedCryptography group record edit\\n"+StringUtil.Trim( AV13sharedGroupId.ToString())+"\\n"+StringUtil.Trim( AV11kind)+"\\n"+StringUtil.Trim( AV12recordKey), out  AV8editSecret, out  AV9error) ;
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
         AV8editSecret = "";
         AV9error = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string AV11kind ;
      private string AV12recordKey ;
      private string AV8editSecret ;
      private Guid AV13sharedGroupId ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP3_editSecret ;
      private string aP4_error ;
   }

}
