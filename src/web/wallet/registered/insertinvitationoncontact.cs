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
   public class insertinvitationoncontact : GXProcedure
   {
      public insertinvitationoncontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public insertinvitationoncontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out bool aP1_isContactDeclined ,
                           out string aP2_error )
      {
         this.AV14sdt_message = aP0_sdt_message;
         this.AV17isContactDeclined = false ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isContactDeclined=this.AV17isContactDeclined;
         aP2_error=this.AV12error;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                out bool aP1_isContactDeclined )
      {
         execute(aP0_sdt_message, out aP1_isContactDeclined, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out bool aP1_isContactDeclined ,
                                 out string aP2_error )
      {
         this.AV14sdt_message = aP0_sdt_message;
         this.AV17isContactDeclined = false ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_isContactDeclined=this.AV17isContactDeclined;
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15message_signature.FromJSonString(AV14sdt_message.gxTpr_Message, null);
         GXt_char1 = AV12error;
         new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  AV15message_signature.gxTpr_Pubkey,  StringUtil.Trim( AV15message_signature.gxTpr_Username)+StringUtil.Trim( AV15message_signature.gxTpr_Pubkey),  AV15message_signature.gxTpr_Signature, out  AV16isOk, out  GXt_char1) ;
         AV12error = GXt_char1;
         if ( AV16isOk )
         {
            AV8contactId = Guid.Empty;
            AV10allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV12error), null);
            AV19GXV1 = 1;
            while ( AV19GXV1 <= AV10allContacts.Count )
            {
               AV11contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV10allContacts.Item(AV19GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV11contact.gxTpr_Username), StringUtil.Trim( AV15message_signature.gxTpr_Username)) == 0 )
               {
                  AV8contactId = AV11contact.gxTpr_Contactrid;
                  AV13contactFound = true;
                  AV17isContactDeclined = AV11contact.gxTpr_Invitationdeclined;
                  if (true) break;
               }
               AV19GXV1 = (int)(AV19GXV1+1);
            }
            if ( ! AV13contactFound && String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               AV11contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
               AV11contact.gxTpr_Username = StringUtil.Trim( AV15message_signature.gxTpr_Username);
               AV11contact.gxTpr_Userpubkey = StringUtil.Trim( AV15message_signature.gxTpr_Newpubkey);
               AV11contact.gxTpr_Messagepubkey = StringUtil.Trim( AV15message_signature.gxTpr_Pubkey);
               AV11contact.gxTpr_Grouppubkey = StringUtil.Trim( AV15message_signature.gxTpr_Grouppubkey);
               GXt_dtime2 = (DateTime)(DateTime.MinValue);
               new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV14sdt_message.gxTpr_Datetimeunix, out  GXt_dtime2) ;
               AV11contact.gxTpr_Invitationsent = GXt_dtime2;
               AV10allContacts.Add(AV11contact, 0);
               GXt_char1 = AV12error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV10allContacts.ToJSonString(false), out  GXt_char1) ;
               AV12error = GXt_char1;
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
         AV12error = "";
         AV15message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8contactId = Guid.Empty;
         AV10allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV11contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV19GXV1 ;
      private string AV12error ;
      private string GXt_char1 ;
      private DateTime GXt_dtime2 ;
      private bool AV17isContactDeclined ;
      private bool AV16isOk ;
      private bool AV13contactFound ;
      private Guid AV8contactId ;
      private bool aP1_isContactDeclined ;
      private string aP2_error ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV10allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV11contact ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV15message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV14sdt_message ;
   }

}
