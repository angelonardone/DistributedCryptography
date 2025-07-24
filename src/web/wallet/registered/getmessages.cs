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
   public class getmessages : GXProcedure
   {
      public getmessages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getmessages( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtDesktopApp_services_SDT_Messages aP0_messages ,
                           out string aP1_error )
      {
         this.AV14messages = new SdtDesktopApp_services_SDT_Messages(context) ;
         this.AV15error = "" ;
         initialize();
         ExecuteImpl();
         aP0_messages=this.AV14messages;
         aP1_error=this.AV15error;
      }

      public string executeUdp( out SdtDesktopApp_services_SDT_Messages aP0_messages )
      {
         execute(out aP0_messages, out aP1_error);
         return AV15error ;
      }

      public void executeSubmit( out SdtDesktopApp_services_SDT_Messages aP0_messages ,
                                 out string aP1_error )
      {
         this.AV14messages = new SdtDesktopApp_services_SDT_Messages(context) ;
         this.AV15error = "" ;
         SubmitImpl();
         aP0_messages=this.AV14messages;
         aP1_error=this.AV15error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV8externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV8externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8externalUser.gxTpr_Externaltoken)) )
         {
            new desktopappservicesrestgetmesssagesget(context ).execute(  AV9ServerUrlTemplatingVar,  StringUtil.Trim( AV8externalUser.gxTpr_Externaltoken), out  AV10getMesssages__getOutputOUT, out  AV11HttpMessage, out  AV12IsSuccess) ;
            if ( ! AV12IsSuccess )
            {
               AV15error = AV11HttpMessage.gxTpr_Description;
            }
            else
            {
               AV15error = AV10getMesssages__getOutputOUT.gxTpr_Error;
               AV14messages = AV10getMesssages__getOutputOUT.gxTpr_Mesages;
            }
         }
         else
         {
            AV15error = "You are not logged into Distributed Cryptography main site";
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
         AV14messages = new SdtDesktopApp_services_SDT_Messages(context);
         AV15error = "";
         AV8externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV9ServerUrlTemplatingVar = new GXProperties();
         AV10getMesssages__getOutputOUT = new SdtgetMesssages__getOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV15error ;
      private bool AV12IsSuccess ;
      private GXProperties AV9ServerUrlTemplatingVar ;
      private SdtDesktopApp_services_SDT_Messages AV14messages ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV8externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtgetMesssages__getOutput AV10getMesssages__getOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private SdtDesktopApp_services_SDT_Messages aP0_messages ;
      private string aP1_error ;
   }

}
