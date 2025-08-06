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
   public class decryptcoldcardbackup : GXProcedure
   {
      public decryptcoldcardbackup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public decryptcoldcardbackup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_passPhrase ,
                           string aP1_inputFile ,
                           out string aP2_mnemonic ,
                           out string aP3_error )
      {
         this.AV10passPhrase = aP0_passPhrase;
         this.AV9inputFile = aP1_inputFile;
         this.AV11mnemonic = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_mnemonic=this.AV11mnemonic;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_passPhrase ,
                                string aP1_inputFile ,
                                out string aP2_mnemonic )
      {
         execute(aP0_passPhrase, aP1_inputFile, out aP2_mnemonic, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_passPhrase ,
                                 string aP1_inputFile ,
                                 out string aP2_mnemonic ,
                                 out string aP3_error )
      {
         this.AV10passPhrase = aP0_passPhrase;
         this.AV9inputFile = aP1_inputFile;
         this.AV11mnemonic = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_mnemonic=this.AV11mnemonic;
         aP3_error=this.AV8error;
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
                string passphrase = AV10passPhrase;
         /* User Code */
                string encryptedFilePath = AV9inputFile;
         /* User Code */
                string mnemonicLine = ExtractMnemonic(passphrase,encryptedFilePath);
         /* User Code */
         		 AV11mnemonic = mnemonicLine;
         /* User Code */
             }catch (Exception ex)
         /* User Code */
             {
         /* User Code */
          		AV8error = ex.Message.ToString();
         /* User Code */
             }
         /* User Code */
            static string ExtractMnemonic(string passphrase, string encryptedFilePath)
         /* User Code */
            {
         /* User Code */
                if (!System.IO.File.Exists(encryptedFilePath))
         /* User Code */
                    throw new System.IO.FileNotFoundException($"Encrypted file not found: '{encryptedFilePath}'");
         /* User Code */
                var readerOptions = new SharpCompress.Readers.ReaderOptions { Password = passphrase };
         /* User Code */
                using (var stream = System.IO.File.OpenRead(encryptedFilePath))
         /* User Code */
                using (var archive = SharpCompress.Archives.ArchiveFactory.Open(stream, readerOptions))
         /* User Code */
                {
         /* User Code */
                    var entry = System.Linq.Enumerable.FirstOrDefault(archive.Entries, e => !e.IsDirectory);
         /* User Code */
                   if (entry == null)
         /* User Code */
                        throw new System.IO.InvalidDataException("Archive is empty or has no files.");
         /* User Code */
                    using (var entryStream = entry.OpenEntryStream())
         /* User Code */
                    using (var memoryStream = new System.IO.MemoryStream())
         /* User Code */
                    {
         /* User Code */
                        entryStream.CopyTo(memoryStream);
         /* User Code */
                        string decryptedContent = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
         /* User Code */
                        foreach (var line in decryptedContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
         /* User Code */
                        {
         /* User Code */
                            if (line.TrimStart().StartsWith("mnemonic ="))
         /* User Code */
                            {
         /* User Code */
                                var value = line.Substring(line.IndexOf('=') + 1).Trim().Trim('"');
         /* User Code */
                                return value;
         /* User Code */
                            }
         /* User Code */
                       }
         /* User Code */
                        throw new System.InvalidOperationException("Incorrect COLDCARD 12 words password");
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
         AV11mnemonic = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string AV11mnemonic ;
      private string AV10passPhrase ;
      private string AV9inputFile ;
      private string aP2_mnemonic ;
      private string aP3_error ;
   }

}
