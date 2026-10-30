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
namespace GeneXus.Programs.wallet.registered {
   public class readmusig2rooms : GXProcedure
   {
      public readmusig2rooms( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readmusig2rooms( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> aP1_rooms )
      {
         this.AV10referenceGroupId = aP0_referenceGroupId;
         this.AV11rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP1_rooms=this.AV11rooms;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> executeUdp( Guid aP0_referenceGroupId )
      {
         execute(aP0_referenceGroupId, out aP1_rooms);
         return AV11rooms ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> aP1_rooms )
      {
         this.AV10referenceGroupId = aP0_referenceGroupId;
         this.AV11rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography") ;
         SubmitImpl();
         aP1_rooms=this.AV11rooms;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV9fileName = "Chat" + (GXt_boolean2 ? "/" : "\\") + "musig2-" + StringUtil.Trim( AV10referenceGroupId.ToString()) + ".json";
         AV11rooms.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV9fileName, out  AV8error), null);
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
         AV11rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV9fileName = "";
         AV8error = "";
         /* GeneXus formulas. */
      }

      private string AV9fileName ;
      private string AV8error ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private Guid AV10referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV11rooms ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> aP1_rooms ;
   }

}
