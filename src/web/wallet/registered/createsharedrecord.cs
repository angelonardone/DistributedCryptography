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
   public class createsharedrecord : GXProcedure
   {
      public createsharedrecord( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createsharedrecord( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_kind ,
                           string aP2_naturalKey ,
                           string aP3_clearText ,
                           out string aP4_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV16kind = aP1_kind;
         this.AV17naturalKey = aP2_naturalKey;
         this.AV9clearText = aP3_clearText;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP4_error=this.AV12error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_kind ,
                                string aP2_naturalKey ,
                                string aP3_clearText )
      {
         execute(aP0_groupId, aP1_kind, aP2_naturalKey, aP3_clearText, out aP4_error);
         return AV12error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_kind ,
                                 string aP2_naturalKey ,
                                 string aP3_clearText ,
                                 out string aP4_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV16kind = aP1_kind;
         this.AV17naturalKey = aP2_naturalKey;
         this.AV9clearText = aP3_clearText;
         this.AV12error = "" ;
         SubmitImpl();
         aP4_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.registered.getsharedgroup(context ).execute(  AV13groupId, out  AV20sharedGroupId, out  AV14groupKey, out  AV21accessToken, out  AV15isOwner, out  AV12error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.grouprecordkey(context ).execute(  AV14groupKey,  AV16kind,  AV17naturalKey, out  AV18recordKey, out  AV12error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.grouprecordeditsecret(context ).execute(  AV20sharedGroupId,  AV16kind,  AV18recordKey, out  AV22editSecret, out  AV12error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         AV10EncryptionResult = AV11EncryptionService.encrypt(AV9clearText, AV14groupKey);
         if ( ! AV10EncryptionResult.gxTpr_Success )
         {
            AV12error = AV10EncryptionResult.gxTpr_Errormessage;
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Groupid = AV20sharedGroupId;
         AV8body.gxTpr_Accesstoken = AV21accessToken;
         new GeneXus.Programs.wallet.registered.grouprecordkindtag(context ).execute(  AV14groupKey,  AV16kind, out  AV23kindTag, out  AV12error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Kind = AV23kindTag;
         AV8body.gxTpr_Recordkey = AV18recordKey;
         AV8body.gxTpr_Encryptedrecord = AV10EncryptionResult.gxTpr_Ciphertext;
         AV8body.gxTpr_Editsecret = AV22editSecret;
         new GeneXus.Programs.wallet.registered.postgrouprecords(context ).execute(  "createGroupRecord",  AV8body, out  AV19response, out  AV12error) ;
         if ( StringUtil.StrCmp(AV12error, "notmember") == 0 )
         {
            AV12error = "The owner of the group has not given you access to its shared records yet: he gives it when he opens the group";
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
         AV12error = "";
         AV20sharedGroupId = Guid.Empty;
         AV14groupKey = "";
         AV21accessToken = "";
         AV18recordKey = "";
         AV22editSecret = "";
         AV10EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV11EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV8body = new GeneXus.Programs.wallet.registered.SdtGroupRecordRequest(context);
         AV23kindTag = "";
         AV19response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context);
         /* GeneXus formulas. */
      }

      private string AV12error ;
      private string AV14groupKey ;
      private bool AV15isOwner ;
      private string AV9clearText ;
      private string AV16kind ;
      private string AV17naturalKey ;
      private string AV21accessToken ;
      private string AV18recordKey ;
      private string AV22editSecret ;
      private string AV23kindTag ;
      private Guid AV13groupId ;
      private Guid AV20sharedGroupId ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV10EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV11EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordRequest AV8body ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse AV19response ;
      private string aP4_error ;
   }

}
