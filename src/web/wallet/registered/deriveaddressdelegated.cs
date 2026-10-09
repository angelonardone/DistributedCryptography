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
namespace GeneXus.Programs.wallet.registered {
   public class deriveaddressdelegated : GXProcedure
   {
      public deriveaddressdelegated( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveaddressdelegated( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                           string aP1_networkType ,
                           long aP2_sequence ,
                           bool aP3_isChange ,
                           out GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult aP4_result )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV13networkType = aP1_networkType;
         this.AV16sequence = aP2_sequence;
         this.AV11isChange = aP3_isChange;
         this.AV15result = new GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult(context) ;
         initialize();
         ExecuteImpl();
         aP4_result=this.AV15result;
      }

      public GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult executeUdp( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                                                                      string aP1_networkType ,
                                                                                      long aP2_sequence ,
                                                                                      bool aP3_isChange )
      {
         execute(aP0_group_sdt, aP1_networkType, aP2_sequence, aP3_isChange, out aP4_result);
         return AV15result ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtGroup_SDT aP0_group_sdt ,
                                 string aP1_networkType ,
                                 long aP2_sequence ,
                                 bool aP3_isChange ,
                                 out GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult aP4_result )
      {
         this.AV9group_sdt = aP0_group_sdt;
         this.AV13networkType = aP1_networkType;
         this.AV16sequence = aP2_sequence;
         this.AV11isChange = aP3_isChange;
         this.AV15result = new GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult(context) ;
         SubmitImpl();
         aP4_result=this.AV15result;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new GeneXus.Programs.wallet.registered.mapgrouptodelegatedeo(context ).execute(  AV9group_sdt, out  AV10groupEO) ;
         AV14ok = AV8delegatedProcessor.fromsdt(AV10groupEO);
         if ( ! AV14ok )
         {
            AV15result.gxTpr_Success = false;
            AV15result.gxTpr_Error = "FromSDT failed (null group)";
            cleanup();
            if (true) return;
         }
         AV12jsonText = AV8delegatedProcessor.createaddress((int)(AV16sequence), AV11isChange, AV13networkType);
         AV15result.FromJSonString(AV12jsonText, null);
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
         AV15result = new GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult(context);
         AV10groupEO = new GeneXus.Programs.distributedcryptographylib.SdtGroupSDT(context);
         AV8delegatedProcessor = new GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor(context);
         AV12jsonText = "";
         /* GeneXus formulas. */
      }

      private long AV16sequence ;
      private string AV13networkType ;
      private bool AV11isChange ;
      private bool AV14ok ;
      private string AV12jsonText ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV9group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult AV15result ;
      private GeneXus.Programs.distributedcryptographylib.SdtGroupSDT AV10groupEO ;
      private GeneXus.Programs.distributedcryptographylib.SdtDelegatedProcessor AV8delegatedProcessor ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedAddressResult aP4_result ;
   }

}
