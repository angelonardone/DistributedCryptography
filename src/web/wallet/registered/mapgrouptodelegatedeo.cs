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
   public class mapgrouptodelegatedeo : GXProcedure
   {
      public mapgrouptodelegatedeo( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public mapgrouptodelegatedeo( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           out GeneXus.Programs.distributedcryptographylib.SdtGroupSDT aP1_groupEO )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV10groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context) ;
         initialize();
         ExecuteImpl();
         aP1_groupEO=this.AV10groupEO;
      }

      public GeneXus.Programs.distributedcryptographylib.SdtGroupSDT executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt )
      {
         execute(aP0_group_sdt, out aP1_groupEO);
         return AV10groupEO ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 out GeneXus.Programs.distributedcryptographylib.SdtGroupSDT aP1_groupEO )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV10groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context) ;
         SubmitImpl();
         aP1_groupEO=this.AV10groupEO;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10groupEO.gxTpr_Grouptype = AV9group_sdt.gxTpr_Grouptype;
         AV10groupEO.gxTpr_Amigroupowner = AV9group_sdt.gxTpr_Amigroupowner;
         AV12otherGroupEO.gxTpr_Referenceusername = AV9group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname;
         AV10groupEO.gxTpr_Othergroup = AV12otherGroupEO;
         AV10groupEO.gxTpr_Extpubkeymultisigreceiving = AV9group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
         AV10groupEO.gxTpr_Extpubkeymultisigchange = AV9group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
         AV10groupEO.gxTpr_Groupname = AV9group_sdt.gxTpr_Groupname;
         AV10groupEO.gxTpr_Minimumshares = AV9group_sdt.gxTpr_Minimumshares;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV9group_sdt.gxTpr_Contact.Count )
         {
            AV11oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV9group_sdt.gxTpr_Contact.Item(AV13GXV1));
            if ( ! ( ( AV11oneContact.gxTpr_Contactid == AV11oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
               AV8contactEO.gxTpr_Contactusername = AV11oneContact.gxTpr_Contactusername;
               AV8contactEO.gxTpr_Extpubkeymultisigreceiving = AV11oneContact.gxTpr_Extpubkeymultisigreceiving;
               AV8contactEO.gxTpr_Extpubkeymultisigchange = AV11oneContact.gxTpr_Extpubkeymultisigchange;
               AV10groupEO.gxTpr_Contact.Add(AV8contactEO, 0);
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV10groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV12otherGroupEO = new GeneXus.Programs.distributedcryptographylib.SdtOtherGroup(context);
         AV11oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV10groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtOtherGroup AV12otherGroupEO ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV11oneContact ;
      private GeneXus.Programs.distributedcryptographylib.SdtContactItem AV8contactEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT aP1_groupEO ;
   }

}
