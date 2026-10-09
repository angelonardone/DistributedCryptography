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
         this.AV14one_address = aP0_one_address;
         this.AV20isActive = false ;
         this.AV19secret = "" ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isActive=this.AV20isActive;
         aP2_secret=this.AV19secret;
         aP3_error=this.AV12error;
      }

      public string executeUdp( string aP0_one_address ,
                                out bool aP1_isActive ,
                                out string aP2_secret )
      {
         execute(aP0_one_address, out aP1_isActive, out aP2_secret, out aP3_error);
         return AV12error ;
      }

      public void executeSubmit( string aP0_one_address ,
                                 out bool aP1_isActive ,
                                 out string aP2_secret ,
                                 out string aP3_error )
      {
         this.AV14one_address = aP0_one_address;
         this.AV20isActive = false ;
         this.AV19secret = "" ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_isActive=this.AV20isActive;
         aP2_secret=this.AV19secret;
         aP3_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV17wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV17wallet = GXt_SdtWallet1;
         GXt_char2 = AV12error;
         new GeneXus.Programs.electrum.get_history(context ).execute(  AV14one_address,  AV17wallet.gxTpr_Networktype,  20, out  AV13message, out  GXt_char2) ;
         AV12error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            AV11electrumResponse.FromJSonString(AV13message, null);
            if ( StringUtil.StrCmp(AV11electrumResponse.gxTpr_Id, "blockchain.scripthash.get_history") == 0 )
            {
               AV9electrumRespGetHistory.FromJSonString(AV13message, null);
               AV22GXV1 = 1;
               while ( AV22GXV1 <= AV9electrumRespGetHistory.gxTpr_Result.Count )
               {
                  AV8elecrumOneHistory = ((GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem)AV9electrumRespGetHistory.gxTpr_Result.Item(AV22GXV1));
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
                     }
                  }
                  AV22GXV1 = (int)(AV22GXV1+1);
               }
               if ( AV9electrumRespGetHistory.gxTpr_Result.Count > 0 )
               {
                  AV21countVouts = (short)(AV10electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Count);
                  AV23I = 1;
                  while ( AV23I <= AV21countVouts )
                  {
                     if ( StringUtil.StrCmp(((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_voutItem)AV10electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vout.Item(AV23I)).gxTpr_Scriptpubkey.gxTpr_Address, StringUtil.Trim( AV14one_address)) == 0 )
                     {
                        AV20isActive = true;
                        cleanup();
                        if (true) return;
                     }
                     AV23I = (short)(AV23I+1);
                  }
                  AV20isActive = false;
                  if ( ((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem)AV10electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Item(1)).gxTpr_Txinwitness.Count > 1 )
                  {
                     AV19secret = StringUtil.Trim( ((string)((GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId_result_vinItem)AV10electrumRespGetTransactionId.gxTpr_Result.gxTpr_Vin.Item(1)).gxTpr_Txinwitness.Item(2)));
                  }
               }
               else
               {
                  AV20isActive = false;
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
         AV19secret = "";
         AV12error = "";
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13message = "";
         AV11electrumResponse = new GeneXus.Programs.electrum.SdtelectrumResponse(context);
         AV9electrumRespGetHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory(context);
         AV8elecrumOneHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem(context);
         GXt_char2 = "";
         AV10electrumRespGetTransactionId = new GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId(context);
         /* GeneXus formulas. */
      }

      private short AV21countVouts ;
      private short AV23I ;
      private int AV22GXV1 ;
      private string AV14one_address ;
      private string AV19secret ;
      private string AV12error ;
      private string GXt_char2 ;
      private bool AV20isActive ;
      private string AV13message ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.electrum.SdtelectrumResponse AV11electrumResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory AV9electrumRespGetHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory_resultItem AV8elecrumOneHistory ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV10electrumRespGetTransactionId ;
      private bool aP1_isActive ;
      private string aP2_secret ;
      private string aP3_error ;
   }

}
