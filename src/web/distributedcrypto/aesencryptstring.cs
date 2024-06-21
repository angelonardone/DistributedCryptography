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
   public class aesencryptstring : GXProcedure
   {
      public aesencryptstring( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesencryptstring( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputString ,
                           string aP1_inEncryptionKey ,
                           out string aP2_encString ,
                           out string aP3_encryptionKey ,
                           out string aP4_IV ,
                           out string aP5_error )
      {
         this.AV11inputString = aP0_inputString;
         this.AV13inEncryptionKey = aP1_inEncryptionKey;
         this.AV12encString = "" ;
         this.AV8encryptionKey = "" ;
         this.AV10IV = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encString=this.AV12encString;
         aP3_encryptionKey=this.AV8encryptionKey;
         aP4_IV=this.AV10IV;
         aP5_error=this.AV9error;
      }

      public string executeUdp( string aP0_inputString ,
                                string aP1_inEncryptionKey ,
                                out string aP2_encString ,
                                out string aP3_encryptionKey ,
                                out string aP4_IV )
      {
         execute(aP0_inputString, aP1_inEncryptionKey, out aP2_encString, out aP3_encryptionKey, out aP4_IV, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_inputString ,
                                 string aP1_inEncryptionKey ,
                                 out string aP2_encString ,
                                 out string aP3_encryptionKey ,
                                 out string aP4_IV ,
                                 out string aP5_error )
      {
         this.AV11inputString = aP0_inputString;
         this.AV13inEncryptionKey = aP1_inEncryptionKey;
         this.AV12encString = "" ;
         this.AV8encryptionKey = "" ;
         this.AV10IV = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_encString=this.AV12encString;
         aP3_encryptionKey=this.AV8encryptionKey;
         aP4_IV=this.AV10IV;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11inputString)) )
         {
            /* User Code */
                try
            /* User Code */
                {
            /* User Code */
                    using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            /* User Code */
                    {
            /* User Code */
             		string skey = AV13inEncryptionKey;
            /* User Code */
            		byte[]  IV = aes.IV;
            /* User Code */
            		byte[] key;
            /* User Code */
            		if ( skey.Length < 1)
            /* User Code */
            		key = aes.Key;
            /* User Code */
            		else
            /* User Code */
            		key = Convert.FromBase64String(skey);
            /* User Code */
             		AV10IV  = Convert.ToBase64String(IV, 0, IV.Length);
            /* User Code */
             		AV8encryptionKey  = Convert.ToBase64String(key, 0, key.Length);
            /* User Code */
            		byte[] encrypted;
            /* User Code */
            		string plainText = AV11inputString;
            /* User Code */
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            /* User Code */
                        {
            /* User Code */
                            using (System.Security.Cryptography.ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
            /* User Code */
                            {
            /* User Code */
                                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
            /* User Code */
                                {
            /* User Code */
            			   using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
            /* User Code */
                                    {
            /* User Code */
            				sw.Write(plainText);
            /* User Code */
                                    }
            /* User Code */
            			   encrypted = ms.ToArray();
            /* User Code */
             			   string strOutput = Convert.ToBase64String(encrypted, 0, encrypted.Length);
            /* User Code */
            			   AV12encString = strOutput;
            /* User Code */
                                }
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
             		AV9error = ex.Message.ToString();
            /* User Code */
                }
         }
         else
         {
            AV9error = "Input String cannot be empty";
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
         AV12encString = "";
         AV8encryptionKey = "";
         AV10IV = "";
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV13inEncryptionKey ;
      private string AV8encryptionKey ;
      private string AV10IV ;
      private string AV11inputString ;
      private string AV12encString ;
      private string AV9error ;
      private string aP2_encString ;
      private string aP3_encryptionKey ;
      private string aP4_IV ;
      private string aP5_error ;
   }

}
