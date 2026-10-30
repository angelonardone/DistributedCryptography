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
   public class getaddresscontext : GXProcedure
   {
      public getaddresscontext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getaddresscontext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out string aP1_transactionFileName ,
                           out string aP2_requestsFileName ,
                           out short aP3_receivingType ,
                           out short aP4_changeType )
      {
         this.AV9groupId = aP0_groupId;
         this.AV12transactionFileName = "" ;
         this.AV11requestsFileName = "" ;
         this.AV10receivingType = 0 ;
         this.AV8changeType = 0 ;
         initialize();
         ExecuteImpl();
         aP1_transactionFileName=this.AV12transactionFileName;
         aP2_requestsFileName=this.AV11requestsFileName;
         aP3_receivingType=this.AV10receivingType;
         aP4_changeType=this.AV8changeType;
      }

      public short executeUdp( Guid aP0_groupId ,
                               out string aP1_transactionFileName ,
                               out string aP2_requestsFileName ,
                               out short aP3_receivingType )
      {
         execute(aP0_groupId, out aP1_transactionFileName, out aP2_requestsFileName, out aP3_receivingType, out aP4_changeType);
         return AV8changeType ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out string aP1_transactionFileName ,
                                 out string aP2_requestsFileName ,
                                 out short aP3_receivingType ,
                                 out short aP4_changeType )
      {
         this.AV9groupId = aP0_groupId;
         this.AV12transactionFileName = "" ;
         this.AV11requestsFileName = "" ;
         this.AV10receivingType = 0 ;
         this.AV8changeType = 0 ;
         SubmitImpl();
         aP1_transactionFileName=this.AV12transactionFileName;
         aP2_requestsFileName=this.AV11requestsFileName;
         aP3_receivingType=this.AV10receivingType;
         aP4_changeType=this.AV8changeType;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( (Guid.Empty==AV9groupId) )
         {
            AV12transactionFileName = "transactions.trn";
            AV11requestsFileName = "paymentrequests.enc";
            AV10receivingType = (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven));
            AV8changeType = (short)(Math.Round(NumberUtil.Val( "1", "."), 18, MidpointRounding.ToEven));
         }
         else
         {
            AV12transactionFileName = StringUtil.Trim( AV9groupId.ToString()) + ".gtrn";
            AV11requestsFileName = StringUtil.Trim( AV9groupId.ToString()) + ".requests";
            AV10receivingType = (short)(Math.Round(NumberUtil.Val( "2", "."), 18, MidpointRounding.ToEven));
            AV8changeType = (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven));
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
         AV12transactionFileName = "";
         AV11requestsFileName = "";
         /* GeneXus formulas. */
      }

      private short AV10receivingType ;
      private short AV8changeType ;
      private string AV12transactionFileName ;
      private string AV11requestsFileName ;
      private Guid AV9groupId ;
      private string aP1_transactionFileName ;
      private string aP2_requestsFileName ;
      private short aP3_receivingType ;
      private short aP4_changeType ;
   }

}
