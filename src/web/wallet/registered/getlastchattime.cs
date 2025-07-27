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
   public class getlastchattime : GXProcedure
   {
      public getlastchattime( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlastchattime( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out long aP0_lastChatTimeInSeconds )
      {
         this.AV8lastChatTimeInSeconds = 0 ;
         initialize();
         ExecuteImpl();
         aP0_lastChatTimeInSeconds=this.AV8lastChatTimeInSeconds;
      }

      public long executeUdp( )
      {
         execute(out aP0_lastChatTimeInSeconds);
         return AV8lastChatTimeInSeconds ;
      }

      public void executeSubmit( out long aP0_lastChatTimeInSeconds )
      {
         this.AV8lastChatTimeInSeconds = 0 ;
         SubmitImpl();
         aP0_lastChatTimeInSeconds=this.AV8lastChatTimeInSeconds;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8lastChatTimeInSeconds = 0;
         AV12allContacts.Clear();
         AV13allContacts_temp.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV17error), null);
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV13allContacts_temp.Count )
         {
            AV15contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV13allContacts_temp.Item(AV18GXV1));
            if ( ! AV15contact.gxTpr_Invitationdeclined )
            {
               AV12allContacts.Add(AV15contact, 0);
            }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         AV19GXV2 = 1;
         while ( AV19GXV2 <= AV12allContacts.Count )
         {
            AV15contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV12allContacts.Item(AV19GXV2));
            GXt_objcol_SdtSDT_Chat1 = AV10sdt_all_chats;
            new GeneXus.Programs.wallet.registered.readcontactchat(context ).execute(  AV15contact.gxTpr_Contactrid, out  GXt_objcol_SdtSDT_Chat1) ;
            AV10sdt_all_chats = GXt_objcol_SdtSDT_Chat1;
            AV20GXV3 = 1;
            while ( AV20GXV3 <= AV10sdt_all_chats.Count )
            {
               AV11sdt_chat = ((GeneXus.Programs.nostr.SdtSDT_Chat)AV10sdt_all_chats.Item(AV20GXV3));
               if ( AV11sdt_chat.gxTpr_Datetimeunix > AV8lastChatTimeInSeconds )
               {
                  AV8lastChatTimeInSeconds = AV11sdt_chat.gxTpr_Datetimeunix;
               }
               AV20GXV3 = (int)(AV20GXV3+1);
            }
            AV19GXV2 = (int)(AV19GXV2+1);
         }
         AV8lastChatTimeInSeconds = (long)(AV8lastChatTimeInSeconds/ (decimal)(1000));
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
         AV12allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV13allContacts_temp = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV17error = "";
         AV15contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV10sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         GXt_objcol_SdtSDT_Chat1 = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         AV11sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private int AV20GXV3 ;
      private long AV8lastChatTimeInSeconds ;
      private string AV17error ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV12allContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV13allContacts_temp ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV15contact ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> AV10sdt_all_chats ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> GXt_objcol_SdtSDT_Chat1 ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV11sdt_chat ;
      private long aP0_lastChatTimeInSeconds ;
   }

}
