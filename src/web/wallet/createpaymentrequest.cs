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
   public class createpaymentrequest : GXProcedure
   {
      public createpaymentrequest( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createpaymentrequest( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           string aP1_label ,
                           decimal aP2_amount ,
                           int aP3_expiresInHours ,
                           out GeneXus.Programs.wallet.SdtPaymentRequest aP4_request ,
                           out string aP5_error )
      {
         this.AV26groupId = aP0_groupId;
         this.AV14label = aP1_label;
         this.AV9amount = aP2_amount;
         this.AV11expiresInHours = aP3_expiresInHours;
         this.AV18request = new GeneXus.Programs.wallet.SdtPaymentRequest(context) ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP4_request=this.AV18request;
         aP5_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                string aP1_label ,
                                decimal aP2_amount ,
                                int aP3_expiresInHours ,
                                out GeneXus.Programs.wallet.SdtPaymentRequest aP4_request )
      {
         execute(aP0_groupId, aP1_label, aP2_amount, aP3_expiresInHours, out aP4_request, out aP5_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 string aP1_label ,
                                 decimal aP2_amount ,
                                 int aP3_expiresInHours ,
                                 out GeneXus.Programs.wallet.SdtPaymentRequest aP4_request ,
                                 out string aP5_error )
      {
         this.AV26groupId = aP0_groupId;
         this.AV14label = aP1_label;
         this.AV9amount = aP2_amount;
         this.AV11expiresInHours = aP3_expiresInHours;
         this.AV18request = new GeneXus.Programs.wallet.SdtPaymentRequest(context) ;
         this.AV10error = "" ;
         SubmitImpl();
         aP4_request=this.AV18request;
         aP5_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10error = "";
         AV18request = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV26groupId, out  AV29transactionFileName, out  AV28requestsFileName, out  AV27receivingType, out  AV25changeType) ;
         GXt_SdtWalletInfo1 = AV24walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
         AV24walletInfo = GXt_SdtWalletInfo1;
         if ( ! new GeneXus.Programs.wallet.iswalletopen(context).executeUdp( ) )
         {
            AV10error = "Open a wallet first";
         }
         else if ( (Guid.Empty==AV26groupId) && ( ( StringUtil.StrCmp(AV24walletInfo.gxTpr_Wallettype, "BrainWallet") == 0 ) || ( StringUtil.StrCmp(AV24walletInfo.gxTpr_Wallettype, "ImportedWIF") == 0 ) ) )
         {
            AV10error = "This wallet has only one address: payment requests need a wallet created from a seed (BIP44/49/84/86)";
         }
         else if ( ( AV9amount < Convert.ToDecimal( 0 )) )
         {
            AV10error = "The amount can not be negative";
         }
         else if ( AV11expiresInHours < 0 )
         {
            AV10error = "The expiry can not be negative";
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV26groupId, out  AV19requests, out  AV10error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            cleanup();
            if (true) return;
         }
         AV20reserved = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV40GXV1 = 1;
         while ( AV40GXV1 <= AV19requests.Count )
         {
            AV16oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV19requests.Item(AV40GXV1));
            if ( StringUtil.StrCmp(AV16oneRequest.gxTpr_Status, "Pending") == 0 )
            {
               AV20reserved.Add(StringUtil.Trim( AV16oneRequest.gxTpr_Address), 0);
            }
            AV40GXV1 = (int)(AV40GXV1+1);
         }
         if ( AV20reserved.Count >= 19 )
         {
            AV10error = "There are already 19 pending payment requests. Cancel one that will not be paid: a wallet restored from the seed only looks 20 unused addresses ahead";
            cleanup();
            if (true) return;
         }
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 = AV21sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2) ;
         AV21sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2;
         AV31chainInSession = false;
         AV41GXV2 = 1;
         while ( AV41GXV2 <= AV21sdt_Addresses.Count )
         {
            AV15one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV21sdt_Addresses.Item(AV41GXV2));
            if ( AV15one_sdt_address.gxTpr_Generatedtype == AV27receivingType )
            {
               AV31chainInSession = true;
               if (true) break;
            }
            AV41GXV2 = (int)(AV41GXV2+1);
         }
         if ( ! AV31chainInSession )
         {
            if ( (Guid.Empty==AV26groupId) )
            {
               new GeneXus.Programs.wallet.createunusedaddresses(context ).execute( out  AV8addressess_to_look_for, out  AV22SDTAddressHistory, out  AV23totalBalance) ;
            }
            else
            {
               GXt_int3 = AV32groupType;
               new GeneXus.Programs.wallet.registered.setgroupeditwallet(context ).execute(  AV26groupId, out  GXt_int3) ;
               AV32groupType = GXt_int3;
               new GeneXus.Programs.wallet.createunusedaddressesgroup(context ).execute( out  AV8addressess_to_look_for, out  AV22SDTAddressHistory, out  AV23totalBalance, out  AV30auxError) ;
            }
            GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 = AV21sdt_Addresses;
            new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2) ;
            AV21sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2;
         }
         AV18request.gxTpr_Requestid = Guid.NewGuid( );
         AV18request.gxTpr_Label = StringUtil.Trim( AV14label);
         AV18request.gxTpr_Amount = AV9amount;
         AV18request.gxTpr_Createdat = DateTimeUtil.Now( context);
         if ( AV11expiresInHours > 0 )
         {
            AV18request.gxTpr_Expiresat = DateTimeUtil.TAdd( AV18request.gxTpr_Createdat, 3600*(AV11expiresInHours));
         }
         AV18request.gxTpr_Cancelled = false;
         AV18request.gxTpr_Receivedamount = (decimal)(0);
         if ( ! (Guid.Empty==AV26groupId) )
         {
            GXt_SdtExternalUser4 = AV33externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser4) ;
            AV33externalUser = GXt_SdtExternalUser4;
            AV18request.gxTpr_Createdby = StringUtil.Trim( AV33externalUser.gxTpr_Userinfo.gxTpr_Username);
         }
         AV12found = false;
         GXt_SdtWallet5 = AV39wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet5) ;
         AV39wallet = GXt_SdtWallet5;
         AV42GXV3 = 1;
         while ( AV42GXV3 <= AV21sdt_Addresses.Count )
         {
            AV15one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV21sdt_Addresses.Item(AV42GXV3));
            if ( ( AV15one_sdt_address.gxTpr_Generatedtype == AV27receivingType ) && ! AV15one_sdt_address.gxTpr_Isused )
            {
               AV13isReserved = false;
               AV43GXV4 = 1;
               while ( AV43GXV4 <= AV20reserved.Count )
               {
                  AV17oneReserved = AV20reserved.GetString(AV43GXV4);
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV17oneReserved), StringUtil.Trim( AV15one_sdt_address.gxTpr_Address)) == 0 )
                  {
                     AV13isReserved = true;
                     if (true) break;
                  }
                  AV43GXV4 = (int)(AV43GXV4+1);
               }
               AV38hasHistory = false;
               if ( ! AV13isReserved )
               {
                  GXt_char6 = AV34electrumError;
                  new GeneXus.Programs.electrum.get_history(context ).execute(  AV15one_sdt_address.gxTpr_Address,  AV39wallet.gxTpr_Networktype,  20, out  AV35electrumMessage, out  GXt_char6) ;
                  AV34electrumError = GXt_char6;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV34electrumError)) )
                  {
                     AV36electrumResponse.FromJSonString(AV35electrumMessage, null);
                     if ( StringUtil.StrCmp(AV36electrumResponse.gxTpr_Id, "blockchain.scripthash.get_history") == 0 )
                     {
                        AV37electrumRespGetHistory.FromJSonString(AV35electrumMessage, null);
                        if ( AV37electrumRespGetHistory.gxTpr_Result.Count > 0 )
                        {
                           AV38hasHistory = true;
                           AV15one_sdt_address.gxTpr_Isused = true;
                        }
                     }
                  }
               }
               if ( ! AV13isReserved && ! AV38hasHistory )
               {
                  AV18request.gxTpr_Address = StringUtil.Trim( AV15one_sdt_address.gxTpr_Address);
                  AV18request.gxTpr_Addressindex = AV15one_sdt_address.gxTpr_Creationsequence;
                  if ( (Guid.Empty==AV26groupId) )
                  {
                     AV12found = true;
                  }
                  else
                  {
                     new GeneXus.Programs.wallet.registered.createsharedrecord(context ).execute(  AV26groupId,  "reservation",  StringUtil.Trim( AV18request.gxTpr_Address),  AV18request.ToJSonString(false, true), out  AV10error) ;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                     {
                        AV12found = true;
                     }
                     else if ( StringUtil.StrCmp(AV10error, "duplicate") == 0 )
                     {
                        AV10error = "";
                     }
                     else
                     {
                        cleanup();
                        if (true) return;
                     }
                  }
                  if ( AV12found )
                  {
                     AV15one_sdt_address.gxTpr_Isused = true;
                     if (true) break;
                  }
               }
            }
            AV42GXV3 = (int)(AV42GXV3+1);
         }
         if ( ! AV12found )
         {
            AV10error = "There is no unused receiving address to reserve: press Rescan Balance in the Balance tab and try again";
            cleanup();
            if (true) return;
         }
         AV18request.gxTpr_Status = "Pending";
         GXt_char6 = "";
         new GeneXus.Programs.wallet.buildpaymenturi(context ).execute(  AV18request.gxTpr_Address,  AV18request.gxTpr_Amount,  AV18request.gxTpr_Label, out  GXt_char6) ;
         AV18request.gxTpr_Uri = GXt_char6;
         if ( (Guid.Empty==AV26groupId) )
         {
            AV19requests.Add(AV18request, 0);
            GXt_char6 = AV10error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV28requestsFileName,  AV19requests.ToJSonString(false), out  GXt_char6) ;
            AV10error = GXt_char6;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV21sdt_Addresses) ;
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
         AV18request = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV10error = "";
         AV29transactionFileName = "";
         AV28requestsFileName = "";
         AV24walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV19requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         AV20reserved = new GxSimpleCollection<string>();
         AV16oneRequest = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV21sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV15one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV8addressess_to_look_for = new SdtGxGetAddressess(context);
         AV22SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV30auxError = "";
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV33externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser4 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV39wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet5 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV17oneReserved = "";
         AV34electrumError = "";
         AV35electrumMessage = "";
         AV36electrumResponse = new GeneXus.Programs.electrum.SdtelectrumResponse(context);
         AV37electrumRespGetHistory = new GeneXus.Programs.electrum.SdtelectrumRespGetHistory(context);
         GXt_char6 = "";
         /* GeneXus formulas. */
      }

      private short AV27receivingType ;
      private short AV25changeType ;
      private short AV32groupType ;
      private short GXt_int3 ;
      private int AV11expiresInHours ;
      private int AV40GXV1 ;
      private int AV41GXV2 ;
      private int AV42GXV3 ;
      private int AV43GXV4 ;
      private decimal AV9amount ;
      private decimal AV23totalBalance ;
      private string AV10error ;
      private string AV29transactionFileName ;
      private string AV28requestsFileName ;
      private string AV30auxError ;
      private string AV17oneReserved ;
      private string AV34electrumError ;
      private string GXt_char6 ;
      private bool AV31chainInSession ;
      private bool AV12found ;
      private bool AV13isReserved ;
      private bool AV38hasHistory ;
      private string AV35electrumMessage ;
      private string AV14label ;
      private Guid AV26groupId ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV18request ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV24walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV19requests ;
      private GxSimpleCollection<string> AV20reserved ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV16oneRequest ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV21sdt_Addresses ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_sdt_address ;
      private SdtGxGetAddressess AV8addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV22SDTAddressHistory ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem2 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV33externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser4 ;
      private GeneXus.Programs.wallet.SdtWallet AV39wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet5 ;
      private GeneXus.Programs.electrum.SdtelectrumResponse AV36electrumResponse ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetHistory AV37electrumRespGetHistory ;
      private GeneXus.Programs.wallet.SdtPaymentRequest aP4_request ;
      private string aP5_error ;
   }

}
