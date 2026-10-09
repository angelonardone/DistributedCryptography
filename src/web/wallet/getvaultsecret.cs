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
   public class getvaultsecret : GXProcedure
   {
      public getvaultsecret( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getvaultsecret( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId ,
                           string aP2_what ,
                           out string aP3_value ,
                           out string aP4_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13passwordId = aP1_passwordId;
         this.AV16what = aP2_what;
         this.AV14value = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP3_value=this.AV14value;
         aP4_error=this.AV8error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                Guid aP1_passwordId ,
                                string aP2_what ,
                                out string aP3_value )
      {
         execute(aP0_groupId, aP1_passwordId, aP2_what, out aP3_value, out aP4_error);
         return AV8error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 Guid aP1_passwordId ,
                                 string aP2_what ,
                                 out string aP3_value ,
                                 out string aP4_error )
      {
         this.AV10groupId = aP0_groupId;
         this.AV13passwordId = aP1_passwordId;
         this.AV16what = aP2_what;
         this.AV14value = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP3_value=this.AV14value;
         aP4_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14value = "";
         GXt_char1 = AV8error;
         new GeneXus.Programs.wallet.readvault(context ).execute(  AV10groupId, out  AV15vault, out  AV11isOwner, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            cleanup();
            if (true) return;
         }
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV15vault.gxTpr_Password.Count )
         {
            AV12onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV15vault.gxTpr_Password.Item(AV17GXV1));
            if ( AV12onePassword.gxTpr_Passwordid == AV13passwordId )
            {
               AV9found = true;
               if ( StringUtil.StrCmp(AV16what, "password") == 0 )
               {
                  AV14value = StringUtil.Trim( AV12onePassword.gxTpr_Password);
               }
               else if ( StringUtil.StrCmp(AV16what, "pin") == 0 )
               {
                  GXt_char1 = AV8error;
                  new GeneXus.Programs.googleauthenticator.getcurrentpin(context ).execute(  StringUtil.Trim( AV12onePassword.gxTpr_Based32key), out  AV14value, out  GXt_char1) ;
                  AV8error = GXt_char1;
               }
               else if ( StringUtil.StrCmp(AV16what, "note") == 0 )
               {
                  AV14value = AV12onePassword.gxTpr_Note;
               }
               else
               {
                  AV8error = "Unknown secret requested";
               }
               if (true) break;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         if ( ! AV9found )
         {
            AV8error = "We couldn't find that password, please refresh the page";
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
         AV14value = "";
         AV8error = "";
         AV15vault = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV12onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private string AV16what ;
      private string AV8error ;
      private string GXt_char1 ;
      private bool AV11isOwner ;
      private bool AV9found ;
      private string AV14value ;
      private Guid AV10groupId ;
      private Guid AV13passwordId ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV15vault ;
      private GeneXus.Programs.wallet.SdtPassword AV12onePassword ;
      private string aP3_value ;
      private string aP4_error ;
   }

}
