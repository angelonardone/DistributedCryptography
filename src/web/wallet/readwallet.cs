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
   public class readwallet : GXProcedure
   {
      public readwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletFile_source ,
                           out GeneXus.Programs.wallet.SdtWallet aP1_wallet )
      {
         this.AV12walletFile_source = aP0_walletFile_source;
         this.AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context) ;
         initialize();
         ExecuteImpl();
         aP1_wallet=this.AV10wallet;
      }

      public GeneXus.Programs.wallet.SdtWallet executeUdp( string aP0_walletFile_source )
      {
         execute(aP0_walletFile_source, out aP1_wallet);
         return AV10wallet ;
      }

      public void executeSubmit( string aP0_walletFile_source ,
                                 out GeneXus.Programs.wallet.SdtWallet aP1_wallet )
      {
         this.AV12walletFile_source = aP0_walletFile_source;
         this.AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context) ;
         SubmitImpl();
         aP1_wallet=this.AV10wallet;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11walletFile.Source = StringUtil.Trim( AV12walletFile_source);
         if ( AV11walletFile.Exists() )
         {
            if ( ! AV10wallet.FromJSonFile(AV11walletFile, AV9messages) )
            {
               GX_msglist.addItem("error: "+AV9messages.ToJSonString(false));
            }
         }
         else
         {
            GX_msglist.addItem("Wallet does not exist");
         }
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
         AV10wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11walletFile = new GxFile(context.GetPhysicalPath());
         AV9messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         /* GeneXus formulas. */
      }

      private string AV12walletFile_source ;
      private GeneXus.Programs.wallet.SdtWallet aP1_wallet ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV9messages ;
      private GxFile AV11walletFile ;
      private GeneXus.Programs.wallet.SdtWallet AV10wallet ;
   }

}
