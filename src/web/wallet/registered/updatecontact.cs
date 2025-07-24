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
   public class updatecontact : GXProcedure
   {
      public updatecontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updatecontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                           Guid aP1_ContactId ,
                           out string aP2_error )
      {
         this.AV23contact = aP0_contact;
         this.AV8ContactId = aP1_ContactId;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV13error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                Guid aP1_ContactId )
      {
         execute(aP0_contact, aP1_ContactId, out aP2_error);
         return AV13error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                 Guid aP1_ContactId ,
                                 out string aP2_error )
      {
         this.AV23contact = aP0_contact;
         this.AV8ContactId = aP1_ContactId;
         this.AV13error = "" ;
         SubmitImpl();
         aP2_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV14externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV14externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14externalUser.gxTpr_Externaltoken)) )
         {
            GXt_char2 = AV13error;
            GXt_char3 = AV24body.gxTpr_Encryptedkey;
            GXt_char4 = AV24body.gxTpr_Contactencrypted;
            new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV23contact.ToJSonString(false, true),  "", out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
            AV24body.gxTpr_Encryptedkey = GXt_char3;
            AV24body.gxTpr_Contactencrypted = GXt_char4;
            AV13error = GXt_char2;
            AV24body.gxTpr_Contactid = AV8ContactId;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               new desktopappservicesrestupdatecontactpost(context ).execute(  AV45ServerUrlTemplatingVar,  AV24body,  AV14externalUser.gxTpr_Externaltoken, out  AV46updateContact__postOutputOUT, out  AV16HttpMessage, out  AV17IsSuccess) ;
               if ( ! AV17IsSuccess )
               {
                  AV13error = AV16HttpMessage.gxTpr_Description;
               }
               else
               {
                  AV13error = AV46updateContact__postOutputOUT.gxTpr_Error;
                  AV47Outcontactid = AV46updateContact__postOutputOUT.gxTpr_Updatedcontactid;
               }
            }
         }
         else
         {
            AV13error = "You are not logged into Distributed Cryptography main site";
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
         AV14externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         AV24body = new SdtupdateContact__postInput(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV45ServerUrlTemplatingVar = new GXProperties();
         AV46updateContact__postOutputOUT = new SdtupdateContact__postOutput(context);
         AV16HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV47Outcontactid = Guid.Empty;
         /* GeneXus formulas. */
      }

      private string AV13error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV17IsSuccess ;
      private Guid AV8ContactId ;
      private Guid AV47Outcontactid ;
      private GXProperties AV45ServerUrlTemplatingVar ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV23contact ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV14externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtupdateContact__postInput AV24body ;
      private SdtupdateContact__postOutput AV46updateContact__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV16HttpMessage ;
      private string aP2_error ;
   }

}
