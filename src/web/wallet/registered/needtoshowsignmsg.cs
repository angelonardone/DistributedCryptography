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
namespace GeneXus.Programs.wallet.registered {
   public class needtoshowsignmsg : GXProcedure
   {
      public needtoshowsignmsg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public needtoshowsignmsg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtMuSigSignatures aP0_oneMuSigSignatures ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP1_CompleatedMuSigSignatures ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP2_alreadySignedByMeMuSigSignatures ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP3_alreadyShownMeMuSigSignatures ,
                           out bool aP4_isCompleated )
      {
         this.AV9oneMuSigSignatures = aP0_oneMuSigSignatures;
         this.AV8CompleatedMuSigSignatures = aP1_CompleatedMuSigSignatures;
         this.AV12alreadySignedByMeMuSigSignatures = aP2_alreadySignedByMeMuSigSignatures;
         this.AV13alreadyShownMeMuSigSignatures = aP3_alreadyShownMeMuSigSignatures;
         this.AV10isCompleated = false ;
         initialize();
         ExecuteImpl();
         aP4_isCompleated=this.AV10isCompleated;
      }

      public bool executeUdp( GeneXus.Programs.wallet.registered.SdtMuSigSignatures aP0_oneMuSigSignatures ,
                              GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP1_CompleatedMuSigSignatures ,
                              GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP2_alreadySignedByMeMuSigSignatures ,
                              GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP3_alreadyShownMeMuSigSignatures )
      {
         execute(aP0_oneMuSigSignatures, aP1_CompleatedMuSigSignatures, aP2_alreadySignedByMeMuSigSignatures, aP3_alreadyShownMeMuSigSignatures, out aP4_isCompleated);
         return AV10isCompleated ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtMuSigSignatures aP0_oneMuSigSignatures ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP1_CompleatedMuSigSignatures ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP2_alreadySignedByMeMuSigSignatures ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> aP3_alreadyShownMeMuSigSignatures ,
                                 out bool aP4_isCompleated )
      {
         this.AV9oneMuSigSignatures = aP0_oneMuSigSignatures;
         this.AV8CompleatedMuSigSignatures = aP1_CompleatedMuSigSignatures;
         this.AV12alreadySignedByMeMuSigSignatures = aP2_alreadySignedByMeMuSigSignatures;
         this.AV13alreadyShownMeMuSigSignatures = aP3_alreadyShownMeMuSigSignatures;
         this.AV10isCompleated = false ;
         SubmitImpl();
         aP4_isCompleated=this.AV10isCompleated;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10isCompleated = false;
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV8CompleatedMuSigSignatures.Count )
         {
            AV11oneCompMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV8CompleatedMuSigSignatures.Item(AV14GXV1));
            if ( AV9oneMuSigSignatures.gxTpr_Id == AV11oneCompMuSigSignatures.gxTpr_Id )
            {
               AV10isCompleated = true;
               if (true) break;
            }
            AV14GXV1 = (int)(AV14GXV1+1);
         }
         AV15GXV2 = 1;
         while ( AV15GXV2 <= AV12alreadySignedByMeMuSigSignatures.Count )
         {
            AV11oneCompMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV12alreadySignedByMeMuSigSignatures.Item(AV15GXV2));
            if ( AV9oneMuSigSignatures.gxTpr_Id == AV11oneCompMuSigSignatures.gxTpr_Id )
            {
               AV10isCompleated = true;
               if (true) break;
            }
            AV15GXV2 = (int)(AV15GXV2+1);
         }
         AV16GXV3 = 1;
         while ( AV16GXV3 <= AV13alreadyShownMeMuSigSignatures.Count )
         {
            AV11oneCompMuSigSignatures = ((GeneXus.Programs.wallet.registered.SdtMuSigSignatures)AV13alreadyShownMeMuSigSignatures.Item(AV16GXV3));
            if ( ( AV9oneMuSigSignatures.gxTpr_Signeddatetime == AV11oneCompMuSigSignatures.gxTpr_Signeddatetime ) && ( AV9oneMuSigSignatures.gxTpr_Id == AV11oneCompMuSigSignatures.gxTpr_Id ) )
            {
               AV10isCompleated = true;
               if (true) break;
            }
            AV16GXV3 = (int)(AV16GXV3+1);
         }
         this.cleanup();
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
         AV11oneCompMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private int AV15GXV2 ;
      private int AV16GXV3 ;
      private bool AV10isCompleated ;
      private bool aP4_isCompleated ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV8CompleatedMuSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV12alreadySignedByMeMuSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> AV13alreadyShownMeMuSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV9oneMuSigSignatures ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV11oneCompMuSigSignatures ;
   }

}
