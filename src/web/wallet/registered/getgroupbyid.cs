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
   public class getgroupbyid : GXProcedure
   {
      public getgroupbyid( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupbyid( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_groupPassword ,
                           out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt ,
                           out string aP3_error )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10groupPassword = aP1_groupPassword;
         this.AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_group_sdt=this.AV8group_sdt;
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_groupPassword ,
                                out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt )
      {
         execute(aP0_groupId, aP1_groupPassword, out aP2_group_sdt, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_groupPassword ,
                                 out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt ,
                                 out string aP3_error )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10groupPassword = aP1_groupPassword;
         this.AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_group_sdt=this.AV8group_sdt;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV16externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV16externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16externalUser.gxTpr_Externaltoken)) )
         {
            new desktopappservicesrestgetgroupget(context ).execute(  AV13properties,  AV9groupId,  AV16externalUser.gxTpr_Externaltoken, out  AV12getGroup__getOutputOUT, out  AV14HttpMessage, out  AV15IsSuccess) ;
            if ( ! AV15IsSuccess )
            {
               AV11error = AV14HttpMessage.gxTpr_Description;
            }
            else
            {
               AV11error = AV12getGroup__getOutputOUT.gxTpr_Error;
               GXt_char2 = AV11error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonwithpassword(context ).execute(  AV12getGroup__getOutputOUT.gxTpr_Group.gxTpr_Groupencrypted,  AV10groupPassword, out  AV18group_sdt_char, out  GXt_char2) ;
               AV11error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV8group_sdt.FromJSonString(AV18group_sdt_char, null);
               }
               else
               {
                  AV11error = "There was an error unencrypting the group information: " + AV11error;
               }
            }
         }
         else
         {
            AV11error = "You are not logged into Distributed Cryptography main site";
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
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11error = "";
         AV16externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13properties = new GXProperties();
         AV12getGroup__getOutputOUT = new SdtgetGroup__getOutput(context);
         AV14HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         GXt_char2 = "";
         AV18group_sdt_char = "";
         /* GeneXus formulas. */
      }

      private string AV10groupPassword ;
      private string AV11error ;
      private string GXt_char2 ;
      private bool AV15IsSuccess ;
      private string AV18group_sdt_char ;
      private Guid AV9groupId ;
      private GXProperties AV13properties ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV16externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtgetGroup__getOutput AV12getGroup__getOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV14HttpMessage ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT aP2_group_sdt ;
      private string aP3_error ;
   }

}
