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
namespace GeneXus.Programs.wallet {
   public class readallnotes : GXProcedure
   {
      public readallnotes( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public readallnotes( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> aP0_notesRead )
      {
         this.AV22notesRead = new GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead>( context, "NoteRead", "distributedcryptography") ;
         initialize();
         ExecuteImpl();
         aP0_notesRead=this.AV22notesRead;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> executeUdp( )
      {
         execute(out aP0_notesRead);
         return AV22notesRead ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> aP0_notesRead )
      {
         this.AV22notesRead = new GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead>( context, "NoteRead", "distributedcryptography") ;
         SubmitImpl();
         aP0_notesRead=this.AV22notesRead;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtWallet1 = AV11wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV11wallet = GXt_SdtWallet1;
         AV18noteDirectory.Source = AV11wallet.gxTpr_Walletbasedirectory+"Notes";
         if ( ! AV18noteDirectory.Exists() )
         {
            AV18noteDirectory.Create();
         }
         AV24GXV2 = 1;
         AV23GXV1 = AV18noteDirectory.GetFiles("*.note");
         while ( AV24GXV2 <= AV23GXV1.ItemCount )
         {
            AV8auxFile = AV23GXV1.Item(AV24GXV2);
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            AV10fileName = "Notes" + (GXt_boolean3 ? "/" : "\\") + AV8auxFile.GetName();
            AV14note.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV10fileName, out  AV20error), null);
            AV21noteRead = new GeneXus.Programs.wallet.SdtNoteRead(context);
            AV21noteRead.gxTpr_Description = AV14note.gxTpr_Description;
            AV21noteRead.gxTpr_Created = AV14note.gxTpr_Created;
            AV21noteRead.gxTpr_Notefilename = StringUtil.Trim( AV10fileName);
            AV22notesRead.Add(AV21noteRead, 0);
            AV24GXV2 = (int)(AV24GXV2+1);
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
         AV22notesRead = new GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead>( context, "NoteRead", "distributedcryptography");
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV18noteDirectory = new GxDirectory(context.GetPhysicalPath());
         AV23GXV1 = new GxFileCollection();
         AV8auxFile = new GxFile(context.GetPhysicalPath());
         AV10fileName = "";
         AV14note = new GeneXus.Programs.wallet.SdtNote(context);
         AV20error = "";
         AV21noteRead = new GeneXus.Programs.wallet.SdtNoteRead(context);
         /* GeneXus formulas. */
      }

      private int AV24GXV2 ;
      private string AV20error ;
      private bool GXt_boolean2 ;
      private bool GXt_boolean3 ;
      private string AV10fileName ;
      private GxFile AV8auxFile ;
      private GxDirectory AV18noteDirectory ;
      private GxFileCollection AV23GXV1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> AV22notesRead ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtNote AV14note ;
      private GeneXus.Programs.wallet.SdtNoteRead AV21noteRead ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> aP0_notesRead ;
   }

}
