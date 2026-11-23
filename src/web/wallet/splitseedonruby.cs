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
   public class splitseedonruby : GXProcedure
   {
      public splitseedonruby( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public splitseedonruby( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_Gx_mode ,
                           GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                           out GeneXus.Programs.wallet.SdtRubySeedSplit aP2_rubySeedSplit ,
                           out string aP3_error )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV10group_sdt = aP1_group_sdt;
         this.AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context) ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_rubySeedSplit=this.AV16rubySeedSplit;
         aP3_error=this.AV9error;
      }

      public string executeUdp( string aP0_Gx_mode ,
                                GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                out GeneXus.Programs.wallet.SdtRubySeedSplit aP2_rubySeedSplit )
      {
         execute(aP0_Gx_mode, aP1_group_sdt, out aP2_rubySeedSplit, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_Gx_mode ,
                                 GeneXus.Programs.wallet.registered.SdtGroup_SDT aP1_group_sdt ,
                                 out GeneXus.Programs.wallet.SdtRubySeedSplit aP2_rubySeedSplit ,
                                 out string aP3_error )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV10group_sdt = aP1_group_sdt;
         this.AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context) ;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_rubySeedSplit=this.AV16rubySeedSplit;
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context);
         GXt_SdtWallet1 = AV19wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV19wallet = GXt_SdtWallet1;
         if ( ( StringUtil.StrCmp(AV19wallet.gxTpr_Keystorage, "Ruby") != 0 ) || String.IsNullOrEmpty(StringUtil.RTrim( AV19wallet.gxTpr_Rubydevice)) )
         {
            AV9error = "Only a Ruby wallet splits its seed on Ruby";
            cleanup();
            if (true) return;
         }
         AV8device = StringUtil.Trim( AV19wallet.gxTpr_Rubydevice);
         AV13networkTypeText = AV19wallet.gxTpr_Networktype;
         AV12json = AV14rubyProcessor.getpublicinfo(AV8device, AV13networkTypeText);
         AV15rubyPublic.FromJSonString(AV12json, null);
         if ( ! AV15rubyPublic.gxTpr_Success )
         {
            AV9error = "Ruby: " + StringUtil.Trim( AV15rubyPublic.gxTpr_Error) + ". Open the Ruby wallet again (Ruby locks itself after a while)";
            cleanup();
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19wallet.gxTpr_Rubyfingerprint)) && ( StringUtil.StrCmp(StringUtil.Trim( AV19wallet.gxTpr_Rubyfingerprint), StringUtil.Trim( AV15rubyPublic.gxTpr_Fingerprint)) != 0 ) )
         {
            AV9error = "This Ruby holds another wallet (fingerprint " + StringUtil.Trim( AV15rubyPublic.gxTpr_Fingerprint) + ", expected " + StringUtil.Trim( AV19wallet.gxTpr_Rubyfingerprint) + ")";
            cleanup();
            if (true) return;
         }
         AV18splitContacts.Clear();
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV10group_sdt.gxTpr_Contact.Count )
         {
            AV11groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV10group_sdt.gxTpr_Contact.Item(AV21GXV1));
            AV17splitContact = new GeneXus.Programs.wallet.SdtRubySeedSplitContact(context);
            AV17splitContact.gxTpr_Name = StringUtil.Trim( AV11groupContact.gxTpr_Contactusername);
            AV17splitContact.gxTpr_Pubkey = StringUtil.Trim( AV11groupContact.gxTpr_Contactuserpubkey);
            AV17splitContact.gxTpr_Shares = AV11groupContact.gxTpr_Numshares;
            AV18splitContacts.Add(AV17splitContact, 0);
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         AV12json = AV14rubyProcessor.seedsplit(AV8device, Gx_mode, AV10group_sdt.gxTpr_Minimumshares, AV18splitContacts.ToJSonString(false));
         AV16rubySeedSplit.FromJSonString(AV12json, null);
         AV12json = "";
         if ( ! AV16rubySeedSplit.gxTpr_Success )
         {
            if ( AV16rubySeedSplit.gxTpr_Cancelled )
            {
               AV9error = "Declined on Ruby: nothing was activated";
            }
            else
            {
               AV9error = "Ruby: " + StringUtil.Trim( AV16rubySeedSplit.gxTpr_Error);
            }
            AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context);
            cleanup();
            if (true) return;
         }
         if ( AV16rubySeedSplit.gxTpr_Contacts.Count != AV10group_sdt.gxTpr_Contact.Count )
         {
            AV9error = "Ruby returned the shares of another number of contacts";
            AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context);
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
         AV16rubySeedSplit = new GeneXus.Programs.wallet.SdtRubySeedSplit(context);
         AV9error = "";
         AV19wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8device = "";
         AV13networkTypeText = "";
         AV12json = "";
         AV14rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV15rubyPublic = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV18splitContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtRubySeedSplitContact>( context, "RubySeedSplitContact", "distributedcryptography");
         AV11groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV17splitContact = new GeneXus.Programs.wallet.SdtRubySeedSplitContact(context);
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private string Gx_mode ;
      private string AV9error ;
      private string AV12json ;
      private string AV8device ;
      private string AV13networkTypeText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV10group_sdt ;
      private GeneXus.Programs.wallet.SdtRubySeedSplit AV16rubySeedSplit ;
      private GeneXus.Programs.wallet.SdtWallet AV19wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV14rubyProcessor ;
      private GeneXus.Programs.wallet.SdtRubyResult AV15rubyPublic ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtRubySeedSplitContact> AV18splitContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV11groupContact ;
      private GeneXus.Programs.wallet.SdtRubySeedSplitContact AV17splitContact ;
      private GeneXus.Programs.wallet.SdtRubySeedSplit aP2_rubySeedSplit ;
      private string aP3_error ;
   }

}
