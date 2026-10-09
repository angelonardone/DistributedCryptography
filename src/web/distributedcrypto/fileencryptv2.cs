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
   public class fileencryptv2 : GXProcedure
   {
      public fileencryptv2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public fileencryptv2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_inputFile ,
                           string aP1_outputFile ,
                           string aP2_recipientPubKey ,
                           string aP3_originalFileName ,
                           out string aP4_error )
      {
         this.AV9inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12recipientPubKey = aP2_recipientPubKey;
         this.AV10originalFileName = aP3_originalFileName;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_inputFile ,
                                string aP1_outputFile ,
                                string aP2_recipientPubKey ,
                                string aP3_originalFileName )
      {
         execute(aP0_inputFile, aP1_outputFile, aP2_recipientPubKey, aP3_originalFileName, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_inputFile ,
                                 string aP1_outputFile ,
                                 string aP2_recipientPubKey ,
                                 string aP3_originalFileName ,
                                 out string aP4_error )
      {
         this.AV9inputFile = aP0_inputFile;
         this.AV11outputFile = aP1_outputFile;
         this.AV12recipientPubKey = aP2_recipientPubKey;
         this.AV10originalFileName = aP3_originalFileName;
         this.AV8error = "" ;
         SubmitImpl();
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
              byte[] pub = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV12recipientPubKey.Trim());
         /* User Code */
              DcEncryptV2(AV9inputFile.Trim(), AV11outputFile.Trim(), new NBitcoin.PubKey(pub), AV10originalFileName.Trim());
         /* User Code */
          }
         /* User Code */
          catch (System.Exception ex)
         /* User Code */
          {
         /* User Code */
              try { System.IO.File.Delete(AV11outputFile.Trim()); } catch { }
         /* User Code */
              AV8error = "File encryption failed: " + ex.Message;
         /* User Code */
          }
         /* User Code */
          static void DcEncryptV2(string inPath, string outPath, NBitcoin.PubKey pub, string name)
         /* User Code */
          {
         /* User Code */
              const int CHUNK = 65536;
         /* User Code */
              byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(name ?? "");
         /* User Code */
              if (nameBytes.Length > 1024) throw new System.ArgumentException("the file name is too long");
         /* User Code */
              byte[] fileKey = System.Security.Cryptography.RandomNumberGenerator.GetBytes(32);
         /* User Code */
              byte[] salt = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
         /* User Code */
              byte[] prefix = System.Security.Cryptography.RandomNumberGenerator.GetBytes(7);
         /* User Code */
              byte[] wrapped = pub.Encrypt(fileKey);
         /* User Code */
              if (wrapped.Length > 1024) throw new System.InvalidOperationException("unexpected wrapped key length");
         /* User Code */
              var hdr = new System.IO.MemoryStream();
         /* User Code */
              hdr.Write(System.Text.Encoding.ASCII.GetBytes("DCF2"));
         /* User Code */
              hdr.WriteByte(1);
         /* User Code */
              hdr.Write(new byte[] { 0, 1, 0, 0 });
         /* User Code */
              hdr.Write(new byte[] { (byte)(wrapped.Length >> 8), (byte)wrapped.Length });
         /* User Code */
              hdr.Write(wrapped);
         /* User Code */
              hdr.Write(salt);
         /* User Code */
              hdr.Write(prefix);
         /* User Code */
              byte[] header = hdr.ToArray();
         /* User Code */
              byte[] key = System.Security.Cryptography.HKDF.DeriveKey(System.Security.Cryptography.HashAlgorithmName.SHA256, fileKey, 32, salt, System.Text.Encoding.ASCII.GetBytes("DistCrypt file v2"));
         /* User Code */
              System.Array.Clear(fileKey);
         /* User Code */
              byte[] pre = new byte[2 + nameBytes.Length];
         /* User Code */
              pre[0] = (byte)(nameBytes.Length >> 8);
         /* User Code */
              pre[1] = (byte)nameBytes.Length;
         /* User Code */
              System.Buffer.BlockCopy(nameBytes, 0, pre, 2, nameBytes.Length);
         /* User Code */
              int preOff = 0;
         /* User Code */
              using (var aes = new System.Security.Cryptography.AesGcm(key, 16))
         /* User Code */
              using (var input = new System.IO.FileStream(inPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
         /* User Code */
              using (var output = new System.IO.FileStream(outPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         /* User Code */
              {
         /* User Code */
                  int Fill(byte[] b)
         /* User Code */
                  {
         /* User Code */
                      int n = 0;
         /* User Code */
                      while (n < b.Length && preOff < pre.Length) b[n++] = pre[preOff++];
         /* User Code */
                      while (n < b.Length) { int r = input.Read(b, n, b.Length - n); if (r <= 0) break; n += r; }
         /* User Code */
                      return n;
         /* User Code */
                  }
         /* User Code */
                  output.Write(header);
         /* User Code */
                  byte[] cur = new byte[CHUNK], next = new byte[CHUNK], ct = new byte[CHUNK], tag = new byte[16], nonce = new byte[12];
         /* User Code */
                  int curLen = Fill(cur);
         /* User Code */
                  uint counter = 0;
         /* User Code */
                  while (true)
         /* User Code */
                  {
         /* User Code */
                      int nextLen = curLen == CHUNK ? Fill(next) : 0;
         /* User Code */
                      bool last = nextLen == 0;
         /* User Code */
                      System.Buffer.BlockCopy(prefix, 0, nonce, 0, 7);
         /* User Code */
                      nonce[7] = (byte)(counter >> 24); nonce[8] = (byte)(counter >> 16); nonce[9] = (byte)(counter >> 8); nonce[10] = (byte)counter;
         /* User Code */
                      nonce[11] = (byte)(last ? 1 : 0);
         /* User Code */
                      aes.Encrypt(nonce, new System.ReadOnlySpan<byte>(cur, 0, curLen), new System.Span<byte>(ct, 0, curLen), tag, header);
         /* User Code */
                      output.Write(ct, 0, curLen);
         /* User Code */
                      output.Write(tag, 0, 16);
         /* User Code */
                      if (last) break;
         /* User Code */
                      counter = checked(counter + 1);
         /* User Code */
                      var t = cur; cur = next; next = t; curLen = nextLen;
         /* User Code */
                  }
         /* User Code */
              }
         /* User Code */
              System.Array.Clear(key);
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
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV9inputFile ;
      private string AV11outputFile ;
      private string AV12recipientPubKey ;
      private string AV10originalFileName ;
      private string AV8error ;
      private string aP4_error ;
   }

}
