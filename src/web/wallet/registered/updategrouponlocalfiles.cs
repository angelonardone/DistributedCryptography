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
   public class updategrouponlocalfiles : GXProcedure
   {
      public updategrouponlocalfiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updategrouponlocalfiles( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           out string aP1_error )
      {
         this.AV20group_sdt = aP0_group_sdt;
         this.AV15error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV15error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         execute(aP0_group_sdt, out aP1_error);
         return AV15error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 out string aP1_error )
      {
         this.AV20group_sdt = aP0_group_sdt;
         this.AV15error = "" ;
         SubmitImpl();
         aP1_error=this.AV15error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12all_groups_sdt.Clear();
         AV12all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV15error), null);
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV12all_groups_sdt.Count )
         {
            AV21group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV12all_groups_sdt.Item(AV22GXV1));
            if ( AV21group_sdt_delete.gxTpr_Groupid == AV20group_sdt.gxTpr_Groupid )
            {
               AV12all_groups_sdt.RemoveItem(AV12all_groups_sdt.IndexOf(AV21group_sdt_delete));
            }
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         AV12all_groups_sdt.Add(AV20group_sdt, 0);
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV12all_groups_sdt.ToJSonString(false), out  AV15error) ;
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
         AV15error = "";
         AV12all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV21group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private string AV15error ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV12all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV21group_sdt_delete ;
      private string aP1_error ;
   }

}
