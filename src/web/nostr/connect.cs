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
namespace GeneXus.Programs.nostr {
   public class connect : GXProcedure
   {
      public connect( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public connect( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out string aP0_error )
      {
         this.AV11error = "" ;
         initialize();
         ExecuteImpl();
         aP0_error=this.AV11error;
      }

      public string executeUdp( )
      {
         execute(out aP0_error);
         return AV11error ;
      }

      public void executeSubmit( out string aP0_error )
      {
         this.AV11error = "" ;
         SubmitImpl();
         aP0_error=this.AV11error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV13externalUser = GXt_SdtExternalUser1;
         GXt_SdtWallet2 = AV27wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV27wallet = GXt_SdtWallet2;
         GXt_char3 = AV11error;
         new GeneXus.Programs.nostr.getnostrconfigservers(context ).execute( out  AV28ConnectionParameters, out  GXt_char3) ;
         AV11error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_guid4 = AV17key;
            new GeneXus.Programs.nostr.getnostrconnectionid(context ).execute( out  GXt_guid4) ;
            AV17key = GXt_guid4;
            if ( (Guid.Empty==AV17key) )
            {
               if ( AV28ConnectionParameters.Count == 0 )
               {
                  if ( StringUtil.StrCmp(AV27wallet.gxTpr_Networktype, "MainNet") == 0 )
                  {
                     AV26OperationResult = GxInternetLibWs.connect("ws://nostr-mainnet.distributedcryptography.com:4848", "receiveFromNoster", 5000);
                  }
                  else if ( StringUtil.StrCmp(AV27wallet.gxTpr_Networktype, "TestNet") == 0 )
                  {
                     AV26OperationResult = GxInternetLibWs.connect("ws://nostr-testnet.distributedcryptography.com:4848", "receiveFromNoster", 5000);
                  }
                  else if ( StringUtil.StrCmp(AV27wallet.gxTpr_Networktype, "RegTest") == 0 )
                  {
                     AV26OperationResult = GxInternetLibWs.connect("ws://nostr-regtest.distributedcryptography.com:4848", "receiveFromNoster", 5000);
                  }
                  else
                  {
                     AV11error = "We couldn't find a Nostr connection associated with the Netwrok Type";
                  }
               }
               else
               {
                  AV29oneConnParameter = (GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV28ConnectionParameters.Item(1)).Clone());
                  if ( AV29oneConnParameter.gxTpr_Secure )
                  {
                     AV26OperationResult = GxInternetLibWs.connect("wss://"+StringUtil.Trim( AV29oneConnParameter.gxTpr_Hostname)+":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV29oneConnParameter.gxTpr_Port), 6, 0)), "receiveFromNoster", AV29oneConnParameter.gxTpr_Timeoutmiliseconds);
                  }
                  else
                  {
                     AV26OperationResult = GxInternetLibWs.connect("ws://"+StringUtil.Trim( AV29oneConnParameter.gxTpr_Hostname)+":"+StringUtil.Trim( StringUtil.Str( (decimal)(AV29oneConnParameter.gxTpr_Port), 6, 0)), "receiveFromNoster", AV29oneConnParameter.gxTpr_Timeoutmiliseconds);
                  }
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  if ( AV26OperationResult.gxTpr_Success )
                  {
                     AV17key = AV26OperationResult.gxTpr_Connectionid;
                     new GeneXus.Programs.nostr.settnostrconnectionid(context ).execute(  AV17key) ;
                  }
                  else
                  {
                     AV11error = "Chat server error (Nostr): " + AV26OperationResult.gxTpr_Errormessage;
                  }
               }
            }
         }
         else
         {
            AV11error = "There was a problem reading Nostr configuration: " + AV11error;
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
         AV11error = "";
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV27wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_char3 = "";
         AV28ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV17key = Guid.Empty;
         GXt_guid4 = Guid.Empty;
         AV26OperationResult = new GeneXus.Programs.gxinternetlib.SdtOperationResult(context);
         GxInternetLibWs = new GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs(context);
         AV29oneConnParameter = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         /* GeneXus formulas. */
      }

      private string AV11error ;
      private string GXt_char3 ;
      private Guid AV17key ;
      private Guid GXt_guid4 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.wallet.SdtWallet AV27wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV28ConnectionParameters ;
      private GeneXus.Programs.gxinternetlib.SdtOperationResult AV26OperationResult ;
      private GeneXus.Programs.gxinternetlib.SdtGxInternetLibWs GxInternetLibWs ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV29oneConnParameter ;
      private string aP0_error ;
   }

}
