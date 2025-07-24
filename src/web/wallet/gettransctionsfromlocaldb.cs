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

      public void execute( SdtGxGetAddressess aP0_addressess_to_look_for ,
                           out SdtGxTransactions aP1_transactionsFromService ,
                           out string aP2_error )
      {
         this.AV22addressess_to_look_for = aP0_addressess_to_look_for;
         this.AV17transactionsFromService = new SdtGxTransactions(context) ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_transactionsFromService=this.AV17transactionsFromService;
         aP2_error=this.AV12error;
      }

      public string executeUdp( SdtGxGetAddressess aP0_addressess_to_look_for ,
                                out SdtGxTransactions aP1_transactionsFromService )
      {
         execute(aP0_addressess_to_look_for, out aP1_transactionsFromService, out aP2_error);
         return AV12error ;
      }

      public void executeSubmit( SdtGxGetAddressess aP0_addressess_to_look_for ,
                                 out SdtGxTransactions aP1_transactionsFromService ,
                                 out string aP2_error )
      {
         this.AV22addressess_to_look_for = aP0_addressess_to_look_for;
         this.AV17transactionsFromService = new SdtGxTransactions(context) ;
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
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV22addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
         {
            AV14one_address = ((string)AV22addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV23GXV1));
            GXt_char2 = AV12error;
            new GeneXus.Programs.electrum.get_history(context ).execute(  AV14one_address,  AV19wallet.gxTpr_Networktype,  20, out  AV13message, out  GXt_char2) ;
            AV12error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               AV11electrumResponse.FromJSonString(AV13message, null);
               if ( StringUtil.StrCmp(AV11electrumResponse.gxTpr_Id, "blockchain.scripthash.get_history") == 0 )
               {
                  AV9electrumRespGetHistory.FromJSonString(AV13message, null);
                  AV24GXV2 = 1;
                  while ( AV24GXV2 <= AV9electrumRespGetHistory.gxTpr_Result.Count )
                  {
                     AV8elecrumOneHistory = ((GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem)AV9electrumRespGetHistory.gxTpr_Result.Item(AV24GXV2));
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
                     AV24GXV2 = (int)(AV24GXV2+1);
                  }
               }
            }
            else
            {
               if (true) break;
            }
            AV23GXV1 = (int)(AV23GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV25GXV3 = 1;
            while ( AV25GXV3 <= AV22addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
            {
               AV14one_address = ((string)AV22addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV25GXV3));
               new GeneXus.Programs.sudodb.gettransactionsfromaddress(context ).execute(  AV14one_address, ref  AV17transactionsFromService) ;
               AV25GXV3 = (int)(AV25GXV3+1);
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
         AV17transactionsFromService = new SdtGxTransactions(context);
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

      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int AV25GXV3 ;
      private string AV12error ;
      private string AV14one_address ;
      private string GXt_char2 ;
      private string AV13message ;
      private SdtGxGetAddressess AV22addressess_to_look_for ;
      private SdtGxTransactions AV17transactionsFromService ;
      private GeneXus.Programs.wallet.SdtWallet AV19wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtelectrumResponse AV11electrumResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory AV9electrumRespGetHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem AV8elecrumOneHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV10electrumRespGetTransactionId ;
      private SdtGxTransactions aP1_transactionsFromService ;
      private string aP2_error ;
   }

}
