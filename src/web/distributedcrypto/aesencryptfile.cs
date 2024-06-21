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
   public class aesencryptfile : GXProcedure
   {
      public aesencryptfile( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public aesencryptfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputFile ,
                           string aP1_outputFile ,
                           out string aP2_encryptionKey ,
                           out string aP3_IV ,
                           out string aP4_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV12outputFile = aP1_outputFile;
         this.AV13encryptionKey = "" ;
         this.AV11IV = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_encryptionKey=this.AV13encryptionKey;
         aP3_IV=this.AV11IV;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_inputFile ,
                                string aP1_outputFile ,
                                out string aP2_encryptionKey ,
                                out string aP3_IV )
      {
         execute(aP0_inputFile, aP1_outputFile, out aP2_encryptionKey, out aP3_IV, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_inputFile ,
                                 string aP1_outputFile ,
                                 out string aP2_encryptionKey ,
                                 out string aP3_IV ,
                                 out string aP4_error )
      {
         this.AV10inputFile = aP0_inputFile;
         this.AV12outputFile = aP1_outputFile;
         this.AV13encryptionKey = "" ;
         this.AV11IV = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_encryptionKey=this.AV13encryptionKey;
         aP3_IV=this.AV11IV;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9file.Source = StringUtil.Trim( AV10inputFile);
         if ( AV9file.Exists() )
         {
            /* User Code */
             string inputFile = AV10inputFile;
            /* User Code */
             string outputFile = AV12outputFile;
            /* User Code */
                try
            /* User Code */
                {
            /* User Code */
                    using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            /* User Code */
                    {
            /* User Code */
            			byte[]  IV = aes.IV;
            /* User Code */
            			byte[] key = aes.Key;
            /* User Code */
             			AV11IV  = Convert.ToBase64String(IV, 0, IV.Length);
            /* User Code */
             			AV13encryptionKey  = Convert.ToBase64String(key, 0, key.Length);
            /* User Code */
                        using (System.IO.FileStream fsCrypt = new System.IO.FileStream(outputFile, System.IO.FileMode.Create))
            /* User Code */
                        {
            /* User Code */
                            using (System.Security.Cryptography.ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
            /* User Code */
                            {
            /* User Code */
                                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fsCrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
            /* User Code */
                                {
            /* User Code */
                                    using (System.IO.FileStream fsIn = new System.IO.FileStream(inputFile, System.IO.FileMode.Open))
            /* User Code */
                                    {
            /* User Code */
                                        int data;
            /* User Code */
                                        while ((data = fsIn.ReadByte()) != -1)
            /* User Code */
                                        {
            /* User Code */
                                            cs.WriteByte((byte)data);
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
         AV13encryptionKey = "";
         AV11IV = "";
         AV8error = "";
         AV9file = new GxFile(context.GetPhysicalPath());
         /* GeneXus formulas. */
      }

      private string AV13encryptionKey ;
      private string AV11IV ;
      private string AV10inputFile ;
      private string AV12outputFile ;
      private string AV8error ;
      private string aP2_encryptionKey ;
      private string aP3_IV ;
      private string aP4_error ;
      private GxFile AV9file ;
   }

}
