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
   public class updaterestorestatusongroup : GXProcedure
   {
      public updaterestorestatusongroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updaterestorestatusongroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           out string aP1_error )
      {
         this.AV12group_sdt = aP0_group_sdt;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV11error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         execute(aP0_group_sdt, out aP1_error);
         return AV11error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 out string aP1_error )
      {
         this.AV12group_sdt = aP0_group_sdt;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV9allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV11error), null);
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV9allContacts.Count )
         {
            AV10contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV9allContacts.Item(AV18GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV10contact.gxTpr_Username), StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
            {
               AV16message_signature.gxTpr_Grouppubkey = AV10contact.gxTpr_Grouppubkey;
               AV16message_signature.gxTpr_Username = AV10contact.gxTpr_Username;
               AV16message_signature.gxTpr_Signature = AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature;
               if (true) break;
            }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) )
         {
            AV11error = "We could not find the owner of the group on the contact list";
         }
         else
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( AV15isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
               AV19GXV2 = 1;
               while ( AV19GXV2 <= AV8all_groups_sdt.Count )
               {
                  AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV19GXV2));
                  if ( ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid ) && ! AV17oneGroup.gxTpr_Amigroupowner )
                  {
                     AV17oneGroup.gxTpr_Restorestopped = AV12group_sdt.gxTpr_Restorestopped;
                     AV17oneGroup.gxTpr_Restorestoppeddatetime = AV12group_sdt.gxTpr_Restorestoppeddatetime;
                     if ( AV12group_sdt.gxTpr_Restorestopped )
                     {
                        AV17oneGroup.gxTpr_Cleartextshare = "";
                        AV17oneGroup.gxTpr_Numofsharesreached = false;
                        AV20GXV3 = 1;
                        while ( AV20GXV3 <= AV17oneGroup.gxTpr_Contact.Count )
                        {
                           AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV20GXV3));
                           AV13groupContact.gxTpr_Cleartextshare = "";
                           AV13groupContact.gxTpr_Numofsharesreached = false;
                           AV20GXV3 = (int)(AV20GXV3+1);
                        }
                     }
                     else
                     {
                        AV17oneGroup.gxTpr_Restoresigneddatetime = (DateTime)(DateTime.MinValue);
                        AV21GXV4 = 1;
                        while ( AV21GXV4 <= AV17oneGroup.gxTpr_Contact.Count )
                        {
                           AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV21GXV4));
                           AV13groupContact.gxTpr_Restoresigneddatetime = (DateTime)(DateTime.MinValue);
                           AV21GXV4 = (int)(AV21GXV4+1);
                        }
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
                  AV19GXV2 = (int)(AV19GXV2+1);
               }
            }
            else
            {
               AV11error = "The signature does NOT match the owner of the group: " + StringUtil.Trim( AV16message_signature.gxTpr_Username);
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
         AV11error = "";
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV9allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private int AV20GXV3 ;
      private int AV21GXV4 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV15isOk ;
      private Guid AV14grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV9allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private string aP1_error ;
   }

}
