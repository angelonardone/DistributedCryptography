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
                           out bool aP1_isOwnerAlert ,
                           out string aP2_error )
      {
         this.AV18sdt_message = aP0_sdt_message;
         this.AV23isOwnerAlert = false ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isOwnerAlert=this.AV23isOwnerAlert;
         aP2_error=this.AV11error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out bool aP1_isOwnerAlert )
      {
         execute(aP0_sdt_message, out aP1_isOwnerAlert, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out bool aP1_isOwnerAlert ,
                                 out string aP2_error )
      {
         this.AV18sdt_message = aP0_sdt_message;
         this.AV23isOwnerAlert = false ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_isOwnerAlert=this.AV23isOwnerAlert;
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12group_sdt.FromJSonString(AV18sdt_message.gxTpr_Message, null);
         AV16message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV23isOwnerAlert = false;
         if ( ! (DateTime.MinValue==AV12group_sdt.gxTpr_Restorestoppeddatetime) )
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.wallet.registered.updaterestorestatusongroup(context ).execute(  AV12group_sdt, out  GXt_char1) ;
            AV11error = GXt_char1;
         }
         else
         {
            AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
            AV25GXV1 = 1;
            while ( AV25GXV1 <= AV8all_groups_sdt.Count )
            {
               AV19group_sdt_temp = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV25GXV1));
               if ( AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
               {
                  GXt_char1 = AV11error;
                  new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword, out  AV20group_sdt_found, out  GXt_char1) ;
                  AV11error = GXt_char1;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV26GXV2 = 1;
                     while ( AV26GXV2 <= AV20group_sdt_found.gxTpr_Contact.Count )
                     {
                        AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20group_sdt_found.gxTpr_Contact.Item(AV26GXV2));
                        if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
                        {
                           AV16message_signature.gxTpr_Grouppubkey = AV13groupContact.gxTpr_Contactuserpubkey;
                           AV16message_signature.gxTpr_Username = AV13groupContact.gxTpr_Contactusername;
                           AV16message_signature.gxTpr_Signature = AV12group_sdt.gxTpr_Othergroup.gxTpr_Signature;
                           if (true) break;
                        }
                        AV26GXV2 = (int)(AV26GXV2+1);
                     }
                  }
               }
               AV25GXV1 = (int)(AV25GXV1+1);
            }
            AV21contactNotFound = true;
            AV24restoreSignedDateTime = AV12group_sdt.gxTpr_Restoresigneddatetime;
            if ( (DateTime.MinValue==AV24restoreSignedDateTime) )
            {
               AV24restoreSignedDateTime = DateTimeUtil.Now( context);
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16message_signature.gxTpr_Username)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               GXt_char1 = AV11error;
               new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV16message_signature.gxTpr_Username)+StringUtil.Trim( AV16message_signature.gxTpr_Grouppubkey),  AV16message_signature.gxTpr_Signature, out  AV15isOk, out  GXt_char1) ;
               AV11error = GXt_char1;
               if ( AV15isOk )
               {
                  AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
                  AV27GXV3 = 1;
                  while ( AV27GXV3 <= AV8all_groups_sdt.Count )
                  {
                     AV17oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV27GXV3));
                     if ( AV17oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                     {
                        if ( AV17oneGroup.gxTpr_Amigroupowner )
                        {
                           AV28GXV4 = 1;
                           while ( AV28GXV4 <= AV17oneGroup.gxTpr_Contact.Count )
                           {
                              AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV28GXV4));
                              if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV16message_signature.gxTpr_Username)) == 0 )
                              {
                                 AV13groupContact.gxTpr_Restoresigneddatetime = AV24restoreSignedDateTime;
                                 AV13groupContact.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                                 AV13groupContact.gxTpr_Cleartextshare = "";
                              }
                              AV28GXV4 = (int)(AV28GXV4+1);
                           }
                           if ( ! AV17oneGroup.gxTpr_Restorestopped )
                           {
                              AV23isOwnerAlert = true;
                           }
                           GXt_char1 = AV11error;
                           new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV17oneGroup,  StringUtil.Trim( AV17oneGroup.gxTpr_Othergroup.gxTpr_Encpassword), out  AV14grpupId, out  GXt_char1) ;
                           AV11error = GXt_char1;
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                           {
                              GXt_char1 = AV11error;
                              new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                              AV11error = GXt_char1;
                           }
                        }
                        else
                        {
                           if ( ! AV17oneGroup.gxTpr_Numofsharesreached )
                           {
                              AV29GXV5 = 1;
                              while ( AV29GXV5 <= AV17oneGroup.gxTpr_Contact.Count )
                              {
                                 AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17oneGroup.gxTpr_Contact.Item(AV29GXV5));
                                 if ( StringUtil.StrCmp(AV13groupContact.gxTpr_Contactusername, AV16message_signature.gxTpr_Username) == 0 )
                                 {
                                    AV13groupContact.gxTpr_Restoresigneddatetime = AV24restoreSignedDateTime;
                                    if ( ! AV17oneGroup.gxTpr_Restorestopped )
                                    {
                                       AV13groupContact.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                                       AV13groupContact.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                                       if ( AV12group_sdt.gxTpr_Numofsharesreached )
                                       {
                                          AV17oneGroup.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                                          AV17oneGroup.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                                       }
                                    }
                                    AV21contactNotFound = false;
                                 }
                                 AV29GXV5 = (int)(AV29GXV5+1);
                              }
                              if ( AV21contactNotFound )
                              {
                                 AV22oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
                                 AV22oneContact.gxTpr_Contactusername = AV16message_signature.gxTpr_Username;
                                 AV22oneContact.gxTpr_Restoresigneddatetime = AV24restoreSignedDateTime;
                                 if ( ! AV17oneGroup.gxTpr_Restorestopped )
                                 {
                                    AV22oneContact.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                                    AV22oneContact.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                                    if ( AV12group_sdt.gxTpr_Numofsharesreached )
                                    {
                                       AV17oneGroup.gxTpr_Cleartextshare = AV12group_sdt.gxTpr_Cleartextshare;
                                       AV17oneGroup.gxTpr_Numofsharesreached = AV12group_sdt.gxTpr_Numofsharesreached;
                                    }
                                 }
                                 AV17oneGroup.gxTpr_Contact.Add(AV22oneContact, 0);
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
                        }
                        if (true) break;
                     }
                     AV27GXV3 = (int)(AV27GXV3+1);
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
         AV24restoreSignedDateTime = (DateTime)(DateTime.MinValue);
         AV17oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14grpupId = Guid.Empty;
         AV22oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV25GXV1 ;
      private int AV26GXV2 ;
      private int AV27GXV3 ;
      private int AV28GXV4 ;
      private int AV29GXV5 ;
      private string AV11error ;
      private string GXt_char1 ;
      private DateTime AV24restoreSignedDateTime ;
      private bool AV23isOwnerAlert ;
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
      private bool aP1_isOwnerAlert ;
      private string aP2_error ;
   }

}
