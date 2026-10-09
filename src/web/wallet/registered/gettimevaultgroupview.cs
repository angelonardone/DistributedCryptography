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
   public class gettimevaultgroupview : GXProcedure
   {
      public gettimevaultgroupview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettimevaultgroupview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                           out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV12view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_view=this.AV12view;
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view )
      {
         execute(aP0_groupId, out aP1_view, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                                 out string aP2_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV12view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_view=this.AV12view;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV9group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV11groupId, out  GXt_SdtGroup_SDT1) ;
         AV9group_sdt = GXt_SdtGroup_SDT1;
         AV12view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         if ( (Guid.Empty==AV9group_sdt.gxTpr_Groupid) )
         {
            AV8error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         if ( AV9group_sdt.gxTpr_Subgrouptype == 20 )
         {
            AV14websession.Set("Group_EDIT_BOUNTY", AV9group_sdt.ToJSonString(false, true));
         }
         else
         {
            AV14websession.Set("Group_EDIT_DATA", AV9group_sdt.ToJSonString(false, true));
         }
         AV12view.gxTpr_Groupid = AV9group_sdt.gxTpr_Groupid;
         AV12view.gxTpr_Groupname = AV9group_sdt.gxTpr_Groupname;
         AV12view.gxTpr_Amigroupowner = AV9group_sdt.gxTpr_Amigroupowner;
         AV12view.gxTpr_Isactive = AV9group_sdt.gxTpr_Isactive;
         AV12view.gxTpr_Minimumshares = AV9group_sdt.gxTpr_Minimumshares;
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV9group_sdt.gxTpr_Contact.Count )
         {
            AV10groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV9group_sdt.gxTpr_Contact.Item(AV15GXV1));
            if ( ! ( AV10groupContact.gxTpr_Contactid == AV10groupContact.gxTpr_Contactgroupid ) )
            {
               AV13viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
               AV13viewContact.gxTpr_Contactid = AV10groupContact.gxTpr_Contactid;
               AV13viewContact.gxTpr_Numshares = AV10groupContact.gxTpr_Numshares;
               AV13viewContact.gxTpr_Contactprivatename = AV10groupContact.gxTpr_Contactprivatename;
               AV13viewContact.gxTpr_Contactusername = AV10groupContact.gxTpr_Contactusername;
               AV13viewContact.gxTpr_Contactuserpubkey = AV10groupContact.gxTpr_Contactuserpubkey;
               AV13viewContact.gxTpr_Contactinvitationsent = AV10groupContact.gxTpr_Contactinvitationsent;
               AV13viewContact.gxTpr_Contactinvitacionaccepted = AV10groupContact.gxTpr_Contactinvitacionaccepted;
               AV13viewContact.gxTpr_Contactinvisent = AV10groupContact.gxTpr_Contactinvisent;
               AV12view.gxTpr_Contact.Add(AV13viewContact, 0);
            }
            AV15GXV1 = (int)(AV15GXV1+1);
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
         AV12view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         AV8error = "";
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14websession = context.GetSession();
         AV10groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV13viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV8error ;
      private Guid AV11groupId ;
      private IGxSession AV14websession ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV12view ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV10groupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV13viewContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ;
      private string aP2_error ;
   }

}
