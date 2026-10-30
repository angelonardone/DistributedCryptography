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
   public class applymusig2roommessage : GXProcedure
   {
      public applymusig2roommessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public applymusig2roommessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage aP1_message ,
                           out string aP2_notice ,
                           out bool aP3_mustSendNonces ,
                           out bool aP4_mustFinalize ,
                           out string aP5_error )
      {
         this.AV12group_sdt = aP0_group_sdt;
         this.AV16message = aP1_message;
         this.AV22notice = "" ;
         this.AV19mustSendNonces = false ;
         this.AV18mustFinalize = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_notice=this.AV22notice;
         aP3_mustSendNonces=this.AV19mustSendNonces;
         aP4_mustFinalize=this.AV18mustFinalize;
         aP5_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage aP1_message ,
                                out string aP2_notice ,
                                out bool aP3_mustSendNonces ,
                                out bool aP4_mustFinalize )
      {
         execute(aP0_group_sdt, aP1_message, out aP2_notice, out aP3_mustSendNonces, out aP4_mustFinalize, out aP5_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage aP1_message ,
                                 out string aP2_notice ,
                                 out bool aP3_mustSendNonces ,
                                 out bool aP4_mustFinalize ,
                                 out string aP5_error )
      {
         this.AV12group_sdt = aP0_group_sdt;
         this.AV16message = aP1_message;
         this.AV22notice = "" ;
         this.AV19mustSendNonces = false ;
         this.AV18mustFinalize = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_notice=this.AV22notice;
         aP3_mustSendNonces=this.AV19mustSendNonces;
         aP4_mustFinalize=this.AV18mustFinalize;
         aP5_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV19mustSendNonces = false;
         AV18mustFinalize = false;
         AV22notice = "";
         AV11from = StringUtil.Trim( AV16message.gxTpr_Fromusername);
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV16message.gxTpr_Referencegroupid, out  AV27rooms) ;
         AV10found = false;
         AV42GXV1 = 1;
         while ( AV42GXV1 <= AV27rooms.Count )
         {
            AV24oneRoom = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV27rooms.Item(AV42GXV1));
            if ( AV24oneRoom.gxTpr_Roomid == AV16message.gxTpr_Roomid )
            {
               AV26room = AV24oneRoom;
               AV10found = true;
               if (true) break;
            }
            AV42GXV1 = (int)(AV42GXV1+1);
         }
         if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "invite") == 0 )
         {
            if ( AV10found )
            {
               cleanup();
               if (true) return;
            }
            AV25proposal.FromJSonString(AV16message.gxTpr_Data, null);
            AV26room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
            AV26room.gxTpr_Roomid = AV16message.gxTpr_Roomid;
            AV26room.gxTpr_Referencegroupid = AV16message.gxTpr_Referencegroupid;
            AV26room.gxTpr_Initiator = AV11from;
            AV26room.gxTpr_Description = AV25proposal.gxTpr_Description;
            AV26room.gxTpr_Sendto = AV25proposal.gxTpr_Sendto;
            AV26room.gxTpr_Amountbtc = AV25proposal.gxTpr_Amountbtc;
            AV26room.gxTpr_Sendall = AV25proposal.gxTpr_Sendall;
            AV26room.gxTpr_Createdunix = AV16message.gxTpr_Datetimeunix;
            AV26room.gxTpr_Gatherdeadlineunix = AV25proposal.gxTpr_Gatherdeadlineunix;
            AV26room.gxTpr_State = "gathering";
            AV26room.gxTpr_Present = ","+AV11from+",";
            GXt_char1 = AV22notice;
            new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV11from, out  GXt_char1) ;
            AV22notice = GXt_char1 + " opened a payment session in the group " + StringUtil.Trim( AV12group_sdt.gxTpr_Groupname) + ": " + StringUtil.Trim( AV26room.gxTpr_Description) + ". Join it in its Signatures tab within 15 minutes";
         }
         else
         {
            if ( ! AV10found )
            {
               AV9error = "a message for a signing room I do not have";
               cleanup();
               if (true) return;
            }
            if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "here") == 0 )
            {
               if ( ( StringUtil.StrCmp(AV26room.gxTpr_State, "gathering") == 0 ) && ( StringUtil.StringSearch( AV26room.gxTpr_Present, ","+AV11from+",", 1) == 0 ) )
               {
                  AV26room.gxTpr_Present = AV26room.gxTpr_Present+AV11from+",";
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "start") == 0 )
            {
               if ( ! ( StringUtil.StrCmp(AV11from, StringUtil.Trim( AV26room.gxTpr_Initiator)) == 0 ) )
               {
                  AV9error = "only the member who opened the room starts the signing";
                  cleanup();
                  if (true) return;
               }
               if ( ! ( StringUtil.StrCmp(AV26room.gxTpr_State, "gathering") == 0 ) )
               {
                  cleanup();
                  if (true) return;
               }
               /* Execute user subroutine: 'LOAD PROCESSOR' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV14jsonText = AV17muSig2Processor.describesession(AV16message.gxTpr_Data, StringUtil.Trim( AV12group_sdt.gxTpr_Extpubkeymultisigreceiving), StringUtil.Trim( AV12group_sdt.gxTpr_Extpubkeymultisigchange), AV29walletInfo.gxTpr_Networktype);
               AV28sessionInfo.FromJSonString(AV14jsonText, null);
               if ( ! AV28sessionInfo.gxTpr_Success )
               {
                  AV26room.gxTpr_State = "aborted";
                  AV26room.gxTpr_Info = "The payment is not valid: "+AV28sessionInfo.gxTpr_Error;
               }
               else
               {
                  if ( ! ( ( StringUtil.StrCmp(StringUtil.Trim( AV28sessionInfo.gxTpr_Sendto), StringUtil.Trim( AV26room.gxTpr_Sendto)) == 0 ) ) || ( ! AV26room.gxTpr_Sendall && ! ( AV28sessionInfo.gxTpr_Amountbtc == AV26room.gxTpr_Amountbtc ) ) )
                  {
                     AV26room.gxTpr_State = "aborted";
                     AV26room.gxTpr_Info = "The transaction does not pay what the room proposed";
                  }
                  else
                  {
                     AV26room.gxTpr_Sessionjson = AV16message.gxTpr_Data;
                     AV26room.gxTpr_Sessionid = StringUtil.Trim( AV28sessionInfo.gxTpr_Sessionid);
                     AV26room.gxTpr_Signers = ","+StringUtil.Trim( AV28sessionInfo.gxTpr_Signers)+",";
                     AV26room.gxTpr_Feebtc = AV28sessionInfo.gxTpr_Feebtc;
                     AV26room.gxTpr_Vsize = AV28sessionInfo.gxTpr_Vsize;
                     AV26room.gxTpr_Amountbtc = AV28sessionInfo.gxTpr_Amountbtc;
                     AV26room.gxTpr_Noncesmap = "";
                     AV26room.gxTpr_Partialsmap = "";
                     AV26room.gxTpr_State = "signing";
                     AV26room.gxTpr_Signdeadlineunix = (long)(AV16message.gxTpr_Datetimeunix+180000);
                     AV19mustSendNonces = AV28sessionInfo.gxTpr_Iamsigner;
                  }
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "nonces") == 0 )
            {
               if ( ( StringUtil.StrCmp(AV26room.gxTpr_State, "signing") == 0 ) && ( StringUtil.StringSearch( AV26room.gxTpr_Signers, ","+AV11from+",", 1) > 0 ) )
               {
                  AV15map = AV26room.gxTpr_Noncesmap;
                  /* Execute user subroutine: 'ADD TO MAP' */
                  S121 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
                  AV26room.gxTpr_Noncesmap = AV15map;
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "partials") == 0 )
            {
               if ( ( StringUtil.StrCmp(AV26room.gxTpr_State, "signing") == 0 ) && ( StringUtil.StringSearch( AV26room.gxTpr_Signers, ","+AV11from+",", 1) > 0 ) )
               {
                  AV15map = AV26room.gxTpr_Partialsmap;
                  /* Execute user subroutine: 'ADD TO MAP' */
                  S121 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
                  AV26room.gxTpr_Partialsmap = AV15map;
                  /* Execute user subroutine: 'ALL IN MAP' */
                  S131 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
                  AV18mustFinalize = AV8allIn;
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "chat") == 0 )
            {
               if ( ( ( StringUtil.StrCmp(AV26room.gxTpr_State, "gathering") == 0 ) || ( StringUtil.StrCmp(AV26room.gxTpr_State, "signing") == 0 ) ) && ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV16message.gxTpr_Data))) )
               {
                  GXt_dtime2 = AV32chatTime;
                  new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV16message.gxTpr_Datetimeunix, out  GXt_dtime2) ;
                  AV32chatTime = GXt_dtime2;
                  AV33hh = (short)(DateTimeUtil.Hour( AV32chatTime));
                  AV34mm = (short)(DateTimeUtil.Minute( AV32chatTime));
                  AV31chatText = StringUtil.Trim( AV16message.gxTpr_Data);
                  if ( StringUtil.Len( AV31chatText) > 300 )
                  {
                     AV31chatText = StringUtil.Substring( AV31chatText, 1, 300);
                  }
                  GXt_char1 = AV30chatLine;
                  new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV11from, out  GXt_char1) ;
                  AV30chatLine = StringUtil.PadL( StringUtil.Trim( StringUtil.Str( (decimal)(AV33hh), 2, 0)), 2, "0") + ":" + StringUtil.PadL( StringUtil.Trim( StringUtil.Str( (decimal)(AV34mm), 2, 0)), 2, "0") + " " + GXt_char1 + ": " + AV31chatText;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV26room.gxTpr_Chatlog))) )
                  {
                     AV26room.gxTpr_Chatlog = AV30chatLine;
                  }
                  else
                  {
                     AV26room.gxTpr_Chatlog = StringUtil.Trim( AV26room.gxTpr_Chatlog)+StringUtil.NewLine( )+AV30chatLine;
                  }
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "done") == 0 )
            {
               AV26room.gxTpr_State = "done";
               AV26room.gxTpr_Txid = StringUtil.Trim( AV16message.gxTpr_Data);
               AV26room.gxTpr_Info = "";
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV26room.gxTpr_Sessionjson)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV26room.gxTpr_Txid)) )
               {
                  AV38sessionData.FromJSonString(AV26room.gxTpr_Sessionjson, null);
                  AV41transactionFileName = StringUtil.Trim( AV12group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
                  AV40StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV41transactionFileName, out  AV36fileError), null);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV36fileError)) )
                  {
                     AV35changed = false;
                     AV43GXV2 = 1;
                     while ( AV43GXV2 <= AV40StoredTransactions.gxTpr_Transaction.Count )
                     {
                        AV37oneStoredTransaction = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV40StoredTransactions.gxTpr_Transaction.Item(AV43GXV2));
                        AV44GXV3 = 1;
                        while ( AV44GXV3 <= AV38sessionData.gxTpr_Inputs.Count )
                        {
                           AV39sessionInput = ((GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem)AV38sessionData.gxTpr_Inputs.Item(AV44GXV3));
                           if ( ( StringUtil.StrCmp(StringUtil.Trim( AV37oneStoredTransaction.gxTpr_Transactionid), StringUtil.Trim( AV39sessionInput.gxTpr_Txid)) == 0 ) && ( AV37oneStoredTransaction.gxTpr_N == AV39sessionInput.gxTpr_Vout ) && String.IsNullOrEmpty(StringUtil.RTrim( AV37oneStoredTransaction.gxTpr_Usedin.gxTpr_Transactionid)) )
                           {
                              AV37oneStoredTransaction.gxTpr_Usedin.gxTpr_Transactionid = AV26room.gxTpr_Txid;
                              AV37oneStoredTransaction.gxTpr_Usedin.gxTpr_Datetime = DateTimeUtil.Now( context);
                              AV35changed = true;
                           }
                           AV44GXV3 = (int)(AV44GXV3+1);
                        }
                        AV43GXV2 = (int)(AV43GXV2+1);
                     }
                     if ( AV35changed )
                     {
                        GXt_char1 = AV36fileError;
                        new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV41transactionFileName,  AV40StoredTransactions.ToJSonString(false, true), out  GXt_char1) ;
                        AV36fileError = GXt_char1;
                     }
                  }
               }
            }
            else if ( StringUtil.StrCmp(AV16message.gxTpr_Kind, "abort") == 0 )
            {
               if ( ( StringUtil.StrCmp(AV26room.gxTpr_State, "gathering") == 0 ) || ( StringUtil.StrCmp(AV26room.gxTpr_State, "signing") == 0 ) )
               {
                  AV26room.gxTpr_State = "aborted";
                  GXt_char1 = "";
                  new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV11from, out  GXt_char1) ;
                  AV26room.gxTpr_Info = GXt_char1+": "+StringUtil.Trim( AV16message.gxTpr_Data);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV26room.gxTpr_Sessionid)) )
                  {
                     AV14jsonText = AV17muSig2Processor.abort(StringUtil.Trim( AV26room.gxTpr_Sessionid));
                  }
               }
            }
         }
         GXt_int3 = 0;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int3) ;
         AV26room.gxTpr_Lastupdateunix = GXt_int3;
         new GeneXus.Programs.wallet.registered.savemusig2room(context ).execute(  AV26room, out  AV9error) ;
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOAD PROCESSOR' Routine */
         returnInSub = false;
         GXt_SdtWalletInfo4 = AV29walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo4) ;
         AV29walletInfo = GXt_SdtWalletInfo4;
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV12group_sdt, out  AV13groupEO) ;
         AV23ok = AV17muSig2Processor.fromsdt(AV13groupEO);
      }

      protected void S121( )
      {
         /* 'ADD TO MAP' Routine */
         returnInSub = false;
         if ( StringUtil.StringSearch( AV15map, "\""+AV11from+"\":", 1) == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV15map))) )
            {
               AV15map = "{\"" + AV11from + "\":" + StringUtil.Trim( AV16message.gxTpr_Data) + "}";
            }
            else
            {
               AV15map = StringUtil.Substring( StringUtil.Trim( AV15map), 1, StringUtil.Len( StringUtil.Trim( AV15map))-1) + ",\"" + AV11from + "\":" + StringUtil.Trim( AV16message.gxTpr_Data) + "}";
            }
         }
      }

      protected void S131( )
      {
         /* 'ALL IN MAP' Routine */
         returnInSub = false;
         AV8allIn = true;
         AV21names = (GxSimpleCollection<string>)(GxRegex.Split(AV26room.gxTpr_Signers,","));
         AV45GXV4 = 1;
         while ( AV45GXV4 <= AV21names.Count )
         {
            AV20name = ((string)AV21names.Item(AV45GXV4));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV20name))) && ( StringUtil.StringSearch( AV15map, "\""+StringUtil.Trim( AV20name)+"\":", 1) == 0 ) )
            {
               AV8allIn = false;
            }
            AV45GXV4 = (int)(AV45GXV4+1);
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
         AV22notice = "";
         AV9error = "";
         AV11from = "";
         AV27rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV24oneRoom = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV26room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV25proposal = new GeneXus.Programs.wallet.registered.SdtMuSig2Proposal(context);
         AV14jsonText = "";
         AV29walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV17muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV28sessionInfo = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionInfo(context);
         AV15map = "";
         AV32chatTime = (DateTime)(DateTime.MinValue);
         GXt_dtime2 = (DateTime)(DateTime.MinValue);
         AV31chatText = "";
         AV30chatLine = "";
         AV38sessionData = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData(context);
         AV41transactionFileName = "";
         AV40StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV36fileError = "";
         AV37oneStoredTransaction = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV39sessionInput = new GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem(context);
         GXt_char1 = "";
         GXt_SdtWalletInfo4 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV13groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV21names = new GxSimpleCollection<string>();
         AV20name = "";
         /* GeneXus formulas. */
      }

      private short AV33hh ;
      private short AV34mm ;
      private int AV42GXV1 ;
      private int AV43GXV2 ;
      private int AV44GXV3 ;
      private int AV45GXV4 ;
      private long GXt_int3 ;
      private string AV9error ;
      private string AV41transactionFileName ;
      private string AV36fileError ;
      private string GXt_char1 ;
      private DateTime AV32chatTime ;
      private DateTime GXt_dtime2 ;
      private bool AV19mustSendNonces ;
      private bool AV18mustFinalize ;
      private bool AV10found ;
      private bool returnInSub ;
      private bool AV8allIn ;
      private bool AV35changed ;
      private bool AV23ok ;
      private string AV14jsonText ;
      private string AV15map ;
      private string AV22notice ;
      private string AV11from ;
      private string AV31chatText ;
      private string AV30chatLine ;
      private string AV20name ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomMessage AV16message ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV27rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV24oneRoom ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV26room ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Proposal AV25proposal ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV29walletInfo ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV17muSig2Processor ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionInfo AV28sessionInfo ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData AV38sessionData ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV40StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV37oneStoredTransaction ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2SessionData_InputsItem AV39sessionInput ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo4 ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV13groupEO ;
      private GxSimpleCollection<string> AV21names ;
      private string aP2_notice ;
      private bool aP3_mustSendNonces ;
      private bool aP4_mustFinalize ;
      private string aP5_error ;
   }

}
