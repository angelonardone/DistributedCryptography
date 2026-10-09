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
   public class readvault : GXProcedure
   {
      public readvault( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readvault( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ,
                           out bool aP2_isOwner ,
                           out string aP3_error )
      {
         this.AV15groupId = aP0_groupId;
         this.AV17vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context) ;
         this.AV16isOwner = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_vault=this.AV17vault;
         aP2_isOwner=this.AV16isOwner;
         aP3_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ,
                                out bool aP2_isOwner )
      {
         execute(aP0_groupId, out aP1_vault, out aP2_isOwner, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ,
                                 out bool aP2_isOwner ,
                                 out string aP3_error )
      {
         this.AV15groupId = aP0_groupId;
         this.AV17vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context) ;
         this.AV16isOwner = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_vault=this.AV17vault;
         aP2_isOwner=this.AV16isOwner;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV17vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV16isOwner = true;
         if ( (Guid.Empty==AV15groupId) )
         {
            AV17vault.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "encpasswords.enc", out  AV11fileError), null);
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV15groupId, out  GXt_SdtGroup_SDT1) ;
         AV13group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV13group_sdt.gxTpr_Groupid) || ! ( AV13group_sdt.gxTpr_Grouptype == 40 ) )
         {
            AV9error = "We couldn't find the password group";
            cleanup();
            if (true) return;
         }
         AV16isOwner = AV13group_sdt.gxTpr_Amigroupowner;
         if ( AV13group_sdt.gxTpr_Amigroupowner )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.distcrypt.decryptwithgroupskey(context ).execute(  AV13group_sdt.gxTpr_Encryptedtextshare,  AV13group_sdt.gxTpr_Encpassword, out  AV8clearText, out  GXt_char2) ;
            AV9error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_char2 = AV9error;
               new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV13group_sdt.gxTpr_Encryptedtextshare,  AV13group_sdt.gxTpr_Encpassword, out  AV8clearText, out  GXt_char2) ;
               AV9error = GXt_char2;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               if ( StringUtil.StrCmp(AV8clearText, "_empty_") == 0 )
               {
                  AV8clearText = "";
               }
               AV17vault.FromJSonString(AV8clearText, null);
            }
         }
         else
         {
            GXt_SdtExternalUserPublic3 = AV10externalUserPublic;
            new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic3) ;
            AV10externalUserPublic = GXt_SdtExternalUserPublic3;
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV12group_owner, out  GXt_char2) ;
            AV9error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV9error = "We have problems reading the group from the server: " + AV9error;
               cleanup();
               if (true) return;
            }
            AV18GXV1 = 1;
            while ( AV18GXV1 <= AV12group_owner.gxTpr_Contact.Count )
            {
               AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_owner.gxTpr_Contact.Item(AV18GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Contactusername), StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
               {
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14groupContact.gxTpr_Contactencryptedtext)) )
                  {
                     GXt_char2 = AV9error;
                     new GeneXus.Programs.distcrypt.decryptwithgroupskey(context ).execute(  AV14groupContact.gxTpr_Contactencryptedtext,  AV14groupContact.gxTpr_Contactencryptedkey, out  AV8clearText, out  GXt_char2) ;
                     AV9error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
                     {
                        AV17vault.FromJSonString(AV8clearText, null);
                     }
                  }
                  if (true) break;
               }
               AV18GXV1 = (int)(AV18GXV1+1);
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
         AV17vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV9error = "";
         AV11fileError = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8clearText = "";
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic3 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12group_owner = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private string AV9error ;
      private string AV11fileError ;
      private string GXt_char2 ;
      private bool AV16isOwner ;
      private string AV8clearText ;
      private Guid AV15groupId ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV17vault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_owner ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags aP1_vault ;
      private bool aP2_isOwner ;
      private string aP3_error ;
   }

}
