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
   public class gethsmsettings : GXProcedure
   {
      public gethsmsettings( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gethsmsettings( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out bool aP0_isActive ,
                           out bool aP1_requireHmac ,
                           out bool aP2_hasApiKey ,
                           out string aP3_error )
      {
         this.AV11isActive = false ;
         this.AV12requireHmac = false ;
         this.AV9hasApiKey = false ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP0_isActive=this.AV11isActive;
         aP1_requireHmac=this.AV12requireHmac;
         aP2_hasApiKey=this.AV9hasApiKey;
         aP3_error=this.AV8error;
      }

      public string executeUdp( out bool aP0_isActive ,
                                out bool aP1_requireHmac ,
                                out bool aP2_hasApiKey )
      {
         execute(out aP0_isActive, out aP1_requireHmac, out aP2_hasApiKey, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( out bool aP0_isActive ,
                                 out bool aP1_requireHmac ,
                                 out bool aP2_hasApiKey ,
                                 out string aP3_error )
      {
         this.AV11isActive = false ;
         this.AV12requireHmac = false ;
         this.AV9hasApiKey = false ;
         this.AV8error = "" ;
         SubmitImpl();
         aP0_isActive=this.AV11isActive;
         aP1_requireHmac=this.AV12requireHmac;
         aP2_hasApiKey=this.AV9hasApiKey;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV8error), null);
         AV11isActive = AV10HSMconfigSDT.gxTpr_Isactive;
         AV12requireHmac = AV10HSMconfigSDT.gxTpr_Requirehmac;
         AV9hasApiKey = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV10HSMconfigSDT.gxTpr_Apikey)));
         AV10HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
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
         AV10HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private bool AV11isActive ;
      private bool AV12requireHmac ;
      private bool AV9hasApiKey ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV10HSMconfigSDT ;
      private bool aP0_isActive ;
      private bool aP1_requireHmac ;
      private bool aP2_hasApiKey ;
      private string aP3_error ;
   }

}
