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
   public class updaterestoreongroup : GXProcedure
   {
      public updaterestoreongroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updaterestoreongroup( IGxContext context )
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
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV8all_groups_sdt.Count )
         {
            AV19group_sdt_temp = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV23GXV1));
            if ( AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
            {
               GXt_char1 = AV11error;
               new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword, out  AV20group_sdt_found, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV24GXV2 = 1;
                  while ( AV24GXV2 <= AV20group_sdt_found.gxTpr_Contact.Count )
                  {
                     AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20group_sdt_found.gxTpr_Contact.Item(AV24GXV2));
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
                     {
                        AV16message_signature.gxTpr_Grouppubkey = AV13groupContact.gxTpr_Contactuserpubkey;
                        AV16message_signature.gxTpr_Username = AV13groupContact.gxTpr_Contactusername;
                        AV16message_signature.gxTpr_Signature = AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature;
                        if (true) break;
                     }
                     AV24GXV2 = (int)(AV24GXV2+1);
                  }
               }
            }
            AV23GXV1 = (int)(AV23GXV1+1);
         }
         AV21contactNotFound = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( AV15isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
               AV25GXV3 = 1;
               while ( AV25GXV3 <= AV8all_groups_sdt.Count )
               {
                  AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV25GXV3));
                  if ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                  {
                     if ( ! AV17oneGroup.gxTpr_Numofsharesreached )
                     {
                        AV26GXV4 = 1;
                        while ( AV26GXV4 <= AV17oneGroup.gxTpr_Contact.Count )
                        {
                           AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV26GXV4));
                           if ( StringUtil.StrCmp(AV13groupContact.gxTpr_Contactusername, AV16message_signature.gxTpr_Username) == 0 )
                           {
                              AV13groupContact.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                              AV13groupContact.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                              if ( AV12group_sdt.gxTpr_Numofsharesreached )
                              {
                                 AV17oneGroup.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                                 AV17oneGroup.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                              }
                              AV21contactNotFound = false;
                           }
                           AV26GXV4 = (int)(AV26GXV4+1);
                        }
                        if ( AV21contactNotFound )
                        {
                           AV22oneContact.gxTpr_Contactusername = AV16message_signature.gxTpr_Username;
                           AV22oneContact.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                           AV22oneContact.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                           AV17oneGroup.gxTpr_Contact.Add(AV22oneContact, 0);
                           if ( AV12group_sdt.gxTpr_Numofsharesreached )
                           {
                              AV17oneGroup.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                              AV17oneGroup.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
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
                     }
                     if (true) break;
                  }
                  AV25GXV3 = (int)(AV25GXV3+1);
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
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV19group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20group_sdt_found = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV22oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14grpupId = Guid.Empty;
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int AV25GXV3 ;
      private int AV26GXV4 ;
      private string AV11error ;
      private string GXt_char1 ;
      private bool AV21contactNotFound ;
      private bool AV15isOk ;
      private Guid AV14grpupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV18sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV16message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt_found ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV22oneContact ;
      private string aP1_error ;
   }

}
