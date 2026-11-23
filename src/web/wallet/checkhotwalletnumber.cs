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
   public class checkhotwalletnumber : GXProcedure
   {
      public checkhotwalletnumber( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public checkhotwalletnumber( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( int aP0_number ,
                           out string aP1_error )
      {
         this.AV11number = aP0_number;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( int aP0_number )
      {
         execute(aP0_number, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( int aP0_number ,
                                 out string aP1_error )
      {
         this.AV11number = aP0_number;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         new GeneXus.Programs.wallet.getrubyhotwallets(context ).execute( out  AV9hotWallets, out  AV10nextNumber) ;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV9hotWallets.Count )
         {
            AV12walletInfo = ((GeneXus.Programs.wallet.SdtWalletInfo)AV9hotWallets.Item(AV13GXV1));
            if ( AV12walletInfo.gxTpr_Hotwalletnumber == AV11number )
            {
               AV8error = StringUtil.Format( "Hot wallet number %1 already exists on this computer: it is the wallet %2", StringUtil.Trim( StringUtil.Str( (decimal)(AV11number), 9, 0)), StringUtil.Trim( AV12walletInfo.gxTpr_Walletname), "", "", "", "", "", "", "");
               cleanup();
               if (true) return;
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV8error = "";
         AV9hotWallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography");
         AV12walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private int AV11number ;
      private int AV10nextNumber ;
      private int AV13GXV1 ;
      private string AV8error ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> AV9hotWallets ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV12walletInfo ;
      private string aP1_error ;
   }

}
