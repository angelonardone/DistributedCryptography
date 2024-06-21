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
   public class addmusigdatatohistory : GXProcedure
   {
      public addmusigdatatohistory( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public addmusigdatatohistory( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> aP0_signatureContacts ,
                           ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_transactionsToSend )
      {
         this.AV8signatureContacts = aP0_signatureContacts;
         this.AV9transactionsToSend = aP1_transactionsToSend;
         initialize();
         ExecuteImpl();
         aP1_transactionsToSend=this.AV9transactionsToSend;
      }

      public GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> executeUdp( GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> aP0_signatureContacts )
      {
         execute(aP0_signatureContacts, ref aP1_transactionsToSend);
         return AV9transactionsToSend ;
      }

      public void executeSubmit( GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> aP0_signatureContacts ,
                                 ref GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_transactionsToSend )
      {
         this.AV8signatureContacts = aP0_signatureContacts;
         this.AV9transactionsToSend = aP1_transactionsToSend;
         SubmitImpl();
         aP1_transactionsToSend=this.AV9transactionsToSend;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14currentDateTime = DateTimeUtil.Now( context);
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV9transactionsToSend.Count )
         {
            AV10oneTransaction = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV9transactionsToSend.Item(AV15GXV1));
            AV16GXV2 = 1;
            while ( AV16GXV2 <= AV8signatureContacts.Count )
            {
               AV11signatureContact = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData)AV8signatureContacts.Item(AV16GXV2));
               AV17GXV3 = 1;
               while ( AV17GXV3 <= AV11signatureContact.gxTpr_Data.Count )
               {
                  AV13muSigDataItem = ((GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem)AV11signatureContact.gxTpr_Data.Item(AV17GXV3));
                  if ( ( StringUtil.StrCmp(AV13muSigDataItem.gxTpr_Transactionid, AV10oneTransaction.gxTpr_Receivedtransactionid) == 0 ) && ( AV13muSigDataItem.gxTpr_N == AV10oneTransaction.gxTpr_Recivedn ) )
                  {
                     AV12oneSignatureContact = (GeneXus.Programs.wallet.SdtMultiSigSignatureData)(AV11signatureContact.Clone());
                     AV10oneTransaction.gxTpr_Multisignaturedata.Add(AV12oneSignatureContact, 0);
                     AV10oneTransaction.gxTpr_Sentdatetime = AV14currentDateTime;
                     if (true) break;
                  }
                  AV17GXV3 = (int)(AV17GXV3+1);
               }
               AV16GXV2 = (int)(AV16GXV2+1);
            }
            AV15GXV1 = (int)(AV15GXV1+1);
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
         AV14currentDateTime = (DateTime)(DateTime.MinValue);
         AV10oneTransaction = new GeneXus.Programs.wallet.SdtSDTAddressHistory(context);
         AV11signatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         AV13muSigDataItem = new GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem(context);
         AV12oneSignatureContact = new GeneXus.Programs.wallet.SdtMultiSigSignatureData(context);
         /* GeneXus formulas. */
      }

      private int AV15GXV1 ;
      private int AV16GXV2 ;
      private int AV17GXV3 ;
      private DateTime AV14currentDateTime ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> aP1_transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtMultiSigSignatureData> AV8signatureContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV9transactionsToSend ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV11signatureContact ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData AV12oneSignatureContact ;
      private GeneXus.Programs.wallet.SdtMultiSigSignatureData_DataItem AV13muSigDataItem ;
      private GeneXus.Programs.wallet.SdtSDTAddressHistory AV10oneTransaction ;
   }

}
