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
namespace GeneXus.Programs.nostr {
   public class serializeevent : GXProcedure
   {
      public serializeevent( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public serializeevent( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref GeneXus.Programs.nostr.SdtEvent aP0_nostrEvent ,
                           string aP1_privateKey ,
                           out string aP2_serialized ,
                           out string aP3_error )
      {
         this.AV12nostrEvent = aP0_nostrEvent;
         this.AV13privateKey = aP1_privateKey;
         this.AV14serialized = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP0_nostrEvent=this.AV12nostrEvent;
         aP2_serialized=this.AV14serialized;
         aP3_error=this.AV10error;
      }

      public string executeUdp( ref GeneXus.Programs.nostr.SdtEvent aP0_nostrEvent ,
                                string aP1_privateKey ,
                                out string aP2_serialized )
      {
         execute(ref aP0_nostrEvent, aP1_privateKey, out aP2_serialized, out aP3_error);
         return AV10error ;
      }

      public void executeSubmit( ref GeneXus.Programs.nostr.SdtEvent aP0_nostrEvent ,
                                 string aP1_privateKey ,
                                 out string aP2_serialized ,
                                 out string aP3_error )
      {
         this.AV12nostrEvent = aP0_nostrEvent;
         this.AV13privateKey = aP1_privateKey;
         this.AV14serialized = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP0_nostrEvent=this.AV12nostrEvent;
         aP2_serialized=this.AV14serialized;
         aP3_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
           int GetHexValue(char hex)
         /* User Code */
          {
         /* User Code */
          return hex - ((hex < ':') ? 48 : ((hex < 'a') ? 55 : 87));
         /* User Code */
          }
         /* User Code */
          byte[] ToByteArray(string hex)
         /* User Code */
          {
         /* User Code */
          if (hex.Length % 2 == 1)
         /* User Code */
          {
         /* User Code */
          throw new Exception("The binary key cannot have an odd number of digits");
         /* User Code */
          }
         /* User Code */
          byte[] array = new byte[hex.Length >> 1];
         /* User Code */
          for (int i = 0; i < hex.Length >> 1; i++)
         /* User Code */
          {
         /* User Code */
          array[i] = (byte)((GetHexValue(hex[i << 1]) << 4) + GetHexValue(hex[(i << 1) + 1]));
         /* User Code */
          }
         /* User Code */
          return array;
         /* User Code */
          }
         /* User Code */
          string hexPrivateKey = AV13privateKey;
         /* User Code */
          byte[] bytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexPrivateKey);
         /* User Code */
          NBitcoin.Secp256k1.ECPrivKey privKey = NBitcoin.Secp256k1.ECPrivKey.Create(bytes);
         /* User Code */
          var derivedPubKey = privKey.CreateXOnlyPubKey();
         /* User Code */
          AV17pubkey = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(derivedPubKey.ToBytes());
         AV12nostrEvent.gxTpr_Pubkey = AV17pubkey;
         AV9created_at = AV12nostrEvent.gxTpr_Created_at;
         AV11kind = AV12nostrEvent.gxTpr_Kind;
         AV8content = StringUtil.Trim( AV12nostrEvent.gxTpr_Content);
         /* User Code */
          string eventId = "";
         /* User Code */
          string pubkey = AV17pubkey;
         /* User Code */
          long createdAt = AV9created_at;
         /* User Code */
          int kind = AV11kind;
         /* User Code */
          string content = AV8content;
         /* User Code */
          string sig = "";
         /* User Code */
          var tags = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV12nostrEvent.gxTpr_Tags.Count )
         {
            AV16tagsoftags = ((GeneXus.Programs.nostr.SdtTagsOfTags)AV12nostrEvent.gxTpr_Tags.Item(AV20GXV1));
            /* User Code */
             System.Collections.Generic.List<string> entry = new System.Collections.Generic.List<string>();
            AV21GXV2 = 1;
            while ( AV21GXV2 <= AV16tagsoftags.gxTpr_Tag.Count )
            {
               AV15tag = AV16tagsoftags.gxTpr_Tag.GetString(AV21GXV2);
               /* User Code */
                entry.Add(AV15tag);
               AV21GXV2 = (int)(AV21GXV2+1);
            }
            /* User Code */
             tags.Add(entry);
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         /* User Code */
          var preEventData = new System.Collections.Generic.List<object>
         /* User Code */
          {
         /* User Code */
           0,
         /* User Code */
          pubkey,
         /* User Code */
          createdAt,
         /* User Code */
          kind,
         /* User Code */
          tags,
         /* User Code */
          content
         /* User Code */
          };
         /* User Code */
          var eventData = new System.Collections.Generic.Dictionary<string, object>
         /* User Code */
          {
         /* User Code */
          { "id", eventId },
         /* User Code */
          { "pubkey", pubkey },
         /* User Code */
          { "created_at", createdAt },
         /* User Code */
          { "kind", kind },
         /* User Code */
          { "tags", tags },
         /* User Code */
          { "content", content },
         /* User Code */
          { "sig", sig }
         /* User Code */
          };
         /* User Code */
          var jsonSha = System.Text.Json.JsonSerializer.Serialize(preEventData);
         /* User Code */
          	byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(jsonSha);
         /* User Code */
          	NBitcoin.uint256 msgHash = new NBitcoin.uint256(NBitcoin.Crypto.Hashes.SHA256(msgBytes) , false);
         /* User Code */
          eventData["id"] = msgHash.ToString();
         /* User Code */
          AV18id = msgHash.ToString();
         AV12nostrEvent.gxTpr_Id = AV18id;
         /* User Code */
          var signature = privKey.SignBIP340(ToByteArray(msgHash.ToString()));
         /* User Code */
          eventData["sig"] = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(signature.ToBytes());
         /* User Code */
          AV19sig = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(signature.ToBytes());
         AV12nostrEvent.gxTpr_Sig = AV19sig;
         /* User Code */
          var json = System.Text.Json.JsonSerializer.Serialize(new object[] { "EVENT", eventData });
         /* User Code */
          AV14serialized = json;
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
         AV14serialized = "";
         AV10error = "";
         AV17pubkey = "";
         AV8content = "";
         AV16tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
         AV15tag = "";
         AV18id = "";
         AV19sig = "";
         /* GeneXus formulas. */
      }

      private int AV11kind ;
      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private long AV9created_at ;
      private string AV13privateKey ;
      private string AV10error ;
      private string AV17pubkey ;
      private string AV15tag ;
      private string AV18id ;
      private string AV19sig ;
      private string AV14serialized ;
      private string AV8content ;
      private GeneXus.Programs.nostr.SdtEvent AV12nostrEvent ;
      private GeneXus.Programs.nostr.SdtEvent aP0_nostrEvent ;
      private GeneXus.Programs.nostr.SdtTagsOfTags AV16tagsoftags ;
      private string aP2_serialized ;
      private string aP3_error ;
   }

}
