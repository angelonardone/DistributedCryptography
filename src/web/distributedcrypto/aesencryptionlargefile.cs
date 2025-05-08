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
   public class aesencryptionlargefile : GXProcedure
   {
      public aesencryptionlargefile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesencryptionlargefile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_encryptAction ,
                           string aP1_inputFile ,
                           string aP2_outputFile ,
                           string aP3_PubKey ,
                           string aP4_privateKey ,
                           ref string aP5_originalFileName ,
                           ref string aP6_ivBase64 ,
                           ref string aP7_hmacBase64 ,
                           ref string aP8_encryptedKeyBase64 ,
                           out string aP9_error )
      {
         this.AV8encryptAction = aP0_encryptAction;
         this.AV10inputFile = aP1_inputFile;
         this.AV11outputFile = aP2_outputFile;
         this.AV12PubKey = aP3_PubKey;
         this.AV13privateKey = aP4_privateKey;
         this.AV17originalFileName = aP5_originalFileName;
         this.AV14ivBase64 = aP6_ivBase64;
         this.AV15hmacBase64 = aP7_hmacBase64;
         this.AV16encryptedKeyBase64 = aP8_encryptedKeyBase64;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP5_originalFileName=this.AV17originalFileName;
         aP6_ivBase64=this.AV14ivBase64;
         aP7_hmacBase64=this.AV15hmacBase64;
         aP8_encryptedKeyBase64=this.AV16encryptedKeyBase64;
         aP9_error=this.AV9error;
      }

      public string executeUdp( short aP0_encryptAction ,
                                string aP1_inputFile ,
                                string aP2_outputFile ,
                                string aP3_PubKey ,
                                string aP4_privateKey ,
                                ref string aP5_originalFileName ,
                                ref string aP6_ivBase64 ,
                                ref string aP7_hmacBase64 ,
                                ref string aP8_encryptedKeyBase64 )
      {
         execute(aP0_encryptAction, aP1_inputFile, aP2_outputFile, aP3_PubKey, aP4_privateKey, ref aP5_originalFileName, ref aP6_ivBase64, ref aP7_hmacBase64, ref aP8_encryptedKeyBase64, out aP9_error);
         return AV9error ;
      }

      public void executeSubmit( short aP0_encryptAction ,
                                 string aP1_inputFile ,
                                 string aP2_outputFile ,
                                 string aP3_PubKey ,
                                 string aP4_privateKey ,
                                 ref string aP5_originalFileName ,
                                 ref string aP6_ivBase64 ,
                                 ref string aP7_hmacBase64 ,
                                 ref string aP8_encryptedKeyBase64 ,
                                 out string aP9_error )
      {
         this.AV8encryptAction = aP0_encryptAction;
         this.AV10inputFile = aP1_inputFile;
         this.AV11outputFile = aP2_outputFile;
         this.AV12PubKey = aP3_PubKey;
         this.AV13privateKey = aP4_privateKey;
         this.AV17originalFileName = aP5_originalFileName;
         this.AV14ivBase64 = aP6_ivBase64;
         this.AV15hmacBase64 = aP7_hmacBase64;
         this.AV16encryptedKeyBase64 = aP8_encryptedKeyBase64;
         this.AV9error = "" ;
         SubmitImpl();
         aP5_originalFileName=this.AV17originalFileName;
         aP6_ivBase64=this.AV14ivBase64;
         aP7_hmacBase64=this.AV15hmacBase64;
         aP8_encryptedKeyBase64=this.AV16encryptedKeyBase64;
         aP9_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
             try
         /* User Code */
             {
         if ( AV8encryptAction == 10 )
         {
            /* User Code */
             string hexPubKey = AV12PubKey;
            /* User Code */
             byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPubKey);
            /* User Code */
             NBitcoin.PubKey publicKey = new NBitcoin.PubKey(bytes);
            /* User Code */
             var (ivBase64, hmacBase64, encryptedKeyBase64) = EncryptFileStreaming(AV17originalFileName, AV10inputFile, AV11outputFile, publicKey);
            /* User Code */
             AV14ivBase64 =  ivBase64;
            /* User Code */
             AV15hmacBase64 =  hmacBase64;
            /* User Code */
             AV16encryptedKeyBase64 = encryptedKeyBase64;
         }
         else if ( AV8encryptAction == 20 )
         {
            /* User Code */
             string hexPrivateKey = AV13privateKey;
            /* User Code */
             byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
            /* User Code */
             NBitcoin.Key privateKey = new NBitcoin.Key(bytes);
            /* User Code */
             System.String originalName = DecryptFileStreaming(AV10inputFile, AV11outputFile, privateKey, AV14ivBase64, AV15hmacBase64, AV16encryptedKeyBase64);
            /* User Code */
             AV17originalFileName = originalName;
         }
         else
         {
            AV9error = "There is no such command for Large File Encryption/Decryption";
         }
         /* User Code */
             }catch (Exception ex)
         /* User Code */
             {
         /* User Code */
          		AV9error = ex.Message.ToString();
         /* User Code */
             }
         /* User Code */
         	static (System.String ivBase64, System.String hmacBase64, System.String encryptedKeyBase64) EncryptFileStreaming(System.String originalName, System.String inputFilePath, System.String outputFilePath, NBitcoin.PubKey publicKey)
         /* User Code */
         	{
         /* User Code */
         		System.Byte[] aesKey = new System.Byte[32];
         /* User Code */
         		System.Byte[] hmacKey = new System.Byte[32];
         /* User Code */
         		using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
         /* User Code */
         		{
         /* User Code */
         			rng.GetBytes(aesKey);
         /* User Code */
         			rng.GetBytes(hmacKey);
         /* User Code */
         		}
         /* User Code */
         		System.Byte[] combinedKey = new System.Byte[64];
         /* User Code */
         		System.Buffer.BlockCopy(aesKey, 0, combinedKey, 0, 32);
         /* User Code */
         		System.Buffer.BlockCopy(hmacKey, 0, combinedKey, 32, 32);
         /* User Code */
         		System.Byte[] encryptedKey = publicKey.Encrypt(combinedKey);
         /* User Code */
         		System.Byte[] iv = new System.Byte[16];
         /* User Code */
         		using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
         /* User Code */
         		{
         /* User Code */
         			rng.GetBytes(iv);
         /* User Code */
         		}
         /* User Code */
         		System.Byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(originalName);
         /* User Code */
         		System.String tempFilePath = outputFilePath + ".tmp";
         /* User Code */
         		try
         /* User Code */
         		{
         /* User Code */
         			using (var tempStream = new System.IO.FileStream(tempFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         /* User Code */
         			using (var aes = System.Security.Cryptography.Aes.Create())
         /* User Code */
         			{
         /* User Code */
         				aes.Key = aesKey;
         /* User Code */
         				aes.IV = iv;
         /* User Code */
         				aes.Mode = System.Security.Cryptography.CipherMode.CBC;
         /* User Code */
         				aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
         /* User Code */
         				using (var encryptor = aes.CreateEncryptor())
         /* User Code */
         				using (var cryptoStream = new System.Security.Cryptography.CryptoStream(tempStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
         /* User Code */
         				using (var writer = new System.IO.BinaryWriter(cryptoStream))
         /* User Code */
         				using (var inputStream = new System.IO.FileStream(inputFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
         				{
         /* User Code */
         					writer.Write(nameBytes.Length);
         /* User Code */
         					writer.Write(nameBytes);
         /* User Code */
         					inputStream.CopyTo(cryptoStream);
         /* User Code */
         				}
         /* User Code */
         			}
         /* User Code */
         			System.Byte[] hmac;
         /* User Code */
         			using (var tempStreamForHmac = new System.IO.FileStream(tempFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
         			using (var hmacSha256 = new System.Security.Cryptography.HMACSHA256(hmacKey))
         /* User Code */
         			{
         /* User Code */
         				hmac = hmacSha256.ComputeHash(tempStreamForHmac);
         /* User Code */
         			}
         /* User Code */
         			using (var outputStream = new System.IO.FileStream(outputFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         /* User Code */
         			using (var tempStreamForCopy = new System.IO.FileStream(tempFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
         			{
         /* User Code */
         				outputStream.Write(iv, 0, iv.Length);
         /* User Code */
         				outputStream.Write(System.BitConverter.GetBytes(32), 0, 4);
         /* User Code */
         				outputStream.Write(hmac, 0, hmac.Length);
         /* User Code */
         				outputStream.Write(System.BitConverter.GetBytes(encryptedKey.Length), 0, 4);
         /* User Code */
         				outputStream.Write(encryptedKey, 0, encryptedKey.Length);
         /* User Code */
         				tempStreamForCopy.CopyTo(outputStream);
         /* User Code */
         			}
         /* User Code */
         			return (
         /* User Code */
         				System.Convert.ToBase64String(iv),
         /* User Code */
         				System.Convert.ToBase64String(hmac),
         /* User Code */
         				System.Convert.ToBase64String(encryptedKey)
         /* User Code */
         			);
         /* User Code */
         		}
         /* User Code */
         		finally
         /* User Code */
         		{
         /* User Code */
         			System.IO.File.Delete(tempFilePath);
         /* User Code */
         		}
         /* User Code */
         	}
         /* User Code */
         	static System.String DecryptFileStreaming(
         /* User Code */
         		System.String inputFilePath,
         /* User Code */
         		System.String outputFilePath,
         /* User Code */
         		NBitcoin.Key privateKey,
         /* User Code */
         		System.String expectedIvBase64,
         /* User Code */
         		System.String expectedHmacBase64,
         /* User Code */
         		System.String expectedEncryptedKeyBase64)
         /* User Code */
         	{
         /* User Code */
         		using (var inputStream = new System.IO.FileStream(inputFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
         		using (var reader = new System.IO.BinaryReader(inputStream, System.Text.Encoding.UTF8, leaveOpen: true))
         /* User Code */
         		{
         /* User Code */
         			System.Byte[] iv = reader.ReadBytes(16);
         /* User Code */
         			System.Int32 hmacLength = reader.ReadInt32();
         /* User Code */
         			if (hmacLength != 32)
         /* User Code */
         				throw new System.Security.Cryptography.CryptographicException($"Unexpected HMAC length: {hmacLength}. Expected 32.");
         /* User Code */
         			System.Byte[] hmac = reader.ReadBytes(hmacLength);
         /* User Code */
         			System.Int32 encryptedKeyLength = reader.ReadInt32();
         /* User Code */
         			System.Byte[] encryptedKey = reader.ReadBytes(encryptedKeyLength);
         /* User Code */
         			if (!System.String.IsNullOrWhiteSpace(expectedIvBase64) && System.Convert.ToBase64String(iv) != expectedIvBase64)
         /* User Code */
         				throw new System.InvalidOperationException("IV mismatch between header and expected value.");
         /* User Code */
         			if (!System.String.IsNullOrWhiteSpace(expectedHmacBase64) && System.Convert.ToBase64String(hmac) != expectedHmacBase64)
         /* User Code */
         				throw new System.InvalidOperationException("HMAC mismatch between header and expected value.");
         /* User Code */
         			if (!System.String.IsNullOrWhiteSpace(expectedEncryptedKeyBase64) && System.Convert.ToBase64String(encryptedKey) != expectedEncryptedKeyBase64)
         /* User Code */
         				throw new System.InvalidOperationException("Encrypted AES key mismatch between header and expected value.");
         /* User Code */
         			System.Byte[] combinedKey;
         /* User Code */
         			try
         /* User Code */
         			{
         /* User Code */
         				combinedKey = privateKey.Decrypt(encryptedKey);
         /* User Code */
         				if (combinedKey == null || combinedKey.Length != 64)
         /* User Code */
         					throw new System.Security.Cryptography.CryptographicException("Failed to decrypt or invalid decrypted key length.");
         /* User Code */
         			}
         /* User Code */
         			catch (System.Exception ex)
         /* User Code */
         			{
         /* User Code */
         				throw new System.Security.Cryptography.CryptographicException("Failed to decrypt the symmetric keys. Check if the correct private key was used.", ex);
         /* User Code */
         			}
         /* User Code */
         			System.Byte[] aesKey = new System.Byte[32];
         /* User Code */
         			System.Byte[] hmacKey = new System.Byte[32];
         /* User Code */
         			System.Buffer.BlockCopy(combinedKey, 0, aesKey, 0, 32);
         /* User Code */
         			System.Buffer.BlockCopy(combinedKey, 32, hmacKey, 0, 32);
         /* User Code */
         			System.Int64 ciphertextStart = inputStream.Position;
         /* User Code */
         			System.Byte[] computedHmac;
         /* User Code */
         			using (var hmacSha256 = new System.Security.Cryptography.HMACSHA256(hmacKey))
         /* User Code */
         			{
         /* User Code */
         				inputStream.Position = ciphertextStart;
         /* User Code */
         				computedHmac = hmacSha256.ComputeHash(inputStream);
         /* User Code */
         				inputStream.Position = ciphertextStart;
         /* User Code */
         			}
         /* User Code */
         			if (!System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(hmac, computedHmac))
         /* User Code */
         				throw new System.Security.Cryptography.CryptographicException("HMAC validation failed. The file may be corrupted or tampered with.");
         /* User Code */
         			using (var aes = System.Security.Cryptography.Aes.Create())
         /* User Code */
         			{
         /* User Code */

         /* User Code */
         				aes.Key = aesKey;
         /* User Code */
         				aes.IV = iv;
         /* User Code */
         				aes.Mode = System.Security.Cryptography.CipherMode.CBC;
         /* User Code */
         				aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
         /* User Code */
         				using (var decryptor = aes.CreateDecryptor())
         /* User Code */

         /* User Code */
         				using (var cryptoStream = new System.Security.Cryptography.CryptoStream(inputStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
         /* User Code */
         				{
         /* User Code */
         					System.String originalName;
         /* User Code */
         					System.Byte[] nameLengthBytes = new System.Byte[4];
         /* User Code */
         					System.Int32 bytesRead = cryptoStream.Read(nameLengthBytes, 0, 4);
         /* User Code */
         					if (bytesRead < 4)
         /* User Code */
         						throw new System.Security.Cryptography.CryptographicException("Failed to read filename length from encrypted stream. File may be truncated or corrupted.");
         /* User Code */
         					System.Int32 nameLength = System.BitConverter.ToInt32(nameLengthBytes, 0);
         /* User Code */
         					const System.Int32 maxReasonableNameLength = 1024 * 4;
         /* User Code */
         					if (nameLength < 0 || nameLength > maxReasonableNameLength)
         /* User Code */
         					{
         /* User Code */
         						throw new System.Security.Cryptography.CryptographicException($"Invalid original filename length decoded: {nameLength}");
         /* User Code */
         					}
         /* User Code */
         					if (nameLength == 0)
         /* User Code */
         					{
         /* User Code */
         						originalName = System.String.Empty;
         /* User Code */
         					}
         /* User Code */
         					else
         /* User Code */
         					{
         /* User Code */
         						System.Byte[] nameBytes = new System.Byte[nameLength];
         /* User Code */
         						System.Int32 totalNameBytesRead = 0;
         /* User Code */
         						while (totalNameBytesRead < nameLength)
         /* User Code */
         						{
         /* User Code */
         							bytesRead = cryptoStream.Read(nameBytes, totalNameBytesRead, nameLength - totalNameBytesRead);
         /* User Code */
         							if (bytesRead == 0)
         /* User Code */
         								throw new System.Security.Cryptography.CryptographicException("Failed to read full filename from encrypted stream. File may be truncated or corrupted.");
         /* User Code */
         							totalNameBytesRead += bytesRead;
         /* User Code */
         						}
         /* User Code */
         						originalName = System.Text.Encoding.UTF8.GetString(nameBytes);
         /* User Code */
         					}
         /* User Code */
         					using (var outStream = new System.IO.FileStream(outputFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         /* User Code */
         					{
         /* User Code */
         						cryptoStream.CopyTo(outStream);
         /* User Code */
         						outStream.Flush();
         /* User Code */
         					}
         /* User Code */
         					return originalName;
         /* User Code */
         				}
         /* User Code */
         			}
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
         AV9error = "";
         /* GeneXus formulas. */
      }

      private short AV8encryptAction ;
      private string AV12PubKey ;
      private string AV13privateKey ;
      private string AV14ivBase64 ;
      private string AV15hmacBase64 ;
      private string AV16encryptedKeyBase64 ;
      private string AV9error ;
      private string AV10inputFile ;
      private string AV11outputFile ;
      private string AV17originalFileName ;
      private string aP5_originalFileName ;
      private string aP6_ivBase64 ;
      private string aP7_hmacBase64 ;
      private string aP8_encryptedKeyBase64 ;
      private string aP9_error ;
   }

}
