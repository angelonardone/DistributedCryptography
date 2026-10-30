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
         if ( (Convert.ToDecimal( AV13GeneratedType ) == NumberUtil.Val( "0", ".") ) )
         {
            GXt_objcol_char2 = AV19reserved;
            new GeneXus.Programs.wallet.getreservedaddresses(context ).execute(  AV21noGroup, out  GXt_objcol_char2) ;
            AV19reserved = GXt_objcol_char2;
         }
         if ( (Convert.ToDecimal( AV13GeneratedType ) == NumberUtil.Val( "2", ".") ) )
         {
            AV20group_sdt.FromJSonString(AV22websession.Get("Group_EDIT_WALLET"), null);
            if ( ! (Guid.Empty==AV20group_sdt.gxTpr_Groupid) )
            {
               GXt_objcol_char2 = AV19reserved;
               new GeneXus.Programs.wallet.getreservedaddresses(context ).execute(  AV20group_sdt.gxTpr_Groupid, out  GXt_objcol_char2) ;
               AV19reserved = GXt_objcol_char2;
            }
         }
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV14sdt_Addresses.Count )
         {
            AV16one_sdt_address = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV14sdt_Addresses.Item(AV23GXV1));
            if ( ! AV16one_sdt_address.gxTpr_Isused && ( AV16one_sdt_address.gxTpr_Generatedtype == AV13GeneratedType ) )
            {
               AV17isReserved = false;
               AV24GXV2 = 1;
               while ( AV24GXV2 <= AV19reserved.Count )
               {
                  AV18oneReserved = AV19reserved.GetString(AV24GXV2);
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV18oneReserved), StringUtil.Trim( AV16one_sdt_address.gxTpr_Address)) == 0 )
                  {
                     AV17isReserved = true;
                     if (true) break;
                  }
                  AV24GXV2 = (int)(AV24GXV2+1);
               }
               if ( ! AV17isReserved )
               {
                  AV9oneAddress = StringUtil.Trim( AV16one_sdt_address.gxTpr_Address);
                  AV16one_sdt_address.gxTpr_Isused = true;
                  if (true) break;
               }
            }
            AV23GXV1 = (int)(AV23GXV1+1);
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
         AV19reserved = new GxSimpleCollection<string>();
         AV21noGroup = Guid.Empty;
         AV20group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV22websession = context.GetSession();
         GXt_objcol_char2 = new GxSimpleCollection<string>();
         AV16one_sdt_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV18oneReserved = "";
         /* GeneXus formulas. */
      }

      private short AV13GeneratedType ;
      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private string AV9oneAddress ;
      private string AV18oneReserved ;
      private bool AV17isReserved ;
      private Guid AV21noGroup ;
      private IGxSession AV22websession ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV14sdt_Addresses ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> GXt_objcol_SdtSDT_Addressess_SDT_AddressessItem1 ;
      private GxSimpleCollection<string> AV19reserved ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt ;
      private GxSimpleCollection<string> GXt_objcol_char2 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV16one_sdt_address ;
      private string aP1_oneAddress ;
   }

}
