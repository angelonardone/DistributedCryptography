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
namespace GeneXus.Programs.wallet.registered {
   public class deriveaddresssfordelegationmusig2 : GXProcedure
   {
      public deriveaddresssfordelegationmusig2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddresssfordelegationmusig2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           string aP2_networkType ,
                           short aP3_cuantity ,
                           short aP4_generatedType ,
                           out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ,
                           out string aP6_error )
      {
         this.AV32StoredTransactions = aP0_StoredTransactions;
         this.AV22group_sdt = aP1_group_sdt;
         this.AV41networkType = aP2_networkType;
         this.AV43cuantity = aP3_cuantity;
         this.AV46generatedType = aP4_generatedType;
         this.AV45sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV18error = "" ;
         initialize();
         ExecuteImpl();
         aP5_sdt_addressess=this.AV45sdt_addressess;
         aP6_error=this.AV18error;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                string aP2_networkType ,
                                short aP3_cuantity ,
                                short aP4_generatedType ,
                                out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess )
      {
         execute(aP0_StoredTransactions, aP1_group_sdt, aP2_networkType, aP3_cuantity, aP4_generatedType, out aP5_sdt_addressess, out aP6_error);
         return AV18error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 string aP2_networkType ,
                                 short aP3_cuantity ,
                                 short aP4_generatedType ,
                                 out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ,
                                 out string aP6_error )
      {
         this.AV32StoredTransactions = aP0_StoredTransactions;
         this.AV22group_sdt = aP1_group_sdt;
         this.AV41networkType = aP2_networkType;
         this.AV43cuantity = aP3_cuantity;
         this.AV46generatedType = aP4_generatedType;
         this.AV45sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV18error = "" ;
         SubmitImpl();
         aP5_sdt_addressess=this.AV45sdt_addressess;
         aP6_error=this.AV18error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV47sequence = 0;
         AV48countNotFound = 0;
         while ( AV48countNotFound < AV43cuantity )
         {
            GXt_char1 = AV18error;
            new GeneXus.Programs.wallet.registered.deriveoneaddressfordelegationmusig2(context ).execute(  AV22group_sdt,  AV41networkType,  AV46generatedType,  AV47sequence, out  AV42oneAddress, out  GXt_char1) ;
            AV18error = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18error)) )
            {
               if (true) break;
            }
            AV44found = false;
            AV49GXV1 = 1;
            while ( AV49GXV1 <= AV32StoredTransactions.gxTpr_Transaction.Count )
            {
               AV36TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV32StoredTransactions.gxTpr_Transaction.Item(AV49GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV36TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV42oneAddress.gxTpr_Address)) == 0 )
               {
                  AV44found = true;
                  if (true) break;
               }
               AV49GXV1 = (int)(AV49GXV1+1);
            }
            if ( ! AV44found )
            {
               AV45sdt_addressess.Add(AV42oneAddress, 0);
               AV48countNotFound = (long)(AV48countNotFound+1);
            }
            AV47sequence = (long)(AV47sequence+1);
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
         AV45sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV18error = "";
         GXt_char1 = "";
         AV42oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV36TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         /* GeneXus formulas. */
      }

      private short AV43cuantity ;
      private short AV46generatedType ;
      private int AV49GXV1 ;
      private long AV47sequence ;
      private long AV48countNotFound ;
      private string AV41networkType ;
      private string AV18error ;
      private string GXt_char1 ;
      private bool AV44found ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV32StoredTransactions ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV22group_sdt ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV45sdt_addressess ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV42oneAddress ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV36TransactionItem ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP5_sdt_addressess ;
      private string aP6_error ;
   }

}
