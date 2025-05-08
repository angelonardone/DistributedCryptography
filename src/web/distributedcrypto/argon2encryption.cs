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
   public class argon2encryption : GXProcedure
   {
      public argon2encryption( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public argon2encryption( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_encryptAction ,
                           string aP1_password ,
                           string aP2_inText ,
                           out string aP3_outText ,
                           ref string aP4_error )
      {
         this.AV11encryptAction = aP0_encryptAction;
         this.AV8password = aP1_password;
         this.AV9inText = aP2_inText;
         this.AV10outText = "" ;
         this.AV12error = aP4_error;
         initialize();
         ExecuteImpl();
         aP3_outText=this.AV10outText;
         aP4_error=this.AV12error;
      }

      public string executeUdp( short aP0_encryptAction ,
                                string aP1_password ,
                                string aP2_inText ,
                                out string aP3_outText )
      {
         execute(aP0_encryptAction, aP1_password, aP2_inText, out aP3_outText, ref aP4_error);
         return AV12error ;
      }

      public void executeSubmit( short aP0_encryptAction ,
                                 string aP1_password ,
                                 string aP2_inText ,
                                 out string aP3_outText ,
                                 ref string aP4_error )
      {
         this.AV11encryptAction = aP0_encryptAction;
         this.AV8password = aP1_password;
         this.AV9inText = aP2_inText;
         this.AV10outText = "" ;
         this.AV12error = aP4_error;
         SubmitImpl();
         aP3_outText=this.AV10outText;
         aP4_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
             try
         /* User Code */
             {
         if ( AV11encryptAction == 10 )
         {
            /* User Code */
            	AV10outText = EncryptText(AV9inText, AV8password);
         }
         else if ( AV11encryptAction == 20 )
         {
            /* User Code */
            	AV10outText = DecryptText(AV9inText, AV8password);
         }
         else
         {
            AV12error = "There is no such command for Argon2";
         }
         /* User Code */
             }catch (Exception ex)
         /* User Code */
             {
         /* User Code */
          		AV12error = ex.Message.ToString();
         /* User Code */
             }
         /* User Code */
         	static string EncryptText(string text, string password)
         /* User Code */
         	{
         /* User Code */
         		if (text == null || password == null)
         /* User Code */
         		{
         /* User Code */
         			throw new ArgumentNullException(text == null ? nameof(text) : nameof(password));
         /* User Code */
         		}
         /* User Code */
         		byte[] salt = GenerateRandomBytes(16);
         /* User Code */
         		byte[] nonce = GenerateRandomBytes(12);
         /* User Code */
         		byte[] key = DeriveKey(password, salt);
         /* User Code */
         		byte[] plaintext = System.Text.Encoding.UTF8.GetBytes(text);
         /* User Code */
         		byte[] ciphertext = new byte[plaintext.Length];
         /* User Code */
         		byte[] tag = new byte[16];
         /* User Code */
         		using (System.Security.Cryptography.AesGcm aesGcm = new System.Security.Cryptography.AesGcm(key, tag.Length))
         /* User Code */
         		{
         /* User Code */
         			aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);
         /* User Code */
         		}
         /* User Code */
         		byte[] combined = new byte[16 + 12 + ciphertext.Length + 16];
         /* User Code */
         		System.Buffer.BlockCopy(salt, 0, combined, 0, 16);
         /* User Code */
         		System.Buffer.BlockCopy(nonce, 0, combined, 16, 12);
         /* User Code */
         		System.Buffer.BlockCopy(ciphertext, 0, combined, 16 + 12, ciphertext.Length);
         /* User Code */
         		System.Buffer.BlockCopy(tag, 0, combined, 16 + 12 + ciphertext.Length, 16);
         /* User Code */
         		return System.Convert.ToBase64String(combined);
         /* User Code */
         	}
         /* User Code */
         	static string DecryptText(string encryptedBase64, string password)
         /* User Code */
         	{
         /* User Code */
         		if (encryptedBase64 == null || password == null)
         /* User Code */
         		{
         /* User Code */
         			throw new ArgumentNullException(encryptedBase64 == null ? nameof(encryptedBase64) : nameof(password));
         /* User Code */
         		}
         /* User Code */
         		byte[] combined = System.Convert.FromBase64String(encryptedBase64);
         /* User Code */
         		if (combined.Length < 16 + 12 + 16)
         /* User Code */
         		{
         /* User Code */
         			throw new ArgumentException("Encrypted data is too short to contain salt, nonce, and tag.");
         /* User Code */
         		}
         /* User Code */
         		byte[] salt = new byte[16];
         /* User Code */
         		byte[] nonce = new byte[12];
         /* User Code */
         		byte[] ciphertext = new byte[combined.Length - 16 - 12 - 16];
         /* User Code */
         		byte[] tag = new byte[16];
         /* User Code */
         		System.Buffer.BlockCopy(combined, 0, salt, 0, 16);
         /* User Code */
         		System.Buffer.BlockCopy(combined, 16, nonce, 0, 12);
         /* User Code */
         		System.Buffer.BlockCopy(combined, 16 + 12, ciphertext, 0, ciphertext.Length);
         /* User Code */
         		System.Buffer.BlockCopy(combined, 16 + 12 + ciphertext.Length, tag, 0, 16);
         /* User Code */
         		byte[] key = DeriveKey(password, salt);
         /* User Code */
         		byte[] plaintext = new byte[ciphertext.Length];
         /* User Code */
         		using (System.Security.Cryptography.AesGcm aesGcm = new System.Security.Cryptography.AesGcm(key, tag.Length))
         /* User Code */
         		{
         /* User Code */
         			aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);
         /* User Code */
         		}
         /* User Code */
         		return System.Text.Encoding.UTF8.GetString(plaintext);
         /* User Code */
         	}
         /* User Code */
         	static byte[] DeriveKey(string password, byte[] salt)
         /* User Code */
         	{
         /* User Code */
         		using (Konscious.Security.Cryptography.Argon2id argon2 = new Konscious.Security.Cryptography.Argon2id(System.Text.Encoding.UTF8.GetBytes(password)))
         /* User Code */
         		{
         /* User Code */
         			argon2.Salt = salt;
         /* User Code */
         			argon2.DegreeOfParallelism = 8;
         /* User Code */
         			argon2.Iterations = 10;
         /* User Code */
         			argon2.MemorySize = 65536 * 10;
         /* User Code */
         			return argon2.GetBytes(32);
         /* User Code */
         		}
         /* User Code */
         	}
         /* User Code */
         	static byte[] GenerateRandomBytes(int length)
         /* User Code */
         	{
         /* User Code */
         		byte[] bytes = new byte[length];
         /* User Code */
         		using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
         /* User Code */
         		{
         /* User Code */
         			rng.GetBytes(bytes);
         /* User Code */
         		}
         /* User Code */
         		return bytes;
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
         AV10outText = "";
         /* GeneXus formulas. */
      }

      private short AV11encryptAction ;
      private string AV8password ;
      private string AV9inText ;
      private string AV10outText ;
      private string AV12error ;
      private string aP4_error ;
      private string aP3_outText ;
   }

}
