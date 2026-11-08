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
   public class rubykeyinfofromextkey : GXProcedure
   {
      public rubykeyinfofromextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public rubykeyinfofromextkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_serializedExtKeyIn ,
                           out GeneXus.Programs.nbitcoin.SdtKeyInfo aP1_keyInfo ,
                           out string aP2_error )
      {
         this.AV16serializedExtKeyIn = aP0_serializedExtKeyIn;
         this.AV13keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV8error = "" ;
         initialize();
         ExecuteImpl();
         aP1_keyInfo=this.AV13keyInfo;
         aP2_error=this.AV8error;
      }

      public string executeUdp( string aP0_serializedExtKeyIn ,
                                out GeneXus.Programs.nbitcoin.SdtKeyInfo aP1_keyInfo )
      {
         execute(aP0_serializedExtKeyIn, out aP1_keyInfo, out aP2_error);
         return AV8error ;
      }

      public void executeSubmit( string aP0_serializedExtKeyIn ,
                                 out GeneXus.Programs.nbitcoin.SdtKeyInfo aP1_keyInfo ,
                                 out string aP2_error )
      {
         this.AV16serializedExtKeyIn = aP0_serializedExtKeyIn;
         this.AV13keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context) ;
         this.AV8error = "" ;
         SubmitImpl();
         aP1_keyInfo=this.AV13keyInfo;
         aP2_error=this.AV8error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV8error;
         new GeneXus.Programs.nbitcoin.parse_serialized_extended_key(context ).execute(  AV16serializedExtKeyIn, out  AV15serializedExtKey, out  AV14networkType, out  AV9extendedKeyType, out  GXt_char1) ;
         AV8error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            AV10extKeyCreate.gxTpr_Networktype = AV14networkType;
            AV10extKeyCreate.gxTpr_Createextkeytype = 70;
            AV10extKeyCreate.gxTpr_Extendedprivatekey = AV15serializedExtKey;
            AV10extKeyCreate.gxTpr_Keypath = "";
            GXt_char1 = AV8error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV10extKeyCreate,  "", out  AV11extKeyInfo, out  GXt_char1) ;
            AV8error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV12keyCreate.gxTpr_Createkeytype = 30;
               AV12keyCreate.gxTpr_Createtext = AV11extKeyInfo.gxTpr_Privatekey;
               AV12keyCreate.gxTpr_Networktype = AV14networkType;
               if ( StringUtil.StrCmp(AV9extendedKeyType, "x") == 0 )
               {
                  AV12keyCreate.gxTpr_Addresstype = 0;
               }
               else if ( StringUtil.StrCmp(AV9extendedKeyType, "y") == 0 )
               {
                  AV12keyCreate.gxTpr_Addresstype = 2;
               }
               else if ( StringUtil.StrCmp(AV9extendedKeyType, "z") == 0 )
               {
                  AV12keyCreate.gxTpr_Addresstype = 1;
               }
               else if ( StringUtil.StrCmp(AV9extendedKeyType, "t") == 0 )
               {
                  AV12keyCreate.gxTpr_Addresstype = 3;
               }
               else
               {
                  AV8error = "Extended type not found";
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  GXt_char1 = AV8error;
                  new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV12keyCreate,  "", out  AV13keyInfo, out  GXt_char1) ;
                  AV8error = GXt_char1;
               }
            }
         }
         AV10extKeyCreate.gxTpr_Extendedprivatekey = "";
         AV11extKeyInfo.gxTpr_Privatekey = "";
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
         AV13keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV8error = "";
         AV15serializedExtKey = "";
         AV14networkType = "";
         AV9extendedKeyType = "";
         AV10extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV11extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV12keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV16serializedExtKeyIn ;
      private string AV8error ;
      private string AV15serializedExtKey ;
      private string AV14networkType ;
      private string AV9extendedKeyType ;
      private string GXt_char1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV13keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV10extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV11extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV12keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo aP1_keyInfo ;
      private string aP2_error ;
   }

}
