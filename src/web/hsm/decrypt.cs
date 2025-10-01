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
   public class decrypt : GXProcedure
   {
      public decrypt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decrypt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedMessage ,
                           short aP1_index ,
                           out string aP2_message ,
                           out string aP3_error )
      {
         this.AV8encryptedMessage = aP0_encryptedMessage;
         this.AV11index = aP1_index;
         this.AV12message = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_message=this.AV12message;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_encryptedMessage ,
                                short aP1_index ,
                                out string aP2_message )
      {
         execute(aP0_encryptedMessage, aP1_index, out aP2_message, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_encryptedMessage ,
                                 short aP1_index ,
                                 out string aP2_message ,
                                 out string aP3_error )
      {
         this.AV8encryptedMessage = aP0_encryptedMessage;
         this.AV11index = aP1_index;
         this.AV12message = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_message=this.AV12message;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12message = AV10HsmManager.decrypt(AV8encryptedMessage, AV11index);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12message)) )
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
         AV12message = "";
         AV9error = "";
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private short AV11index ;
      private string AV9error ;
      private string AV8encryptedMessage ;
      private string AV12message ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP2_message ;
      private string aP3_error ;
   }

}
