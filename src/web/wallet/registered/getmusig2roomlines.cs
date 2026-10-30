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
   public class getmusig2roomlines : GXProcedure
   {
      public getmusig2roomlines( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getmusig2roomlines( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> aP1_lines ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV19lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine>( context, "MuSig2RoomLine", "distributedcryptography") ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_lines=this.AV19lines;
         aP2_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> aP1_lines )
      {
         execute(aP0_groupId, out aP1_lines, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> aP1_lines ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV19lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine>( context, "MuSig2RoomLine", "distributedcryptography") ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_lines=this.AV19lines;
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV13group_sdt = GXt_SdtGroup_SDT1;
         GXt_SdtExternalUserPublic2 = AV12externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV12externalUserPublic = GXt_SdtExternalUserPublic2;
         AV20me = StringUtil.Trim( AV12externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         GXt_int3 = AV25now;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int3) ;
         AV25now = GXt_int3;
         AV16isDelegatedOwner = false;
         if ( ( AV13group_sdt.gxTpr_Grouptype == 80 ) && AV13group_sdt.gxTpr_Amigroupowner )
         {
            AV16isDelegatedOwner = true;
         }
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV30rooms) ;
         AV30rooms.Sort("createdUnix");
         AV35GXV1 = 1;
         while ( AV35GXV1 <= AV30rooms.Count )
         {
            AV29room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV30rooms.Item(AV35GXV1));
            AV11expired = false;
            if ( ( StringUtil.StrCmp(AV29room.gxTpr_State, "gathering") == 0 ) && ( AV25now > AV29room.gxTpr_Gatherdeadlineunix ) )
            {
               AV29room.gxTpr_State = "aborted";
               AV29room.gxTpr_Info = "Nobody started the signing within 15 minutes";
               AV11expired = true;
            }
            if ( ( StringUtil.StrCmp(AV29room.gxTpr_State, "signing") == 0 ) && ( AV25now > AV29room.gxTpr_Signdeadlineunix ) )
            {
               AV29room.gxTpr_State = "aborted";
               AV29room.gxTpr_Info = "The signing took more than 3 minutes";
               AV11expired = true;
               AV18jsonText = AV21muSig2Processor.abort(StringUtil.Trim( AV29room.gxTpr_Sessionid));
            }
            if ( AV11expired )
            {
               new GeneXus.Programs.wallet.registered.savemusig2room(context ).execute(  AV29room, out  AV10error) ;
            }
            AV31roomLine = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine(context);
            AV31roomLine.gxTpr_Roomid = AV29room.gxTpr_Roomid;
            GXt_dtime4 = AV9createdAt;
            new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV29room.gxTpr_Createdunix, out  GXt_dtime4) ;
            AV9createdAt = GXt_dtime4;
            AV31roomLine.gxTpr_Created = context.localUtil.TToC( AV9createdAt, 8, 5, 1, 2, "/", ":", " ");
            GXt_char5 = "";
            new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV29room.gxTpr_Initiator, out  GXt_char5) ;
            AV31roomLine.gxTpr_Openedby = GXt_char5;
            AV31roomLine.gxTpr_Description = AV29room.gxTpr_Description;
            if ( AV29room.gxTpr_Sendall )
            {
               AV31roomLine.gxTpr_Amounttext = "everything";
            }
            else
            {
               AV31roomLine.gxTpr_Amounttext = StringUtil.Trim( StringUtil.Str( AV29room.gxTpr_Amountbtc, 16, 8))+" BTC";
            }
            AV31roomLine.gxTpr_Sendto = AV29room.gxTpr_Sendto;
            AV15iAmHere = false;
            if ( StringUtil.StringSearch( AV29room.gxTpr_Present, ","+AV20me+",", 1) > 0 )
            {
               AV15iAmHere = true;
            }
            AV17iSign = false;
            if ( StringUtil.StringSearch( AV29room.gxTpr_Signers, ","+AV20me+",", 1) > 0 )
            {
               AV17iSign = true;
            }
            AV22mySignatureSent = false;
            if ( StringUtil.StringSearch( AV29room.gxTpr_Partialsmap, "\""+AV20me+"\":", 1) > 0 )
            {
               AV22mySignatureSent = true;
            }
            AV8allNonces = true;
            AV26people = "";
            AV24names = (GxSimpleCollection<string>)(GxRegex.Split(AV29room.gxTpr_Signers,","));
            AV36GXV2 = 1;
            while ( AV36GXV2 <= AV24names.Count )
            {
               AV23name = ((string)AV24names.Item(AV36GXV2));
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV23name))) )
               {
                  if ( StringUtil.StringSearch( AV29room.gxTpr_Noncesmap, "\""+StringUtil.Trim( AV23name)+"\":", 1) == 0 )
                  {
                     AV8allNonces = false;
                  }
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV26people)) )
                  {
                     AV26people += ", ";
                  }
                  GXt_char5 = AV26people;
                  new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  StringUtil.Trim( AV23name), out  GXt_char5) ;
                  AV26people += GXt_char5;
                  if ( StringUtil.StringSearch( AV29room.gxTpr_Partialsmap, "\""+StringUtil.Trim( AV23name)+"\":", 1) > 0 )
                  {
                     AV26people += " - signed";
                  }
                  else
                  {
                     if ( StringUtil.StringSearch( AV29room.gxTpr_Noncesmap, "\""+StringUtil.Trim( AV23name)+"\":", 1) > 0 )
                     {
                        AV26people += " - ready to sign";
                     }
                     else
                     {
                        AV26people += " - connecting...";
                     }
                  }
               }
               AV36GXV2 = (int)(AV36GXV2+1);
            }
            AV28presentPeople = "";
            AV24names = (GxSimpleCollection<string>)(GxRegex.Split(AV29room.gxTpr_Present,","));
            AV37GXV3 = 1;
            while ( AV37GXV3 <= AV24names.Count )
            {
               AV23name = ((string)AV24names.Item(AV37GXV3));
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV23name))) )
               {
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV28presentPeople)) )
                  {
                     AV28presentPeople += ", ";
                  }
                  GXt_char5 = AV28presentPeople;
                  new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  StringUtil.Trim( AV23name), out  GXt_char5) ;
                  AV28presentPeople += GXt_char5;
               }
               AV37GXV3 = (int)(AV37GXV3+1);
            }
            if ( StringUtil.StrCmp(AV29room.gxTpr_State, "gathering") == 0 )
            {
               AV27timeLeft = NumberUtil.Int( (long)(Math.Round((AV29room.gxTpr_Gatherdeadlineunix-AV25now)/ (decimal)(60000), 18, MidpointRounding.ToEven)));
            }
            else
            {
               AV27timeLeft = NumberUtil.Int( (long)(Math.Round((AV29room.gxTpr_Signdeadlineunix-AV25now)/ (decimal)(1000), 18, MidpointRounding.ToEven)));
            }
            AV34waiting = "";
            AV38GXV4 = 1;
            while ( AV38GXV4 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV32groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV38GXV4));
               AV33memberName = StringUtil.Trim( AV32groupContact.gxTpr_Contactusername);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV33memberName)) && ! ( ( AV13group_sdt.gxTpr_Grouptype == 80 ) && ( AV32groupContact.gxTpr_Contactid == AV32groupContact.gxTpr_Contactgroupid ) ) )
               {
                  if ( ( ( StringUtil.StrCmp(AV29room.gxTpr_State, "gathering") == 0 ) && ( StringUtil.StringSearch( AV29room.gxTpr_Present, ","+AV33memberName+",", 1) == 0 ) ) || ( ( StringUtil.StrCmp(AV29room.gxTpr_State, "signing") == 0 ) && ( StringUtil.StringSearch( AV29room.gxTpr_Signers, ","+AV33memberName+",", 1) == 0 ) ) )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV34waiting)) )
                     {
                        AV34waiting += ", ";
                     }
                     GXt_char5 = AV34waiting;
                     new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV33memberName, out  GXt_char5) ;
                     AV34waiting += GXt_char5;
                  }
               }
               AV38GXV4 = (int)(AV38GXV4+1);
            }
            AV31roomLine.gxTpr_Chatlog = AV29room.gxTpr_Chatlog;
            if ( StringUtil.StrCmp(AV29room.gxTpr_State, "gathering") == 0 )
            {
               AV31roomLine.gxTpr_Isopen = true;
               AV31roomLine.gxTpr_Stateshort = "Waiting for signers";
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV34waiting)) )
               {
                  AV31roomLine.gxTpr_Waiting = "Not joined yet: "+AV34waiting;
               }
               AV31roomLine.gxTpr_Statetext = "Waiting for the signers ("+StringUtil.Trim( StringUtil.Str( (decimal)(AV13group_sdt.gxTpr_Minimumshares), 4, 0))+" needed); "+StringUtil.Trim( StringUtil.Str( (decimal)(AV27timeLeft), 18, 0))+" min left";
               AV31roomLine.gxTpr_People = "Joined (online now): "+AV28presentPeople;
               if ( ! AV15iAmHere && ! AV16isDelegatedOwner )
               {
                  AV31roomLine.gxTpr_Canhere = true;
               }
               if ( StringUtil.StrCmp(StringUtil.Trim( AV29room.gxTpr_Initiator), AV20me) == 0 )
               {
                  AV31roomLine.gxTpr_Cansignnow = true;
               }
               AV31roomLine.gxTpr_Canabort = AV15iAmHere;
            }
            else if ( StringUtil.StrCmp(AV29room.gxTpr_State, "signing") == 0 )
            {
               if ( AV8allNonces )
               {
                  AV31roomLine.gxTpr_Statetext = "Signing: every signer must now press Sign with password; "+StringUtil.Trim( StringUtil.Str( (decimal)(AV27timeLeft), 18, 0))+" s left. Fee "+StringUtil.Trim( StringUtil.Str( AV29room.gxTpr_Feebtc, 16, 8))+" BTC ("+StringUtil.Trim( StringUtil.Str( (decimal)(AV29room.gxTpr_Vsize), 18, 0))+" vB)";
               }
               else
               {
                  AV31roomLine.gxTpr_Statetext = "Signing: waiting for every signer to connect; "+StringUtil.Trim( StringUtil.Str( (decimal)(AV27timeLeft), 18, 0))+" s left. Fee "+StringUtil.Trim( StringUtil.Str( AV29room.gxTpr_Feebtc, 16, 8))+" BTC ("+StringUtil.Trim( StringUtil.Str( (decimal)(AV29room.gxTpr_Vsize), 18, 0))+" vB)";
               }
               AV31roomLine.gxTpr_Isopen = true;
               AV31roomLine.gxTpr_Stateshort = "Signing";
               AV31roomLine.gxTpr_People = "Signers: "+AV26people;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV34waiting)) )
               {
                  AV31roomLine.gxTpr_Waiting = "Not signing this payment: "+AV34waiting;
               }
               if ( AV17iSign && AV8allNonces && ! AV22mySignatureSent )
               {
                  AV31roomLine.gxTpr_Cansign = true;
               }
               if ( AV17iSign || ( StringUtil.StrCmp(StringUtil.Trim( AV29room.gxTpr_Initiator), AV20me) == 0 ) )
               {
                  AV31roomLine.gxTpr_Canabort = true;
               }
            }
            else if ( StringUtil.StrCmp(AV29room.gxTpr_State, "done") == 0 )
            {
               AV31roomLine.gxTpr_Stateshort = "Paid";
               AV31roomLine.gxTpr_Statetext = "Paid: transaction "+StringUtil.Trim( AV29room.gxTpr_Txid);
               AV31roomLine.gxTpr_People = "Signed by: "+AV26people;
            }
            else
            {
               AV31roomLine.gxTpr_Stateshort = "Closed";
               AV31roomLine.gxTpr_Statetext = "Closed: "+StringUtil.Trim( AV29room.gxTpr_Info);
               AV31roomLine.gxTpr_People = "";
            }
            AV19lines.Add(AV31roomLine, 1);
            AV35GXV1 = (int)(AV35GXV1+1);
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
         AV19lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine>( context, "MuSig2RoomLine", "distributedcryptography");
         AV10error = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV20me = "";
         AV30rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV29room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV18jsonText = "";
         AV21muSig2Processor = new GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor(context);
         AV31roomLine = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine(context);
         AV9createdAt = (DateTime)(DateTime.MinValue);
         GXt_dtime4 = (DateTime)(DateTime.MinValue);
         AV26people = "";
         AV24names = new GxSimpleCollection<string>();
         AV23name = "";
         AV28presentPeople = "";
         AV34waiting = "";
         AV32groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV33memberName = "";
         GXt_char5 = "";
         /* GeneXus formulas. */
      }

      private int AV35GXV1 ;
      private int AV36GXV2 ;
      private int AV37GXV3 ;
      private int AV38GXV4 ;
      private long AV25now ;
      private long GXt_int3 ;
      private long AV27timeLeft ;
      private string AV10error ;
      private string GXt_char5 ;
      private DateTime AV9createdAt ;
      private DateTime GXt_dtime4 ;
      private bool AV16isDelegatedOwner ;
      private bool AV11expired ;
      private bool AV15iAmHere ;
      private bool AV17iSign ;
      private bool AV22mySignatureSent ;
      private bool AV8allNonces ;
      private string AV18jsonText ;
      private string AV20me ;
      private string AV26people ;
      private string AV23name ;
      private string AV28presentPeople ;
      private string AV34waiting ;
      private string AV33memberName ;
      private Guid AV14groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> AV19lines ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV12externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV30rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV29room ;
      private GeneXus.Programs.distributedcryptographylib.SdtMuSig2Processor AV21muSig2Processor ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine AV31roomLine ;
      private GxSimpleCollection<string> AV24names ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV32groupContact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> aP1_lines ;
      private string aP2_error ;
   }

}
