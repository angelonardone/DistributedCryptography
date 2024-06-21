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
namespace GeneXus.Programs.shamirss {
   public class combineshares : GXProcedure
   {
      public combineshares( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public combineshares( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_shares ,
                           out string aP1_secret ,
                           out string aP2_error )
      {
         this.AV13shares = aP0_shares;
         this.AV8secret = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_secret=this.AV8secret;
         aP2_error=this.AV11error;
      }

      public string executeUdp( GxSimpleCollection<string> aP0_shares ,
                                out string aP1_secret )
      {
         execute(aP0_shares, out aP1_secret, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_shares ,
                                 out string aP1_secret ,
                                 out string aP2_error )
      {
         this.AV13shares = aP0_shares;
         this.AV8secret = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_secret=this.AV8secret;
         aP2_error=this.AV11error;
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
          var gcd = new SecretSharingDotNet.Math.ExtendedEuclideanAlgorithm<System.Numerics.BigInteger>();
         /* User Code */
          var combine = new SecretSharingDotNet.Cryptography.ShamirsSecretSharing<System.Numerics.BigInteger>(gcd);
         /* User Code */
          string shares = "";
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV13shares.Count )
         {
            AV12share = ((string)AV13shares.Item(AV17GXV1));
            /* User Code */
             shares += AV12share + System.Environment.NewLine;
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         /* User Code */
          var recoveredSecret = combine.Reconstruction(shares);
         /* User Code */
          AV16updatedSecret = recoveredSecret.ToString();
         AV15shamir_input.FromJSonString(AV16updatedSecret, null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15shamir_input.gxTpr_Secret)) )
         {
            AV8secret = "";
         }
         else
         {
            GXt_char1 = AV11error;
            new GeneXus.Programs.nbitcoin.sha256(context ).execute(  StringUtil.Trim( AV15shamir_input.gxTpr_Secret), out  AV14sha, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( StringUtil.StrCmp(AV15shamir_input.gxTpr_Sha256, StringUtil.Trim( AV14sha)) == 0 )
            {
               AV8secret = StringUtil.Trim( AV15shamir_input.gxTpr_Secret);
            }
            else
            {
               AV8secret = "";
            }
         }
         /* User Code */
         	}
         /* User Code */
         	catch (Exception ex)
         /* User Code */
         	{
         /* User Code */
         		AV11error = ex.Message.ToString();
         /* User Code */
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
         AV8secret = "";
         AV11error = "";
         AV12share = "";
         AV16updatedSecret = "";
         AV15shamir_input = new GeneXus.Programs.shamirss.SdtShamir_input_SDT(context);
         GXt_char1 = "";
         AV14sha = "";
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private string AV11error ;
      private string GXt_char1 ;
      private string AV14sha ;
      private string AV8secret ;
      private string AV12share ;
      private string AV16updatedSecret ;
      private string aP1_secret ;
      private string aP2_error ;
      private GxSimpleCollection<string> AV13shares ;
      private GeneXus.Programs.shamirss.SdtShamir_input_SDT AV15shamir_input ;
   }

}
