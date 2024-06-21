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
   public class aesdecryptstring : GXProcedure
   {
      public aesdecryptstring( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesdecryptstring( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_encText ,
                           out string aP1_clearText ,
                           string aP2_base64Key ,
                           string aP3_IV ,
                           out string aP4_error )
      {
         this.AV14encText = aP0_encText;
         this.AV15clearText = "" ;
         this.AV8base64Key = aP2_base64Key;
         this.AV12IV = aP3_IV;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_clearText=this.AV15clearText;
         aP4_error=this.AV9error;
      }

      public string executeUdp( string aP0_encText ,
                                out string aP1_clearText ,
                                string aP2_base64Key ,
                                string aP3_IV )
      {
         execute(aP0_encText, out aP1_clearText, aP2_base64Key, aP3_IV, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_encText ,
                                 out string aP1_clearText ,
                                 string aP2_base64Key ,
                                 string aP3_IV ,
                                 out string aP4_error )
      {
         this.AV14encText = aP0_encText;
         this.AV15clearText = "" ;
         this.AV8base64Key = aP2_base64Key;
         this.AV12IV = aP3_IV;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_clearText=this.AV15clearText;
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14encText)) )
         {
            /* User Code */
             string sCipherText = AV14encText;
            /* User Code */
             string skey = AV8base64Key;
            /* User Code */
             string svi = AV12IV;
            /* User Code */
             string plaintext = null;
            /* User Code */
                try
            /* User Code */
                {
            /* User Code */
                    using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            /* User Code */
                    {
            /* User Code */
            		byte[] key = Convert.FromBase64String(skey);
            /* User Code */
            		byte[] IV = Convert.FromBase64String(svi);
            /* User Code */
            		byte[] cipherText = Convert.FromBase64String(sCipherText);
            /* User Code */
                  	using (System.IO.MemoryStream ms = new System.IO.MemoryStream(cipherText))
            /* User Code */
                        {
            /* User Code */
                        	using (System.Security.Cryptography.ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
            /* User Code */
                               {
            /* User Code */
                                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
            /* User Code */
                                    {
            /* User Code */
                	                    using (System.IO.StreamReader sw = new System.IO.StreamReader(cs))
            /* User Code */
                        	           {
            /* User Code */
                         	               plaintext = sw.ReadToEnd();
            /* User Code */
            				      AV15clearText = plaintext;
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
         AV15clearText = "";
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV8base64Key ;
      private string AV12IV ;
      private string AV14encText ;
      private string AV15clearText ;
      private string AV9error ;
      private string aP1_clearText ;
      private string aP4_error ;
   }

}
