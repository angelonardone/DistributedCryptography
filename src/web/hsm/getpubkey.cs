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
   public class getpubkey : GXProcedure
   {
      public getpubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getpubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_index ,
                           out string aP1_pubKey ,
                           out string aP2_error )
      {
         this.AV8index = aP0_index;
         this.AV9pubKey = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_pubKey=this.AV9pubKey;
         aP2_error=this.AV11error;
      }

      public string executeUdp( short aP0_index ,
                                out string aP1_pubKey )
      {
         execute(aP0_index, out aP1_pubKey, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( short aP0_index ,
                                 out string aP1_pubKey ,
                                 out string aP2_error )
      {
         this.AV8index = aP0_index;
         this.AV9pubKey = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_pubKey=this.AV9pubKey;
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9pubKey = AV10HsmManager.getpublickey(AV8index);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9pubKey)) )
         {
            AV11error = AV10HsmManager.getlasterror();
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
         AV9pubKey = "";
         AV11error = "";
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private short AV8index ;
      private string AV9pubKey ;
      private string AV11error ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP1_pubKey ;
      private string aP2_error ;
   }

}
