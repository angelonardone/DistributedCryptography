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
         AV33GXV1 = 1;
         while ( AV33GXV1 <= AV19requests.Count )
         {
            AV16oneRequest = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV19requests.Item(AV33GXV1));
            if ( StringUtil.StrCmp(AV16oneRequest.gxTpr_Status, "Pending") == 0 )
            {
               AV20reserved.Add(StringUtil.Trim( AV16oneRequest.gxTpr_Address), 0);
            }
            AV33GXV1 = (int)(AV33GXV1+1);
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
         AV34GXV2 = 1;
         while ( AV34GXV2 <= AV21sdt_Addresses.Count )
         {
            AV15one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV21sdt_Addresses.Item(AV34GXV2));
            if ( AV15one_sdt_address.gxTpr_Generatedtype == AV27receivingType )
            {
               AV31chainInSession = true;
               if (true) break;
            }
            AV34GXV2 = (int)(AV34GXV2+1);
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
         AV12found = false;
         AV35GXV3 = 1;
         while ( AV35GXV3 <= AV21sdt_Addresses.Count )
         {
            AV15one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV21sdt_Addresses.Item(AV35GXV3));
            if ( ( AV15one_sdt_address.gxTpr_Generatedtype == AV27receivingType ) && ! AV15one_sdt_address.gxTpr_Isused )
            {
               AV13isReserved = false;
               AV36GXV4 = 1;
               while ( AV36GXV4 <= AV20reserved.Count )
               {
                  AV17oneReserved = AV20reserved.GetString(AV36GXV4);
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV17oneReserved), StringUtil.Trim( AV15one_sdt_address.gxTpr_Address)) == 0 )
                  {
                     AV13isReserved = true;
                     if (true) break;
                  }
                  AV36GXV4 = (int)(AV36GXV4+1);
               }
               if ( ! AV13isReserved )
               {
                  AV12found = true;
                  AV18request.gxTpr_Address = StringUtil.Trim( AV15one_sdt_address.gxTpr_Address);
                  AV18request.gxTpr_Addressindex = AV15one_sdt_address.gxTpr_Creationsequence;
                  AV15one_sdt_address.gxTpr_Isused = true;
                  if (true) break;
               }
            }
            AV35GXV3 = (int)(AV35GXV3+1);
         }
         if ( ! AV12found )
         {
            AV10error = "There is no unused receiving address to reserve: press Rescan Balance in the Balance tab and try again";
            cleanup();
            if (true) return;
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
         AV18request.gxTpr_Status = "Pending";
         AV18request.gxTpr_Receivedamount = (decimal)(0);
         GXt_char4 = "";
         new GeneXus.Programs.wallet.buildpaymenturi(context ).execute(  AV18request.gxTpr_Address,  AV18request.gxTpr_Amount,  AV18request.gxTpr_Label, out  GXt_char4) ;
         AV18request.gxTpr_Uri = GXt_char4;
         AV19requests.Add(AV18request, 0);
         GXt_char4 = AV10error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV28requestsFileName,  AV19requests.ToJSonString(false), out  GXt_char4) ;
         AV10error = GXt_char4;
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
         AV17oneReserved = "";
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV27receivingType ;
      private short AV25changeType ;
      private short AV32groupType ;
      private short GXt_int3 ;
      private int AV11expiresInHours ;
      private int AV33GXV1 ;
      private int AV34GXV2 ;
      private int AV35GXV3 ;
      private int AV36GXV4 ;
      private decimal AV9amount ;
      private decimal AV23totalBalance ;
      private string AV10error ;
      private string AV29transactionFileName ;
      private string AV28requestsFileName ;
      private string AV30auxError ;
      private string AV17oneReserved ;
      private string GXt_char4 ;
      private bool AV31chainInSession ;
      private bool AV12found ;
      private bool AV13isReserved ;
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
      private GeneXus.Programs.wallet.SdtPaymentRequest aP4_request ;
      private string aP5_error ;
   }

}
