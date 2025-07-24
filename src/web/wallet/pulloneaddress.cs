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
   public class pulloneaddress : GXProcedure
   {
      public pulloneaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public pulloneaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_GeneratedType ,
                           out string aP1_oneAddress )
      {
         this.AV13GeneratedType = aP0_GeneratedType;
         this.AV9oneAddress = "" ;
         initialize();
         ExecuteImpl();
         aP1_oneAddress=this.AV9oneAddress;
      }

      public string executeUdp( short aP0_GeneratedType )
      {
         execute(aP0_GeneratedType, out aP1_oneAddress);
         return AV9oneAddress ;
      }

      public void executeSubmit( short aP0_GeneratedType ,
                                 out string aP1_oneAddress )
      {
         this.AV13GeneratedType = aP0_GeneratedType;
         this.AV9oneAddress = "" ;
         SubmitImpl();
         aP1_oneAddress=this.AV9oneAddress;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV14sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV14sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV14sdt_Addresses.Count )
         {
            AV16one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV14sdt_Addresses.Item(AV17GXV1));
            if ( ! AV16one_sdt_address.gxTpr_Isused && ( AV16one_sdt_address.gxTpr_Generatedtype == AV13GeneratedType ) )
            {
               AV9oneAddress = StringUtil.Trim( AV16one_sdt_address.gxTpr_Address);
               AV16one_sdt_address.gxTpr_Isused = true;
               if (true) break;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV14sdt_Addresses) ;
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
         AV9oneAddress = "";
         AV14sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV16one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private short AV13GeneratedType ;
      private int AV17GXV1 ;
      private string AV9oneAddress ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV14sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV16one_sdt_address ;
      private string aP1_oneAddress ;
   }

}
