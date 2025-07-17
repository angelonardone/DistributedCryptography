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
   public class createunusedaddressesgroup : GXProcedure
   {
      public createunusedaddressesgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createunusedaddressesgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                           out decimal aP2_totalBalance ,
                           out string aP3_error )
      {
         this.AV60addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP0_addressess_to_look_for=this.AV60addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
         aP3_error=this.AV11error;
      }

      public string executeUdp( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                                out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                out decimal aP2_totalBalance )
      {
         execute(out aP0_addressess_to_look_for, out aP1_SDTAddressHistory, out aP2_totalBalance, out aP3_error);
         return AV11error ;
      }

      public void executeSubmit( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                                 out decimal aP2_totalBalance ,
                                 out string aP3_error )
      {
         this.AV60addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV26totalBalance = 0 ;
         this.AV11error = "" ;
         SubmitImpl();
         aP0_addressess_to_look_for=this.AV60addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV21SDTAddressHistory;
         aP2_totalBalance=this.AV26totalBalance;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExtKeyInfo1 = AV12extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV12extKeyInfoRoot = GXt_SdtExtKeyInfo1;
         GXt_SdtWallet2 = AV31wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV31wallet = GXt_SdtWallet2;
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV60addressess_to_look_for = new SdtGxGetAddressess(context);
         AV32group_sdt.FromJSonString(AV33websession.Get("Group_EDIT_WALLET"), null);
         AV54transactionFileName = StringUtil.Trim( AV32group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         AV24StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV54transactionFileName, out  AV11error), null);
         if ( StringUtil.StrCmp(AV31wallet.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV10deserializedExtPubKey = AV12extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
         }
         else
         {
            AV11error = "We couldn't find the this type of wallet addresses";
            cleanup();
            if (true) return;
         }
         if ( AV32group_sdt.gxTpr_Grouptype == 30 )
         {
            GXt_char3 = AV57error1;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig1(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  20,  (short)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven)), out  AV20sdt_addressessRec, out  GXt_char3) ;
            AV57error1 = GXt_char3;
            GXt_char3 = AV58error2;
            new GeneXus.Programs.wallet.registered.deriveaddresssfordelegationmusig1(context ).execute(  AV24StoredTransactions,  AV32group_sdt,  AV31wallet.gxTpr_Networktype,  10,  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  AV19sdt_addressessChange, out  GXt_char3) ;
            AV58error2 = GXt_char3;
            AV11error = StringUtil.Trim( AV57error1) + StringUtil.Trim( AV58error2);
         }
         else if ( AV32group_sdt.gxTpr_Grouptype == 20 )
         {
            if ( AV32group_sdt.gxTpr_Subgrouptype == 10 )
            {
               GXt_SdtGroup_SDT4 = AV63bount_group_sdt;
               new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV32group_sdt.gxTpr_Bountygroupid, out  GXt_SdtGroup_SDT4) ;
               AV63bount_group_sdt = GXt_SdtGroup_SDT4;
            }
            else
            {
               AV63bount_group_sdt = AV32group_sdt;
            }
            GXt_SdtExternalUser5 = AV64externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser5) ;
            AV64externalUser = GXt_SdtExternalUser5;
            AV66addressToCreate = (short)(AV63bount_group_sdt.gxTpr_Timeconstrain.Count);
            if ( AV63bount_group_sdt.gxTpr_Amigroupowner )
            {
               GXt_char3 = AV11error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV63bount_group_sdt.gxTpr_Encryptedtextshare,  AV63bount_group_sdt.gxTpr_Encpassword,  AV64externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV62base64_secret, out  GXt_char3) ;
               AV11error = GXt_char3;
            }
            else
            {
               GXt_char3 = AV11error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV63bount_group_sdt.gxTpr_Encryptedtextshare,  AV63bount_group_sdt.gxTpr_Encpassword,  AV64externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV62base64_secret, out  GXt_char3) ;
               AV11error = GXt_char3;
            }
            GXt_char3 = AV11error;
            new GeneXus.Programs.wallet.registered.deriveaddressstimebounty(context ).execute(  AV24StoredTransactions,  AV63bount_group_sdt,  AV31wallet.gxTpr_Networktype,  AV66addressToCreate,  (short)(Math.Round(NumberUtil.Val( "4", "."), 18, MidpointRounding.ToEven)),  StringUtil.Trim( AV62base64_secret), out  AV20sdt_addressessRec, out  GXt_char3) ;
            AV11error += GXt_char3;
         }
         else
         {
            AV11error = "We don't have a group associated with this addresses";
            cleanup();
            if (true) return;
         }
         AV67GXV1 = 1;
         while ( AV67GXV1 <= AV20sdt_addressessRec.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV20sdt_addressessRec.Item(AV67GXV1));
            AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV67GXV1 = (int)(AV67GXV1+1);
         }
         AV68GXV2 = 1;
         while ( AV68GXV2 <= AV19sdt_addressessChange.Count )
         {
            AV15one_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV19sdt_addressessChange.Item(AV68GXV2));
            AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV15one_address.gxTpr_Address, 0);
            AV9allAddressess.Add(AV15one_address, 0);
            AV68GXV2 = (int)(AV68GXV2+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV9allAddressess) ;
         AV69GXV3 = 1;
         while ( AV69GXV3 <= AV24StoredTransactions.gxTpr_Transaction.Count )
         {
            AV27TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV24StoredTransactions.gxTpr_Transaction.Item(AV69GXV3));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV27TransactionItem.gxTpr_Usedin.gxTpr_Transactionid)) && ( AV27TransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count == 0 ) )
            {
               AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(AV27TransactionItem.gxTpr_Scriptpubkey_address, 0);
            }
            AV69GXV3 = (int)(AV69GXV3+1);
         }
         GXt_decimal6 = AV26totalBalance;
         new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV24StoredTransactions, out  AV21SDTAddressHistory, out  GXt_decimal6) ;
         AV26totalBalance = GXt_decimal6;
         AV70GXV4 = 1;
         while ( AV70GXV4 <= AV21SDTAddressHistory.Count )
         {
            AV16oneSDTAddressHistory = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV21SDTAddressHistory.Item(AV70GXV4));
            AV55found = false;
            if ( AV16oneSDTAddressHistory.gxTpr_Confirmations < 6 )
            {
               AV71GXV5 = 1;
               while ( AV71GXV5 <= AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Count )
               {
                  AV59one_address_to_look_for = ((string)AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Item(AV71GXV5));
                  if ( StringUtil.StrCmp(AV59one_address_to_look_for, StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress)) == 0 )
                  {
                     AV55found = true;
                     if (true) break;
                  }
                  AV71GXV5 = (int)(AV71GXV5+1);
               }
               if ( ! AV55found )
               {
                  AV60addressess_to_look_for.gxTpr_Sdt_addressess.gxTpr_Address.Add(StringUtil.Trim( AV16oneSDTAddressHistory.gxTpr_Receivedaddress), 0);
               }
            }
            AV70GXV4 = (int)(AV70GXV4+1);
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
         AV60addressess_to_look_for = new SdtGxGetAddressess(context);
         AV21SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV11error = "";
         AV12extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV31wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9allAddressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV32group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV33websession = context.GetSession();
         AV54transactionFileName = "";
         AV24StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV10deserializedExtPubKey = "";
         AV57error1 = "";
         AV20sdt_addressessRec = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV58error2 = "";
         AV19sdt_addressessChange = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV63bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT4 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV64externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser5 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV62base64_secret = "";
         GXt_char3 = "";
         AV15one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV27TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV16oneSDTAddressHistory = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV59one_address_to_look_for = "";
         /* GeneXus formulas. */
      }

      private short AV66addressToCreate ;
      private int AV67GXV1 ;
      private int AV68GXV2 ;
      private int AV69GXV3 ;
      private int AV70GXV4 ;
      private int AV71GXV5 ;
      private decimal AV26totalBalance ;
      private decimal GXt_decimal6 ;
      private string AV11error ;
      private string AV54transactionFileName ;
      private string AV10deserializedExtPubKey ;
      private string AV57error1 ;
      private string AV58error2 ;
      private string AV62base64_secret ;
      private string GXt_char3 ;
      private string AV59one_address_to_look_for ;
      private bool AV55found ;
      private IGxSession AV33websession ;
      private SdtGxGetAddressess AV60addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV21SDTAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV31wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV9allAddressess ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV32group_sdt ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV24StoredTransactions ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV20sdt_addressessRec ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV19sdt_addressessChange ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV63bount_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT4 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV64externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser5 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV15one_address ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV27TransactionItem ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV16oneSDTAddressHistory ;
      private SdtGxGetAddressess aP0_addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
      private string aP3_error ;
   }

}
