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
   public class deletecontact : GXProcedure
   {
      public deletecontact( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletecontact( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_ContactId ,
                           out string aP1_error )
      {
         this.AV23ContactId = aP0_ContactId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_ContactId )
      {
         execute(aP0_ContactId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_ContactId ,
                                 out string aP1_error )
      {
         this.AV23ContactId = aP0_ContactId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV10externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10externalUser.gxTpr_Externaltoken)) )
         {
            AV26body.gxTpr_Contactid = AV23ContactId;
            new desktopappservicesrestdeletecontactpost(context ).execute(  AV13properties,  AV26body,  AV10externalUser.gxTpr_Externaltoken, out  AV27deleteContact__postOutput, out  AV11HttpMessage, out  AV12IsSuccess) ;
            if ( ! AV12IsSuccess )
            {
               AV9error = AV11HttpMessage.gxTpr_Description;
            }
            else
            {
               AV9error = AV27deleteContact__postOutput.gxTpr_Error;
            }
         }
         else
         {
            AV9error = "You are not logged into Distributed Cryptography main site";
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
         AV9error = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV26body = new SdtdeleteContact__postInput(context);
         AV13properties = new GXProperties();
         AV27deleteContact__postOutput = new SdtdeleteContact__postOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private bool AV12IsSuccess ;
      private Guid AV23ContactId ;
      private GXProperties AV13properties ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtdeleteContact__postInput AV26body ;
      private SdtdeleteContact__postOutput AV27deleteContact__postOutput ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private string aP1_error ;
   }

}
