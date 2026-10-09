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
   public class setvaultpasswordcontacts : GXProcedure
   {
      public setvaultpasswordcontacts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setvaultpasswordcontacts( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP2_contacts ,
                           out string aP3_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV14passwordId = aP1_passwordId;
         this.AV8contacts = aP2_contacts;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_passwordId ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP2_contacts )
      {
         execute(aP0_groupId, aP1_passwordId, aP2_contacts, out aP3_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_passwordId ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> aP2_contacts ,
                                 out string aP3_error )
      {
         this.AV11groupId = aP0_groupId;
         this.AV14passwordId = aP1_passwordId;
         this.AV8contacts = aP2_contacts;
         this.AV9error = "" ;
         SubmitImpl();
         aP3_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV11groupId, out  AV15vault, out  AV12isOwner, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( (Guid.Empty==AV11groupId) || ! AV12isOwner )
         {
            AV9error = "Only the owner of the group can share the passwords";
            cleanup();
            if (true) return;
         }
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV15vault.gxTpr_Password.Count )
         {
            AV10findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV15vault.gxTpr_Password.Item(AV16GXV1));
            if ( AV10findPassword.gxTpr_Passwordid == AV14passwordId )
            {
               AV10findPassword.gxTpr_Contact.Clear();
               AV17GXV2 = 1;
               while ( AV17GXV2 <= AV8contacts.Count )
               {
                  AV13oneContact = ((GeneXus.Programs.wallet.SdtVaultContact)AV8contacts.Item(AV17GXV2));
                  AV10findPassword.gxTpr_Contact.Add(AV13oneContact.gxTpr_Contactid, 0);
                  AV17GXV2 = (int)(AV17GXV2+1);
               }
            }
            AV16GXV1 = (int)(AV16GXV1+1);
         }
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.writevault(context ).execute(  AV11groupId,  AV15vault, out  GXt_char1) ;
         AV9error = GXt_char1;
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
         AV9error = "";
         AV15vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV10findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV13oneContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private int AV17GXV2 ;
      private string AV9error ;
      private string GXt_char1 ;
      private bool AV12isOwner ;
      private Guid AV11groupId ;
      private Guid AV14passwordId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV8contacts ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV15vault ;
      private GeneXus.Programs.wallet.SdtPassword AV10findPassword ;
      private GeneXus.Programs.wallet.SdtVaultContact AV13oneContact ;
      private string aP3_error ;
   }

}
