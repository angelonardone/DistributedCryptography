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
   public class getcontacts : GXProcedure
   {
      public getcontacts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcontacts( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> aP0_allContacts ,
                           out string aP1_error )
      {
         this.AV30allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography") ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_allContacts=this.AV30allContacts;
         aP1_error=this.AV9error;
      }

      public string executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> aP0_allContacts )
      {
         execute(out aP0_allContacts, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> aP0_allContacts ,
                                 out string aP1_error )
      {
         this.AV30allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography") ;
         this.AV9error = "" ;
         SubmitImpl();
         aP0_allContacts=this.AV30allContacts;
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV10externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10externalUser.gxTpr_Externaltoken)) )
         {
            new desktopappservicesrestgetusercontactsget(context ).execute(  AV13properties,  AV10externalUser.gxTpr_Externaltoken, out  AV32getUserContacts__getOutput, out  AV11HttpMessage, out  AV12IsSuccess) ;
            if ( ! AV12IsSuccess )
            {
               AV9error = AV11HttpMessage.gxTpr_Description;
            }
            else
            {
               AV9error = AV32getUserContacts__getOutput.gxTpr_Error;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV25contacts = AV32getUserContacts__getOutput.gxTpr_Contacts;
                  AV33GXV1 = 1;
                  while ( AV33GXV1 <= AV25contacts.gxTpr_Contact.Count )
                  {
                     AV26contact = ((SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem)AV25contacts.gxTpr_Contact.Item(AV33GXV1));
                     GXt_char2 = AV9error;
                     new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV26contact.gxTpr_Contactencrypted,  AV26contact.gxTpr_Encryptedkey, out  AV27clearText, out  GXt_char2) ;
                     AV9error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
                     {
                        AV29oneContact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                        AV29oneContact.FromJSonString(AV27clearText, null);
                        AV29oneContact.gxTpr_Contactrid = AV26contact.gxTpr_Contactid;
                        AV30allContacts.Add(AV29oneContact, 0);
                     }
                     else
                     {
                        cleanup();
                        if (true) return;
                     }
                     AV33GXV1 = (int)(AV33GXV1+1);
                  }
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
         AV30allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV9error = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13properties = new GXProperties();
         AV32getUserContacts__getOutput = new SdtgetUserContacts__getOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV25contacts = new SdtDesktopApp_services_SDT_Contacts(context);
         AV26contact = new SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem(context);
         GXt_char2 = "";
         AV27clearText = "";
         AV29oneContact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV33GXV1 ;
      private string AV9error ;
      private string GXt_char2 ;
      private bool AV12IsSuccess ;
      private string AV27clearText ;
      private GXProperties AV13properties ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV30allContacts ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtgetUserContacts__getOutput AV32getUserContacts__getOutput ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private SdtDesktopApp_services_SDT_Contacts AV25contacts ;
      private SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem AV26contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV29oneContact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> aP0_allContacts ;
      private string aP1_error ;
   }

}
