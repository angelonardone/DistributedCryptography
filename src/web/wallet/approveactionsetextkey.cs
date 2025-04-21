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
   public class approveactionsetextkey : GXProcedure
   {
      public approveactionsetextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public approveactionsetextkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           out string aP1_error )
      {
         this.AV15password = aP0_password;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( string aP0_password )
      {
         execute(aP0_password, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_password ,
                                 out string aP1_error )
      {
         this.AV15password = aP0_password;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV16wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV16wallet = GXt_SdtWallet1;
         GXt_char2 = AV9error;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV15password,  AV16wallet.gxTpr_Encryptedsecret, out  AV8clearText, ref  GXt_char2) ;
         AV9error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV10extendeSecretAndAuthenticator.FromJSonString(AV8clearText, null);
            AV11extKeyCreate.gxTpr_Createextkeytype = 70;
            AV11extKeyCreate.gxTpr_Extendedprivatekey = AV10extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey;
            AV11extKeyCreate.gxTpr_Networktype = AV16wallet.gxTpr_Networktype;
            GXt_char2 = AV9error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV11extKeyCreate,  "", out  AV12extKeyInfo, out  GXt_char2) ;
            AV9error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GX_msglist.addItem("We couldn't read the Extended Key: "+AV9error);
            }
            else
            {
               new GeneXus.Programs.wallet.setextkey(context ).execute(  AV12extKeyInfo) ;
               cleanup();
               if (true) return;
            }
         }
         else
         {
            GX_msglist.addItem("We couldn't decrypt the wallet with the password provided: "+AV9error);
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
         AV9error = "";
         AV16wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8clearText = "";
         AV10extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV11extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         GXt_char2 = "";
         AV12extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private string AV15password ;
      private string AV9error ;
      private string GXt_char2 ;
      private string AV8clearText ;
      private GeneXus.Programs.wallet.SdtWallet AV16wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV10extendeSecretAndAuthenticator ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV11extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfo ;
      private string aP1_error ;
   }

}
