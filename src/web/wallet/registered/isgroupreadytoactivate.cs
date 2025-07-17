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
   public class isgroupreadytoactivate : GXProcedure
   {
      public isgroupreadytoactivate( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public isgroupreadytoactivate( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_isReady )
      {
         this.AV12groupId = aP0_groupId;
         this.AV13isReady = false ;
         initialize();
         ExecuteImpl();
         aP1_isReady=this.AV13isReady;
      }

      public bool executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_isReady);
         return AV13isReady ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_isReady )
      {
         this.AV12groupId = aP0_groupId;
         this.AV13isReady = false ;
         SubmitImpl();
         aP1_isReady=this.AV13isReady;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV10group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV12groupId, out  GXt_SdtGroup_SDT1) ;
         AV10group_sdt = GXt_SdtGroup_SDT1;
         AV14totalInvitationsAccepted = 0;
         if ( AV10group_sdt.gxTpr_Amigroupowner )
         {
            if ( AV10group_sdt.gxTpr_Isactive )
            {
               AV13isReady = false;
            }
            else
            {
               AV15GXV1 = 1;
               while ( AV15GXV1 <= AV10group_sdt.gxTpr_Contact.Count )
               {
                  AV11groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV10group_sdt.gxTpr_Contact.Item(AV15GXV1));
                  if ( ! (DateTime.MinValue==AV11groupContact.gxTpr_Contactinvitacionaccepted) )
                  {
                     AV14totalInvitationsAccepted = (short)(AV14totalInvitationsAccepted+1);
                  }
                  AV15GXV1 = (int)(AV15GXV1+1);
               }
               if ( ( AV14totalInvitationsAccepted == AV10group_sdt.gxTpr_Contact.Count ) && ( AV14totalInvitationsAccepted > 0 ) )
               {
                  AV13isReady = true;
               }
               else
               {
                  AV13isReady = false;
               }
            }
         }
         else
         {
            AV13isReady = false;
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
         AV10group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         /* GeneXus formulas. */
      }

      private short AV14totalInvitationsAccepted ;
      private int AV15GXV1 ;
      private bool AV13isReady ;
      private Guid AV12groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV11groupContact ;
      private bool aP1_isReady ;
   }

}
