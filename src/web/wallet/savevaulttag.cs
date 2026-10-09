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
   public class savevaulttag : GXProcedure
   {
      public savevaulttag( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savevaulttag( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           GeneXus.Programs.wallet.SdtPassword_tag aP1_tag ,
                           out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13tag = aP1_tag;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                GeneXus.Programs.wallet.SdtPassword_tag aP1_tag )
      {
         execute(aP0_groupId, aP1_tag, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 GeneXus.Programs.wallet.SdtPassword_tag aP1_tag ,
                                 out string aP2_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13tag = aP1_tag;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13tag.gxTpr_Name)) )
         {
            AV8error = "Please enter a Name for the tag";
            cleanup();
            if (true) return;
         }
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
         AV12oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV12oneTag.gxTpr_Name = StringUtil.Trim( AV13tag.gxTpr_Name);
         if ( (Guid.Empty==AV13tag.gxTpr_Tagid) )
         {
            AV12oneTag.gxTpr_Tagid = Guid.NewGuid( );
         }
         else
         {
            AV12oneTag.gxTpr_Tagid = AV13tag.gxTpr_Tagid;
            AV15GXV1 = 1;
            while ( AV15GXV1 <= AV14vault.gxTpr_Password_tag.Count )
            {
               AV9findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV14vault.gxTpr_Password_tag.Item(AV15GXV1));
               if ( AV9findTag.gxTpr_Tagid == AV13tag.gxTpr_Tagid )
               {
                  AV14vault.gxTpr_Password_tag.RemoveItem(AV14vault.gxTpr_Password_tag.IndexOf(AV9findTag));
                  if (true) break;
               }
               AV15GXV1 = (int)(AV15GXV1+1);
            }
         }
         AV14vault.gxTpr_Password_tag.Add(AV12oneTag, 0);
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
         AV12oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV9findTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private Guid AV10groupId ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV13tag ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV14vault ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV12oneTag ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV9findTag ;
      private string aP2_error ;
   }

}
