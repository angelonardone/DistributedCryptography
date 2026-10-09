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
   public class newmnemonic : GXProcedure
   {
      public newmnemonic( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public newmnemonic( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_networkType ,
                           short aP1_mnemonicNumberWords ,
                           short aP2_mnemonicLanguage ,
                           out string aP3_mnemonicText ,
                           out string aP4_error )
      {
         this.AV14networkType = aP0_networkType;
         this.AV12mnemonicNumberWords = aP1_mnemonicNumberWords;
         this.AV11mnemonicLanguage = aP2_mnemonicLanguage;
         this.AV13mnemonicText = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_mnemonicText=this.AV13mnemonicText;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_networkType ,
                                short aP1_mnemonicNumberWords ,
                                short aP2_mnemonicLanguage ,
                                out string aP3_mnemonicText )
      {
         execute(aP0_networkType, aP1_mnemonicNumberWords, aP2_mnemonicLanguage, out aP3_mnemonicText, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_networkType ,
                                 short aP1_mnemonicNumberWords ,
                                 short aP2_mnemonicLanguage ,
                                 out string aP3_mnemonicText ,
                                 out string aP4_error )
      {
         this.AV14networkType = aP0_networkType;
         this.AV12mnemonicNumberWords = aP1_mnemonicNumberWords;
         this.AV11mnemonicLanguage = aP2_mnemonicLanguage;
         this.AV13mnemonicText = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_mnemonicText=this.AV13mnemonicText;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9extKeyCreate.gxTpr_Networktype = AV14networkType;
         AV9extKeyCreate.gxTpr_Createextkeytype = 20;
         AV9extKeyCreate.gxTpr_Mnemonicnumberwords = AV12mnemonicNumberWords;
         AV9extKeyCreate.gxTpr_Mnemoniclanguage = AV11mnemonicLanguage;
         AV9extKeyCreate.gxTpr_Keypath = "";
         GXt_char1 = AV8error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV9extKeyCreate,  "", out  AV10extKeyInfo, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV13mnemonicText = AV10extKeyInfo.gxTpr_Mnemonic;
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
         AV13mnemonicText = "";
         AV8error = "";
         AV9extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         GXt_char1 = "";
         AV10extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private short AV12mnemonicNumberWords ;
      private short AV11mnemonicLanguage ;
      private string AV14networkType ;
      private string AV8error ;
      private string GXt_char1 ;
      private string AV13mnemonicText ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV9extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV10extKeyInfo ;
      private string aP3_mnemonicText ;
      private string aP4_error ;
   }

}
