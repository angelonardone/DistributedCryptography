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
   public class gettimevaultmemberview : GXProcedure
   {
      public gettimevaultmemberview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettimevaultmemberview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                           out DateTime aP2_restoreDate ,
                           out bool aP3_canRestore ,
                           out string aP4_status ,
                           out string aP5_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV19view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV16restoreDate = DateTime.MinValue ;
         this.AV9canRestore = false ;
         this.AV18status = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_view=this.AV19view;
         aP2_restoreDate=this.AV16restoreDate;
         aP3_canRestore=this.AV9canRestore;
         aP4_status=this.AV18status;
         aP5_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                                out DateTime aP2_restoreDate ,
                                out bool aP3_canRestore ,
                                out string aP4_status )
      {
         execute(aP0_groupId, out aP1_view, out aP2_restoreDate, out aP3_canRestore, out aP4_status, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                                 out DateTime aP2_restoreDate ,
                                 out bool aP3_canRestore ,
                                 out string aP4_status ,
                                 out string aP5_error )
      {
         this.AV12groupId = aP0_groupId;
         this.AV19view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV16restoreDate = DateTime.MinValue ;
         this.AV9canRestore = false ;
         this.AV18status = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_view=this.AV19view;
         aP2_restoreDate=this.AV16restoreDate;
         aP3_canRestore=this.AV9canRestore;
         aP4_status=this.AV18status;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9canRestore = false;
         AV18status = "";
         GXt_SdtGroup_SDT1 = AV13my_group;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV12groupId, out  GXt_SdtGroup_SDT1) ;
         AV13my_group = GXt_SdtGroup_SDT1;
         AV19view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         if ( (Guid.Empty==AV13my_group.gxTpr_Groupid) )
         {
            AV10error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         AV19view.gxTpr_Groupid = AV13my_group.gxTpr_Groupid;
         AV19view.gxTpr_Groupname = AV13my_group.gxTpr_Groupname;
         AV19view.gxTpr_Amigroupowner = AV13my_group.gxTpr_Amigroupowner;
         AV19view.gxTpr_Isactive = AV13my_group.gxTpr_Isactive;
         AV19view.gxTpr_Numofsharesreached = AV13my_group.gxTpr_Numofsharesreached;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV13my_group.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV13my_group.gxTpr_Othergroup.gxTpr_Encpassword, out  AV15owner_group, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV19view.gxTpr_Minimumshares = AV15owner_group.gxTpr_Minimumshares;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV15owner_group.gxTpr_Contact.Count )
         {
            AV11groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15owner_group.gxTpr_Contact.Item(AV21GXV1));
            AV20viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
            AV20viewContact.gxTpr_Contactid = AV11groupContact.gxTpr_Contactid;
            AV20viewContact.gxTpr_Numshares = AV11groupContact.gxTpr_Numshares;
            AV20viewContact.gxTpr_Contactprivatename = AV11groupContact.gxTpr_Contactprivatename;
            AV20viewContact.gxTpr_Contactusername = AV11groupContact.gxTpr_Contactusername;
            AV20viewContact.gxTpr_Contactinvitationsent = AV11groupContact.gxTpr_Contactinvitationsent;
            AV20viewContact.gxTpr_Contactinvitacionaccepted = AV11groupContact.gxTpr_Contactinvitacionaccepted;
            AV19view.gxTpr_Contact.Add(AV20viewContact, 0);
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         if ( AV13my_group.gxTpr_Isactive )
         {
            GXt_SdtGroup_SDT_TimeConstrainItem3 = AV14oneTimeConstrain;
            new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV15owner_group.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem3) ;
            AV14oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem3;
            AV16restoreDate = AV14oneTimeConstrain.gxTpr_Date;
            if ( ! AV13my_group.gxTpr_Numofsharesreached )
            {
               GXt_char2 = AV10error;
               new GeneXus.Programs.electrum.getsecretfromoneaddress(context ).execute(  StringUtil.Trim( AV14oneTimeConstrain.gxTpr_Address), out  AV8bountyActive, out  AV17secret, out  GXt_char2) ;
               AV10error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  if ( AV8bountyActive || String.IsNullOrEmpty(StringUtil.RTrim( AV17secret)) )
                  {
                     AV18status = "Although the backup is Active it needs final authorization from the owner";
                  }
                  else
                  {
                     AV9canRestore = true;
                  }
               }
               AV17secret = "";
            }
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
         AV19view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         AV16restoreDate = DateTime.MinValue;
         AV18status = "";
         AV10error = "";
         AV13my_group = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15owner_group = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV20viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV14oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem3 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_char2 = "";
         AV17secret = "";
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private string AV18status ;
      private string AV10error ;
      private string GXt_char2 ;
      private string AV17secret ;
      private DateTime AV16restoreDate ;
      private bool AV9canRestore ;
      private bool AV8bountyActive ;
      private Guid AV12groupId ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV19view ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13my_group ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15owner_group ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV11groupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV20viewContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV14oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem3 ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ;
      private DateTime aP2_restoreDate ;
      private bool aP3_canRestore ;
      private string aP4_status ;
      private string aP5_error ;
   }

}
