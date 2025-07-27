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
   public class unixtimetolocaldatetimeseconds : GXProcedure
   {
      public unixtimetolocaldatetimeseconds( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public unixtimetolocaldatetimeseconds( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( long aP0_seconds ,
                           out DateTime aP1_localDateTime )
      {
         this.AV9seconds = aP0_seconds;
         this.AV8localDateTime = DateTime.MinValue ;
         initialize();
         ExecuteImpl();
         aP1_localDateTime=this.AV8localDateTime;
      }

      public DateTime executeUdp( long aP0_seconds )
      {
         execute(aP0_seconds, out aP1_localDateTime);
         return AV8localDateTime ;
      }

      public void executeSubmit( long aP0_seconds ,
                                 out DateTime aP1_localDateTime )
      {
         this.AV9seconds = aP0_seconds;
         this.AV8localDateTime = DateTime.MinValue ;
         SubmitImpl();
         aP1_localDateTime=this.AV8localDateTime;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          long seconds = AV9seconds;
         /* User Code */
          DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(seconds);
         /* User Code */
          DateTime dateTime = dateTimeOffset.UtcDateTime.ToLocalTime();
         /* User Code */
          AV8localDateTime = dateTime;
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
         AV8localDateTime = (DateTime)(DateTime.MinValue);
         /* GeneXus formulas. */
      }

      private long AV9seconds ;
      private DateTime AV8localDateTime ;
      private DateTime aP1_localDateTime ;
   }

}
