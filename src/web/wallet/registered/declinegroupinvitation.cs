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
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV8all_groups_sdt.Count )
         {
            AV11group_sdt = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV13GXV1));
            if ( (Guid.Empty==AV11group_sdt.gxTpr_Groupid) && ( AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid == AV12referenceGroupId ) )
            {
               AV11group_sdt.gxTpr_Othergroup.gxTpr_Invitationdeclined = true;
               AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = "";
               AV10found = true;
            }
            AV13GXV1 = (int)(AV13GXV1+1);
         }
         if ( AV10found )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  GXt_char1) ;
            AV9error = GXt_char1;
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
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private string AV9error ;
      private string GXt_char1 ;
      private bool AV10found ;
      private Guid AV12referenceGroupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private string aP1_error ;
   }

}
