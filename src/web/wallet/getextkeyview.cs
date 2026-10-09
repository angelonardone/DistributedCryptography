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
   public class getextkeyview : GXProcedure
   {
      public getextkeyview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getextkeyview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_masterPublicKey ,
                           out string aP1_fingerprint ,
                           out string aP2_parentFingerprint ,
                           out short aP3_depth ,
                           out long aP4_child ,
                           out bool aP5_isHardened ,
                           out string aP6_error )
      {
         this.AV14masterPublicKey = "" ;
         this.AV12fingerprint = "" ;
         this.AV15parentFingerprint = "" ;
         this.AV9depth = 0 ;
         this.AV8child = 0 ;
         this.AV13isHardened = false ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP0_masterPublicKey=this.AV14masterPublicKey;
         aP1_fingerprint=this.AV12fingerprint;
         aP2_parentFingerprint=this.AV15parentFingerprint;
         aP3_depth=this.AV9depth;
         aP4_child=this.AV8child;
         aP5_isHardened=this.AV13isHardened;
         aP6_error=this.AV10error;
      }

      public string executeUdp( out string aP0_masterPublicKey ,
                                out string aP1_fingerprint ,
                                out string aP2_parentFingerprint ,
                                out short aP3_depth ,
                                out long aP4_child ,
                                out bool aP5_isHardened )
      {
         execute(out aP0_masterPublicKey, out aP1_fingerprint, out aP2_parentFingerprint, out aP3_depth, out aP4_child, out aP5_isHardened, out aP6_error);
         return AV10error ;
      }

      public void executeSubmit( out string aP0_masterPublicKey ,
                                 out string aP1_fingerprint ,
                                 out string aP2_parentFingerprint ,
                                 out short aP3_depth ,
                                 out long aP4_child ,
                                 out bool aP5_isHardened ,
                                 out string aP6_error )
      {
         this.AV14masterPublicKey = "" ;
         this.AV12fingerprint = "" ;
         this.AV15parentFingerprint = "" ;
         this.AV9depth = 0 ;
         this.AV8child = 0 ;
         this.AV13isHardened = false ;
         this.AV10error = "" ;
         SubmitImpl();
         aP0_masterPublicKey=this.AV14masterPublicKey;
         aP1_fingerprint=this.AV12fingerprint;
         aP2_parentFingerprint=this.AV15parentFingerprint;
         aP3_depth=this.AV9depth;
         aP4_child=this.AV8child;
         aP5_isHardened=this.AV13isHardened;
         aP6_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         GXt_SdtExtKeyInfo1 = AV11extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV11extKeyInfo = GXt_SdtExtKeyInfo1;
         GXt_SdtWalletInfo2 = AV16walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo2) ;
         AV16walletInfo = GXt_SdtWalletInfo2;
         if ( StringUtil.StrCmp(AV16walletInfo.gxTpr_Wallettype, "BIP44") == 0 )
         {
            AV14masterPublicKey = AV11extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickey;
         }
         else if ( StringUtil.StrCmp(AV16walletInfo.gxTpr_Wallettype, "BIP49") == 0 )
         {
            AV14masterPublicKey = AV11extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh;
         }
         else if ( StringUtil.StrCmp(AV16walletInfo.gxTpr_Wallettype, "BIP84") == 0 )
         {
            AV14masterPublicKey = AV11extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwit;
         }
         else if ( StringUtil.StrCmp(AV16walletInfo.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV14masterPublicKey = AV11extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
         }
         else
         {
            AV10error = "We couldn't find the this type of wallet addresses";
         }
         AV12fingerprint = AV11extKeyInfo.gxTpr_Extended.gxTpr_Fingerprint;
         AV15parentFingerprint = AV11extKeyInfo.gxTpr_Extended.gxTpr_Parentfingerprint;
         AV9depth = AV11extKeyInfo.gxTpr_Extended.gxTpr_Depth;
         AV8child = AV11extKeyInfo.gxTpr_Extended.gxTpr_Child;
         AV13isHardened = AV11extKeyInfo.gxTpr_Extended.gxTpr_Ishardended;
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
         AV14masterPublicKey = "";
         AV12fingerprint = "";
         AV15parentFingerprint = "";
         AV10error = "";
         AV11extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV16walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo2 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         /* GeneXus formulas. */
      }

      private short AV9depth ;
      private long AV8child ;
      private string AV14masterPublicKey ;
      private string AV12fingerprint ;
      private string AV15parentFingerprint ;
      private string AV10error ;
      private bool AV13isHardened ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV11extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV16walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo2 ;
      private string aP0_masterPublicKey ;
      private string aP1_fingerprint ;
      private string aP2_parentFingerprint ;
      private short aP3_depth ;
      private long aP4_child ;
      private bool aP5_isHardened ;
      private string aP6_error ;
   }

}
