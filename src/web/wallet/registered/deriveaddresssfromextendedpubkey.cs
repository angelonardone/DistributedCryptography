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
   public class deriveaddresssfromextendedpubkey : GXProcedure
   {
      public deriveaddresssfromextendedpubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddresssfromextendedpubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           string aP1_deserializedExtPubKey ,
                           short aP2_cuantity ,
                           short aP3_generatedType ,
                           out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP4_sdt_addressess ,
                           out string aP5_error )
      {
         this.AV18StoredTransactions = aP0_StoredTransactions;
         this.AV20deserializedExtPubKey = aP1_deserializedExtPubKey;
         this.AV9cuantity = aP2_cuantity;
         this.AV12generatedType = aP3_generatedType;
         this.AV16sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP4_sdt_addressess=this.AV16sdt_addressess;
         aP5_error=this.AV10error;
      }

      public string executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                string aP1_deserializedExtPubKey ,
                                short aP2_cuantity ,
                                short aP3_generatedType ,
                                out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP4_sdt_addressess )
      {
         execute(aP0_StoredTransactions, aP1_deserializedExtPubKey, aP2_cuantity, aP3_generatedType, out aP4_sdt_addressess, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 string aP1_deserializedExtPubKey ,
                                 short aP2_cuantity ,
                                 short aP3_generatedType ,
                                 out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP4_sdt_addressess ,
                                 out string aP5_error )
      {
         this.AV18StoredTransactions = aP0_StoredTransactions;
         this.AV20deserializedExtPubKey = aP1_deserializedExtPubKey;
         this.AV9cuantity = aP2_cuantity;
         this.AV12generatedType = aP3_generatedType;
         this.AV16sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography") ;
         this.AV10error = "" ;
         SubmitImpl();
         aP4_sdt_addressess=this.AV16sdt_addressess;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV17sequence = 0;
         AV8countNotFound = 0;
         while ( AV8countNotFound < AV9cuantity )
         {
            GXt_char1 = AV10error;
            new GeneXus.Programs.nbitcoin.deriveoneaddressfromextpubkey(context ).execute(  AV20deserializedExtPubKey,  AV12generatedType,  AV17sequence, out  AV15oneAddress, out  GXt_char1) ;
            AV10error = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               if (true) break;
            }
            AV11found = false;
            AV21GXV1 = 1;
            while ( AV21GXV1 <= AV18StoredTransactions.gxTpr_Transaction.Count )
            {
               AV19TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV18StoredTransactions.gxTpr_Transaction.Item(AV21GXV1));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV19TransactionItem.gxTpr_Scriptpubkey_address), StringUtil.Trim( AV15oneAddress.gxTpr_Address)) == 0 )
               {
                  AV11found = true;
                  if (true) break;
               }
               AV21GXV1 = (int)(AV21GXV1+1);
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
         GXt_char1 = "";
         AV15oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV19TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         /* GeneXus formulas. */
      }

      private short AV9cuantity ;
      private short AV12generatedType ;
      private int AV21GXV1 ;
      private long AV17sequence ;
      private long AV8countNotFound ;
      private string AV20deserializedExtPubKey ;
      private string AV10error ;
      private string GXt_char1 ;
      private bool AV11found ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV18StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV16sdt_addressess ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15oneAddress ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV19TransactionItem ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> aP4_sdt_addressess ;
      private string aP5_error ;
   }

}
