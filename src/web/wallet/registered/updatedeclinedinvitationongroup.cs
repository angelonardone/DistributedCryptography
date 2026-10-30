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
   public class updatedeclinedinvitationongroup : GXProcedure
   {
      public updatedeclinedinvitationongroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updatedeclinedinvitationongroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_memberUserName ,
                           out Guid aP2_groupId ,
                           out string aP3_groupName ,
                           out string aP4_error )
      {
         this.AV21sdt_message = aP0_sdt_message;
         this.AV19memberUserName = "" ;
         this.AV14groupId = Guid.Empty ;
         this.AV15groupName = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_memberUserName=this.AV19memberUserName;
         aP2_groupId=this.AV14groupId;
         aP3_groupName=this.AV15groupName;
         aP4_error=this.AV11error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out string aP1_memberUserName ,
                                out Guid aP2_groupId ,
                                out string aP3_groupName )
      {
         execute(aP0_sdt_message, out aP1_memberUserName, out aP2_groupId, out aP3_groupName, out aP4_error);
         return AV11error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out string aP1_memberUserName ,
                                 out Guid aP2_groupId ,
                                 out string aP3_groupName ,
                                 out string aP4_error )
      {
         this.AV21sdt_message = aP0_sdt_message;
         this.AV19memberUserName = "" ;
         this.AV14groupId = Guid.Empty ;
         this.AV15groupName = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_memberUserName=this.AV19memberUserName;
         aP2_groupId=this.AV14groupId;
         aP3_groupName=this.AV15groupName;
         aP4_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12group_sdt.FromJSonString(AV21sdt_message.gxTpr_Message, null);
         AV19memberUserName = StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname);
         AV9allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV11error), null);
         AV16groupPubKey = "";
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV9allContacts.Count )
         {
            AV10contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV9allContacts.Item(AV22GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV10contact.gxTpr_Username), AV19memberUserName) == 0 )
            {
               AV16groupPubKey = StringUtil.Trim( AV10contact.gxTpr_Grouppubkey);
               if (true) break;
            }
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16groupPubKey)) )
         {
            AV11error = "A decline from somebody who is not a contact";
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV11error;
         new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  AV16groupPubKey,  AV19memberUserName+AV16groupPubKey,  StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature), out  AV18isOk, out  GXt_char1) ;
         AV11error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) || ! AV18isOk )
         {
            AV11error = "The signature of the decline does NOT match the user";
            cleanup();
            if (true) return;
         }
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
         AV23GXV2 = 1;
         while ( AV23GXV2 <= AV8all_groups_sdt.Count )
         {
            AV20oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV23GXV2));
            if ( AV20oneGroup.gxTpr_Amigroupowner && ( AV20oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid ) )
            {
               AV24GXV3 = 1;
               while ( AV24GXV3 <= AV20oneGroup.gxTpr_Contact.Count )
               {
                  AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20oneGroup.gxTpr_Contact.Item(AV24GXV3));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), AV19memberUserName) == 0 )
                  {
                     AV13groupContact.gxTpr_Contactinvitationdelined = true;
                     AV14groupId = AV20oneGroup.gxTpr_Groupid;
                     AV15groupName = StringUtil.Trim( AV20oneGroup.gxTpr_Groupname);
                  }
                  AV24GXV3 = (int)(AV24GXV3+1);
               }
               GXt_char1 = AV11error;
               new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV20oneGroup,  StringUtil.Trim( AV20oneGroup.gxTpr_Othergroup.gxTpr_Encpassword), out  AV17grpupId, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char1 = AV11error;
                  new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                  AV11error = GXt_char1;
               }
               if (true) break;
            }
            AV23GXV2 = (int)(AV23GXV2+1);
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
         AV19memberUserName = "";
         AV14groupId = Guid.Empty;
         AV15groupName = "";
         AV11error = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV9allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV16groupPubKey = "";
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV20oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV17grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV18isOk ;
      private string AV19memberUserName ;
      private string AV15groupName ;
      private string AV16groupPubKey ;
      private Guid AV14groupId ;
      private Guid AV17grpupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV21sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV9allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private string aP1_memberUserName ;
      private Guid aP2_groupId ;
      private string aP3_groupName ;
      private string aP4_error ;
   }

}
