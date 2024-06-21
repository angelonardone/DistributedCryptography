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
namespace GeneXus.Programs.wallet {
   public class doesaddressbelongtowallet : GXProcedure
   {
      public doesaddressbelongtowallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public doesaddressbelongtowallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_oneAddress ,
                           short aP1_GeneratedType ,
                           ref bool aP2_isUsed ,
                           out bool aP3_doesBelong )
      {
         this.AV9oneAddress = aP0_oneAddress;
         this.AV8GeneratedType = aP1_GeneratedType;
         this.AV12isUsed = aP2_isUsed;
         this.AV13doesBelong = false ;
         initialize();
         ExecuteImpl();
         aP2_isUsed=this.AV12isUsed;
         aP3_doesBelong=this.AV13doesBelong;
      }

      public bool executeUdp( string aP0_oneAddress ,
                              short aP1_GeneratedType ,
                              ref bool aP2_isUsed )
      {
         execute(aP0_oneAddress, aP1_GeneratedType, ref aP2_isUsed, out aP3_doesBelong);
         return AV13doesBelong ;
      }

      public void executeSubmit( string aP0_oneAddress ,
                                 short aP1_GeneratedType ,
                                 ref bool aP2_isUsed ,
                                 out bool aP3_doesBelong )
      {
         this.AV9oneAddress = aP0_oneAddress;
         this.AV8GeneratedType = aP1_GeneratedType;
         this.AV12isUsed = aP2_isUsed;
         this.AV13doesBelong = false ;
         SubmitImpl();
         aP2_isUsed=this.AV12isUsed;
         aP3_doesBelong=this.AV13doesBelong;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = AV10sdt_Addresses;
         new GeneXus.Programs.wallet.getalladdress(context ).execute( out  GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1) ;
         AV10sdt_Addresses = GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1;
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV10sdt_Addresses.Count )
         {
            AV11one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV10sdt_Addresses.Item(AV14GXV1));
            if ( ( StringUtil.StrCmp(StringUtil.Trim( AV9oneAddress), StringUtil.Trim( AV11one_sdt_address.gxTpr_Address)) == 0 ) && ( AV11one_sdt_address.gxTpr_Generatedtype == AV8GeneratedType ) )
            {
               AV11one_sdt_address.gxTpr_Isused = AV12isUsed;
               AV13doesBelong = true;
               if (true) break;
            }
            AV14GXV1 = (int)(AV14GXV1+1);
         }
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
         AV10sdt_Addresses = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV11one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         /* GeneXus formulas. */
      }

      private short AV8GeneratedType ;
      private int AV14GXV1 ;
      private string AV9oneAddress ;
      private bool AV12isUsed ;
      private bool AV13doesBelong ;
      private bool aP2_isUsed ;
      private bool aP3_doesBelong ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV10sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV11one_sdt_address ;
   }

}
