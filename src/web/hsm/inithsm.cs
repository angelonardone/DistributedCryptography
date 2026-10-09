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
         this.AV18error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV18error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV18error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV18error = "" ;
         SubmitImpl();
         aP0_error=this.AV18error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV59wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV59wallet = GXt_SdtWallet1;
         GXt_SdtExtKeyInfo2 = AV22extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV22extKeyInfo = GXt_SdtExtKeyInfo2;
         AV58HSMconfigSDT.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "hsm.dat", out  AV18error), null);
         if ( AV58HSMconfigSDT.gxTpr_Isactive )
         {
            AV21extKeyCreate.gxTpr_Networktype = AV59wallet.gxTpr_Networktype;
            AV21extKeyCreate.gxTpr_Createextkeytype = 70;
            AV21extKeyCreate.gxTpr_Extendedprivatekey = AV22extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot;
            AV21extKeyCreate.gxTpr_Keypath = "6000'";
            GXt_char3 = AV18error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV21extKeyCreate,  "", out  AV22extKeyInfo, out  GXt_char3) ;
            AV18error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV18error)) )
            {
               AV55HsmManager.clear();
               AV56initOk = AV55HsmManager.initialize(AV22extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot, AV59wallet.gxTpr_Networktype);
               if ( ! AV56initOk )
               {
                  AV18error = "There is a problem initializing the HSM moduel";
               }
            }
            else
            {
               GX_msglist.addItem("Error creating HSM Extended Key: "+AV18error);
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
         AV18error = "";
         AV59wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV22extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV58HSMconfigSDT = new GeneXus.Programs.hsm.SdtHSMconfigSDT(context);
         AV21extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         GXt_char3 = "";
         AV55HsmManager = new GeneXus.Programs.hsm.SdtHsmManager(context);
         /* GeneXus formulas. */
      }

      private string AV18error ;
      private string GXt_char3 ;
      private bool AV56initOk ;
      private GeneXus.Programs.wallet.SdtWallet AV59wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV22extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.hsm.SdtHSMconfigSDT AV58HSMconfigSDT ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV21extKeyCreate ;
      private GeneXus.Programs.hsm.SdtHsmManager AV55HsmManager ;
      private string aP0_error ;
   }

}
