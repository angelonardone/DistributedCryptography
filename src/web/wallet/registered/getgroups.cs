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
   public class getgroups : GXProcedure
   {
      public getgroups( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public getgroups( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> aP0_all_groups_sdt ,
                           out string aP1_error )
      {
         this.AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography") ;
         this.AV16error = "" ;
         initialize();
         ExecuteImpl();
         aP0_all_groups_sdt=this.AV8all_groups_sdt;
         aP1_error=this.AV16error;
      }

      public string executeUdp( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> aP0_all_groups_sdt )
      {
         execute(out aP0_all_groups_sdt, out aP1_error);
         return AV16error ;
      }

      public void executeSubmit( out GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> aP0_all_groups_sdt ,
                                 out string aP1_error )
      {
         this.AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography") ;
         this.AV16error = "" ;
         SubmitImpl();
         aP0_all_groups_sdt=this.AV8all_groups_sdt;
         aP1_error=this.AV16error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV9externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV9externalUser = GXt_SdtExternalUser1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9externalUser.gxTpr_Externaltoken)) )
         {
            new desktopappservicesrestgetusergroupsget(context ).execute(  AV13properties,  AV9externalUser.gxTpr_Externaltoken, out  AV10getUserGroups__getOutputOUT, out  AV11HttpMessage, out  AV12IsSuccess) ;
            if ( ! AV12IsSuccess )
            {
               AV16error = AV11HttpMessage.gxTpr_Description;
            }
            else
            {
               AV16error = AV10getUserGroups__getOutputOUT.gxTpr_Error;
               AV14encriptedGroups = AV10getUserGroups__getOutputOUT.gxTpr_Groups;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16error)) )
               {
                  AV19GXV1 = 1;
                  while ( AV19GXV1 <= AV14encriptedGroups.gxTpr_Group.Count )
                  {
                     AV15oneEncGroup = ((SdtDesktopApp_services_SDT_Groups_Group_GroupItem)AV14encriptedGroups.gxTpr_Group.Item(AV19GXV1));
                     GXt_char2 = AV16error;
                     new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV15oneEncGroup.gxTpr_Groupencrypted,  AV15oneEncGroup.gxTpr_Groupencryptedkey, out  AV17group_sdt_chart, out  GXt_char2) ;
                     AV16error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16error)) )
                     {
                        AV18group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
                        AV18group_sdt.FromJSonString(AV17group_sdt_chart, null);
                        AV18group_sdt.gxTpr_Groupid = AV15oneEncGroup.gxTpr_Groupid;
                        AV8all_groups_sdt.Add(AV18group_sdt, 0);
                     }
                     else
                     {
                        AV16error = "There was an error unencrypting the group information: " + AV16error;
                        cleanup();
                        if (true) return;
                     }
                     AV19GXV1 = (int)(AV19GXV1+1);
                  }
               }
            }
         }
         else
         {
            AV16error = "You are not logged into Distributed Cryptography main site";
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
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV16error = "";
         AV9externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13properties = new GXProperties();
         AV10getUserGroups__getOutputOUT = new SdtgetUserGroups__getOutput(context);
         AV11HttpMessage = new GeneXus.Utils.SdtMessages_Message(context);
         AV14encriptedGroups = new SdtDesktopApp_services_SDT_Groups(context);
         AV15oneEncGroup = new SdtDesktopApp_services_SDT_Groups_Group_GroupItem(context);
         GXt_char2 = "";
         AV17group_sdt_chart = "";
         AV18group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV19GXV1 ;
      private string AV16error ;
      private string GXt_char2 ;
      private bool AV12IsSuccess ;
      private string AV17group_sdt_chart ;
      private GXProperties AV13properties ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV9externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private SdtgetUserGroups__getOutput AV10getUserGroups__getOutputOUT ;
      private GeneXus.Utils.SdtMessages_Message AV11HttpMessage ;
      private SdtDesktopApp_services_SDT_Groups AV14encriptedGroups ;
      private SdtDesktopApp_services_SDT_Groups_Group_GroupItem AV15oneEncGroup ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV18group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> aP0_all_groups_sdt ;
      private string aP1_error ;
   }

}
