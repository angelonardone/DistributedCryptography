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
   public class updateacceptedinvitation : GXProcedure
   {
      public updateacceptedinvitation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updateacceptedinvitation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_error )
      {
         this.AV15sdt_message = aP0_sdt_message;
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
         this.AV15sdt_message = aP0_sdt_message;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14message_signature.FromJSonString(AV15sdt_message.gxTpr_Message, null);
         GXt_char1 = AV12error;
         new GeneXus.Programs.nbitcoin.eccverifymsg(context ).execute(  AV14message_signature.gxTpr_Pubkey,  StringUtil.Trim( AV14message_signature.gxTpr_Username)+StringUtil.Trim( AV14message_signature.gxTpr_Pubkey),  AV14message_signature.gxTpr_Signature, out  AV13isOk, out  GXt_char1) ;
         AV12error = GXt_char1;
         if ( AV13isOk )
         {
            AV11contactId = Guid.Empty;
            AV8allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV12error), null);
            AV18GXV1 = 1;
            while ( AV18GXV1 <= AV8allContacts.Count )
            {
               AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV8allContacts.Item(AV18GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV9contact.gxTpr_Username), StringUtil.Trim( AV14message_signature.gxTpr_Username)) == 0 )
               {
                  if ( StringUtil.StrCmp(AV9contact.gxTpr_Userpubkey, AV14message_signature.gxTpr_Pubkey) == 0 )
                  {
                     AV9contact.gxTpr_Userpubkey = StringUtil.Trim( AV14message_signature.gxTpr_Newpubkey);
                     AV9contact.gxTpr_Grouppubkey = StringUtil.Trim( AV14message_signature.gxTpr_Grouppubkey);
                     GXt_dtime2 = (DateTime)(DateTime.MinValue);
                     new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV15sdt_message.gxTpr_Datetimeunix, out  GXt_dtime2) ;
                     AV9contact.gxTpr_Invitacionaccepted = GXt_dtime2;
                     GXt_dtime2 = (DateTime)(DateTime.MinValue);
                     new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV15sdt_message.gxTpr_Datetimeunix, out  GXt_dtime2) ;
                     ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV8allContacts.CurrentItem)).gxTpr_Invitacionaccepted = GXt_dtime2;
                     GXt_char1 = AV12error;
                     new GeneXus.Programs.wallet.registered.updatecontact(context ).execute(  AV9contact,  AV9contact.gxTpr_Contactrid, out  GXt_char1) ;
                     AV12error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        GXt_char1 = AV12error;
                        new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV8allContacts.ToJSonString(false), out  GXt_char1) ;
                        AV12error = GXt_char1;
                     }
                  }
                  else
                  {
                     AV12error = "Publick Key of sender does not match";
                  }
                  if (true) break;
               }
               AV18GXV1 = (int)(AV18GXV1+1);
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
         AV14message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV11contactId = Guid.Empty;
         AV8allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private string AV12error ;
      private string GXt_char1 ;
      private DateTime GXt_dtime2 ;
      private bool AV13isOk ;
      private Guid AV11contactId ;
      private GeneXus.Programs.nostr.SdtSDT_message AV15sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV14message_signature ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV8allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private string aP1_error ;
   }

}
