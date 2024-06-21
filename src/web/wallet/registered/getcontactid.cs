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
   public class getcontactid : GXProcedure
   {
      public getcontactid( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getcontactid( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_userName ,
                           out string aP1_userPrivateName ,
                           out Guid aP2_contactId )
      {
         this.AV9userName = aP0_userName;
         this.AV13userPrivateName = "" ;
         this.AV8contactId = Guid.Empty ;
         initialize();
         ExecuteImpl();
         aP1_userPrivateName=this.AV13userPrivateName;
         aP2_contactId=this.AV8contactId;
      }

      public Guid executeUdp( string aP0_userName ,
                              out string aP1_userPrivateName )
      {
         execute(aP0_userName, out aP1_userPrivateName, out aP2_contactId);
         return AV8contactId ;
      }

      public void executeSubmit( string aP0_userName ,
                                 out string aP1_userPrivateName ,
                                 out Guid aP2_contactId )
      {
         this.AV9userName = aP0_userName;
         this.AV13userPrivateName = "" ;
         this.AV8contactId = Guid.Empty ;
         SubmitImpl();
         aP1_userPrivateName=this.AV13userPrivateName;
         aP2_contactId=this.AV8contactId;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8contactId = Guid.Empty;
         AV10allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV12error), null);
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV10allContacts.Count )
         {
            AV11contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV10allContacts.Item(AV14GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV11contact.gxTpr_Username), StringUtil.Trim( AV9userName)) == 0 )
            {
               AV8contactId = AV11contact.gxTpr_Contactrid;
               AV13userPrivateName = StringUtil.Trim( AV11contact.gxTpr_Userprivatename);
               if (true) break;
            }
            AV14GXV1 = (int)(AV14GXV1+1);
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
         AV13userPrivateName = "";
         AV8contactId = Guid.Empty;
         AV10allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV12error = "";
         AV11contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV14GXV1 ;
      private string AV9userName ;
      private string AV13userPrivateName ;
      private string AV12error ;
      private Guid AV8contactId ;
      private string aP1_userPrivateName ;
      private Guid aP2_contactId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV10allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV11contact ;
   }

}
