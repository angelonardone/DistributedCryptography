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
   public class iswalletopen : GXProcedure
   {
      public iswalletopen( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public iswalletopen( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out bool aP0_isOpen )
      {
         this.AV8isOpen = false ;
         initialize();
         ExecuteImpl();
         aP0_isOpen=this.AV8isOpen;
      }

      public bool executeUdp( )
      {
         execute(out aP0_isOpen);
         return AV8isOpen ;
      }

      public void executeSubmit( out bool aP0_isOpen )
      {
         this.AV8isOpen = false ;
         SubmitImpl();
         aP0_isOpen=this.AV8isOpen;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8isOpen = false;
         GXt_SdtWallet1 = AV10wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV10wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV9keyInfo;
         new GeneXus.Programs.wallet.getdfaultjasonkey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV9keyInfo = GXt_SdtKeyInfo2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10wallet.gxTpr_Walletname)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV9keyInfo.gxTpr_Publickey)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV9keyInfo.gxTpr_Privatekey)) )
         {
            AV8isOpen = true;
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
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private bool AV8isOpen ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV9keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private bool aP0_isOpen ;
   }

}
