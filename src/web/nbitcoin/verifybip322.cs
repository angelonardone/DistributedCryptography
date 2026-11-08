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
   public class verifybip322 : GXProcedure
   {
      public verifybip322( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public verifybip322( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           string aP1_networkType ,
                           string aP2_message ,
                           string aP3_signature ,
                           out bool aP4_verified ,
                           out string aP5_error )
      {
         this.AV8address = aP0_address;
         this.AV11networkType = aP1_networkType;
         this.AV10message = aP2_message;
         this.AV12signature = aP3_signature;
         this.AV13verified = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP4_verified=this.AV13verified;
         aP5_error=this.AV9error;
      }

      public string executeUdp( string aP0_address ,
                                string aP1_networkType ,
                                string aP2_message ,
                                string aP3_signature ,
                                out bool aP4_verified )
      {
         execute(aP0_address, aP1_networkType, aP2_message, aP3_signature, out aP4_verified, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_address ,
                                 string aP1_networkType ,
                                 string aP2_message ,
                                 string aP3_signature ,
                                 out bool aP4_verified ,
                                 out string aP5_error )
      {
         this.AV8address = aP0_address;
         this.AV11networkType = aP1_networkType;
         this.AV10message = aP2_message;
         this.AV12signature = aP3_signature;
         this.AV13verified = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP4_verified=this.AV13verified;
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
         AV13verified = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
             	NBitcoin.BitcoinAddress bip322Address = NBitcoin.BitcoinAddress.Create(AV8address.Trim(), network);
            /* User Code */
             	AV13verified = bip322Address.VerifyBIP322(AV10message, AV12signature.Trim());
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
         AV9error = "";
         /* GeneXus formulas. */
      }

      private string AV8address ;
      private string AV11networkType ;
      private string AV9error ;
      private bool AV13verified ;
      private string AV10message ;
      private string AV12signature ;
      private bool aP4_verified ;
      private string aP5_error ;
   }

}
