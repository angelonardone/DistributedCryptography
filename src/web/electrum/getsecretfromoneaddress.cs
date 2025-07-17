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
namespace GeneXus.Programs.electrum {
   public class getsecretfromoneaddress : GXProcedure
   {
      public getsecretfromoneaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getsecretfromoneaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_one_address ,
                           out bool aP1_isActive ,
                           out string aP2_secret ,
                           out string aP3_error )
      {
         this.AV17one_address = aP0_one_address;
         this.AV15isActive = false ;
         this.AV18secret = "" ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isActive=this.AV15isActive;
         aP2_secret=this.AV18secret;
         aP3_error=this.AV14error;
      }

      public string executeUdp( string aP0_one_address ,
                                out bool aP1_isActive ,
                                out string aP2_secret )
      {
         execute(aP0_one_address, out aP1_isActive, out aP2_secret, out aP3_error);
         return AV14error ;
      }

      public void executeSubmit( string aP0_one_address ,
                                 out bool aP1_isActive ,
                                 out string aP2_secret ,
                                 out string aP3_error )
      {
         this.AV17one_address = aP0_one_address;
         this.AV15isActive = false ;
         this.AV18secret = "" ;
         this.AV14error = "" ;
         SubmitImpl();
         aP1_isActive=this.AV15isActive;
         aP2_secret=this.AV18secret;
         aP3_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV20wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV20wallet = GXt_SdtWallet1;
         GXt_char2 = AV14error;
         new GeneXus.Programs.electrum.get_history(context ).execute(  AV17one_address,  AV20wallet.gxTpr_Networktype,  20, out  AV16message, out  GXt_char2) ;
         AV14error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV13electrumResponse.FromJSonString(AV16message, null);
            if ( StringUtil.StrCmp(AV13electrumResponse.gxTpr_Id, "blockchain.scripthash.get_history") == 0 )
            {
               AV11electrumRespGetHistory.FromJSonString(AV16message, null);
               AV22GXV1 = 1;
               while ( AV22GXV1 <= AV11electrumRespGetHistory.gxTpr_Result.Count )
               {
                  AV10elecrumOneHistory = ((GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem)AV11electrumRespGetHistory.gxTpr_Result.Item(AV22GXV1));
                  GXt_char2 = AV14error;
                  new GeneXus.Programs.electrum.get_transaction(context ).execute(  AV10elecrumOneHistory.gxTpr_Tx_hash,  20, out  AV16message, out  GXt_char2) ;
                  AV14error = GXt_char2;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     AV13electrumResponse.FromJSonString(AV16message, null);
                     if ( StringUtil.StrCmp(AV13electrumResponse.gxTpr_Id, "blockchain.transaction.get") == 0 )
                     {
                        AV12electrumRespGetTransactionId.FromJSonString(AV16message, null);
                        if ( AV10elecrumOneHistory.gxTpr_Height == 0 )
                        {
                           AV12electrumRespGetTransactionId.gxTpr_Result.gxTpr_Confirmations = (decimal)(0);
                        }
                     }
                  }
                  AV22GXV1 = (int)(AV22GXV1+1);
               }
               if ( AV11electrumRespGetHistory.gxTpr_Result.Count > 0 )
               {
                  AV21countVouts = (short)(AV12electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Count);
                  AV23I = 1;
                  while ( AV23I <= AV21countVouts )
                  {
                     if ( StringUtil.StrCmp(((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_voutItem)AV12electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Item(AV23I)).gxTpr_Scriptpubkey.gxTpr_Address, StringUtil.Trim( AV17one_address)) == 0 )
                     {
                        AV15isActive = true;
                        cleanup();
                        if (true) return;
                     }
                     AV23I = (short)(AV23I+1);
                  }
                  AV15isActive = false;
                  if ( ((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem)AV12electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Item(1)).gxTpr_Txinwitness.Count > 1 )
                  {
                     AV18secret = StringUtil.Trim( ((string)((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem)AV12electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Item(1)).gxTpr_Txinwitness.Item(2)));
                  }
               }
               else
               {
                  AV15isActive = false;
               }
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
         AV18secret = "";
         AV14error = "";
         AV20wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV16message = "";
         AV13electrumResponse = new GeneXus.Programs.electrum.SdtelectrumResponse(context);
         AV11electrumRespGetHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory(context);
         AV10elecrumOneHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem(context);
         GXt_char2 = "";
         AV12electrumRespGetTransactionId = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId(context);
         /* GeneXus formulas. */
      }

      private short AV21countVouts ;
      private short AV23I ;
      private int AV22GXV1 ;
      private string AV17one_address ;
      private string AV18secret ;
      private string AV14error ;
      private string GXt_char2 ;
      private bool AV15isActive ;
      private string AV16message ;
      private GeneXus.Programs.wallet.SdtWallet AV20wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtelectrumResponse AV13electrumResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory AV11electrumRespGetHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem AV10elecrumOneHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV12electrumRespGetTransactionId ;
      private bool aP1_isActive ;
      private string aP2_secret ;
      private string aP3_error ;
   }

}
