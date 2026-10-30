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
   public class discovergroupaddresses : GXProcedure
   {
      public discovergroupaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public discovergroupaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtGxGetAddressess aP0_addressess_to_look_for ,
                           out GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ,
                           out decimal aP2_totalBalance ,
                           out string aP3_error )
      {
         this.AV8addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV15SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV17totalBalance = 0 ;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP0_addressess_to_look_for=this.AV8addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV15SDTAddressHistory;
         aP2_totalBalance=this.AV17totalBalance;
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
         this.AV8addressess_to_look_for = new SdtGxGetAddressess(context) ;
         this.AV15SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography") ;
         this.AV17totalBalance = 0 ;
         this.AV11error = "" ;
         SubmitImpl();
         aP0_addressess_to_look_for=this.AV8addressess_to_look_for;
         aP1_SDTAddressHistory=this.AV15SDTAddressHistory;
         aP2_totalBalance=this.AV17totalBalance;
         aP3_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12group_sdt.FromJSonString(AV19websession.Get("Group_EDIT_WALLET"), null);
         AV18transactionFileName = StringUtil.Trim( AV12group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
         AV10countReceiving = 0;
         AV9countChange = 0;
         AV14rounds = 0;
         while ( ( AV10countReceiving < 20 ) || ( AV9countChange < 10 ) )
         {
            AV15SDTAddressHistory.Clear();
            AV13historyWithBalance.Clear();
            new GeneXus.Programs.wallet.sethistorywithbalance(context ).execute(  AV13historyWithBalance) ;
            AV17totalBalance = 0;
            GXt_char1 = AV11error;
            new GeneXus.Programs.wallet.createunusedaddressesgroup(context ).execute( out  AV8addressess_to_look_for, out  AV15SDTAddressHistory, out  AV17totalBalance, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               if (true) break;
            }
            GXt_char1 = AV11error;
            new GeneXus.Programs.wallet.gettransactionsfromservice(context ).execute(  AV18transactionFileName,  AV8addressess_to_look_for, out  AV16StoredTransactions, out  GXt_char1) ;
            AV11error = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV11error = "We cannot connect with the Electrum server: " + StringUtil.Trim( AV11error);
               if (true) break;
            }
            GXt_decimal2 = AV17totalBalance;
            new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV16StoredTransactions, out  AV15SDTAddressHistory, out  GXt_decimal2) ;
            AV17totalBalance = GXt_decimal2;
            GXt_int3 = AV10countReceiving;
            new GeneXus.Programs.wallet.areenoughunusedaddresses(context ).execute(  (short)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven)), out  GXt_int3) ;
            AV10countReceiving = GXt_int3;
            GXt_int3 = AV9countChange;
            new GeneXus.Programs.wallet.areenoughunusedaddresses(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_int3) ;
            AV9countChange = GXt_int3;
            AV14rounds = (short)(AV14rounds+1);
            if ( AV14rounds >= 50 )
            {
               if (true) break;
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
         AV8addressess_to_look_for = new SdtGxGetAddressess(context);
         AV15SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV11error = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19websession = context.GetSession();
         AV18transactionFileName = "";
         AV13historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_char1 = "";
         AV16StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         /* GeneXus formulas. */
      }

      private short AV10countReceiving ;
      private short AV9countChange ;
      private short AV14rounds ;
      private short GXt_int3 ;
      private decimal AV17totalBalance ;
      private decimal GXt_decimal2 ;
      private string AV11error ;
      private string AV18transactionFileName ;
      private string GXt_char1 ;
      private IGxSession AV19websession ;
      private SdtGxGetAddressess AV8addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV15SDTAddressHistory ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV13historyWithBalance ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV16StoredTransactions ;
      private SdtGxGetAddressess aP0_addressess_to_look_for ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_SDTAddressHistory ;
      private decimal aP2_totalBalance ;
      private string aP3_error ;
   }

}
