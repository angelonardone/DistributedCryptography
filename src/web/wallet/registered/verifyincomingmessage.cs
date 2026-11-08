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
   public class verifyincomingmessage : GXProcedure
   {
      public verifyincomingmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public verifyincomingmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtProcessedMessage> aP1_processed ,
                           out bool aP2_isReplay ,
                           out string aP3_reason )
      {
         this.AV16sdt_message = aP0_sdt_message;
         this.AV14processed = aP1_processed;
         this.AV9isReplay = false ;
         this.AV15reason = "" ;
         initialize();
         ExecuteImpl();
         aP2_isReplay=this.AV9isReplay;
         aP3_reason=this.AV15reason;
      }

      public string executeUdp( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                GXBaseCollection<GeneXus.Programs.wallet.registered.SdtProcessedMessage> aP1_processed ,
                                out bool aP2_isReplay )
      {
         execute(aP0_sdt_message, aP1_processed, out aP2_isReplay, out aP3_reason);
         return AV15reason ;
      }

      public void executeSubmit( GeneXus.Programs.nostr.SdtSDT_message aP0_sdt_message ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtProcessedMessage> aP1_processed ,
                                 out bool aP2_isReplay ,
                                 out string aP3_reason )
      {
         this.AV16sdt_message = aP0_sdt_message;
         this.AV14processed = aP1_processed;
         this.AV9isReplay = false ;
         this.AV15reason = "" ;
         SubmitImpl();
         aP2_isReplay=this.AV9isReplay;
         aP3_reason=this.AV15reason;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9isReplay = false;
         AV15reason = "";
         GXt_SdtKeyInfo1 = AV10keyInfo;
         new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV10keyInfo = GXt_SdtKeyInfo1;
         AV11me = StringUtil.Trim( AV10keyInfo.gxTpr_Address);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV16sdt_message.gxTpr_From))) || String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV16sdt_message.gxTpr_To))) || String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV16sdt_message.gxTpr_Signature))) )
         {
            AV15reason = "a message without a signed envelope";
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11me)) || ! ( StringUtil.StrCmp(StringUtil.Trim( AV16sdt_message.gxTpr_To), AV11me) == 0 ) )
         {
            AV15reason = "a message for somebody else";
            cleanup();
            if (true) return;
         }
         GXt_int2 = AV12now;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int2) ;
         AV12now = GXt_int2;
         if ( AV16sdt_message.gxTpr_Datetimeunix > AV12now + 600000 )
         {
            AV15reason = "a message dated in the future";
            cleanup();
            if (true) return;
         }
         if ( AV16sdt_message.gxTpr_Datetimeunix < AV12now - 31536000000L )
         {
            AV15reason = "a message older than a year";
            cleanup();
            if (true) return;
         }
         if ( ! ( ( AV16sdt_message.gxTpr_Messagetype == 50 ) || ( AV16sdt_message.gxTpr_Messagetype == 130 ) ) )
         {
            AV20GXV1 = 1;
            while ( AV20GXV1 <= AV14processed.Count )
            {
               AV13oneProcessed = ((GeneXus.Programs.wallet.registered.SdtProcessedMessage)AV14processed.Item(AV20GXV1));
               if ( AV13oneProcessed.gxTpr_Id == AV16sdt_message.gxTpr_Id )
               {
                  AV9isReplay = true;
                  AV15reason = "a message already processed";
                  cleanup();
                  if (true) return;
               }
               AV20GXV1 = (int)(AV20GXV1+1);
            }
         }
         GXt_char3 = AV17text;
         new GeneXus.Programs.wallet.registered.messageenvelopetext(context ).execute(  AV16sdt_message, out  GXt_char3) ;
         AV17text = GXt_char3;
         GXt_SdtWalletInfo4 = AV19walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo4) ;
         AV19walletInfo = GXt_SdtWalletInfo4;
         GXt_char3 = AV8error;
         new GeneXus.Programs.nbitcoin.verifybip322(context ).execute(  StringUtil.Trim( AV16sdt_message.gxTpr_From),  AV19walletInfo.gxTpr_Networktype,  AV17text,  StringUtil.Trim( AV16sdt_message.gxTpr_Signature), out  AV18verified, out  GXt_char3) ;
         AV8error = GXt_char3;
         if ( ! AV18verified )
         {
            AV15reason = "a message whose signature is not the one of its sender";
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV15reason;
         new GeneXus.Programs.wallet.registered.authorizeincomingmessage(context ).execute(  AV16sdt_message, out  GXt_char3) ;
         AV15reason = GXt_char3;
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
         AV15reason = "";
         AV10keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV11me = "";
         AV13oneProcessed = new GeneXus.Programs.wallet.registered.SdtProcessedMessage(context);
         AV17text = "";
         AV19walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo4 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV8error = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV20GXV1 ;
      private long AV12now ;
      private long GXt_int2 ;
      private string AV8error ;
      private string GXt_char3 ;
      private bool AV9isReplay ;
      private bool AV18verified ;
      private string AV17text ;
      private string AV15reason ;
      private string AV11me ;
      private GeneXus.Programs.nostr.SdtSDT_message AV16sdt_message ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtProcessedMessage> AV14processed ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV10keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.wallet.registered.SdtProcessedMessage AV13oneProcessed ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV19walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo4 ;
      private bool aP2_isReplay ;
      private string aP3_reason ;
   }

}
