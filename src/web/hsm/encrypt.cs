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
   public class encrypt : GXProcedure
   {
      public encrypt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encrypt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_message ,
                           short aP1_index ,
                           out string aP2_encryptedMessage ,
                           out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV10index = aP1_index;
         this.AV13encryptedMessage = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptedMessage=this.AV13encryptedMessage;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_message ,
                                short aP1_index ,
                                out string aP2_encryptedMessage )
      {
         execute(aP0_message, aP1_index, out aP2_encryptedMessage, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_message ,
                                 short aP1_index ,
                                 out string aP2_encryptedMessage ,
                                 out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV10index = aP1_index;
         this.AV13encryptedMessage = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_encryptedMessage=this.AV13encryptedMessage;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13encryptedMessage = AV9HsmManager.encrypt(AV12message, AV10index);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13encryptedMessage)) )
         {
            AV8error = AV9HsmManager.getlasterror();
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
         AV13encryptedMessage = "";
         AV8error = "";
         AV9HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private short AV10index ;
      private string AV8error ;
      private string AV12message ;
      private string AV13encryptedMessage ;
      private GeneXus.Programs.hsm.SdtHsmManager AV9HsmManager ;
      private string aP2_encryptedMessage ;
      private string aP3_error ;
   }

}
