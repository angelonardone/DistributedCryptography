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
   public class getlegacygroupview : GXProcedure
   {
      public getlegacygroupview( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlegacygroupview( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                           out string aP2_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV15view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_view=this.AV15view;
         aP2_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view )
      {
         execute(aP0_groupId, out aP1_view, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ,
                                 out string aP2_error )
      {
         this.AV13groupId = aP0_groupId;
         this.AV15view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context) ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_view=this.AV15view;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV13groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         AV15view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) )
         {
            AV9error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic2;
         AV15view.gxTpr_Groupid = AV11group_sdt.gxTpr_Groupid;
         AV15view.gxTpr_Groupname = AV11group_sdt.gxTpr_Groupname;
         AV15view.gxTpr_Amigroupowner = AV11group_sdt.gxTpr_Amigroupowner;
         AV15view.gxTpr_Isactive = AV11group_sdt.gxTpr_Isactive;
         AV15view.gxTpr_Minimumshares = AV11group_sdt.gxTpr_Minimumshares;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV12groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV17GXV1));
            AV16viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
            AV16viewContact.gxTpr_Contactid = AV12groupContact.gxTpr_Contactid;
            AV16viewContact.gxTpr_Contactprivatename = AV12groupContact.gxTpr_Contactprivatename;
            AV16viewContact.gxTpr_Contactusername = AV12groupContact.gxTpr_Contactusername;
            AV16viewContact.gxTpr_Contactuserpubkey = AV12groupContact.gxTpr_Contactuserpubkey;
            AV16viewContact.gxTpr_Contactinvitationsent = AV12groupContact.gxTpr_Contactinvitationsent;
            AV16viewContact.gxTpr_Contactinvitacionaccepted = AV12groupContact.gxTpr_Contactinvitacionaccepted;
            AV16viewContact.gxTpr_Contactinvisent = AV12groupContact.gxTpr_Contactinvisent;
            if ( AV11group_sdt.gxTpr_Amigroupowner )
            {
               if ( ! ( AV12groupContact.gxTpr_Contactid == AV12groupContact.gxTpr_Contactgroupid ) )
               {
                  AV15view.gxTpr_Contact.Add(AV16viewContact, 0);
               }
               else
               {
                  if ( AV11group_sdt.gxTpr_Isactive && ! ( AV11group_sdt.gxTpr_Grouptype == 30 ) )
                  {
                     AV16viewContact.gxTpr_Contactprivatename = "me";
                     AV15view.gxTpr_Contact.Add(AV16viewContact, 0);
                  }
               }
            }
            else
            {
               if ( StringUtil.StrCmp(StringUtil.Trim( AV12groupContact.gxTpr_Contactuserpubkey), StringUtil.Trim( AV10externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey)) == 0 )
               {
                  AV16viewContact.gxTpr_Contactusername = "me";
               }
               else
               {
                  new GeneXus.Programs.wallet.registered.getcontactid(context ).execute(  StringUtil.Trim( AV12groupContact.gxTpr_Contactusername), out  AV14userPrivateName, out  AV8contactId) ;
                  if ( ! (Guid.Empty==AV8contactId) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV14userPrivateName)) )
                  {
                     AV16viewContact.gxTpr_Contactusername = StringUtil.Trim( AV14userPrivateName);
                  }
               }
               if ( ! ( ( AV11group_sdt.gxTpr_Grouptype == 30 ) && ( AV12groupContact.gxTpr_Contactid == AV12groupContact.gxTpr_Contactgroupid ) ) )
               {
                  AV15view.gxTpr_Contact.Add(AV16viewContact, 0);
               }
            }
            AV17GXV1 = (int)(AV17GXV1+1);
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
         AV15view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         AV9error = "";
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV12groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV16viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV14userPrivateName = "";
         AV8contactId = Guid.Empty;
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private string AV9error ;
      private string AV14userPrivateName ;
      private Guid AV13groupId ;
      private Guid AV8contactId ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV15view ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV12groupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV16viewContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView aP1_view ;
      private string aP2_error ;
   }

}
