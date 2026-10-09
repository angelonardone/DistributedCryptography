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
   public class getwalletbackupview : GXProcedure
   {
      public getwalletbackupview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwalletbackupview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                           out string aP2_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV13view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_view=this.AV13view;
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
         this.AV12groupId = aP0_groupId;
         this.AV13view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_view=this.AV13view;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV9group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV12groupId, out  GXt_SdtGroup_SDT1) ;
         AV9group_sdt = GXt_SdtGroup_SDT1;
         AV13view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         if ( (Guid.Empty==AV9group_sdt.gxTpr_Groupid) )
         {
            AV8error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         AV13view.gxTpr_Groupid = AV9group_sdt.gxTpr_Groupid;
         AV13view.gxTpr_Groupname = AV9group_sdt.gxTpr_Groupname;
         AV13view.gxTpr_Amigroupowner = AV9group_sdt.gxTpr_Amigroupowner;
         AV13view.gxTpr_Isactive = AV9group_sdt.gxTpr_Isactive;
         AV13view.gxTpr_Numofsharesreached = AV9group_sdt.gxTpr_Numofsharesreached;
         if ( AV9group_sdt.gxTpr_Amigroupowner )
         {
            AV10group_sdt_owner = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV9group_sdt.Clone());
         }
         else
         {
            GXt_char2 = AV8error;
            new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV9group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV9group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV10group_sdt_owner, out  GXt_char2) ;
            AV8error = GXt_char2;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV13view.gxTpr_Minimumshares = AV10group_sdt_owner.gxTpr_Minimumshares;
            AV13view.gxTpr_Restorestopped = AV10group_sdt_owner.gxTpr_Restorestopped;
            AV13view.gxTpr_Restorestoppeddatetime = AV10group_sdt_owner.gxTpr_Restorestoppeddatetime;
            AV15GXV1 = 1;
            while ( AV15GXV1 <= AV10group_sdt_owner.gxTpr_Contact.Count )
            {
               AV11groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV10group_sdt_owner.gxTpr_Contact.Item(AV15GXV1));
               AV14viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
               AV14viewContact.gxTpr_Contactid = AV11groupContact.gxTpr_Contactid;
               AV14viewContact.gxTpr_Numshares = AV11groupContact.gxTpr_Numshares;
               AV14viewContact.gxTpr_Contactprivatename = AV11groupContact.gxTpr_Contactprivatename;
               AV14viewContact.gxTpr_Contactusername = AV11groupContact.gxTpr_Contactusername;
               AV14viewContact.gxTpr_Contactuserpubkey = AV11groupContact.gxTpr_Contactuserpubkey;
               AV14viewContact.gxTpr_Contactinvitationsent = AV11groupContact.gxTpr_Contactinvitationsent;
               AV14viewContact.gxTpr_Contactinvitacionaccepted = AV11groupContact.gxTpr_Contactinvitacionaccepted;
               AV14viewContact.gxTpr_Contactinvisent = AV11groupContact.gxTpr_Contactinvisent;
               AV14viewContact.gxTpr_Restoresigneddatetime = AV11groupContact.gxTpr_Restoresigneddatetime;
               AV13view.gxTpr_Contact.Add(AV14viewContact, 0);
               AV15GXV1 = (int)(AV15GXV1+1);
            }
         }
         else
         {
            AV13view.gxTpr_Minimumshares = AV9group_sdt.gxTpr_Minimumshares;
            AV13view.gxTpr_Restorestopped = AV9group_sdt.gxTpr_Restorestopped;
            AV13view.gxTpr_Restorestoppeddatetime = AV9group_sdt.gxTpr_Restorestoppeddatetime;
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
         AV13view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         AV8error = "";
         AV9group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV10group_sdt_owner = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_char2 = "";
         AV11groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV8error ;
      private string GXt_char2 ;
      private Guid AV12groupId ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV13view ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt_owner ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV11groupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV14viewContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ;
      private string aP2_error ;
   }

}
