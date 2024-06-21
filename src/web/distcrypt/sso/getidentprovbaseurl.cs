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
namespace GeneXus.Programs.distcrypt.sso {
   public class getidentprovbaseurl : GXProcedure
   {
      public getidentprovbaseurl( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getidentprovbaseurl( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_IdentityProvider ,
                           out string aP1_BaseUrl )
      {
         this.AV9IdentityProvider = aP0_IdentityProvider;
         this.AV8BaseUrl = "" ;
         initialize();
         ExecuteImpl();
         aP1_BaseUrl=this.AV8BaseUrl;
      }

      public string executeUdp( GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_IdentityProvider )
      {
         execute(aP0_IdentityProvider, out aP1_BaseUrl);
         return AV8BaseUrl ;
      }

      public void executeSubmit( GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_IdentityProvider ,
                                 out string aP1_BaseUrl )
      {
         this.AV9IdentityProvider = aP0_IdentityProvider;
         this.AV8BaseUrl = "" ;
         SubmitImpl();
         aP1_BaseUrl=this.AV8BaseUrl;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( AV9IdentityProvider.gxTpr_Identityprovidersecure )
         {
            AV8BaseUrl = "https://";
         }
         else
         {
            AV8BaseUrl = "http://";
         }
         AV8BaseUrl += StringUtil.Trim( AV9IdentityProvider.gxTpr_Identityproviderhost);
         if ( AV9IdentityProvider.gxTpr_Identityproviderport > 0 )
         {
            AV8BaseUrl += ":" + StringUtil.Trim( StringUtil.Str( (decimal)(AV9IdentityProvider.gxTpr_Identityproviderport), 4, 0));
         }
         AV8BaseUrl += "/" + StringUtil.Trim( AV9IdentityProvider.gxTpr_Identityproviderapplication);
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
         AV8BaseUrl = "";
         /* GeneXus formulas. */
      }

      private string AV8BaseUrl ;
      private string aP1_BaseUrl ;
      private GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider AV9IdentityProvider ;
   }

}
