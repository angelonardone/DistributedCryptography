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
namespace GeneXus.Programs.nbitcoin {
   public class sha512 : GXProcedure
   {
      public sha512( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sha512( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_textToSha ,
                           out string aP1_sha ,
                           out string aP2_error )
      {
         this.AV10textToSha = aP0_textToSha;
         this.AV11sha = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_sha=this.AV11sha;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_textToSha ,
                                out string aP1_sha )
      {
         execute(aP0_textToSha, out aP1_sha, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_textToSha ,
                                 out string aP1_sha ,
                                 out string aP2_error )
      {
         this.AV10textToSha = aP0_textToSha;
         this.AV11sha = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_sha=this.AV11sha;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          string text = AV10textToSha;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(text);
         /* User Code */
          var HashedText =  NBitcoin.Crypto.Hashes.SHA512(bytes);
         /* User Code */
          string val = System.Convert.ToHexString(HashedText);
         /* User Code */
          	AV11sha = val;
         /* User Code */
         	}
         /* User Code */
         	catch (Exception ex)
         /* User Code */
         	{
         /* User Code */
         		AV8error = ex.Message.ToString();
         /* User Code */
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
         AV11sha = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV11sha ;
      private string AV8error ;
      private string AV10textToSha ;
      private string aP1_sha ;
      private string aP2_error ;
   }

}
