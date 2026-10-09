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
   public class buildtransactionlegacy : GXProcedure
   {
      public buildtransactionlegacy( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildtransactionlegacy( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo> aP1_utxos ,
                           string aP2_sendTo ,
                           decimal aP3_amountBtc ,
                           string aP4_changeAddr ,
                           decimal aP5_feeBtc ,
                           bool aP6_sendAll ,
                           long aP7_sequence ,
                           bool aP8_isChange ,
                           string aP9_networkType ,
                           out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP10_result )
      {
         this.AV13group_sdt = aP0_group_sdt;
         this.AV25utxos = aP1_utxos;
         this.AV21sendTo = aP2_sendTo;
         this.AV8amountBtc = aP3_amountBtc;
         this.AV9changeAddr = aP4_changeAddr;
         this.AV12feeBtc = aP5_feeBtc;
         this.AV20sendAll = aP6_sendAll;
         this.AV22sequence = aP7_sequence;
         this.AV26isChange = aP8_isChange;
         this.AV16networkType = aP9_networkType;
         this.AV24result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         initialize();
         ExecuteImpl();
         aP10_result=this.AV24result;
      }

      public GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                                                                GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo> aP1_utxos ,
                                                                                string aP2_sendTo ,
                                                                                decimal aP3_amountBtc ,
                                                                                string aP4_changeAddr ,
                                                                                decimal aP5_feeBtc ,
                                                                                bool aP6_sendAll ,
                                                                                long aP7_sequence ,
                                                                                bool aP8_isChange ,
                                                                                string aP9_networkType )
      {
         execute(aP0_group_sdt, aP1_utxos, aP2_sendTo, aP3_amountBtc, aP4_changeAddr, aP5_feeBtc, aP6_sendAll, aP7_sequence, aP8_isChange, aP9_networkType, out aP10_result);
         return AV24result ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo> aP1_utxos ,
                                 string aP2_sendTo ,
                                 decimal aP3_amountBtc ,
                                 string aP4_changeAddr ,
                                 decimal aP5_feeBtc ,
                                 bool aP6_sendAll ,
                                 long aP7_sequence ,
                                 bool aP8_isChange ,
                                 string aP9_networkType ,
                                 out GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP10_result )
      {
         this.AV13group_sdt = aP0_group_sdt;
         this.AV25utxos = aP1_utxos;
         this.AV21sendTo = aP2_sendTo;
         this.AV8amountBtc = aP3_amountBtc;
         this.AV9changeAddr = aP4_changeAddr;
         this.AV12feeBtc = aP5_feeBtc;
         this.AV20sendAll = aP6_sendAll;
         this.AV22sequence = aP7_sequence;
         this.AV26isChange = aP8_isChange;
         this.AV16networkType = aP9_networkType;
         this.AV24result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context) ;
         SubmitImpl();
         aP10_result=this.AV24result;
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
         AV17ok = AV15groupProcessor.fromsdt(AV14groupEO);
         if ( ! AV17ok )
         {
            AV24result.gxTpr_Success = false;
            AV24result.gxTpr_Error = "FromSDT failed (null group)";
            cleanup();
            if (true) return;
         }
         AV23jsonText = AV15groupProcessor.buildlegacypsbt(AV25utxos.ToJSonString(false), StringUtil.Trim( AV21sendTo), StringUtil.Trim( StringUtil.Str( AV8amountBtc, 17, 8)), StringUtil.Trim( AV9changeAddr), StringUtil.Trim( StringUtil.Str( AV12feeBtc, 17, 8)), AV20sendAll, (int)(AV22sequence), AV26isChange, AV16networkType);
         AV24result.FromJSonString(AV23jsonText, null);
         cleanup();
      }

      protected void S111( )
      {
         /* 'MAP GROUP TO EO' Routine */
         returnInSub = false;
         if ( AV13group_sdt.gxTpr_Amigroupowner )
         {
            AV14groupEO.gxTpr_Extpubkeymultisigreceiving = AV13group_sdt.gxTpr_Extpubkeymultisigreceiving;
            AV14groupEO.gxTpr_Extpubkeymultisigchange = AV13group_sdt.gxTpr_Extpubkeymultisigchange;
         }
         else
         {
            AV14groupEO.gxTpr_Extpubkeymultisigreceiving = AV13group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigreceiving;
            AV14groupEO.gxTpr_Extpubkeymultisigchange = AV13group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeymultisigchange;
         }
         AV14groupEO.gxTpr_Groupname = AV13group_sdt.gxTpr_Groupname;
         AV14groupEO.gxTpr_Minimumshares = AV13group_sdt.gxTpr_Minimumshares;
         AV27GXV1 = 1;
         while ( AV27GXV1 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV18oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV27GXV1));
            if ( ! ( ( AV18oneContact.gxTpr_Contactid == AV18oneContact.gxTpr_Contactgroupid ) ) )
            {
               AV10contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
               AV10contactEO.gxTpr_Contactusername = AV18oneContact.gxTpr_Contactusername;
               AV10contactEO.gxTpr_Extpubkeymultisigreceiving = AV18oneContact.gxTpr_Extpubkeymultisigreceiving;
               AV10contactEO.gxTpr_Extpubkeymultisigchange = AV18oneContact.gxTpr_Extpubkeymultisigchange;
               AV14groupEO.gxTpr_Contact.Add(AV10contactEO, 0);
            }
            AV27GXV1 = (int)(AV27GXV1+1);
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
         AV24result = new GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult(context);
         AV14groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV15groupProcessor = new GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor(context);
         AV23jsonText = "";
         AV18oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV10contactEO = new GeneXus.Programs.distributedcryptographylib.SdtContactItem(context);
         /* GeneXus formulas. */
      }

      private int AV27GXV1 ;
      private long AV22sequence ;
      private decimal AV8amountBtc ;
      private decimal AV12feeBtc ;
      private string AV21sendTo ;
      private string AV9changeAddr ;
      private string AV16networkType ;
      private bool AV20sendAll ;
      private bool AV26isChange ;
      private bool returnInSub ;
      private bool AV17ok ;
      private string AV23jsonText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacyUtxo> AV25utxos ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult AV24result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV14groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupProcessor AV15groupProcessor ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV18oneContact ;
      private GeneXus.Programs.distributedcryptographylib.SdtContactItem AV10contactEO ;
      private GeneXus.Programs.wallet.registered.SdtLegacyPsbtResult aP10_result ;
   }

}
