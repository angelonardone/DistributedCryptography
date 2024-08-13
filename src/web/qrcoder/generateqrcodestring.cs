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
namespace GeneXus.Programs.qrcoder {
   public class generateqrcodestring : GXProcedure
   {
      public generateqrcodestring( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public generateqrcodestring( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_textForQRcode ,
                           out string aP1_base64text )
      {
         this.AV11textForQRcode = aP0_textForQRcode;
         this.AV13base64text = "" ;
         initialize();
         ExecuteImpl();
         aP1_base64text=this.AV13base64text;
      }

      public string executeUdp( string aP0_textForQRcode )
      {
         execute(aP0_textForQRcode, out aP1_base64text);
         return AV13base64text ;
      }

      public void executeSubmit( string aP0_textForQRcode ,
                                 out string aP1_base64text )
      {
         this.AV11textForQRcode = aP0_textForQRcode;
         this.AV13base64text = "" ;
         SubmitImpl();
         aP1_base64text=this.AV13base64text;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( StringUtil.Len( AV11textForQRcode) > 1663 )
         {
            AV14error = "The maximum number of characters is 1663, and you've used " + StringUtil.Str( (decimal)(StringUtil.Len( AV11textForQRcode)), 10, 0);
         }
         else
         {
            /* User Code */
             try
            /* User Code */
             {
            /* User Code */
             string texto = AV11textForQRcode;
            /* User Code */
             QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            /* User Code */
             QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCoder.QRCodeGenerator.ECCLevel.Q);
            /* User Code */
             QRCoder.Base64QRCode qrCode = new QRCoder.Base64QRCode(qrCodeData);
            /* User Code */
             AV13base64text = qrCode.GetGraphic(20);
            /* User Code */
            	}
            /* User Code */
            	catch (Exception ex)
            /* User Code */
            	{
            /* User Code */
            		AV14error = ex.Message.ToString();
            /* User Code */
            	}
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            GX_msglist.addItem(AV14error);
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
         AV13base64text = "";
         AV14error = "";
         /* GeneXus formulas. */
      }

      private string AV14error ;
      private string AV11textForQRcode ;
      private string AV13base64text ;
      private string aP1_base64text ;
   }

}
