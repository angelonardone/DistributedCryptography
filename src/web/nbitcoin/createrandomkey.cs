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
namespace GeneXus.Programs.nbitcoin {
   public class createrandomkey : GXProcedure
   {
      public createrandomkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createrandomkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo ,
                           out string aP1_error )
      {
         this.AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP0_keyInfo=this.AV9keyInfo;
         aP1_error=this.AV10error;
      }

      public string executeUdp( out GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo )
      {
         execute(out aP0_keyInfo, out aP1_error);
         return AV10error ;
      }

      public void executeSubmit( out GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo ,
                                 out string aP1_error )
      {
         this.AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV10error = "" ;
         SubmitImpl();
         aP0_keyInfo=this.AV9keyInfo;
         aP1_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8keyCreate.gxTpr_Createkeytype = 10;
         AV8keyCreate.gxTpr_Networktype = "RegTest";
         AV8keyCreate.gxTpr_Addresstype = 3;
         GXt_char1 = AV10error;
         new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV8keyCreate,  "", out  AV9keyInfo, out  GXt_char1) ;
         AV10error = GXt_char1;
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
         AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV10error = "";
         AV8keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV10error ;
      private string GXt_char1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV9keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV8keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo ;
      private string aP1_error ;
   }

}
