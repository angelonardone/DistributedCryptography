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
   public class setvaultpasswordtoedit : GXProcedure
   {
      public setvaultpasswordtoedit( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setvaultpasswordtoedit( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           out string aP2_error )
      {
         this.AV9groupId = aP0_groupId;
         this.AV12passwordId = aP1_passwordId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_passwordId )
      {
         execute(aP0_groupId, aP1_passwordId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_passwordId ,
                                 out string aP2_error )
      {
         this.AV9groupId = aP0_groupId;
         this.AV12passwordId = aP1_passwordId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14websession.Set("ONE_PASSWORD_TO_ENCR", "");
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV9groupId, out  AV13vault, out  AV10isOwner, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( ! AV10isOwner )
         {
            AV8error = "Only the owner of the group can change the passwords";
            cleanup();
            if (true) return;
         }
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV13vault.gxTpr_Password.Count )
         {
            AV11onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV13vault.gxTpr_Password.Item(AV15GXV1));
            if ( AV11onePassword.gxTpr_Passwordid == AV12passwordId )
            {
               AV14websession.Set("ONE_PASSWORD_TO_ENCR", AV11onePassword.ToJSonString(false, true));
               cleanup();
               if (true) return;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         AV8error = "We couldn't find that password, please refresh the page";
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
         AV8error = "";
         AV14websession = context.GetSession();
         GXt_char1 = "";
         AV13vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV11onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV10isOwner ;
      private Guid AV9groupId ;
      private Guid AV12passwordId ;
      private IGxSession AV14websession ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV13vault ;
      private GeneXus.Programs.wallet.SdtPassword AV11onePassword ;
      private string aP2_error ;
   }

}
