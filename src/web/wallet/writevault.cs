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
   public class writevault : GXProcedure
   {
      public writevault( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public writevault( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ,
                           out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV21vault = aP1_vault;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault )
      {
         execute(aP0_groupId, aP1_vault, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ,
                                 out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV21vault = aP1_vault;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( (Guid.Empty==AV11groupId) )
         {
            GXt_char1 = AV8error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV21vault.ToJSonString(false, true), out  GXt_char1) ;
            AV8error = GXt_char1;
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT2 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT2) ;
         AV10group_sdt = GXt_SdtGroup_SDT2;
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) || ! ( AV10group_sdt.gxTpr_Grouptype == 40 ) )
         {
            AV8error = "We couldn't find the password group";
            cleanup();
            if (true) return;
         }
         if ( ! AV10group_sdt.gxTpr_Amigroupowner )
         {
            AV8error = "Only the owner of the group can change the passwords";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic3 = AV9externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic3) ;
         AV9externalUserPublic = GXt_SdtExternalUserPublic3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey)) )
         {
            AV8error = "The groups key is not available: please unlock the wallet and log in to the server again";
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV8error;
         GXt_char4 = AV10group_sdt.gxTpr_Encpassword;
         GXt_char5 = AV10group_sdt.gxTpr_Encryptedtextshare;
         new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV21vault.ToJSonString(false, true),  StringUtil.Trim( AV9externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey), out  GXt_char4, out  GXt_char5, out  GXt_char1) ;
         AV10group_sdt.gxTpr_Encpassword = GXt_char4;
         AV10group_sdt.gxTpr_Encryptedtextshare = GXt_char5;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV8error = "There was an error encrypting the vault: " + AV8error;
            cleanup();
            if (true) return;
         }
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV10group_sdt.gxTpr_Contact.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV10group_sdt.gxTpr_Contact.Item(AV22GXV1));
            AV14memberVault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
            AV23GXV2 = 1;
            while ( AV23GXV2 <= AV21vault.gxTpr_Password.Count )
            {
               AV16onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV21vault.gxTpr_Password.Item(AV23GXV2));
               AV18shared = false;
               AV24GXV3 = 1;
               while ( AV24GXV3 <= AV16onePassword.gxTpr_Contact.Count )
               {
                  AV19sharedWith = ((Guid)AV16onePassword.gxTpr_Contact.Item(AV24GXV3));
                  if ( AV19sharedWith == AV15oneContact.gxTpr_Contactid )
                  {
                     AV18shared = true;
                     if (true) break;
                  }
                  AV24GXV3 = (int)(AV24GXV3+1);
               }
               if ( AV18shared )
               {
                  AV14memberVault.gxTpr_Password.Add((GeneXus.Programs.wallet.SdtPassword)(AV16onePassword.Clone()), 0);
                  AV25GXV4 = 1;
                  while ( AV25GXV4 <= AV16onePassword.gxTpr_Password_tag.Count )
                  {
                     AV17oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV16onePassword.gxTpr_Password_tag.Item(AV25GXV4));
                     AV20tagFound = false;
                     AV26GXV5 = 1;
                     while ( AV26GXV5 <= AV14memberVault.gxTpr_Password_tag.Count )
                     {
                        AV13memberTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV14memberVault.gxTpr_Password_tag.Item(AV26GXV5));
                        if ( AV13memberTag.gxTpr_Tagid == AV17oneTag.gxTpr_Tagid )
                        {
                           AV20tagFound = true;
                           if (true) break;
                        }
                        AV26GXV5 = (int)(AV26GXV5+1);
                     }
                     if ( ! AV20tagFound )
                     {
                        AV14memberVault.gxTpr_Password_tag.Add((GeneXus.Programs.wallet.SdtPassword_tag)(AV17oneTag.Clone()), 0);
                     }
                     AV25GXV4 = (int)(AV25GXV4+1);
                  }
               }
               AV23GXV2 = (int)(AV23GXV2+1);
            }
            GXt_char5 = AV8error;
            GXt_char4 = AV15oneContact.gxTpr_Contactencryptedkey;
            GXt_char1 = AV15oneContact.gxTpr_Contactencryptedtext;
            new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV14memberVault.ToJSonString(false, true),  StringUtil.Trim( AV15oneContact.gxTpr_Contactuserpubkey), out  GXt_char4, out  GXt_char1, out  GXt_char5) ;
            AV15oneContact.gxTpr_Contactencryptedkey = GXt_char4;
            AV15oneContact.gxTpr_Contactencryptedtext = GXt_char1;
            AV8error = GXt_char5;
            AV15oneContact.gxTpr_Cleartextshare = "";
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV8error = "There was an error encrypting the passwords of " + StringUtil.Trim( AV15oneContact.gxTpr_Contactprivatename) + ": " + AV8error;
               cleanup();
               if (true) return;
            }
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         GXt_char5 = AV8error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV10group_sdt,  StringUtil.Trim( AV10group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV12grpupId, out  GXt_char5) ;
         AV8error = GXt_char5;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_char5 = AV8error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV10group_sdt, out  GXt_char5) ;
            AV8error = GXt_char5;
         }
         else
         {
            AV8error = "There was an error updating group on server: " + AV8error;
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
         AV8error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic3 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV15oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14memberVault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV16onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV19sharedWith = Guid.Empty;
         AV17oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV13memberTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         GXt_char4 = "";
         GXt_char1 = "";
         AV12grpupId = Guid.Empty;
         GXt_char5 = "";
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private int AV25GXV4 ;
      private int AV26GXV5 ;
      private string AV8error ;
      private string GXt_char4 ;
      private string GXt_char1 ;
      private string GXt_char5 ;
      private bool AV18shared ;
      private bool AV20tagFound ;
      private Guid AV11groupId ;
      private Guid AV19sharedWith ;
      private Guid AV12grpupId ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV21vault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV9externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15oneContact ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV14memberVault ;
      private GeneXus.Programs.wallet.SdtPassword AV16onePassword ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV17oneTag ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV13memberTag ;
      private string aP2_error ;
   }

}
