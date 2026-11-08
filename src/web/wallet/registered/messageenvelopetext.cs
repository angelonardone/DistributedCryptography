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
   public class messageenvelopetext : GXProcedure
   {
      public messageenvelopetext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public messageenvelopetext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           out string aP1_text )
      {
         this.AV10sdt_message = aP0_sdt_message;
         this.AV11text = "" ;
         initialize();
         ExecuteImpl();
         aP1_text=this.AV11text;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message )
      {
         execute(aP0_sdt_message, out aP1_text);
         return AV11text ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 out string aP1_text )
      {
         this.AV10sdt_message = aP0_sdt_message;
         this.AV11text = "" ;
         SubmitImpl();
         aP1_text=this.AV11text;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8content = StringUtil.Trim( AV10sdt_message.gxTpr_Message);
         /* User Code */
          AV9contentHash = System.Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(AV8content))).ToLower();
         AV11text = "DistributedCryptography message v1" + StringUtil.Chr( 10) + StringUtil.Lower( StringUtil.Trim( AV10sdt_message.gxTpr_Id.ToString())) + StringUtil.Chr( 10) + StringUtil.Trim( StringUtil.Str( (decimal)(AV10sdt_message.gxTpr_Messagetype), 6, 0)) + StringUtil.Chr( 10) + StringUtil.Trim( StringUtil.Str( (decimal)(AV10sdt_message.gxTpr_Datetimeunix), 18, 0)) + StringUtil.Chr( 10) + StringUtil.Trim( AV10sdt_message.gxTpr_From) + StringUtil.Chr( 10) + StringUtil.Trim( AV10sdt_message.gxTpr_To) + StringUtil.Chr( 10) + StringUtil.Trim( AV9contentHash);
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
         AV11text = "";
         AV8content = "";
         AV9contentHash = "";
         /* GeneXus formulas. */
      }

      private string AV11text ;
      private string AV8content ;
      private string AV9contentHash ;
      private GeneXus.Programs.nostr.SdtSDT_message AV10sdt_message ;
      private string aP1_text ;
   }

}
