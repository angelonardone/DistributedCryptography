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
   public class getgroupbackupfingerprint : GXProcedure
   {
      public getgroupbackupfingerprint( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupbackupfingerprint( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_fingerprint ,
                           out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV9fingerprint = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_fingerprint=this.AV9fingerprint;
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out string aP1_fingerprint )
      {
         execute(aP0_groupId, out aP1_fingerprint, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_fingerprint ,
                                 out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV9fingerprint = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_fingerprint=this.AV9fingerprint;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.registered.checkgrouprestoreallowed(context ).execute(  AV11groupId, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT2 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT2) ;
         AV10group_sdt = GXt_SdtGroup_SDT2;
         AV13rubyResult.FromJSonString(AV12rubyProcessor.seedfingerprint(StringUtil.Trim( AV10group_sdt.gxTpr_Cleartextshare)), null);
         AV10group_sdt.gxTpr_Cleartextshare = "";
         if ( AV13rubyResult.gxTpr_Success )
         {
            AV9fingerprint = StringUtil.Upper( StringUtil.Trim( AV13rubyResult.gxTpr_Fingerprint));
         }
         else
         {
            AV8error = "The recovered seed is not valid: " + StringUtil.Trim( AV13rubyResult.gxTpr_Error);
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
         AV9fingerprint = "";
         AV8error = "";
         GXt_char1 = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV12rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         /* GeneXus formulas. */
      }

      private string AV9fingerprint ;
      private string AV8error ;
      private string GXt_char1 ;
      private Guid AV11groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.SdtRubyResult AV13rubyResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV12rubyProcessor ;
      private string aP1_fingerprint ;
      private string aP2_error ;
   }

}
