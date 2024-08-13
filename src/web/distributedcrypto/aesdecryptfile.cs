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
   public class aesdecryptfile : GXProcedure
   {
      public aesdecryptfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesdecryptfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputFile ,
                           string aP1_outputFile ,
                           string aP2_base64Key ,
                           string aP3_IV ,
                           out string aP4_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV13base64Key = aP2_base64Key;
         this.AV9IV = aP3_IV;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_inputFile ,
                                string aP1_outputFile ,
                                string aP2_base64Key ,
                                string aP3_IV )
      {
         execute(aP0_inputFile, aP1_outputFile, aP2_base64Key, aP3_IV, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_inputFile ,
                                 string aP1_outputFile ,
                                 string aP2_base64Key ,
                                 string aP3_IV ,
                                 out string aP4_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV13base64Key = aP2_base64Key;
         this.AV9IV = aP3_IV;
         this.AV8error = "" ;
         SubmitImpl();
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12file.Source = StringUtil.Trim( AV10inputFile);
         if ( AV12file.Exists() )
         {
            /* User Code */
             string inputFile = AV10inputFile;
            /* User Code */
             string outputFile = AV11outputFile;
            /* User Code */
             string skey = AV13base64Key;
            /* User Code */
             string svi = AV9IV;
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
                  	using (System.IO.FileStream fsCrypt = new System.IO.FileStream(inputFile, System.IO.FileMode.Open))
            /* User Code */
                        {
            /* User Code */
                            using (System.IO.FileStream fsOut = new System.IO.FileStream(outputFile, System.IO.FileMode.Create))
            /* User Code */
                            {
            /* User Code */
                                using (System.Security.Cryptography.ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
            /* User Code */
                               {
            /* User Code */
                                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fsCrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
            /* User Code */
                                    {
            /* User Code */
                                        int data;
            /* User Code */
                                        while ((data = cs.ReadByte()) != -1)
            /* User Code */
                                        {
            /* User Code */
                                            fsOut.WriteByte((byte)data);
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
         AV12file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV13base64Key ;
      private string AV9IV ;
      private string AV10inputFile ;
      private string AV11outputFile ;
      private string AV8error ;
      private GxFile AV12file ;
      private string aP4_error ;
   }

}
