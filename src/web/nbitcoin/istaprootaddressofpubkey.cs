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
   public class istaprootaddressofpubkey : GXProcedure
   {
      public istaprootaddressofpubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public istaprootaddressofpubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           string aP1_networkType ,
                           string aP2_publicKey ,
                           out bool aP3_isOwner ,
                           out string aP4_error )
      {
         this.AV8address = aP0_address;
         this.AV11networkType = aP1_networkType;
         this.AV12publicKey = aP2_publicKey;
         this.AV10isOwner = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP3_isOwner=this.AV10isOwner;
         aP4_error=this.AV9error;
      }

      public string executeUdp( string aP0_address ,
                                string aP1_networkType ,
                                string aP2_publicKey ,
                                out bool aP3_isOwner )
      {
         execute(aP0_address, aP1_networkType, aP2_publicKey, out aP3_isOwner, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_address ,
                                 string aP1_networkType ,
                                 string aP2_publicKey ,
                                 out bool aP3_isOwner ,
                                 out string aP4_error )
      {
         this.AV8address = aP0_address;
         this.AV11networkType = aP1_networkType;
         this.AV12publicKey = aP2_publicKey;
         this.AV10isOwner = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP3_isOwner=this.AV10isOwner;
         aP4_error=this.AV9error;
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
         AV10isOwner = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
             	NBitcoin.PubKey ownerPubKey = new NBitcoin.PubKey(AV12publicKey.Trim());
            /* User Code */
             	string derived = ownerPubKey.GetAddress(NBitcoin.ScriptPubKeyType.TaprootBIP86, network).ToString();
            /* User Code */
             	AV10isOwner = derived == AV8address.Trim();
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
      private string AV12publicKey ;
      private string AV9error ;
      private bool AV10isOwner ;
      private bool aP3_isOwner ;
      private string aP4_error ;
   }

}
