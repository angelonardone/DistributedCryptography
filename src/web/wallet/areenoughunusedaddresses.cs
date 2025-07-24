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
         this.AV11count = 0 ;
         initialize();
         ExecuteImpl();
         aP1_count=this.AV11count;
      }

      public short executeUdp( short aP0_GeneratedType )
      {
         execute(aP0_GeneratedType, out aP1_count);
         return AV11count ;
      }

      public void executeSubmit( short aP0_GeneratedType ,
                                 out short aP1_count )
      {
         this.AV8GeneratedType = aP0_GeneratedType;
         this.AV11count = 0 ;
         SubmitImpl();
         aP1_count=this.AV11count;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV10sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV10sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV11count = 0;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV10sdt_Addresses.Count )
         {
            AV9one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV10sdt_Addresses.Item(AV12GXV1));
            if ( ! AV9one_sdt_address.gxTpr_Isused && ( AV9one_sdt_address.gxTpr_Generatedtype == AV8GeneratedType ) )
            {
               AV11count = (short)(AV11count+1);
            }
            AV12GXV1 = (int)(AV12GXV1+1);
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
         AV9one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private short AV8GeneratedType ;
      private short AV11count ;
      private int AV12GXV1 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV10sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV9one_sdt_address ;
      private short aP1_count ;
   }

}
