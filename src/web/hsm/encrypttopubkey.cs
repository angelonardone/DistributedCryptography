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
   public class encrypttopubkey : GXProcedure
   {
      public encrypttopubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encrypttopubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_message ,
                           string aP1_pubKey ,
                           out string aP2_encryptedMessage ,
                           out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV13pubKey = aP1_pubKey;
         this.AV8encryptedMessage = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptedMessage=this.AV8encryptedMessage;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_message ,
                                string aP1_pubKey ,
                                out string aP2_encryptedMessage )
      {
         execute(aP0_message, aP1_pubKey, out aP2_encryptedMessage, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_message ,
                                 string aP1_pubKey ,
                                 out string aP2_encryptedMessage ,
                                 out string aP3_error )
      {
         this.AV12message = aP0_message;
         this.AV13pubKey = aP1_pubKey;
         this.AV8encryptedMessage = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_encryptedMessage=this.AV8encryptedMessage;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8encryptedMessage = AV10HsmManager.encrypttopubkey(AV12message, AV13pubKey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8encryptedMessage)) )
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
         AV8encryptedMessage = "";
         AV9error = "";
         AV10HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private string AV13pubKey ;
      private string AV9error ;
      private string AV12message ;
      private string AV8encryptedMessage ;
      private GeneXus.Programs.hsm.SdtHsmManager AV10HsmManager ;
      private string aP2_encryptedMessage ;
      private string aP3_error ;
   }

}
