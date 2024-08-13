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
   public class cleanextkey : GXProcedure
   {
      public cleanextkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public cleanextkey( IGxContext context )
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
         AV8extKeyInfo.FromJSonString(AV9WebSession.Get("ExtendedKey"), null);
         AV8extKeyInfo.gxTpr_Privatekey = "";
         AV8extKeyInfo.gxTpr_Chaincode = "";
         AV8extKeyInfo.gxTpr_Encryptedwif = "";
         AV8extKeyInfo.gxTpr_Mnemonic = "";
         AV8extKeyInfo.gxTpr_Wif = "";
         AV8extKeyInfo.gxTpr_Extended.gxTpr_Privatekey = "";
         AV8extKeyInfo.gxTpr_Extended.gxTpr_Privatekeysegwit = "";
         AV8extKeyInfo.gxTpr_Extended.gxTpr_Privatekeysegwitp2sh = "";
         AV8extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot = "";
         AV9WebSession.Set("ExtendedKey", AV8extKeyInfo.ToJSonString(false, true));
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
         AV8extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV9WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9WebSession ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV8extKeyInfo ;
   }

}
