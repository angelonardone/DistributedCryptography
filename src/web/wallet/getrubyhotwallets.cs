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
   public class getrubyhotwallets : GXProcedure
   {
      public getrubyhotwallets( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getrubyhotwallets( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_hotWallets ,
                           out int aP1_nextNumber )
      {
         this.AV8hotWallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography") ;
         this.AV9nextNumber = 0 ;
         initialize();
         ExecuteImpl();
         aP0_hotWallets=this.AV8hotWallets;
         aP1_nextNumber=this.AV9nextNumber;
      }

      public int executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_hotWallets )
      {
         execute(out aP0_hotWallets, out aP1_nextNumber);
         return AV9nextNumber ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_hotWallets ,
                                 out int aP1_nextNumber )
      {
         this.AV8hotWallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography") ;
         this.AV9nextNumber = 0 ;
         SubmitImpl();
         aP0_hotWallets=this.AV8hotWallets;
         aP1_nextNumber=this.AV9nextNumber;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV10parent;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV10parent = GXt_SdtWallet1;
         AV9nextNumber = 0;
         if ( ( StringUtil.StrCmp(AV10parent.gxTpr_Keystorage, "Ruby") == 0 ) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV10parent.gxTpr_Rubyfingerprint)) )
         {
            GXt_objcol_SdtWalletInfo2 = AV12walletList;
            new GeneXus.Programs.wallet.readallwalletsinfo(context ).execute( out  GXt_objcol_SdtWalletInfo2) ;
            AV12walletList = GXt_objcol_SdtWalletInfo2;
            AV13GXV1 = 1;
            while ( AV13GXV1 <= AV12walletList.Count )
            {
               AV11walletInfo = ((GeneXus.Programs.wallet.SdtWalletInfo)AV12walletList.Item(AV13GXV1));
               if ( ( StringUtil.StrCmp(StringUtil.Trim( AV11walletInfo.gxTpr_Hotwalletof), StringUtil.Trim( AV10parent.gxTpr_Rubyfingerprint)) == 0 ) && ( StringUtil.StrCmp(AV11walletInfo.gxTpr_Networktype, AV10parent.gxTpr_Networktype) == 0 ) )
               {
                  AV8hotWallets.Add(AV11walletInfo, 0);
                  if ( AV11walletInfo.gxTpr_Hotwalletnumber >= AV9nextNumber )
                  {
                     AV9nextNumber = (int)(AV11walletInfo.gxTpr_Hotwalletnumber+1);
                  }
               }
               AV13GXV1 = (int)(AV13GXV1+1);
            }
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
         AV8hotWallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography");
         AV10parent = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV12walletList = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography");
         GXt_objcol_SdtWalletInfo2 = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo>( context, "WalletInfo", "distributedcryptography");
         AV11walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private int AV9nextNumber ;
      private int AV13GXV1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> AV8hotWallets ;
      private GeneXus.Programs.wallet.SdtWallet AV10parent ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> AV12walletList ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> GXt_objcol_SdtWalletInfo2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV11walletInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletInfo> aP0_hotWallets ;
      private int aP1_nextNumber ;
   }

}
