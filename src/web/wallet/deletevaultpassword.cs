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
   public class deletevaultpassword : GXProcedure
   {
      public deletevaultpassword( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletevaultpassword( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
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
         this.AV10groupId = aP0_groupId;
         this.AV12passwordId = aP1_passwordId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
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
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV13vault.gxTpr_Password.Count )
         {
            AV9findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV13vault.gxTpr_Password.Item(AV14GXV1));
            if ( AV9findPassword.gxTpr_Passwordid == AV12passwordId )
            {
               AV13vault.gxTpr_Password.RemoveItem(AV13vault.gxTpr_Password.IndexOf(AV9findPassword));
               if (true) break;
            }
            AV14GXV1 = (int)(AV14GXV1+1);
         }
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
         AV13vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV9findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private Guid AV10groupId ;
      private Guid AV12passwordId ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV13vault ;
      private GeneXus.Programs.wallet.SdtPassword AV9findPassword ;
      private string aP2_error ;
   }

}
