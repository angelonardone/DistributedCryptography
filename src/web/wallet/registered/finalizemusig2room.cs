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
   public class finalizemusig2room : GXProcedure
   {
      public finalizemusig2room( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public finalizemusig2room( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV21roomId = aP1_roomId;
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
         this.AV14groupId = aP0_groupId;
         this.AV21roomId = aP1_roomId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV12group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV12group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV22rooms) ;
         AV11found = false;
         AV30GXV1 = 1;
         while ( AV30GXV1 <= AV22rooms.Count )
         {
            AV20room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV22rooms.Item(AV30GXV1));
            if ( AV20room.gxTpr_Roomid == AV21roomId )
            {
               AV11found = true;
               if (true) break;
            }
            AV30GXV1 = (int)(AV30GXV1+1);
         }
         if ( ! AV11found || ! ( StringUtil.StrCmp(AV20room.gxTpr_State, "signing") == 0 ) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtWalletInfo2 = AV29walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo2) ;
         AV29walletInfo = GXt_SdtWalletInfo2;
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV12group_sdt, out  AV13groupEO) ;
         AV18ok = AV17muSig2Processor.fromsdt(AV13groupEO);
         if ( ! AV18ok )
         {
            AV8error = "We couldn't load the group";
            cleanup();
            if (true) return;
         }
         AV16jsonText = AV17muSig2Processor.finalize(AV20room.gxTpr_Sessionjson, AV20room.gxTpr_Noncesmap, AV20room.gxTpr_Partialsmap, AV29walletInfo.gxTpr_Networktype);
         AV25spendResult.FromJSonString(AV16jsonText, null);
         if ( ! AV25spendResult.gxTpr_Success )
         {
            AV8error = AV25spendResult.gxTpr_Error;
            new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV14groupId,  AV21roomId,  "abort",  "The payment could not be completed: "+AV25spendResult.gxTpr_Error, out  AV9errorAbort) ;
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV8error;
         new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV25spendResult.gxTpr_Txhex, out  AV27TransactionId, out  GXt_char3) ;
         AV8error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            if ( StringUtil.StringSearch( StringUtil.Lower( AV8error), "already", 1) > 0 )
            {
               AV8error = "";
               AV27TransactionId = AV25spendResult.gxTpr_Txid;
            }
            else
            {
               cleanup();
               if (true) return;
            }
         }
         AV23sessionData.FromJSonString(AV20room.gxTpr_Sessionjson, null);
         GXt_objcol_SdtSDTAddressHistory4 = AV15history;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory4) ;
         AV15history = GXt_objcol_SdtSDTAddressHistory4;
         AV31GXV2 = 1;
         while ( AV31GXV2 <= AV23sessionData.gxTpr_Inputs.Count )
         {
            AV24sessionInput = ((GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem)AV23sessionData.gxTpr_Inputs.Item(AV31GXV2));
            AV32GXV3 = 1;
            while ( AV32GXV3 <= AV15history.Count )
            {
               AV19oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV15history.Item(AV32GXV3));
               if ( ( StringUtil.StrCmp(StringUtil.Trim( AV19oneTransaction.gxTpr_Receivedtransactionid), StringUtil.Trim( AV24sessionInput.gxTpr_Txid)) == 0 ) && ( AV19oneTransaction.gxTpr_Recivedn == AV24sessionInput.gxTpr_Vout ) )
               {
                  AV19oneTransaction.gxTpr_Senttransactionid = StringUtil.Trim( AV27TransactionId);
                  AV28transactionsToSend.Add(AV19oneTransaction, 0);
                  if (true) break;
               }
               AV32GXV3 = (int)(AV32GXV3+1);
            }
            AV31GXV2 = (int)(AV31GXV2+1);
         }
         AV26transactionFileName = StringUtil.Trim( AV12group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         if ( AV28transactionsToSend.Count > 0 )
         {
            GXt_char3 = AV10errorAfter;
            new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV26transactionFileName,  AV27TransactionId,  AV28transactionsToSend, out  GXt_char3) ;
            AV10errorAfter = GXt_char3;
         }
         GXt_char3 = AV10errorAfter;
         new GeneXus.Programs.wallet.recordsentoutputs(context ).execute(  AV26transactionFileName,  AV27TransactionId,  AV25spendResult.gxTpr_Txhex, out  GXt_char3) ;
         AV10errorAfter = GXt_char3;
         GXt_char3 = AV8error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV14groupId,  AV21roomId,  "done",  StringUtil.Trim( AV27TransactionId), out  GXt_char3) ;
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
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV22rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV20room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV29walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo2 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV17muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV16jsonText = "";
         AV25spendResult = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult(context);
         AV9errorAbort = "";
         AV27TransactionId = "";
         AV23sessionData = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData(context);
         AV15history = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory4 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV24sessionInput = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem(context);
         AV19oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV28transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV26transactionFileName = "";
         AV10errorAfter = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV30GXV1 ;
      private int AV31GXV2 ;
      private int AV32GXV3 ;
      private string AV8error ;
      private string AV9errorAbort ;
      private string AV27TransactionId ;
      private string AV26transactionFileName ;
      private string AV10errorAfter ;
      private string GXt_char3 ;
      private bool AV11found ;
      private bool AV18ok ;
      private string AV16jsonText ;
      private Guid AV14groupId ;
      private Guid AV21roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV22rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV20room ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV29walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo2 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV13groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV17muSig2Processor ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendResult AV25spendResult ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData AV23sessionData ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV15history ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory4 ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem AV24sessionInput ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV19oneTransaction ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV28transactionsToSend ;
      private string aP2_error ;
   }

}
