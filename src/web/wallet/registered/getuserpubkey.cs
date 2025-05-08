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
   public class getuserpubkey : GXProcedure
   {
      public getuserpubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getuserpubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_userName ,
                           out string aP1_userPubKey ,
                           out string aP2_error )
      {
         this.AV11userName = aP0_userName;
         this.AV10userPubKey = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_userPubKey=this.AV10userPubKey;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_userName ,
                                out string aP1_userPubKey )
      {
         execute(aP0_userName, out aP1_userPubKey, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_userName ,
                                 out string aP1_userPubKey ,
                                 out string aP2_error )
      {
         this.AV11userName = aP0_userName;
         this.AV10userPubKey = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_userPubKey=this.AV10userPubKey;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUser.gxTpr_Externaltoken)) )
         {
            new desktopappservicesrestgetuserpubkeyget(context ).execute(  AV14properties,  AV11userName,  AV9externalUser.gxTpr_Externaltoken, out  AV15getUserPubKey__getOutputOUT, out  AV12HttpMessage, out  AV13IsSuccess) ;
            if ( AV13IsSuccess )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15getUserPubKey__getOutputOUT.gxTpr_Error)) )
               {
                  AV10userPubKey = StringUtil.Trim( AV15getUserPubKey__getOutputOUT.gxTpr_Userpubkey);
               }
               else
               {
                  AV8error = AV15getUserPubKey__getOutputOUT.gxTpr_Error;
               }
            }
            else
            {
               AV8error = AV12HttpMessage.gxTpr_Description;
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
         AV10userPubKey = "";
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV14properties = new GXProperties();
         AV15getUserPubKey__getOutputOUT = new SdtgetUserPubKey__getOutput(context);
         AV12HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV11userName ;
      private string AV10userPubKey ;
      private string AV8error ;
      private bool AV13IsSuccess ;
      private GXProperties AV14properties ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtgetUserPubKey__getOutput AV15getUserPubKey__getOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV12HttpMessage ;
      private string aP1_userPubKey ;
      private string aP2_error ;
   }

}
