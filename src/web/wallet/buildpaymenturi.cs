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
namespace GeneXus.Programs.wallet {
   public class buildpaymenturi : GXProcedure
   {
      public buildpaymenturi( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildpaymenturi( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           decimal aP1_amount ,
                           string aP2_label ,
                           out string aP3_uri )
      {
         this.AV8address = aP0_address;
         this.AV9amount = aP1_amount;
         this.AV11label = aP2_label;
         this.AV13uri = "" ;
         initialize();
         ExecuteImpl();
         aP3_uri=this.AV13uri;
      }

      public string executeUdp( string aP0_address ,
                                decimal aP1_amount ,
                                string aP2_label )
      {
         execute(aP0_address, aP1_amount, aP2_label, out aP3_uri);
         return AV13uri ;
      }

      public void executeSubmit( string aP0_address ,
                                 decimal aP1_amount ,
                                 string aP2_label ,
                                 out string aP3_uri )
      {
         this.AV8address = aP0_address;
         this.AV9amount = aP1_amount;
         this.AV11label = aP2_label;
         this.AV13uri = "" ;
         SubmitImpl();
         aP3_uri=this.AV13uri;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13uri = "bitcoin:" + StringUtil.Trim( AV8address);
         AV12separator = "?";
         if ( ( AV9amount > Convert.ToDecimal( 0 )) )
         {
            AV10amountText = StringUtil.Trim( StringUtil.Str( AV9amount, 17, 8));
            while ( StringUtil.EndsWith( AV10amountText, "0") )
            {
               AV10amountText = StringUtil.Substring( AV10amountText, 1, StringUtil.Len( AV10amountText)-1);
            }
            if ( StringUtil.EndsWith( AV10amountText, ".") )
            {
               AV10amountText = StringUtil.Substring( AV10amountText, 1, StringUtil.Len( AV10amountText)-1);
            }
            AV13uri += AV12separator + "amount=" + AV10amountText;
            AV12separator = "&";
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV11label))) )
         {
            AV14encodedLabel = GXUtil.UrlEncode( StringUtil.Trim( AV11label));
            AV14encodedLabel = StringUtil.StringReplace( AV14encodedLabel, "+", "%20");
            AV13uri += AV12separator + "label=" + StringUtil.Trim( AV14encodedLabel);
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
         AV13uri = "";
         AV12separator = "";
         AV10amountText = "";
         AV14encodedLabel = "";
         /* GeneXus formulas. */
      }

      private decimal AV9amount ;
      private string AV8address ;
      private string AV12separator ;
      private string AV11label ;
      private string AV13uri ;
      private string AV10amountText ;
      private string AV14encodedLabel ;
      private string aP3_uri ;
   }

}
