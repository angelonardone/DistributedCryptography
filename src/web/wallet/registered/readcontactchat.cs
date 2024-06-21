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
   public class readcontactchat : GXProcedure
   {
      public readcontactchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readcontactchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_contactId ,
                           out GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> aP1_sdt_all_chats )
      {
         this.AV8contactId = aP0_contactId;
         this.AV12sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP1_sdt_all_chats=this.AV12sdt_all_chats;
      }

      public GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> executeUdp( Guid aP0_contactId )
      {
         execute(aP0_contactId, out aP1_sdt_all_chats);
         return AV12sdt_all_chats ;
      }

      public void executeSubmit( Guid aP0_contactId ,
                                 out GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> aP1_sdt_all_chats )
      {
         this.AV8contactId = aP0_contactId;
         this.AV12sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography") ;
         SubmitImpl();
         aP1_sdt_all_chats=this.AV12sdt_all_chats;
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
         AV12sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         AV14wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13directory = new GxDirectory(context.GetPhysicalPath());
         AV10contac_file_name = "";
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV10contac_file_name ;
      private string AV9error ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private Guid AV8contactId ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> aP1_sdt_all_chats ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> AV12sdt_all_chats ;
      private GxDirectory AV13directory ;
      private GeneXus.Programs.wallet.SdtWallet AV14wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
   }

}
