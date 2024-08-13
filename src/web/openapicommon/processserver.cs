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
namespace GeneXus.Programs.openapicommon {
   public class processserver : GXProcedure
   {
      public processserver( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public processserver( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_server ,
                           GXProperties aP1_varServerParams ,
                           out string aP2_serverTemplated )
      {
         this.AV8server = aP0_server;
         this.AV11varServerParams = aP1_varServerParams;
         this.AV10serverTemplated = "" ;
         initialize();
         ExecuteImpl();
         aP2_serverTemplated=this.AV10serverTemplated;
      }

      public string executeUdp( string aP0_server ,
                                GXProperties aP1_varServerParams )
      {
         execute(aP0_server, aP1_varServerParams, out aP2_serverTemplated);
         return AV10serverTemplated ;
      }

      public void executeSubmit( string aP0_server ,
                                 GXProperties aP1_varServerParams ,
                                 out string aP2_serverTemplated )
      {
         this.AV8server = aP0_server;
         this.AV11varServerParams = aP1_varServerParams;
         this.AV10serverTemplated = "" ;
         SubmitImpl();
         aP2_serverTemplated=this.AV10serverTemplated;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9serverPart = StringUtil.Trim( AV8server);
         if ( StringUtil.StartsWith( AV9serverPart, "/") )
         {
            AV9serverPart = StringUtil.Format( "http://localhost:80%1", StringUtil.Trim( AV9serverPart), "", "", "", "", "", "", "", "");
         }
         AV12RegularExpression = "(\\{(\\w+?)\\})";
         AV14RegExMatchCollection = GxRegex.Matches(AV9serverPart,AV12RegularExpression);
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV14RegExMatchCollection.Count )
         {
            AV15RegExMatch = ((GxRegexMatch)AV14RegExMatchCollection.Item(AV16GXV1));
            AV13VarServerValue = AV11varServerParams.Get(((string)AV15RegExMatch.Groups.Item(2)));
            AV9serverPart = StringUtil.StringReplace( AV9serverPart, AV15RegExMatch.Value, AV13VarServerValue);
            AV16GXV1 = (int)(AV16GXV1+1);
         }
         AV10serverTemplated = AV9serverPart;
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
         AV10serverTemplated = "";
         AV9serverPart = "";
         AV12RegularExpression = "";
         AV14RegExMatchCollection = new GxUnknownObjectCollection();
         AV15RegExMatch = new GxRegexMatch();
         AV13VarServerValue = "";
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private string AV12RegularExpression ;
      private string AV13VarServerValue ;
      private string AV8server ;
      private string AV10serverTemplated ;
      private string AV9serverPart ;
      private GXProperties AV11varServerParams ;
      private GxRegexMatch AV15RegExMatch ;
      private GxUnknownObjectCollection AV14RegExMatchCollection ;
      private string aP2_serverTemplated ;
   }

}
