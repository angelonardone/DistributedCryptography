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
namespace GeneXus.Programs.hsm {
   public class inithsm : GXProcedure
   {
      public inithsm( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public inithsm( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV8error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV8error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV8error = "" ;
         SubmitImpl();
         aP0_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV15wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV15wallet = GXt_SdtWallet1;
         GXt_SdtExtKeyInfo2 = AV10extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV10extKeyInfo = GXt_SdtExtKeyInfo2;
         AV11HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV8error), null);
         if ( AV11HSMconfigSDT.gxTpr_Isactive )
         {
            AV9extKeyCreate.gxTpr_Networktype = AV15wallet.gxTpr_Networktype;
            AV9extKeyCreate.gxTpr_Createextkeytype = 70;
            AV9extKeyCreate.gxTpr_Extendedprivatekey = AV10extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot;
            AV9extKeyCreate.gxTpr_Keypath = "6000'";
            GXt_char3 = AV8error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV9extKeyCreate,  "", out  AV10extKeyInfo, out  GXt_char3) ;
            AV8error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV12HsmManager.clear();
               AV13initOk = AV12HsmManager.initialize(AV10extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot, AV15wallet.gxTpr_Networktype);
               if ( ! AV13initOk )
               {
                  AV8error = "There is a problem initializing the HSM moduel";
               }
            }
            else
            {
               GX_msglist.addItem("Error creating HSM Extended Key: "+AV8error);
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
         AV8error = "";
         AV15wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV10extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV11HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         AV9extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         GXt_char3 = "";
         AV12HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private string GXt_char3 ;
      private bool AV13initOk ;
      private GeneXus.Programs.wallet.SdtWallet AV15wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV10extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV11HSMconfigSDT ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV9extKeyCreate ;
      private GeneXus.Programs.hsm.SdtHsmManager AV12HsmManager ;
      private string aP0_error ;
   }

}
