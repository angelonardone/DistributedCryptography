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
   public class getmemberdisplayname : GXProcedure
   {
      public getmemberdisplayname( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getmemberdisplayname( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_userName ,
                           out string aP1_displayName )
      {
         this.AV12userName = aP0_userName;
         this.AV9displayName = "" ;
         initialize();
         ExecuteImpl();
         aP1_displayName=this.AV9displayName;
      }

      public string executeUdp( string aP0_userName )
      {
         execute(aP0_userName, out aP1_displayName);
         return AV9displayName ;
      }

      public void executeSubmit( string aP0_userName ,
                                 out string aP1_displayName )
      {
         this.AV12userName = aP0_userName;
         this.AV9displayName = "" ;
         SubmitImpl();
         aP1_displayName=this.AV9displayName;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUserPublic1 = AV10externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV10externalUserPublic = GXt_SdtExternalUserPublic1;
         if ( StringUtil.StrCmp(StringUtil.Trim( AV12userName), StringUtil.Trim( AV10externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
         {
            AV9displayName = "me";
            cleanup();
            if (true) return;
         }
         AV9displayName = "";
         new GeneXus.Programs.wallet.registered.getcontactid(context ).execute(  StringUtil.Trim( AV12userName), out  AV13userPrivateName, out  AV8contactId) ;
         if ( ! (Guid.Empty==AV8contactId) && ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV13userPrivateName))) )
         {
            AV9displayName = StringUtil.Trim( AV13userPrivateName);
         }
         else
         {
            AV9displayName = StringUtil.Trim( AV12userName);
            if ( StringUtil.Len( AV9displayName) > 16 )
            {
               AV9displayName = StringUtil.Substring( AV9displayName, 1, 14) + "...";
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
         AV9displayName = "";
         AV10externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV13userPrivateName = "";
         AV8contactId = Guid.Empty;
         /* GeneXus formulas. */
      }

      private string AV13userPrivateName ;
      private string AV12userName ;
      private string AV9displayName ;
      private Guid AV8contactId ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV10externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private string aP1_displayName ;
   }

}
