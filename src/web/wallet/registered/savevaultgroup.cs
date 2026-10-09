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
   public class savevaultgroup : GXProcedure
   {
      public savevaultgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savevaultgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP1_contacts ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV8contacts = aP1_contacts;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP1_contacts )
      {
         execute(aP0_groupId, aP1_contacts, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP1_contacts ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV8contacts = aP1_contacts;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV11group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV11group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) || ! AV11group_sdt.gxTpr_Amigroupowner )
         {
            AV9error = "Only the owner of the group can change it";
            cleanup();
            if (true) return;
         }
         AV16newContacts.Clear();
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV8contacts.Count )
         {
            AV17viewContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV8contacts.Item(AV18GXV1));
            AV10found = false;
            AV19GXV2 = 1;
            while ( AV19GXV2 <= AV11group_sdt.gxTpr_Contact.Count )
            {
               AV12groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV19GXV2));
               if ( AV12groupContact.gxTpr_Contactid == AV17viewContact.gxTpr_Contactid )
               {
                  AV13groupContactNew = (GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV12groupContact.Clone());
                  AV10found = true;
                  if (true) break;
               }
               AV19GXV2 = (int)(AV19GXV2+1);
            }
            if ( ! AV10found )
            {
               AV13groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
               AV13groupContactNew.gxTpr_Contactid = AV17viewContact.gxTpr_Contactid;
               AV13groupContactNew.gxTpr_Contactprivatename = AV17viewContact.gxTpr_Contactprivatename;
               AV13groupContactNew.gxTpr_Contactusername = AV17viewContact.gxTpr_Contactusername;
               AV13groupContactNew.gxTpr_Contactuserpubkey = AV17viewContact.gxTpr_Contactuserpubkey;
            }
            AV16newContacts.Add(AV13groupContactNew, 0);
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         AV11group_sdt.gxTpr_Contact.Clear();
         AV11group_sdt.gxTpr_Contact = AV16newContacts;
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char2) ;
         AV9error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV11group_sdt, out  GXt_char2) ;
            AV9error = GXt_char2;
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
         AV9error = "";
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16newContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         AV17viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV12groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV13groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15grpupId = Guid.Empty;
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private string AV9error ;
      private string GXt_char2 ;
      private bool AV10found ;
      private Guid AV14groupId ;
      private Guid AV15grpupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV8contacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV16newContacts ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV17viewContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV12groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContactNew ;
      private string aP2_error ;
   }

}
