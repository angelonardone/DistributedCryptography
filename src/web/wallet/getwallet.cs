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
   public class getwallet : GXProcedure
   {
      public getwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.wallet.SdtWallet aP0_wallet )
      {
         this.AV9wallet = new GeneXus.Programs.wallet.SdtWallet(context) ;
         initialize();
         ExecuteImpl();
         aP0_wallet=this.AV9wallet;
      }

      public GeneXus.Programs.wallet.SdtWallet executeUdp( )
      {
         execute(out aP0_wallet);
         return AV9wallet ;
      }

      public void executeSubmit( out GeneXus.Programs.wallet.SdtWallet aP0_wallet )
      {
         this.AV9wallet = new GeneXus.Programs.wallet.SdtWallet(context) ;
         SubmitImpl();
         aP0_wallet=this.AV9wallet;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9wallet.FromJSonString(AV8WebSession.Get("Wallet"), null);
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
         AV9wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV8WebSession ;
      private GeneXus.Programs.wallet.SdtWallet aP0_wallet ;
      private GeneXus.Programs.wallet.SdtWallet AV9wallet ;
   }

}
