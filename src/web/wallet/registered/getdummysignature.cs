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
namespace GeneXus.Programs.wallet.registered {
   public class getdummysignature : GXProcedure
   {
      public getdummysignature( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getdummysignature( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_signature )
      {
         this.AV8signature = "" ;
         initialize();
         ExecuteImpl();
         aP0_signature=this.AV8signature;
      }

      public string executeUdp( )
      {
         execute(out aP0_signature);
         return AV8signature ;
      }

      public void executeSubmit( out string aP0_signature )
      {
         this.AV8signature = "" ;
         SubmitImpl();
         aP0_signature=this.AV8signature;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8signature = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364140FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364140";
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
         AV8signature = "";
         /* GeneXus formulas. */
      }

      private string AV8signature ;
      private string aP0_signature ;
   }

}
