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
namespace GeneXus.Programs {
   public class apibaseurlauth : GXProcedure
   {
      public apibaseurlauth( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public apibaseurlauth( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_PathAndMethod ,
                           out string aP1_BaseURL )
      {
         this.AV9PathAndMethod = aP0_PathAndMethod;
         this.AV8BaseURL = "" ;
         initialize();
         ExecuteImpl();
         aP1_BaseURL=this.AV8BaseURL;
      }

      public string executeUdp( string aP0_PathAndMethod )
      {
         execute(aP0_PathAndMethod, out aP1_BaseURL);
         return AV8BaseURL ;
      }

      public void executeSubmit( string aP0_PathAndMethod ,
                                 out string aP1_BaseURL )
      {
         this.AV9PathAndMethod = aP0_PathAndMethod;
         this.AV8BaseURL = "" ;
         SubmitImpl();
         aP1_BaseURL=this.AV8BaseURL;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV11wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV11wallet = GXt_SdtWallet1;
         if ( ( StringUtil.StrCmp(AV9PathAndMethod, "/getUserGroups - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/getUserPubKey - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/getUserContacts - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/getGroup - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/getMesssages - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/createContact - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/updateContact - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/deleteContact - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/sendMesage - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/deleteMesage - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/createGroup - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/updateGroup - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/deleteGroup - POST") == 0 ) )
         {
            if ( StringUtil.StrCmp(AV11wallet.gxTpr_Networktype, "MainNet") == 0 )
            {
               AV8BaseURL = "http://auth.distributedcryptography.com//DesktopApp/services/rest";
            }
            else
            {
               AV8BaseURL = "http://auth-testnet.distributedcryptography.com//DesktopApp/services/rest";
            }
         }
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
         AV8BaseURL = "";
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         /* GeneXus formulas. */
      }

      private string AV9PathAndMethod ;
      private string AV8BaseURL ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP1_BaseURL ;
   }

}
