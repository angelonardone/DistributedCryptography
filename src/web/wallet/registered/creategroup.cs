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
   public class creategroup : GXProcedure
   {
      public creategroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public creategroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                           out Guid aP1_groupId ,
                           out string aP2_error )
      {
         this.AV10group = aP0_group;
         this.AV11groupId = Guid.Empty ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_groupId=this.AV11groupId;
         aP2_error=this.AV8error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                                out Guid aP1_groupId )
      {
         execute(aP0_group, out aP1_groupId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group ,
                                 out Guid aP1_groupId ,
                                 out string aP2_error )
      {
         this.AV10group = aP0_group;
         this.AV11groupId = Guid.Empty ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_groupId=this.AV11groupId;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUser.gxTpr_Externaltoken)) )
         {
            GXt_char2 = AV8error;
            GXt_char3 = AV12body.gxTpr_Groupencryptedkey;
            GXt_char4 = AV12body.gxTpr_Groupencrypted;
            new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV10group.ToJSonString(false, true),  "", out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
            AV12body.gxTpr_Groupencryptedkey = GXt_char3;
            AV12body.gxTpr_Groupencrypted = GXt_char4;
            AV8error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               new desktopappservicesrestcreategrouppost(context ).execute(  AV16properties,  AV12body,  AV9externalUser.gxTpr_Externaltoken, out  AV13createGroup__postOutputOUT, out  AV14HttpMessage, out  AV15IsSuccess) ;
               if ( ! AV15IsSuccess )
               {
                  AV8error = AV14HttpMessage.gxTpr_Description;
               }
               else
               {
                  AV8error = AV13createGroup__postOutputOUT.gxTpr_Error;
                  AV11groupId = AV13createGroup__postOutputOUT.gxTpr_Groupid;
               }
            }
         }
         else
         {
            AV8error = "You are not logged into Distributed Cryptography main site";
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
         AV11groupId = Guid.Empty;
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         AV12body = new SdtcreateGroup__postInput(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV16properties = new GXProperties();
         AV13createGroup__postOutputOUT = new SdtcreateGroup__postOutput(context);
         AV14HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV15IsSuccess ;
      private Guid AV11groupId ;
      private GXProperties AV16properties ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtcreateGroup__postInput AV12body ;
      private SdtcreateGroup__postOutput AV13createGroup__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV14HttpMessage ;
      private Guid aP1_groupId ;
      private string aP2_error ;
   }

}
