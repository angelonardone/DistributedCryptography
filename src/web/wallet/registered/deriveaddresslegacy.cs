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
   public class deriveaddresslegacy : GXProcedure
   {
      public deriveaddresslegacy( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddresslegacy( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           string aP1_networkType ,
                           long aP2_sequence ,
                           bool aP3_isChange ,
                           out GeneXus.Programs.wallet.registered.SdtLegacyAddressResult aP4_result )
      {
         this.AV10group_sdt = aP0_group_sdt;
         this.AV13networkType = aP1_networkType;
         this.AV17sequence = aP2_sequence;
         this.AV20isChange = aP3_isChange;
         this.AV19result = new GeneXus.Programs.wallet.registered.SdtLegacyAddressResult(context) ;
         initialize();
         ExecuteImpl();
         aP4_result=this.AV19result;
      }

      public GeneXus.Programs.wallet.registered.SdtLegacyAddressResult executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                                                                   string aP1_networkType ,
                                                                                   long aP2_sequence ,
                                                                                   bool aP3_isChange )
      {
         execute(aP0_group_sdt, aP1_networkType, aP2_sequence, aP3_isChange, out aP4_result);
         return AV19result ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 string aP1_networkType ,
                                 long aP2_sequence ,
                                 bool aP3_isChange ,
                                 out GeneXus.Programs.wallet.registered.SdtLegacyAddressResult aP4_result )
      {
         this.AV10group_sdt = aP0_group_sdt;
         this.AV13networkType = aP1_networkType;
         this.AV17sequence = aP2_sequence;
         this.AV20isChange = aP3_isChange;
         this.AV19result = new GeneXus.Programs.wallet.registered.SdtLegacyAddressResult(context) ;
         SubmitImpl();
         aP4_result=this.AV19result;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Execute user subroutine: 'MAP GROUP TO EO' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV14ok = AV12groupProcessor.fromsdt(AV11groupEO);
         if ( ! AV14ok )
         {
            AV19result.gxTpr_Success = false;
            AV19result.gxTpr_Error = "FromSDT failed (null group)";
            cleanup();
            if (true) return;
         }
         AV18jsonText = AV12groupProcessor.createlegacyaddress((int)(AV17sequence), AV20isChange, AV13networkType);
         AV19result.FromJSonString(AV18jsonText, null);
         cleanup();
      }

      protected void S111( )
      {
         /* 'MAP GROUP TO EO' Routine */
         returnInSub = false;
         if ( AV10group_sdt.gxTpr_Amigroupowner )
         {
            AV11groupEO.gxTpr_Extpubkeymultisigreceiving = AV10group_sdt.gxTpr_Extpubkeymultisigreceiving;
            AV11groupEO.gxTpr_Extpubkeymultisigchange = AV10group_sdt.gxTpr_Extpubkeymultisigchange;
         }
         else
         {
            AV11groupEO.gxTpr_Extpubkeymultisigreceiving = AV10group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
            AV11groupEO.gxTpr_Extpubkeymultisigchange = AV10group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
         }
         AV11groupEO.gxTpr_Groupname = AV10group_sdt.gxTpr_Groupname;
         AV11groupEO.gxTpr_Minimumshares = AV10group_sdt.gxTpr_Minimumshares;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV10group_sdt.gxTpr_Contact.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV10group_sdt.gxTpr_Contact.Item(AV21GXV1));
            if ( ! ( ( AV15oneContact.gxTpr_Contactid == AV15oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
               AV8contactEO.gxTpr_Contactusername = AV15oneContact.gxTpr_Contactusername;
               AV8contactEO.gxTpr_Extpubkeymultisigreceiving = AV15oneContact.gxTpr_Extpubkeymultisigreceiving;
               AV8contactEO.gxTpr_Extpubkeymultisigchange = AV15oneContact.gxTpr_Extpubkeymultisigchange;
               AV11groupEO.gxTpr_Contact.Add(AV8contactEO, 0);
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
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
         AV19result = new GeneXus.Programs.wallet.registered.SdtLegacyAddressResult(context);
         AV11groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV12groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         AV18jsonText = "";
         AV15oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private long AV17sequence ;
      private string AV13networkType ;
      private bool AV20isChange ;
      private bool returnInSub ;
      private bool AV14ok ;
      private string AV18jsonText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtLegacyAddressResult AV19result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV11groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV12groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15oneContact ;
      private GeneXus.Programs.distributedcryptographylib.SdtContactItem AV8contactEO ;
      private GeneXus.Programs.wallet.registered.SdtLegacyAddressResult aP4_result ;
   }

}
