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
   public class getlastjasonencritionkey : GXProcedure
   {
      public getlastjasonencritionkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getlastjasonencritionkey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_encryptionKey )
      {
         this.AV12encryptionKey = "" ;
         initialize();
         ExecuteImpl();
         aP0_encryptionKey=this.AV12encryptionKey;
      }

      public string executeUdp( )
      {
         execute(out aP0_encryptionKey);
         return AV12encryptionKey ;
      }

      public void executeSubmit( out string aP0_encryptionKey )
      {
         this.AV12encryptionKey = "" ;
         SubmitImpl();
         aP0_encryptionKey=this.AV12encryptionKey;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9encryptedKey = AV8websession.Get("LastJasonEncKey");
         GXt_SdtKeyInfo1 = AV15keyInfo;
         new GeneXus.Programs.wallet.getdfaultjasonkey(context ).execute( out  GXt_SdtKeyInfo1) ;
         AV15keyInfo = GXt_SdtKeyInfo1;
         GXt_char2 = AV13error;
         new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV15keyInfo.gxTpr_Privatekey,  AV9encryptedKey, out  AV12encryptionKey, out  GXt_char2) ;
         AV13error = GXt_char2;
         AV8websession.Set("LastJasonEncKey", "");
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
         AV12encryptionKey = "";
         AV9encryptedKey = "";
         AV8websession = context.GetSession();
         AV15keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_SdtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV13error = "";
         GXt_char2 = "";
         /* GeneXus formulas. */
      }

      private string AV12encryptionKey ;
      private string AV9encryptedKey ;
      private string AV13error ;
      private string GXt_char2 ;
      private IGxSession AV8websession ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV15keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo1 ;
      private string aP0_encryptionKey ;
   }

}
