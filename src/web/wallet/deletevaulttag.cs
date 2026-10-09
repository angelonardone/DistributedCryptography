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
   public class deletevaulttag : GXProcedure
   {
      public deletevaulttag( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletevaulttag( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_tagId ,
                           out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13tagId = aP1_tagId;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_tagId )
      {
         execute(aP0_groupId, aP1_tagId, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_tagId ,
                                 out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13tagId = aP1_tagId;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV10groupId, out  AV14vault, out  AV11isOwner, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( ! AV11isOwner )
         {
            AV8error = "Only the owner of the group can change the tags";
            cleanup();
            if (true) return;
         }
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV14vault.gxTpr_Password_tag.Count )
         {
            AV9findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV14vault.gxTpr_Password_tag.Item(AV15GXV1));
            if ( AV9findTag.gxTpr_Tagid == AV13tagId )
            {
               AV14vault.gxTpr_Password_tag.RemoveItem(AV14vault.gxTpr_Password_tag.IndexOf(AV9findTag));
               if (true) break;
            }
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         AV16GXV2 = 1;
         while ( AV16GXV2 <= AV14vault.gxTpr_Password.Count )
         {
            AV12onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV14vault.gxTpr_Password.Item(AV16GXV2));
            AV17GXV3 = 1;
            while ( AV17GXV3 <= AV12onePassword.gxTpr_Password_tag.Count )
            {
               AV9findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV12onePassword.gxTpr_Password_tag.Item(AV17GXV3));
               if ( AV9findTag.gxTpr_Tagid == AV13tagId )
               {
                  AV12onePassword.gxTpr_Password_tag.RemoveItem(AV12onePassword.gxTpr_Password_tag.IndexOf(AV9findTag));
                  if (true) break;
               }
               AV17GXV3 = (int)(AV17GXV3+1);
            }
            AV16GXV2 = (int)(AV16GXV2+1);
         }
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.writevault(context ).execute(  AV10groupId,  AV14vault, out  GXt_char1) ;
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
         AV14vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV9findTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV12onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private int AV16GXV2 ;
      private int AV17GXV3 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private Guid AV10groupId ;
      private Guid AV13tagId ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV14vault ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV9findTag ;
      private GeneXus.Programs.wallet.SdtPassword AV12onePassword ;
      private string aP2_error ;
   }

}
