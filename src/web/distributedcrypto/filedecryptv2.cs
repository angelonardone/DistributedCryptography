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
   public class filedecryptv2 : GXProcedure
   {
      public filedecryptv2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public filedecryptv2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputFile ,
                           string aP1_outputFile ,
                           string aP2_privateKey ,
                           out string aP3_originalFileName ,
                           out string aP4_error )
      {
         this.AV9inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12privateKey = aP2_privateKey;
         this.AV10originalFileName = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_originalFileName=this.AV10originalFileName;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_inputFile ,
                                string aP1_outputFile ,
                                string aP2_privateKey ,
                                out string aP3_originalFileName )
      {
         execute(aP0_inputFile, aP1_outputFile, aP2_privateKey, out aP3_originalFileName, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_inputFile ,
                                 string aP1_outputFile ,
                                 string aP2_privateKey ,
                                 out string aP3_originalFileName ,
                                 out string aP4_error )
      {
         this.AV9inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12privateKey = aP2_privateKey;
         this.AV10originalFileName = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_originalFileName=this.AV10originalFileName;
         aP4_error=this.AV8error;
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
              byte[] priv = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV12privateKey.Trim());
         /* User Code */
              AV10originalFileName = DcDecryptV2(AV9inputFile.Trim(), AV11outputFile.Trim(), new NBitcoin.Key(priv));
         /* User Code */
          }
         /* User Code */
          catch (System.Security.Cryptography.AuthenticationTagMismatchException)
         /* User Code */
          {
         /* User Code */
              try { System.IO.File.Delete(AV11outputFile.Trim()); } catch { }
         /* User Code */
              AV10originalFileName = "";
         /* User Code */
              AV8error = "The file was modified or damaged (authentication failed)";
         /* User Code */
          }
         /* User Code */
          catch (System.Exception ex)
         /* User Code */
          {
         /* User Code */
              try { System.IO.File.Delete(AV11outputFile.Trim()); } catch { }
         /* User Code */
              AV10originalFileName = "";
         /* User Code */
              AV8error = "File decryption failed: " + ex.Message;
         /* User Code */
          }
         /* User Code */
          static string DcDecryptV2(string inPath, string outPath, NBitcoin.Key priv)
         /* User Code */
          {
         /* User Code */
              const int TAG = 16;
         /* User Code */
              using (var input = new System.IO.FileStream(inPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
              {
         /* User Code */
                  int ReadUpTo(byte[] b, int len)
         /* User Code */
                  {
         /* User Code */
                      int n = 0;
         /* User Code */
                      while (n < len) { int r = input.Read(b, n, len - n); if (r <= 0) break; n += r; }
         /* User Code */
                      return n;
         /* User Code */
                  }
         /* User Code */
                  byte[] fixedHdr = new byte[11];
         /* User Code */
                  if (ReadUpTo(fixedHdr, 11) != 11 || System.Text.Encoding.ASCII.GetString(fixedHdr, 0, 4) != "DCF2")
         /* User Code */
                      throw new System.InvalidOperationException("this is not a DistCrypt v2 file");
         /* User Code */
                  if (fixedHdr[4] != 1) throw new System.InvalidOperationException("unsupported file version " + fixedHdr[4]);
         /* User Code */
                  int chunk = (fixedHdr[5] << 24) | (fixedHdr[6] << 16) | (fixedHdr[7] << 8) | fixedHdr[8];
         /* User Code */
                  if (chunk != 65536) throw new System.InvalidOperationException("unsupported chunk size");
         /* User Code */
                  int wlen = (fixedHdr[9] << 8) | fixedHdr[10];
         /* User Code */
                  if (wlen < 1 || wlen > 1024) throw new System.InvalidOperationException("invalid header");
         /* User Code */
                  byte[] rest = new byte[wlen + 16 + 7];
         /* User Code */
                  if (ReadUpTo(rest, rest.Length) != rest.Length) throw new System.InvalidOperationException("the file is truncated");
         /* User Code */
                  byte[] header = new byte[11 + rest.Length];
         /* User Code */
                  System.Buffer.BlockCopy(fixedHdr, 0, header, 0, 11);
         /* User Code */
                  System.Buffer.BlockCopy(rest, 0, header, 11, rest.Length);
         /* User Code */
                  byte[] wrapped = new byte[wlen], salt = new byte[16], prefix = new byte[7];
         /* User Code */
                  System.Buffer.BlockCopy(rest, 0, wrapped, 0, wlen);
         /* User Code */
                  System.Buffer.BlockCopy(rest, wlen, salt, 0, 16);
         /* User Code */
                  System.Buffer.BlockCopy(rest, wlen + 16, prefix, 0, 7);
         /* User Code */
                  byte[] fileKey;
         /* User Code */
                  try { fileKey = priv.Decrypt(wrapped); }
         /* User Code */
                  catch (System.Exception) { throw new System.InvalidOperationException("this file was not encrypted for this wallet"); }
         /* User Code */
                  if (fileKey == null || fileKey.Length != 32) throw new System.InvalidOperationException("this file was not encrypted for this wallet");
         /* User Code */
                  byte[] key = System.Security.Cryptography.HKDF.DeriveKey(System.Security.Cryptography.HashAlgorithmName.SHA256, fileKey, 32, salt, System.Text.Encoding.ASCII.GetBytes("DistCrypt file v2"));
         /* User Code */
                  System.Array.Clear(fileKey);
         /* User Code */
                  string name = null;
         /* User Code */
                  using (var aes = new System.Security.Cryptography.AesGcm(key, TAG))
         /* User Code */
                  using (var output = new System.IO.FileStream(outPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         /* User Code */
                  {
         /* User Code */
                      byte[] buf = new byte[chunk + TAG], plain = new byte[chunk], nonce = new byte[12];
         /* User Code */
                      uint counter = 0;
         /* User Code */
                      while (true)
         /* User Code */
                      {
         /* User Code */
                          int n = ReadUpTo(buf, buf.Length);
         /* User Code */
                          if (n < TAG) throw new System.InvalidOperationException("the file is truncated");
         /* User Code */
                          bool last = n < buf.Length || input.Position == input.Length;
         /* User Code */
                          int plen = n - TAG;
         /* User Code */
                          System.Buffer.BlockCopy(prefix, 0, nonce, 0, 7);
         /* User Code */
                          nonce[7] = (byte)(counter >> 24); nonce[8] = (byte)(counter >> 16); nonce[9] = (byte)(counter >> 8); nonce[10] = (byte)counter;
         /* User Code */
                          nonce[11] = (byte)(last ? 1 : 0);
         /* User Code */
                          aes.Decrypt(nonce, new System.ReadOnlySpan<byte>(buf, 0, plen), new System.ReadOnlySpan<byte>(buf, plen, TAG), new System.Span<byte>(plain, 0, plen), header);
         /* User Code */
                          int off = 0;
         /* User Code */
                          if (counter == 0)
         /* User Code */
                          {
         /* User Code */
                              if (plen < 2) throw new System.InvalidOperationException("invalid content");
         /* User Code */
                              int nl = (plain[0] << 8) | plain[1];
         /* User Code */
                              if (nl > 1024 || 2 + nl > plen) throw new System.InvalidOperationException("invalid file name");
         /* User Code */
                              name = System.Text.Encoding.UTF8.GetString(plain, 2, nl);
         /* User Code */
                              off = 2 + nl;
         /* User Code */
                          }
         /* User Code */
                          output.Write(plain, off, plen - off);
         /* User Code */
                          if (last) break;
         /* User Code */
                          counter = checked(counter + 1);
         /* User Code */
                      }
         /* User Code */
                      System.Array.Clear(plain);
         /* User Code */
                  }
         /* User Code */
                  System.Array.Clear(key);
         /* User Code */
                  return name ?? "";
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
         AV10originalFileName = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV9inputFile ;
      private string AV11outputFile ;
      private string AV12privateKey ;
      private string AV10originalFileName ;
      private string AV8error ;
      private string aP3_originalFileName ;
      private string aP4_error ;
   }

}
