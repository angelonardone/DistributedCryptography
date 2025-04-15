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
   public class aesencryptfileto : GXProcedure
   {
      public aesencryptfileto( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesencryptfileto( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputFile ,
                           string aP1_outputFile ,
                           string aP2_PubKey ,
                           out string aP3_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12PubKey = aP2_PubKey;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_inputFile ,
                                string aP1_outputFile ,
                                string aP2_PubKey )
      {
         execute(aP0_inputFile, aP1_outputFile, aP2_PubKey, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_inputFile ,
                                 string aP1_outputFile ,
                                 string aP2_PubKey ,
                                 out string aP3_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12PubKey = aP2_PubKey;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9file.Source = StringUtil.Trim( AV10inputFile);
         if ( AV9file.Exists() )
         {
            /* User Code */
             string inputFilePath = AV10inputFile;
            /* User Code */
             string outputFilePath = AV11outputFile;
            /* User Code */
                try
            /* User Code */
                {
            /* User Code */
            		using (var aes = System.Security.Cryptography.Aes.Create())
            /* User Code */
            		{
            /* User Code */
            			aes.KeySize = 256;
            /* User Code */
            			aes.BlockSize = 128;
            /* User Code */
            			aes.Mode = System.Security.Cryptography.CipherMode.CBC;
            /* User Code */
            			aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            /* User Code */
             			string hexPubKey = AV12PubKey;
            /* User Code */
             			byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPubKey);
            /* User Code */
             			NBitcoin.PubKey publicKey = new NBitcoin.PubKey(bytes);
            /* User Code */
            			var encrypted_key = publicKey.Encrypt(aes.Key);
            /* User Code */
            			int encrypted_key_size = encrypted_key.Length;
            /* User Code */
            			using (var encryptor = aes.CreateEncryptor())
            /* User Code */
            			using (var inputStream = new System.IO.FileStream(inputFilePath, System.IO.FileMode.Open))
            /* User Code */
            			using (var outputStream = new System.IO.FileStream(outputFilePath, System.IO.FileMode.Create))
            /* User Code */
            			using (var writer = new System.IO.BinaryWriter(outputStream))
            /* User Code */
            			{
            /* User Code */
            				writer.Write(aes.IV);
            /* User Code */
            				writer.Write(encrypted_key_size);
            /* User Code */
            				writer.Write(encrypted_key);
            /* User Code */
            				using (var cryptoStream = new System.Security.Cryptography.CryptoStream(outputStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
            /* User Code */
            				{
            /* User Code */
            					inputStream.CopyTo(cryptoStream);
            /* User Code */
            				}
            /* User Code */
            			}
            /* User Code */
            		}
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
         }
         else
         {
            AV8error = "Input File does not exist";
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
         AV8error = "";
         AV9file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV12PubKey ;
      private string AV10inputFile ;
      private string AV11outputFile ;
      private string AV8error ;
      private GxFile AV9file ;
      private string aP3_error ;
   }

}
