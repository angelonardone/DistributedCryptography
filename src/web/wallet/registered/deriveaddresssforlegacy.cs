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
   public class deriveaddresssforlegacy : GXProcedure
   {
      public deriveaddresssforlegacy( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddresssforlegacy( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           string aP2_networkType ,
                           long aP3_cuantity ,
                           bool aP4_isChange ,
                           out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ,
                           out string aP6_error )
      {
         this.AV20StoredTransactions = aP0_StoredTransactions;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV15networkType = aP2_networkType;
         this.AV9cuantity = aP3_cuantity;
         this.AV14isChange = aP4_isChange;
         this.AV18sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP5_sdt_addressess=this.AV18sdt_addressess;
         aP6_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                string aP2_networkType ,
                                long aP3_cuantity ,
                                bool aP4_isChange ,
                                out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess )
      {
         execute(aP0_StoredTransactions, aP1_group_sdt, aP2_networkType, aP3_cuantity, aP4_isChange, out aP5_sdt_addressess, out aP6_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 string aP2_networkType ,
                                 long aP3_cuantity ,
                                 bool aP4_isChange ,
                                 out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ,
                                 out string aP6_error )
      {
         this.AV20StoredTransactions = aP0_StoredTransactions;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV15networkType = aP2_networkType;
         this.AV9cuantity = aP3_cuantity;
         this.AV14isChange = aP4_isChange;
         this.AV18sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         SubmitImpl();
         aP5_sdt_addressess=this.AV18sdt_addressess;
         aP6_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV19sequence = 0;
         AV8countNotFound = 0;
         if ( AV14isChange )
         {
            AV12generatedType = (long)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven));
         }
         else
         {
            AV12generatedType = (long)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven));
         }
         while ( AV8countNotFound < AV9cuantity )
         {
            GXt_SdtLegacyAddressResult1 = AV17result;
            new GeneXus.Programs.wallet.registered.deriveaddresslegacy(context ).execute(  AV13group_sdt,  AV15networkType,  AV19sequence,  AV14isChange, out  GXt_SdtLegacyAddressResult1) ;
            AV17result = GXt_SdtLegacyAddressResult1;
            if ( ! AV17result.gxTpr_Success )
            {
               AV10error = AV17result.gxTpr_Error;
               cleanup();
               if (true) return;
            }
            AV11found = false;
            AV22GXV1 = 1;
            while ( AV22GXV1 <= AV20StoredTransactions.gxTpr_Transaction.Count )
            {
               AV21TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV20StoredTransactions.gxTpr_Transaction.Item(AV22GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV21TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV17result.gxTpr_Address)) == 0 )
               {
                  AV11found = true;
                  if (true) break;
               }
               AV22GXV1 = (int)(AV22GXV1+1);
            }
            if ( ! AV11found )
            {
               AV16oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
               AV16oneAddress.gxTpr_Address = StringUtil.Trim( AV17result.gxTpr_Address);
               AV16oneAddress.gxTpr_Generatedtype = (short)(AV12generatedType);
               AV16oneAddress.gxTpr_Isused = false;
               AV16oneAddress.gxTpr_Creationsequence = AV19sequence;
               AV18sdt_addressess.Add(AV16oneAddress, 0);
               AV8countNotFound = (long)(AV8countNotFound+1);
            }
            AV19sequence = (long)(AV19sequence+1);
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
         AV18sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV10error = "";
         AV17result = new GeneXus.Programs.wallet.registered.SdtLegacyAddressResult(context);
         GXt_SdtLegacyAddressResult1 = new GeneXus.Programs.wallet.registered.SdtLegacyAddressResult(context);
         AV21TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV16oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private int AV22GXV1 ;
      private long AV9cuantity ;
      private long AV19sequence ;
      private long AV8countNotFound ;
      private long AV12generatedType ;
      private string AV15networkType ;
      private string AV10error ;
      private bool AV14isChange ;
      private bool AV11found ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV20StoredTransactions ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV18sdt_addressess ;
      private GeneXus.Programs.wallet.registered.SdtLegacyAddressResult AV17result ;
      private GeneXus.Programs.wallet.registered.SdtLegacyAddressResult GXt_SdtLegacyAddressResult1 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV21TransactionItem ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV16oneAddress ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ;
      private string aP6_error ;
   }

}
