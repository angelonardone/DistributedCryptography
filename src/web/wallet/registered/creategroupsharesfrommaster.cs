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
   public class creategroupsharesfrommaster : GXProcedure
   {
      public creategroupsharesfrommaster( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public creategroupsharesfrommaster( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_password ,
                           ref GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           out string aP2_error )
      {
         this.AV15password = aP0_password;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP1_group_sdt=this.AV13group_sdt;
         aP2_error=this.AV11error;
      }

      public string executeUdp( string aP0_password ,
                                ref GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt )
      {
         execute(aP0_password, ref aP1_group_sdt, out aP2_error);
         return AV11error ;
      }

      public void executeSubmit( string aP0_password ,
                                 ref GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 out string aP2_error )
      {
         this.AV15password = aP0_password;
         this.AV13group_sdt = aP1_group_sdt;
         this.AV11error = "" ;
         SubmitImpl();
         aP1_group_sdt=this.AV13group_sdt;
         aP2_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV18wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV18wallet = GXt_SdtWallet1;
         GXt_char2 = AV11error;
         new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  20,  AV15password,  AV18wallet.gxTpr_Encryptedsecret, out  AV10clearText, ref  GXt_char2) ;
         AV11error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV11error = "We couldn't decrypt the wallet with the password provided: " + AV11error;
            cleanup();
            if (true) return;
         }
         AV12extendeSecretAndAuthenticator.FromJSonString(AV10clearText, null);
         AV19totalUserShares = 0;
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV20GXV1));
            AV19totalUserShares = (short)(AV19totalUserShares+(AV14groupContact.gxTpr_Numshares));
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         GXt_char2 = AV11error;
         new GeneXus.Programs.shamirss.createshares(context ).execute(  StringUtil.Trim( AV12extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey),  AV19totalUserShares,  AV13group_sdt.gxTpr_Minimumshares, out  AV16shares, ref  GXt_char2) ;
         AV11error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV8assignShares = 1;
            AV21GXV2 = 1;
            while ( AV21GXV2 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV14groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV21GXV2));
               AV17userShares.Clear();
               AV9i = 1;
               while ( AV9i <= AV14groupContact.gxTpr_Numshares )
               {
                  AV17userShares.Add(((string)AV16shares.Item(AV8assignShares)), 0);
                  AV8assignShares = (short)(AV8assignShares+1);
                  AV9i = (short)(AV9i+1);
               }
               GXt_char2 = AV11error;
               GXt_char3 = AV14groupContact.gxTpr_Contactencryptedkey;
               GXt_char4 = AV14groupContact.gxTpr_Contactencryptedtext;
               new GeneXus.Programs.distributedcryptographylib.encryptjsonto(context ).execute(  AV17userShares.ToJSonString(false),  StringUtil.Trim( AV14groupContact.gxTpr_Contactuserpubkey), out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
               AV14groupContact.gxTpr_Contactencryptedkey = GXt_char3;
               AV14groupContact.gxTpr_Contactencryptedtext = GXt_char4;
               AV11error = GXt_char2;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  if (true) break;
               }
               AV21GXV2 = (int)(AV21GXV2+1);
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
         AV11error = "";
         AV18wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV10clearText = "";
         AV12extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         AV14groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV16shares = new GxSimpleCollection<string>();
         AV17userShares = new GxSimpleCollection<string>();
         GXt_char2 = "";
         GXt_char3 = "";
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV19totalUserShares ;
      private short AV8assignShares ;
      private short AV9i ;
      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private string AV15password ;
      private string AV11error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private string AV10clearText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ;
      private GeneXus.Programs.wallet.SdtWallet AV18wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV12extendeSecretAndAuthenticator ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV14groupContact ;
      private GxSimpleCollection<string> AV16shares ;
      private GxSimpleCollection<string> AV17userShares ;
      private string aP2_error ;
   }

}
