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
   public class startmusig2room : GXProcedure
   {
      public startmusig2room( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public startmusig2room( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           bool aP1_sendAllCoins ,
                           decimal aP2_sendCoins ,
                           string aP3_sendTo ,
                           string aP4_description ,
                           out Guid aP5_roomId ,
                           out string aP6_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV14sendAllCoins = aP1_sendAllCoins;
         this.AV15sendCoins = aP2_sendCoins;
         this.AV16sendTo = aP3_sendTo;
         this.AV8description = aP4_description;
         this.AV13roomId = Guid.Empty ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP5_roomId=this.AV13roomId;
         aP6_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                bool aP1_sendAllCoins ,
                                decimal aP2_sendCoins ,
                                string aP3_sendTo ,
                                string aP4_description ,
                                out Guid aP5_roomId )
      {
         execute(aP0_groupId, aP1_sendAllCoins, aP2_sendCoins, aP3_sendTo, aP4_description, out aP5_roomId, out aP6_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 bool aP1_sendAllCoins ,
                                 decimal aP2_sendCoins ,
                                 string aP3_sendTo ,
                                 string aP4_description ,
                                 out Guid aP5_roomId ,
                                 out string aP6_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV14sendAllCoins = aP1_sendAllCoins;
         this.AV15sendCoins = aP2_sendCoins;
         this.AV16sendTo = aP3_sendTo;
         this.AV8description = aP4_description;
         this.AV13roomId = Guid.Empty ;
         this.AV9error = "" ;
         SubmitImpl();
         aP5_roomId=this.AV13roomId;
         aP6_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV10group_sdt.gxTpr_Groupid) || ! AV10group_sdt.gxTpr_Isactive || ! ( ( AV10group_sdt.gxTpr_Grouptype == 80 ) || ( AV10group_sdt.gxTpr_Grouptype == 90 ) ) )
         {
            AV9error = "This is not an active MuSig2 group";
            cleanup();
            if (true) return;
         }
         if ( ( AV10group_sdt.gxTpr_Grouptype == 80 ) && AV10group_sdt.gxTpr_Amigroupowner )
         {
            AV9error = "The owner pays alone: use Send in the Wallet Balance tab";
            cleanup();
            if (true) return;
         }
         AV12proposal = new GeneXus.Programs.wallet.registered.SdtMuSig2Proposal(context);
         AV12proposal.gxTpr_Sendto = StringUtil.Trim( AV16sendTo);
         AV12proposal.gxTpr_Amountbtc = AV15sendCoins;
         AV12proposal.gxTpr_Sendall = AV14sendAllCoins;
         AV12proposal.gxTpr_Description = StringUtil.Trim( AV8description);
         GXt_int2 = 0;
         new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int2) ;
         AV12proposal.gxTpr_Gatherdeadlineunix = (long)(GXt_int2+900000);
         AV13roomId = Guid.NewGuid( );
         GXt_char3 = AV9error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV11groupId,  AV13roomId,  "invite",  AV12proposal.ToJSonString(false, true), out  GXt_char3) ;
         AV9error = GXt_char3;
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
         AV13roomId = Guid.Empty;
         AV9error = "";
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12proposal = new GeneXus.Programs.wallet.registered.SdtMuSig2Proposal(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private long GXt_int2 ;
      private decimal AV15sendCoins ;
      private string AV16sendTo ;
      private string AV9error ;
      private string GXt_char3 ;
      private bool AV14sendAllCoins ;
      private string AV8description ;
      private Guid AV11groupId ;
      private Guid AV13roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Proposal AV12proposal ;
      private Guid aP5_roomId ;
      private string aP6_error ;
   }

}
