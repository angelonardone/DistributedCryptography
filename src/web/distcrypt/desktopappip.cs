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
   public class desktopappip : GXProcedure
   {
      public desktopappip( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public desktopappip( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_Gxm1sdt_identityprovider )
      {
         this.Gxm1sdt_identityprovider = new GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider(context) ;
         initialize();
         ExecuteImpl();
         aP0_Gxm1sdt_identityprovider=this.Gxm1sdt_identityprovider;
      }

      public GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider executeUdp( )
      {
         execute(out aP0_Gxm1sdt_identityprovider);
         return Gxm1sdt_identityprovider ;
      }

      public void executeSubmit( out GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_Gxm1sdt_identityprovider )
      {
         this.Gxm1sdt_identityprovider = new GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider(context) ;
         SubmitImpl();
         aP0_Gxm1sdt_identityprovider=this.Gxm1sdt_identityprovider;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1sdt_identityprovider.gxTpr_Identityproviderid = 1;
         Gxm1sdt_identityprovider.gxTpr_Identityproviderhost = "auth.distributedcryptography.com";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderport = 80;
         Gxm1sdt_identityprovider.gxTpr_Identityproviderapplication = "";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderclientid = "Idxgm8xgj7FMSpjZzQ4RR8UrUAQUzJ468eM3gU7e";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderclientsecret = "xlREvzI1rOCXoPvYfI6u3teqQyyABAFCyJUZcrD1HvGdQWRNAFLnpkfV6jhntIK9G3Ud9F1CXpjbCzE9";
         Gxm1sdt_identityprovider.gxTpr_Identityprovidersecure = true;
         Gxm1sdt_identityprovider.gxTpr_Identityproviderredirurl = "http://localhost:5080/distcrypt.sso.aloginresponse.aspx";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderdynamiccallprocedure = "";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderencryptparameter = false;
         Gxm1sdt_identityprovider.gxTpr_Identityproviderrepository = "";
         Gxm1sdt_identityprovider.gxTpr_Identityproviderencryptionkey = "";
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
         /* GeneXus formulas. */
      }

      private GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider Gxm1sdt_identityprovider ;
      private GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider aP0_Gxm1sdt_identityprovider ;
   }

}
