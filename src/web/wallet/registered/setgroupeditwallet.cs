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
   public class setgroupeditwallet : GXProcedure
   {
      public setgroupeditwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setgroupeditwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out short aP1_groupType )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10groupType = 0 ;
         initialize();
         ExecuteImpl();
         aP1_groupType=this.AV10groupType;
      }

      public short executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_groupType);
         return AV10groupType ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out short aP1_groupType )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10groupType = 0 ;
         SubmitImpl();
         aP1_groupType=this.AV10groupType;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV8group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV9groupId, out  GXt_SdtGroup_SDT1) ;
         AV8group_sdt = GXt_SdtGroup_SDT1;
         AV11websession.Set("Group_EDIT_WALLET", AV8group_sdt.ToJSonString(false, true));
         AV10groupType = AV8group_sdt.gxTpr_Grouptype;
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

      private short AV10groupType ;
      private Guid AV9groupId ;
      private IGxSession AV11websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private short aP1_groupType ;
   }

}
