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
   public class cleantempstorage : GXProcedure
   {
      public cleantempstorage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public cleantempstorage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9directory.Source = "PublicTempStorage";
         if ( AV9directory.Exists() )
         {
            AV12GXV2 = 1;
            AV11GXV1 = AV9directory.GetFiles("");
            while ( AV12GXV2 <= AV11GXV1.ItemCount )
            {
               AV8auxFile = AV11GXV1.Item(AV12GXV2);
               AV8auxFile.Delete();
               AV12GXV2 = (int)(AV12GXV2+1);
            }
         }
         GXt_char1 = AV10privateDir;
         new GeneXus.Programs.wallet.getprivatetempdir(context ).execute( out  GXt_char1) ;
         AV10privateDir = GXt_char1;
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
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV11GXV1 = new GxFileCollection();
         AV8auxFile = new GxFile(context.GetPhysicalPath());
         AV10privateDir = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV12GXV2 ;
      private string GXt_char1 ;
      private string AV10privateDir ;
      private GxFile AV8auxFile ;
      private GxDirectory AV9directory ;
      private GxFileCollection AV11GXV1 ;
   }

}
