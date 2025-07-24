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
namespace GeneXus.Programs.nbitcoin {
   public class deriveoneaddressfromextpubkey : GXProcedure
   {
      public deriveoneaddressfromextpubkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public deriveoneaddressfromextpubkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_deserializedExtPubKey ,
                           long aP1_base ,
                           long aP2_sequence ,
                           out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP3_one_address ,
                           out string aP4_error )
      {
         this.AV12deserializedExtPubKey = aP0_deserializedExtPubKey;
         this.AV9base = aP1_base;
         this.AV22sequence = aP2_sequence;
         this.AV19one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP3_one_address=this.AV19one_address;
         aP4_error=this.AV14error;
      }

      public string executeUdp( string aP0_deserializedExtPubKey ,
                                long aP1_base ,
                                long aP2_sequence ,
                                out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP3_one_address )
      {
         execute(aP0_deserializedExtPubKey, aP1_base, aP2_sequence, out aP3_one_address, out aP4_error);
         return AV14error ;
      }

      public void executeSubmit( string aP0_deserializedExtPubKey ,
                                 long aP1_base ,
                                 long aP2_sequence ,
                                 out GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP3_one_address ,
                                 out string aP4_error )
      {
         this.AV12deserializedExtPubKey = aP0_deserializedExtPubKey;
         this.AV9base = aP1_base;
         this.AV22sequence = aP2_sequence;
         this.AV19one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context) ;
         this.AV14error = "" ;
         SubmitImpl();
         aP3_one_address=this.AV19one_address;
         aP4_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV14error;
         new GeneXus.Programs.nbitcoin.parse_serialized_extended_key(context ).execute(  AV12deserializedExtPubKey, out  AV16extendedPublicKey, out  AV18networkType, out  AV15extendedKeyType, out  GXt_char1) ;
         AV14error = GXt_char1;
         AV10base_char = "/" + StringUtil.Trim( StringUtil.Str( (decimal)(AV9base), 10, 0)) + "/";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            GXt_char1 = AV14error;
            new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV16extendedPublicKey,  AV18networkType,  AV10base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV22sequence), 10, 0)), out  AV17extPubKeyInfo, out  GXt_char1) ;
            AV14error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
            {
               AV19one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
               if ( StringUtil.StrCmp(AV15extendedKeyType, "x") == 0 )
               {
                  AV19one_address.gxTpr_Address = AV17extPubKeyInfo.gxTpr_Addresslegacy;
               }
               else if ( StringUtil.StrCmp(AV15extendedKeyType, "y") == 0 )
               {
                  AV19one_address.gxTpr_Address = AV17extPubKeyInfo.gxTpr_Addresssegwitp2sh;
               }
               else if ( StringUtil.StrCmp(AV15extendedKeyType, "z") == 0 )
               {
                  AV19one_address.gxTpr_Address = AV17extPubKeyInfo.gxTpr_Addresssegwit;
               }
               else if ( StringUtil.StrCmp(AV15extendedKeyType, "t") == 0 )
               {
                  AV19one_address.gxTpr_Address = AV17extPubKeyInfo.gxTpr_Addresstaproot;
               }
               else
               {
                  AV14error = "Extended type not found";
               }
               AV19one_address.gxTpr_Creationsequence = AV22sequence;
               AV19one_address.gxTpr_Generatedtype = (short)(AV9base);
               AV19one_address.gxTpr_Isused = false;
            }
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
         AV19one_address = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         AV14error = "";
         AV16extendedPublicKey = "";
         AV18networkType = "";
         AV15extendedKeyType = "";
         AV10base_char = "";
         GXt_char1 = "";
         AV17extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         /* GeneXus formulas. */
      }

      private long AV9base ;
      private long AV22sequence ;
      private string AV12deserializedExtPubKey ;
      private string AV14error ;
      private string AV16extendedPublicKey ;
      private string AV18networkType ;
      private string AV15extendedKeyType ;
      private string AV10base_char ;
      private string GXt_char1 ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV19one_address ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV17extPubKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem aP3_one_address ;
      private string aP4_error ;
   }

}
