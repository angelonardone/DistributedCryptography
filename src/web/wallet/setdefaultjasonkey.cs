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
   public class setdefaultjasonkey : GXProcedure
   {
      public setdefaultjasonkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public setdefaultjasonkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_InKeyInfo )
      {
         this.AV11InKeyInfo = aP0_InKeyInfo;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GeneXus.Programs.nbitcoin.SdtKeyInfo aP0_InKeyInfo )
      {
         this.AV11InKeyInfo = aP0_InKeyInfo;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11InKeyInfo.gxTpr_Privatekey)) )
         {
            AV13WebSession.Set("DefaultJasonKey", AV11InKeyInfo.ToJSonString(false, true));
         }
         else
         {
            AV8allKeyInfo.Clear();
            GXt_SdtExtKeyInfo1 = AV10extKeyInfo;
            new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
            AV10extKeyInfo = GXt_SdtExtKeyInfo1;
            GXt_char2 = AV9error;
            new GeneXus.Programs.nbitcoin.derivekeysfromextkey(context ).execute(  AV10extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot,  (long)(Math.Round(NumberUtil.Val( "4000", "."), 18, MidpointRounding.ToEven)),  0,  0, out  AV8allKeyInfo, out  GXt_char2) ;
            AV9error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV12keyInfo = ((GeneXus.Programs.nbitcoin.SdtKeyInfo)AV8allKeyInfo.Item(1));
               AV13WebSession.Set("DefaultJasonKey", AV12keyInfo.ToJSonString(false, true));
            }
            else
            {
               AV13WebSession.Set("DefaultJasonKey", "");
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
         AV13WebSession = context.GetSession();
         AV8allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography");
         AV10extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV9error = "";
         GXt_char2 = "";
         AV12keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char2 ;
      private IGxSession AV13WebSession ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV11InKeyInfo ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> AV8allKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV10extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV12keyInfo ;
   }

}
