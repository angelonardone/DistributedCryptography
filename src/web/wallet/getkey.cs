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
namespace GeneXus.Programs.wallet {
   public class getkey : GXProcedure
   {
      public getkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo )
      {
         this.AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         initialize();
         ExecuteImpl();
         aP0_keyInfo=this.AV9keyInfo;
      }

      public GeneXus.Programs.nbitcoin.SdtKeyInfo executeUdp( )
      {
         execute(out aP0_keyInfo);
         return AV9keyInfo ;
      }

      public void executeSubmit( out GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo )
      {
         this.AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         SubmitImpl();
         aP0_keyInfo=this.AV9keyInfo;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9keyInfo.FromJSonString(AV8WebSession.Get("Key"), null);
         this.cleanup();
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
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV8WebSession ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV9keyInfo ;
   }

}
