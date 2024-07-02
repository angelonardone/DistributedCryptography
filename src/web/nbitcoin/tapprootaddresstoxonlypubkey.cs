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
namespace GeneXus.Programs.nbitcoin {
   public class tapprootaddresstoxonlypubkey : GXProcedure
   {
      public tapprootaddresstoxonlypubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public tapprootaddresstoxonlypubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_tapRootAddress ,
                           string aP1_networkType ,
                           out string aP2_XonlyPubKey ,
                           out string aP3_error )
      {
         this.AV12tapRootAddress = aP0_tapRootAddress;
         this.AV10networkType = aP1_networkType;
         this.AV14XonlyPubKey = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_XonlyPubKey=this.AV14XonlyPubKey;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_tapRootAddress ,
                                string aP1_networkType ,
                                out string aP2_XonlyPubKey )
      {
         execute(aP0_tapRootAddress, aP1_networkType, out aP2_XonlyPubKey, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_tapRootAddress ,
                                 string aP1_networkType ,
                                 out string aP2_XonlyPubKey ,
                                 out string aP3_error )
      {
         this.AV12tapRootAddress = aP0_tapRootAddress;
         this.AV10networkType = aP1_networkType;
         this.AV14XonlyPubKey = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_XonlyPubKey=this.AV14XonlyPubKey;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network;
         /* User Code */
          network = NBitcoin.Network.Main;
         if ( StringUtil.StrCmp(AV10networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV10networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV10networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV8error = "Network Type not sopported";
         }
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          NBitcoin.TaprootAddress tapPubAddress = NBitcoin.TaprootAddress.Create(AV12tapRootAddress, network);
         /* User Code */
          NBitcoin.TaprootPubKey publicKey = new NBitcoin.TaprootPubKey(tapPubAddress.PubKey.ToBytes());
         /* User Code */
          AV14XonlyPubKey = publicKey.ToString();
         /* User Code */
         	}
         /* User Code */
             catch (Exception ex)
         /* User Code */
             {
         /* User Code */
              AV8error = ex.Message.ToString();
         /* User Code */
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
         AV14XonlyPubKey = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV12tapRootAddress ;
      private string AV10networkType ;
      private string AV14XonlyPubKey ;
      private string AV8error ;
      private string aP2_XonlyPubKey ;
      private string aP3_error ;
   }

}
