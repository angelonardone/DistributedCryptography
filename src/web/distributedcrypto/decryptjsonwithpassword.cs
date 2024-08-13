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
namespace GeneXus.Programs.distributedcrypto {
   public class decryptjsonwithpassword : GXProcedure
   {
      public decryptjsonwithpassword( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptjsonwithpassword( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptionKey ,
                           string aP2_IV ,
                           out string aP3_clearText ,
                           out string aP4_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV11encryptionKey = aP1_encryptionKey;
         this.AV13IV = aP2_IV;
         this.AV8clearText = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP3_clearText=this.AV8clearText;
         aP4_error=this.AV12error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptionKey ,
                                string aP2_IV ,
                                out string aP3_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptionKey, aP2_IV, out aP3_clearText, out aP4_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptionKey ,
                                 string aP2_IV ,
                                 out string aP3_clearText ,
                                 out string aP4_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV11encryptionKey = aP1_encryptionKey;
         this.AV13IV = aP2_IV;
         this.AV8clearText = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP3_clearText=this.AV8clearText;
         aP4_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV12error;
         new GeneXus.Programs.distributedcrypto.aesdecryptstring(context ).execute(  AV10encryptedText, out  AV8clearText,  AV11encryptionKey,  AV13IV, out  GXt_char1) ;
         AV12error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
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
         AV8clearText = "";
         AV12error = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV11encryptionKey ;
      private string AV13IV ;
      private string AV12error ;
      private string GXt_char1 ;
      private string AV10encryptedText ;
      private string AV8clearText ;
      private string aP3_clearText ;
      private string aP4_error ;
   }

}
