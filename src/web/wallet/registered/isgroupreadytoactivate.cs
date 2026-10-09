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
         this.AV29groupId = aP0_groupId;
         this.AV45isReady = false ;
         initialize();
         ExecuteImpl();
         aP1_isReady=this.AV45isReady;
      }

      public bool executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_isReady);
         return AV45isReady ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_isReady )
      {
         this.AV29groupId = aP0_groupId;
         this.AV45isReady = false ;
         SubmitImpl();
         aP1_isReady=this.AV45isReady;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV23group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV29groupId, out  GXt_SdtGroup_SDT1) ;
         AV23group_sdt = GXt_SdtGroup_SDT1;
         AV41totalInvitationsAccepted = 0;
         if ( AV23group_sdt.gxTpr_Amigroupowner )
         {
            if ( AV23group_sdt.gxTpr_Isactive )
            {
               AV45isReady = false;
            }
            else
            {
               AV46GXV1 = 1;
               while ( AV46GXV1 <= AV23group_sdt.gxTpr_Contact.Count )
               {
                  AV26groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV23group_sdt.gxTpr_Contact.Item(AV46GXV1));
                  if ( ! (DateTime.MinValue==AV26groupContact.gxTpr_Contactinvitacionaccepted) )
                  {
                     AV41totalInvitationsAccepted = (short)(AV41totalInvitationsAccepted+1);
                  }
                  AV46GXV1 = (int)(AV46GXV1+1);
               }
               if ( ( AV41totalInvitationsAccepted == AV23group_sdt.gxTpr_Contact.Count ) && ( AV41totalInvitationsAccepted > 0 ) )
               {
                  AV45isReady = true;
               }
               else
               {
                  AV45isReady = false;
               }
            }
         }
         else
         {
            AV45isReady = false;
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
         AV23group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV26groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         /* GeneXus formulas. */
      }

      private short AV41totalInvitationsAccepted ;
      private int AV46GXV1 ;
      private bool AV45isReady ;
      private Guid AV29groupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV23group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV26groupContact ;
      private bool aP1_isReady ;
   }

}
