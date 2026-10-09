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
   public class savetimevaultgroup : GXProcedure
   {
      public savetimevaultgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savetimevaultgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           short aP1_minimumShares ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts ,
                           out string aP3_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV8minimumShares = aP1_minimumShares;
         this.AV10contacts = aP2_contacts;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV11error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                short aP1_minimumShares ,
                                GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts )
      {
         execute(aP0_groupId, aP1_minimumShares, aP2_contacts, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 short aP1_minimumShares ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> aP2_contacts ,
                                 out string aP3_error )
      {
         this.AV16groupId = aP0_groupId;
         this.AV8minimumShares = aP1_minimumShares;
         this.AV10contacts = aP2_contacts;
         this.AV11error = "" ;
         SubmitImpl();
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV16groupId, out  GXt_SdtGroup_SDT1) ;
         AV13group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV13group_sdt.gxTpr_Groupid) || ! AV13group_sdt.gxTpr_Amigroupowner )
         {
            AV11error = "Only the owner of the Time Encrypted Vault can change it";
            cleanup();
            if (true) return;
         }
         if ( AV13group_sdt.gxTpr_Isactive )
         {
            AV11error = "The vault is active and its groups can not be changed";
            cleanup();
            if (true) return;
         }
         if ( AV13group_sdt.gxTpr_Subgrouptype == 20 )
         {
            if ( AV10contacts.Count == 0 )
            {
               AV11error = "You have to have at least one Contact assigned for bounty";
               cleanup();
               if (true) return;
            }
         }
         else
         {
            if ( AV8minimumShares <= 1 )
            {
               AV11error = "The \"Minimum amount of votes to recover the secret\" has to be at least 2";
               cleanup();
               if (true) return;
            }
            AV18hasContactEmptyShares = false;
            AV9totalUserShares = 0;
            AV22GXV1 = 1;
            while ( AV22GXV1 <= AV10contacts.Count )
            {
               AV20viewContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV10contacts.Item(AV22GXV1));
               AV9totalUserShares = (short)(AV9totalUserShares+(AV20viewContact.gxTpr_Numshares));
               if ( AV20viewContact.gxTpr_Numshares == 0 )
               {
                  AV18hasContactEmptyShares = true;
               }
               AV22GXV1 = (int)(AV22GXV1+1);
            }
            if ( AV8minimumShares > AV9totalUserShares )
            {
               AV11error = "The \"Minimum amount of votes to recover the secret\" cannot be bigger that the total amont of votes for each contact";
               cleanup();
               if (true) return;
            }
            if ( AV18hasContactEmptyShares )
            {
               AV11error = "Each contact must have at least one (1) vote";
               cleanup();
               if (true) return;
            }
            AV13group_sdt.gxTpr_Minimumshares = AV8minimumShares;
         }
         AV19newContacts.Clear();
         AV23GXV2 = 1;
         while ( AV23GXV2 <= AV10contacts.Count )
         {
            AV20viewContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV10contacts.Item(AV23GXV2));
            AV12found = false;
            AV24GXV3 = 1;
            while ( AV24GXV3 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV24GXV3));
               if ( AV14groupContact.gxTpr_Contactid == AV20viewContact.gxTpr_Contactid )
               {
                  AV15groupContactNew = (GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV14groupContact.Clone());
                  AV12found = true;
                  if (true) break;
               }
               AV24GXV3 = (int)(AV24GXV3+1);
            }
            if ( ! AV12found )
            {
               AV15groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
               AV15groupContactNew.gxTpr_Contactid = AV20viewContact.gxTpr_Contactid;
               AV15groupContactNew.gxTpr_Contactprivatename = AV20viewContact.gxTpr_Contactprivatename;
               AV15groupContactNew.gxTpr_Contactusername = AV20viewContact.gxTpr_Contactusername;
               AV15groupContactNew.gxTpr_Contactuserpubkey = AV20viewContact.gxTpr_Contactuserpubkey;
            }
            AV15groupContactNew.gxTpr_Numshares = AV20viewContact.gxTpr_Numshares;
            AV19newContacts.Add(AV15groupContactNew, 0);
            AV23GXV2 = (int)(AV23GXV2+1);
         }
         AV13group_sdt.gxTpr_Contact.Clear();
         AV13group_sdt.gxTpr_Contact = AV19newContacts;
         GXt_char2 = AV11error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV13group_sdt,  StringUtil.Trim( AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV17grpupId, out  GXt_char2) ;
         AV11error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char2 = AV11error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV13group_sdt, out  GXt_char2) ;
            AV11error = GXt_char2;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            if ( AV13group_sdt.gxTpr_Subgrouptype == 20 )
            {
               AV21websession.Set("Group_EDIT_BOUNTY", AV13group_sdt.ToJSonString(false, true));
            }
            else
            {
               AV21websession.Set("Group_EDIT_DATA", AV13group_sdt.ToJSonString(false, true));
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
         AV11error = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20viewContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV19newContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15groupContactNew = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV17grpupId = Guid.Empty;
         GXt_char2 = "";
         AV21websession = context.GetSession();
         /* GeneXus formulas. */
      }

      private short AV8minimumShares ;
      private short AV9totalUserShares ;
      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private string AV11error ;
      private string GXt_char2 ;
      private bool AV18hasContactEmptyShares ;
      private bool AV12found ;
      private Guid AV16groupId ;
      private Guid AV17grpupId ;
      private IGxSession AV21websession ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV10contacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV20viewContact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV19newContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15groupContactNew ;
      private string aP3_error ;
   }

}
