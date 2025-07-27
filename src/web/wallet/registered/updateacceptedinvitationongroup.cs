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
   public class updateacceptedinvitationongroup : GXProcedure
   {
      public updateacceptedinvitationongroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updateacceptedinvitationongroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_error )
      {
         this.AV18sdt_message = aP0_sdt_message;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV11error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message )
      {
         execute(aP0_sdt_message, out aP1_error);
         return AV11error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out string aP1_error )
      {
         this.AV18sdt_message = aP0_sdt_message;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12group_sdt.FromJSonString(AV18sdt_message.gxTpr_Message, null);
         AV9allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV11error), null);
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV9allContacts.Count )
         {
            AV10contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV9allContacts.Item(AV21GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV10contact.gxTpr_Username), StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
            {
               AV16message_signature.gxTpr_Grouppubkey = AV10contact.gxTpr_Grouppubkey;
               AV16message_signature.gxTpr_Username = AV10contact.gxTpr_Username;
               AV16message_signature.gxTpr_Signature = AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature;
               if (true) break;
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) )
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( AV15isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
               AV22GXV2 = 1;
               while ( AV22GXV2 <= AV8all_groups_sdt.Count )
               {
                  AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV22GXV2));
                  if ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                  {
                     AV23GXV3 = 1;
                     while ( AV23GXV3 <= AV17oneGroup.gxTpr_Contact.Count )
                     {
                        AV19groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV23GXV3));
                        if ( StringUtil.StrCmp(AV19groupContact.gxTpr_Contactusername, AV16message_signature.gxTpr_Username) == 0 )
                        {
                           GXt_dtime2 = (DateTime)(DateTime.MinValue);
                           new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV18sdt_message.gxTpr_Datetimeunix, out  GXt_dtime2) ;
                           AV19groupContact.gxTpr_Contactinvitacionaccepted = GXt_dtime2;
                           AV19groupContact.gxTpr_Contactinvrec = true;
                           AV19groupContact.gxTpr_Contactgroupid = AV12group_sdt.gxTpr_Groupid;
                           AV19groupContact.gxTpr_Contactgroupencpassword = AV12group_sdt.gxTpr_Encpassword;
                           AV19groupContact.gxTpr_Extpubkeymultisigreceiving = AV12group_sdt.gxTpr_Extpubkeymultisigreceiving;
                           AV19groupContact.gxTpr_Extpubkeymultisigchange = AV12group_sdt.gxTpr_Extpubkeymultisigchange;
                           AV19groupContact.gxTpr_Extpubkeytimebountyreceiving = AV12group_sdt.gxTpr_Extpubkeytimebountyreceiving;
                        }
                        AV23GXV3 = (int)(AV23GXV3+1);
                     }
                     GXt_char1 = AV11error;
                     new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV17oneGroup,  StringUtil.Trim( AV17oneGroup.gxTpr_Othergroup.gxTpr_Encpassword), out  AV20grpupId, out  GXt_char1) ;
                     AV11error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        GXt_char1 = AV11error;
                        new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                        AV11error = GXt_char1;
                     }
                     if (true) break;
                  }
                  AV22GXV2 = (int)(AV22GXV2+1);
               }
            }
            else
            {
               AV11error = "The signature does NOT match the user: " + AV16message_signature.ToJSonString(false, true);
            }
         }
         else
         {
            AV11error = "We could not find the ContactId on the Group Invitation";
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
         AV11error = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV9allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         AV20grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private int AV23GXV3 ;
      private string AV11error ;
      private string GXt_char1 ;
      private DateTime GXt_dtime2 ;
      private bool AV15isOk ;
      private Guid AV20grpupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV18sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV9allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV19groupContact ;
      private string aP1_error ;
   }

}
