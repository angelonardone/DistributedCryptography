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
namespace GeneXus.Programs.nostr {
   public class savenostrconfigservers : GXProcedure
   {
      public savenostrconfigservers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savenostrconfigservers( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ,
                           out string aP1_error )
      {
         this.AV9ConnectionParameters = aP0_ConnectionParameters;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV14error;
      }

      public string executeUdp( GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters )
      {
         execute(aP0_ConnectionParameters, out aP1_error);
         return AV14error ;
      }

      public void executeSubmit( GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ,
                                 out string aP1_error )
      {
         this.AV9ConnectionParameters = aP0_ConnectionParameters;
         this.AV14error = "" ;
         SubmitImpl();
         aP1_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15nostrFileName = "nostr.conf";
         GXt_char1 = AV14error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  StringUtil.Trim( AV15nostrFileName),  AV9ConnectionParameters.ToJSonString(false), out  GXt_char1) ;
         AV14error = GXt_char1;
         AV16websession.Set("NOSTR_CONFIG_CONNECTION", AV9ConnectionParameters.ToJSonString(false));
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
         AV14error = "";
         AV15nostrFileName = "";
         GXt_char1 = "";
         AV16websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV14error ;
      private string AV15nostrFileName ;
      private string GXt_char1 ;
      private IGxSession AV16websession ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV9ConnectionParameters ;
      private string aP1_error ;
   }

}
