using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
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
namespace GeneXus.Programs {
   public class receivefromelectrum : GXProcedure
   {
      public receivefromelectrum( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
      }

      public receivefromelectrum( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_key ,
                           string aP1_topic ,
                           string aP2_message ,
                           DateTime aP3_datetime )
      {
         this.AV2key = aP0_key;
         this.AV3topic = aP1_topic;
         this.AV4message = aP2_message;
         this.AV5datetime = aP3_datetime;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_key ,
                                 string aP1_topic ,
                                 string aP2_message ,
                                 DateTime aP3_datetime )
      {
         this.AV2key = aP0_key;
         this.AV3topic = aP1_topic;
         this.AV4message = aP2_message;
         this.AV5datetime = aP3_datetime;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         args = new Object[] {(Guid)AV2key,(string)AV3topic,(string)AV4message,(DateTime)AV5datetime} ;
         ClassLoader.Execute("areceivefromelectrum","GeneXus.Programs","areceivefromelectrum", new Object[] {context }, "execute", args);
         if ( ( args != null ) && ( args.Length == 4 ) )
         {
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
      }

      public override void initialize( )
      {
         /* GeneXus formulas. */
      }

      private string AV3topic ;
      private DateTime AV5datetime ;
      private string AV4message ;
      private Guid AV2key ;
      private IGxDataStore dsDefault ;
      private Object[] args ;
   }

}
