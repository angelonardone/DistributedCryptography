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
   public class savelegacygroup : GXProcedure
   {
      public savelegacygroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savelegacygroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           short aP1_minimumShares ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts ,
                           out string aP3_error )
      {
         this.AV15groupId = aP0_groupId;
         this.AV8minimumShares = aP1_minimumShares;
         this.AV9contacts = aP2_contacts;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                short aP1_minimumShares ,
                                GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts )
      {
         execute(aP0_groupId, aP1_minimumShares, aP2_contacts, out aP3_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 short aP1_minimumShares ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts ,
                                 out string aP3_error )
      {
         this.AV15groupId = aP0_groupId;
         this.AV8minimumShares = aP1_minimumShares;
         this.AV9contacts = aP2_contacts;
         this.AV10error = "" ;
         SubmitImpl();
         aP3_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( AV8minimumShares < 1 )
         {
            AV10error = "The \"Minimum amount of users  to approve a spend\" has to be at least 1";
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV12group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV15groupId, out  GXt_SdtGroup_SDT1) ;
         AV12group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV12group_sdt.gxTpr_Groupid) || ! AV12group_sdt.gxTpr_Amigroupowner )
         {
            AV10error = "Only the owner of the group can change it";
            cleanup();
            if (true) return;
         }
         if ( AV12group_sdt.gxTpr_Isactive )
         {
            AV10error = "The group is already active and can not be changed";
            cleanup();
            if (true) return;
         }
         if ( AV12group_sdt.gxTpr_Grouptype == 30 )
         {
            if ( AV9contacts.Count < 2 )
            {
               AV10error = "A Delegated multisignature group needs at least 2 members";
               cleanup();
               if (true) return;
            }
            if ( AV8minimumShares > AV9contacts.Count )
            {
               AV10error = "The \"Minimum amount of users  to approve a spend\" cannot be more than the amount of users selected";
               cleanup();
               if (true) return;
            }
            AV21smallerK = AV8minimumShares;
            if ( AV9contacts.Count - AV8minimumShares < AV21smallerK )
            {
               AV21smallerK = (short)(AV9contacts.Count-AV8minimumShares);
            }
            AV19combinations = 1;
            AV20i = 1;
            while ( AV20i <= AV21smallerK )
            {
               AV19combinations = (long)(AV19combinations*(AV9contacts.Count-AV20i+1)/ (decimal)(AV20i));
               AV20i = (short)(AV20i+1);
            }
            if ( AV19combinations > 2000 )
            {
               AV10error = "This group would have " + StringUtil.Trim( StringUtil.Str( (decimal)(AV19combinations), 18, 0)) + " combinations of signers; the maximum is 2000. Use fewer members, or a minimum closer to 1 or to all the members";
               cleanup();
               if (true) return;
            }
         }
         else
         {
            if ( AV8minimumShares > AV9contacts.Count + 1 )
            {
               AV10error = "The \"Minimum amount of users  to approve a spend\" cannot be more than the users selected plus you";
               cleanup();
               if (true) return;
            }
            if ( AV8minimumShares > 16 )
            {
               AV10error = "The \"Maximum amount of users  to approve a spend\" cannot be more than 16";
               cleanup();
               if (true) return;
            }
            if ( AV9contacts.Count + 1 > 16 )
            {
               AV10error = "A Legacy multisignature group can have at most 16 signers: you and 15 other users";
               cleanup();
               if (true) return;
            }
         }
         AV12group_sdt.gxTpr_Minimumshares = AV8minimumShares;
         AV17newContacts.Clear();
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV9contacts.Count )
         {
            AV18viewContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV9contacts.Item(AV22GXV1));
            AV11found = false;
            AV23GXV2 = 1;
            while ( AV23GXV2 <= AV12group_sdt.gxTpr_Contact.Count )
            {
               AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_sdt.gxTpr_Contact.Item(AV23GXV2));
               if ( AV13groupContact.gxTpr_Contactid == AV18viewContact.gxTpr_Contactid )
               {
                  AV14groupContactNew = (GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV13groupContact.Clone());
                  AV11found = true;
                  if (true) break;
               }
               AV23GXV2 = (int)(AV23GXV2+1);
            }
            if ( ! AV11found )
            {
               AV14groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
               AV14groupContactNew.gxTpr_Contactid = AV18viewContact.gxTpr_Contactid;
               AV14groupContactNew.gxTpr_Contactprivatename = AV18viewContact.gxTpr_Contactprivatename;
               AV14groupContactNew.gxTpr_Contactusername = AV18viewContact.gxTpr_Contactusername;
               AV14groupContactNew.gxTpr_Contactuserpubkey = AV18viewContact.gxTpr_Contactuserpubkey;
            }
            AV17newContacts.Add(AV14groupContactNew, 0);
            AV22GXV1 = (int)(AV22GXV1+1);
         }
         AV12group_sdt.gxTpr_Contact.Clear();
         AV12group_sdt.gxTpr_Contact = AV17newContacts;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV12group_sdt,  StringUtil.Trim( AV12group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV16grpupId, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GXt_char2 = AV10error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV12group_sdt, out  GXt_char2) ;
            AV10error = GXt_char2;
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
         AV10error = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17newContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         AV18viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV16grpupId = Guid.Empty;
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private short AV8minimumShares ;
      private short AV21smallerK ;
      private short AV20i ;
      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private long AV19combinations ;
      private string AV10error ;
      private string GXt_char2 ;
      private bool AV11found ;
      private Guid AV15groupId ;
      private Guid AV16grpupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV9contacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV17newContacts ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV18viewContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContactNew ;
      private string aP3_error ;
   }

}
