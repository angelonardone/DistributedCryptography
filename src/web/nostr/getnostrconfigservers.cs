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
namespace GeneXus.Programs.nostr {
   public class getnostrconfigservers : GXProcedure
   {
      public getnostrconfigservers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getnostrconfigservers( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ,
                           out string aP1_error )
      {
         this.AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP0_ConnectionParameters=this.AV9ConnectionParameters;
         aP1_error=this.AV14error;
      }

      public string executeUdp( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters )
      {
         execute(out aP0_ConnectionParameters, out aP1_error);
         return AV14error ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ,
                                 out string aP1_error )
      {
         this.AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography") ;
         this.AV14error = "" ;
         SubmitImpl();
         aP0_ConnectionParameters=this.AV9ConnectionParameters;
         aP1_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9ConnectionParameters.FromJSonString(AV16websession.Get("NOSTR_CONFIG_CONNECTION"), null);
         if ( AV9ConnectionParameters.Count == 0 )
         {
            AV15nostrFileName = "nostr.conf";
            AV9ConnectionParameters.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  StringUtil.Trim( AV15nostrFileName), out  AV14error), null);
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
         AV9ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV14error = "";
         AV16websession = context.GetSession();
         AV15nostrFileName = "";
         /* GeneXus formulas. */
      }

      private string AV14error ;
      private string AV15nostrFileName ;
      private IGxSession AV16websession ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV9ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> aP0_ConnectionParameters ;
      private string aP1_error ;
   }

}
