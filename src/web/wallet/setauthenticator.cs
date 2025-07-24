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
   public class setauthenticator : GXProcedure
   {
      public setauthenticator( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setauthenticator( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator aP0_authenticator )
      {
         this.AV10authenticator = aP0_authenticator;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator aP0_authenticator )
      {
         this.AV10authenticator = aP0_authenticator;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9WebSession.Set("AuthenticatorSession", AV10authenticator.ToJSonString(false, true));
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
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator AV10authenticator ;
   }

}
