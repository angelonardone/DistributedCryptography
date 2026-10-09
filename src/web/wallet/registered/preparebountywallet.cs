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
   public class preparebountywallet : GXProcedure
   {
      public preparebountywallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public preparebountywallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_isOwner ,
                           out DateTime aP2_bountyDate ,
                           out string aP3_bountyAddress ,
                           out long aP4_bountySequence ,
                           out string aP5_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV15isOwner = false ;
         this.AV10bountyDate = DateTime.MinValue ;
         this.AV9bountyAddress = "" ;
         this.AV11bountySequence = 0 ;
         this.AV12error = "" ;
         initialize();
         ExecuteImpl();
         aP1_isOwner=this.AV15isOwner;
         aP2_bountyDate=this.AV10bountyDate;
         aP3_bountyAddress=this.AV9bountyAddress;
         aP4_bountySequence=this.AV11bountySequence;
         aP5_error=this.AV12error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out bool aP1_isOwner ,
                                out DateTime aP2_bountyDate ,
                                out string aP3_bountyAddress ,
                                out long aP4_bountySequence )
      {
         execute(aP0_groupId, out aP1_isOwner, out aP2_bountyDate, out aP3_bountyAddress, out aP4_bountySequence, out aP5_error);
         return AV12error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_isOwner ,
                                 out DateTime aP2_bountyDate ,
                                 out string aP3_bountyAddress ,
                                 out long aP4_bountySequence ,
                                 out string aP5_error )
      {
         this.AV14groupId = aP0_groupId;
         this.AV15isOwner = false ;
         this.AV10bountyDate = DateTime.MinValue ;
         this.AV9bountyAddress = "" ;
         this.AV11bountySequence = 0 ;
         this.AV12error = "" ;
         SubmitImpl();
         aP1_isOwner=this.AV15isOwner;
         aP2_bountyDate=this.AV10bountyDate;
         aP3_bountyAddress=this.AV9bountyAddress;
         aP4_bountySequence=this.AV11bountySequence;
         aP5_error=this.AV12error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtGroup_SDT1 = AV13group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14groupId, out  GXt_SdtGroup_SDT1) ;
         AV13group_sdt = GXt_SdtGroup_SDT1;
         if ( (Guid.Empty==AV13group_sdt.gxTpr_Groupid) || ! ( AV13group_sdt.gxTpr_Grouptype == 20 ) || ! ( AV13group_sdt.gxTpr_Subgrouptype == 20 ) )
         {
            AV12error = "This should be a Bounty Group";
            cleanup();
            if (true) return;
         }
         AV15isOwner = AV13group_sdt.gxTpr_Amigroupowner;
         AV19websession.Set("Group_EDIT_WALLET", AV13group_sdt.ToJSonString(false, true));
         if ( ! AV13group_sdt.gxTpr_Amigroupowner )
         {
            GXt_char2 = AV12error;
            new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV13group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV8bount_group_sdt, out  GXt_char2) ;
            AV12error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               AV13group_sdt.gxTpr_Timeconstrain.Clear();
               AV13group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV8bount_group_sdt.gxTpr_Timeconstrain.Clone());
               AV20GXV1 = 1;
               while ( AV20GXV1 <= AV8bount_group_sdt.gxTpr_Contact.Count )
               {
                  AV16oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV8bount_group_sdt.gxTpr_Contact.Item(AV20GXV1));
                  if ( AV16oneGroupContact.gxTpr_Contactgroupid == AV13group_sdt.gxTpr_Groupid )
                  {
                     AV13group_sdt.gxTpr_Encpassword = AV16oneGroupContact.gxTpr_Contactencryptedkey;
                     AV13group_sdt.gxTpr_Encryptedtextshare = AV16oneGroupContact.gxTpr_Contactencryptedtext;
                  }
                  AV20GXV1 = (int)(AV20GXV1+1);
               }
               GXt_char2 = AV12error;
               new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV13group_sdt,  StringUtil.Trim( AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV18temp_groupId, out  GXt_char2) ;
               AV12error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  GXt_char2 = AV12error;
                  new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV13group_sdt, out  GXt_char2) ;
                  AV12error = GXt_char2;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     AV19websession.Set("Group_EDIT_WALLET", AV13group_sdt.ToJSonString(false, true));
                  }
               }
            }
         }
         GXt_SdtGroup_SDT_TimeConstrainItem3 = AV17oneTimeConstrain;
         new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV13group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem3) ;
         AV17oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem3;
         AV10bountyDate = AV17oneTimeConstrain.gxTpr_Date;
         AV9bountyAddress = AV17oneTimeConstrain.gxTpr_Address;
         AV11bountySequence = AV17oneTimeConstrain.gxTpr_Sequence;
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
         AV10bountyDate = DateTime.MinValue;
         AV9bountyAddress = "";
         AV12error = "";
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19websession = context.GetSession();
         AV8bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV16oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV18temp_groupId = Guid.Empty;
         GXt_char2 = "";
         AV17oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem3 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         /* GeneXus formulas. */
      }

      private int AV20GXV1 ;
      private long AV11bountySequence ;
      private string AV9bountyAddress ;
      private string AV12error ;
      private string GXt_char2 ;
      private DateTime AV10bountyDate ;
      private bool AV15isOwner ;
      private Guid AV14groupId ;
      private Guid AV18temp_groupId ;
      private IGxSession AV19websession ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8bount_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV16oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV17oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem3 ;
      private bool aP1_isOwner ;
      private DateTime aP2_bountyDate ;
      private string aP3_bountyAddress ;
      private long aP4_bountySequence ;
      private string aP5_error ;
   }

}
