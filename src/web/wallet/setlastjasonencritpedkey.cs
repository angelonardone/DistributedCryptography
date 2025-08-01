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
   public class setlastjasonencritpedkey : GXProcedure
   {
      public setlastjasonencritpedkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setlastjasonencritpedkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedKey )
      {
         this.AV10encryptedKey = aP0_encryptedKey;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_encryptedKey )
      {
         this.AV10encryptedKey = aP0_encryptedKey;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9websession.Set("LastJasonEncKey", StringUtil.Trim( AV10encryptedKey));
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
         AV9websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV10encryptedKey ;
      private IGxSession AV9websession ;
   }

}
