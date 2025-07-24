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
namespace GeneXus.Programs.math {
   public class generatecombinations : GXProcedure
   {
      public generatecombinations( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public generatecombinations( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GxSimpleCollection<string> aP0_items ,
                           short aP1_k ,
                           short aP2_currentIndex ,
                           ref GxSimpleCollection<string> aP3_currentCombination ,
                           ref GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP4_finalCombinations )
      {
         this.AV11items = aP0_items;
         this.AV12k = aP1_k;
         this.AV9currentIndex = aP2_currentIndex;
         this.AV8currentCombination = aP3_currentCombination;
         this.AV15finalCombinations = aP4_finalCombinations;
         initialize();
         ExecuteImpl();
         aP3_currentCombination=this.AV8currentCombination;
         aP4_finalCombinations=this.AV15finalCombinations;
      }

      public GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> executeUdp( GxSimpleCollection<string> aP0_items ,
                                                                                     short aP1_k ,
                                                                                     short aP2_currentIndex ,
                                                                                     ref GxSimpleCollection<string> aP3_currentCombination )
      {
         execute(aP0_items, aP1_k, aP2_currentIndex, ref aP3_currentCombination, ref aP4_finalCombinations);
         return AV15finalCombinations ;
      }

      public void executeSubmit( GxSimpleCollection<string> aP0_items ,
                                 short aP1_k ,
                                 short aP2_currentIndex ,
                                 ref GxSimpleCollection<string> aP3_currentCombination ,
                                 ref GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP4_finalCombinations )
      {
         this.AV11items = aP0_items;
         this.AV12k = aP1_k;
         this.AV9currentIndex = aP2_currentIndex;
         this.AV8currentCombination = aP3_currentCombination;
         this.AV15finalCombinations = aP4_finalCombinations;
         SubmitImpl();
         aP3_currentCombination=this.AV8currentCombination;
         aP4_finalCombinations=this.AV15finalCombinations;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10i = AV9currentIndex;
         while ( AV10i <= AV11items.Count )
         {
            AV8currentCombination.Add(AV11items.GetString(AV10i), 0);
            if ( AV12k == 1 )
            {
               AV16finalCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
               AV18GXV1 = 1;
               while ( AV18GXV1 <= AV8currentCombination.Count )
               {
                  AV17oneItem = AV8currentCombination.GetString(AV18GXV1);
                  AV16finalCombination.gxTpr_Items.Add(AV17oneItem, 0);
                  AV18GXV1 = (int)(AV18GXV1+1);
               }
               AV15finalCombinations.Add(AV16finalCombination, 0);
            }
            else
            {
               AV14newK = (short)(AV12k-1);
               AV13newIndex = (short)(AV10i+1);
               new GeneXus.Programs.math.generatecombinations(context ).execute(  AV11items,  AV14newK,  AV13newIndex, ref  AV8currentCombination, ref  AV15finalCombinations) ;
            }
            AV8currentCombination.RemoveItem(AV8currentCombination.Count);
            AV10i = (short)(AV10i+1);
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
         AV16finalCombination = new GeneXus.Programs.math.SdtfinalCombination(context);
         AV17oneItem = "";
         /* GeneXus formulas. */
      }

      private short AV12k ;
      private short AV9currentIndex ;
      private short AV10i ;
      private short AV14newK ;
      private short AV13newIndex ;
      private int AV18GXV1 ;
      private string AV17oneItem ;
      private GxSimpleCollection<string> AV11items ;
      private GxSimpleCollection<string> AV8currentCombination ;
      private GxSimpleCollection<string> aP3_currentCombination ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> AV15finalCombinations ;
      private GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> aP4_finalCombinations ;
      private GeneXus.Programs.math.SdtfinalCombination AV16finalCombination ;
   }

}
