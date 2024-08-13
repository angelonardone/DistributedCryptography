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
namespace GeneXus.Programs.wallet.registered {
   public class updategroup : GXProcedure
   {
      public updategroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updategroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                           string aP1_encryptKey ,
                           out Guid aP2_outGroupId ,
                           out string aP3_error )
      {
         this.AV10group = aP0_group;
         this.AV27encryptKey = aP1_encryptKey;
         this.AV26outGroupId = Guid.Empty ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_outGroupId=this.AV26outGroupId;
         aP3_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                                string aP1_encryptKey ,
                                out Guid aP2_outGroupId )
      {
         execute(aP0_group, aP1_encryptKey, out aP2_outGroupId, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                                 string aP1_encryptKey ,
                                 out Guid aP2_outGroupId ,
                                 out string aP3_error )
      {
         this.AV10group = aP0_group;
         this.AV27encryptKey = aP1_encryptKey;
         this.AV26outGroupId = Guid.Empty ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_outGroupId=this.AV26outGroupId;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV8externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV8externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8externalUser.gxTpr_Externaltoken)) )
         {
            AV11body.gxTpr_Groupid = AV10group.gxTpr_Groupid;
            GXt_char2 = AV9error;
            GXt_char3 = AV11body.gxTpr_Groupencryptedkey;
            GXt_char4 = AV11body.gxTpr_Groupiv;
            GXt_char5 = AV11body.gxTpr_Groupencrypted;
            new GeneXus.Programs.distributedcrypto.encryptjson(context ).execute(  AV10group.ToJSonString(false, true),  AV27encryptKey, out  GXt_char3, out  GXt_char4, out  GXt_char5, out  GXt_char2) ;
            AV11body.gxTpr_Groupencryptedkey = GXt_char3;
            AV11body.gxTpr_Groupiv = GXt_char4;
            AV11body.gxTpr_Groupencrypted = GXt_char5;
            AV9error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               new desktopappservicesrestupdategrouppost(context ).execute(  AV25properties,  AV11body,  AV8externalUser.gxTpr_Externaltoken, out  AV24updateGroup__postOutputOUT, out  AV12HttpMessage, out  AV13IsSuccess) ;
               if ( ! AV13IsSuccess )
               {
                  AV9error = AV12HttpMessage.gxTpr_Description;
               }
               else
               {
                  AV9error = AV24updateGroup__postOutputOUT.gxTpr_Error;
                  AV26outGroupId = AV24updateGroup__postOutputOUT.gxTpr_Updatedgroupid;
               }
            }
         }
         else
         {
            AV9error = "You are not logged into Distributed Cryptography main site";
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
         AV26outGroupId = Guid.Empty;
         AV9error = "";
         AV8externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV11body = new SdtupdateGroup__postInput(context);
         GXt_char2 = "";
         GXt_char3 = "";
         GXt_char4 = "";
         GXt_char5 = "";
         AV25properties = new GXProperties();
         AV24updateGroup__postOutputOUT = new SdtupdateGroup__postOutput(context);
         AV12HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV27encryptKey ;
      private string AV9error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private string GXt_char5 ;
      private bool AV13IsSuccess ;
      private Guid AV26outGroupId ;
      private GXProperties AV25properties ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV8externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtupdateGroup__postInput AV11body ;
      private SdtupdateGroup__postOutput AV24updateGroup__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV12HttpMessage ;
      private Guid aP2_outGroupId ;
      private string aP3_error ;
   }

}
