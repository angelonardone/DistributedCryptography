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
namespace GeneXus.Programs.distributedcrypto {
   public class argon2derivekey512 : GXProcedure
   {
      public argon2derivekey512( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public argon2derivekey512( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           string aP1_salt ,
                           out string aP2_ExtendedKeySeed ,
                           out string aP3_error )
      {
         this.AV8password = aP0_password;
         this.AV13salt = aP1_salt;
         this.AV14ExtendedKeySeed = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP2_ExtendedKeySeed=this.AV14ExtendedKeySeed;
         aP3_error=this.AV12error;
      }

      public string executeUdp( string aP0_password ,
                                string aP1_salt ,
                                out string aP2_ExtendedKeySeed )
      {
         execute(aP0_password, aP1_salt, out aP2_ExtendedKeySeed, out aP3_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_password ,
                                 string aP1_salt ,
                                 out string aP2_ExtendedKeySeed ,
                                 out string aP3_error )
      {
         this.AV8password = aP0_password;
         this.AV13salt = aP1_salt;
         this.AV14ExtendedKeySeed = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP2_ExtendedKeySeed=this.AV14ExtendedKeySeed;
         aP3_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
             try
         /* User Code */
             {
         GXt_char1 = AV12error;
         new GeneXus.Programs.nbitcoin.sha256(context ).execute(  AV13salt, out  AV15shaSalt, out  GXt_char1) ;
         AV12error = GXt_char1;
         /* User Code */
          var seed = DeriveKey(AV8password, AV15shaSalt);
         /* User Code */
          AV14ExtendedKeySeed = seed;
         /* User Code */
             }catch (Exception ex)
         /* User Code */
             {
         /* User Code */
          		AV12error = ex.Message.ToString();
         /* User Code */
             }
         /* User Code */
         	static string DeriveKey(string password, string salt)
         /* User Code */
         	{
         /* User Code */
         		using (Konscious.Security.Cryptography.Argon2id argon2 = new Konscious.Security.Cryptography.Argon2id(System.Text.Encoding.UTF8.GetBytes(password)))
         /* User Code */
         		{
         /* User Code */
         			argon2.Salt = System.Text.Encoding.UTF8.GetBytes(salt);
         /* User Code */
         			argon2.DegreeOfParallelism = 8;
         /* User Code */
         			argon2.Iterations = 10;
         /* User Code */
         			argon2.MemorySize = 65536 * 10;
         /* User Code */
         			return System.Convert.ToHexString(argon2.GetBytes(64));
         /* User Code */
         		}
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
         AV14ExtendedKeySeed = "";
         AV12error = "";
         GXt_char1 = "";
         AV15shaSalt = "";
         /* GeneXus formulas. */
      }

      private string AV8password ;
      private string AV14ExtendedKeySeed ;
      private string AV12error ;
      private string GXt_char1 ;
      private string AV15shaSalt ;
      private string AV13salt ;
      private string aP2_ExtendedKeySeed ;
      private string aP3_error ;
   }

}
