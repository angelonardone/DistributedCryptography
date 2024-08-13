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
namespace GeneXus.Programs.nbitcoin {
   public class derivekeysfromextkey : GXProcedure
   {
      public derivekeysfromextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public derivekeysfromextkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_serializedRootExtKey ,
                           long aP1_base ,
                           long aP2_begin ,
                           long aP3_end ,
                           out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> aP4_allKeyInfo ,
                           out string aP5_error )
      {
         this.AV26serializedRootExtKey = aP0_serializedRootExtKey;
         this.AV10base = aP1_base;
         this.AV29begin = aP2_begin;
         this.AV13end = aP3_end;
         this.AV28allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography") ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP4_allKeyInfo=this.AV28allKeyInfo;
         aP5_error=this.AV14error;
      }

      public string executeUdp( string aP0_serializedRootExtKey ,
                                long aP1_base ,
                                long aP2_begin ,
                                long aP3_end ,
                                out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> aP4_allKeyInfo )
      {
         execute(aP0_serializedRootExtKey, aP1_base, aP2_begin, aP3_end, out aP4_allKeyInfo, out aP5_error);
         return AV14error ;
      }

      public void executeSubmit( string aP0_serializedRootExtKey ,
                                 long aP1_base ,
                                 long aP2_begin ,
                                 long aP3_end ,
                                 out GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> aP4_allKeyInfo ,
                                 out string aP5_error )
      {
         this.AV26serializedRootExtKey = aP0_serializedRootExtKey;
         this.AV10base = aP1_base;
         this.AV29begin = aP2_begin;
         this.AV13end = aP3_end;
         this.AV28allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography") ;
         this.AV14error = "" ;
         SubmitImpl();
         aP4_allKeyInfo=this.AV28allKeyInfo;
         aP5_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV14error;
         new GeneXus.Programs.nbitcoin.parse_serialized_extended_key(context ).execute(  AV26serializedRootExtKey, out  AV25serializedExtKey, out  AV22networkType, out  AV15extendedKeyType, out  GXt_char1) ;
         AV14error = GXt_char1;
         AV11base_char = "/" + StringUtil.Trim( StringUtil.Str( (decimal)(AV10base), 10, 0)) + "/";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV17extKeyCreate.gxTpr_Networktype = AV22networkType;
            AV17extKeyCreate.gxTpr_Createextkeytype = 70;
            AV17extKeyCreate.gxTpr_Extendedprivatekey = AV25serializedExtKey;
            AV27start = AV29begin;
            while ( AV27start <= AV13end )
            {
               AV17extKeyCreate.gxTpr_Keypath = AV11base_char+StringUtil.Trim( StringUtil.Str( (decimal)(AV27start), 10, 0));
               GXt_char1 = AV14error;
               new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV17extKeyCreate,  "", out  AV18extKeyInfo, out  GXt_char1) ;
               AV14error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  AV20keyCreate.gxTpr_Createkeytype = 30;
                  AV20keyCreate.gxTpr_Createtext = AV18extKeyInfo.gxTpr_Privatekey;
                  AV20keyCreate.gxTpr_Networktype = AV22networkType;
                  if ( StringUtil.StrCmp(AV15extendedKeyType, "x") == 0 )
                  {
                     AV20keyCreate.gxTpr_Addresstype = 0;
                  }
                  else if ( StringUtil.StrCmp(AV15extendedKeyType, "y") == 0 )
                  {
                     AV20keyCreate.gxTpr_Addresstype = 2;
                  }
                  else if ( StringUtil.StrCmp(AV15extendedKeyType, "z") == 0 )
                  {
                     AV20keyCreate.gxTpr_Addresstype = 1;
                  }
                  else if ( StringUtil.StrCmp(AV15extendedKeyType, "t") == 0 )
                  {
                     AV20keyCreate.gxTpr_Addresstype = 3;
                  }
                  else
                  {
                     AV14error = "Extended type not found";
                  }
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     GXt_char1 = AV14error;
                     new GeneXus.Programs.nbitcoin.createkey(context ).execute(  AV20keyCreate,  "", out  AV21keyInfo, out  GXt_char1) ;
                     AV14error = GXt_char1;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                     {
                        AV28allKeyInfo.Add(AV21keyInfo, 0);
                     }
                     else
                     {
                        if (true) break;
                     }
                  }
                  else
                  {
                     if (true) break;
                  }
               }
               else
               {
                  if (true) break;
               }
               AV27start = (long)(AV27start+1);
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
         AV28allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography");
         AV14error = "";
         AV25serializedExtKey = "";
         AV22networkType = "";
         AV15extendedKeyType = "";
         AV11base_char = "";
         AV17extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV18extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV20keyCreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
         GXt_char1 = "";
         AV21keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private long AV10base ;
      private long AV29begin ;
      private long AV13end ;
      private long AV27start ;
      private string AV26serializedRootExtKey ;
      private string AV14error ;
      private string AV25serializedExtKey ;
      private string AV22networkType ;
      private string AV15extendedKeyType ;
      private string AV11base_char ;
      private string GXt_char1 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> AV28allKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV17extKeyCreate ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV18extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyCreate AV20keyCreate ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV21keyInfo ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> aP4_allKeyInfo ;
      private string aP5_error ;
   }

}
