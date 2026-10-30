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
namespace GeneXus.Programs.wallet.registered {
   public class musig2roomsignnow : GXProcedure
   {
      public musig2roomsignnow( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public musig2roomsignnow( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           out string aP2_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV33roomId = aP1_roomId;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV12error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_roomId )
      {
         execute(aP0_groupId, aP1_roomId, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_roomId ,
                                 out string aP2_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV33roomId = aP1_roomId;
         this.AV12error = "" ;
         SubmitImpl();
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV16group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV19groupId, out  GXt_SdtGroup_SDT1) ;
         AV16group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV16group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV34rooms) ;
         AV15found = false;
         AV44GXV1 = 1;
         while ( AV44GXV1 <= AV34rooms.Count )
         {
            AV32room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV34rooms.Item(AV44GXV1));
            if ( AV32room.gxTpr_Roomid == AV33roomId )
            {
               AV15found = true;
               if (true) break;
            }
            AV44GXV1 = (int)(AV44GXV1+1);
         }
         if ( ! AV15found )
         {
            AV12error = "This signing room does not exist";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV13externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV13externalUserPublic = GXt_SdtExternalUserPublic2;
         AV23me = StringUtil.Trim( AV13externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         if ( ! ( StringUtil.StrCmp(AV32room.gxTpr_State, "gathering") == 0 ) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV32room.gxTpr_Initiator), AV23me) == 0 ) )
         {
            AV12error = "Only the member who opened the room starts the signing, while it waits for its members";
            cleanup();
            if (true) return;
         }
         GXt_SdtWalletInfo3 = AV42walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV42walletInfo = GXt_SdtWalletInfo3;
         AV22k = AV16group_sdt.gxTpr_Minimumshares;
         AV37signersJson = "[\"" + AV23me + "\"";
         AV11count = 1;
         AV31presentCount = 0;
         AV26names = (GxSimpleCollection<string>)(GxRegex.Split(AV32room.gxTpr_Present,","));
         AV45GXV2 = 1;
         while ( AV45GXV2 <= AV26names.Count )
         {
            AV25name = ((string)AV26names.Item(AV45GXV2));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV25name))) )
            {
               if ( ! ( ( AV16group_sdt.gxTpr_Grouptype == 80 ) && ( StringUtil.StrCmp(StringUtil.Trim( AV25name), StringUtil.Trim( AV16group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 ) ) )
               {
                  AV31presentCount = (short)(AV31presentCount+1);
                  if ( ( AV11count < AV22k ) && ! ( StringUtil.StrCmp(StringUtil.Trim( AV25name), AV23me) == 0 ) )
                  {
                     AV37signersJson += ",\"" + StringUtil.Trim( AV25name) + "\"";
                     AV11count = (short)(AV11count+1);
                  }
               }
            }
            AV45GXV2 = (int)(AV45GXV2+1);
         }
         AV37signersJson += "]";
         if ( AV11count < AV22k )
         {
            AV12error = "Only " + StringUtil.Trim( StringUtil.Str( (decimal)(AV31presentCount), 4, 0)) + " of the " + StringUtil.Trim( StringUtil.Str( (decimal)(AV22k), 4, 0)) + " signers needed are here";
            cleanup();
            if (true) return;
         }
         AV38signerTotal = 0;
         AV46GXV3 = 1;
         while ( AV46GXV3 <= AV16group_sdt.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV16group_sdt.gxTpr_Contact.Item(AV46GXV3));
            AV38signerTotal = (short)(AV38signerTotal+1);
            AV46GXV3 = (int)(AV46GXV3+1);
         }
         if ( ( AV16group_sdt.gxTpr_Grouptype == 90 ) && ( AV31presentCount == AV38signerTotal ) )
         {
            AV37signersJson = "all";
         }
         GXt_int4 = AV20groupType;
         new GeneXus.Programs.wallet.registered.setgroupeditwallet(context ).execute(  AV19groupId, out  GXt_int4) ;
         AV20groupType = GXt_int4;
         AV39transactionFee = NumberUtil.Val( "0.00001000", ".");
         GXt_objcol_SdtSDTAddressHistory5 = AV40transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV32room.gxTpr_Description,  AV32room.gxTpr_Amountbtc,  AV39transactionFee, out  GXt_objcol_SdtSDTAddressHistory5) ;
         AV40transactionsToSend = GXt_objcol_SdtSDTAddressHistory5;
         if ( AV40transactionsToSend.Count == 0 )
         {
            AV12error = "The group has no coins to send: update its balance";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'BUILD UTXOS' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         GXt_char6 = AV10changeTo;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char6) ;
         AV10changeTo = GXt_char6;
         AV9changeSequence = -1;
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem7 = AV35sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem7) ;
         AV35sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem7;
         AV47GXV4 = 1;
         while ( AV47GXV4 <= AV35sdt_Addresses.Count )
         {
            AV28one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV35sdt_Addresses.Item(AV47GXV4));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV28one_sdt_address.gxTpr_Address), StringUtil.Trim( AV10changeTo)) == 0 )
            {
               AV9changeSequence = AV28one_sdt_address.gxTpr_Creationsequence;
               if (true) break;
            }
            AV47GXV4 = (int)(AV47GXV4+1);
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV16group_sdt, out  AV18groupEO) ;
         AV27ok = AV24muSig2Processor.fromsdt(AV18groupEO);
         if ( ! AV27ok )
         {
            AV12error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV21jsonText = AV24muSig2Processor.buildsession(AV41utxos.ToJSonString(false), StringUtil.Trim( AV32room.gxTpr_Sendto), StringUtil.Trim( StringUtil.Str( AV32room.gxTpr_Amountbtc, 17, 8)), StringUtil.Trim( AV10changeTo), (int)(AV9changeSequence), StringUtil.Trim( StringUtil.Str( AV39transactionFee, 17, 8)), AV32room.gxTpr_Sendall, AV37signersJson, AV42walletInfo.gxTpr_Networktype);
         AV36sessionInfo.FromJSonString(AV21jsonText, null);
         if ( ! AV36sessionInfo.gxTpr_Success )
         {
            AV12error = AV36sessionInfo.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         GXt_char6 = AV12error;
         GXt_int4 = (short)(AV8blocks);
         new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV36sessionInfo.gxTpr_Vsize,  6,  "conservative", out  AV14fee, out  GXt_int4, out  GXt_char6) ;
         AV8blocks = GXt_int4;
         AV12error = GXt_char6;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV12error = "The network fee could not be calculated: " + AV12error;
            cleanup();
            if (true) return;
         }
         if ( ( AV14fee <= Convert.ToDecimal( 0 )) )
         {
            AV43feeRate = NumberUtil.Val( "0.00001000", ".");
            AV14fee = (decimal)(AV43feeRate/ (decimal)(1000)*AV36sessionInfo.gxTpr_Vsize);
         }
         if ( ! AV32room.gxTpr_Sendall && ( AV36sessionInfo.gxTpr_Totalinbtc < AV32room.gxTpr_Amountbtc + AV14fee ) )
         {
            GXt_objcol_SdtSDTAddressHistory5 = AV40transactionsToSend;
            new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV32room.gxTpr_Description,  AV32room.gxTpr_Amountbtc,  AV14fee, out  GXt_objcol_SdtSDTAddressHistory5) ;
            AV40transactionsToSend = GXt_objcol_SdtSDTAddressHistory5;
            /* Execute user subroutine: 'BUILD UTXOS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV21jsonText = AV24muSig2Processor.buildsession(AV41utxos.ToJSonString(false), StringUtil.Trim( AV32room.gxTpr_Sendto), StringUtil.Trim( StringUtil.Str( AV32room.gxTpr_Amountbtc, 17, 8)), StringUtil.Trim( AV10changeTo), (int)(AV9changeSequence), StringUtil.Trim( StringUtil.Str( AV14fee, 17, 8)), AV32room.gxTpr_Sendall, AV37signersJson, AV42walletInfo.gxTpr_Networktype);
         AV36sessionInfo.FromJSonString(AV21jsonText, null);
         if ( ! AV36sessionInfo.gxTpr_Success )
         {
            AV12error = AV36sessionInfo.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         GXt_char6 = AV12error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV19groupId,  AV33roomId,  "start",  AV36sessionInfo.gxTpr_Session, out  GXt_char6) ;
         AV12error = GXt_char6;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            GXt_char6 = AV12error;
            new GeneXus.Programs.wallet.registered.musig2roomsendnonces(context ).execute(  AV19groupId,  AV33roomId, out  GXt_char6) ;
            AV12error = GXt_char6;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'BUILD UTXOS' Routine */
         returnInSub = false;
         AV41utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV48GXV5 = 1;
         while ( AV48GXV5 <= AV40transactionsToSend.Count )
         {
            AV29oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV40transactionsToSend.Item(AV48GXV5));
            AV30oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
            AV30oneUtxo.gxTpr_Txid = StringUtil.Trim( AV29oneTransaction.gxTpr_Receivedtransactionid);
            AV30oneUtxo.gxTpr_Vout = AV29oneTransaction.gxTpr_Recivedn;
            AV30oneUtxo.gxTpr_Amountbtc = StringUtil.Trim( StringUtil.Str( AV29oneTransaction.gxTpr_Balance, 17, 8));
            AV30oneUtxo.gxTpr_Sequence = AV29oneTransaction.gxTpr_Addresscreationsequence;
            if ( (Convert.ToDecimal( AV29oneTransaction.gxTpr_Addressgeneratedtype ) == NumberUtil.Val( "3", ".") ) )
            {
               AV30oneUtxo.gxTpr_Ischange = true;
            }
            else
            {
               AV30oneUtxo.gxTpr_Ischange = false;
            }
            AV30oneUtxo.gxTpr_Address = StringUtil.Trim( AV29oneTransaction.gxTpr_Receivedaddress);
            AV41utxos.Add(AV30oneUtxo, 0);
            AV48GXV5 = (int)(AV48GXV5+1);
         }
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
         AV12error = "";
         AV16group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV34rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV32room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV13externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV23me = "";
         AV42walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV37signersJson = "";
         AV26names = new GxSimpleCollection<string>();
         AV25name = "";
         AV17groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV40transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV10changeTo = "";
         AV35sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem7 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV28one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV18groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV24muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV21jsonText = "";
         AV41utxos = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo>( context, "DelegatedUtxo", "distributedcryptography");
         AV36sessionInfo = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionInfo(context);
         GXt_objcol_SdtSDTAddressHistory5 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_char6 = "";
         AV29oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV30oneUtxo = new GeneXus.Programs.wallet.registered.SdtDelegatedUtxo(context);
         /* GeneXus formulas. */
      }

      private short AV22k ;
      private short AV11count ;
      private short AV31presentCount ;
      private short AV38signerTotal ;
      private short AV20groupType ;
      private short GXt_int4 ;
      private int AV44GXV1 ;
      private int AV45GXV2 ;
      private int AV46GXV3 ;
      private int AV47GXV4 ;
      private int AV48GXV5 ;
      private long AV9changeSequence ;
      private long AV8blocks ;
      private decimal AV39transactionFee ;
      private decimal AV14fee ;
      private decimal AV43feeRate ;
      private string AV12error ;
      private string AV10changeTo ;
      private string GXt_char6 ;
      private bool AV15found ;
      private bool returnInSub ;
      private bool AV27ok ;
      private string AV37signersJson ;
      private string AV21jsonText ;
      private string AV23me ;
      private string AV25name ;
      private Guid AV19groupId ;
      private Guid AV33roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV34rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV32room ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV13externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV42walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GxSimpleCollection<string> AV26names ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17groupContact ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV40transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV35sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem7 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV28one_sdt_address ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV18groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV24muSig2Processor ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtDelegatedUtxo> AV41utxos ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionInfo AV36sessionInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory5 ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV29oneTransaction ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedUtxo AV30oneUtxo ;
      private string aP2_error ;
   }

}
