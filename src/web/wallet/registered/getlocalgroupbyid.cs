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
   public class getlocalgroupbyid : GXProcedure
   {
      public getlocalgroupbyid( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlocalgroupbyid( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt )
      {
         this.AV10groupId = aP0_groupId;
         this.AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         initialize();
         ExecuteImpl();
         aP1_group_sdt=this.AV11group_sdt;
      }

      public GeneXus.Programs.wallet.registered.SdtGroup_SDT executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_group_sdt);
         return AV11group_sdt ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt )
      {
         this.AV10groupId = aP0_groupId;
         this.AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context) ;
         SubmitImpl();
         aP1_group_sdt=this.AV11group_sdt;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8all_groups_sdt.Clear();
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV12error), null);
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV8all_groups_sdt.Count )
         {
            AV9group_sdt_temp = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV13GXV1));
            if ( AV9group_sdt_temp.gxTpr_Groupid == AV10groupId )
            {
               AV11group_sdt = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV9group_sdt_temp.Clone());
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV12error = "";
         AV9group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV12error ;
      private Guid AV10groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt_temp ;
   }

}
