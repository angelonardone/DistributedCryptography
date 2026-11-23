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
   public class savehsmsettings : GXProcedure
   {
      public savehsmsettings( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savehsmsettings( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( bool aP0_isActive ,
                           bool aP1_requireHmac ,
                           out string aP2_newApiKey ,
                           out string aP3_error )
      {
         this.AV11isActive = aP0_isActive;
         this.AV14requireHmac = aP1_requireHmac;
         this.AV12newApiKey = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_newApiKey=this.AV12newApiKey;
         aP3_error=this.AV8error;
      }

      public string executeUdp( bool aP0_isActive ,
                                bool aP1_requireHmac ,
                                out string aP2_newApiKey )
      {
         execute(aP0_isActive, aP1_requireHmac, out aP2_newApiKey, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( bool aP0_isActive ,
                                 bool aP1_requireHmac ,
                                 out string aP2_newApiKey ,
                                 out string aP3_error )
      {
         this.AV11isActive = aP0_isActive;
         this.AV14requireHmac = aP1_requireHmac;
         this.AV12newApiKey = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_newApiKey=this.AV12newApiKey;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12newApiKey = "";
         AV9HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV8error), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV16isMine = AV10HsmManager.isrunningwithapikey(AV9HSMconfigSDT.gxTpr_Apikey);
            AV9HSMconfigSDT.gxTpr_Isactive = AV11isActive;
            AV9HSMconfigSDT.gxTpr_Requirehmac = AV14requireHmac;
            if ( AV11isActive )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9HSMconfigSDT.gxTpr_Apikey)) )
               {
                  AV9HSMconfigSDT.gxTpr_Apikey = AV10HsmManager.newapikey();
                  AV12newApiKey = AV9HSMconfigSDT.gxTpr_Apikey;
               }
            }
            else
            {
               AV9HSMconfigSDT.gxTpr_Apikey = "";
            }
            GXt_char1 = AV8error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "hsm.dat",  AV9HSMconfigSDT.ToJSonString(false, true), out  GXt_char1) ;
            AV8error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               if ( AV11isActive )
               {
                  if ( AV16isMine )
                  {
                     AV13ok = AV10HsmManager.setaccess(AV9HSMconfigSDT.gxTpr_Apikey, AV9HSMconfigSDT.gxTpr_Requirehmac);
                  }
               }
               else
               {
                  if ( AV16isMine )
                  {
                     AV10HsmManager.clear();
                  }
               }
            }
            else
            {
               AV12newApiKey = "";
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
         AV12newApiKey = "";
         AV8error = "";
         AV9HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV12newApiKey ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isActive ;
      private bool AV14requireHmac ;
      private bool AV16isMine ;
      private bool AV13ok ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV9HSMconfigSDT ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP2_newApiKey ;
      private string aP3_error ;
   }

}
