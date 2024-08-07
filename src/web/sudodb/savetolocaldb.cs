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
namespace GeneXus.Programs.sudodb {
   public class savetolocaldb : GXProcedure
   {
      public savetolocaldb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savetolocaldb( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                           out string aP1_error )
      {
         this.AV8electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId )
      {
         execute(aP0_electrumRespGetTransactionId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId aP0_electrumRespGetTransactionId ,
                                 out string aP1_error )
      {
         this.AV8electrumRespGetTransactionId = aP0_electrumRespGetTransactionId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV9error;
         new GeneXus.Programs.sudodb.savetransaction(context ).execute(  AV8electrumRespGetTransactionId, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.sudodb.savevouts(context ).execute(  AV8electrumRespGetTransactionId, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_char1 = AV9error;
               new GeneXus.Programs.sudodb.savevins(context ).execute(  AV8electrumRespGetTransactionId, out  GXt_char1) ;
               AV9error = GXt_char1;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV9error = "vINs " + AV9error;
               }
            }
            else
            {
               AV9error = "vOUTs " + AV9error;
            }
         }
         else
         {
            AV9error = "Transactions " + AV9error;
         }
         this.cleanup();
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
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char1 ;
      private string aP1_error ;
      private GeneXus.Programs.electrum.SdtelectrumRespGetTransactionId AV8electrumRespGetTransactionId ;
   }

}
