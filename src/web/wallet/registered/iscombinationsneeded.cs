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
namespace GeneXus.Programs.wallet.registered {
   public class iscombinationsneeded : GXProcedure
   {
      public iscombinationsneeded( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public iscombinationsneeded( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_all_EC_PublicKey ,
                           GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination ,
                           out bool aP2_isNeeded )
      {
         this.AV15all_EC_PublicKey = aP0_all_EC_PublicKey;
         this.AV9finalOneCombination = aP1_finalOneCombination;
         this.AV11isNeeded = false ;
         initialize();
         ExecuteImpl();
         aP2_isNeeded=this.AV11isNeeded;
      }

      public bool executeUdp( GxSimpleCollection<string> aP0_all_EC_PublicKey ,
                              GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination )
      {
         execute(aP0_all_EC_PublicKey, aP1_finalOneCombination, out aP2_isNeeded);
         return AV11isNeeded ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_all_EC_PublicKey ,
                                 GeneXus.Programs.math.SdtfinalCombination aP1_finalOneCombination ,
                                 out bool aP2_isNeeded )
      {
         this.AV15all_EC_PublicKey = aP0_all_EC_PublicKey;
         this.AV9finalOneCombination = aP1_finalOneCombination;
         this.AV11isNeeded = false ;
         SubmitImpl();
         aP2_isNeeded=this.AV11isNeeded;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV17found = 0;
         AV18GXV1 = 1;
         while ( AV18GXV1 <= AV15all_EC_PublicKey.Count )
         {
            AV16one_EC_PublicKey = AV15all_EC_PublicKey.GetString(AV18GXV1);
            AV19GXV2 = 1;
            while ( AV19GXV2 <= AV9finalOneCombination.gxTpr_Items.Count )
            {
               AV13oneItem = AV9finalOneCombination.gxTpr_Items.GetString(AV19GXV2);
               if ( StringUtil.StrCmp(AV16one_EC_PublicKey, AV13oneItem) == 0 )
               {
                  AV17found = (short)(AV17found+1);
                  if (true) break;
               }
               AV19GXV2 = (int)(AV19GXV2+1);
            }
            AV18GXV1 = (int)(AV18GXV1+1);
         }
         if ( AV17found == AV15all_EC_PublicKey.Count )
         {
            AV11isNeeded = true;
         }
         else
         {
            AV11isNeeded = false;
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
         AV16one_EC_PublicKey = "";
         AV13oneItem = "";
         /* GeneXus formulas. */
      }

      private short AV17found ;
      private int AV18GXV1 ;
      private int AV19GXV2 ;
      private string AV16one_EC_PublicKey ;
      private string AV13oneItem ;
      private bool AV11isNeeded ;
      private GxSimpleCollection<string> AV15all_EC_PublicKey ;
      private GeneXus.Programs.math.SdtfinalCombination AV9finalOneCombination ;
      private bool aP2_isNeeded ;
   }

}
