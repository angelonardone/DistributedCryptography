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
   public class updategroupandsendlegacypsbt : GXProcedure
   {
      public updategroupandsendlegacypsbt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updategroupandsendlegacypsbt( IGxContext context )
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
                           decimal aP6_transactionFee ,
                           Guid aP7_signatureID ,
                           bool aP8_muSigCompleated ,
                           string aP9_psbt ,
                           out string aP10_error )
      {
         this.AV14group_sdt = aP0_group_sdt;
         this.AV24sendAllCoins = aP1_sendAllCoins;
         this.AV32transactionsToSend = aP2_transactionsToSend;
         this.AV25sendCoins = aP3_sendCoins;
         this.AV26sendTo = aP4_sendTo;
         this.AV9changeTo = aP5_changeTo;
         this.AV29transactionFee = aP6_transactionFee;
         this.AV27signatureID = aP7_signatureID;
         this.AV18muSigCompleated = aP8_muSigCompleated;
         this.AV22psbt = aP9_psbt;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP10_error=this.AV12error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                bool aP1_sendAllCoins ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                decimal aP3_sendCoins ,
                                string aP4_sendTo ,
                                string aP5_changeTo ,
                                decimal aP6_transactionFee ,
                                Guid aP7_signatureID ,
                                bool aP8_muSigCompleated ,
                                string aP9_psbt )
      {
         execute(aP0_group_sdt, aP1_sendAllCoins, aP2_transactionsToSend, aP3_sendCoins, aP4_sendTo, aP5_changeTo, aP6_transactionFee, aP7_signatureID, aP8_muSigCompleated, aP9_psbt, out aP10_error);
         return AV12error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 bool aP1_sendAllCoins ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP2_transactionsToSend ,
                                 decimal aP3_sendCoins ,
                                 string aP4_sendTo ,
                                 string aP5_changeTo ,
                                 decimal aP6_transactionFee ,
                                 Guid aP7_signatureID ,
                                 bool aP8_muSigCompleated ,
                                 string aP9_psbt ,
                                 out string aP10_error )
      {
         this.AV14group_sdt = aP0_group_sdt;
         this.AV24sendAllCoins = aP1_sendAllCoins;
         this.AV32transactionsToSend = aP2_transactionsToSend;
         this.AV25sendCoins = aP3_sendCoins;
         this.AV26sendTo = aP4_sendTo;
         this.AV9changeTo = aP5_changeTo;
         this.AV29transactionFee = aP6_transactionFee;
         this.AV27signatureID = aP7_signatureID;
         this.AV18muSigCompleated = aP8_muSigCompleated;
         this.AV22psbt = aP9_psbt;
         this.AV12error = "" ;
         SubmitImpl();
         aP10_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV33GXV1 = 1;
         while ( AV33GXV1 <= AV32transactionsToSend.Count )
         {
            AV21oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV32transactionsToSend.Item(AV33GXV1));
            AV11description = StringUtil.Trim( AV21oneTransaction.gxTpr_Description);
            AV33GXV1 = (int)(AV33GXV1+1);
         }
         AV28signedDateTime = DateTimeUtil.Now( context);
         AV34GXV2 = 1;
         while ( AV34GXV2 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV20oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV34GXV2));
            if ( AV20oneGroupContact.gxTpr_Contactgroupid == AV14group_sdt.gxTpr_Groupid )
            {
               if ( (Guid.Empty==AV27signatureID) )
               {
                  AV19muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
                  AV19muSigSignatures.gxTpr_Id = Guid.NewGuid( );
               }
               else
               {
                  AV19muSigSignatures.gxTpr_Id = AV27signatureID;
               }
               AV19muSigSignatures.gxTpr_Description = StringUtil.Trim( AV11description);
               AV19muSigSignatures.gxTpr_Signeddatetime = AV28signedDateTime;
               AV19muSigSignatures.gxTpr_Compleated = AV18muSigCompleated;
               AV19muSigSignatures.gxTpr_Transactions = (GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>)(AV32transactionsToSend.Clone());
               AV19muSigSignatures.gxTpr_Sendallcoins = AV24sendAllCoins;
               AV19muSigSignatures.gxTpr_Sendcoins = AV25sendCoins;
               AV19muSigSignatures.gxTpr_Sendto = StringUtil.Trim( AV26sendTo);
               AV19muSigSignatures.gxTpr_Changeto = StringUtil.Trim( AV9changeTo);
               AV19muSigSignatures.gxTpr_Transactionfee = AV29transactionFee;
               AV19muSigSignatures.gxTpr_Referencegroupid = AV14group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
               AV19muSigSignatures.gxTpr_Senderusername = StringUtil.Trim( AV20oneGroupContact.gxTpr_Contactusername);
               AV19muSigSignatures.gxTpr_Psbt = StringUtil.Trim( AV22psbt);
               AV20oneGroupContact.gxTpr_Musigsignatures.Add(AV19muSigSignatures, 0);
               if (true) break;
            }
            AV34GXV2 = (int)(AV34GXV2+1);
         }
         GXt_SdtExternalUser1 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV13externalUser = GXt_SdtExternalUser1;
         AV17message_signature.gxTpr_Username = StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username);
         AV17message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV13externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char2 = AV12error;
         GXt_char3 = AV17message_signature.gxTpr_Signature;
         new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV13externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV17message_signature.gxTpr_Username)+StringUtil.Trim( AV17message_signature.gxTpr_Grouppubkey), out  GXt_char3, out  GXt_char2) ;
         AV17message_signature.gxTpr_Signature = GXt_char3;
         AV12error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV19muSigSignatures.gxTpr_Sendersignature = StringUtil.Trim( AV17message_signature.gxTpr_Signature);
            AV35GXV3 = 1;
            while ( AV35GXV3 <= AV14group_sdt.gxTpr_Contact.Count )
            {
               AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV35GXV3));
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV16groupContact.gxTpr_Contactusername), StringUtil.Trim( AV19muSigSignatures.gxTpr_Senderusername)) == 0 ) )
               {
                  AV23sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int4 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int4) ;
                  AV23sdt_message.gxTpr_Datetimeunix = GXt_int4;
                  if ( AV18muSigCompleated )
                  {
                     AV23sdt_message.gxTpr_Messagetype = 120;
                  }
                  else
                  {
                     AV23sdt_message.gxTpr_Messagetype = 110;
                  }
                  AV23sdt_message.gxTpr_Message = AV19muSigSignatures.ToJSonString(false, true);
                  AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV10contact.gxTpr_Username = StringUtil.Trim( AV16groupContact.gxTpr_Contactusername);
                  AV10contact.gxTpr_Messagepubkey = StringUtil.Trim( AV16groupContact.gxTpr_Contactuserpubkey);
                  GXt_char3 = AV12error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV10contact,  AV23sdt_message, out  GXt_char3) ;
                  AV12error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     AV12error = "There was a problem sending the PSBT to the Group: " + AV12error;
                     if (true) break;
                  }
               }
               AV35GXV3 = (int)(AV35GXV3+1);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            GXt_char3 = AV12error;
            GXt_guid5 = AV14group_sdt.gxTpr_Groupid;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  StringUtil.Trim( AV14group_sdt.gxTpr_Encpassword), out  GXt_guid5, out  GXt_char3) ;
            AV14group_sdt.gxTpr_Groupid = GXt_guid5;
            AV12error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               AV8all_groups_sdt.Clear();
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV12error), null);
               AV36GXV4 = 1;
               while ( AV36GXV4 <= AV8all_groups_sdt.Count )
               {
                  AV15group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV36GXV4));
                  if ( AV15group_sdt_delete.gxTpr_Groupid == AV14group_sdt.gxTpr_Groupid )
                  {
                     AV8all_groups_sdt.RemoveItem(AV8all_groups_sdt.IndexOf(AV15group_sdt_delete));
                     if (true) break;
                  }
                  AV36GXV4 = (int)(AV36GXV4+1);
               }
               AV8all_groups_sdt.Add(AV14group_sdt, 0);
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  AV12error) ;
            }
            else
            {
               AV12error = "There was a problem updating the server Group with your PSBT: " + StringUtil.Trim( AV12error);
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            if ( ! AV18muSigCompleated )
            {
               AV30transactionFileName = StringUtil.Trim( AV14group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
               AV31TransactionId = "MULTISIGNATURE IN PROGRESS";
               GXt_char3 = AV12error;
               new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV30transactionFileName,  AV31TransactionId,  AV32transactionsToSend, out  GXt_char3) ;
               AV12error = GXt_char3;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV12error = "There was a problem updating your local transaction file: " + StringUtil.Trim( AV12error);
               }
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
         AV12error = "";
         AV21oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV11description = "";
         AV28signedDateTime = (DateTime)(DateTime.MinValue);
         AV20oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV19muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV17message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char2 = "";
         AV16groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV23sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_guid5 = Guid.Empty;
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV15group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV30transactionFileName = "";
         AV31TransactionId = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV33GXV1 ;
      private int AV34GXV2 ;
      private int AV35GXV3 ;
      private int AV36GXV4 ;
      private long GXt_int4 ;
      private decimal AV25sendCoins ;
      private decimal AV29transactionFee ;
      private string AV26sendTo ;
      private string AV9changeTo ;
      private string AV12error ;
      private string GXt_char2 ;
      private string AV30transactionFileName ;
      private string AV31TransactionId ;
      private string GXt_char3 ;
      private DateTime AV28signedDateTime ;
      private bool AV24sendAllCoins ;
      private bool AV18muSigCompleated ;
      private string AV22psbt ;
      private string AV11description ;
      private Guid AV27signatureID ;
      private Guid GXt_guid5 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV32transactionsToSend ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV21oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV20oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV19muSigSignatures ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV17message_signature ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16groupContact ;
      private GeneXus.Programs.nostr.SdtSDT_message AV23sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt_delete ;
      private string aP10_error ;
   }

}
