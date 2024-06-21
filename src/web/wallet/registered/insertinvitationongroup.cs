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
   public class insertinvitationongroup : GXProcedure
   {
      public insertinvitationongroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public insertinvitationongroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out bool aP1_isGroupDeclined ,
                           out string aP2_error )
      {
         this.AV16sdt_message = aP0_sdt_message;
         this.AV13isGroupDeclined = false ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isGroupDeclined=this.AV13isGroupDeclined;
         aP2_error=this.AV12error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out bool aP1_isGroupDeclined )
      {
         execute(aP0_sdt_message, out aP1_isGroupDeclined, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out bool aP1_isGroupDeclined ,
                                 out string aP2_error )
      {
         this.AV16sdt_message = aP0_sdt_message;
         this.AV13isGroupDeclined = false ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_isGroupDeclined=this.AV13isGroupDeclined;
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV19group_sdt.FromJSonString(AV16sdt_message.gxTpr_Message, null);
         AV8allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV12error), null);
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV8allContacts.Count )
         {
            AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV8allContacts.Item(AV22GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV9contact.gxTpr_Username), StringUtil.Trim( AV19group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
            {
               AV15message_signature.gxTpr_Grouppubkey = AV9contact.gxTpr_Grouppubkey;
               AV15message_signature.gxTpr_Username = AV9contact.gxTpr_Username;
               AV15message_signature.gxTpr_Signature = AV19group_sdt.gxTpr_Othergroup.gxTpr_Signature;
               if (true) break;
            }
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15message_signature.gxTpr_Username)) )
         {
            GXt_char1 = AV12error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV15message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV15message_signature.gxTpr_Username)+StringUtil.Trim( AV15message_signature.gxTpr_Grouppubkey),  AV15message_signature.gxTpr_Signature, out  AV14isOk, out  GXt_char1) ;
            AV12error = GXt_char1;
            if ( AV14isOk )
            {
               AV20all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV12error), null);
               AV23GXV2 = 1;
               while ( AV23GXV2 <= AV20all_groups_sdt.Count )
               {
                  AV21oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV20all_groups_sdt.Item(AV23GXV2));
                  if ( AV21oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV19group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                  {
                     AV10groupFound = true;
                     AV13isGroupDeclined = true;
                     if (true) break;
                  }
                  AV23GXV2 = (int)(AV23GXV2+1);
               }
               if ( ! AV10groupFound && String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV21oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
                  AV21oneGroup.gxTpr_Groupname = AV19group_sdt.gxTpr_Groupname;
                  AV21oneGroup.gxTpr_Grouptype = AV19group_sdt.gxTpr_Grouptype;
                  AV21oneGroup.gxTpr_Othergroup.gxTpr_Referenceusernname = AV19group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname;
                  AV21oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid = AV19group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
                  AV21oneGroup.gxTpr_Othergroup.gxTpr_Encpassword = AV19group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
                  AV20all_groups_sdt.Add(AV21oneGroup, 0);
                  GXt_char1 = AV12error;
                  new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV20all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                  AV12error = GXt_char1;
               }
            }
            else
            {
               AV12error = "The signature does NOT match the user: " + AV15message_signature.ToJSonString(false, true);
            }
         }
         else
         {
            AV12error = "We could not find the ContactId on the Group Invitation";
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
         AV12error = "";
         AV19group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV15message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV20all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV21oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private string AV12error ;
      private string GXt_char1 ;
      private bool AV13isGroupDeclined ;
      private bool AV14isOk ;
      private bool AV10groupFound ;
      private bool aP1_isGroupDeclined ;
      private string aP2_error ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV20all_groups_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV8allContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV21oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV15message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV16sdt_message ;
   }

}
