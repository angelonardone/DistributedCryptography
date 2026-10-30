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
   public class readgroupchat : GXProcedure
   {
      public readgroupchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readgroupchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> aP1_messages )
      {
         this.AV11referenceGroupId = aP0_referenceGroupId;
         this.AV10messages = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP1_messages=this.AV10messages;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> executeUdp( Guid aP0_referenceGroupId )
      {
         execute(aP0_referenceGroupId, out aP1_messages);
         return AV10messages ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> aP1_messages )
      {
         this.AV11referenceGroupId = aP0_referenceGroupId;
         this.AV10messages = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography") ;
         SubmitImpl();
         aP1_messages=this.AV10messages;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV9fileName = "Chat" + (GXt_boolean2 ? "/" : "\\") + "group-" + StringUtil.Trim( AV11referenceGroupId.ToString()) + ".json";
         AV10messages.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV9fileName, out  AV8error), null);
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
         AV10messages = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography");
         AV9fileName = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV9fileName ;
      private string AV8error ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private Guid AV11referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> AV10messages ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> aP1_messages ;
   }

}
