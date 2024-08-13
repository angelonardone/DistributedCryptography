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
namespace GeneXus.Programs.nostr {
   public class startconnection : GXProcedure
   {
      public startconnection( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public startconnection( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV9error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV9error = "" ;
         SubmitImpl();
         aP0_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_guid1 = AV10key;
         new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid1) ;
         AV10key = GXt_guid1;
         if ( (Guid.Empty==AV10key) )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.nostr.connect(context ).execute( out  GXt_char2) ;
            AV9error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_char2 = AV9error;
               new GeneXus.Programs.nostr.startsubscription(context ).execute( out  GXt_char2) ;
               AV9error = GXt_char2;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV9error = "Subscribe error: " + AV9error;
                  AV10key = Guid.Empty;
                  new GeneXus.Programs.nostr.settnostrconnectionid(context ).execute(  AV10key) ;
               }
            }
            else
            {
               AV9error = "Connect error: " + AV9error;
               AV10key = Guid.Empty;
               new GeneXus.Programs.nostr.settnostrconnectionid(context ).execute(  AV10key) ;
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
         AV10key = Guid.Empty;
         GXt_guid1 = Guid.Empty;
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char2 ;
      private Guid AV10key ;
      private Guid GXt_guid1 ;
      private string aP0_error ;
   }

}
