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
   public class approvespendingkeys : GXProcedure
   {
      public approvespendingkeys( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public approvespendingkeys( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           out string aP1_error )
      {
         this.AV11password = aP0_password;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( string aP0_password )
      {
         execute(aP0_password, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_password ,
                                 out string aP1_error )
      {
         this.AV11password = aP0_password;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8error = "";
         GXt_SdtWallet1 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV12wallet = GXt_SdtWallet1;
         GXt_SdtWallet1 = AV12wallet;
         new GeneXus.Programs.wallet.readwallet(context ).execute(  AV12wallet.gxTpr_Walletfilename, out  GXt_SdtWallet1) ;
         AV12wallet = GXt_SdtWallet1;
         if ( ( StringUtil.StrCmp(AV12wallet.gxTpr_Wallettype, "BrainWallet") == 0 ) || ( StringUtil.StrCmp(AV12wallet.gxTpr_Wallettype, "ImportedWIF") == 0 ) )
         {
            AV9keyCreate.gxTpr_Createkeytype = 30;
            AV9keyCreate.gxTpr_Networktype = AV12wallet.gxTpr_Networktype;
            AV9keyCreate.gxTpr_Addresstype = 0;
            GXt_char2 = AV8error;
            GXt_char3 = AV9keyCreate.gxTpr_Createtext;
            new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV11password,  AV12wallet.gxTpr_Encryptedsecret, out  GXt_char3, ref  GXt_char2) ;
            AV9keyCreate.gxTpr_Createtext = GXt_char3;
            AV8error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GXt_char3 = AV8error;
               new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV9keyCreate,  "", out  AV10keyInfo, out  GXt_char3) ;
               AV8error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  new GeneXus.Programs.wallet.setkey(context ).execute(  AV10keyInfo) ;
                  new GeneXus.Programs.wallet.setdefaultjasonkey(context ).execute(  AV10keyInfo) ;
               }
               else
               {
                  AV8error = "We couldn't create the Key with the password provided: " + AV8error;
               }
            }
            else
            {
               AV8error = "We couldn't decrypt the wallet with the password provided: " + AV8error;
            }
         }
         else
         {
            GXt_char3 = AV8error;
            new GeneXus.Programs.wallet.approveactionsetextkey(context ).execute(  AV11password, out  GXt_char3) ;
            AV8error = GXt_char3;
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
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         GXt_char2 = "";
         AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private string AV11password ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string AV8error ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV9keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV10keyInfo ;
      private string aP1_error ;
   }

}
