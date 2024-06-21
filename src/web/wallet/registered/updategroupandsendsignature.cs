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
   public class updategroupandsendsignature : GXProcedure
   {
      public updategroupandsendsignature( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updategroupandsendsignature( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           bool aP1_sendAllCoins ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                           decimal aP3_sendCoins ,
                           string aP4_sendTo ,
                           string aP5_changeTo ,
                           Guid aP6_signatureID ,
                           bool aP7_muSigCompleated ,
                           out string aP8_error )
      {
         this.AV17group_sdt = aP0_group_sdt;
         this.AV22sendAllCoins = aP1_sendAllCoins;
         this.AV32transactionsToSend = aP2_transactionsToSend;
         this.AV23sendCoins = aP3_sendCoins;
         this.AV24sendTo = aP4_sendTo;
         this.AV10changeTo = aP5_changeTo;
         this.AV71signatureID = aP6_signatureID;
         this.AV68muSigCompleated = aP7_muSigCompleated;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP8_error=this.AV14error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                bool aP1_sendAllCoins ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                decimal aP3_sendCoins ,
                                string aP4_sendTo ,
                                string aP5_changeTo ,
                                Guid aP6_signatureID ,
                                bool aP7_muSigCompleated )
      {
         execute(aP0_group_sdt, aP1_sendAllCoins, aP2_transactionsToSend, aP3_sendCoins, aP4_sendTo, aP5_changeTo, aP6_signatureID, aP7_muSigCompleated, out aP8_error);
         return AV14error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 bool aP1_sendAllCoins ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                 decimal aP3_sendCoins ,
                                 string aP4_sendTo ,
                                 string aP5_changeTo ,
                                 Guid aP6_signatureID ,
                                 bool aP7_muSigCompleated ,
                                 out string aP8_error )
      {
         this.AV17group_sdt = aP0_group_sdt;
         this.AV22sendAllCoins = aP1_sendAllCoins;
         this.AV32transactionsToSend = aP2_transactionsToSend;
         this.AV23sendCoins = aP3_sendCoins;
         this.AV24sendTo = aP4_sendTo;
         this.AV10changeTo = aP5_changeTo;
         this.AV71signatureID = aP6_signatureID;
         this.AV68muSigCompleated = aP7_muSigCompleated;
         this.AV14error = "" ;
         SubmitImpl();
         aP8_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV76GXV1 = 1;
         while ( AV76GXV1 <= AV32transactionsToSend.Count )
         {
            AV66oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV32transactionsToSend.Item(AV76GXV1));
            AV11description = StringUtil.Trim( AV66oneTransaction.gxTpr_Description);
            AV67signedDateTime = AV66oneTransaction.gxTpr_Sentdatetime;
            AV76GXV1 = (int)(AV76GXV1+1);
         }
         AV77GXV2 = 1;
         while ( AV77GXV2 <= AV17group_sdt.gxTpr_Contact.Count )
         {
            AV55oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV77GXV2));
            if ( AV55oneGroupContact.gxTpr_Contactgroupid == AV17group_sdt.gxTpr_Groupid )
            {
               if ( (Guid.Empty==AV71signatureID) )
               {
                  AV65muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
                  AV65muSigSignatures.gxTpr_Id = Guid.NewGuid( );
               }
               else
               {
                  AV65muSigSignatures.gxTpr_Id = AV71signatureID;
               }
               AV65muSigSignatures.gxTpr_Description = StringUtil.Trim( AV11description);
               AV65muSigSignatures.gxTpr_Signeddatetime = AV67signedDateTime;
               AV65muSigSignatures.gxTpr_Compleated = AV68muSigCompleated;
               AV65muSigSignatures.gxTpr_Transactions = (GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>)(AV32transactionsToSend.Clone());
               AV65muSigSignatures.gxTpr_Sendallcoins = AV22sendAllCoins;
               AV65muSigSignatures.gxTpr_Sendcoins = AV23sendCoins;
               AV65muSigSignatures.gxTpr_Sendto = StringUtil.Trim( AV24sendTo);
               AV65muSigSignatures.gxTpr_Changeto = StringUtil.Trim( AV10changeTo);
               AV65muSigSignatures.gxTpr_Referencegroupid = AV17group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
               AV65muSigSignatures.gxTpr_Senderusername = StringUtil.Trim( AV55oneGroupContact.gxTpr_Contactusername);
               AV55oneGroupContact.gxTpr_Musigsignatures.Add(AV65muSigSignatures, 0);
               if (true) break;
            }
            AV77GXV2 = (int)(AV77GXV2+1);
         }
         GXt_SdtExternalUser1 = AV70externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV70externalUser = GXt_SdtExternalUser1;
         AV54message_signature.gxTpr_Username = StringUtil.Trim( AV70externalUser.gxTpr_Userinfo.gxTpr_Username);
         AV54message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV70externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char2 = AV14error;
         GXt_char3 = AV54message_signature.gxTpr_Signature;
         new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV70externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV54message_signature.gxTpr_Username)+StringUtil.Trim( AV54message_signature.gxTpr_Grouppubkey), out  GXt_char3, out  GXt_char2) ;
         AV54message_signature.gxTpr_Signature = GXt_char3;
         AV14error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV65muSigSignatures.gxTpr_Sendersignature = StringUtil.Trim( AV54message_signature.gxTpr_Signature);
            AV78GXV3 = 1;
            while ( AV78GXV3 <= AV17group_sdt.gxTpr_Contact.Count )
            {
               AV48groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV78GXV3));
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV48groupContact.gxTpr_Contactusername), StringUtil.Trim( AV65muSigSignatures.gxTpr_Senderusername)) == 0 ) )
               {
                  AV59sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int4 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int4) ;
                  AV59sdt_message.gxTpr_Datetimeunix = GXt_int4;
                  if ( AV68muSigCompleated )
                  {
                     AV59sdt_message.gxTpr_Messagetype = 120;
                  }
                  else
                  {
                     AV59sdt_message.gxTpr_Messagetype = 110;
                  }
                  AV59sdt_message.gxTpr_Message = AV65muSigSignatures.ToJSonString(false, true);
                  AV40contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV40contact.gxTpr_Username = StringUtil.Trim( AV48groupContact.gxTpr_Contactusername);
                  AV40contact.gxTpr_Messagepubkey = StringUtil.Trim( AV48groupContact.gxTpr_Contactuserpubkey);
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV40contact,  AV59sdt_message, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     AV14error = "There was a problem sending the Signature to the Group: " + AV14error;
                     if (true) break;
                  }
               }
               AV78GXV3 = (int)(AV78GXV3+1);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            GXt_char3 = AV14error;
            GXt_guid5 = AV17group_sdt.gxTpr_Groupid;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV17group_sdt,  StringUtil.Trim( AV17group_sdt.gxTpr_Encpassword), out  GXt_guid5, out  GXt_char3) ;
            AV17group_sdt.gxTpr_Groupid = GXt_guid5;
            AV14error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
            {
               AV38all_groups_sdt.Clear();
               AV38all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV14error), null);
               AV79GXV4 = 1;
               while ( AV79GXV4 <= AV38all_groups_sdt.Count )
               {
                  AV46group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV38all_groups_sdt.Item(AV79GXV4));
                  if ( AV46group_sdt_delete.gxTpr_Groupid == AV17group_sdt.gxTpr_Groupid )
                  {
                     AV38all_groups_sdt.RemoveItem(AV38all_groups_sdt.IndexOf(AV46group_sdt_delete));
                     if (true) break;
                  }
                  AV79GXV4 = (int)(AV79GXV4+1);
               }
               AV38all_groups_sdt.Add(AV17group_sdt, 0);
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV38all_groups_sdt.ToJSonString(false), out  AV14error) ;
            }
            else
            {
               AV14error = "There was a problem updating the server Group with your signature: " + StringUtil.Trim( AV14error);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            if ( ! AV68muSigCompleated )
            {
               AV30transactionFileName = StringUtil.Trim( AV17group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
               AV31TransactionId = "MULTISIGNATURE IN PROGRESS";
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV30transactionFileName,  AV31TransactionId, ref  AV32transactionsToSend, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  AV14error = "There was a problem updating you local transaction file with the signatures: " + StringUtil.Trim( AV14error);
               }
            }
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
         AV14error = "";
         AV66oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV11description = "";
         AV67signedDateTime = (DateTime)(DateTime.MinValue);
         AV55oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV65muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV70externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV54message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char2 = "";
         AV48groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV59sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV40contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_guid5 = Guid.Empty;
         AV38all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV46group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV30transactionFileName = "";
         AV31TransactionId = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV76GXV1 ;
      private int AV77GXV2 ;
      private int AV78GXV3 ;
      private int AV79GXV4 ;
      private long GXt_int4 ;
      private decimal AV23sendCoins ;
      private string AV24sendTo ;
      private string AV10changeTo ;
      private string AV14error ;
      private string GXt_char2 ;
      private string AV30transactionFileName ;
      private string AV31TransactionId ;
      private string GXt_char3 ;
      private DateTime AV67signedDateTime ;
      private bool AV22sendAllCoins ;
      private bool AV68muSigCompleated ;
      private string AV11description ;
      private Guid AV71signatureID ;
      private Guid GXt_guid5 ;
      private string aP8_error ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV32transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV38all_groups_sdt ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV66oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV46group_sdt_delete ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV55oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV48groupContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV65muSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV40contact ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV70externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV54message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV59sdt_message ;
   }

}
