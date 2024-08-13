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
   public class parse_serialized_extended_key : GXProcedure
   {
      public parse_serialized_extended_key( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public parse_serialized_extended_key( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_in_extended_key ,
                           out string aP1_out_extended_key ,
                           out string aP2_networkType ,
                           out string aP3_extendedKeyType ,
                           out string aP4_error )
      {
         this.AV10in_extended_key = aP0_in_extended_key;
         this.AV12out_extended_key = "" ;
         this.AV11networkType = "" ;
         this.AV9extendedKeyType = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_out_extended_key=this.AV12out_extended_key;
         aP2_networkType=this.AV11networkType;
         aP3_extendedKeyType=this.AV9extendedKeyType;
         aP4_error=this.AV8error;
      }

      public string executeUdp( string aP0_in_extended_key ,
                                out string aP1_out_extended_key ,
                                out string aP2_networkType ,
                                out string aP3_extendedKeyType )
      {
         execute(aP0_in_extended_key, out aP1_out_extended_key, out aP2_networkType, out aP3_extendedKeyType, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_in_extended_key ,
                                 out string aP1_out_extended_key ,
                                 out string aP2_networkType ,
                                 out string aP3_extendedKeyType ,
                                 out string aP4_error )
      {
         this.AV10in_extended_key = aP0_in_extended_key;
         this.AV12out_extended_key = "" ;
         this.AV11networkType = "" ;
         this.AV9extendedKeyType = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_out_extended_key=this.AV12out_extended_key;
         aP2_networkType=this.AV11networkType;
         aP3_extendedKeyType=this.AV9extendedKeyType;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          NBitcoin.Network network;
         if ( GxRegex.IsMatch(AV10in_extended_key,"^xprv") || GxRegex.IsMatch(AV10in_extended_key,"^yprv") || GxRegex.IsMatch(AV10in_extended_key,"^zprv") || GxRegex.IsMatch(AV10in_extended_key,"^xpub") || GxRegex.IsMatch(AV10in_extended_key,"^ypub") || GxRegex.IsMatch(AV10in_extended_key,"^zpub") )
         {
            AV11networkType = "MainNet";
            /* User Code */
             network = NBitcoin.Network.Main;
         }
         else
         {
            if ( GxRegex.IsMatch(AV10in_extended_key,"^tprv") || GxRegex.IsMatch(AV10in_extended_key,"^uprv") || GxRegex.IsMatch(AV10in_extended_key,"^vprv") || GxRegex.IsMatch(AV10in_extended_key,"^tpub") || GxRegex.IsMatch(AV10in_extended_key,"^upub") || GxRegex.IsMatch(AV10in_extended_key,"^vpub") )
            {
               AV11networkType = "TestNet";
               /* User Code */
                network = NBitcoin.Network.TestNet;
            }
            else
            {
               AV8error = "Not recognized extended type";
               /* User Code */
                network = NBitcoin.Network.Main;
            }
         }
         if ( GxRegex.IsMatch(AV10in_extended_key,"^xprv") || GxRegex.IsMatch(AV10in_extended_key,"^yprv") || GxRegex.IsMatch(AV10in_extended_key,"^zprv") || GxRegex.IsMatch(AV10in_extended_key,"^tprv") || GxRegex.IsMatch(AV10in_extended_key,"^uprv") || GxRegex.IsMatch(AV10in_extended_key,"^vprv") )
         {
            AV13isInputPrivate = true;
         }
         else
         {
            if ( GxRegex.IsMatch(AV10in_extended_key,"^xpub") || GxRegex.IsMatch(AV10in_extended_key,"^ypub") || GxRegex.IsMatch(AV10in_extended_key,"^zpub") || GxRegex.IsMatch(AV10in_extended_key,"^tpub") || GxRegex.IsMatch(AV10in_extended_key,"^upub") || GxRegex.IsMatch(AV10in_extended_key,"^vpub") )
            {
               AV14isInputPublic = true;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            if ( GxRegex.IsMatch(AV10in_extended_key,"^xprv") || GxRegex.IsMatch(AV10in_extended_key,"^xpub") || GxRegex.IsMatch(AV10in_extended_key,"^tprv") || GxRegex.IsMatch(AV10in_extended_key,"^tpub") )
            {
               AV9extendedKeyType = "x";
            }
            else if ( GxRegex.IsMatch(AV10in_extended_key,"^yprv") || GxRegex.IsMatch(AV10in_extended_key,"^ypub") || GxRegex.IsMatch(AV10in_extended_key,"^uprv") || GxRegex.IsMatch(AV10in_extended_key,"^upub") )
            {
               AV9extendedKeyType = "y";
            }
            else if ( GxRegex.IsMatch(AV10in_extended_key,"^zprv") || GxRegex.IsMatch(AV10in_extended_key,"^zpub") || GxRegex.IsMatch(AV10in_extended_key,"^vprv") || GxRegex.IsMatch(AV10in_extended_key,"^vpub") )
            {
               AV9extendedKeyType = "z";
            }
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
             string derivationScheme = AV10in_extended_key;
            /* User Code */
             System.Collections.Generic.Dictionary<uint, string[]> electrumMapping = new System.Collections.Generic.Dictionary<uint, string[]>();
            if ( AV13isInputPrivate )
            {
               /* User Code */
                electrumMapping.Add(0x0488ade4U, new[] { "legacy" });
               /* User Code */
                electrumMapping.Add(0x049d7878U, new string[] { "p2sh" });
               /* User Code */
                electrumMapping.Add(0x04b2430cU, new string[] { });
               /* User Code */
                electrumMapping.Add(0x04358394U, new[] { "legacy" });
               /* User Code */
                electrumMapping.Add(0x044a4e28U, new string[] { "p2sh" });
               /* User Code */
                electrumMapping.Add(0x45f18bcU, new string[] { });
            }
            else
            {
               if ( AV14isInputPublic )
               {
                  /* User Code */
                   electrumMapping.Add(0x0488b21eU, new[] { "legacy" });
                  /* User Code */
                   electrumMapping.Add(0x049d7cb2U, new string[] { "p2sh" });
                  /* User Code */
                   electrumMapping.Add(0x4b24746U, new string[] { });
                  /* User Code */
                   electrumMapping.Add(0x043587cfU, new[] { "legacy" });
                  /* User Code */
                   electrumMapping.Add(0x044a5262U, new string[] { "p2sh" });
                  /* User Code */
                   electrumMapping.Add(0x45f1cf6U, new string[] { });
               }
            }
            /* User Code */
             var data = NBitcoin.DataEncoders.Encoders.Base58Check.DecodeData(derivationScheme);
            /* User Code */
             var prefix = NBitcoin.Utils.ToUInt32(data, false);
            /* User Code */
             if (!electrumMapping.TryGetValue(prefix, out string[] labels))
            /* User Code */
             	throw new FormatException("!electrumMapping.TryGetValue(prefix, out string[] labels)");
            /* User Code */
             byte[] standardPrefix = null;
            if ( AV13isInputPrivate )
            {
               /* User Code */
                standardPrefix = NBitcoin.Utils.ToBytes(network == NBitcoin.Network.Main ? 0x0488ade4U : 0x04358394U, false);
            }
            else
            {
               if ( AV14isInputPublic )
               {
                  /* User Code */
                   standardPrefix = NBitcoin.Utils.ToBytes(network == NBitcoin.Network.Main ? 0x0488b21eU : 0x043587cfU, false);
               }
            }
            /* User Code */
             for (int i = 0; i < 4; i++)
            /* User Code */
             	data[i] = standardPrefix[i];
            if ( AV13isInputPrivate )
            {
               /* User Code */
                	derivationScheme = new NBitcoin.BitcoinExtKey(NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(data), network).ToString();
            }
            else
            {
               if ( AV14isInputPublic )
               {
                  /* User Code */
                   	derivationScheme = new NBitcoin.BitcoinExtPubKey(NBitcoin.DataEncoders.Encoders.Base58Check.EncodeData(data), network).ToString();
               }
            }
            /* User Code */
             AV12out_extended_key = derivationScheme;
            if ( GxRegex.IsMatch(AV12out_extended_key,"^xprv") || GxRegex.IsMatch(AV12out_extended_key,"^yprv") || GxRegex.IsMatch(AV12out_extended_key,"^zprv") || GxRegex.IsMatch(AV12out_extended_key,"^tprv") || GxRegex.IsMatch(AV12out_extended_key,"^uprv") || GxRegex.IsMatch(AV12out_extended_key,"^vprv") )
            {
               AV15isOutputPrivate = true;
            }
            else
            {
               if ( GxRegex.IsMatch(AV12out_extended_key,"^xpub") || GxRegex.IsMatch(AV12out_extended_key,"^ypub") || GxRegex.IsMatch(AV12out_extended_key,"^zpub") || GxRegex.IsMatch(AV12out_extended_key,"^tpub") || GxRegex.IsMatch(AV12out_extended_key,"^upub") || GxRegex.IsMatch(AV12out_extended_key,"^vpub") )
               {
                  AV16isOutputPublic = true;
               }
            }
            if ( AV13isInputPrivate && ! AV15isOutputPrivate )
            {
               AV8error = "prvkey version / pubkey mismatch";
               AV12out_extended_key = "";
            }
            else if ( AV14isInputPublic && ! AV16isOutputPublic )
            {
               AV8error = "pubkey version / prvkey mismatch";
               AV12out_extended_key = "";
            }
            GXt_SdtWallet1 = AV17wallet;
            new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
            AV17wallet = GXt_SdtWallet1;
            if ( StringUtil.StrCmp(AV11networkType, "TestNet") == 0 )
            {
               if ( StringUtil.StrCmp(AV17wallet.gxTpr_Networktype, "RegTest") == 0 )
               {
                  AV11networkType = "RegTest";
               }
            }
            if ( StringUtil.StrCmp(AV9extendedKeyType, "x") == 0 )
            {
               if ( StringUtil.StrCmp(AV17wallet.gxTpr_Wallettype, "BIP86") == 0 )
               {
                  AV9extendedKeyType = "t";
               }
            }
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
         AV12out_extended_key = "";
         AV11networkType = "";
         AV9extendedKeyType = "";
         AV8error = "";
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         /* GeneXus formulas. */
      }

      private string AV10in_extended_key ;
      private string AV12out_extended_key ;
      private string AV11networkType ;
      private string AV9extendedKeyType ;
      private string AV8error ;
      private bool AV13isInputPrivate ;
      private bool AV14isInputPublic ;
      private bool AV15isOutputPrivate ;
      private bool AV16isOutputPublic ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private string aP1_out_extended_key ;
      private string aP2_networkType ;
      private string aP3_extendedKeyType ;
      private string aP4_error ;
   }

}
