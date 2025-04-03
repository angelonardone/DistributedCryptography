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
namespace GeneXus.Programs.general.ui {
   public class sidebarmenu : GXProcedure
   {
      public sidebarmenu( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sidebarmenu( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_SidebarItems )
      {
         this.AV15SidebarItems = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo") ;
         initialize();
         ExecuteImpl();
         aP0_SidebarItems=this.AV15SidebarItems;
      }

      public GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> executeUdp( )
      {
         execute(out aP0_SidebarItems);
         return AV15SidebarItems ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_SidebarItems )
      {
         this.AV15SidebarItems = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo") ;
         SubmitImpl();
         aP0_SidebarItems=this.AV15SidebarItems;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV13externalUser = GXt_SdtExternalUser1;
         GXt_SdtKeyInfo2 = AV19keyInfo;
         new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV19keyInfo = GXt_SdtKeyInfo2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19keyInfo.gxTpr_Privatekey)) )
         {
            AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
            AV16SidebarItem.gxTpr_Id = "Balance";
            AV16SidebarItem.gxTpr_Title = "Wallet Balance";
            AV16SidebarItem.gxTpr_Target = formatLink("wallet.balance") ;
            AV15SidebarItems.Add(AV16SidebarItem, 0);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13externalUser.gxTpr_Externaltoken)) )
            {
               AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
               AV16SidebarItem.gxTpr_Id = "DistCryptLogin";
               AV16SidebarItem.gxTpr_Title = "Login";
               AV16SidebarItem.gxTpr_Target = formatLink("wallet.distcryptlogin") ;
               AV15SidebarItems.Add(AV16SidebarItem, 0);
            }
            else
            {
               AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
               AV16SidebarItem.gxTpr_Id = "Online";
               AV16SidebarItem.gxTpr_Title = "Online";
               AV16SidebarItem.gxTpr_Target = "";
               AV16SidebarItem.gxTpr_Hassubitems = true;
               AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
               AV18SubItem.gxTpr_Id = "UserInfo";
               AV18SubItem.gxTpr_Title = "User Info";
               AV18SubItem.gxTpr_Target = formatLink("wallet.distcryptlogin") ;
               AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
               AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
               AV18SubItem.gxTpr_Id = "Contacts";
               AV18SubItem.gxTpr_Title = "Contacts";
               AV18SubItem.gxTpr_Target = formatLink("wallet.registered.contacts") ;
               AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
               AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
               AV18SubItem.gxTpr_Id = "SmartGroups";
               AV18SubItem.gxTpr_Title = "Smart Groups";
               AV18SubItem.gxTpr_Target = formatLink("wallet.registered.smartgroups") ;
               AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
               AV15SidebarItems.Add(AV16SidebarItem, 0);
            }
            AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
            AV16SidebarItem.gxTpr_Id = "Off-line";
            AV16SidebarItem.gxTpr_Title = "Off-line";
            AV16SidebarItem.gxTpr_Target = "";
            AV16SidebarItem.gxTpr_Hassubitems = true;
            AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
            AV18SubItem.gxTpr_Id = "EncryptedNotes";
            AV18SubItem.gxTpr_Title = "Encrypted Notes";
            AV18SubItem.gxTpr_Target = formatLink("wallet.encryptednotes") ;
            AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
            AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
            AV18SubItem.gxTpr_Id = "EncryptedFiles";
            AV18SubItem.gxTpr_Title = "Encrypted Files";
            AV18SubItem.gxTpr_Target = formatLink("wallet.encryptedfiles") ;
            AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
            AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
            AV18SubItem.gxTpr_Id = "Autheticators";
            AV18SubItem.gxTpr_Title = "Autheticators";
            AV18SubItem.gxTpr_Target = formatLink("wallet.autheticators") ;
            AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
            AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
            AV18SubItem.gxTpr_Id = "CreateQRCodes";
            AV18SubItem.gxTpr_Title = "Create QRCodes";
            AV18SubItem.gxTpr_Target = formatLink("qrcoder.createqrcode") ;
            AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
            AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
            AV18SubItem.gxTpr_Id = "SplitASecret";
            AV18SubItem.gxTpr_Title = "Split a secret";
            AV18SubItem.gxTpr_Target = formatLink("shamirss.shamirsecretsharing") ;
            AV16SidebarItem.gxTpr_Sidebarsubitems.Add(AV18SubItem, 0);
            AV15SidebarItems.Add(AV16SidebarItem, 0);
         }
         AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
         AV16SidebarItem.gxTpr_Id = "ReturnToWallets";
         AV16SidebarItem.gxTpr_Title = "Return to Wallets";
         AV16SidebarItem.gxTpr_Target = formatLink("wallet.returntowallets") ;
         AV15SidebarItems.Add(AV16SidebarItem, 0);
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
         AV15SidebarItems = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo");
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV19keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV16SidebarItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem(context);
         AV18SubItem = new GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> AV15SidebarItems ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV19keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem AV16SidebarItem ;
      private GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem_SubItem AV18SubItem ;
      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> aP0_SidebarItems ;
   }

}
