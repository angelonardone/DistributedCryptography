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
   public class receivedfinishedmusig : GXProcedure
   {
      public receivedfinishedmusig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public receivedfinishedmusig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_error )
      {
         this.AV22sdt_message = aP0_sdt_message;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV12error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message )
      {
         execute(aP0_sdt_message, out aP1_error);
         return AV12error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out string aP1_error )
      {
         this.AV22sdt_message = aP0_sdt_message;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV23muSigSignatures.FromJSonString(AV22sdt_message.gxTpr_Message, null);
         AV19message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV12error), null);
         AV27GXV1 = 1;
         while ( AV27GXV1 <= AV8all_groups_sdt.Count )
         {
            AV15group_sdt_temp = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV27GXV1));
            if ( AV15group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid == AV23muSigSignatures.gxTpr_Referencegroupid )
            {
               AV14group_sdt_found = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV15group_sdt_temp.Clone());
               AV28GXV2 = 1;
               while ( AV28GXV2 <= AV14group_sdt_found.gxTpr_Contact.Count )
               {
                  AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt_found.gxTpr_Contact.Item(AV28GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV16groupContact.gxTpr_Contactusername), StringUtil.Trim( AV23muSigSignatures.gxTpr_Senderusername)) == 0 )
                  {
                     AV19message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV16groupContact.gxTpr_Contactuserpubkey);
                     AV19message_signature.gxTpr_Username = StringUtil.Trim( AV16groupContact.gxTpr_Contactusername);
                     AV19message_signature.gxTpr_Signature = StringUtil.Trim( AV23muSigSignatures.gxTpr_Sendersignature);
                     if (true) break;
                  }
                  AV28GXV2 = (int)(AV28GXV2+1);
               }
            }
            AV27GXV1 = (int)(AV27GXV1+1);
         }
         AV11contactNotFound = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19message_signature.gxTpr_Username)) && String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            GXt_char1 = AV12error;
            new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  StringUtil.Trim( AV19message_signature.gxTpr_Grouppubkey),  StringUtil.Trim( AV19message_signature.gxTpr_Username)+StringUtil.Trim( AV19message_signature.gxTpr_Grouppubkey),  AV19message_signature.gxTpr_Signature, out  AV18isOk, out  GXt_char1) ;
            AV12error = GXt_char1;
            if ( AV18isOk )
            {
               AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV12error), null);
               AV29GXV3 = 1;
               while ( AV29GXV3 <= AV8all_groups_sdt.Count )
               {
                  AV21oneGroup = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV29GXV3));
                  if ( AV21oneGroup.gxTpr_Othergroup.gxTpr_Referencegroupid == AV23muSigSignatures.gxTpr_Referencegroupid )
                  {
                     AV30GXV4 = 1;
                     while ( AV30GXV4 <= AV21oneGroup.gxTpr_Contact.Count )
                     {
                        AV16groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV21oneGroup.gxTpr_Contact.Item(AV30GXV4));
                        if ( StringUtil.StrCmp(StringUtil.Trim( AV16groupContact.gxTpr_Contactusername), StringUtil.Trim( AV23muSigSignatures.gxTpr_Senderusername)) == 0 )
                        {
                           AV16groupContact.gxTpr_Musigsignatures.Add(AV23muSigSignatures, 0);
                           if (true) break;
                        }
                        AV30GXV4 = (int)(AV30GXV4+1);
                     }
                     GXt_char1 = AV12error;
                     new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV21oneGroup,  StringUtil.Trim( AV21oneGroup.gxTpr_Encpassword), out  AV17grpupId, out  GXt_char1) ;
                     AV12error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        GXt_char1 = AV12error;
                        new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
                        AV12error = GXt_char1;
                        AV25transactionsToSend = (GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>)(AV23muSigSignatures.gxTpr_Transactions.Clone());
                        AV24transactionFileName = StringUtil.Trim( AV21oneGroup.gxTpr_Groupid.ToString()) + ".gtrn";
                        AV26TransactionId = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV25transactionsToSend.Item(1)).gxTpr_Senttransactionid);
                        GXt_char1 = AV12error;
                        new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV24transactionFileName,  AV26TransactionId,  AV25transactionsToSend, out  GXt_char1) ;
                        AV12error = GXt_char1;
                     }
                     if (true) break;
                  }
                  AV29GXV3 = (int)(AV29GXV3+1);
               }
            }
            else
            {
               AV12error = "The signature does NOT match the user: " + AV19message_signature.ToJSonString(false, true) + AV23muSigSignatures.ToJSonString(false, true);
            }
         }
         else
         {
            AV12error = "We could not find the ContactId on the Finished Signature";
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
         AV23muSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV19message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV15group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14group_sdt_found = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV21oneGroup = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17grpupId = Guid.Empty;
         AV25transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV24transactionFileName = "";
         AV26TransactionId = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV27GXV1 ;
      private int AV28GXV2 ;
      private int AV29GXV3 ;
      private int AV30GXV4 ;
      private string AV12error ;
      private string AV24transactionFileName ;
      private string AV26TransactionId ;
      private string GXt_char1 ;
      private bool AV11contactNotFound ;
      private bool AV18isOk ;
      private Guid AV17grpupId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV22sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV23muSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV19message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt_found ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV21oneGroup ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV25transactionsToSend ;
      private string aP1_error ;
   }

}
