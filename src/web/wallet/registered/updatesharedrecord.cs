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
   public class updatesharedrecord : GXProcedure
   {
      public updatesharedrecord( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updatesharedrecord( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_kind ,
                           string aP2_recordKey ,
                           string aP3_clearText ,
                           long aP4_version ,
                           out string aP5_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV17kind = aP1_kind;
         this.AV18recordKey = aP2_recordKey;
         this.AV9clearText = aP3_clearText;
         this.AV21version = aP4_version;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP5_error=this.AV13error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_kind ,
                                string aP2_recordKey ,
                                string aP3_clearText ,
                                long aP4_version )
      {
         execute(aP0_groupId, aP1_kind, aP2_recordKey, aP3_clearText, aP4_version, out aP5_error);
         return AV13error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_kind ,
                                 string aP2_recordKey ,
                                 string aP3_clearText ,
                                 long aP4_version ,
                                 out string aP5_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV17kind = aP1_kind;
         this.AV18recordKey = aP2_recordKey;
         this.AV9clearText = aP3_clearText;
         this.AV21version = aP4_version;
         this.AV13error = "" ;
         SubmitImpl();
         aP5_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.registered.getsharedgroup(context ).execute(  AV14groupId, out  AV20sharedGroupId, out  AV15groupKey, out  AV22accessToken, out  AV16isOwner, out  AV13error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.grouprecordeditsecret(context ).execute(  AV20sharedGroupId,  AV17kind,  AV18recordKey, out  AV23editSecret, out  AV13error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV11EncryptionResult = AV12EncryptionService.encrypt(AV9clearText, AV15groupKey);
         if ( ! AV11EncryptionResult.gxTpr_Success )
         {
            AV13error = AV11EncryptionResult.gxTpr_Errormessage;
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Groupid = AV20sharedGroupId;
         AV8body.gxTpr_Accesstoken = AV22accessToken;
         new GeneXus.Programs.wallet.registered.grouprecordkindtag(context ).execute(  AV15groupKey,  AV17kind, out  AV24kindTag, out  AV13error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Kind = AV24kindTag;
         AV8body.gxTpr_Recordkey = StringUtil.Trim( AV18recordKey);
         AV8body.gxTpr_Encryptedrecord = AV11EncryptionResult.gxTpr_Ciphertext;
         AV8body.gxTpr_Version = AV21version;
         AV8body.gxTpr_Editsecret = AV23editSecret;
         new GeneXus.Programs.wallet.registered.postgrouprecords(context ).execute(  "updateGroupRecord",  AV8body, out  AV19response, out  AV13error) ;
         if ( StringUtil.StrCmp(AV13error, "notmember") == 0 )
         {
            AV13error = "The owner of the group has not given you access to its shared records yet: he gives it when he opens the group";
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
         AV13error = "";
         AV20sharedGroupId = Guid.Empty;
         AV15groupKey = "";
         AV22accessToken = "";
         AV23editSecret = "";
         AV11EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV12EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV8body = new GeneXus.Programs.wallet.registered.SdtGroupRecordRequest(context);
         AV24kindTag = "";
         AV19response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context);
         /* GeneXus formulas. */
      }

      private long AV21version ;
      private string AV13error ;
      private string AV15groupKey ;
      private bool AV16isOwner ;
      private string AV9clearText ;
      private string AV17kind ;
      private string AV18recordKey ;
      private string AV22accessToken ;
      private string AV23editSecret ;
      private string AV24kindTag ;
      private Guid AV14groupId ;
      private Guid AV20sharedGroupId ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV11EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV12EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordRequest AV8body ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse AV19response ;
      private string aP5_error ;
   }

}
