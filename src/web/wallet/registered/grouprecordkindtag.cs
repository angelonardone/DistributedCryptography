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
   public class grouprecordkindtag : GXProcedure
   {
      public grouprecordkindtag( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public grouprecordkindtag( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_groupKey ,
                           string aP1_kind ,
                           out string aP2_kindTag ,
                           out string aP3_error )
      {
         this.AV9groupKey = aP0_groupKey;
         this.AV10kind = aP1_kind;
         this.AV11kindTag = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_kindTag=this.AV11kindTag;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_groupKey ,
                                string aP1_kind ,
                                out string aP2_kindTag )
      {
         execute(aP0_groupKey, aP1_kind, out aP2_kindTag, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_groupKey ,
                                 string aP1_kind ,
                                 out string aP2_kindTag ,
                                 out string aP3_error )
      {
         this.AV9groupKey = aP0_groupKey;
         this.AV10kind = aP1_kind;
         this.AV11kindTag = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_kindTag=this.AV11kindTag;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.registered.hmacsha256hex(context ).execute(  AV9groupKey,  "DistributedCryptography group record kind\\n"+StringUtil.Trim( AV10kind), out  AV11kindTag, out  AV8error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) && ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV9groupKey))) || ( StringUtil.Len( StringUtil.Trim( AV11kindTag)) != 64 ) ) )
         {
            AV8error = "The kind of the shared record could not be computed";
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
         AV11kindTag = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV9groupKey ;
      private string AV8error ;
      private string AV10kind ;
      private string AV11kindTag ;
      private string aP2_kindTag ;
      private string aP3_error ;
   }

}
