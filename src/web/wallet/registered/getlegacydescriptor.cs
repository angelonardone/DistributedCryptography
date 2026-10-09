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
   public class getlegacydescriptor : GXProcedure
   {
      public getlegacydescriptor( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlegacydescriptor( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           string aP1_networkType ,
                           bool aP2_isChange ,
                           out GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult aP3_result )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV14networkType = aP1_networkType;
         this.AV12isChange = aP2_isChange;
         this.AV17result = new GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult(context) ;
         initialize();
         ExecuteImpl();
         aP3_result=this.AV17result;
      }

      public GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                                                                     string aP1_networkType ,
                                                                                     bool aP2_isChange )
      {
         execute(aP0_group_sdt, aP1_networkType, aP2_isChange, out aP3_result);
         return AV17result ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 string aP1_networkType ,
                                 bool aP2_isChange ,
                                 out GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult aP3_result )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV14networkType = aP1_networkType;
         this.AV12isChange = aP2_isChange;
         this.AV17result = new GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult(context) ;
         SubmitImpl();
         aP3_result=this.AV17result;
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
         AV15ok = AV11groupProcessor.fromsdt(AV10groupEO);
         if ( ! AV15ok )
         {
            AV17result.gxTpr_Success = false;
            AV17result.gxTpr_Error = "FromSDT failed (null group)";
            cleanup();
            if (true) return;
         }
         AV13jsonText = AV11groupProcessor.getlegacydescriptor(AV12isChange, AV14networkType);
         AV17result.FromJSonString(AV13jsonText, null);
         cleanup();
      }

      protected void S111( )
      {
         /* 'MAP GROUP TO EO' Routine */
         returnInSub = false;
         if ( AV9group_sdt.gxTpr_Amigroupowner )
         {
            AV10groupEO.gxTpr_Extpubkeymultisigreceiving = AV9group_sdt.gxTpr_Extpubkeymultisigreceiving;
            AV10groupEO.gxTpr_Extpubkeymultisigchange = AV9group_sdt.gxTpr_Extpubkeymultisigchange;
         }
         else
         {
            AV10groupEO.gxTpr_Extpubkeymultisigreceiving = AV9group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
            AV10groupEO.gxTpr_Extpubkeymultisigchange = AV9group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
         }
         AV10groupEO.gxTpr_Groupname = AV9group_sdt.gxTpr_Groupname;
         AV10groupEO.gxTpr_Minimumshares = AV9group_sdt.gxTpr_Minimumshares;
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV9group_sdt.gxTpr_Contact.Count )
         {
            AV16oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV9group_sdt.gxTpr_Contact.Item(AV18GXV1));
            if ( ! ( ( AV16oneContact.gxTpr_Contactid == AV16oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
               AV8contactEO.gxTpr_Contactusername = AV16oneContact.gxTpr_Contactusername;
               AV8contactEO.gxTpr_Extpubkeymultisigreceiving = AV16oneContact.gxTpr_Extpubkeymultisigreceiving;
               AV8contactEO.gxTpr_Extpubkeymultisigchange = AV16oneContact.gxTpr_Extpubkeymultisigchange;
               AV10groupEO.gxTpr_Contact.Add(AV8contactEO, 0);
            }
            AV18GXV1 = (int)(AV18GXV1+1);
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
         AV17result = new GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult(context);
         AV10groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV11groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         AV13jsonText = "";
         AV16oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV8contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV18GXV1 ;
      private string AV14networkType ;
      private bool AV12isChange ;
      private bool returnInSub ;
      private bool AV15ok ;
      private string AV13jsonText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult AV17result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV10groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV11groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16oneContact ;
      private GeneXus.Programs.distributedcryptographylib.SdtContactItem AV8contactEO ;
      private GeneXus.Programs.wallet.registered.SdtGroupDescriptorResult aP3_result ;
   }

}
