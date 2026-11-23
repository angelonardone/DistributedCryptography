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
namespace GeneXus.Programs.hsm {
   public class regeneratehsmapikey : GXProcedure
   {
      public regeneratehsmapikey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public regeneratehsmapikey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_newApiKey ,
                           out string aP1_error )
      {
         this.AV11newApiKey = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP0_newApiKey=this.AV11newApiKey;
         aP1_error=this.AV8error;
      }

      public string executeUdp( out string aP0_newApiKey )
      {
         execute(out aP0_newApiKey, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( out string aP0_newApiKey ,
                                 out string aP1_error )
      {
         this.AV11newApiKey = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP0_newApiKey=this.AV11newApiKey;
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11newApiKey = "";
         AV9HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV8error), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            if ( AV9HSMconfigSDT.gxTpr_Isactive )
            {
               AV13isMine = AV10HsmManager.isrunningwithapikey(AV9HSMconfigSDT.gxTpr_Apikey);
               AV9HSMconfigSDT.gxTpr_Apikey = AV10HsmManager.newapikey();
               GXt_char1 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "hsm.dat",  AV9HSMconfigSDT.ToJSonString(false, true), out  GXt_char1) ;
               AV8error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  AV11newApiKey = AV9HSMconfigSDT.gxTpr_Apikey;
                  if ( AV13isMine )
                  {
                     AV12ok = AV10HsmManager.setaccess(AV9HSMconfigSDT.gxTpr_Apikey, AV9HSMconfigSDT.gxTpr_Requirehmac);
                  }
               }
            }
            else
            {
               AV8error = "The HSM is not active";
            }
         }
         AV9HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
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
         AV11newApiKey = "";
         AV8error = "";
         AV9HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV11newApiKey ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV13isMine ;
      private bool AV12ok ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV9HSMconfigSDT ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP0_newApiKey ;
      private string aP1_error ;
   }

}
