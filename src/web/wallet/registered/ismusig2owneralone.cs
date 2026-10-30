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
   public class ismusig2owneralone : GXProcedure
   {
      public ismusig2owneralone( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public ismusig2owneralone( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_ownerAlone )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10ownerAlone = false ;
         initialize();
         ExecuteImpl();
         aP1_ownerAlone=this.AV10ownerAlone;
      }

      public bool executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_ownerAlone);
         return AV10ownerAlone ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_ownerAlone )
      {
         this.AV9groupId = aP0_groupId;
         this.AV10ownerAlone = false ;
         SubmitImpl();
         aP1_ownerAlone=this.AV10ownerAlone;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV8group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV9groupId, out  GXt_SdtGroup_SDT1) ;
         AV8group_sdt = GXt_SdtGroup_SDT1;
         AV10ownerAlone = false;
         if ( ( AV8group_sdt.gxTpr_Grouptype == 80 ) && AV8group_sdt.gxTpr_Amigroupowner )
         {
            AV10ownerAlone = true;
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
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private bool AV10ownerAlone ;
      private Guid AV9groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private bool aP1_ownerAlone ;
   }

}
