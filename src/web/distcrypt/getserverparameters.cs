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
   public class getserverparameters : GXProcedure
   {
      public getserverparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getserverparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters )
      {
         this.AV10ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context) ;
         initialize();
         ExecuteImpl();
         aP0_ServerParameters=this.AV10ServerParameters;
      }

      public GeneXus.Programs.distcrypt.SdtServerParameters executeUdp( )
      {
         execute(out aP0_ServerParameters);
         return AV10ServerParameters ;
      }

      public void executeSubmit( out GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters )
      {
         this.AV10ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context) ;
         SubmitImpl();
         aP0_ServerParameters=this.AV10ServerParameters;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10ServerParameters.FromJSonString(AV12websession.Get("DistCryptServerParameters"), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10ServerParameters.gxTpr_Serverhost)) )
         {
            GXt_boolean1 = AV9isOpen;
            new GeneXus.Programs.wallet.iswalletopen(context ).execute( out  GXt_boolean1) ;
            AV9isOpen = GXt_boolean1;
            if ( AV9isOpen )
            {
               GXt_char2 = AV11stored;
               new GeneXus.Programs.wallet.readjsonencfile(context ).execute(  "server.conf", out  AV8error, out  GXt_char2) ;
               AV11stored = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV11stored)) )
               {
                  AV10ServerParameters.FromJSonString(AV11stored, null);
               }
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10ServerParameters.gxTpr_Serverhost)) )
            {
               GXt_SdtServerParameters3 = AV10ServerParameters;
               new GeneXus.Programs.distcrypt.getdefaultserverparameters(context ).execute( out  GXt_SdtServerParameters3) ;
               AV10ServerParameters = GXt_SdtServerParameters3;
            }
            if ( AV9isOpen )
            {
               AV12websession.Set("DistCryptServerParameters", AV10ServerParameters.ToJSonString(false, true));
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
         AV10ServerParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV12websession = context.GetSession();
         AV11stored = "";
         GXt_char2 = "";
         AV8error = "";
         GXt_SdtServerParameters3 = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         /* GeneXus formulas. */
      }

      private string GXt_char2 ;
      private string AV8error ;
      private bool AV9isOpen ;
      private bool GXt_boolean1 ;
      private string AV11stored ;
      private IGxSession AV12websession ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV10ServerParameters ;
      private GeneXus.Programs.distcrypt.SdtServerParameters GXt_SdtServerParameters3 ;
      private GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters ;
   }

}
