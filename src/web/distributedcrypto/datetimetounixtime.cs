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
namespace GeneXus.Programs.distributedcrypto {
   public class datetimetounixtime : GXProcedure
   {
      public datetimetounixtime( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public datetimetounixtime( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( DateTime aP0_dateTime ,
                           out long aP1_unixTime )
      {
         this.AV8dateTime = aP0_dateTime;
         this.AV9unixTime = 0 ;
         initialize();
         ExecuteImpl();
         aP1_unixTime=this.AV9unixTime;
      }

      public long executeUdp( DateTime aP0_dateTime )
      {
         execute(aP0_dateTime, out aP1_unixTime);
         return AV9unixTime ;
      }

      public void executeSubmit( DateTime aP0_dateTime ,
                                 out long aP1_unixTime )
      {
         this.AV8dateTime = aP0_dateTime;
         this.AV9unixTime = 0 ;
         SubmitImpl();
         aP1_unixTime=this.AV9unixTime;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          DateTime dateTime = AV8dateTime;
         /* User Code */
          AV9unixTime  = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
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
         /* GeneXus formulas. */
      }

      private long AV9unixTime ;
      private DateTime AV8dateTime ;
      private long aP1_unixTime ;
   }

}
