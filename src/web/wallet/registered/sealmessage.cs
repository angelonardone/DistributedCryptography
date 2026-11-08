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
   public class sealmessage : GXProcedure
   {
      public sealmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sealmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_toUserName ,
                           ref GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message ,
                           out string aP2_error )
      {
         this.AV13toUserName = aP0_toUserName;
         this.AV10sdt_message = aP1_sdt_message;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_sdt_message=this.AV10sdt_message;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_toUserName ,
                                ref GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message )
      {
         execute(aP0_toUserName, ref aP1_sdt_message, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_toUserName ,
                                 ref GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message ,
                                 out string aP2_error )
      {
         this.AV13toUserName = aP0_toUserName;
         this.AV10sdt_message = aP1_sdt_message;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_sdt_message=this.AV10sdt_message;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtKeyInfo1 = AV9keyInfo;
         new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV9keyInfo = GXt_SdtKeyInfo1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9keyInfo.gxTpr_Privatekey)) || String.IsNullOrEmpty(StringUtil.RTrim( AV9keyInfo.gxTpr_Address)) )
         {
            AV8error = "Open the wallet to send messages";
            cleanup();
            if (true) return;
         }
         if ( (Guid.Empty==AV10sdt_message.gxTpr_Id) )
         {
            AV10sdt_message.gxTpr_Id = Guid.NewGuid( );
         }
         if ( AV10sdt_message.gxTpr_Datetimeunix == 0 )
         {
            GXt_int2 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int2) ;
            AV10sdt_message.gxTpr_Datetimeunix = GXt_int2;
         }
         AV10sdt_message.gxTpr_From = StringUtil.Trim( AV9keyInfo.gxTpr_Address);
         AV10sdt_message.gxTpr_To = StringUtil.Trim( AV13toUserName);
         AV10sdt_message.gxTpr_Signature = "";
         GXt_SdtWalletInfo3 = AV14walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV14walletInfo = GXt_SdtWalletInfo3;
         GXt_char4 = AV12text;
         new GeneXus.Programs.wallet.registered.messageenvelopetext(context ).execute(  AV10sdt_message, out  GXt_char4) ;
         AV12text = GXt_char4;
         GXt_char4 = AV8error;
         new GeneXus.Programs.nbitcoin.signbip322(context ).execute(  AV9keyInfo.gxTpr_Privatekey,  AV9keyInfo.gxTpr_Address,  AV14walletInfo.gxTpr_Networktype,  AV12text, out  AV11signature, out  GXt_char4) ;
         AV8error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV10sdt_message.gxTpr_Signature = StringUtil.Trim( AV11signature);
         }
         else
         {
            AV8error = "The message could not be signed: " + StringUtil.Trim( AV8error);
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
         AV8error = "";
         AV9keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV14walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV12text = "";
         GXt_char4 = "";
         AV11signature = "";
         /* GeneXus formulas. */
      }

      private long GXt_int2 ;
      private string AV8error ;
      private string GXt_char4 ;
      private string AV12text ;
      private string AV13toUserName ;
      private string AV11signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV10sdt_message ;
      private GeneXus.Programs.nostr.SdtSDT_message aP1_sdt_message ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV9keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV14walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private string aP2_error ;
   }

}
