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
namespace GeneXus.Programs.nostr {
   public class processrecivedfromnostr : GXProcedure
   {
      public processrecivedfromnostr( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public processrecivedfromnostr( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Core.genexus.server.SdtNotificationInfo aP0_NotificationInfo ,
                           out GeneXus.Programs.nostr.SdtRecFromNostr aP1_recFromNostr )
      {
         this.AV17NotificationInfo = aP0_NotificationInfo;
         this.AV25recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context) ;
         initialize();
         ExecuteImpl();
         aP1_recFromNostr=this.AV25recFromNostr;
      }

      public GeneXus.Programs.nostr.SdtRecFromNostr executeUdp( GeneXus.Core.genexus.server.SdtNotificationInfo aP0_NotificationInfo )
      {
         execute(aP0_NotificationInfo, out aP1_recFromNostr);
         return AV25recFromNostr ;
      }

      public void executeSubmit( GeneXus.Core.genexus.server.SdtNotificationInfo aP0_NotificationInfo ,
                                 out GeneXus.Programs.nostr.SdtRecFromNostr aP1_recFromNostr )
      {
         this.AV17NotificationInfo = aP0_NotificationInfo;
         this.AV25recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context) ;
         SubmitImpl();
         aP1_recFromNostr=this.AV25recFromNostr;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13message = StringUtil.StringReplace( AV17NotificationInfo.gxTpr_Message, "\\\"", "\"");
         AV18pattern = "\\[\"" + "EVENT" + "\",";
         if ( GxRegex.IsMatch(AV13message,AV18pattern) )
         {
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV13message,AV18pattern));
            AV21stringToWorkWith = ((string)AV19retSplit.Item(2));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\""));
            AV22subscriptionId = ((string)AV19retSplit.Item(2));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV21stringToWorkWith,"{"));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"}"));
            AV21stringToWorkWith = StringUtil.Trim( ((string)AV19retSplit.Item(1)));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV21stringToWorkWith,"\""));
            AV16nostrEvent.gxTpr_Id = ((string)AV19retSplit.Item(4));
            AV16nostrEvent.gxTpr_Pubkey = ((string)AV19retSplit.Item(8));
            AV26created_at_txt = StringUtil.StringReplace( ((string)AV19retSplit.Item(11)), ",", "");
            AV26created_at_txt = StringUtil.StringReplace( AV26created_at_txt, ":", "");
            AV16nostrEvent.gxTpr_Created_at = (long)(Math.Round(NumberUtil.Val( AV26created_at_txt, "."), 18, MidpointRounding.ToEven));
            AV27kind_txt = StringUtil.StringReplace( ((string)AV19retSplit.Item(13)), ",", "");
            AV27kind_txt = StringUtil.StringReplace( AV27kind_txt, ":", "");
            AV16nostrEvent.gxTpr_Kind = (int)(Math.Round(NumberUtil.Val( AV27kind_txt, "."), 18, MidpointRounding.ToEven));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV21stringToWorkWith,"\\[\\["));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\\]\\]"));
            AV23tags = ((string)AV19retSplit.Item(1));
            AV9afterTags = ((string)AV19retSplit.Item(2));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV9afterTags,"\""));
            AV16nostrEvent.gxTpr_Content = ((string)AV19retSplit.Item(4));
            AV16nostrEvent.gxTpr_Sig = ((string)AV19retSplit.Item(8));
            AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV23tags,"\\],\\["));
            AV11i = 1;
            while ( AV11i <= AV19retSplit.Count )
            {
               AV20retSplit2 = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(AV11i)),"[\",\\,]"));
               AV24tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
               AV12j = 1;
               while ( AV12j <= AV20retSplit2.Count )
               {
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( ((string)AV20retSplit2.Item(AV12j)))) )
                  {
                     AV24tagsoftags.gxTpr_Tag.Add(((string)AV20retSplit2.Item(AV12j)), 0);
                  }
                  AV12j = (short)(AV12j+1);
               }
               AV16nostrEvent.gxTpr_Tags.Add(AV24tagsoftags, 0);
               AV11i = (short)(AV11i+1);
            }
            AV25recFromNostr.gxTpr_Responsetype = "EVENT";
            AV25recFromNostr.gxTpr_Subscription_id = AV22subscriptionId;
            AV25recFromNostr.gxTpr_Event = AV16nostrEvent;
         }
         else
         {
            AV18pattern = "\\[\"" + "OK" + "\",";
            if ( GxRegex.IsMatch(AV13message,AV18pattern) )
            {
               AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV13message,AV18pattern));
               AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\""));
               AV10eventId = ((string)AV19retSplit.Item(2));
               AV15messageOK = ((string)AV19retSplit.Item(4));
               AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(3)),","));
               AV8acceptedOK = ((StringUtil.StrCmp(((string)AV19retSplit.Item(2)), "true")==0) ? true : false);
               AV25recFromNostr.gxTpr_Responsetype = "OK";
               AV25recFromNostr.gxTpr_Event_id = AV10eventId;
               AV25recFromNostr.gxTpr_Event_accepted = AV8acceptedOK;
               AV25recFromNostr.gxTpr_Messge = AV15messageOK;
            }
            else
            {
               AV18pattern = "\\[\"" + "EOSE" + "\",";
               if ( GxRegex.IsMatch(AV13message,AV18pattern) )
               {
                  AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV13message,AV18pattern));
                  AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\""));
                  AV22subscriptionId = ((string)AV19retSplit.Item(2));
                  AV25recFromNostr.gxTpr_Responsetype = "EOSE";
                  AV25recFromNostr.gxTpr_Subscription_id = AV22subscriptionId;
               }
               else
               {
                  AV18pattern = "\\[\"" + "CLOSED" + "\",";
                  if ( GxRegex.IsMatch(AV13message,AV18pattern) )
                  {
                     AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV13message,AV18pattern));
                     AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\""));
                     AV22subscriptionId = ((string)AV19retSplit.Item(2));
                     AV15messageOK = ((string)AV19retSplit.Item(4));
                     AV25recFromNostr.gxTpr_Responsetype = "CLOSED";
                     AV25recFromNostr.gxTpr_Subscription_id = AV22subscriptionId;
                     AV25recFromNostr.gxTpr_Messge = AV15messageOK;
                  }
                  else
                  {
                     AV18pattern = "\\[\"" + "NOTICE" + "\",";
                     if ( GxRegex.IsMatch(AV13message,AV18pattern) )
                     {
                        AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(AV13message,AV18pattern));
                        AV19retSplit = (GxSimpleCollection<string>)(GxRegex.Split(((string)AV19retSplit.Item(2)),"\""));
                        AV14messageNOTICE = ((string)AV19retSplit.Item(2));
                        AV25recFromNostr.gxTpr_Responsetype = "NOTICE";
                        AV25recFromNostr.gxTpr_Messge = AV14messageNOTICE;
                     }
                     else
                     {
                        GX_msglist.addItem("error: "+AV13message);
                     }
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
         AV25recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
         AV13message = "";
         AV18pattern = "";
         AV19retSplit = new GxSimpleCollection<string>();
         AV21stringToWorkWith = "";
         AV22subscriptionId = "";
         AV16nostrEvent = new GeneXus.Programs.nostr.SdtEvent(context);
         AV26created_at_txt = "";
         AV27kind_txt = "";
         AV23tags = "";
         AV9afterTags = "";
         AV20retSplit2 = new GxSimpleCollection<string>();
         AV24tagsoftags = new GeneXus.Programs.nostr.SdtTagsOfTags(context);
         AV10eventId = "";
         AV15messageOK = "";
         AV14messageNOTICE = "";
         /* GeneXus formulas. */
      }

      private short AV11i ;
      private short AV12j ;
      private string AV18pattern ;
      private string AV22subscriptionId ;
      private string AV26created_at_txt ;
      private string AV27kind_txt ;
      private string AV10eventId ;
      private string AV15messageOK ;
      private string AV14messageNOTICE ;
      private bool AV8acceptedOK ;
      private string AV13message ;
      private string AV21stringToWorkWith ;
      private string AV23tags ;
      private string AV9afterTags ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV17NotificationInfo ;
      private GeneXus.Programs.nostr.SdtRecFromNostr AV25recFromNostr ;
      private GxSimpleCollection<string> AV19retSplit ;
      private GeneXus.Programs.nostr.SdtEvent AV16nostrEvent ;
      private GxSimpleCollection<string> AV20retSplit2 ;
      private GeneXus.Programs.nostr.SdtTagsOfTags AV24tagsoftags ;
      private GeneXus.Programs.nostr.SdtRecFromNostr aP1_recFromNostr ;
   }

}
