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
   public class savevaultpassword : GXProcedure
   {
      public savevaultpassword( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savevaultpassword( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId )
      {
         execute(aP0_groupId, out aP1_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12onePassword.FromJSonString(AV14websession.Get("ONE_PASSWORD_TO_ENCR"), null);
         AV14websession.Set("ONE_PASSWORD_TO_ENCR", "");
         if ( (Guid.Empty==AV12onePassword.gxTpr_Passwordid) )
         {
            cleanup();
            if (true) return;
         }
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV10groupId, out  AV13vault, out  AV11isOwner, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( ! AV11isOwner )
         {
            AV8error = "Only the owner of the group can change the passwords";
            cleanup();
            if (true) return;
         }
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV13vault.gxTpr_Password.Count )
         {
            AV9findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV13vault.gxTpr_Password.Item(AV15GXV1));
            if ( AV9findPassword.gxTpr_Passwordid == AV12onePassword.gxTpr_Passwordid )
            {
               AV13vault.gxTpr_Password.RemoveItem(AV13vault.gxTpr_Password.IndexOf(AV9findPassword));
               if (true) break;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         AV13vault.gxTpr_Password.Add(AV12onePassword, 0);
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.writevault(context ).execute(  AV10groupId,  AV13vault, out  GXt_char1) ;
         AV8error = GXt_char1;
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
         AV12onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV14websession = context.GetSession();
         AV13vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV9findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private Guid AV10groupId ;
      private IGxSession AV14websession ;
      private GeneXus.Programs.wallet.SdtPassword AV12onePassword ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV13vault ;
      private GeneXus.Programs.wallet.SdtPassword AV9findPassword ;
      private string aP1_error ;
   }

}
