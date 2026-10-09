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
   public class areenoughunusedaddresses : GXProcedure
   {
      public areenoughunusedaddresses( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public areenoughunusedaddresses( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_GeneratedType ,
                           out short aP1_count )
      {
         this.AV8GeneratedType = aP0_GeneratedType;
         this.AV12count = 0 ;
         initialize();
         ExecuteImpl();
         aP1_count=this.AV12count;
      }

      public short executeUdp( short aP0_GeneratedType )
      {
         execute(aP0_GeneratedType, out aP1_count);
         return AV12count ;
      }

      public void executeSubmit( short aP0_GeneratedType ,
                                 out short aP1_count )
      {
         this.AV8GeneratedType = aP0_GeneratedType;
         this.AV12count = 0 ;
         SubmitImpl();
         aP1_count=this.AV12count;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV10sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV10sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV12count = 0;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV10sdt_Addresses.Count )
         {
            AV11one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV10sdt_Addresses.Item(AV13GXV1));
            if ( ! AV11one_sdt_address.gxTpr_Isused && ( AV11one_sdt_address.gxTpr_Generatedtype == AV8GeneratedType ) )
            {
               AV12count = (short)(AV12count+1);
            }
            AV13GXV1 = (int)(AV13GXV1+1);
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
         AV10sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV11one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private short AV8GeneratedType ;
      private short AV12count ;
      private int AV13GXV1 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV10sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV11one_sdt_address ;
      private short aP1_count ;
   }

}
