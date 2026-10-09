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
namespace GeneXus.Programs.wallet.registered {
   public class activatetimevault : GXProcedure
   {
      public activatetimevault( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public activatetimevault( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           Guid aP1_groupId ,
                           DateTime aP2_restoreDate ,
                           out bool aP3_wasActive ,
                           out string aP4_error )
      {
         this.AV21password = aP0_password;
         this.AV16groupId = aP1_groupId;
         this.AV22restoreDate = aP2_restoreDate;
         this.AV30wasActive = false ;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP3_wasActive=this.AV30wasActive;
         aP4_error=this.AV13error;
      }

      public string executeUdp( string aP0_password ,
                                Guid aP1_groupId ,
                                DateTime aP2_restoreDate ,
                                out bool aP3_wasActive )
      {
         execute(aP0_password, aP1_groupId, aP2_restoreDate, out aP3_wasActive, out aP4_error);
         return AV13error ;
      }

      public void executeSubmit( string aP0_password ,
                                 Guid aP1_groupId ,
                                 DateTime aP2_restoreDate ,
                                 out bool aP3_wasActive ,
                                 out string aP4_error )
      {
         this.AV21password = aP0_password;
         this.AV16groupId = aP1_groupId;
         this.AV22restoreDate = aP2_restoreDate;
         this.AV30wasActive = false ;
         this.AV13error = "" ;
         SubmitImpl();
         aP3_wasActive=this.AV30wasActive;
         aP4_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV30wasActive = false;
         GXt_SdtGroup_SDT1 = AV15group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV16groupId, out  GXt_SdtGroup_SDT1) ;
         AV15group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV15group_sdt.gxTpr_Groupid) || ! AV15group_sdt.gxTpr_Amigroupowner || ! ( AV15group_sdt.gxTpr_Grouptype == 20 ) )
         {
            AV13error = "Only the owner of the Time Encrypted Vault can activate it";
            cleanup();
            if (true) return;
         }
         AV30wasActive = AV15group_sdt.gxTpr_Isactive;
         GXt_SdtGroup_SDT_TimeConstrainItem2 = AV20oneTimeConstrain;
         new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV15group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem2) ;
         AV20oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem2;
         if ( AV15group_sdt.gxTpr_Isactive )
         {
            if ( (DateTime.MinValue==AV22restoreDate) )
            {
               AV13error = "Please select the date when the backup became available to be restored.";
               cleanup();
               if (true) return;
            }
            if ( DateTimeUtil.ResetTime ( AV22restoreDate ) <= DateTimeUtil.ResetTime ( Gx_date ) )
            {
               AV13error = "The date must be in the future";
               cleanup();
               if (true) return;
            }
            if ( DateTimeUtil.ResetTime ( AV22restoreDate ) <= DateTimeUtil.ResetTime ( AV20oneTimeConstrain.gxTpr_Date ) )
            {
               AV13error = "The date must be after the previous configure date";
               cleanup();
               if (true) return;
            }
            AV19newTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
            AV19newTimeConstrain.gxTpr_Date = AV22restoreDate;
            AV19newTimeConstrain.gxTpr_Sequence = (int)(AV20oneTimeConstrain.gxTpr_Sequence+1);
         }
         else
         {
            if ( (DateTime.MinValue==AV20oneTimeConstrain.gxTpr_Date) || ( DateTimeUtil.ResetTime ( AV20oneTimeConstrain.gxTpr_Date ) <= DateTimeUtil.ResetTime ( Gx_date ) ) )
            {
               AV13error = "Please save a restore date in the future first";
               cleanup();
               if (true) return;
            }
            AV19newTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
            AV19newTimeConstrain.gxTpr_Date = AV20oneTimeConstrain.gxTpr_Date;
            AV19newTimeConstrain.gxTpr_Sequence = 0;
         }
         GXt_SdtWallet3 = AV28wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet3) ;
         AV28wallet = GXt_SdtWallet3;
         GXt_char4 = AV13error;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV21password,  AV28wallet.gxTpr_Encryptedsecret, out  AV9clearText, ref  GXt_char4) ;
         AV13error = GXt_char4;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV13error = "We couldn't decrypt the wallet with the password provided: " + AV13error;
            cleanup();
            if (true) return;
         }
         AV14extendeSecretAndAuthenticator.FromJSonString(AV9clearText, null);
         AV9clearText = "";
         GXt_char4 = AV13error;
         new GeneXus.Programs.shamirss.createshares(context ).execute(  StringUtil.Trim( AV14extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey),  2,  2, out  AV25shares, ref  GXt_char4) ;
         AV13error = GXt_char4;
         AV14extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV11EncryptionResult = AV12EncryptionService.encrypt(((string)AV25shares.Item(2)), "");
         if ( ! AV11EncryptionResult.gxTpr_Success )
         {
            AV13error = "There was a problem encrypting the second share";
            cleanup();
            if (true) return;
         }
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.tevactivatedatagroup(context ).execute(  AV15group_sdt.gxTpr_Datagroupid,  ((string)AV25shares.Item(1)),  AV11EncryptionResult.gxTpr_Ciphertext, out  GXt_char4) ;
         AV13error = GXt_char4;
         AV25shares.Clear();
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.tevactivatebountygroup(context ).execute(  AV15group_sdt.gxTpr_Bountygroupid,  AV11EncryptionResult.gxTpr_Generatedkey, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV8bount_group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV15group_sdt.gxTpr_Bountygroupid, out  GXt_SdtGroup_SDT1) ;
         AV8bount_group_sdt = GXt_SdtGroup_SDT1;
         AV27timeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV15group_sdt.gxTpr_Timeconstrain.Clone());
         /* Execute user subroutine: 'REPLACE NEWEST' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV8bount_group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV27timeConstrains.Clone());
         GXt_SdtWalletInfo5 = AV29walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo5) ;
         AV29walletInfo = GXt_SdtWalletInfo5;
         AV26storedTransactions.gxTpr_Transaction.Clear();
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.deriveaddressstimebounty(context ).execute(  AV26storedTransactions,  AV8bount_group_sdt,  AV29walletInfo.gxTpr_Networktype,  (short)(AV19newTimeConstrain.gxTpr_Sequence+1),  (short)(Math.Round(NumberUtil.Val( "4", "."), 18, MidpointRounding.ToEven)),  AV11EncryptionResult.gxTpr_Generatedkey, out  AV23sdt_addressess, out  GXt_char4) ;
         AV13error = GXt_char4;
         AV11EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         AV32GXV1 = 1;
         while ( AV32GXV1 <= AV23sdt_addressess.Count )
         {
            AV24sdt_oneAddress = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV23sdt_addressess.Item(AV32GXV1));
            if ( AV24sdt_oneAddress.gxTpr_Creationsequence == AV19newTimeConstrain.gxTpr_Sequence )
            {
               AV19newTimeConstrain.gxTpr_Address = StringUtil.Trim( AV24sdt_oneAddress.gxTpr_Address);
               AV19newTimeConstrain.gxTpr_Encryptedsecret = AV8bount_group_sdt.gxTpr_Encryptedtextshare;
               AV19newTimeConstrain.gxTpr_Encryptedkey = AV8bount_group_sdt.gxTpr_Encpassword;
               if (true) break;
            }
            AV32GXV1 = (int)(AV32GXV1+1);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV19newTimeConstrain.gxTpr_Address)) )
         {
            AV13error = "We couldn't derive the bounty address";
            cleanup();
            if (true) return;
         }
         AV27timeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV15group_sdt.gxTpr_Timeconstrain.Clone());
         /* Execute user subroutine: 'REPLACE NEWEST' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV15group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV27timeConstrains.Clone());
         AV15group_sdt.gxTpr_Isactive = true;
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV15group_sdt,  AV15group_sdt.gxTpr_Encpassword, out  AV17grpupId, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char4 = AV13error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV15group_sdt, out  GXt_char4) ;
            AV13error = GXt_char4;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV8bount_group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV15group_sdt.gxTpr_Bountygroupid, out  GXt_SdtGroup_SDT1) ;
         AV8bount_group_sdt = GXt_SdtGroup_SDT1;
         AV27timeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV8bount_group_sdt.gxTpr_Timeconstrain.Clone());
         /* Execute user subroutine: 'REPLACE NEWEST' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV8bount_group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV27timeConstrains.Clone());
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV8bount_group_sdt,  AV8bount_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV17grpupId, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char4 = AV13error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV8bount_group_sdt, out  GXt_char4) ;
            AV13error = GXt_char4;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT1 = AV10data_group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV15group_sdt.gxTpr_Datagroupid, out  GXt_SdtGroup_SDT1) ;
         AV10data_group_sdt = GXt_SdtGroup_SDT1;
         AV27timeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV10data_group_sdt.gxTpr_Timeconstrain.Clone());
         /* Execute user subroutine: 'REPLACE NEWEST' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV10data_group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV27timeConstrains.Clone());
         GXt_char4 = AV13error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV10data_group_sdt,  AV10data_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV17grpupId, out  GXt_char4) ;
         AV13error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GXt_char4 = AV13error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV10data_group_sdt, out  GXt_char4) ;
            AV13error = GXt_char4;
         }
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         cleanup();
      }

      protected void S111( )
      {
         /* 'REPLACE NEWEST' Routine */
         returnInSub = false;
         AV18keepTimeConstrains.Clear();
         AV33GXV2 = 1;
         while ( AV33GXV2 <= AV27timeConstrains.Count )
         {
            AV20oneTimeConstrain = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)AV27timeConstrains.Item(AV33GXV2));
            if ( ! ( AV20oneTimeConstrain.gxTpr_Sequence == AV19newTimeConstrain.gxTpr_Sequence ) )
            {
               AV18keepTimeConstrains.Add((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)(AV20oneTimeConstrain.Clone()), 0);
            }
            AV33GXV2 = (int)(AV33GXV2+1);
         }
         AV18keepTimeConstrains.Add((GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem)(AV19newTimeConstrain.Clone()), 0);
         AV27timeConstrains = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV18keepTimeConstrains.Clone());
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
         AV13error = "";
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         Gx_date = DateTime.MinValue;
         AV19newTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV28wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9clearText = "";
         AV14extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV25shares = new GxSimpleCollection<string>();
         AV11EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV12EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV8bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV27timeConstrains = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>( context, "Group_SDT.TimeConstrainItem", "distributedcryptography");
         AV29walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo5 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         AV26storedTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV23sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV24sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV17grpupId = Guid.Empty;
         AV10data_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_char4 = "";
         AV18keepTimeConstrains = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>( context, "Group_SDT.TimeConstrainItem", "distributedcryptography");
         Gx_date = DateTimeUtil.Today( context);
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      private int AV32GXV1 ;
      private int AV33GXV2 ;
      private string AV21password ;
      private string AV13error ;
      private string GXt_char4 ;
      private DateTime AV22restoreDate ;
      private DateTime Gx_date ;
      private bool AV30wasActive ;
      private bool returnInSub ;
      private string AV9clearText ;
      private Guid AV16groupId ;
      private Guid AV17grpupId ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV20oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV19newTimeConstrain ;
      private GeneXus.Programs.wallet.SdtWallet AV28wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV14extendeSecretAndAuthenticator ;
      private GxSimpleCollection<string> AV25shares ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV11EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV12EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8bount_group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> AV27timeConstrains ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV29walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo5 ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV26storedTransactions ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV23sdt_addressess ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV24sdt_oneAddress ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10data_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem> AV18keepTimeConstrains ;
      private bool aP3_wasActive ;
      private string aP4_error ;
   }

}
