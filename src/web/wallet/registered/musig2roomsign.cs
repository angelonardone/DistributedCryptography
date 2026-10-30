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
   public class musig2roomsign : GXProcedure
   {
      public musig2roomsign( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public musig2roomsign( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           out string aP2_txId ,
                           out string aP3_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV28roomId = aP1_roomId;
         this.AV32txId = "" ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP2_txId=this.AV32txId;
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_roomId ,
                                out string aP2_txId )
      {
         execute(aP0_groupId, aP1_roomId, out aP2_txId, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_roomId ,
                                 out string aP2_txId ,
                                 out string aP3_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV28roomId = aP1_roomId;
         this.AV32txId = "" ;
         this.AV11error = "" ;
         SubmitImpl();
         aP2_txId=this.AV32txId;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV17group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV19groupId, out  GXt_SdtGroup_SDT1) ;
         AV17group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV17group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV29rooms) ;
         AV16found = false;
         AV33GXV1 = 1;
         while ( AV33GXV1 <= AV29rooms.Count )
         {
            AV27room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV29rooms.Item(AV33GXV1));
            if ( AV27room.gxTpr_Roomid == AV28roomId )
            {
               AV16found = true;
               if (true) break;
            }
            AV33GXV1 = (int)(AV33GXV1+1);
         }
         if ( ! AV16found )
         {
            AV11error = "This signing room does not exist";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV12externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV12externalUserPublic = GXt_SdtExternalUserPublic2;
         AV21me = StringUtil.Trim( AV12externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         if ( ! ( StringUtil.StrCmp(AV27room.gxTpr_State, "signing") == 0 ) || ( StringUtil.StringSearch( AV27room.gxTpr_Signers, ","+AV21me+",", 1) == 0 ) )
         {
            AV11error = "You are not signing in this room";
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            cleanup();
            if (true) return;
         }
         GXt_SdtWalletInfo3 = AV31walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV31walletInfo = GXt_SdtWalletInfo3;
         GXt_SdtExtKeyInfo4 = AV14extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo4) ;
         AV14extKeyInfo = GXt_SdtExtKeyInfo4;
         AV8accountXprv = StringUtil.Trim( AV14extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8accountXprv)) )
         {
            AV11error = "Signing key not available: please re-enter your password";
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'DERIVE CHAIN KEYS' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV8accountXprv = "";
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV26receivingXprv = "";
            AV10changeXprv = "";
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV17group_sdt, out  AV18groupEO) ;
         AV25ok = AV22muSig2Processor.fromsdt(AV18groupEO);
         AV20jsonText = AV22muSig2Processor.sign(AV27room.gxTpr_Sessionjson, AV27room.gxTpr_Noncesmap, AV26receivingXprv, AV10changeXprv, AV31walletInfo.gxTpr_Networktype);
         AV26receivingXprv = "";
         AV10changeXprv = "";
         AV30roundResult.FromJSonString(AV20jsonText, null);
         if ( ! AV30roundResult.gxTpr_Success )
         {
            AV11error = AV30roundResult.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         GXt_char5 = AV11error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV19groupId,  AV28roomId,  "partials",  AV30roundResult.gxTpr_Partialsignatures, out  GXt_char5) ;
         AV11error = GXt_char5;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV17group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV29rooms) ;
         AV34GXV2 = 1;
         while ( AV34GXV2 <= AV29rooms.Count )
         {
            AV27room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV29rooms.Item(AV34GXV2));
            if ( AV27room.gxTpr_Roomid == AV28roomId )
            {
               if (true) break;
            }
            AV34GXV2 = (int)(AV34GXV2+1);
         }
         AV9allIn = true;
         AV24names = (GxSimpleCollection<string>)(GxRegex.Split(AV27room.gxTpr_Signers,","));
         AV35GXV3 = 1;
         while ( AV35GXV3 <= AV24names.Count )
         {
            AV23name = ((string)AV24names.Item(AV35GXV3));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV23name))) && ( StringUtil.StringSearch( AV27room.gxTpr_Partialsmap, "\""+StringUtil.Trim( AV23name)+"\":", 1) == 0 ) )
            {
               AV9allIn = false;
            }
            AV35GXV3 = (int)(AV35GXV3+1);
         }
         if ( AV9allIn )
         {
            GXt_char5 = AV11error;
            new GeneXus.Programs.wallet.registered.finalizemusig2room(context ).execute(  AV19groupId,  AV28roomId, out  GXt_char5) ;
            AV11error = GXt_char5;
         }
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV17group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV29rooms) ;
         AV36GXV4 = 1;
         while ( AV36GXV4 <= AV29rooms.Count )
         {
            AV27room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV29rooms.Item(AV36GXV4));
            if ( ( AV27room.gxTpr_Roomid == AV28roomId ) && ( StringUtil.StrCmp(AV27room.gxTpr_State, "done") == 0 ) )
            {
               AV32txId = StringUtil.Trim( AV27room.gxTpr_Txid);
            }
            AV36GXV4 = (int)(AV36GXV4+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'DERIVE CHAIN KEYS' Routine */
         returnInSub = false;
         AV13extKeyCreateChain.gxTpr_Createextkeytype = 70;
         AV13extKeyCreateChain.gxTpr_Extendedprivatekey = AV8accountXprv;
         AV13extKeyCreateChain.gxTpr_Networktype = AV31walletInfo.gxTpr_Networktype;
         AV13extKeyCreateChain.gxTpr_Keypath = "2";
         GXt_char5 = AV11error;
         new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV13extKeyCreateChain,  "", out  AV15extKeyInfoChain, out  GXt_char5) ;
         AV11error = GXt_char5;
         AV26receivingXprv = StringUtil.Trim( AV15extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV15extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
            AV13extKeyCreateChain.gxTpr_Keypath = "3";
            GXt_char5 = AV11error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV13extKeyCreateChain,  "", out  AV15extKeyInfoChain, out  GXt_char5) ;
            AV11error = GXt_char5;
            AV10changeXprv = StringUtil.Trim( AV15extKeyInfoChain.gxTpr_Extended.gxTpr_Privatekey);
         }
         AV15extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV13extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
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
         AV32txId = "";
         AV11error = "";
         AV17group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV29rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV27room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV12externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV21me = "";
         AV31walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV14extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo4 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV8accountXprv = "";
         AV26receivingXprv = "";
         AV10changeXprv = "";
         AV18groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV22muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV20jsonText = "";
         AV30roundResult = new GeneXus.Programs.wallet.registered.SdtMuSig2RoundResult(context);
         AV24names = new GxSimpleCollection<string>();
         AV23name = "";
         AV13extKeyCreateChain = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV15extKeyInfoChain = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char5 = "";
         /* GeneXus formulas. */
      }

      private int AV33GXV1 ;
      private int AV34GXV2 ;
      private int AV35GXV3 ;
      private int AV36GXV4 ;
      private string AV11error ;
      private string AV8accountXprv ;
      private string AV26receivingXprv ;
      private string AV10changeXprv ;
      private string GXt_char5 ;
      private bool AV16found ;
      private bool returnInSub ;
      private bool AV25ok ;
      private bool AV9allIn ;
      private string AV20jsonText ;
      private string AV32txId ;
      private string AV21me ;
      private string AV23name ;
      private Guid AV19groupId ;
      private Guid AV28roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV29rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV27room ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV12externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV31walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV14extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo4 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV18groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV22muSig2Processor ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoundResult AV30roundResult ;
      private GxSimpleCollection<string> AV24names ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV13extKeyCreateChain ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV15extKeyInfoChain ;
      private string aP2_txId ;
      private string aP3_error ;
   }

}
