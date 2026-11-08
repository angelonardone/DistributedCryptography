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
   public class listsharedrecords : GXProcedure
   {
      public listsharedrecords( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public listsharedrecords( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_kind ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> aP2_records ,
                           out string aP3_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV15kind = aP1_kind;
         this.AV18records = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography") ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_records=this.AV18records;
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_kind ,
                                out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> aP2_records )
      {
         execute(aP0_groupId, aP1_kind, out aP2_records, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_kind ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> aP2_records ,
                                 out string aP3_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV15kind = aP1_kind;
         this.AV18records = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography") ;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_records=this.AV18records;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV18records = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography");
         new GeneXus.Programs.wallet.registered.getsharedgroup(context ).execute(  AV12groupId, out  AV20sharedGroupId, out  AV13groupKey, out  AV21accessToken, out  AV14isOwner, out  AV11error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Groupid = AV20sharedGroupId;
         AV8body.gxTpr_Accesstoken = AV21accessToken;
         new GeneXus.Programs.wallet.registered.grouprecordkindtag(context ).execute(  AV13groupKey,  AV15kind, out  AV22kindTag, out  AV11error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8body.gxTpr_Kind = AV22kindTag;
         new GeneXus.Programs.wallet.registered.postgrouprecords(context ).execute(  "listGroupRecords",  AV8body, out  AV19response, out  AV11error) ;
         if ( StringUtil.StrCmp(AV11error, "notmember") == 0 )
         {
            AV11error = "The owner of the group has not given you access to its shared records yet: he gives it when he opens the group";
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV19response.gxTpr_Records.gxTpr_Record.Count )
         {
            AV16oneItem = ((GeneXus.Programs.wallet.registered.SdtGroupRecordResponse_records_RecordItem)AV19response.gxTpr_Records.gxTpr_Record.Item(AV23GXV1));
            AV9DecryptionResult = AV10EncryptionService.decrypt(AV16oneItem.gxTpr_Encryptedrecord, AV13groupKey);
            if ( AV9DecryptionResult.gxTpr_Success )
            {
               AV17record = new GeneXus.Programs.wallet.registered.SdtGroupSharedRecord(context);
               AV17record.gxTpr_Recordkey = StringUtil.Trim( AV16oneItem.gxTpr_Recordkey);
               AV17record.gxTpr_Version = AV16oneItem.gxTpr_Version;
               AV17record.gxTpr_Cleartext = AV9DecryptionResult.gxTpr_Decryptedtext;
               AV18records.Add(AV17record, 0);
            }
            AV23GXV1 = (int)(AV23GXV1+1);
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
         AV18records = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord>( context, "GroupSharedRecord", "distributedcryptography");
         AV11error = "";
         AV20sharedGroupId = Guid.Empty;
         AV13groupKey = "";
         AV21accessToken = "";
         AV8body = new GeneXus.Programs.wallet.registered.SdtGroupRecordRequest(context);
         AV22kindTag = "";
         AV19response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context);
         AV16oneItem = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse_records_RecordItem(context);
         AV9DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV10EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV17record = new GeneXus.Programs.wallet.registered.SdtGroupSharedRecord(context);
         /* GeneXus formulas. */
      }

      private int AV23GXV1 ;
      private string AV11error ;
      private string AV13groupKey ;
      private bool AV14isOwner ;
      private string AV15kind ;
      private string AV21accessToken ;
      private string AV22kindTag ;
      private Guid AV12groupId ;
      private Guid AV20sharedGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> AV18records ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordRequest AV8body ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse AV19response ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse_records_RecordItem AV16oneItem ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV9DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV10EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroupSharedRecord AV17record ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupSharedRecord> aP2_records ;
      private string aP3_error ;
   }

}
