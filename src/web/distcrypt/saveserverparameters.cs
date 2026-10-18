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
   public class saveserverparameters : GXProcedure
   {
      public saveserverparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public saveserverparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters ,
                           out bool aP1_loggedOut ,
                           out string aP2_error )
      {
         this.AV13ServerParameters = aP0_ServerParameters;
         this.AV11loggedOut = false ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_loggedOut=this.AV11loggedOut;
         aP2_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters ,
                                out bool aP1_loggedOut )
      {
         execute(aP0_ServerParameters, out aP1_loggedOut, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.distcrypt.SdtServerParameters aP0_ServerParameters ,
                                 out bool aP1_loggedOut ,
                                 out string aP2_error )
      {
         this.AV13ServerParameters = aP0_ServerParameters;
         this.AV11loggedOut = false ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_loggedOut=this.AV11loggedOut;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         AV11loggedOut = false;
         if ( ! new GeneXus.Programs.wallet.iswalletopen(context).executeUdp( ) )
         {
            AV9error = "Open a wallet first";
         }
         else
         {
            AV14toSave = (GeneXus.Programs.distcrypt.SdtServerParameters)(AV13ServerParameters.Clone());
            AV14toSave.gxTpr_Serverhost = StringUtil.Trim( AV13ServerParameters.gxTpr_Serverhost);
            if ( ! GxRegex.IsMatch(StringUtil.Trim( AV14toSave.gxTpr_Serverhost),"^[A-Za-z0-9._-]+$") )
            {
               AV9error = "Type the host name of the server, without http:// (letters, numbers, dots and hyphens)";
            }
            else if ( ( AV14toSave.gxTpr_Serverport < 1 ) || ( AV14toSave.gxTpr_Serverport > 65535 ) )
            {
               AV9error = "The port of the server must be between 1 and 65535";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_SdtServerParameters1 = AV12previous;
               new GeneXus.Programs.distcrypt.getserverparameters(context ).execute( out  GXt_SdtServerParameters1) ;
               AV12previous = GXt_SdtServerParameters1;
               GXt_char2 = AV9error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "server.conf",  AV14toSave.ToJSonString(false, true), out  GXt_char2) ;
               AV9error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV15websession.Set("DistCryptServerParameters", AV14toSave.ToJSonString(false, true));
                  if ( ( StringUtil.StrCmp(StringUtil.Lower( StringUtil.Trim( AV12previous.gxTpr_Serverhost)), StringUtil.Lower( AV14toSave.gxTpr_Serverhost)) != 0 ) || ( AV12previous.gxTpr_Serverport != AV14toSave.gxTpr_Serverport ) || ( AV12previous.gxTpr_Serversecure != AV14toSave.gxTpr_Serversecure ) )
                  {
                     GXt_SdtExternalUser3 = AV10externalUser;
                     new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
                     AV10externalUser = GXt_SdtExternalUser3;
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10externalUser.gxTpr_Externaltoken)) )
                     {
                        GXt_char2 = AV8auxError;
                        new GeneXus.Programs.distcrypt.logoutexternaluser(context ).execute( out  GXt_char2) ;
                        AV8auxError = GXt_char2;
                        AV11loggedOut = true;
                     }
                  }
               }
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
         AV9error = "";
         AV14toSave = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV12previous = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         GXt_SdtServerParameters1 = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV15websession = context.GetSession();
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser3 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV8auxError = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string AV8auxError ;
      private string GXt_char2 ;
      private bool AV11loggedOut ;
      private IGxSession AV15websession ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV13ServerParameters ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV14toSave ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV12previous ;
      private GeneXus.Programs.distcrypt.SdtServerParameters GXt_SdtServerParameters1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser3 ;
      private bool aP1_loggedOut ;
      private string aP2_error ;
   }

}
