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
   public class hextobase32 : GXProcedure
   {
      public hextobase32( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hextobase32( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_hexString ,
                           out string aP1_base32String ,
                           out string aP2_error )
      {
         this.AV8hexString = aP0_hexString;
         this.AV9base32String = "" ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_base32String=this.AV9base32String;
         aP2_error=this.AV10error;
      }

      public string executeUdp( string aP0_hexString ,
                                out string aP1_base32String )
      {
         execute(aP0_hexString, out aP1_base32String, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( string aP0_hexString ,
                                 out string aP1_base32String ,
                                 out string aP2_error )
      {
         this.AV8hexString = aP0_hexString;
         this.AV9base32String = "" ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_base32String=this.AV9base32String;
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* User Code */
          try
         /* User Code */
          {
         /* User Code */
          string hexString = AV8hexString;
         /* User Code */
          var tempBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(hexString);
         /* User Code */
          var base32string = NBitcoin.DataEncoders.Encoders.Base32.EncodeData(tempBytes);
         /* User Code */
          AV9base32String = base32string;
         /* User Code */
         	}
         /* User Code */
         	catch (Exception ex)
         /* User Code */
         	{
         /* User Code */
         		AV10error = ex.Message.ToString();
         /* User Code */
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
         AV9base32String = "";
         AV10error = "";
         /* GeneXus formulas. */
      }

      private string AV8hexString ;
      private string AV9base32String ;
      private string AV10error ;
      private string aP1_base32String ;
      private string aP2_error ;
   }

}
