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
namespace GeneXus.Programs.wallet.registered {
   public class grouprecordkey : GXProcedure
   {
      public grouprecordkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public grouprecordkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_groupKey ,
                           string aP1_kind ,
                           string aP2_naturalKey ,
                           out string aP3_recordKey ,
                           out string aP4_error )
      {
         this.AV9groupKey = aP0_groupKey;
         this.AV10kind = aP1_kind;
         this.AV12naturalKey = aP2_naturalKey;
         this.AV13recordKey = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_recordKey=this.AV13recordKey;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_groupKey ,
                                string aP1_kind ,
                                string aP2_naturalKey ,
                                out string aP3_recordKey )
      {
         execute(aP0_groupKey, aP1_kind, aP2_naturalKey, out aP3_recordKey, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_groupKey ,
                                 string aP1_kind ,
                                 string aP2_naturalKey ,
                                 out string aP3_recordKey ,
                                 out string aP4_error )
      {
         this.AV9groupKey = aP0_groupKey;
         this.AV10kind = aP1_kind;
         this.AV12naturalKey = aP2_naturalKey;
         this.AV13recordKey = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_recordKey=this.AV13recordKey;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13recordKey = "";
         AV8error = "";
         AV11message = "DistributedCryptography group record\\n" + StringUtil.Trim( AV10kind) + "\\n" + StringUtil.Trim( AV12naturalKey);
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          	byte[] recordHmacKey = System.Text.Encoding.UTF8.GetBytes(AV9groupKey.Trim());
         /* User Code */
          	using (System.Security.Cryptography.HMACSHA256 recordHmac = new System.Security.Cryptography.HMACSHA256(recordHmacKey))
         /* User Code */
          	{
         /* User Code */
          		byte[] recordHash = recordHmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(AV11message));
         /* User Code */
          		AV13recordKey = BitConverter.ToString(recordHash).Replace("-", "").ToLowerInvariant();
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
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV9groupKey))) || ( StringUtil.Len( StringUtil.Trim( AV13recordKey)) != 64 ) ) )
         {
            AV8error = "The key of the shared record could not be computed";
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
         AV13recordKey = "";
         AV8error = "";
         AV11message = "";
         /* GeneXus formulas. */
      }

      private string AV9groupKey ;
      private string AV8error ;
      private string AV10kind ;
      private string AV12naturalKey ;
      private string AV13recordKey ;
      private string AV11message ;
      private string aP3_recordKey ;
      private string aP4_error ;
   }

}
