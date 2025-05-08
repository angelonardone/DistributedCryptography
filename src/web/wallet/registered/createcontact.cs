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
   public class createcontact : GXProcedure
   {
      public createcontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createcontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                           out Guid aP1_ContactId ,
                           out string aP2_error )
      {
         this.AV26contact = aP0_contact;
         this.AV25ContactId = Guid.Empty ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_ContactId=this.AV25ContactId;
         aP2_error=this.AV8error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                out Guid aP1_ContactId )
      {
         execute(aP0_contact, out aP1_ContactId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ,
                                 out Guid aP1_ContactId ,
                                 out string aP2_error )
      {
         this.AV26contact = aP0_contact;
         this.AV25ContactId = Guid.Empty ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_ContactId=this.AV25ContactId;
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
            GXt_char3 = AV13createContact__postInput.gxTpr_Encryptedkey;
            GXt_char4 = AV13createContact__postInput.gxTpr_Contactencrypted;
            new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV26contact.ToJSonString(false, true),  "", out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
            AV13createContact__postInput.gxTpr_Encryptedkey = GXt_char3;
            AV13createContact__postInput.gxTpr_Contactencrypted = GXt_char4;
            AV8error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               new desktopappservicesrestcreatecontactpost(context ).execute(  AV12properties,  AV13createContact__postInput,  AV9externalUser.gxTpr_Externaltoken, out  AV24createContact__postOutput, out  AV10HttpMessage, out  AV11IsSuccess) ;
               if ( ! AV11IsSuccess )
               {
                  AV8error = AV10HttpMessage.gxTpr_Description;
               }
               else
               {
                  AV8error = AV24createContact__postOutput.gxTpr_Error;
                  AV25ContactId = AV24createContact__postOutput.gxTpr_Contactid;
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
         AV25ContactId = Guid.Empty;
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         AV13createContact__postInput = new SdtcreateContact__postInput(context);
         GXt_char3 = "";
         GXt_char4 = "";
         AV12properties = new GXProperties();
         AV24createContact__postOutput = new SdtcreateContact__postOutput(context);
         AV10HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private bool AV11IsSuccess ;
      private Guid AV25ContactId ;
      private GXProperties AV12properties ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV26contact ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtcreateContact__postInput AV13createContact__postInput ;
      private SdtcreateContact__postOutput AV24createContact__postOutput ;
      private GeneXus.Utils.SdtMessages_Message AV10HttpMessage ;
      private Guid aP1_ContactId ;
      private string aP2_error ;
   }

}
