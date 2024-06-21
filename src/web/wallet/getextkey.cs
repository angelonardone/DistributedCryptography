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
   public class getextkey : GXProcedure
   {
      public getextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getextkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP0_extKeyInfo )
      {
         this.AV8extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context) ;
         initialize();
         ExecuteImpl();
         aP0_extKeyInfo=this.AV8extKeyInfo;
      }

      public GeneXus.Programs.nbitcoin.SdtExtKeyInfo executeUdp( )
      {
         execute(out aP0_extKeyInfo);
         return AV8extKeyInfo ;
      }

      public void executeSubmit( out GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP0_extKeyInfo )
      {
         this.AV8extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context) ;
         SubmitImpl();
         aP0_extKeyInfo=this.AV8extKeyInfo;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8extKeyInfo.FromJSonString(AV9WebSession.Get("ExtendedKey"), null);
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
         AV8extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo aP0_extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV8extKeyInfo ;
   }

}
