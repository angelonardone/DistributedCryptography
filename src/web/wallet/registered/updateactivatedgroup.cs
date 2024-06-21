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
   public class updateactivatedgroup : GXProcedure
   {
      public updateactivatedgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updateactivatedgroup( IGxContext context )
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
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV9allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV11error), null);
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV9allContacts.Count )
         {
            AV10contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV9allContacts.Item(AV24GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV10contact.gxTpr_Username), StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
            {
               AV16message_signature.gxTpr_Grouppubkey = AV10contact.gxTpr_Grouppubkey;
               AV16message_signature.gxTpr_Username = AV10contact.gxTpr_Username;
               AV16message_signature.gxTpr_Signature = AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature;
               if (true) break;
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( AV15isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
               AV25GXV2 = 1;
               while ( AV25GXV2 <= AV8all_groups_sdt.Count )
               {
                  AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV25GXV2));
                  if ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                  {
                     AV17oneGroup.gxTpr_Isactive = true;
                     AV17oneGroup.gxTpr_Minimumshares = AV12group_sdt.gxTpr_Minimumshares;
                     if ( AV17oneGroup.gxTpr_Grouptype == 200 )
                     {
                        AV26GXV3 = 1;
                        while ( AV26GXV3 <= AV12group_sdt.gxTpr_Contact.Count )
                        {
                           AV23oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_sdt.gxTpr_Contact.Item(AV26GXV3));
                           AV17oneGroup.gxTpr_Contact.Add(AV23oneGroupContact, 0);
                           AV26GXV3 = (int)(AV26GXV3+1);
                        }
                        AV17oneGroup.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving = AV12group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
                        AV17oneGroup.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange = AV12group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
                     }
                     GXt_char1 = AV11error;
                     new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV17oneGroup,  StringUtil.Trim( AV17oneGroup.gxTpr_Encpassword), out  AV14grpupId, out  GXt_char1) ;
                     AV11error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        GXt_char1 = AV11error;
                        new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                        AV11error = GXt_char1;
                     }
                     if (true) break;
                  }
                  AV25GXV2 = (int)(AV25GXV2+1);
               }
            }
            else
            {
               AV11error = "The signature does NOT match the user: " + AV16message_signature.ToJSonString(false, true) + AV12group_sdt.ToJSonString(false, true);
            }
         }
         else
         {
            AV11error = "We could not find the ContactId on the Group Invitation";
         }
         this.cleanup();
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
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV9allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV23oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private int AV26GXV3 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV15isOk ;
      private Guid AV14grpupId ;
      private string aP1_error ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV9allContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV23oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV18sdt_message ;
   }

}
