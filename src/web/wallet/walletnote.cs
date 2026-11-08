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
   public class walletnote : GXProcedure
   {
      public walletnote( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public walletnote( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_noteFileName ,
                           bool aP1_save ,
                           ref string aP2_description ,
                           ref string aP3_text ,
                           out string aP4_error )
      {
         this.AV12noteFileName = aP0_noteFileName;
         this.AV15save = aP1_save;
         this.AV8description = aP2_description;
         this.AV16text = aP3_text;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP2_description=this.AV8description;
         aP3_text=this.AV16text;
         aP4_error=this.AV9error;
      }

      public string executeUdp( string aP0_noteFileName ,
                                bool aP1_save ,
                                ref string aP2_description ,
                                ref string aP3_text )
      {
         execute(aP0_noteFileName, aP1_save, ref aP2_description, ref aP3_text, out aP4_error);
         return AV9error ;
      }

      public void executeSubmit( string aP0_noteFileName ,
                                 bool aP1_save ,
                                 ref string aP2_description ,
                                 ref string aP3_text ,
                                 out string aP4_error )
      {
         this.AV12noteFileName = aP0_noteFileName;
         this.AV15save = aP1_save;
         this.AV8description = aP2_description;
         this.AV16text = aP3_text;
         this.AV9error = "" ;
         SubmitImpl();
         aP2_description=this.AV8description;
         aP3_text=this.AV16text;
         aP4_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9error = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12noteFileName))) )
         {
            if ( ! AV15save )
            {
               AV9error = "No note";
            }
            else
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV8description))) )
               {
                  AV9error = "The Description is mandatory";
               }
               else
               {
                  if ( GxRegex.IsMatch(AV8description,"^[ ]") )
                  {
                     AV9error = "Spaces at the beginning of the name are not allowed";
                  }
                  else
                  {
                     GXt_SdtWallet1 = AV17wallet;
                     new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
                     AV17wallet = GXt_SdtWallet1;
                     AV11noteDirectory.Source = AV17wallet.gxTpr_Walletbasedirectory+"Notes";
                     if ( ! AV11noteDirectory.Exists() )
                     {
                        AV11noteDirectory.Create();
                     }
                     GXt_boolean2 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
                     GXt_boolean3 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
                     AV14safeName = "Notes" + (GXt_boolean3 ? "/" : "\\") + StringUtil.Trim( Guid.NewGuid( ).ToString()) + ".note";
                     AV10note = new GeneXus.Programs.wallet.SdtNote(context);
                     AV10note.gxTpr_Description = StringUtil.Trim( AV8description);
                     AV10note.gxTpr_Created = DateTimeUtil.Now( context);
                     AV10note.gxTpr_Notetext = AV16text;
                     GXt_char4 = AV9error;
                     new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV14safeName,  AV10note.ToJSonString(false, true), out  GXt_char4) ;
                     AV9error = GXt_char4;
                  }
               }
            }
         }
         else
         {
            /* User Code */
             AV13plainName = System.IO.Path.GetFileName(AV12noteFileName.Trim().Replace('\\', '/'));
            if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV13plainName))) || ( StringUtil.StringSearch( AV13plainName, "..", 1) > 0 ) || ! StringUtil.EndsWith( StringUtil.Lower( StringUtil.Trim( AV13plainName)), ".note") )
            {
               AV9error = "Invalid note file name";
            }
            else
            {
               GXt_boolean3 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
               GXt_boolean2 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
               AV14safeName = "Notes" + (GXt_boolean2 ? "/" : "\\") + StringUtil.Trim( AV13plainName);
               new GeneXus.Programs.wallet.walletfilepath(context ).execute(  AV14safeName, out  AV18fullPath, out  AV9error) ;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV19noteFile.Source = AV18fullPath;
                  if ( ! AV19noteFile.Exists() )
                  {
                     AV9error = "The note does not exist";
                  }
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV10note.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV14safeName, out  AV9error), null);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  if ( AV15save )
                  {
                     AV10note.gxTpr_Notetext = AV16text;
                     GXt_char4 = AV9error;
                     new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  AV14safeName,  AV10note.ToJSonString(false, true), out  GXt_char4) ;
                     AV9error = GXt_char4;
                  }
                  else
                  {
                     AV8description = AV10note.gxTpr_Description;
                     AV16text = AV10note.gxTpr_Notetext;
                  }
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
         AV9error = "";
         AV17wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11noteDirectory = new GxDirectory(context.GetPhysicalPath());
         AV14safeName = "";
         AV10note = new GeneXus.Programs.wallet.SdtNote(context);
         AV13plainName = "";
         AV18fullPath = "";
         AV19noteFile = new GxFile(context.GetPhysicalPath());
         GXt_char4 = "";
         /* GeneXus formulas. */
      }

      private string AV8description ;
      private string AV9error ;
      private string GXt_char4 ;
      private bool AV15save ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean2 ;
      private string AV16text ;
      private string AV12noteFileName ;
      private string AV14safeName ;
      private string AV13plainName ;
      private string AV18fullPath ;
      private GxFile AV19noteFile ;
      private GxDirectory AV11noteDirectory ;
      private string aP2_description ;
      private string aP3_text ;
      private GeneXus.Programs.wallet.SdtWallet AV17wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.SdtNote AV10note ;
      private string aP4_error ;
   }

}
