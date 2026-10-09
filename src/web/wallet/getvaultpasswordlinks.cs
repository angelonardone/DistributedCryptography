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
namespace GeneXus.Programs.wallet {
   public class getvaultpasswordlinks : GXProcedure
   {
      public getvaultpasswordlinks( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getvaultpasswordlinks( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           out string aP2_description ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP3_assignedTags ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_allTags ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_assignedContacts ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP6_availableContacts ,
                           out string aP7_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV20passwordId = aP1_passwordId;
         this.AV12description = "" ;
         this.AV10assignedTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV8allTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV9assignedContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV11availableContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP2_description=this.AV12description;
         aP3_assignedTags=this.AV10assignedTags;
         aP4_allTags=this.AV8allTags;
         aP5_assignedContacts=this.AV9assignedContacts;
         aP6_availableContacts=this.AV11availableContacts;
         aP7_error=this.AV13error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_passwordId ,
                                out string aP2_description ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP3_assignedTags ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_allTags ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_assignedContacts ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP6_availableContacts )
      {
         execute(aP0_groupId, aP1_passwordId, out aP2_description, out aP3_assignedTags, out aP4_allTags, out aP5_assignedContacts, out aP6_availableContacts, out aP7_error);
         return AV13error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_passwordId ,
                                 out string aP2_description ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP3_assignedTags ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_allTags ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_assignedContacts ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP6_availableContacts ,
                                 out string aP7_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV20passwordId = aP1_passwordId;
         this.AV12description = "" ;
         this.AV10assignedTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV8allTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV9assignedContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV11availableContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV13error = "" ;
         SubmitImpl();
         aP2_description=this.AV12description;
         aP3_assignedTags=this.AV10assignedTags;
         aP4_allTags=this.AV8allTags;
         aP5_assignedContacts=this.AV9assignedContacts;
         aP6_availableContacts=this.AV11availableContacts;
         aP7_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10assignedTags.Clear();
         AV8allTags.Clear();
         AV9assignedContacts.Clear();
         AV11availableContacts.Clear();
         GXt_char1 = AV13error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV16groupId, out  AV22vault, out  AV17isOwner, out  GXt_char1) ;
         AV13error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV8allTags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV22vault.gxTpr_Password_tag.Clone());
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV22vault.gxTpr_Password.Count )
         {
            AV19onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV22vault.gxTpr_Password.Item(AV24GXV1));
            if ( AV19onePassword.gxTpr_Passwordid == AV20passwordId )
            {
               AV12description = AV19onePassword.gxTpr_Description;
               AV10assignedTags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV19onePassword.gxTpr_Password_tag.Clone());
               AV14found = true;
               if (true) break;
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         if ( ! AV14found )
         {
            AV13error = "We couldn't find that password, please refresh the page";
            cleanup();
            if (true) return;
         }
         if ( ! (Guid.Empty==AV16groupId) && AV17isOwner )
         {
            GXt_SdtGroup_SDT2 = AV15group_sdt;
            new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV16groupId, out  GXt_SdtGroup_SDT2) ;
            AV15group_sdt = GXt_SdtGroup_SDT2;
            AV25GXV2 = 1;
            while ( AV25GXV2 <= AV15group_sdt.gxTpr_Contact.Count )
            {
               AV18oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV25GXV2));
               if ( ! (DateTime.MinValue==AV18oneContact.gxTpr_Contactinvitacionaccepted) )
               {
                  AV23vaultContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
                  AV23vaultContact.gxTpr_Contactid = AV18oneContact.gxTpr_Contactid;
                  AV23vaultContact.gxTpr_Contactprivatename = AV18oneContact.gxTpr_Contactprivatename;
                  AV11availableContacts.Add(AV23vaultContact, 0);
               }
               AV25GXV2 = (int)(AV25GXV2+1);
            }
            AV26GXV3 = 1;
            while ( AV26GXV3 <= AV19onePassword.gxTpr_Contact.Count )
            {
               AV21sharedWith = ((Guid)AV19onePassword.gxTpr_Contact.Item(AV26GXV3));
               AV27GXV4 = 1;
               while ( AV27GXV4 <= AV15group_sdt.gxTpr_Contact.Count )
               {
                  AV18oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV27GXV4));
                  if ( AV18oneContact.gxTpr_Contactid == AV21sharedWith )
                  {
                     AV23vaultContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
                     AV23vaultContact.gxTpr_Contactid = AV18oneContact.gxTpr_Contactid;
                     AV23vaultContact.gxTpr_Contactprivatename = AV18oneContact.gxTpr_Contactprivatename;
                     AV9assignedContacts.Add(AV23vaultContact, 0);
                  }
                  AV27GXV4 = (int)(AV27GXV4+1);
               }
               AV26GXV3 = (int)(AV26GXV3+1);
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
         AV12description = "";
         AV10assignedTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV8allTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV9assignedContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography");
         AV11availableContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography");
         AV13error = "";
         GXt_char1 = "";
         AV22vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV19onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV23vaultContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
         AV21sharedWith = Guid.Empty;
         /* GeneXus formulas. */
      }

      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private int AV26GXV3 ;
      private int AV27GXV4 ;
      private string AV12description ;
      private string AV13error ;
      private string GXt_char1 ;
      private bool AV17isOwner ;
      private bool AV14found ;
      private Guid AV16groupId ;
      private Guid AV20passwordId ;
      private Guid AV21sharedWith ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV10assignedTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV8allTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV9assignedContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV11availableContacts ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV22vault ;
      private GeneXus.Programs.wallet.SdtPassword AV19onePassword ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV18oneContact ;
      private GeneXus.Programs.wallet.SdtVaultContact AV23vaultContact ;
      private string aP2_description ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP3_assignedTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_allTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_assignedContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP6_availableContacts ;
      private string aP7_error ;
   }

}
