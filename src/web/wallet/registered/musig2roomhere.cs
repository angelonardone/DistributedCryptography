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
   public class musig2roomhere : GXProcedure
   {
      public musig2roomhere( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public musig2roomhere( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_roomId ,
                           out string aP2_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV15roomId = aP1_roomId;
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
         this.AV15roomId = aP1_roomId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV12groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         new GeneXus.Programs.wallet.registered.readmusig2rooms(context ).execute(  AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  AV16rooms) ;
         AV10found = false;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV16rooms.Count )
         {
            AV14room = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV16rooms.Item(AV17GXV1));
            if ( AV14room.gxTpr_Roomid == AV15roomId )
            {
               AV10found = true;
               if (true) break;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         if ( ! AV10found )
         {
            AV8error = "This signing room does not exist";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV9externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV9externalUserPublic = GXt_SdtExternalUserPublic2;
         AV13me = StringUtil.Trim( AV9externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         if ( ! ( StringUtil.StrCmp(AV14room.gxTpr_State, "gathering") == 0 ) )
         {
            AV8error = "This signing room is not waiting for its members any more";
            cleanup();
            if (true) return;
         }
         if ( ( AV11group_sdt.gxTpr_Grouptype == 80 ) && AV11group_sdt.gxTpr_Amigroupowner )
         {
            AV8error = "The owner of a Delegated MultiSig 2 group is not one of the signers of the members";
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV8error;
         new GeneXus.Programs.wallet.registered.sendmusig2roommessage(context ).execute(  AV12groupId,  AV15roomId,  "here",  "", out  GXt_char3) ;
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
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV14room = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV13me = "";
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private string AV8error ;
      private string GXt_char3 ;
      private bool AV10found ;
      private string AV13me ;
      private Guid AV12groupId ;
      private Guid AV15roomId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV16rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV14room ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV9externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private string aP2_error ;
   }

}
