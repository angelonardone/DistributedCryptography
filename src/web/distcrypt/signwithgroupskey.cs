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
   public class signwithgroupskey : GXProcedure
   {
      public signwithgroupskey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signwithgroupskey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_message ,
                           out string aP1_signature ,
                           out string aP2_error )
      {
         this.AV10message = aP0_message;
         this.AV11signature = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_signature=this.AV11signature;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_message ,
                                out string aP1_signature )
      {
         execute(aP0_message, out aP1_signature, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_message ,
                                 out string aP1_signature ,
                                 out string aP2_error )
      {
         this.AV10message = aP0_message;
         this.AV11signature = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_signature=this.AV11signature;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         GXt_char2 = AV8error;
         new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV9externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  AV10message, out  AV11signature, out  GXt_char2) ;
         AV8error = GXt_char2;
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
         AV11signature = "";
         AV8error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV11signature ;
      private string AV8error ;
      private string GXt_char2 ;
      private string AV10message ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private string aP1_signature ;
      private string aP2_error ;
   }

}
