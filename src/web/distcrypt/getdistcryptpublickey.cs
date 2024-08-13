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
namespace GeneXus.Programs.distcrypt {
   public class getdistcryptpublickey : GXProcedure
   {
      public getdistcryptpublickey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getdistcryptpublickey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_PublicKey )
      {
         this.AV8PublicKey = "" ;
         initialize();
         ExecuteImpl();
         aP0_PublicKey=this.AV8PublicKey;
      }

      public string executeUdp( )
      {
         execute(out aP0_PublicKey);
         return AV8PublicKey ;
      }

      public void executeSubmit( out string aP0_PublicKey )
      {
         this.AV8PublicKey = "" ;
         SubmitImpl();
         aP0_PublicKey=this.AV8PublicKey;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8PublicKey = "024684c047eed7b4e5e7a2f91b9886eeea2975062cc46c94680de427c9f5f0996d";
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
         AV8PublicKey = "";
         /* GeneXus formulas. */
      }

      private string AV8PublicKey ;
      private string aP0_PublicKey ;
   }

}
