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
namespace GeneXus.Programs.wallet.registered {
   public class hasspendingkeys : GXProcedure
   {
      public hasspendingkeys( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hasspendingkeys( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out bool aP0_available )
      {
         this.AV8available = false ;
         initialize();
         ExecuteImpl();
         aP0_available=this.AV8available;
      }

      public bool executeUdp( )
      {
         execute(out aP0_available);
         return AV8available ;
      }

      public void executeSubmit( out bool aP0_available )
      {
         this.AV8available = false ;
         SubmitImpl();
         aP0_available=this.AV8available;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtKeyInfo1 = AV10keyInfo;
         new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV10keyInfo = GXt_SdtKeyInfo1;
         GXt_SdtExtKeyInfo2 = AV9extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV9extKeyInfo = GXt_SdtExtKeyInfo2;
         AV8available = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV10keyInfo.gxTpr_Privatekey))||!String.IsNullOrEmpty(StringUtil.RTrim( AV9extKeyInfo.gxTpr_Privatekey)));
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
         AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV9extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private bool AV8available ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV10keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV9extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private bool aP0_available ;
   }

}
