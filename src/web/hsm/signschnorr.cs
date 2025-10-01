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
   public class signschnorr : GXProcedure
   {
      public signschnorr( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signschnorr( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_message ,
                           short aP1_index ,
                           out string aP2_signature ,
                           out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV11index = aP1_index;
         this.AV14signature = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_signature=this.AV14signature;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_message ,
                                short aP1_index ,
                                out string aP2_signature )
      {
         execute(aP0_message, aP1_index, out aP2_signature, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_message ,
                                 short aP1_index ,
                                 out string aP2_signature ,
                                 out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV11index = aP1_index;
         this.AV14signature = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_signature=this.AV14signature;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14signature = AV10HsmManager.signschnorr(AV12message, AV11index);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14signature)) )
         {
            AV9error = AV10HsmManager.getlasterror();
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
         AV14signature = "";
         AV9error = "";
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private short AV11index ;
      private string AV14signature ;
      private string AV9error ;
      private string AV12message ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP2_signature ;
      private string aP3_error ;
   }

}
