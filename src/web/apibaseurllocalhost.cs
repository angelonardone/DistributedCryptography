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
namespace GeneXus.Programs {
   public class apibaseurllocalhost : GXProcedure
   {
      public apibaseurllocalhost( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public apibaseurllocalhost( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_PathAndMethod ,
                           out string aP1_BaseURL )
      {
         this.AV9PathAndMethod = aP0_PathAndMethod;
         this.AV8BaseURL = "" ;
         initialize();
         ExecuteImpl();
         aP1_BaseURL=this.AV8BaseURL;
      }

      public string executeUdp( string aP0_PathAndMethod )
      {
         execute(aP0_PathAndMethod, out aP1_BaseURL);
         return AV8BaseURL ;
      }

      public void executeSubmit( string aP0_PathAndMethod ,
                                 out string aP1_BaseURL )
      {
         this.AV9PathAndMethod = aP0_PathAndMethod;
         this.AV8BaseURL = "" ;
         SubmitImpl();
         aP1_BaseURL=this.AV8BaseURL;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtSDT_Rest_Parameters_Item1 = AV11restParameters;
         new getrestparameters(context ).execute( out  GXt_SdtSDT_Rest_Parameters_Item1) ;
         AV11restParameters = GXt_SdtSDT_Rest_Parameters_Item1;
         if ( ( StringUtil.StrCmp(AV9PathAndMethod, "/GetRawTransaction - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/EstimateSmartFee - GET") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/GetTransactions - POST") == 0 ) || ( StringUtil.StrCmp(AV9PathAndMethod, "/SendRawTransaction - POST") == 0 ) )
         {
            AV12protocole = ((AV11restParameters.gxTpr_Secure) ? "https://" : "http://");
            AV13port = ((AV11restParameters.gxTpr_Port==0) ? "" : ":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV11restParameters.gxTpr_Port), 4, 0)));
            AV8BaseURL = StringUtil.Trim( AV12protocole) + StringUtil.Trim( AV11restParameters.gxTpr_Hostname) + StringUtil.Trim( AV13port) + StringUtil.Trim( AV11restParameters.gxTpr_Restpath);
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
         AV8BaseURL = "";
         AV11restParameters = new SdtSDT_Rest_Parameters_Item(context);
         GXt_SdtSDT_Rest_Parameters_Item1 = new SdtSDT_Rest_Parameters_Item(context);
         AV12protocole = "";
         AV13port = "";
         /* GeneXus formulas. */
      }

      private string AV9PathAndMethod ;
      private string AV12protocole ;
      private string AV13port ;
      private string AV8BaseURL ;
      private SdtSDT_Rest_Parameters_Item AV11restParameters ;
      private SdtSDT_Rest_Parameters_Item GXt_SdtSDT_Rest_Parameters_Item1 ;
      private string aP1_BaseURL ;
   }

}
