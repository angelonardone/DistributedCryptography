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
   public class createshares : GXProcedure
   {
      public createshares( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createshares( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_secret ,
                           short aP1_totalShares ,
                           short aP2_minimumShares ,
                           out GxSimpleCollection<string> aP3_shares ,
                           ref string aP4_error )
      {
         this.AV8secret = aP0_secret;
         this.AV9totalShares = aP1_totalShares;
         this.AV10minimumShares = aP2_minimumShares;
         this.AV13shares = new GxSimpleCollection<string>() ;
         this.AV11error = aP4_error;
         initialize();
         ExecuteImpl();
         aP3_shares=this.AV13shares;
         aP4_error=this.AV11error;
      }

      public string executeUdp( string aP0_secret ,
                                short aP1_totalShares ,
                                short aP2_minimumShares ,
                                out GxSimpleCollection<string> aP3_shares )
      {
         execute(aP0_secret, aP1_totalShares, aP2_minimumShares, out aP3_shares, ref aP4_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_secret ,
                                 short aP1_totalShares ,
                                 short aP2_minimumShares ,
                                 out GxSimpleCollection<string> aP3_shares ,
                                 ref string aP4_error )
      {
         this.AV8secret = aP0_secret;
         this.AV9totalShares = aP1_totalShares;
         this.AV10minimumShares = aP2_minimumShares;
         this.AV13shares = new GxSimpleCollection<string>() ;
         this.AV11error = aP4_error;
         SubmitImpl();
         aP3_shares=this.AV13shares;
         aP4_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          try
         /* User Code */
          {
         GXt_char1 = AV11error;
         new GeneXus.Programs.nbitcoin.sha256(context ).execute(  StringUtil.Trim( AV8secret), out  AV16sha, out  GXt_char1) ;
         AV11error = GXt_char1;
         AV15updatedSecret = StringUtil.Substring( AV16sha, 1, 4) + StringUtil.Trim( AV8secret);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            /* User Code */
             var gcd = new SecretSharingDotNet.Math.ExtendedEuclideanAlgorithm<System.Numerics.BigInteger>();
            /* User Code */
             var split = new SecretSharingDotNet.Cryptography.ShamirsSecretSharing<System.Numerics.BigInteger>(gcd);
            /* User Code */
             string password = AV15updatedSecret;
            /* User Code */
             int minnum = AV10minimumShares;
            /* User Code */
             int totnum = AV9totalShares;
            /* User Code */
             var shares = split.MakeShares(minnum, totnum, password);
            /* User Code */
             for (int i = 0; i < shares.Count; i++)
            /* User Code */
             {
            /* User Code */
             AV12share = shares[i].ToString();
            AV13shares.Add(AV12share, 0);
            /* User Code */
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
         AV13shares = new GxSimpleCollection<string>();
         GXt_char1 = "";
         AV16sha = "";
         AV15updatedSecret = "";
         AV12share = "";
         /* GeneXus formulas. */
      }

      private short AV9totalShares ;
      private short AV10minimumShares ;
      private string AV11error ;
      private string GXt_char1 ;
      private string AV16sha ;
      private string AV8secret ;
      private string AV15updatedSecret ;
      private string AV12share ;
      private GxSimpleCollection<string> AV13shares ;
      private string aP4_error ;
      private GxSimpleCollection<string> aP3_shares ;
   }

}
