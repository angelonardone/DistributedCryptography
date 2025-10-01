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
namespace GeneXus.Programs.distcrypt.sso {
   public class createandsaveexternaluser : GXProcedure
   {
      public createandsaveexternaluser( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createandsaveexternaluser( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV9error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV9error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV9error = "" ;
         SubmitImpl();
         aP0_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExtKeyInfo1 = AV11extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV11extKeyInfo = GXt_SdtExtKeyInfo1;
         GXt_SdtExternalUser2 = AV10externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV10externalUser = GXt_SdtExternalUser2;
         AV8allKeyInfo.Clear();
         GXt_char3 = AV9error;
         new GeneXus.Programs.nbitcoin.derivekeysfromextkey(context ).execute(  AV11extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot,  (long)(Math.Round(NumberUtil.Val( "2000'", "."), 18, MidpointRounding.ToEven)),  0,  0, out  AV8allKeyInfo, out  GXt_char3) ;
         AV9error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV12keyInfo = ((GeneXus.Programs.nbitcoin.SdtKeyInfo)AV8allKeyInfo.Item(1));
            AV10externalUser.gxTpr_Chatkeyinfo = AV12keyInfo;
            AV8allKeyInfo.Clear();
            GXt_char3 = AV9error;
            new GeneXus.Programs.nbitcoin.derivekeysfromextkey(context ).execute(  AV11extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot,  (long)(Math.Round(NumberUtil.Val( "3000'", "."), 18, MidpointRounding.ToEven)),  0,  0, out  AV8allKeyInfo, out  GXt_char3) ;
            AV9error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV12keyInfo = ((GeneXus.Programs.nbitcoin.SdtKeyInfo)AV8allKeyInfo.Item(1));
               AV10externalUser.gxTpr_Groupskeyinfo = AV12keyInfo;
               new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV10externalUser) ;
               GXt_char3 = AV9error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "distcrypt.conf",  AV10externalUser.ToJSonString(false, true), out  GXt_char3) ;
               AV9error = GXt_char3;
            }
            else
            {
               GX_msglist.addItem("Error creating key for groups");
            }
         }
         else
         {
            GX_msglist.addItem("Error creating Key for chat");
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
         AV9error = "";
         AV11extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV10externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV8allKeyInfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "distributedcryptography");
         AV12keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_char3 = "";
         /* GeneXus formulas. */
      }

      private string AV9error ;
      private string GXt_char3 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV11extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV10externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> AV8allKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV12keyInfo ;
      private string aP0_error ;
   }

}
