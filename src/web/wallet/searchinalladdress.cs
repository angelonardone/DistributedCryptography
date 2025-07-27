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
   public class searchinalladdress : GXProcedure
   {
      public searchinalladdress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public searchinalladdress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_oneAddress ,
                           out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP1_one_sdt_address )
      {
         this.AV9oneAddress = aP0_oneAddress;
         this.AV11one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         initialize();
         ExecuteImpl();
         aP1_one_sdt_address=this.AV11one_sdt_address;
      }

      public GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem executeUdp( string aP0_oneAddress )
      {
         execute(aP0_oneAddress, out aP1_one_sdt_address);
         return AV11one_sdt_address ;
      }

      public void executeSubmit( string aP0_oneAddress ,
                                 out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP1_one_sdt_address )
      {
         this.AV9oneAddress = aP0_oneAddress;
         this.AV11one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         SubmitImpl();
         aP1_one_sdt_address=this.AV11one_sdt_address;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV10sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV10sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV12GXV1 = 1;
         while ( AV12GXV1 <= AV10sdt_Addresses.Count )
         {
            AV11one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV10sdt_Addresses.Item(AV12GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV11one_sdt_address.gxTpr_Address), StringUtil.Trim( AV9oneAddress)) == 0 )
            {
               if (true) break;
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
         AV11one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV10sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         /* GeneXus formulas. */
      }

      private int AV12GXV1 ;
      private string AV9oneAddress ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV11one_sdt_address ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV10sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP1_one_sdt_address ;
   }

}
