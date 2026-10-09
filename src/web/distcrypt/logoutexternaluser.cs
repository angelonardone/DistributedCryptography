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
   public class logoutexternaluser : GXProcedure
   {
      public logoutexternaluser( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public logoutexternaluser( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV8error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV8error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV8error = "" ;
         SubmitImpl();
         aP0_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         AV9externalUser.gxTpr_Externaltoken = "";
         AV9externalUser.gxTpr_Userinfo = new();
         new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV9externalUser) ;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "distcrypt.conf",  AV9externalUser.ToJSonString(false, true), out  AV8error) ;
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
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP0_error ;
   }

}
