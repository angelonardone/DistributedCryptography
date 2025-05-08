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
   public class texttohex : GXProcedure
   {
      public texttohex( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public texttohex( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_text ,
                           out string aP1_textHex )
      {
         this.AV8text = aP0_text;
         this.AV9textHex = "" ;
         initialize();
         ExecuteImpl();
         aP1_textHex=this.AV9textHex;
      }

      public string executeUdp( string aP0_text )
      {
         execute(aP0_text, out aP1_textHex);
         return AV9textHex ;
      }

      public void executeSubmit( string aP0_text ,
                                 out string aP1_textHex )
      {
         this.AV8text = aP0_text;
         this.AV9textHex = "" ;
         SubmitImpl();
         aP1_textHex=this.AV9textHex;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          string input = AV8text;
         /* User Code */
          byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(input);
         /* User Code */
          System.Text.StringBuilder hexString = new System.Text.StringBuilder(byteArray.Length * 2);
         /* User Code */
          foreach (byte b in byteArray)
         /* User Code */
          {
         /* User Code */
          hexString.AppendFormat("{0:X2}", b);
         /* User Code */
          }
         /* User Code */
          AV9textHex = hexString.ToString();
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
         AV9textHex = "";
         /* GeneXus formulas. */
      }

      private string AV8text ;
      private string AV9textHex ;
      private string aP1_textHex ;
   }

}
