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
namespace GeneXus.Programs.nbitcoin {
   public class signbip322 : GXProcedure
   {
      public signbip322( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signbip322( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_privateKey ,
                           string aP1_address ,
                           string aP2_networkType ,
                           string aP3_message ,
                           out string aP4_signature ,
                           out string aP5_error )
      {
         this.AV12privateKey = aP0_privateKey;
         this.AV8address = aP1_address;
         this.AV11networkType = aP2_networkType;
         this.AV10message = aP3_message;
         this.AV13signature = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP4_signature=this.AV13signature;
         aP5_error=this.AV9error;
      }

      public string executeUdp( string aP0_privateKey ,
                                string aP1_address ,
                                string aP2_networkType ,
                                string aP3_message ,
                                out string aP4_signature )
      {
         execute(aP0_privateKey, aP1_address, aP2_networkType, aP3_message, out aP4_signature, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_privateKey ,
                                 string aP1_address ,
                                 string aP2_networkType ,
                                 string aP3_message ,
                                 out string aP4_signature ,
                                 out string aP5_error )
      {
         this.AV12privateKey = aP0_privateKey;
         this.AV8address = aP1_address;
         this.AV11networkType = aP2_networkType;
         this.AV10message = aP3_message;
         this.AV13signature = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP4_signature=this.AV13signature;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network = NBitcoin.Network.Main;
         if ( StringUtil.StrCmp(AV11networkType, "MainNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else if ( StringUtil.StrCmp(AV11networkType, "TestNet") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.TestNet;
         }
         else if ( StringUtil.StrCmp(AV11networkType, "RegTest") == 0 )
         {
            /* User Code */
             network = NBitcoin.Network.RegTest;
         }
         else
         {
            AV9error = "Network Type not sopported";
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
             	NBitcoin.Key bip322Key = new NBitcoin.Key(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV12privateKey.Trim()));
            /* User Code */
             	NBitcoin.BitcoinAddress bip322Address = NBitcoin.BitcoinAddress.Create(AV8address.Trim(), network);
            /* User Code */
             	AV13signature = bip322Key.SignBIP322(bip322Address, AV10message, NBitcoin.BIP322.SignatureType.Simple).ToBase64();
            /* User Code */
             }
            /* User Code */
             catch (Exception ex)
            /* User Code */
             {
            /* User Code */
             	AV9error = ex.Message.ToString();
            /* User Code */
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
         AV13signature = "";
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV12privateKey ;
      private string AV8address ;
      private string AV11networkType ;
      private string AV9error ;
      private string AV10message ;
      private string AV13signature ;
      private string aP4_signature ;
      private string aP5_error ;
   }

}
