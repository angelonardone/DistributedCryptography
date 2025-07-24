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
   public class getnewesttimeconstrain : GXProcedure
   {
      public getnewesttimeconstrain( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getnewesttimeconstrain( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> aP0_inlTimeConstrains ,
                           out GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem aP1_oneTimeConstrain )
      {
         this.AV10inlTimeConstrains = aP0_inlTimeConstrains;
         this.AV9oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context) ;
         initialize();
         ExecuteImpl();
         aP1_oneTimeConstrain=this.AV9oneTimeConstrain;
      }

      public GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem executeUdp( GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> aP0_inlTimeConstrains )
      {
         execute(aP0_inlTimeConstrains, out aP1_oneTimeConstrain);
         return AV9oneTimeConstrain ;
      }

      public void executeSubmit( GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> aP0_inlTimeConstrains ,
                                 out GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem aP1_oneTimeConstrain )
      {
         this.AV10inlTimeConstrains = aP0_inlTimeConstrains;
         this.AV9oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context) ;
         SubmitImpl();
         aP1_oneTimeConstrain=this.AV9oneTimeConstrain;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8allTimeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV10inlTimeConstrains.Clone());
         AV8allTimeConstrains.Sort("[sequence]");
         AV11GXV1 = 1;
         while ( AV11GXV1 <= AV8allTimeConstrains.Count )
         {
            AV9oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV8allTimeConstrains.Item(AV11GXV1));
            if (true) break;
            AV11GXV1 = (int)(AV11GXV1+1);
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
         AV9oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV8allTimeConstrains = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>( context, "Group_SDT.TimeConstrainItem", "distributedcryptography");
         /* GeneXus formulas. */
      }

      private int AV11GXV1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> AV10inlTimeConstrains ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV9oneTimeConstrain ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> AV8allTimeConstrains ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem aP1_oneTimeConstrain ;
   }

}
