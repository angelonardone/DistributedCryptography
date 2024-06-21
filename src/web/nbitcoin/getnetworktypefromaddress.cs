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
namespace GeneXus.Programs.nbitcoin {
   public class getnetworktypefromaddress : GXProcedure
   {
      public getnetworktypefromaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getnetworktypefromaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           out string aP1_networkType )
      {
         this.AV9address = aP0_address;
         this.AV8networkType = "" ;
         initialize();
         ExecuteImpl();
         aP1_networkType=this.AV8networkType;
      }

      public string executeUdp( string aP0_address )
      {
         execute(aP0_address, out aP1_networkType);
         return AV8networkType ;
      }

      public void executeSubmit( string aP0_address ,
                                 out string aP1_networkType )
      {
         this.AV9address = aP0_address;
         this.AV8networkType = "" ;
         SubmitImpl();
         aP1_networkType=this.AV8networkType;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( StringUtil.StartsWith( AV9address, "bc1") )
         {
            AV8networkType = "Main";
         }
         else if ( StringUtil.StartsWith( AV9address, "tb1") )
         {
            AV8networkType = "TestNet";
         }
         else if ( StringUtil.StartsWith( AV9address, "bcrt1") )
         {
            AV8networkType = "RegTest";
         }
         else
         {
            AV8networkType = "Main";
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
         AV8networkType = "";
         /* GeneXus formulas. */
      }

      private string AV9address ;
      private string AV8networkType ;
      private string aP1_networkType ;
   }

}
