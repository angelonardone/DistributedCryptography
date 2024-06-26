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
   public class hextotext : GXProcedure
   {
      public hextotext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hextotext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_textHex ,
                           out string aP1_text )
      {
         this.AV9textHex = aP0_textHex;
         this.AV8text = "" ;
         initialize();
         ExecuteImpl();
         aP1_text=this.AV8text;
      }

      public string executeUdp( string aP0_textHex )
      {
         execute(aP0_textHex, out aP1_text);
         return AV8text ;
      }

      public void executeSubmit( string aP0_textHex ,
                                 out string aP1_text )
      {
         this.AV9textHex = aP0_textHex;
         this.AV8text = "" ;
         SubmitImpl();
         aP1_text=this.AV8text;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          string hexInput = AV9textHex;
         /* User Code */
          int numberChars = hexInput.Length;
         /* User Code */
          byte[] byteArray = new byte[numberChars / 2];
         /* User Code */
          for (int i = 0; i < numberChars; i += 2)
         /* User Code */
          {
         /* User Code */
          byteArray[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
         /* User Code */
          }
         /* User Code */
          string originalString = System.Text.Encoding.UTF8.GetString(byteArray);
         /* User Code */
          AV8text = originalString;
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
         AV8text = "";
         /* GeneXus formulas. */
      }

      private string AV9textHex ;
      private string AV8text ;
      private string aP1_text ;
   }

}
