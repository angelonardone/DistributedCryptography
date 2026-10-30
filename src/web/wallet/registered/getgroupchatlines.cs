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
namespace GeneXus.Programs.wallet.registered {
   public class getgroupchatlines : GXProcedure
   {
      public getgroupchatlines( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroupchatlines( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> aP1_lines ,
                           out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV15lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine>( context, "GroupChatLine", "distributedcryptography") ;
         this.AV10error = "" ;
         initialize();
         ExecuteImpl();
         aP1_lines=this.AV15lines;
         aP2_error=this.AV10error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> aP1_lines )
      {
         execute(aP0_groupId, out aP1_lines, out aP2_error);
         return AV10error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> aP1_lines ,
                                 out string aP2_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV15lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine>( context, "GroupChatLine", "distributedcryptography") ;
         this.AV10error = "" ;
         SubmitImpl();
         aP1_lines=this.AV15lines;
         aP2_error=this.AV10error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV15lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine>( context, "GroupChatLine", "distributedcryptography");
         GXt_SdtGroup_SDT1 = AV12group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV12group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV12group_sdt.gxTpr_Groupid) )
         {
            AV10error = "Group not found";
            cleanup();
            if (true) return;
         }
         GXt_SdtExternalUserPublic2 = AV11externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
         AV11externalUserPublic = GXt_SdtExternalUserPublic2;
         AV8allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV10error), null);
         GXt_objcol_SdtGroupChatMessage3 = AV17messages;
         new GeneXus.Programs.wallet.registered.readgroupchat(context ).execute(  AV12group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid, out  GXt_objcol_SdtGroupChatMessage3) ;
         AV17messages = GXt_objcol_SdtGroupChatMessage3;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV17messages.Count )
         {
            AV16message = ((GeneXus.Programs.wallet.registered.SdtGroupChatMessage)AV17messages.Item(AV21GXV1));
            AV19chatLine = new GeneXus.Programs.wallet.registered.SdtGroupChatLine(context);
            GXt_dtime4 = (DateTime)(DateTime.MinValue);
            new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV16message.gxTpr_Datetimeunix, out  GXt_dtime4) ;
            AV19chatLine.gxTpr_Datetime = GXt_dtime4;
            AV19chatLine.gxTpr_Text = StringUtil.Trim( AV16message.gxTpr_Text);
            if ( StringUtil.StrCmp(StringUtil.Trim( AV16message.gxTpr_Fromusername), StringUtil.Trim( AV11externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               AV19chatLine.gxTpr_Who = "You";
               AV19chatLine.gxTpr_Mine = true;
            }
            else
            {
               AV18who = "";
               AV22GXV2 = 1;
               while ( AV22GXV2 <= AV8allContacts.Count )
               {
                  AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV8allContacts.Item(AV22GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV9contact.gxTpr_Username), StringUtil.Trim( AV16message.gxTpr_Fromusername)) == 0 )
                  {
                     AV18who = StringUtil.Trim( AV9contact.gxTpr_Userprivatename);
                     if (true) break;
                  }
                  AV22GXV2 = (int)(AV22GXV2+1);
               }
               AV20place = 0;
               AV23GXV3 = 1;
               while ( AV23GXV3 <= AV12group_sdt.gxTpr_Contact.Count )
               {
                  AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV12group_sdt.gxTpr_Contact.Item(AV23GXV3));
                  AV20place = (short)(AV20place+1);
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV13groupContact.gxTpr_Contactusername), StringUtil.Trim( AV16message.gxTpr_Fromusername)) == 0 )
                  {
                     if (true) break;
                  }
                  AV23GXV3 = (int)(AV23GXV3+1);
               }
               AV19chatLine.gxTpr_Colorindex = (short)(((int)((AV20place-1) % (6)))+1);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV18who)) )
               {
                  AV18who = StringUtil.Trim( AV16message.gxTpr_Fromusername);
                  AV18who = StringUtil.Substring( AV18who, 1, 14) + "...";
               }
               AV19chatLine.gxTpr_Who = AV18who;
            }
            AV15lines.Add(AV19chatLine, 0);
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         AV15lines.Sort("[dateTime]");
         AV10error = "";
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
         AV15lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine>( context, "GroupChatLine", "distributedcryptography");
         AV10error = "";
         AV12group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV8allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV17messages = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography");
         GXt_objcol_SdtGroupChatMessage3 = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage>( context, "GroupChatMessage", "distributedcryptography");
         AV16message = new GeneXus.Programs.wallet.registered.SdtGroupChatMessage(context);
         AV19chatLine = new GeneXus.Programs.wallet.registered.SdtGroupChatLine(context);
         GXt_dtime4 = (DateTime)(DateTime.MinValue);
         AV18who = "";
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         /* GeneXus formulas. */
      }

      private short AV20place ;
      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private int AV23GXV3 ;
      private string AV10error ;
      private DateTime GXt_dtime4 ;
      private string AV18who ;
      private Guid AV14groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> AV15lines ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV11externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV8allContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> AV17messages ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatMessage> GXt_objcol_SdtGroupChatMessage3 ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatMessage AV16message ;
      private GeneXus.Programs.wallet.registered.SdtGroupChatLine AV19chatLine ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> aP1_lines ;
      private string aP2_error ;
   }

}
