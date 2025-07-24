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
   public class getauthenticator : GXProcedure
   {
      public getauthenticator( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getauthenticator( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator aP0_authenticator )
      {
         this.AV10authenticator = new GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator(context) ;
         initialize();
         ExecuteImpl();
         aP0_authenticator=this.AV10authenticator;
      }

      public GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator executeUdp( )
      {
         execute(out aP0_authenticator);
         return AV10authenticator ;
      }

      public void executeSubmit( out GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator aP0_authenticator )
      {
         this.AV10authenticator = new GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator(context) ;
         SubmitImpl();
         aP0_authenticator=this.AV10authenticator;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10authenticator.FromJSonString(AV8WebSession.Get("AuthenticatorSession"), null);
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
         AV10authenticator = new GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator(context);
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV8WebSession ;
      private GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator AV10authenticator ;
      private GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator aP0_authenticator ;
   }

}
