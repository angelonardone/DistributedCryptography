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
   public class deriveaddressstimebounty : GXProcedure
   {
      public deriveaddressstimebounty( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddressstimebounty( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           string aP2_networkType ,
                           short aP3_cuantity ,
                           short aP4_generatedType ,
                           string aP5_secret ,
                           out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP6_sdt_addressess ,
                           out string aP7_error )
      {
         this.AV18StoredTransactions = aP0_StoredTransactions;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV14networkType = aP2_networkType;
         this.AV9cuantity = aP3_cuantity;
         this.AV12generatedType = aP4_generatedType;
         this.AV39secret = aP5_secret;
         this.AV16sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP6_sdt_addressess=this.AV16sdt_addressess;
         aP7_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                string aP2_networkType ,
                                short aP3_cuantity ,
                                short aP4_generatedType ,
                                string aP5_secret ,
                                out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP6_sdt_addressess )
      {
         execute(aP0_StoredTransactions, aP1_group_sdt, aP2_networkType, aP3_cuantity, aP4_generatedType, aP5_secret, out aP6_sdt_addressess, out aP7_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 string aP2_networkType ,
                                 short aP3_cuantity ,
                                 short aP4_generatedType ,
                                 string aP5_secret ,
                                 out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP6_sdt_addressess ,
                                 out string aP7_error )
      {
         this.AV18StoredTransactions = aP0_StoredTransactions;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV14networkType = aP2_networkType;
         this.AV9cuantity = aP3_cuantity;
         this.AV12generatedType = aP4_generatedType;
         this.AV39secret = aP5_secret;
         this.AV16sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         SubmitImpl();
         aP6_sdt_addressess=this.AV16sdt_addressess;
         aP7_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV17sequence = 0;
         AV8countNotFound = 0;
         AV20base_char = "/";
         if ( AV13group_sdt.gxTpr_Contact.Count < 1 )
         {
            AV10error = "There are not enough contacts on the group";
            cleanup();
            if (true) return;
         }
         while ( AV8countNotFound < AV9cuantity )
         {
            /* Execute user subroutine: 'GENERATE ONE ADDRESS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               cleanup();
               if (true) return;
            }
            AV11found = false;
            AV42GXV1 = 1;
            while ( AV42GXV1 <= AV18StoredTransactions.gxTpr_Transaction.Count )
            {
               AV19TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV18StoredTransactions.gxTpr_Transaction.Item(AV42GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV19TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV15oneAddress.gxTpr_Address)) == 0 )
               {
                  AV11found = true;
                  if (true) break;
               }
               AV42GXV1 = (int)(AV42GXV1+1);
            }
            if ( ! AV11found )
            {
               AV16sdt_addressess.Add(AV15oneAddress, 0);
               AV8countNotFound = (long)(AV8countNotFound+1);
            }
            AV17sequence = (long)(AV17sequence+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'GENERATE ONE ADDRESS' Routine */
         returnInSub = false;
         AV27items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV43GXV2 = 1;
         while ( AV43GXV2 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV32oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV43GXV2));
            if ( ( AV13group_sdt.gxTpr_Grouptype == 20 ) && ! ( AV32oneContact.gxTpr_Contactid == AV32oneContact.gxTpr_Contactgroupid ) )
            {
               if ( (Convert.ToDecimal( AV12generatedType ) == NumberUtil.Val( "4", ".") ) )
               {
                  AV22extendedPublicKey = AV32oneContact.gxTpr_Extpubkeytimebountyreceiving;
                  AV34ownerPublicKey = AV13group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving;
               }
               else
               {
                  AV10error = "The generated Type is not a TimeBountyReceiving Type";
                  returnInSub = true;
                  if (true) return;
               }
               GXt_char1 = AV10error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV22extendedPublicKey,  AV14networkType,  AV20base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV17sequence), 10, 0)), out  AV23extPubKeyInfo, out  GXt_char1) ;
               AV10error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  AV27items.Add(StringUtil.Trim( AV23extPubKeyInfo.gxTpr_Ec_publickey), 0);
               }
               else
               {
                  if (true) break;
               }
            }
            AV43GXV2 = (int)(AV43GXV2+1);
         }
         GXt_char1 = AV10error;
         new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV34ownerPublicKey,  AV14networkType,  AV20base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV17sequence), 10, 0)), out  AV23extPubKeyInfo, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV34ownerPublicKey = AV23extPubKeyInfo.gxTpr_Ec_publickey;
         }
         else
         {
            returnInSub = true;
            if (true) return;
         }
         AV44GXV3 = 1;
         while ( AV44GXV3 <= AV13group_sdt.gxTpr_Timeconstrain.Count )
         {
            AV41oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV13group_sdt.gxTpr_Timeconstrain.Item(AV44GXV3));
            if ( AV41oneTimeConstrain.gxTpr_Sequence == AV17sequence )
            {
               AV38bountyRestoreDate = AV41oneTimeConstrain.gxTpr_Date;
            }
            AV44GXV3 = (int)(AV44GXV3+1);
         }
         GXt_char1 = AV10error;
         new GeneXus.Programs.wallet.registered.deroneaddressfortimebounty(context ).execute(  AV27items,  AV34ownerPublicKey,  AV14networkType,  AV38bountyRestoreDate,  AV39secret, out  AV31one_address, out  GXt_char1) ;
         AV10error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV15oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
            AV15oneAddress.gxTpr_Address = AV31one_address;
            AV15oneAddress.gxTpr_Generatedtype = AV12generatedType;
            AV15oneAddress.gxTpr_Isused = false;
            AV15oneAddress.gxTpr_Creationsequence = AV17sequence;
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
         AV16sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV10error = "";
         AV20base_char = "";
         AV19TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV15oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV27items = new GxSimpleCollection<string>();
         AV32oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV22extendedPublicKey = "";
         AV34ownerPublicKey = "";
         AV23extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV41oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV38bountyRestoreDate = DateTime.MinValue;
         GXt_char1 = "";
         AV31one_address = "";
         /* GeneXus formulas. */
      }

      private short AV9cuantity ;
      private short AV12generatedType ;
      private int AV42GXV1 ;
      private int AV43GXV2 ;
      private int AV44GXV3 ;
      private long AV17sequence ;
      private long AV8countNotFound ;
      private string AV14networkType ;
      private string AV39secret ;
      private string AV10error ;
      private string AV20base_char ;
      private string AV22extendedPublicKey ;
      private string AV34ownerPublicKey ;
      private string GXt_char1 ;
      private string AV31one_address ;
      private DateTime AV38bountyRestoreDate ;
      private bool returnInSub ;
      private bool AV11found ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV18StoredTransactions ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV16sdt_addressess ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV19TransactionItem ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15oneAddress ;
      private GxSimpleCollection<string> AV27items ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV32oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV23extPubKeyInfo ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV41oneTimeConstrain ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP6_sdt_addressess ;
      private string aP7_error ;
   }

}
