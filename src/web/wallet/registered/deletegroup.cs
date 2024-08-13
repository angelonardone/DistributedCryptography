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
namespace GeneXus.Programs.wallet.registered {
   public class deletegroup : GXProcedure
   {
      public deletegroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deletegroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_error )
      {
         this.AV9groupId = aP0_groupId;
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
         this.AV9groupId = aP0_groupId;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_error=this.AV8error;
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
            AV13body.gxTpr_Groupid = AV9groupId;
            new desktopappservicesrestdeletegrouppost(context ).execute(  AV12properties,  AV13body,  AV10externalUser.gxTpr_Externaltoken, out  AV14deleteGroup__postOutputOUT, out  AV15HttpMessage, out  AV11IsSuccess) ;
            if ( ! AV11IsSuccess )
            {
               AV8error = AV15HttpMessage.gxTpr_Description;
            }
            else
            {
               AV8error = AV14deleteGroup__postOutputOUT.gxTpr_Error;
            }
         }
         else
         {
            AV8error = "You are not logged into Distributed Cryptography main site";
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
         AV8error = "";
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13body = new SdtdeleteGroup__postInput(context);
         AV12properties = new GXProperties();
         AV14deleteGroup__postOutputOUT = new SdtdeleteGroup__postOutput(context);
         AV15HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV8error ;
      private bool AV11IsSuccess ;
      private Guid AV9groupId ;
      private GXProperties AV12properties ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtdeleteGroup__postInput AV13body ;
      private SdtdeleteGroup__postOutput AV14deleteGroup__postOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV15HttpMessage ;
      private string aP1_error ;
   }

}
