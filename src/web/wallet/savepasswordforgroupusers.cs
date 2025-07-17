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
namespace GeneXus.Programs.wallet {
   public class savepasswordforgroupusers : GXProcedure
   {
      public savepasswordforgroupusers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savepasswordforgroupusers( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Execute user subroutine: 'READ PASSWORDS_AND_TAGS' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV29Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV30Passwords_and_tags.gxTpr_Password.Clone());
         AV27Password_for_User.Clear();
         AV39GXV1 = 1;
         while ( AV39GXV1 <= AV29Passwords.Count )
         {
            AV24onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV29Passwords.Item(AV39GXV1));
            AV40GXV2 = 1;
            while ( AV40GXV2 <= AV24onePassword.gxTpr_Contact.Count )
            {
               AV9contactId = ((Guid)AV24onePassword.gxTpr_Contact.Item(AV40GXV2));
               AV31tempPassword_for_User = new GeneXus.Programs.wallet.registered.SdtPassword_for_User(context);
               AV31tempPassword_for_User.gxTpr_Userid = AV9contactId;
               AV31tempPassword_for_User.gxTpr_Passwords.Add((GeneXus.Programs.wallet.SdtPassword)(AV24onePassword.Clone()), 0);
               AV41GXV3 = 1;
               while ( AV41GXV3 <= AV24onePassword.gxTpr_Password_tag.Count )
               {
                  AV20one_Password_tag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV24onePassword.gxTpr_Password_tag.Item(AV41GXV3));
                  AV31tempPassword_for_User.gxTpr_Password_tag.Add((GeneXus.Programs.wallet.SdtPassword_tag)(AV20one_Password_tag.Clone()), 0);
                  AV41GXV3 = (int)(AV41GXV3+1);
               }
               AV27Password_for_User.Add(AV31tempPassword_for_User, 0);
               AV40GXV2 = (int)(AV40GXV2+1);
            }
            AV39GXV1 = (int)(AV39GXV1+1);
         }
         AV19index = 1;
         AV27Password_for_User.Sort("UserId");
         AV42GXV4 = 1;
         while ( AV42GXV4 <= AV27Password_for_User.Count )
         {
            AV25onePassword_for_User = ((GeneXus.Programs.wallet.registered.SdtPassword_for_User)AV27Password_for_User.Item(AV42GXV4));
            if ( AV19index == 1 )
            {
               AV35Password_for_User_consolidated.Add((GeneXus.Programs.wallet.registered.SdtPassword_for_User)(AV25onePassword_for_User.Clone()), 0);
            }
            else
            {
               if ( AV25onePassword_for_User.gxTpr_Userid == ((GeneXus.Programs.wallet.registered.SdtPassword_for_User)AV35Password_for_User_consolidated.Item(AV19index-1)).gxTpr_Userid )
               {
                  AV43GXV5 = 1;
                  while ( AV43GXV5 <= AV25onePassword_for_User.gxTpr_Passwords.Count )
                  {
                     AV24onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV25onePassword_for_User.gxTpr_Passwords.Item(AV43GXV5));
                     ((GeneXus.Programs.wallet.registered.SdtPassword_for_User)AV35Password_for_User_consolidated.Item(AV19index-1)).gxTpr_Passwords.Add((GeneXus.Programs.wallet.SdtPassword)(AV24onePassword.Clone()), 0);
                     AV43GXV5 = (int)(AV43GXV5+1);
                  }
                  AV44GXV6 = 1;
                  while ( AV44GXV6 <= AV25onePassword_for_User.gxTpr_Password_tag.Count )
                  {
                     AV20one_Password_tag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV25onePassword_for_User.gxTpr_Password_tag.Item(AV44GXV6));
                     ((GeneXus.Programs.wallet.registered.SdtPassword_for_User)AV35Password_for_User_consolidated.Item(AV19index-1)).gxTpr_Password_tag.Add((GeneXus.Programs.wallet.SdtPassword_tag)(AV20one_Password_tag.Clone()), 0);
                     AV44GXV6 = (int)(AV44GXV6+1);
                  }
               }
               else
               {
                  AV35Password_for_User_consolidated.Add((GeneXus.Programs.wallet.registered.SdtPassword_for_User)(AV25onePassword_for_User.Clone()), 0);
               }
            }
            AV19index = (short)(AV19index+1);
            AV42GXV4 = (int)(AV42GXV4+1);
         }
         AV37foundContacts = (GxSimpleCollection<Guid>)(new GxSimpleCollection<Guid>());
         AV45GXV7 = 1;
         while ( AV45GXV7 <= AV35Password_for_User_consolidated.Count )
         {
            AV25onePassword_for_User = ((GeneXus.Programs.wallet.registered.SdtPassword_for_User)AV35Password_for_User_consolidated.Item(AV45GXV7));
            AV46GXV8 = 1;
            while ( AV46GXV8 <= AV17group_sdt.gxTpr_Contact.Count )
            {
               AV21oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV46GXV8));
               if ( AV21oneContact.gxTpr_Contactid == AV25onePassword_for_User.gxTpr_Userid )
               {
                  AV37foundContacts.Add(AV21oneContact.gxTpr_Contactid, 0);
                  AV33UserPasswords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
                  AV33UserPasswords_and_tags.gxTpr_Password = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV25onePassword_for_User.gxTpr_Passwords.Clone());
                  AV33UserPasswords_and_tags.gxTpr_Password_tag = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV25onePassword_for_User.gxTpr_Password_tag.Clone());
                  GXt_char1 = AV12error;
                  GXt_char2 = AV21oneContact.gxTpr_Contactencryptedkey;
                  GXt_char3 = AV21oneContact.gxTpr_Contactencryptedtext;
                  new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV33UserPasswords_and_tags.ToJSonString(false, true),  StringUtil.Trim( AV21oneContact.gxTpr_Contactuserpubkey), out  GXt_char2, out  GXt_char3, out  GXt_char1) ;
                  AV21oneContact.gxTpr_Contactencryptedkey = GXt_char2;
                  AV21oneContact.gxTpr_Contactencryptedtext = GXt_char3;
                  AV12error = GXt_char1;
                  AV21oneContact.gxTpr_Cleartextshare = AV33UserPasswords_and_tags.ToJSonString(false, true);
               }
               AV46GXV8 = (int)(AV46GXV8+1);
            }
            AV45GXV7 = (int)(AV45GXV7+1);
         }
         AV47GXV9 = 1;
         while ( AV47GXV9 <= AV17group_sdt.gxTpr_Contact.Count )
         {
            AV21oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV47GXV9));
            AV38contactFound = false;
            AV48GXV10 = 1;
            while ( AV48GXV10 <= AV37foundContacts.Count )
            {
               AV22oneContactItem = ((Guid)AV37foundContacts.Item(AV48GXV10));
               if ( AV21oneContact.gxTpr_Contactid == AV22oneContactItem )
               {
                  AV38contactFound = true;
                  if (true) break;
               }
               AV48GXV10 = (int)(AV48GXV10+1);
            }
            if ( ! AV38contactFound )
            {
               AV33UserPasswords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
               GXt_char3 = AV12error;
               GXt_char2 = AV21oneContact.gxTpr_Contactencryptedkey;
               GXt_char1 = AV21oneContact.gxTpr_Contactencryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV33UserPasswords_and_tags.ToJSonString(false, true),  StringUtil.Trim( AV21oneContact.gxTpr_Contactuserpubkey), out  GXt_char2, out  GXt_char1, out  GXt_char3) ;
               AV21oneContact.gxTpr_Contactencryptedkey = GXt_char2;
               AV21oneContact.gxTpr_Contactencryptedtext = GXt_char1;
               AV12error = GXt_char3;
               AV21oneContact.gxTpr_Cleartextshare = AV33UserPasswords_and_tags.ToJSonString(false, true);
            }
            AV47GXV9 = (int)(AV47GXV9+1);
         }
         AV34websession.Set("Group_EDIT", AV17group_sdt.ToJSonString(false, true));
         cleanup();
      }

      protected void S111( )
      {
         /* 'READ PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV17group_sdt.FromJSonString(AV34websession.Get("Group_EDIT"), null);
         GXt_char3 = AV12error;
         new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV17group_sdt.gxTpr_Encryptedtextshare,  AV17group_sdt.gxTpr_Encpassword, out  AV8clearText, out  GXt_char3) ;
         AV12error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            if ( StringUtil.StrCmp(AV8clearText, "_empty_") == 0 )
            {
               AV8clearText = "";
            }
            AV30Passwords_and_tags.FromJSonString(AV8clearText, null);
         }
         else
         {
            GX_msglist.addItem(AV12error);
         }
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
         AV29Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV30Passwords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV27Password_for_User = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtPassword_for_User>( context, "Password_for_User", "distributedcryptography");
         AV24onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV9contactId = Guid.Empty;
         AV31tempPassword_for_User = new GeneXus.Programs.wallet.registered.SdtPassword_for_User(context);
         AV20one_Password_tag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV25onePassword_for_User = new GeneXus.Programs.wallet.registered.SdtPassword_for_User(context);
         AV35Password_for_User_consolidated = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtPassword_for_User>( context, "Password_for_User", "distributedcryptography");
         AV37foundContacts = new GxSimpleCollection<Guid>();
         AV17group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV33UserPasswords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV12error = "";
         AV22oneContactItem = Guid.Empty;
         GXt_char2 = "";
         GXt_char1 = "";
         AV34websession = context.GetSession();
         GXt_char3 = "";
         AV8clearText = "";
         /* GeneXus formulas. */
      }

      private short AV19index ;
      private int AV39GXV1 ;
      private int AV40GXV2 ;
      private int AV41GXV3 ;
      private int AV42GXV4 ;
      private int AV43GXV5 ;
      private int AV44GXV6 ;
      private int AV45GXV7 ;
      private int AV46GXV8 ;
      private int AV47GXV9 ;
      private int AV48GXV10 ;
      private string AV12error ;
      private string GXt_char2 ;
      private string GXt_char1 ;
      private string GXt_char3 ;
      private bool returnInSub ;
      private bool AV38contactFound ;
      private string AV8clearText ;
      private Guid AV9contactId ;
      private Guid AV22oneContactItem ;
      private IGxSession AV34websession ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV29Passwords ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV30Passwords_and_tags ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtPassword_for_User> AV27Password_for_User ;
      private GeneXus.Programs.wallet.SdtPassword AV24onePassword ;
      private GeneXus.Programs.wallet.registered.SdtPassword_for_User AV31tempPassword_for_User ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV20one_Password_tag ;
      private GeneXus.Programs.wallet.registered.SdtPassword_for_User AV25onePassword_for_User ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtPassword_for_User> AV35Password_for_User_consolidated ;
      private GxSimpleCollection<Guid> AV37foundContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV21oneContact ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV33UserPasswords_and_tags ;
   }

}
