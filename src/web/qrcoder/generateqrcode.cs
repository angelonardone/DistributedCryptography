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
   public class generateqrcode : GXProcedure
   {
      public generateqrcode( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public generateqrcode( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_textForQRcode ,
                           out string aP1_url )
      {
         this.AV11textForQRcode = aP0_textForQRcode;
         this.AV12url = "" ;
         initialize();
         ExecuteImpl();
         aP1_url=this.AV12url;
      }

      public string executeUdp( string aP0_textForQRcode )
      {
         execute(aP0_textForQRcode, out aP1_url);
         return AV12url ;
      }

      public void executeSubmit( string aP0_textForQRcode ,
                                 out string aP1_url )
      {
         this.AV11textForQRcode = aP0_textForQRcode;
         this.AV12url = "" ;
         SubmitImpl();
         aP1_url=this.AV12url;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
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
         AV10tempGUID = Guid.NewGuid( );
         AV8file.Source = "PublicTempStorage\\"+AV10tempGUID.ToString()+".png";
         AV8file.FromBase64(AV13base64text);
         AV8file.Create();
         AV9imageBlob = AV8file.GetAbsoluteName();
         AV12url = context.PathToUrl( AV9imageBlob);
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
         AV12url = "";
         AV13base64text = "";
         AV10tempGUID = Guid.Empty;
         AV8file = new GxFile(context.GetPhysicalPath());
         AV9imageBlob = "";
         /* GeneXus formulas. */
      }

      private string AV11textForQRcode ;
      private string AV13base64text ;
      private string AV12url ;
      private Guid AV10tempGUID ;
      private string AV9imageBlob ;
      private string aP1_url ;
      private GxFile AV8file ;
   }

}
