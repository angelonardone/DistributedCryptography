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
namespace GeneXus.Programs.distcrypt {
   public class decryptwithgroupskey : GXProcedure
   {
      public decryptwithgroupskey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptwithgroupskey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encryptedText ,
                           string aP1_encryptedKey ,
                           out string aP2_clearText ,
                           out string aP3_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV9encryptedKey = aP1_encryptedKey;
         this.AV8clearText = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV11error;
      }

      public string executeUdp( string aP0_encryptedText ,
                                string aP1_encryptedKey ,
                                out string aP2_clearText )
      {
         execute(aP0_encryptedText, aP1_encryptedKey, out aP2_clearText, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_encryptedText ,
                                 string aP1_encryptedKey ,
                                 out string aP2_clearText ,
                                 out string aP3_error )
      {
         this.AV10encryptedText = aP0_encryptedText;
         this.AV9encryptedKey = aP1_encryptedKey;
         this.AV8clearText = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_clearText=this.AV8clearText;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV12externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV12externalUser = GXt_SdtExternalUser1;
         GXt_char2 = AV11error;
         new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV10encryptedText,  AV9encryptedKey,  AV12externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV8clearText, out  GXt_char2) ;
         AV11error = GXt_char2;
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
         AV11error = "";
         AV12externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV9encryptedKey ;
      private string AV11error ;
      private string GXt_char2 ;
      private string AV10encryptedText ;
      private string AV8clearText ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV12externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP2_clearText ;
      private string aP3_error ;
   }

}
