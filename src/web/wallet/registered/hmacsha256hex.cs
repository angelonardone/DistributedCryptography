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
   public class hmacsha256hex : GXProcedure
   {
      public hmacsha256hex( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hmacsha256hex( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_key ,
                           string aP1_message ,
                           out string aP2_hex ,
                           out string aP3_error )
      {
         this.AV10key = aP0_key;
         this.AV11message = aP1_message;
         this.AV9hex = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_hex=this.AV9hex;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_key ,
                                string aP1_message ,
                                out string aP2_hex )
      {
         execute(aP0_key, aP1_message, out aP2_hex, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_key ,
                                 string aP1_message ,
                                 out string aP2_hex ,
                                 out string aP3_error )
      {
         this.AV10key = aP0_key;
         this.AV11message = aP1_message;
         this.AV9hex = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_hex=this.AV9hex;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9hex = "";
         AV8error = "";
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          	using (System.Security.Cryptography.HMACSHA256 sharedHmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(AV10key.Trim())))
         /* User Code */
          	{
         /* User Code */
          		AV9hex = BitConverter.ToString(sharedHmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(AV11message))).Replace("-", "").ToLowerInvariant();
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
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV10key))) || ( StringUtil.Len( StringUtil.Trim( AV9hex)) != 64 ) ) )
         {
            AV8error = "The secret of the shared records could not be computed";
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
         AV9hex = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string AV10key ;
      private string AV11message ;
      private string AV9hex ;
      private string aP2_hex ;
      private string aP3_error ;
   }

}
