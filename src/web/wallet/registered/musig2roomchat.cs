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
   public class musig2roomchat : GXProcedure
   {
      public musig2roomchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public musig2roomchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           string aP2_text ,
                           out string aP3_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV13roomId = aP1_roomId;
         this.AV15text = aP2_text;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_roomId ,
                                string aP2_text )
      {
         execute(aP0_groupId, aP1_roomId, aP2_text, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_roomId ,
                                 string aP2_text ,
                                 out string aP3_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV13roomId = aP1_roomId;
         this.AV15text = aP2_text;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV10group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV14rooms) ;
         AV9found = false;
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV14rooms.Count )
         {
            AV12room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV14rooms.Item(AV16GXV1));
            if ( AV12room.gxTpr_Roomid == AV13roomId )
            {
               AV9found = true;
               if (true) break;
            }
            AV16GXV1 = (int)(AV16GXV1+1);
         }
         if ( ! AV9found || ! ( ( StringUtil.StrCmp(AV12room.gxTpr_State, "gathering") == 0 ) || ( StringUtil.StrCmp(AV12room.gxTpr_State, "signing") == 0 ) ) )
         {
            AV8error = "This signing room is closed";
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV15text))) )
         {
            cleanup();
            if (true) return;
         }
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV11groupId,  AV13roomId,  "chat",  StringUtil.Trim( AV15text), out  GXt_char2) ;
         AV8error = GXt_char2;
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
         AV14rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV12room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private string AV8error ;
      private string GXt_char2 ;
      private bool AV9found ;
      private string AV15text ;
      private Guid AV11groupId ;
      private Guid AV13roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV14rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV12room ;
      private string aP3_error ;
   }

}
