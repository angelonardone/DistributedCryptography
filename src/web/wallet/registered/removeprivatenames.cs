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
   public class removeprivatenames : GXProcedure
   {
      public removeprivatenames( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public removeprivatenames( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         this.AV8group_sdt = aP0_group_sdt;
         initialize();
         ExecuteImpl();
         aP0_group_sdt=this.AV8group_sdt;
      }

      public GeneXus.Programs.wallet.registered.SdtGroup_SDT executeUdp( )
      {
         execute(ref aP0_group_sdt);
         return AV8group_sdt ;
      }

      public void executeSubmit( ref GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         this.AV8group_sdt = aP0_group_sdt;
         SubmitImpl();
         aP0_group_sdt=this.AV8group_sdt;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10GXV1 = 1;
         while ( AV10GXV1 <= AV8group_sdt.gxTpr_Contact.Count )
         {
            AV9groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV8group_sdt.gxTpr_Contact.Item(AV10GXV1));
            AV9groupContact.gxTpr_Contactprivatename = "";
            AV10GXV1 = (int)(AV10GXV1+1);
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
         AV9groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV10GXV1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV9groupContact ;
   }

}
