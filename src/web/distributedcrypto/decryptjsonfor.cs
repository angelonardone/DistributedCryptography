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
   public class decryptjsonfor : GXProcedure
   {
      public decryptjsonfor( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptjsonfor( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptedKey ,
                           string aP2_IV ,
                           string aP3_privateKey ,
                           out string aP4_clearText ,
                           out string aP5_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV11IV = aP2_IV;
         this.AV15privateKey = aP3_privateKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP4_clearText=this.AV12clearText;
         aP5_error=this.AV9error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptedKey ,
                                string aP2_IV ,
                                string aP3_privateKey ,
                                out string aP4_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptedKey, aP2_IV, aP3_privateKey, out aP4_clearText, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptedKey ,
                                 string aP2_IV ,
                                 string aP3_privateKey ,
                                 out string aP4_clearText ,
                                 out string aP5_error )
      {
         this.AV13encryptedText = aP0_encryptedText;
         this.AV14encryptedKey = aP1_encryptedKey;
         this.AV11IV = aP2_IV;
         this.AV15privateKey = aP3_privateKey;
         this.AV12clearText = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP4_clearText=this.AV12clearText;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV15privateKey,  AV14encryptedKey, out  AV8encryptionKey, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.distributedcrypto.aesdecryptstring(context ).execute(  AV13encryptedText, out  AV12clearText,  AV8encryptionKey,  AV11IV, out  GXt_char1) ;
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
         AV12clearText = "";
         AV9error = "";
         AV8encryptionKey = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV14encryptedKey ;
      private string AV11IV ;
      private string AV15privateKey ;
      private string AV9error ;
      private string AV8encryptionKey ;
      private string GXt_char1 ;
      private string AV13encryptedText ;
      private string AV12clearText ;
      private string aP4_clearText ;
      private string aP5_error ;
   }

}
