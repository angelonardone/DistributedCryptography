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
namespace GeneXus.Programs.wallet {
   public class gettransctionsfromlocaldb : GXProcedure
   {
      public gettransctionsfromlocaldb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public gettransctionsfromlocaldb( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( SdtGetTransactions__postInput aP0_transactions__postInput ,
                           out SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService ,
                           out string aP2_error )
      {
         this.AV16transactions__postInput = aP0_transactions__postInput;
         this.AV17transactionsFromService = new SdtGxExplorer_services_TxoutFromAddresses(context) ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_transactionsFromService=this.AV17transactionsFromService;
         aP2_error=this.AV12error;
      }

      public string executeUdp( SdtGetTransactions__postInput aP0_transactions__postInput ,
                                out SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService )
      {
         execute(aP0_transactions__postInput, out aP1_transactionsFromService, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( SdtGetTransactions__postInput aP0_transactions__postInput ,
                                 out SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService ,
                                 out string aP2_error )
      {
         this.AV16transactions__postInput = aP0_transactions__postInput;
         this.AV17transactionsFromService = new SdtGxExplorer_services_TxoutFromAddresses(context) ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_transactionsFromService=this.AV17transactionsFromService;
         aP2_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV19wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV19wallet = GXt_SdtWallet1;
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV16transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Count )
         {
            AV14one_address = ((string)AV16transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV20GXV1));
            GXt_char2 = AV12error;
            new GeneXus.Programs.electrum.get_history(context ).execute(  AV14one_address,  AV19wallet.gxTpr_Networktype,  20, out  AV13message, out  GXt_char2) ;
            AV12error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               AV11electrumResponse.FromJSonString(AV13message, null);
               if ( StringUtil.StrCmp(AV11electrumResponse.gxTpr_Id, "blockchain.scripthash.get_history") == 0 )
               {
                  AV9electrumRespGetHistory.FromJSonString(AV13message, null);
                  AV21GXV2 = 1;
                  while ( AV21GXV2 <= AV9electrumRespGetHistory.gxTpr_Result.Count )
                  {
                     AV8elecrumOneHistory = ((GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem)AV9electrumRespGetHistory.gxTpr_Result.Item(AV21GXV2));
                     GXt_char2 = AV12error;
                     new GeneXus.Programs.electrum.get_transaction(context ).execute(  AV8elecrumOneHistory.gxTpr_Tx_hash,  20, out  AV13message, out  GXt_char2) ;
                     AV12error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        AV11electrumResponse.FromJSonString(AV13message, null);
                        if ( StringUtil.StrCmp(AV11electrumResponse.gxTpr_Id, "blockchain.transaction.get") == 0 )
                        {
                           AV10electrumRespGetTransactionId.FromJSonString(AV13message, null);
                           if ( AV8elecrumOneHistory.gxTpr_Height == 0 )
                           {
                              AV10electrumRespGetTransactionId.gxTpr_Result.gxTpr_Confirmations = (decimal)(0);
                           }
                           GXt_char2 = AV12error;
                           new GeneXus.Programs.sudodb.savetolocaldb(context ).execute(  AV10electrumRespGetTransactionId, out  GXt_char2) ;
                           AV12error = GXt_char2;
                        }
                     }
                     AV21GXV2 = (int)(AV21GXV2+1);
                  }
               }
            }
            else
            {
               if (true) break;
            }
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV22GXV3 = 1;
            while ( AV22GXV3 <= AV16transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Count )
            {
               AV14one_address = ((string)AV16transactions__postInput.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV22GXV3));
               new GeneXus.Programs.sudodb.gettransactionsfromaddress(context ).execute(  AV14one_address, ref  AV17transactionsFromService) ;
               AV22GXV3 = (int)(AV22GXV3+1);
            }
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
         AV17transactionsFromService = new SdtGxExplorer_services_TxoutFromAddresses(context);
         AV12error = "";
         AV19wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV14one_address = "";
         AV13message = "";
         AV11electrumResponse = new GeneXus.Programs.electrum.SdtelectrumResponse(context);
         AV9electrumRespGetHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory(context);
         AV8elecrumOneHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem(context);
         AV10electrumRespGetTransactionId = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private int AV22GXV3 ;
      private string AV12error ;
      private string AV14one_address ;
      private string GXt_char2 ;
      private string AV13message ;
      private SdtGetTransactions__postInput AV16transactions__postInput ;
      private SdtGxExplorer_services_TxoutFromAddresses AV17transactionsFromService ;
      private GeneXus.Programs.wallet.SdtWallet AV19wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtelectrumResponse AV11electrumResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory AV9electrumRespGetHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem AV8elecrumOneHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV10electrumRespGetTransactionId ;
      private SdtGxExplorer_services_TxoutFromAddresses aP1_transactionsFromService ;
      private string aP2_error ;
   }

}
