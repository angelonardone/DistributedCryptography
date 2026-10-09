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
   public class hassigningkeybip48 : GXProcedure
   {
      public hassigningkeybip48( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hassigningkeybip48( IGxContext context )
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
         GXt_SdtExtKeyInfo1 = AV9extKeyInfoBIP48;
         new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV9extKeyInfoBIP48 = GXt_SdtExtKeyInfo1;
         AV8available = (bool)(!String.IsNullOrEmpty(StringUtil.RTrim( AV9extKeyInfoBIP48.gxTpr_Extended.gxTpr_Privatekey)));
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
         AV9extKeyInfoBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private bool AV8available ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV9extKeyInfoBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private bool aP0_available ;
   }

}
