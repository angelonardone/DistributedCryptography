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
   public class buildaddressqrtext : GXProcedure
   {
      public buildaddressqrtext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public buildaddressqrtext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_address ,
                           out string aP1_qrText )
      {
         this.AV8address = aP0_address;
         this.AV10qrText = "" ;
         initialize();
         ExecuteImpl();
         aP1_qrText=this.AV10qrText;
      }

      public string executeUdp( string aP0_address )
      {
         execute(aP0_address, out aP1_qrText);
         return AV10qrText ;
      }

      public void executeSubmit( string aP0_address ,
                                 out string aP1_qrText )
      {
         this.AV8address = aP0_address;
         this.AV10qrText = "" ;
         SubmitImpl();
         aP1_qrText=this.AV10qrText;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9lowerAddress = StringUtil.Lower( StringUtil.Trim( AV8address));
         if ( StringUtil.StartsWith( AV9lowerAddress, "bc1") || StringUtil.StartsWith( AV9lowerAddress, "tb1") || StringUtil.StartsWith( AV9lowerAddress, "bcrt1") )
         {
            AV10qrText = "BITCOIN:" + StringUtil.Upper( AV9lowerAddress);
         }
         else
         {
            AV10qrText = "bitcoin:" + StringUtil.Trim( AV8address);
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
         AV10qrText = "";
         AV9lowerAddress = "";
         /* GeneXus formulas. */
      }

      private string AV8address ;
      private string AV10qrText ;
      private string AV9lowerAddress ;
      private string aP1_qrText ;
   }

}
