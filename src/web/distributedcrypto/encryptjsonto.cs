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
   public class encryptjsonto : GXProcedure
   {
      public encryptjsonto( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptjsonto( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_clearText ,
                           string aP1_pubKey ,
                           out string aP2_encryptedKey ,
                           out string aP3_IV ,
                           out string aP4_encryptedText ,
                           out string aP5_error )
      {
         this.AV12clearText = aP0_clearText;
         this.AV15pubKey = aP1_pubKey;
         this.AV14encryptedKey = "" ;
         this.AV11IV = "" ;
         this.AV13encryptedText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptedKey=this.AV14encryptedKey;
         aP3_IV=this.AV11IV;
         aP4_encryptedText=this.AV13encryptedText;
         aP5_error=this.AV9error;
      }

      public string executeUdp( string aP0_clearText ,
                                string aP1_pubKey ,
                                out string aP2_encryptedKey ,
                                out string aP3_IV ,
                                out string aP4_encryptedText )
      {
         execute(aP0_clearText, aP1_pubKey, out aP2_encryptedKey, out aP3_IV, out aP4_encryptedText, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_clearText ,
                                 string aP1_pubKey ,
                                 out string aP2_encryptedKey ,
                                 out string aP3_IV ,
                                 out string aP4_encryptedText ,
                                 out string aP5_error )
      {
         this.AV12clearText = aP0_clearText;
         this.AV15pubKey = aP1_pubKey;
         this.AV14encryptedKey = "" ;
         this.AV11IV = "" ;
         this.AV13encryptedText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_encryptedKey=this.AV14encryptedKey;
         aP3_IV=this.AV11IV;
         aP4_encryptedText=this.AV13encryptedText;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.distributedcrypto.aesencryptstring(context ).execute(  AV12clearText,  "", out  AV13encryptedText, out  AV8encryptionKey, out  AV11IV, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  AV15pubKey,  AV8encryptionKey, out  AV14encryptedKey, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
            }
         }
         this.cleanup();
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
         AV14encryptedKey = "";
         AV11IV = "";
         AV13encryptedText = "";
         AV9error = "";
         AV8encryptionKey = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV15pubKey ;
      private string AV14encryptedKey ;
      private string AV11IV ;
      private string AV9error ;
      private string AV8encryptionKey ;
      private string GXt_char1 ;
      private string AV12clearText ;
      private string AV13encryptedText ;
      private string aP2_encryptedKey ;
      private string aP3_IV ;
      private string aP4_encryptedText ;
      private string aP5_error ;
   }

}
