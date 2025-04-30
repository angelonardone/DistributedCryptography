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
namespace GeneXus.Programs.debug {
   public class dbg : GXProcedure
   {
      public dbg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public dbg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref string aP0_from ,
                           ref string aP1_comment ,
                           ref string aP2_texto )
      {
         this.AV10from = aP0_from;
         this.AV9comment = aP1_comment;
         this.AV8texto = aP2_texto;
         initialize();
         ExecuteImpl();
         aP0_from=this.AV10from;
         aP1_comment=this.AV9comment;
         aP2_texto=this.AV8texto;
      }

      public string executeUdp( ref string aP0_from ,
                                ref string aP1_comment )
      {
         execute(ref aP0_from, ref aP1_comment, ref aP2_texto);
         return AV8texto ;
      }

      public void executeSubmit( ref string aP0_from ,
                                 ref string aP1_comment ,
                                 ref string aP2_texto )
      {
         this.AV10from = aP0_from;
         this.AV9comment = aP1_comment;
         this.AV8texto = aP2_texto;
         SubmitImpl();
         aP0_from=this.AV10from;
         aP1_comment=this.AV9comment;
         aP2_texto=this.AV8texto;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV13wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV13wallet = GXt_SdtWallet1;
         AV12file.Source = "C:\\debug.txt";
         AV14datetime = DateTimeUtil.Now( context);
         AV12file.OpenWrite("");
         AV12file.WriteLine("###########################################################");
         AV12file.WriteLine(AV10from+":"+context.localUtil.TToC( AV14datetime, 8, 5, 1, 2, "/", ":", " ")+":"+AV9comment);
         AV12file.WriteLine(AV8texto);
         AV12file.Close();
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
         AV13wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV12file = new GxFile(context.GetPhysicalPath());
         AV14datetime = (DateTime)(DateTime.MinValue);
         /* GeneXus formulas. */
      }

      private string AV10from ;
      private DateTime AV14datetime ;
      private string AV8texto ;
      private string AV9comment ;
      private GxFile AV12file ;
      private string aP0_from ;
      private string aP1_comment ;
      private string aP2_texto ;
      private GeneXus.Programs.wallet.SdtWallet AV13wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
   }

}
