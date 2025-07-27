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
   public class appendcontactchat : GXProcedure
   {
      public appendcontactchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public appendcontactchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_contactId ,
                           GeneXus.Programs.nostr.SdtSDT_Chat aP1_sdt_chat )
      {
         this.AV8contactId = aP0_contactId;
         this.AV11sdt_chat = aP1_sdt_chat;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_contactId ,
                                 GeneXus.Programs.nostr.SdtSDT_Chat aP1_sdt_chat )
      {
         this.AV8contactId = aP0_contactId;
         this.AV11sdt_chat = aP1_sdt_chat;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV14wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV14wallet = GXt_SdtWallet1;
         AV13directory.Source = AV14wallet.gxTpr_Walletbasedirectory+"Chat";
         if ( ! AV13directory.Exists() )
         {
            AV13directory.Create();
         }
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV10contac_file_name = "Chat" + (GXt_boolean3 ? "/" : "\\") + StringUtil.Trim( AV8contactId.ToString()) + ".json";
         AV12sdt_all_chats.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV10contac_file_name, out  AV9error), null);
         AV12sdt_all_chats.Add(AV11sdt_chat, 0);
         GXt_char4 = AV9error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV10contac_file_name,  AV12sdt_all_chats.ToJSonString(false), out  GXt_char4) ;
         AV9error = GXt_char4;
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
         AV14wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13directory = new GxDirectory(context.GetPhysicalPath());
         AV10contac_file_name = "";
         AV12sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         AV9error = "";
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private string AV10contac_file_name ;
      private string AV9error ;
      private string GXt_char4 ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private Guid AV8contactId ;
      private GxDirectory AV13directory ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV11sdt_chat ;
      private GeneXus.Programs.wallet.SdtWallet AV14wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> AV12sdt_all_chats ;
   }

}
