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
namespace GeneXus.Programs.googleauthenticator {
   public class getcurrentpin : GXProcedure
   {
      public getcurrentpin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcurrentpin( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_based32Key ,
                           out string aP1_generatedPIN ,
                           out string aP2_error )
      {
         this.AV8based32Key = aP0_based32Key;
         this.AV10generatedPIN = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_generatedPIN=this.AV10generatedPIN;
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_based32Key ,
                                out string aP1_generatedPIN )
      {
         execute(aP0_based32Key, out aP1_generatedPIN, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_based32Key ,
                                 out string aP1_generatedPIN ,
                                 out string aP2_error )
      {
         this.AV8based32Key = aP0_based32Key;
         this.AV10generatedPIN = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_generatedPIN=this.AV10generatedPIN;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          Google.Authenticator.TwoFactorAuthenticator twoFactor = new Google.Authenticator.TwoFactorAuthenticator();
         /* User Code */
          string pin = twoFactor.GetCurrentPIN(AV8based32Key, true);
         /* User Code */
          AV10generatedPIN = pin;
         /* User Code */
         	}
         /* User Code */
         	catch (Exception ex)
         /* User Code */
         	{
         /* User Code */
         		AV9error = ex.Message.ToString();
         /* User Code */
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
         AV10generatedPIN = "";
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV8based32Key ;
      private string AV10generatedPIN ;
      private string AV9error ;
      private string aP1_generatedPIN ;
      private string aP2_error ;
   }

}
