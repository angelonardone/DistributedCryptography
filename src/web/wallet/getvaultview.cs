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
   public class getvaultview : GXProcedure
   {
      public getvaultview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getvaultview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_tagId ,
                           Guid aP2_contactId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow> aP3_rows ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_tags ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_contacts ,
                           out bool aP6_isOwner ,
                           out string aP7_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV21tagId = aP1_tagId;
         this.AV8contactId = aP2_contactId;
         this.AV19rows = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow>( context, "VaultRow", "distributedcryptography") ;
         this.AV23tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV10contacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV14isOwner = false ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP3_rows=this.AV19rows;
         aP4_tags=this.AV23tags;
         aP5_contacts=this.AV10contacts;
         aP6_isOwner=this.AV14isOwner;
         aP7_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_tagId ,
                                Guid aP2_contactId ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow> aP3_rows ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_tags ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_contacts ,
                                out bool aP6_isOwner )
      {
         execute(aP0_groupId, aP1_tagId, aP2_contactId, out aP3_rows, out aP4_tags, out aP5_contacts, out aP6_isOwner, out aP7_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_tagId ,
                                 Guid aP2_contactId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow> aP3_rows ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_tags ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_contacts ,
                                 out bool aP6_isOwner ,
                                 out string aP7_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV21tagId = aP1_tagId;
         this.AV8contactId = aP2_contactId;
         this.AV19rows = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow>( context, "VaultRow", "distributedcryptography") ;
         this.AV23tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography") ;
         this.AV10contacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography") ;
         this.AV14isOwner = false ;
         this.AV11error = "" ;
         SubmitImpl();
         aP3_rows=this.AV19rows;
         aP4_tags=this.AV23tags;
         aP5_contacts=this.AV10contacts;
         aP6_isOwner=this.AV14isOwner;
         aP7_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV11error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV13groupId, out  AV24vault, out  AV14isOwner, out  GXt_char1) ;
         AV11error = GXt_char1;
         AV19rows.Clear();
         AV23tags.Clear();
         AV10contacts.Clear();
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         AV23tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV24vault.gxTpr_Password_tag.Clone());
         if ( ! (Guid.Empty==AV13groupId) && AV14isOwner )
         {
            GXt_SdtGroup_SDT2 = AV12group_sdt;
            new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV13groupId, out  GXt_SdtGroup_SDT2) ;
            AV12group_sdt = GXt_SdtGroup_SDT2;
            AV26GXV1 = 1;
            while ( AV26GXV1 <= AV12group_sdt.gxTpr_Contact.Count )
            {
               AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_sdt.gxTpr_Contact.Item(AV26GXV1));
               if ( ! (DateTime.MinValue==AV15oneContact.gxTpr_Contactinvitacionaccepted) )
               {
                  AV25vaultContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
                  AV25vaultContact.gxTpr_Contactid = AV15oneContact.gxTpr_Contactid;
                  AV25vaultContact.gxTpr_Contactprivatename = AV15oneContact.gxTpr_Contactprivatename;
                  AV10contacts.Add(AV25vaultContact, 0);
               }
               AV26GXV1 = (int)(AV26GXV1+1);
            }
         }
         AV27GXV2 = 1;
         while ( AV27GXV2 <= AV24vault.gxTpr_Password.Count )
         {
            AV16onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV24vault.gxTpr_Password.Item(AV27GXV2));
            AV22tagOk = (Guid.Empty==AV21tagId);
            AV28GXV3 = 1;
            while ( AV28GXV3 <= AV16onePassword.gxTpr_Password_tag.Count )
            {
               AV17oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV16onePassword.gxTpr_Password_tag.Item(AV28GXV3));
               if ( AV17oneTag.gxTpr_Tagid == AV21tagId )
               {
                  AV22tagOk = true;
                  if (true) break;
               }
               AV28GXV3 = (int)(AV28GXV3+1);
            }
            AV9contactOk = (Guid.Empty==AV8contactId);
            AV29GXV4 = 1;
            while ( AV29GXV4 <= AV16onePassword.gxTpr_Contact.Count )
            {
               AV20sharedWith = ((Guid)AV16onePassword.gxTpr_Contact.Item(AV29GXV4));
               if ( AV20sharedWith == AV8contactId )
               {
                  AV9contactOk = true;
                  if (true) break;
               }
               AV29GXV4 = (int)(AV29GXV4+1);
            }
            if ( AV22tagOk && AV9contactOk )
            {
               AV18row = new GeneXus.Programs.wallet.SdtVaultRow(context);
               AV18row.gxTpr_Passwordid = AV16onePassword.gxTpr_Passwordid;
               AV18row.gxTpr_Description = AV16onePassword.gxTpr_Description;
               AV18row.gxTpr_Url = AV16onePassword.gxTpr_Url;
               AV18row.gxTpr_Login = AV16onePassword.gxTpr_Login;
               AV18row.gxTpr_Hasauthenticator = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV16onePassword.gxTpr_Based32key)));
               AV18row.gxTpr_Hasnote = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV16onePassword.gxTpr_Note)));
               AV19rows.Add(AV18row, 0);
            }
            AV27GXV2 = (int)(AV27GXV2+1);
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
         AV19rows = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow>( context, "VaultRow", "distributedcryptography");
         AV23tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV10contacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography");
         AV11error = "";
         GXt_char1 = "";
         AV24vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV25vaultContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
         AV16onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV17oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV20sharedWith = Guid.Empty;
         AV18row = new GeneXus.Programs.wallet.SdtVaultRow(context);
         /* GeneXus formulas. */
      }

      private int AV26GXV1 ;
      private int AV27GXV2 ;
      private int AV28GXV3 ;
      private int AV29GXV4 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV14isOwner ;
      private bool AV22tagOk ;
      private bool AV9contactOk ;
      private Guid AV13groupId ;
      private Guid AV21tagId ;
      private Guid AV8contactId ;
      private Guid AV20sharedWith ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow> AV19rows ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV23tags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV10contacts ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV24vault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15oneContact ;
      private GeneXus.Programs.wallet.SdtVaultContact AV25vaultContact ;
      private GeneXus.Programs.wallet.SdtPassword AV16onePassword ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV17oneTag ;
      private GeneXus.Programs.wallet.SdtVaultRow AV18row ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultRow> aP3_rows ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP4_tags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP5_contacts ;
      private bool aP6_isOwner ;
      private string aP7_error ;
   }

}
