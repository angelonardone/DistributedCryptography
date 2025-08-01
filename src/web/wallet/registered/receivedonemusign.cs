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
   public class receivedonemusign : GXProcedure
   {
      public receivedonemusign( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public receivedonemusign( IGxContext context )
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
         AV23muSigSignatures.FromJSonString(AV18sdt_message.gxTpr_Message, null);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV8all_groups_sdt.Count )
         {
            AV19group_sdt_temp = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV24GXV1));
            if ( AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid == AV23muSigSignatures.gxTpr_Referencegroupid )
            {
               AV20group_sdt_found = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV19group_sdt_temp.Clone());
               AV25GXV2 = 1;
               while ( AV25GXV2 <= AV20group_sdt_found.gxTpr_Contact.Count )
               {
                  AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20group_sdt_found.gxTpr_Contact.Item(AV25GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV23muSigSignatures.gxTpr_Senderusername)) == 0 )
                  {
                     AV16message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey);
                     AV16message_signature.gxTpr_Username = StringUtil.Trim( AV13groupContact.gxTpr_Contactusername);
                     AV16message_signature.gxTpr_Signature = StringUtil.Trim( AV23muSigSignatures.gxTpr_Sendersignature);
                     if (true) break;
                  }
                  AV25GXV2 = (int)(AV25GXV2+1);
               }
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         AV21contactNotFound = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) && String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( AV15isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
               AV26GXV3 = 1;
               while ( AV26GXV3 <= AV8all_groups_sdt.Count )
               {
                  AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV26GXV3));
                  if ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV23muSigSignatures.gxTpr_Referencegroupid )
                  {
                     AV27GXV4 = 1;
                     while ( AV27GXV4 <= AV17oneGroup.gxTpr_Contact.Count )
                     {
                        AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV27GXV4));
                        if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV23muSigSignatures.gxTpr_Senderusername)) == 0 )
                        {
                           AV13groupContact.gxTpr_Musigsignatures.Add(AV23muSigSignatures, 0);
                           if (true) break;
                        }
                        AV27GXV4 = (int)(AV27GXV4+1);
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
                  AV26GXV3 = (int)(AV26GXV3+1);
               }
            }
            else
            {
               AV11error = "The signature does NOT match the user: " + AV16message_signature.ToJSonString(false, true) + AV23muSigSignatures.ToJSonString(false, true);
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
         AV23muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV19group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20group_sdt_found = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private int AV26GXV3 ;
      private int AV27GXV4 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV21contactNotFound ;
      private bool AV15isOk ;
      private Guid AV14grpupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV18sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV23muSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt_found ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17oneGroup ;
      private string aP1_error ;
   }

}
