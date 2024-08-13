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
namespace GeneXus.Programs.electrum {
   public class sendmessage : GXProcedure
   {
      public sendmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_message ,
                           short aP1_messageType ,
                           out string aP2_sendResponse ,
                           out string aP3_error )
      {
         this.AV11message = aP0_message;
         this.AV13messageType = aP1_messageType;
         this.AV14sendResponse = "" ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP2_sendResponse=this.AV14sendResponse;
         aP3_error=this.AV8error;
      }

      public string executeUdp( string aP0_message ,
                                short aP1_messageType ,
                                out string aP2_sendResponse )
      {
         execute(aP0_message, aP1_messageType, out aP2_sendResponse, out aP3_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_message ,
                                 short aP1_messageType ,
                                 out string aP2_sendResponse ,
                                 out string aP3_error )
      {
         this.AV11message = aP0_message;
         this.AV13messageType = aP1_messageType;
         this.AV14sendResponse = "" ;
         this.AV8error = "" ;
         SubmitImpl();
         aP2_sendResponse=this.AV14sendResponse;
         aP3_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtConnection1 = AV16Connection;
         new GeneXus.Programs.electrum.getelectrumconnid(context ).execute( out  GXt_SdtConnection1) ;
         AV16Connection = GXt_SdtConnection1;
         if ( StringUtil.StrCmp(AV16Connection.gxTpr_Connectiontype, "tcp") == 0 )
         {
            if ( AV13messageType == 20 )
            {
               AV18OperationResult = GxInternetLibTcp.sendmessageandwaitforresponse(AV16Connection.gxTpr_Connectionid, AV11message, 5000);
               if ( ! AV18OperationResult.gxTpr_Success )
               {
                  AV8error = "error sending message: " + AV18OperationResult.gxTpr_Errormessage;
               }
               else
               {
                  AV14sendResponse = StringUtil.Trim( AV18OperationResult.gxTpr_Responsemessage);
               }
            }
            else
            {
               AV18OperationResult = GxInternetLibTcp.sendmessage(AV16Connection.gxTpr_Connectionid, AV11message);
               if ( ! AV18OperationResult.gxTpr_Success )
               {
                  AV8error = "error sending message: " + AV18OperationResult.gxTpr_Errormessage;
               }
            }
         }
         else
         {
            if ( StringUtil.StrCmp(AV16Connection.gxTpr_Connectiontype, "ws") == 0 )
            {
               if ( AV13messageType == 20 )
               {
                  AV18OperationResult = GxInternetLibWs.sendmessageandwaitforresponse(AV16Connection.gxTpr_Connectionid, AV11message, 5000);
                  if ( ! AV18OperationResult.gxTpr_Success )
                  {
                     AV8error = "error sending message: " + AV18OperationResult.gxTpr_Errormessage;
                  }
                  else
                  {
                     AV14sendResponse = StringUtil.Trim( AV18OperationResult.gxTpr_Responsemessage);
                  }
               }
               else
               {
                  AV18OperationResult = GxInternetLibWs.sendmessage(AV16Connection.gxTpr_Connectionid, AV11message);
                  if ( ! AV18OperationResult.gxTpr_Success )
                  {
                     AV8error = "error sending message: " + AV18OperationResult.gxTpr_Errormessage;
                  }
               }
            }
            else
            {
               AV8error = "We didn't recognize the Conection Type";
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
         AV14sendResponse = "";
         AV8error = "";
         AV16Connection = new GeneXus.Programs.electrum.SdtConnection(context);
         GXt_SdtConnection1 = new GeneXus.Programs.electrum.SdtConnection(context);
         AV18OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibTcp = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         /* GeneXus formulas. */
      }

      private short AV13messageType ;
      private string AV8error ;
      private string AV11message ;
      private string AV14sendResponse ;
      private GeneXus.Programs.electrum.SdtConnection AV16Connection ;
      private GeneXus.Programs.electrum.SdtConnection GXt_SdtConnection1 ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV18OperationResult ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibTcp GxInternetLibTcp ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private string aP2_sendResponse ;
      private string aP3_error ;
   }

}
