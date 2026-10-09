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
namespace GeneXus.Programs.distcrypt {
   public class getexternaluserpublic : GXProcedure
   {
      public getexternaluserpublic( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getexternaluserpublic( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GeneXus.Programs.distcrypt.SdtExternalUserPublic aP0_externalUserPublic )
      {
         this.AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context) ;
         initialize();
         ExecuteImpl();
         aP0_externalUserPublic=this.AV9externalUserPublic;
      }

      public GeneXus.Programs.distcrypt.SdtExternalUserPublic executeUdp( )
      {
         execute(out aP0_externalUserPublic);
         return AV9externalUserPublic ;
      }

      public void executeSubmit( out GeneXus.Programs.distcrypt.SdtExternalUserPublic aP0_externalUserPublic )
      {
         this.AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context) ;
         SubmitImpl();
         aP0_externalUserPublic=this.AV9externalUserPublic;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV8externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV8externalUser = GXt_SdtExternalUser1;
         AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8externalUser.gxTpr_Externaltoken)) )
         {
            AV9externalUserPublic.gxTpr_Isloggedin = false;
         }
         else
         {
            AV9externalUserPublic.gxTpr_Isloggedin = true;
         }
         AV9externalUserPublic.gxTpr_Userinfo = AV8externalUser.gxTpr_Userinfo;
         AV9externalUserPublic.gxTpr_Keyinfo.gxTpr_Publickey = AV8externalUser.gxTpr_Keyinfo.gxTpr_Publickey;
         AV9externalUserPublic.gxTpr_Chatkeyinfo.gxTpr_Publickey = AV8externalUser.gxTpr_Chatkeyinfo.gxTpr_Publickey;
         AV9externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey = AV8externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey;
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
         AV9externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV8externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         /* GeneXus formulas. */
      }

      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV9externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV8externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic aP0_externalUserPublic ;
   }

}
