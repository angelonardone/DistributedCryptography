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
   public class getwalletbackupsignatures : GXProcedure
   {
      public getwalletbackupsignatures( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getwalletbackupsignatures( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_isOwner ,
                           out bool aP2_restoreStopped ,
                           out string aP3_restoreStatus ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP4_signatures ,
                           out string aP5_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV20isOwner = false ;
         this.AV23restoreStopped = false ;
         this.AV22restoreStatus = "" ;
         this.AV24signatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography") ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isOwner=this.AV20isOwner;
         aP2_restoreStopped=this.AV23restoreStopped;
         aP3_restoreStatus=this.AV22restoreStatus;
         aP4_signatures=this.AV24signatures;
         aP5_error=this.AV12error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out bool aP1_isOwner ,
                                out bool aP2_restoreStopped ,
                                out string aP3_restoreStatus ,
                                out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP4_signatures )
      {
         execute(aP0_groupId, out aP1_isOwner, out aP2_restoreStopped, out aP3_restoreStatus, out aP4_signatures, out aP5_error);
         return AV12error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_isOwner ,
                                 out bool aP2_restoreStopped ,
                                 out string aP3_restoreStatus ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP4_signatures ,
                                 out string aP5_error )
      {
         this.AV19groupId = aP0_groupId;
         this.AV20isOwner = false ;
         this.AV23restoreStopped = false ;
         this.AV22restoreStatus = "" ;
         this.AV24signatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography") ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_isOwner=this.AV20isOwner;
         aP2_restoreStopped=this.AV23restoreStopped;
         aP3_restoreStatus=this.AV22restoreStatus;
         aP4_signatures=this.AV24signatures;
         aP5_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV15group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV19groupId, out  GXt_SdtGroup_SDT1) ;
         AV15group_sdt = GXt_SdtGroup_SDT1;
         AV20isOwner = AV15group_sdt.gxTpr_Amigroupowner;
         AV24signatures.Clear();
         AV9signedShares = 0;
         AV8signedMembers = 0;
         if ( AV20isOwner )
         {
            AV16group_sdt_owner = (GeneXus.Programs.wallet.registered.SdtGroup_SDT)(AV15group_sdt.Clone());
         }
         else
         {
            GXt_char2 = AV12error;
            new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV15group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV15group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV16group_sdt_owner, out  GXt_char2) ;
            AV12error = GXt_char2;
         }
         AV25GXV1 = 1;
         while ( AV25GXV1 <= AV16group_sdt_owner.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV16group_sdt_owner.gxTpr_Contact.Item(AV25GXV1));
            AV21oneSignature = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
            AV21oneSignature.gxTpr_Contactusername = AV17groupContact.gxTpr_Contactusername;
            AV21oneSignature.gxTpr_Numshares = AV17groupContact.gxTpr_Numshares;
            AV21oneSignature.gxTpr_Restoresigneddatetime = AV17groupContact.gxTpr_Restoresigneddatetime;
            if ( ! AV20isOwner )
            {
               AV26GXV2 = 1;
               while ( AV26GXV2 <= AV15group_sdt.gxTpr_Contact.Count )
               {
                  AV18groupContactMy = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV15group_sdt.gxTpr_Contact.Item(AV26GXV2));
                  if ( ( StringUtil.StrCmp(StringUtil.Trim( AV18groupContactMy.gxTpr_Contactusername), StringUtil.Trim( AV17groupContact.gxTpr_Contactusername)) == 0 ) && ( AV18groupContactMy.gxTpr_Restoresigneddatetime > AV21oneSignature.gxTpr_Restoresigneddatetime ) )
                  {
                     AV21oneSignature.gxTpr_Restoresigneddatetime = AV18groupContactMy.gxTpr_Restoresigneddatetime;
                  }
                  AV26GXV2 = (int)(AV26GXV2+1);
               }
            }
            GXt_char2 = "";
            new GeneXus.Programs.wallet.registered.getmemberdisplayname(context ).execute(  AV17groupContact.gxTpr_Contactusername, out  GXt_char2) ;
            AV21oneSignature.gxTpr_Contactprivatename = GXt_char2;
            if ( ! (DateTime.MinValue==AV21oneSignature.gxTpr_Restoresigneddatetime) )
            {
               AV9signedShares = (short)(AV9signedShares+(AV17groupContact.gxTpr_Numshares));
               AV8signedMembers = (short)(AV8signedMembers+1);
            }
            AV24signatures.Add(AV21oneSignature, 0);
            AV25GXV1 = (int)(AV25GXV1+1);
         }
         AV24signatures.Sort("[restoreSignedDateTime]");
         AV23restoreStopped = AV16group_sdt_owner.gxTpr_Restorestopped;
         if ( AV23restoreStopped )
         {
            AV22restoreStatus = "Restore STOPPED by the owner on " + context.localUtil.TToC( AV16group_sdt_owner.gxTpr_Restorestoppeddatetime, 8, 5, 1, 2, "/", ":", " ");
         }
         else
         {
            if ( AV8signedMembers == 0 )
            {
               AV22restoreStatus = "No restore in progress";
            }
            else
            {
               if ( AV9signedShares >= AV16group_sdt_owner.gxTpr_Minimumshares )
               {
                  AV22restoreStatus = "Restore COMPLETED: " + StringUtil.Trim( StringUtil.Str( (decimal)(AV9signedShares), 4, 0)) + " of " + StringUtil.Trim( StringUtil.Str( (decimal)(AV16group_sdt_owner.gxTpr_Minimumshares), 4, 0)) + " required shares were signed";
               }
               else
               {
                  AV22restoreStatus = "Restore IN PROGRESS: " + StringUtil.Trim( StringUtil.Str( (decimal)(AV9signedShares), 4, 0)) + " of " + StringUtil.Trim( StringUtil.Str( (decimal)(AV16group_sdt_owner.gxTpr_Minimumshares), 4, 0)) + " required shares signed";
               }
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
         AV22restoreStatus = "";
         AV24signatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography");
         AV12error = "";
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16group_sdt_owner = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV21oneSignature = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV18groupContactMy = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private short AV9signedShares ;
      private short AV8signedMembers ;
      private int AV25GXV1 ;
      private int AV26GXV2 ;
      private string AV22restoreStatus ;
      private string AV12error ;
      private string GXt_char2 ;
      private bool AV20isOwner ;
      private bool AV23restoreStopped ;
      private Guid AV19groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV24signatures ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_owner ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17groupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV21oneSignature ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV18groupContactMy ;
      private bool aP1_isOwner ;
      private bool aP2_restoreStopped ;
      private string aP3_restoreStatus ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP4_signatures ;
      private string aP5_error ;
   }

}
