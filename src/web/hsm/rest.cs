using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.hsm {
   public class rest : GXProcedure
   {
      public rest( )
      {
         context = new GxContext(  );
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         IsApiObject = true;
      }

      public rest( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         IsApiObject = true;
         dsDefault = context.GetDataStore("Default");
         if ( context.HttpContext != null )
         {
            Gx_restmethod = (string)(context.HttpContext.Request.Method);
         }
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         cleanup();
      }

      public void gxep_getpubkey( short aP0_indexKey ,
                                  out string aP1_publicKey ,
                                  out string aP2_error )
      {
         this.AV5indexKey = aP0_indexKey;
         initialize();
         /* getPubKey Constructor */
         new GeneXus.Programs.hsm.getpubkey(context ).execute(  AV5indexKey, out  AV6publicKey, out  AV7error) ;
         aP1_publicKey=this.AV6publicKey;
         aP2_error=this.AV7error;
      }

      public void gxep_encrypt( string aP0_message ,
                                short aP1_indexKey ,
                                out string aP2_encryptedMessage ,
                                out string aP3_error )
      {
         this.AV9message = aP0_message;
         this.AV5indexKey = aP1_indexKey;
         initialize();
         /* encrypt Constructor */
         new GeneXus.Programs.hsm.encrypt(context ).execute(  AV9message,  AV5indexKey, out  AV10encryptedMessage, out  AV7error) ;
         aP2_encryptedMessage=this.AV10encryptedMessage;
         aP3_error=this.AV7error;
      }

      public void gxep_encrypttopubkey( string aP0_message ,
                                        string aP1_publicKey ,
                                        out string aP2_encryptedMessage ,
                                        out string aP3_error )
      {
         this.AV9message = aP0_message;
         this.AV6publicKey = aP1_publicKey;
         initialize();
         /* encryptToPubKey Constructor */
         new GeneXus.Programs.hsm.encrypttopubkey(context ).execute(  AV9message,  AV6publicKey, out  AV10encryptedMessage, out  AV7error) ;
         aP2_encryptedMessage=this.AV10encryptedMessage;
         aP3_error=this.AV7error;
      }

      public void gxep_decrypt( string aP0_encryptedMessage ,
                                short aP1_indexKey ,
                                out string aP2_message ,
                                out string aP3_error )
      {
         this.AV10encryptedMessage = aP0_encryptedMessage;
         this.AV5indexKey = aP1_indexKey;
         initialize();
         /* decrypt Constructor */
         new GeneXus.Programs.hsm.decrypt(context ).execute(  AV10encryptedMessage,  AV5indexKey, out  AV9message, out  AV7error) ;
         aP2_message=this.AV9message;
         aP3_error=this.AV7error;
      }

      public void gxep_sign( string aP0_message ,
                             short aP1_indexKey ,
                             out string aP2_signature ,
                             out string aP3_error )
      {
         this.AV9message = aP0_message;
         this.AV5indexKey = aP1_indexKey;
         initialize();
         /* sign Constructor */
         new GeneXus.Programs.hsm.sign(context ).execute(  AV9message,  AV5indexKey, out  AV11signature, out  AV7error) ;
         aP2_signature=this.AV11signature;
         aP3_error=this.AV7error;
      }

      public void gxep_signschnorr( string aP0_message ,
                                    short aP1_indexKey ,
                                    out string aP2_signature ,
                                    out string aP3_error )
      {
         this.AV9message = aP0_message;
         this.AV5indexKey = aP1_indexKey;
         initialize();
         /* SignSchnorr Constructor */
         new GeneXus.Programs.hsm.signschnorr(context ).execute(  AV9message,  AV5indexKey, out  AV11signature, out  AV7error) ;
         aP2_signature=this.AV11signature;
         aP3_error=this.AV7error;
      }

      public override void cleanup( )
      {
         CloseCursors();
      }

      public override void initialize( )
      {
         AV7error = "";
         AV11signature = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.hsm.rest__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
      }

      protected short AV5indexKey ;
      protected string Gx_restmethod ;
      protected string AV6publicKey ;
      protected string AV7error ;
      protected string AV11signature ;
      protected string AV9message ;
      protected string AV10encryptedMessage ;
      protected IGxDataStore dsDefault ;
      protected string aP1_publicKey ;
      protected string aP2_error ;
      protected string aP2_encryptedMessage ;
      protected string aP3_error ;
      protected string aP2_message ;
      protected string aP2_signature ;
      protected IDataStoreProvider pr_default ;
   }

   public class rest__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          def= new CursorDef[] {
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
    }

 }

}
