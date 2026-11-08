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
   public class authorizeincomingmessage : GXProcedure
   {
      public authorizeincomingmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public authorizeincomingmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_reason )
      {
         this.AV23sdt_message = aP0_sdt_message;
         this.AV21reason = "" ;
         initialize();
         ExecuteImpl();
         aP1_reason=this.AV21reason;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message )
      {
         execute(aP0_sdt_message, out aP1_reason);
         return AV21reason ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out string aP1_reason )
      {
         this.AV23sdt_message = aP0_sdt_message;
         this.AV21reason = "" ;
         SubmitImpl();
         aP1_reason=this.AV21reason;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV21reason = "";
         AV11from = StringUtil.Trim( AV23sdt_message.gxTpr_From);
         if ( AV23sdt_message.gxTpr_Messagetype == 50 )
         {
            AV22sdt_chat.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV22sdt_chat.gxTpr_From), AV11from) == 0 ) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV22sdt_chat.gxTpr_To), StringUtil.Trim( AV23sdt_message.gxTpr_To)) == 0 ) )
            {
               AV21reason = "a chat whose sender or recipient is not the one of its envelope";
            }
         }
         else if ( ( AV23sdt_message.gxTpr_Messagetype == 30 ) || ( AV23sdt_message.gxTpr_Messagetype == 40 ) )
         {
            AV17message_signature.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV17message_signature.gxTpr_Username), AV11from) == 0 ) )
            {
               AV21reason = "a contact invitation whose user is not the sender";
            }
            else
            {
               GXt_SdtWalletInfo1 = AV24walletInfo;
               new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
               AV24walletInfo = GXt_SdtWalletInfo1;
               GXt_char2 = AV9error;
               new GeneXus.Programs.nbitcoin.istaprootaddressofpubkey(context ).execute(  AV11from,  AV24walletInfo.gxTpr_Networktype,  StringUtil.Trim( AV17message_signature.gxTpr_Pubkey), out  AV16isKey, out  GXt_char2) ;
               AV9error = GXt_char2;
               if ( ! AV16isKey )
               {
                  AV21reason = "a contact invitation whose key is not the key of its sender";
               }
            }
         }
         else if ( ( AV23sdt_message.gxTpr_Messagetype == 70 ) || ( AV23sdt_message.gxTpr_Messagetype == 80 ) || ( AV23sdt_message.gxTpr_Messagetype == 140 ) || ( AV23sdt_message.gxTpr_Messagetype == 90 ) || ( AV23sdt_message.gxTpr_Messagetype == 100 ) )
         {
            AV12group_sdt.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname), AV11from) == 0 ) )
            {
               AV21reason = "a group message whose member is not the sender";
            }
            else
            {
               if ( AV23sdt_message.gxTpr_Messagetype == 80 )
               {
                  /* Execute user subroutine: 'ACCEPTANCE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               else if ( AV23sdt_message.gxTpr_Messagetype == 90 )
               {
                  /* Execute user subroutine: 'FROM THE OWNER' */
                  S121 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               else if ( ( AV23sdt_message.gxTpr_Messagetype == 100 ) && ! (DateTime.MinValue==AV12group_sdt.gxTpr_Restorestoppeddatetime) )
               {
                  /* Execute user subroutine: 'FROM THE OWNER' */
                  S121 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
            }
         }
         else if ( ( AV23sdt_message.gxTpr_Messagetype == 110 ) || ( AV23sdt_message.gxTpr_Messagetype == 120 ) )
         {
            AV19muSigSignatures.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV19muSigSignatures.gxTpr_Senderusername), AV11from) == 0 ) )
            {
               AV21reason = "a signature whose member is not the sender";
            }
         }
         else if ( AV23sdt_message.gxTpr_Messagetype == 130 )
         {
            AV13groupChatMessage.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV13groupChatMessage.gxTpr_Fromusername), AV11from) == 0 ) )
            {
               AV21reason = "a group chat message whose member is not the sender";
            }
         }
         else if ( AV23sdt_message.gxTpr_Messagetype == 150 )
         {
            AV18muSig2RoomMessage.FromJSonString(AV23sdt_message.gxTpr_Message, null);
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV18muSig2RoomMessage.gxTpr_Fromusername), AV11from) == 0 ) )
            {
               AV21reason = "a signing room message whose member is not the sender";
            }
         }
         else
         {
            AV21reason = "a message of an unknown type";
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ACCEPTANCE' Routine */
         returnInSub = false;
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV10found = false;
         AV25GXV1 = 1;
         while ( AV25GXV1 <= AV8all_groups_sdt.Count )
         {
            AV20oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV25GXV1));
            if ( AV20oneGroup.gxTpr_Amigroupowner && ( AV20oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid ) )
            {
               AV10found = true;
               if (true) break;
            }
            AV25GXV1 = (int)(AV25GXV1+1);
         }
         if ( ! AV10found )
         {
            AV21reason = "an acceptance for a group that is not mine";
            returnInSub = true;
            if (true) return;
         }
         if ( AV20oneGroup.gxTpr_Isactive && ! ( AV20oneGroup.gxTpr_Grouptype == 40 ) )
         {
            AV21reason = "an acceptance for a group that is already active";
            returnInSub = true;
            if (true) return;
         }
         AV15invited = false;
         AV26GXV2 = 1;
         while ( AV26GXV2 <= AV20oneGroup.gxTpr_Contact.Count )
         {
            AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20oneGroup.gxTpr_Contact.Item(AV26GXV2));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Contactusername), AV11from) == 0 )
            {
               AV15invited = true;
               if ( AV14groupContact.gxTpr_Contactinvrec )
               {
                  if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV12group_sdt.gxTpr_Extpubkeymultisigreceiving)) == 0 ) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Extpubkeymultisigchange), StringUtil.Trim( AV12group_sdt.gxTpr_Extpubkeymultisigchange)) == 0 ) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Extpubkeytimebountyreceiving), StringUtil.Trim( AV12group_sdt.gxTpr_Extpubkeytimebountyreceiving)) == 0 ) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV14groupContact.gxTpr_Contactgroupencpassword), StringUtil.Trim( AV12group_sdt.gxTpr_Encpassword)) == 0 ) || ! ( AV14groupContact.gxTpr_Contactgroupid == AV12group_sdt.gxTpr_Groupid ) )
                  {
                     AV21reason = "a second acceptance with other keys";
                  }
               }
               if (true) break;
            }
            AV26GXV2 = (int)(AV26GXV2+1);
         }
         if ( ! AV15invited )
         {
            AV21reason = "an acceptance from somebody who was not invited";
         }
      }

      protected void S121( )
      {
         /* 'FROM THE OWNER' Routine */
         returnInSub = false;
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV27GXV3 = 1;
         while ( AV27GXV3 <= AV8all_groups_sdt.Count )
         {
            AV20oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV27GXV3));
            if ( AV20oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
            {
               if ( AV20oneGroup.gxTpr_Amigroupowner )
               {
                  AV21reason = "a message of the owner for a group that is mine";
               }
               else
               {
                  if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV20oneGroup.gxTpr_Othergroup.gxTpr_Referenceusernname), AV11from) == 0 ) )
                  {
                     AV21reason = "a message of the owner that does not come from the owner";
                  }
               }
               if (true) break;
            }
            AV27GXV3 = (int)(AV27GXV3+1);
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
         AV21reason = "";
         AV11from = "";
         AV22sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         AV17message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV24walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV9error = "";
         GXt_char2 = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV13groupChatMessage = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         AV18muSig2RoomMessage = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV20oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV25GXV1 ;
      private int AV26GXV2 ;
      private int AV27GXV3 ;
      private string AV9error ;
      private string GXt_char2 ;
      private bool AV16isKey ;
      private bool returnInSub ;
      private bool AV10found ;
      private bool AV15invited ;
      private string AV21reason ;
      private string AV11from ;
      private GeneXus.Programs.nostr.SdtSDT_message AV23sdt_message ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV22sdt_chat ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV17message_signature ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV24walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV19muSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV13groupChatMessage ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage AV18muSig2RoomMessage ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20oneGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private string aP1_reason ;
   }

}
