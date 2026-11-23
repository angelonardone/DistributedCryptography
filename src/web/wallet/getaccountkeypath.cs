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
   public class getaccountkeypath : GXProcedure
   {
      public getaccountkeypath( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getaccountkeypath( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_walletType ,
                           string aP1_networkType ,
                           out string aP2_keyPath )
      {
         this.AV10walletType = aP0_walletType;
         this.AV9networkType = aP1_networkType;
         this.AV8keyPath = "" ;
         initialize();
         ExecuteImpl();
         aP2_keyPath=this.AV8keyPath;
      }

      public string executeUdp( string aP0_walletType ,
                                string aP1_networkType )
      {
         execute(aP0_walletType, aP1_networkType, out aP2_keyPath);
         return AV8keyPath ;
      }

      public void executeSubmit( string aP0_walletType ,
                                 string aP1_networkType ,
                                 out string aP2_keyPath )
      {
         this.AV10walletType = aP0_walletType;
         this.AV9networkType = aP1_networkType;
         this.AV8keyPath = "" ;
         SubmitImpl();
         aP2_keyPath=this.AV8keyPath;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8keyPath = "";
         if ( StringUtil.StrCmp(AV10walletType, "BIP86") == 0 )
         {
            if ( StringUtil.StrCmp(AV9networkType, "MainNet") == 0 )
            {
               AV8keyPath = "m/86'/0'/0'";
            }
            else if ( StringUtil.StrCmp(AV9networkType, "TestNet") == 0 )
            {
               AV8keyPath = "m/86'/1'/0'";
            }
            else
            {
               AV8keyPath = "m/86'/1'/0'";
            }
         }
         else if ( StringUtil.StrCmp(AV10walletType, "BIP44") == 0 )
         {
            if ( StringUtil.StrCmp(AV9networkType, "MainNet") == 0 )
            {
               AV8keyPath = "m/44'/0'/0'";
            }
            else if ( StringUtil.StrCmp(AV9networkType, "TestNet") == 0 )
            {
               AV8keyPath = "m/44'/1'/0'";
            }
            else
            {
               AV8keyPath = "m/44'/1'/0'";
            }
         }
         else if ( StringUtil.StrCmp(AV10walletType, "BIP49") == 0 )
         {
            if ( StringUtil.StrCmp(AV9networkType, "MainNet") == 0 )
            {
               AV8keyPath = "m/49'/0'/0'";
            }
            else if ( StringUtil.StrCmp(AV9networkType, "TestNet") == 0 )
            {
               AV8keyPath = "m/49'/1'/0'";
            }
            else
            {
               AV8keyPath = "m/49'/1'/0'";
            }
         }
         else if ( StringUtil.StrCmp(AV10walletType, "BIP84") == 0 )
         {
            if ( StringUtil.StrCmp(AV9networkType, "MainNet") == 0 )
            {
               AV8keyPath = "m/84'/0'/0'";
            }
            else if ( StringUtil.StrCmp(AV9networkType, "TestNet") == 0 )
            {
               AV8keyPath = "m/84'/1'/0'";
            }
            else
            {
               AV8keyPath = "m/84'/1'/0'";
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
         AV8keyPath = "";
         /* GeneXus formulas. */
      }

      private string AV10walletType ;
      private string AV9networkType ;
      private string AV8keyPath ;
      private string aP2_keyPath ;
   }

}
