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
namespace GeneXus.Programs {
   public class getrestparameters : GXProcedure
   {
      public getrestparameters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getrestparameters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out SdtSDT_Rest_Parameters_Item aP0_restParameters )
      {
         this.AV9restParameters = new SdtSDT_Rest_Parameters_Item(context) ;
         initialize();
         ExecuteImpl();
         aP0_restParameters=this.AV9restParameters;
      }

      public SdtSDT_Rest_Parameters_Item executeUdp( )
      {
         execute(out aP0_restParameters);
         return AV9restParameters ;
      }

      public void executeSubmit( out SdtSDT_Rest_Parameters_Item aP0_restParameters )
      {
         this.AV9restParameters = new SdtSDT_Rest_Parameters_Item(context) ;
         SubmitImpl();
         aP0_restParameters=this.AV9restParameters;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9restParameters.FromJSonString(AV8websession.Get(AV12Pgmname), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9restParameters.gxTpr_Hostname)) )
         {
            GXt_objcol_SdtSDT_Rest_Parameters_Item1 = AV10rest_parameters;
            new readrestconfigfile(context ).execute( out  GXt_objcol_SdtSDT_Rest_Parameters_Item1) ;
            AV10rest_parameters = GXt_objcol_SdtSDT_Rest_Parameters_Item1;
            GXt_SdtWallet2 = AV11wallet;
            new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
            AV11wallet = GXt_SdtWallet2;
            AV13GXV1 = 1;
            while ( AV13GXV1 <= AV10rest_parameters.Count )
            {
               AV9restParameters = ((SdtSDT_Rest_Parameters_Item)AV10rest_parameters.Item(AV13GXV1));
               if ( StringUtil.StrCmp(AV9restParameters.gxTpr_Networktype, AV11wallet.gxTpr_Networktype) == 0 )
               {
                  if (true) break;
               }
               AV13GXV1 = (int)(AV13GXV1+1);
            }
            AV8websession.Set(AV12Pgmname, AV9restParameters.ToJSonString(false, true));
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
         AV9restParameters = new SdtSDT_Rest_Parameters_Item(context);
         AV12Pgmname = "";
         AV8websession = context.GetSession();
         AV10rest_parameters = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography");
         GXt_objcol_SdtSDT_Rest_Parameters_Item1 = new GXBaseCollection<SdtSDT_Rest_Parameters_Item>( context, "Item", "distributedcryptography");
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV12Pgmname = "getRestParameters";
         /* GeneXus formulas. */
         AV12Pgmname = "getRestParameters";
      }

      private int AV13GXV1 ;
      private string AV12Pgmname ;
      private IGxSession AV8websession ;
      private SdtSDT_Rest_Parameters_Item aP0_restParameters ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> AV10rest_parameters ;
      private GXBaseCollection<SdtSDT_Rest_Parameters_Item> GXt_objcol_SdtSDT_Rest_Parameters_Item1 ;
      private SdtSDT_Rest_Parameters_Item AV9restParameters ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
   }

}
