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
         this.AV29StoredTransactions = aP0_StoredTransactions;
         this.AV19group_sdt = aP1_group_sdt;
         this.AV21networkType = aP2_networkType;
         this.AV11cuantity = aP3_cuantity;
         this.AV18generatedType = aP4_generatedType;
         this.AV27secret = aP5_secret;
         this.AV26sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP6_sdt_addressess=this.AV26sdt_addressess;
         aP7_error=this.AV13error;
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
         return AV13error ;
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
         this.AV29StoredTransactions = aP0_StoredTransactions;
         this.AV19group_sdt = aP1_group_sdt;
         this.AV21networkType = aP2_networkType;
         this.AV11cuantity = aP3_cuantity;
         this.AV18generatedType = aP4_generatedType;
         this.AV27secret = aP5_secret;
         this.AV26sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV13error = "" ;
         SubmitImpl();
         aP6_sdt_addressess=this.AV26sdt_addressess;
         aP7_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV28sequence = 0;
         AV10countNotFound = 0;
         AV8base_char = "/";
         if ( AV19group_sdt.gxTpr_Contact.Count < 1 )
         {
            AV13error = "There are not enough contacts on the group";
            cleanup();
            if (true) return;
         }
         while ( AV10countNotFound < AV11cuantity )
         {
            /* Execute user subroutine: 'GENERATE ONE ADDRESS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               cleanup();
               if (true) return;
            }
            AV17found = false;
            AV32GXV1 = 1;
            while ( AV32GXV1 <= AV29StoredTransactions.gxTpr_Transaction.Count )
            {
               AV30TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV29StoredTransactions.gxTpr_Transaction.Item(AV32GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV30TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV23oneAddress.gxTpr_Address)) == 0 )
               {
                  AV17found = true;
                  if (true) break;
               }
               AV32GXV1 = (int)(AV32GXV1+1);
            }
            if ( ! AV17found )
            {
               AV26sdt_addressess.Add(AV23oneAddress, 0);
               AV10countNotFound = (long)(AV10countNotFound+1);
            }
            AV28sequence = (long)(AV28sequence+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'GENERATE ONE ADDRESS' Routine */
         returnInSub = false;
         AV20items = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV33GXV2 = 1;
         while ( AV33GXV2 <= AV19group_sdt.gxTpr_Contact.Count )
         {
            AV24oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV19group_sdt.gxTpr_Contact.Item(AV33GXV2));
            if ( ( AV19group_sdt.gxTpr_Grouptype == 20 ) && ! ( AV24oneContact.gxTpr_Contactid == AV24oneContact.gxTpr_Contactgroupid ) )
            {
               if ( (Convert.ToDecimal( AV18generatedType ) == NumberUtil.Val( "4", ".") ) )
               {
                  AV14extendedPublicKey = AV24oneContact.gxTpr_Extpubkeytimebountyreceiving;
                  AV25ownerPublicKey = AV19group_sdt.gxTpr_Othergroup.gxTpr_Extpubkeytimebountyreceiving;
               }
               else
               {
                  AV13error = "The generated Type is not a TimeBountyReceiving Type";
                  returnInSub = true;
                  if (true) return;
               }
               GXt_char1 = AV13error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV14extendedPublicKey,  AV21networkType,  AV8base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV28sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char1) ;
               AV13error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV20items.Add(StringUtil.Trim( AV15extPubKeyInfo.gxTpr_Ec_publickey), 0);
               }
               else
               {
                  if (true) break;
               }
            }
            AV33GXV2 = (int)(AV33GXV2+1);
         }
         GXt_char1 = AV13error;
         new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV25ownerPublicKey,  AV21networkType,  AV8base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV28sequence), 10, 0)), out  AV15extPubKeyInfo, out  GXt_char1) ;
         AV13error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV25ownerPublicKey = AV15extPubKeyInfo.gxTpr_Ec_publickey;
         }
         else
         {
            returnInSub = true;
            if (true) return;
         }
         AV34GXV3 = 1;
         while ( AV34GXV3 <= AV19group_sdt.gxTpr_Timeconstrain.Count )
         {
            AV31oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV19group_sdt.gxTpr_Timeconstrain.Item(AV34GXV3));
            if ( AV31oneTimeConstrain.gxTpr_Sequence == AV28sequence )
            {
               AV9bountyRestoreDate = AV31oneTimeConstrain.gxTpr_Date;
            }
            AV34GXV3 = (int)(AV34GXV3+1);
         }
         GXt_char1 = AV13error;
         new GeneXus.Programs.wallet.registered.deroneaddressfortimebounty(context ).execute(  AV20items,  AV25ownerPublicKey,  AV21networkType,  AV9bountyRestoreDate,  AV27secret, out  AV22one_address, out  GXt_char1) ;
         AV13error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV23oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
            AV23oneAddress.gxTpr_Address = AV22one_address;
            AV23oneAddress.gxTpr_Generatedtype = AV18generatedType;
            AV23oneAddress.gxTpr_Isused = false;
            AV23oneAddress.gxTpr_Creationsequence = AV28sequence;
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
         AV26sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV13error = "";
         AV8base_char = "";
         AV30TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV23oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV20items = new GxSimpleCollection<string>();
         AV24oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV14extendedPublicKey = "";
         AV25ownerPublicKey = "";
         AV15extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV31oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV9bountyRestoreDate = DateTime.MinValue;
         GXt_char1 = "";
         AV22one_address = "";
         /* GeneXus formulas. */
      }

      private short AV11cuantity ;
      private short AV18generatedType ;
      private int AV32GXV1 ;
      private int AV33GXV2 ;
      private int AV34GXV3 ;
      private long AV28sequence ;
      private long AV10countNotFound ;
      private string AV21networkType ;
      private string AV27secret ;
      private string AV13error ;
      private string AV8base_char ;
      private string AV14extendedPublicKey ;
      private string AV25ownerPublicKey ;
      private string GXt_char1 ;
      private string AV22one_address ;
      private DateTime AV9bountyRestoreDate ;
      private bool returnInSub ;
      private bool AV17found ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV29StoredTransactions ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV26sdt_addressess ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV30TransactionItem ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV23oneAddress ;
      private GxSimpleCollection<string> AV20items ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV24oneContact ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV15extPubKeyInfo ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV31oneTimeConstrain ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP6_sdt_addressess ;
      private string aP7_error ;
   }

}
