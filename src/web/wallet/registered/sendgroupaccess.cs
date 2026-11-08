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
   public class sendgroupaccess : GXProcedure
   {
      public sendgroupaccess( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendgroupaccess( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           out string aP1_error )
      {
         this.AV11group_sdt = aP0_group_sdt;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         execute(aP0_group_sdt, out aP1_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 out string aP1_error )
      {
         this.AV11group_sdt = aP0_group_sdt;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         if ( ! AV11group_sdt.gxTpr_Amigroupowner || (Guid.Empty==AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid) || String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword))) )
         {
            cleanup();
            if (true) return;
         }
         AV13sessionKey = "GroupAccessSet_" + StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid.ToString());
         if ( StringUtil.StrCmp(AV14websession.Get(AV13sessionKey), "1") == 0 )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.hmacsha256hex(context ).execute(  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword),  "DistributedCryptography group access\\n"+StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid.ToString()), out  AV8accessToken, out  AV10error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV9body.gxTpr_Groupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV9body.gxTpr_Accesstoken = AV8accessToken;
            new GeneXus.Programs.wallet.registered.postgrouprecords(context ).execute(  "setGroupAccess",  AV9body, out  AV12response, out  AV10error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               AV14websession.Set(AV13sessionKey, "1");
            }
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
         AV10error = "";
         AV13sessionKey = "";
         AV14websession = context.GetSession();
         AV8accessToken = "";
         AV9body = new GeneXus.Programs.wallet.registered.SdtGroupRecordRequest(context);
         AV12response = new GeneXus.Programs.wallet.registered.SdtGroupRecordResponse(context);
         /* GeneXus formulas. */
      }

      private string AV10error ;
      private string AV13sessionKey ;
      private string AV8accessToken ;
      private IGxSession AV14websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordRequest AV9body ;
      private GeneXus.Programs.wallet.registered.SdtGroupRecordResponse AV12response ;
      private string aP1_error ;
   }

}
