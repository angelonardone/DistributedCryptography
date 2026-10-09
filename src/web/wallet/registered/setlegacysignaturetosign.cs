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
   public class setlegacysignaturetosign : GXProcedure
   {
      public setlegacysignaturetosign( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setlegacysignaturetosign( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           short aP1_rowIndex ,
                           Guid aP2_muSigId ,
                           out short aP3_groupType ,
                           out string aP4_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV15rowIndex = aP1_rowIndex;
         this.AV11muSigId = aP2_muSigId;
         this.AV17groupType = 0 ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_groupType=this.AV17groupType;
         aP4_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                short aP1_rowIndex ,
                                Guid aP2_muSigId ,
                                out short aP3_groupType )
      {
         execute(aP0_groupId, aP1_rowIndex, aP2_muSigId, out aP3_groupType, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 short aP1_rowIndex ,
                                 Guid aP2_muSigId ,
                                 out short aP3_groupType ,
                                 out string aP4_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV15rowIndex = aP1_rowIndex;
         this.AV11muSigId = aP2_muSigId;
         this.AV17groupType = 0 ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_groupType=this.AV17groupType;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV9group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV10groupId, out  GXt_SdtGroup_SDT1) ;
         AV9group_sdt = GXt_SdtGroup_SDT1;
         AV17groupType = AV9group_sdt.gxTpr_Grouptype;
         if ( ! ( AV17groupType == 50 ) && ! ( AV17groupType == 30 ) )
         {
            AV8error = "This is not a multisignature group";
            cleanup();
            if (true) return;
         }
         AV12muSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV9group_sdt.gxTpr_Contact.Count )
         {
            AV13oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV9group_sdt.gxTpr_Contact.Item(AV18GXV1));
            AV19GXV2 = 1;
            while ( AV19GXV2 <= AV13oneContact.gxTpr_Musigsignatures.Count )
            {
               AV14oneMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV13oneContact.gxTpr_Musigsignatures.Item(AV19GXV2));
               AV12muSigSignatures.Add((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)(AV14oneMuSigSignatures.Clone()), 0);
               AV19GXV2 = (int)(AV19GXV2+1);
            }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         AV12muSigSignatures.Sort("[signedDateTime]");
         if ( ( AV15rowIndex < 1 ) || ( AV15rowIndex > AV12muSigSignatures.Count ) )
         {
            AV8error = "We couldn't find the transaction to sign, please refresh the page";
            cleanup();
            if (true) return;
         }
         AV14oneMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV12muSigSignatures.Item(AV15rowIndex));
         if ( ! ( AV14oneMuSigSignatures.gxTpr_Id == AV11muSigId ) )
         {
            AV8error = "We couldn't find the transaction to sign, please refresh the page";
            cleanup();
            if (true) return;
         }
         AV16websession.Set("MuSign_ONE", AV14oneMuSigSignatures.ToJSonString(false, true));
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
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12muSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV13oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV16websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private short AV15rowIndex ;
      private short AV17groupType ;
      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private string AV8error ;
      private Guid AV10groupId ;
      private Guid AV11muSigId ;
      private IGxSession AV16websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV12muSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13oneContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV14oneMuSigSignatures ;
      private short aP3_groupType ;
      private string aP4_error ;
   }

}
