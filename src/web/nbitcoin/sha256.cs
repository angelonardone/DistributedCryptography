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
namespace GeneXus.Programs.nbitcoin {
   public class sha256 : GXProcedure
   {
      public sha256( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sha256( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_textToSha ,
                           out string aP1_sha ,
                           out string aP2_error )
      {
         this.AV14textToSha = aP0_textToSha;
         this.AV15sha = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_sha=this.AV15sha;
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_textToSha ,
                                out string aP1_sha )
      {
         execute(aP0_textToSha, out aP1_sha, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_textToSha ,
                                 out string aP1_sha ,
                                 out string aP2_error )
      {
         this.AV14textToSha = aP0_textToSha;
         this.AV15sha = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_sha=this.AV15sha;
         aP2_error=this.AV9error;
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
          string brain_text = AV14textToSha;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(brain_text);
         /* User Code */
          var HashedText =  NBitcoin.Crypto.Hashes.SHA256(bytes);
         /* User Code */
          NBitcoin.Key privateKey = new NBitcoin.Key(HashedText);
         /* User Code */
          string val = privateKey.ToHex().ToString();
         /* User Code */
          	AV13val = val;
         AV15sha = AV13val;
         /* User Code */
         	}
         /* User Code */
         	catch (Exception ex)
         /* User Code */
         	{
         /* User Code */
         		AV9error = ex.Message.ToString();
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
         AV15sha = "";
         AV9error = "";
         AV13val = "";
         /* GeneXus formulas. */
      }

      private string AV15sha ;
      private string AV9error ;
      private string AV13val ;
      private string AV14textToSha ;
      private string aP1_sha ;
      private string aP2_error ;
   }

}
