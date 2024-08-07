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
   public class reversehexstring : GXProcedure
   {
      public reversehexstring( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public reversehexstring( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_source ,
                           out string aP1_dest ,
                           out string aP2_error )
      {
         this.AV10source = aP0_source;
         this.AV8dest = "" ;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_dest=this.AV8dest;
         aP2_error=this.AV9error;
      }

      public string executeUdp( string aP0_source ,
                                out string aP1_dest )
      {
         execute(aP0_source, out aP1_dest, out aP2_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_source ,
                                 out string aP1_dest ,
                                 out string aP2_error )
      {
         this.AV10source = aP0_source;
         this.AV8dest = "" ;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_dest=this.AV8dest;
         aP2_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10source)) )
         {
            AV9error = "The input string cannot be empty";
         }
         else
         {
            AV11Length = (short)(StringUtil.Len( AV10source));
            if ( ((int)((AV11Length) % (2))) > 0 )
            {
               AV9error = "The input string length must be even.";
            }
            else
            {
               AV13i = 1;
               while ( AV13i <= AV11Length )
               {
                  AV12ReversedHexString = StringUtil.Substring( AV10source, AV13i, 2) + AV12ReversedHexString;
                  AV13i = (short)(AV13i+2);
               }
               AV8dest = StringUtil.Trim( AV12ReversedHexString);
            }
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
         AV8dest = "";
         AV9error = "";
         AV12ReversedHexString = "";
         /* GeneXus formulas. */
      }

      private short AV11Length ;
      private short AV13i ;
      private string AV10source ;
      private string AV8dest ;
      private string AV9error ;
      private string AV12ReversedHexString ;
      private string aP1_dest ;
      private string aP2_error ;
   }

}
