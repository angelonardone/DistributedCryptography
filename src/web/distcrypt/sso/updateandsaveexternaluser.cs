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
namespace GeneXus.Programs.distcrypt.sso {
   public class updateandsaveexternaluser : GXProcedure
   {
      public updateandsaveexternaluser( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public updateandsaveexternaluser( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser ,
                           out string aP1_error )
      {
         this.AV10externalUser = aP0_externalUser;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser )
      {
         execute(aP0_externalUser, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.distcrypt.SdtExternalUser aP0_externalUser ,
                                 out string aP1_error )
      {
         this.AV10externalUser = aP0_externalUser;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV10externalUser) ;
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "distcrypt.conf",  AV10externalUser.ToJSonString(false, true), out  GXt_char1) ;
         AV9error = GXt_char1;
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
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private string aP1_error ;
   }

}
