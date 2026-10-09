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
   public class setgroupedit : GXProcedure
   {
      public setgroupedit( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setgroupedit( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_referenceGroupId )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10referenceGroupId = aP1_referenceGroupId;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_referenceGroupId )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10referenceGroupId = aP1_referenceGroupId;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV8group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupforrow(context ).execute(  AV9groupId,  AV10referenceGroupId, out  GXt_SdtGroup_SDT1) ;
         AV8group_sdt = GXt_SdtGroup_SDT1;
         AV11websession.Set("Group_EDIT", AV8group_sdt.ToJSonString(false, true));
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
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private Guid AV9groupId ;
      private Guid AV10referenceGroupId ;
      private IGxSession AV11websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
   }

}
