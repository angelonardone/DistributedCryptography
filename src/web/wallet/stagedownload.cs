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
   public class stagedownload : GXProcedure
   {
      public stagedownload( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public stagedownload( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_filePath ,
                           string aP1_downloadName ,
                           out string aP2_token )
      {
         this.AV9filePath = aP0_filePath;
         this.AV8downloadName = aP1_downloadName;
         this.AV10token = "" ;
         initialize();
         ExecuteImpl();
         aP2_token=this.AV10token;
      }

      public string executeUdp( string aP0_filePath ,
                                string aP1_downloadName )
      {
         execute(aP0_filePath, aP1_downloadName, out aP2_token);
         return AV10token ;
      }

      public void executeSubmit( string aP0_filePath ,
                                 string aP1_downloadName ,
                                 out string aP2_token )
      {
         this.AV9filePath = aP0_filePath;
         this.AV8downloadName = aP1_downloadName;
         this.AV10token = "" ;
         SubmitImpl();
         aP2_token=this.AV10token;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10token = Guid.NewGuid( ).ToString();
         AV11webSession.Set("DL."+StringUtil.Trim( AV10token), StringUtil.Trim( AV9filePath));
         AV11webSession.Set("DLN."+StringUtil.Trim( AV10token), new GeneXus.Programs.wallet.safefilename(context).executeUdp(  AV8downloadName));
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
         AV10token = "";
         AV11webSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV9filePath ;
      private string AV8downloadName ;
      private string AV10token ;
      private IGxSession AV11webSession ;
      private string aP2_token ;
   }

}
