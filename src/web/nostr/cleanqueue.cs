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
namespace GeneXus.Programs.nostr {
   public class cleanqueue : GXProcedure
   {
      public cleanqueue( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public cleanqueue( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_key ,
                           bool aP1_deleteOwn )
      {
         this.AV10key = aP0_key;
         this.AV8deleteOwn = aP1_deleteOwn;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_key ,
                                 bool aP1_deleteOwn )
      {
         this.AV10key = aP0_key;
         this.AV8deleteOwn = aP1_deleteOwn;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_boolean1 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean1) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV15separator = (GXt_boolean2 ? "/" : "\\");
         AV14root.Source = "Comqueue";
         if ( AV14root.Exists() )
         {
            if ( AV8deleteOwn )
            {
               AV9folder.Source = "Comqueue"+AV15separator+StringUtil.Trim( AV10key.ToString());
               if ( AV9folder.Exists() )
               {
                  AV17GXV2 = 1;
                  AV16GXV1 = AV9folder.GetFiles("");
                  while ( AV17GXV2 <= AV16GXV1.ItemCount )
                  {
                     AV12queueFile = AV16GXV1.Item(AV17GXV2);
                     AV12queueFile.Delete();
                     AV17GXV2 = (int)(AV17GXV2+1);
                  }
                  AV9folder.Delete();
               }
            }
            else
            {
               AV11limit = DateTimeUtil.Now( context);
               AV11limit = DateTimeUtil.TAdd( AV11limit, 60*(-60));
               AV19GXV4 = 1;
               AV18GXV3 = AV14root.GetFiles(".queue");
               while ( AV19GXV4 <= AV18GXV3.ItemCount )
               {
                  AV12queueFile = AV18GXV3.Item(AV19GXV4);
                  AV12queueFile.Delete();
                  AV19GXV4 = (int)(AV19GXV4+1);
               }
               AV21GXV6 = 1;
               AV20GXV5 = AV14root.GetDirectories();
               while ( AV21GXV6 <= AV20GXV5.ItemCount )
               {
                  AV9folder = AV20GXV5.Item(AV21GXV6);
                  if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV9folder.GetName()), StringUtil.Trim( AV10key.ToString())) == 0 ) )
                  {
                     AV13remaining = 0;
                     AV23GXV8 = 1;
                     AV22GXV7 = AV9folder.GetFiles("");
                     while ( AV23GXV8 <= AV22GXV7.ItemCount )
                     {
                        AV12queueFile = AV22GXV7.Item(AV23GXV8);
                        if ( AV12queueFile.GetLastModified() < AV11limit )
                        {
                           AV12queueFile.Delete();
                        }
                        else
                        {
                           AV13remaining = (int)(AV13remaining+1);
                        }
                        AV23GXV8 = (int)(AV23GXV8+1);
                     }
                     if ( AV13remaining == 0 )
                     {
                        AV9folder.Delete();
                     }
                  }
                  AV21GXV6 = (int)(AV21GXV6+1);
               }
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
         AV15separator = "";
         AV14root = new GxDirectory(context.GetPhysicalPath());
         AV9folder = new GxDirectory(context.GetPhysicalPath());
         AV16GXV1 = new GxFileCollection();
         AV12queueFile = new GxFile(context.GetPhysicalPath());
         AV11limit = (DateTime)(DateTime.MinValue);
         AV18GXV3 = new GxFileCollection();
         AV20GXV5 = new GxDirectoryCollection();
         AV22GXV7 = new GxFileCollection();
         /* GeneXus formulas. */
      }

      private int AV17GXV2 ;
      private int AV19GXV4 ;
      private int AV21GXV6 ;
      private int AV13remaining ;
      private int AV23GXV8 ;
      private string AV15separator ;
      private DateTime AV11limit ;
      private bool AV8deleteOwn ;
      private bool GXt_boolean1 ;
      private bool GXt_boolean2 ;
      private Guid AV10key ;
      private GxFile AV12queueFile ;
      private GxDirectory AV14root ;
      private GxDirectory AV9folder ;
      private GxFileCollection AV16GXV1 ;
      private GxFileCollection AV18GXV3 ;
      private GxFileCollection AV22GXV7 ;
      private GxDirectoryCollection AV20GXV5 ;
   }

}
