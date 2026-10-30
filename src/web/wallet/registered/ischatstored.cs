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
   public class ischatstored : GXProcedure
   {
      public ischatstored( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public ischatstored( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_contactId ,
                           Guid aP1_chatId ,
                           out bool aP2_isStored )
      {
         this.AV10contactId = aP0_contactId;
         this.AV8chatId = aP1_chatId;
         this.AV12isStored = false ;
         initialize();
         ExecuteImpl();
         aP2_isStored=this.AV12isStored;
      }

      public bool executeUdp( Guid aP0_contactId ,
                              Guid aP1_chatId )
      {
         execute(aP0_contactId, aP1_chatId, out aP2_isStored);
         return AV12isStored ;
      }

      public void executeSubmit( Guid aP0_contactId ,
                                 Guid aP1_chatId ,
                                 out bool aP2_isStored )
      {
         this.AV10contactId = aP0_contactId;
         this.AV8chatId = aP1_chatId;
         this.AV12isStored = false ;
         SubmitImpl();
         aP2_isStored=this.AV12isStored;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12isStored = false;
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV9contac_file_name = "Chat" + (GXt_boolean2 ? "/" : "\\") + StringUtil.Trim( AV10contactId.ToString()) + ".json";
         AV14sdt_all_chats.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV9contac_file_name, out  AV11error), null);
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV14sdt_all_chats.Count )
         {
            AV13one_chat = ((GeneXus.Programs.nostr.SdtSDT_Chat)AV14sdt_all_chats.Item(AV15GXV1));
            if ( AV13one_chat.gxTpr_Id == AV8chatId )
            {
               AV12isStored = true;
               if (true) break;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
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
         AV9contac_file_name = "";
         AV14sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         AV11error = "";
         AV13one_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV9contac_file_name ;
      private string AV11error ;
      private bool AV12isStored ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private Guid AV10contactId ;
      private Guid AV8chatId ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> AV14sdt_all_chats ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV13one_chat ;
      private bool aP2_isStored ;
   }

}
