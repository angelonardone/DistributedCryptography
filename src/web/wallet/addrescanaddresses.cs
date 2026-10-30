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
   public class addrescanaddresses : GXProcedure
   {
      public addrescanaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public addrescanaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                           ref SdtGxGetAddressess aP1_addressess_to_look_for )
      {
         this.AV11StoredTransactions = aP0_StoredTransactions;
         this.AV8addressess_to_look_for = aP1_addressess_to_look_for;
         initialize();
         ExecuteImpl();
         aP1_addressess_to_look_for=this.AV8addressess_to_look_for;
      }

      public SdtGxGetAddressess executeUdp( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions )
      {
         execute(aP0_StoredTransactions, ref aP1_addressess_to_look_for);
         return AV8addressess_to_look_for ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.SdtStoredTransactions aP0_StoredTransactions ,
                                 ref SdtGxGetAddressess aP1_addressess_to_look_for )
      {
         this.AV11StoredTransactions = aP0_StoredTransactions;
         this.AV8addressess_to_look_for = aP1_addressess_to_look_for;
         SubmitImpl();
         aP1_addressess_to_look_for=this.AV8addressess_to_look_for;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( StringUtil.StrCmp(AV13websession.Get("RESCAN_BALANCE"), "1") == 0 )
         {
            AV13websession.Set("RESCAN_BALANCE", "");
            AV14GXV1 = 1;
            while ( AV14GXV1 <= AV11StoredTransactions.gxTpr_Transaction.Count )
            {
               AV12TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV11StoredTransactions.gxTpr_Transaction.Item(AV14GXV1));
               AV9found = false;
               AV15GXV2 = 1;
               while ( AV15GXV2 <= AV8addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
               {
                  AV10one_to_look_for = ((string)AV8addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV15GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV10one_to_look_for), StringUtil.Trim( AV12TransactionItem.gxTpr_Scriptpubkey_address)) == 0 )
                  {
                     AV9found = true;
                     if (true) break;
                  }
                  AV15GXV2 = (int)(AV15GXV2+1);
               }
               if ( ! AV9found && ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12TransactionItem.gxTpr_Scriptpubkey_address))) )
               {
                  AV8addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV12TransactionItem.gxTpr_Scriptpubkey_address), 0);
               }
               AV14GXV1 = (int)(AV14GXV1+1);
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
         AV13websession = context.GetSession();
         AV12TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV10one_to_look_for = "";
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private int AV15GXV2 ;
      private string AV10one_to_look_for ;
      private bool AV9found ;
      private IGxSession AV13websession ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV11StoredTransactions ;
      private SdtGxGetAddressess AV8addressess_to_look_for ;
      private SdtGxGetAddressess aP1_addressess_to_look_for ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV12TransactionItem ;
   }

}
