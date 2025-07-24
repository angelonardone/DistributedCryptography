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
   public class getprivatekeyfromaddresshistory : GXProcedure
   {
      public getprivatekeyfromaddresshistory( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getprivatekeyfromaddresshistory( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                           out string aP1_privateKey )
      {
         this.AV8oneAddressHistory = aP0_oneAddressHistory;
         this.AV9privateKey = "" ;
         initialize();
         ExecuteImpl();
         aP1_privateKey=this.AV9privateKey;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory )
      {
         execute(aP0_oneAddressHistory, out aP1_privateKey);
         return AV9privateKey ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtSDTAddressHistory aP0_oneAddressHistory ,
                                 out string aP1_privateKey )
      {
         this.AV8oneAddressHistory = aP0_oneAddressHistory;
         this.AV9privateKey = "" ;
         SubmitImpl();
         aP1_privateKey=this.AV9privateKey;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExtKeyInfo1 = AV21extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV21extKeyInfoRoot = GXt_SdtExtKeyInfo1;
         GXt_SdtWallet2 = AV45wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV45wallet = GXt_SdtWallet2;
         if ( ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "BrainWallet") == 0 ) || ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "ImportedWIF") == 0 ) )
         {
            GXt_SdtKeyInfo3 = AV46keyInfo;
            new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo3) ;
            AV46keyInfo = GXt_SdtKeyInfo3;
            AV9privateKey = StringUtil.Trim( AV46keyInfo.gxTpr_Privatekey);
         }
         else
         {
            if ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "BIP44") == 0 )
            {
               AV19deserializedExtPubKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickey;
               AV18deserializedExtKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Privatekey;
            }
            else if ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "BIP49") == 0 )
            {
               AV19deserializedExtPubKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh;
               AV18deserializedExtKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Privatekeysegwitp2sh;
            }
            else if ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "BIP84") == 0 )
            {
               AV19deserializedExtPubKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeysegwit;
               AV18deserializedExtKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Privatekeysegwit;
            }
            else if ( StringUtil.StrCmp(AV45wallet.gxTpr_Wallettype, "BIP86") == 0 )
            {
               AV19deserializedExtPubKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
               AV18deserializedExtKey = AV21extKeyInfoRoot.gxTpr_Extended.gxTpr_Privatekeytaproot;
            }
            else
            {
               AV10error = "We couldn't find the this type of wallet addresses";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               GXt_char4 = AV10error;
               new GeneXus.Programs.nbitcoin.derivekeysfromextkey(context ).execute(  AV18deserializedExtKey,  AV8oneAddressHistory.gxTpr_Addressgeneratedtype,  AV8oneAddressHistory.gxTpr_Addresscreationsequence,  AV8oneAddressHistory.gxTpr_Addresscreationsequence, out  AV32recAllKeyInfo, out  GXt_char4) ;
               AV10error = GXt_char4;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  AV47GXV1 = 1;
                  while ( AV47GXV1 <= AV32recAllKeyInfo.Count )
                  {
                     AV30oneKeyInfo = ((GeneXus.Programs.nbitcoin.SdtKeyInfo)AV32recAllKeyInfo.Item(AV47GXV1));
                     AV9privateKey = AV30oneKeyInfo.gxTpr_Privatekey;
                     cleanup();
                     if (true) return;
                     AV47GXV1 = (int)(AV47GXV1+1);
                  }
               }
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
         AV9privateKey = "";
         AV21extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV45wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV46keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo3 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV19deserializedExtPubKey = "";
         AV18deserializedExtKey = "";
         AV10error = "";
         GXt_char4 = "";
         AV32recAllKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography");
         AV30oneKeyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private int AV47GXV1 ;
      private string AV9privateKey ;
      private string AV19deserializedExtPubKey ;
      private string AV18deserializedExtKey ;
      private string AV10error ;
      private string GXt_char4 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV8oneAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV21extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV45wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV46keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo3 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> AV32recAllKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV30oneKeyInfo ;
      private string aP1_privateKey ;
   }

}
