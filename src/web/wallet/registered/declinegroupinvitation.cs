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
   public class declinegroupinvitation : GXProcedure
   {
      public declinegroupinvitation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public declinegroupinvitation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           out string aP1_error )
      {
         this.AV12referenceGroupId = aP0_referenceGroupId;
         this.AV9error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV9error;
      }

      public string executeUdp( Guid aP0_referenceGroupId )
      {
         execute(aP0_referenceGroupId, out aP1_error);
         return AV9error ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 out string aP1_error )
      {
         this.AV12referenceGroupId = aP0_referenceGroupId;
         this.AV9error = "" ;
         SubmitImpl();
         aP1_error=this.AV9error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV9error), null);
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV8all_groups_sdt.Count )
         {
            AV11group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV21GXV1));
            if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) && ( AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12referenceGroupId ) )
            {
               AV11group_sdt.gxTpr_Othergroup.gxTpr_Invitationdeclined = true;
               AV18ownerUserName = StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname);
               AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = "";
               AV10found = true;
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         if ( AV10found )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_SdtExternalUserPublic2 = AV16externalUserPublic;
               new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic2) ;
               AV16externalUserPublic = GXt_SdtExternalUserPublic2;
               AV17group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
               AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV12referenceGroupId;
               AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
               GXt_char1 = AV9error;
               new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username)+StringUtil.Trim( AV16externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey), out  AV20signature, out  GXt_char1) ;
               AV9error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV20signature);
                  AV19sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int3 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int3) ;
                  AV19sdt_message.gxTpr_Datetimeunix = GXt_int3;
                  AV19sdt_message.gxTpr_Messagetype = 140;
                  AV19sdt_message.gxTpr_Message = AV17group_sdt_temp.ToJSonString(false, true);
                  AV13allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV14auxError), null);
                  AV22GXV2 = 1;
                  while ( AV22GXV2 <= AV13allContacts.Count )
                  {
                     AV15contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV13allContacts.Item(AV22GXV2));
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV15contact.gxTpr_Username), StringUtil.Trim( AV18ownerUserName)) == 0 )
                     {
                        AV15contact.gxTpr_Messagepubkey = StringUtil.Trim( AV15contact.gxTpr_Grouppubkey);
                        GXt_char1 = AV9error;
                        new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV15contact,  AV19sdt_message, out  GXt_char1) ;
                        AV9error = GXt_char1;
                        if (true) break;
                     }
                     AV22GXV2 = (int)(AV22GXV2+1);
                  }
               }
            }
         }
         else
         {
            AV9error = "We couldn't find the invitation";
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
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV18ownerUserName = "";
         AV16externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic2 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV17group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20signature = "";
         AV19sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV13allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV14auxError = "";
         AV15contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private long GXt_int3 ;
      private string AV9error ;
      private string AV14auxError ;
      private string GXt_char1 ;
      private bool AV10found ;
      private string AV18ownerUserName ;
      private string AV20signature ;
      private Guid AV12referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV16externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt_temp ;
      private GeneXus.Programs.nostr.SdtSDT_message AV19sdt_message ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV13allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV15contact ;
      private string aP1_error ;
   }

}
