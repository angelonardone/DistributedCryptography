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
   public class recovergroupsfromserver : GXProcedure
   {
      public recovergroupsfromserver( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public recovergroupsfromserver( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV9error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV9error = "" ;
         SubmitImpl();
         aP0_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.registered.getgroups(context ).execute( out  AV8all_groups_sdt, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
            AV9error = GXt_char1;
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
         AV9error = "";
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private string aP0_error ;
   }

}
