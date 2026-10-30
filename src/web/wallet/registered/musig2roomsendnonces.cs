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
   public class musig2roomsendnonces : GXProcedure
   {
      public musig2roomsendnonces( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public musig2roomsendnonces( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           out string aP2_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV17roomId = aP1_roomId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_roomId )
      {
         execute(aP0_groupId, aP1_roomId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_roomId ,
                                 out string aP2_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV17roomId = aP1_roomId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV12groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV10group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV18rooms) ;
         AV9found = false;
         AV29GXV1 = 1;
         while ( AV29GXV1 <= AV18rooms.Count )
         {
            AV16room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV18rooms.Item(AV29GXV1));
            if ( AV16room.gxTpr_Roomid == AV17roomId )
            {
               AV9found = true;
               if (true) break;
            }
            AV29GXV1 = (int)(AV29GXV1+1);
         }
         if ( ! AV9found || ! ( StringUtil.StrCmp(AV16room.gxTpr_State, "signing") == 0 ) )
         {
            AV8error = "This signing room is not signing";
            cleanup();
            if (true) return;
         }
         GXt_SdtWalletInfo2 = AV20walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo2) ;
         AV20walletInfo = GXt_SdtWalletInfo2;
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV10group_sdt, out  AV11groupEO) ;
         AV15ok = AV14muSig2Processor.fromsdt(AV11groupEO);
         if ( ! AV15ok )
         {
            AV8error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV25prevTxs = "{";
         AV26sessionData.FromJSonString(AV16room.gxTpr_Sessionjson, null);
         AV30GXV2 = 1;
         while ( AV30GXV2 <= AV26sessionData.gxTpr_Inputs.Count )
         {
            AV27sessionInput = ((GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem)AV26sessionData.gxTpr_Inputs.Item(AV30GXV2));
            if ( StringUtil.StringSearch( AV25prevTxs, "\""+StringUtil.Trim( AV27sessionInput.gxTpr_Txid)+"\"", 1) == 0 )
            {
               GXt_char3 = AV21electrumError;
               new GeneXus.Programs.electrum.get_transaction(context ).execute(  StringUtil.Trim( AV27sessionInput.gxTpr_Txid),  20, out  AV24messageResponse, out  GXt_char3) ;
               AV21electrumError = GXt_char3;
               AV22electrumTx.FromJSonString(AV24messageResponse, null);
               if ( ! ( StringUtil.StrCmp(AV25prevTxs, "{") == 0 ) )
               {
                  AV25prevTxs += ",";
               }
               AV25prevTxs += "\"" + StringUtil.Trim( AV27sessionInput.gxTpr_Txid) + "\":\"" + StringUtil.Trim( AV22electrumTx.gxTpr_Result.gxTpr_Hex) + "\"";
            }
            AV30GXV2 = (int)(AV30GXV2+1);
         }
         AV25prevTxs += "}";
         AV13jsonText = AV14muSig2Processor.verifycoins(AV16room.gxTpr_Sessionjson, AV25prevTxs, AV20walletInfo.gxTpr_Networktype);
         AV28verify.FromJSonString(AV13jsonText, null);
         if ( ! AV28verify.gxTpr_Success )
         {
            AV8error = "The coins of the payment are not what it says: " + AV28verify.gxTpr_Error;
            new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV12groupId,  AV17roomId,  "abort",  AV8error, out  AV23errorAbort) ;
            cleanup();
            if (true) return;
         }
         AV13jsonText = AV14muSig2Processor.createnonces(AV16room.gxTpr_Sessionjson, StringUtil.Trim( AV10group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV10group_sdt.gxTpr_Extpubkeymultisigchange), AV20walletInfo.gxTpr_Networktype);
         AV19roundResult.FromJSonString(AV13jsonText, null);
         if ( ! AV19roundResult.gxTpr_Success )
         {
            AV8error = AV19roundResult.gxTpr_Error;
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV8error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV12groupId,  AV17roomId,  "nonces",  AV19roundResult.gxTpr_Publicnonces, out  GXt_char3) ;
         AV8error = GXt_char3;
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
         AV8error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV16room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV20walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo2 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV11groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV14muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV25prevTxs = "";
         AV26sessionData = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData(context);
         AV27sessionInput = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem(context);
         AV21electrumError = "";
         AV24messageResponse = "";
         AV22electrumTx = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId(context);
         AV13jsonText = "";
         AV28verify = new GeneXus.Programs.wallet.registered.SdtMuSig2VsizeResult(context);
         AV23errorAbort = "";
         AV19roundResult = new GeneXus.Programs.wallet.registered.SdtMuSig2RoundResult(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV29GXV1 ;
      private int AV30GXV2 ;
      private string AV8error ;
      private string AV21electrumError ;
      private string AV23errorAbort ;
      private string GXt_char3 ;
      private bool AV9found ;
      private bool AV15ok ;
      private string AV25prevTxs ;
      private string AV24messageResponse ;
      private string AV13jsonText ;
      private Guid AV12groupId ;
      private Guid AV17roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV18rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV16room ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV20walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV11groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV14muSig2Processor ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData AV26sessionData ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem AV27sessionInput ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV22electrumTx ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2VsizeResult AV28verify ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoundResult AV19roundResult ;
      private string aP2_error ;
   }

}
