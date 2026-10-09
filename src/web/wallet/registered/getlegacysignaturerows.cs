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
   public class getlegacysignaturerows : GXProcedure
   {
      public getlegacysignaturerows( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlegacysignaturerows( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> aP1_rows )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20rows = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow>( context, "LegacySignatureRow", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP1_rows=this.AV20rows;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_rows);
         return AV20rows ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> aP1_rows )
      {
         this.AV13groupId = aP0_groupId;
         this.AV20rows = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow>( context, "LegacySignatureRow", "distributedcryptography") ;
         SubmitImpl();
         aP1_rows=this.AV20rows;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUserPublic1 = AV11externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV11externalUserPublic = GXt_SdtExternalUserPublic1;
         GXt_SdtGroup_SDT2 = AV12group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV13groupId, out  GXt_SdtGroup_SDT2) ;
         AV12group_sdt = GXt_SdtGroup_SDT2;
         AV21websession.Set("Group_EDIT", AV12group_sdt.ToJSonString(false, true));
         AV14muSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV10compleatedMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV9alreadySignedByMeMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV8alreadyShownMeMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV12group_sdt.gxTpr_Contact.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_sdt.gxTpr_Contact.Item(AV22GXV1));
            AV23GXV2 = 1;
            while ( AV23GXV2 <= AV15oneContact.gxTpr_Musigsignatures.Count )
            {
               AV16oneMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV15oneContact.gxTpr_Musigsignatures.Item(AV23GXV2));
               AV14muSigSignatures.Add((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)(AV16oneMuSigSignatures.Clone()), 0);
               if ( AV16oneMuSigSignatures.gxTpr_Compleated )
               {
                  AV10compleatedMuSigSignatures.Add((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)(AV16oneMuSigSignatures.Clone()), 0);
               }
               if ( StringUtil.StrCmp(AV16oneMuSigSignatures.gxTpr_Senderusername, StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
               {
                  AV9alreadySignedByMeMuSigSignatures.Add((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)(AV16oneMuSigSignatures.Clone()), 0);
               }
               AV23GXV2 = (int)(AV23GXV2+1);
            }
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         AV14muSigSignatures.Sort("[signedDateTime]");
         AV24GXV3 = 1;
         while ( AV24GXV3 <= AV14muSigSignatures.Count )
         {
            AV16oneMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV14muSigSignatures.Item(AV24GXV3));
            if ( AV16oneMuSigSignatures.gxTpr_Id == AV17previousId )
            {
               AV8alreadyShownMeMuSigSignatures.Add((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)(AV16oneMuSigSignatures.Clone()), 0);
            }
            else
            {
               AV17previousId = AV16oneMuSigSignatures.gxTpr_Id;
            }
            AV24GXV3 = (int)(AV24GXV3+1);
         }
         AV20rows.Clear();
         AV19rowIndex = 0;
         AV25GXV4 = 1;
         while ( AV25GXV4 <= AV14muSigSignatures.Count )
         {
            AV16oneMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV14muSigSignatures.Item(AV25GXV4));
            AV19rowIndex = (short)(AV19rowIndex+1);
            AV18row = new GeneXus.Programs.wallet.registered.SdtLegacySignatureRow(context);
            AV18row.gxTpr_Rowindex = AV19rowIndex;
            AV18row.gxTpr_Id = AV16oneMuSigSignatures.gxTpr_Id;
            AV18row.gxTpr_Description = AV16oneMuSigSignatures.gxTpr_Description;
            AV18row.gxTpr_Signeddatetime = AV16oneMuSigSignatures.gxTpr_Signeddatetime;
            AV18row.gxTpr_Senderusername = AV16oneMuSigSignatures.gxTpr_Senderusername;
            AV18row.gxTpr_Compleated = AV16oneMuSigSignatures.gxTpr_Compleated;
            AV18row.gxTpr_Sendcoins = AV16oneMuSigSignatures.gxTpr_Sendcoins;
            AV18row.gxTpr_Sendto = AV16oneMuSigSignatures.gxTpr_Sendto;
            if ( ! AV16oneMuSigSignatures.gxTpr_Compleated && ! ( StringUtil.StrCmp(StringUtil.Trim( AV16oneMuSigSignatures.gxTpr_Senderusername), StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 ) && ! new GeneXus.Programs.wallet.registered.needtoshowsignmsg(context).executeUdp(  AV16oneMuSigSignatures,  AV10compleatedMuSigSignatures,  AV9alreadySignedByMeMuSigSignatures,  AV8alreadyShownMeMuSigSignatures) )
            {
               AV18row.gxTpr_Cansign = true;
            }
            else
            {
               AV18row.gxTpr_Cansign = false;
            }
            if ( ( AV12group_sdt.gxTpr_Grouptype == 30 ) && AV12group_sdt.gxTpr_Amigroupowner )
            {
               AV18row.gxTpr_Cansign = false;
            }
            AV20rows.Add(AV18row, 0);
            AV25GXV4 = (int)(AV25GXV4+1);
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
         AV20rows = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow>( context, "LegacySignatureRow", "distributedcryptography");
         AV11externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21websession = context.GetSession();
         AV14muSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV10compleatedMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV9alreadySignedByMeMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV8alreadyShownMeMuSigSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
         AV15oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV16oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         AV17previousId = Guid.Empty;
         AV18row = new GeneXus.Programs.wallet.registered.SdtLegacySignatureRow(context);
         /* GeneXus formulas. */
      }

      private short AV19rowIndex ;
      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private int AV25GXV4 ;
      private Guid AV13groupId ;
      private Guid AV17previousId ;
      private IGxSession AV21websession ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> AV20rows ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV11externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV14muSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV10compleatedMuSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV9alreadySignedByMeMuSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV8alreadyShownMeMuSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15oneContact ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV16oneMuSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtLegacySignatureRow AV18row ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> aP1_rows ;
   }

}
