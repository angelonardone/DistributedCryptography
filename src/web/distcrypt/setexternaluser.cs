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
namespace GeneXus.Programs.distcrypt {
   public class setexternaluser : GXProcedure
   {
      public setexternaluser( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setexternaluser( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser )
      {
         this.AV9externalUser = aP0_externalUser;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser )
      {
         this.AV9externalUser = aP0_externalUser;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8WebSession.Set("DistCryptExternalUser", AV9externalUser.ToJSonString(false, true));
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
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV8WebSession ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
   }

}
