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
namespace GeneXus.Programs.distcrypt {
   public class getexternaluser : GXProcedure
   {
      public getexternaluser( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getexternaluser( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser )
      {
         this.AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context) ;
         initialize();
         ExecuteImpl();
         aP0_externalUser=this.AV9externalUser;
      }

      public GeneXus.Programs.distcrypt.SdtExternalUser executeUdp( )
      {
         execute(out aP0_externalUser);
         return AV9externalUser ;
      }

      public void executeSubmit( out GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser )
      {
         this.AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context) ;
         SubmitImpl();
         aP0_externalUser=this.AV9externalUser;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9externalUser.FromJSonString(AV8WebSession.Get("DistCryptExternalUser"), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUser.gxTpr_Externaltoken)) )
         {
            AV9externalUser.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "distcrypt.conf", out  AV10error), null);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV9externalUser) ;
            }
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
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV8WebSession = context.GetSession();
         AV10error = "";
         /* GeneXus formulas. */
      }

      private string AV10error ;
      private IGxSession AV8WebSession ;
      private GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
   }

}
