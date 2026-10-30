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
   public class appendgroupchat : GXProcedure
   {
      public appendgroupchat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public appendgroupchat( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           GeneXus.Programs.wallet.registered.SdtGroupChatMessage aP1_message ,
                           out bool aP2_added )
      {
         this.AV15referenceGroupId = aP0_referenceGroupId;
         this.AV12message = aP1_message;
         this.AV8added = false ;
         initialize();
         ExecuteImpl();
         aP2_added=this.AV8added;
      }

      public bool executeUdp( Guid aP0_referenceGroupId ,
                              GeneXus.Programs.wallet.registered.SdtGroupChatMessage aP1_message )
      {
         execute(aP0_referenceGroupId, aP1_message, out aP2_added);
         return AV8added ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 GeneXus.Programs.wallet.registered.SdtGroupChatMessage aP1_message ,
                                 out bool aP2_added )
      {
         this.AV15referenceGroupId = aP0_referenceGroupId;
         this.AV12message = aP1_message;
         this.AV8added = false ;
         SubmitImpl();
         aP2_added=this.AV8added;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV16wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV16wallet = GXt_SdtWallet1;
         AV9directory.Source = AV16wallet.gxTpr_Walletbasedirectory+"Chat";
         if ( ! AV9directory.Exists() )
         {
            AV9directory.Create();
         }
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV11fileName = "Chat" + (GXt_boolean3 ? "/" : "\\") + "group-" + StringUtil.Trim( AV15referenceGroupId.ToString()) + ".json";
         AV13messages.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV11fileName, out  AV10error), null);
         AV8added = true;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV13messages.Count )
         {
            AV14oneMessage = ((GeneXus.Programs.wallet.registered.SdtGroupChatMessage)AV13messages.Item(AV17GXV1));
            if ( AV14oneMessage.gxTpr_Id == AV12message.gxTpr_Id )
            {
               AV8added = false;
               if (true) break;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         if ( AV8added )
         {
            AV13messages.Add(AV12message, 0);
            GXt_char4 = AV10error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV11fileName,  AV13messages.ToJSonString(false), out  GXt_char4) ;
            AV10error = GXt_char4;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               AV8added = false;
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
         AV16wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV11fileName = "";
         AV13messages = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography");
         AV10error = "";
         AV14oneMessage = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private int AV17GXV1 ;
      private string AV11fileName ;
      private string AV10error ;
      private string GXt_char4 ;
      private bool AV8added ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private Guid AV15referenceGroupId ;
      private GxDirectory AV9directory ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV12message ;
      private GeneXus.Programs.wallet.SdtWallet AV16wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> AV13messages ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV14oneMessage ;
      private bool aP2_added ;
   }

}
