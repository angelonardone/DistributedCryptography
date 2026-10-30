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
   public class savemusig2room : GXProcedure
   {
      public savemusig2room( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public savemusig2room( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Programs.wallet.registered.SdtMuSig2Room aP0_room ,
                           out string aP1_error )
      {
         this.AV14room = aP0_room;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( GeneXus.Programs.wallet.registered.SdtMuSig2Room aP0_room )
      {
         execute(aP0_room, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( GeneXus.Programs.wallet.registered.SdtMuSig2Room aP0_room ,
                                 out string aP1_error )
      {
         this.AV14room = aP0_room;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV16wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV16wallet = GXt_SdtWallet1;
         AV8directory.Source = AV16wallet.gxTpr_Walletbasedirectory+"Chat";
         if ( ! AV8directory.Exists() )
         {
            AV8directory.Create();
         }
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV10fileName = "Chat" + (GXt_boolean3 ? "/" : "\\") + "musig2-" + StringUtil.Trim( AV14room.gxTpr_Referencegroupid.ToString()) + ".json";
         AV15rooms.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV10fileName, out  AV9error), null);
         AV9error = "";
         AV11found = false;
         AV12index = 0;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV15rooms.Count )
         {
            AV13oneRoom = ((GeneXus.Programs.wallet.registered.SdtMuSig2Room)AV15rooms.Item(AV17GXV1));
            AV12index = (short)(AV12index+1);
            if ( AV13oneRoom.gxTpr_Roomid == AV14room.gxTpr_Roomid )
            {
               AV11found = true;
               if (true) break;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         if ( AV11found )
         {
            AV15rooms.RemoveItem(AV12index);
         }
         AV15rooms.Add(AV14room, 0);
         GXt_char4 = AV9error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV10fileName,  AV15rooms.ToJSonString(false), out  GXt_char4) ;
         AV9error = GXt_char4;
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
         AV9error = "";
         AV16wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV8directory = new GxDirectory(context.GetPhysicalPath());
         AV10fileName = "";
         AV15rooms = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room>( context, "MuSig2Room", "distributedcryptography");
         AV13oneRoom = new GeneXus.Programs.wallet.registered.SdtMuSig2Room(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private short AV12index ;
      private int AV17GXV1 ;
      private string AV9error ;
      private string AV10fileName ;
      private string GXt_char4 ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private bool AV11found ;
      private GxDirectory AV8directory ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV14room ;
      private GeneXus.Programs.wallet.SdtWallet AV16wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2Room> AV15rooms ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2Room AV13oneRoom ;
      private string aP1_error ;
   }

}
