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
   public class setvaultpasswordtags : GXProcedure
   {
      public setvaultpasswordtags( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setvaultpasswordtags( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP2_tags ,
                           out string aP3_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13passwordId = aP1_passwordId;
         this.AV14tags = aP2_tags;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_passwordId ,
                                GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP2_tags )
      {
         execute(aP0_groupId, aP1_passwordId, aP2_tags, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_passwordId ,
                                 GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> aP2_tags ,
                                 out string aP3_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13passwordId = aP1_passwordId;
         this.AV14tags = aP2_tags;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV10groupId, out  AV15vault, out  AV11isOwner, out  GXt_char1) ;
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
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV15vault.gxTpr_Password.Count )
         {
            AV9findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV15vault.gxTpr_Password.Item(AV16GXV1));
            if ( AV9findPassword.gxTpr_Passwordid == AV13passwordId )
            {
               AV9findPassword.gxTpr_Password_tag.Clear();
               AV17GXV2 = 1;
               while ( AV17GXV2 <= AV14tags.Count )
               {
                  AV12oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV14tags.Item(AV17GXV2));
                  AV9findPassword.gxTpr_Password_tag.Add((GeneXus.Programs.wallet.SdtPassword_tag)(AV12oneTag.Clone()), 0);
                  AV17GXV2 = (int)(AV17GXV2+1);
               }
            }
            AV16GXV1 = (int)(AV16GXV1+1);
         }
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.writevault(context ).execute(  AV10groupId,  AV15vault, out  GXt_char1) ;
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
         AV15vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV9findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV12oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private int AV17GXV2 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private Guid AV10groupId ;
      private Guid AV13passwordId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV14tags ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV15vault ;
      private GeneXus.Programs.wallet.SdtPassword AV9findPassword ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV12oneTag ;
      private string aP3_error ;
   }

}
