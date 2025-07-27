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
namespace GeneXus.Programs.wallet {
   public class getcontact : GXProcedure
   {
      public getcontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact )
      {
         this.AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context) ;
         initialize();
         ExecuteImpl();
         aP0_contact=this.AV10contact;
      }

      public GeneXus.Programs.wallet.registered.SdtContact_SDT executeUdp( )
      {
         execute(out aP0_contact);
         return AV10contact ;
      }

      public void executeSubmit( out GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact )
      {
         this.AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context) ;
         SubmitImpl();
         aP0_contact=this.AV10contact;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10contact.FromJSonString(AV9WebSession.Get("Contact_Session"), null);
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
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT aP0_contact ;
   }

}
