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
   public class markinalladdressasused : GXProcedure
   {
      public markinalladdressasused( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public markinalladdressasused( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_oneAddress )
      {
         this.AV9oneAddress = aP0_oneAddress;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_oneAddress )
      {
         this.AV9oneAddress = aP0_oneAddress;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV10sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV10sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV11GXV1 = 1;
         while ( AV11GXV1 <= AV10sdt_Addresses.Count )
         {
            AV8one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV10sdt_Addresses.Item(AV11GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV8one_sdt_address.gxTpr_Address), StringUtil.Trim( AV9oneAddress)) == 0 )
            {
               AV8one_sdt_address.gxTpr_Isused = true;
               if (true) break;
            }
            AV11GXV1 = (int)(AV11GXV1+1);
         }
         new GeneXus.Programs.wallet.setalladdress(context ).execute(  AV10sdt_Addresses) ;
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
         AV8one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private int AV11GXV1 ;
      private string AV9oneAddress ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV10sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV8one_sdt_address ;
   }

}
