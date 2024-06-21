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
   public class setfileenckey : GXProcedure
   {
      public setfileenckey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setfileenckey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExtKeyInfo1 = AV12extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV12extKeyInfo = GXt_SdtExtKeyInfo1;
         GXt_char2 = AV11error;
         new GeneXus.Programs.nbitcoin.derivekeysfromextkey(context ).execute(  AV12extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot,  (long)(Math.Round(NumberUtil.Val( "5000", "."), 18, MidpointRounding.ToEven)),  0,  0, out  AV10allKeyInfo, out  GXt_char2) ;
         AV11error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV8keyInfo = ((GeneXus.Programs.nbitcoin.SdtKeyInfo)AV10allKeyInfo.Item(1));
            AV9WebSession.Set("FileEncKey", AV8keyInfo.ToJSonString(false, true));
         }
         else
         {
            AV9WebSession.Set("FileEncKey", "");
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
         AV12extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV11error = "";
         GXt_char2 = "";
         AV10allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography");
         AV8keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV11error ;
      private string GXt_char2 ;
      private IGxSession AV9WebSession ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> AV10allKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV8keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
   }

}
