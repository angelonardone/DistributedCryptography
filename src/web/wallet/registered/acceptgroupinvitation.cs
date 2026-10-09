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
   public class acceptgroupinvitation : GXProcedure
   {
      public acceptgroupinvitation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public acceptgroupinvitation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_referenceGroupId ,
                           out string aP1_error )
      {
         this.AV23referenceGroupId = aP0_referenceGroupId;
         this.AV13error = "" ;
         initialize();
         ExecuteImpl();
         aP1_error=this.AV13error;
      }

      public string executeUdp( Guid aP0_referenceGroupId )
      {
         execute(aP0_referenceGroupId, out aP1_error);
         return AV13error ;
      }

      public void executeSubmit( Guid aP0_referenceGroupId ,
                                 out string aP1_error )
      {
         this.AV23referenceGroupId = aP0_referenceGroupId;
         this.AV13error = "" ;
         SubmitImpl();
         aP1_error=this.AV13error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_SdtExternalUser1 = AV14externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV14externalUser = GXt_SdtExternalUser1;
         GXt_SdtExtKeyInfo2 = AV15extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
         AV15extKeyInfoRoot = GXt_SdtExtKeyInfo2;
         GXt_SdtWallet3 = AV25wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet3) ;
         AV25wallet = GXt_SdtWallet3;
         GXt_SdtGroup_SDT4 = AV18group_sdt;
         new GeneXus.Programs.wallet.registered.getlocalgroupforrow(context ).execute(  AV11emptyGroupId,  AV23referenceGroupId, out  GXt_SdtGroup_SDT4) ;
         AV18group_sdt = GXt_SdtGroup_SDT4;
         if ( (Guid.Empty==AV18group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid) )
         {
            AV13error = "We couldn't find the invitation";
            cleanup();
            if (true) return;
         }
         AV20group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV20group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV18group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV20group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV14externalUser.gxTpr_Userinfo.gxTpr_Username);
         GXt_char5 = AV13error;
         new GeneXus.Programs.wallet.registered.creategroup(context ).execute(  AV20group_sdt_temp, out  AV21grpupId, out  GXt_char5) ;
         AV13error = GXt_char5;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV18group_sdt.gxTpr_Groupid = AV21grpupId;
            AV20group_sdt_temp.gxTpr_Groupid = AV21grpupId;
            if ( AV18group_sdt.gxTpr_Grouptype == 10 )
            {
               GXt_char5 = AV12encryptionKey;
               new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char5) ;
               AV12encryptionKey = GXt_char5;
               AV18group_sdt.gxTpr_Encpassword = AV12encryptionKey;
               AV20group_sdt_temp.gxTpr_Encpassword = AV12encryptionKey;
            }
            else if ( ( AV18group_sdt.gxTpr_Grouptype == 20 ) && ( AV18group_sdt.gxTpr_Subgrouptype == 30 ) )
            {
               GXt_char5 = AV12encryptionKey;
               new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char5) ;
               AV12encryptionKey = GXt_char5;
               AV18group_sdt.gxTpr_Encpassword = AV12encryptionKey;
               AV20group_sdt_temp.gxTpr_Encpassword = AV12encryptionKey;
            }
            else if ( AV18group_sdt.gxTpr_Grouptype == 30 )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV15extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV25wallet.gxTpr_Networktype,  "2", out  AV17extPubKeyInfo, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV20group_sdt_temp.gxTpr_Extpubkeymultisigreceiving = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
                  AV18group_sdt.gxTpr_Extpubkeymultisigreceiving = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV15extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV25wallet.gxTpr_Networktype,  "3", out  AV17extPubKeyInfo, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV20group_sdt_temp.gxTpr_Extpubkeymultisigchange = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
                     AV18group_sdt.gxTpr_Extpubkeymultisigchange = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
                  }
                  else
                  {
                     AV13error = "We couldn't create the Change Extended Public Key for the multisignature: " + AV13error;
                  }
               }
               else
               {
                  AV13error = "We couldn't create the Receiving Extended Public Key for the multisignature: " + AV13error;
               }
            }
            else if ( AV18group_sdt.gxTpr_Grouptype == 50 )
            {
               GXt_SdtExtKeyInfo2 = AV16extKeyInfoRootBIP48;
               new GeneXus.Programs.wallet.getextkeybip48(context ).execute( out  GXt_SdtExtKeyInfo2) ;
               AV16extKeyInfoRootBIP48 = GXt_SdtExtKeyInfo2;
               GXt_char5 = AV13error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV16extKeyInfoRootBIP48.gxTpr_Extended.gxTpr_Nuterpublickey,  AV25wallet.gxTpr_Networktype,  "0", out  AV17extPubKeyInfo, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV20group_sdt_temp.gxTpr_Extpubkeymultisigreceiving = AV17extPubKeyInfo.gxTpr_Publickey;
                  AV18group_sdt.gxTpr_Extpubkeymultisigreceiving = AV17extPubKeyInfo.gxTpr_Publickey;
                  GXt_char5 = AV13error;
                  new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV16extKeyInfoRootBIP48.gxTpr_Extended.gxTpr_Nuterpublickey,  AV25wallet.gxTpr_Networktype,  "1", out  AV17extPubKeyInfo, out  GXt_char5) ;
                  AV13error = GXt_char5;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                  {
                     AV20group_sdt_temp.gxTpr_Extpubkeymultisigchange = AV17extPubKeyInfo.gxTpr_Publickey;
                     AV18group_sdt.gxTpr_Extpubkeymultisigchange = AV17extPubKeyInfo.gxTpr_Publickey;
                  }
                  else
                  {
                     AV13error = "We couldn't create the Change Extended Public Key for the Legacy multisignature: " + AV13error;
                  }
               }
               else
               {
                  AV13error = "We couldn't create the Receiving Extended Public Key for the Legacy multisignature: " + AV13error;
               }
            }
            else if ( ( AV18group_sdt.gxTpr_Grouptype == 20 ) && ( AV18group_sdt.gxTpr_Subgrouptype == 20 ) )
            {
               GXt_char5 = AV13error;
               new GeneXus.Programs.nbitcoin.createexpubtkey(context ).execute(  AV15extKeyInfoRoot.gxTpr_Extended.gxTpr_Nuterpublickeytaproot,  AV25wallet.gxTpr_Networktype,  "4", out  AV17extPubKeyInfo, out  GXt_char5) ;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV20group_sdt_temp.gxTpr_Extpubkeytimebountyreceiving = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
                  AV18group_sdt.gxTpr_Extpubkeytimebountyreceiving = AV17extPubKeyInfo.gxTpr_Publickeytaproot;
               }
               else
               {
                  AV13error = "We couldn't create the Receiving Extended Public Key for the multisignature: " + AV13error;
               }
            }
            else if ( AV18group_sdt.gxTpr_Grouptype == 40 )
            {
            }
            else
            {
               AV13error = "Group type not sopported";
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
            {
               AV22message_signature.gxTpr_Username = StringUtil.Trim( AV14externalUser.gxTpr_Userinfo.gxTpr_Username);
               AV22message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV14externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
               GXt_char5 = AV13error;
               GXt_char6 = AV22message_signature.gxTpr_Signature;
               new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV14externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV22message_signature.gxTpr_Username)+StringUtil.Trim( AV22message_signature.gxTpr_Grouppubkey), out  GXt_char6, out  GXt_char5) ;
               AV22message_signature.gxTpr_Signature = GXt_char6;
               AV13error = GXt_char5;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
               {
                  AV20group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV22message_signature.gxTpr_Signature);
                  AV24sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int7 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int7) ;
                  AV24sdt_message.gxTpr_Datetimeunix = GXt_int7;
                  AV24sdt_message.gxTpr_Messagetype = 80;
                  AV24sdt_message.gxTpr_Message = AV20group_sdt_temp.ToJSonString(false, true);
                  AV9allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV13error), null);
                  AV26GXV1 = 1;
                  while ( AV26GXV1 <= AV9allContacts.Count )
                  {
                     AV10contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV9allContacts.Item(AV26GXV1));
                     if ( StringUtil.StrCmp(AV10contact.gxTpr_Username, StringUtil.Trim( AV18group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname)) == 0 )
                     {
                        if (true) break;
                     }
                     AV26GXV1 = (int)(AV26GXV1+1);
                  }
                  if ( ! (Guid.Empty==AV10contact.gxTpr_Contactrid) )
                  {
                     AV10contact.gxTpr_Username = StringUtil.Trim( AV18group_sdt.gxTpr_Othergroup.gxTpr_Referenceusernname);
                     AV10contact.gxTpr_Messagepubkey = StringUtil.Trim( AV10contact.gxTpr_Grouppubkey);
                     GXt_char6 = AV13error;
                     new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV10contact,  AV24sdt_message, out  GXt_char6) ;
                     AV13error = GXt_char6;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                     {
                        GXt_char6 = AV13error;
                        new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV18group_sdt,  StringUtil.Trim( AV18group_sdt.gxTpr_Encpassword), out  AV21grpupId, out  GXt_char6) ;
                        AV13error = GXt_char6;
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
                        {
                           AV8all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV13error), null);
                           AV27GXV2 = 1;
                           while ( AV27GXV2 <= AV8all_groups_sdt.Count )
                           {
                              AV19group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV8all_groups_sdt.Item(AV27GXV2));
                              if ( AV19group_sdt_delete.gxTpr_Othergroup.gxTpr_Referencegroupid == AV18group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid )
                              {
                                 AV8all_groups_sdt.RemoveItem(AV8all_groups_sdt.IndexOf(AV19group_sdt_delete));
                              }
                              AV27GXV2 = (int)(AV27GXV2+1);
                           }
                           AV8all_groups_sdt.Add(AV18group_sdt, 0);
                           new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV8all_groups_sdt.ToJSonString(false), out  AV13error) ;
                        }
                        else
                        {
                           AV13error = "There was a problem updating the Group: " + AV13error;
                        }
                     }
                     else
                     {
                        AV13error = "There was a problem sending the Invitation to the Group: " + AV13error;
                     }
                  }
                  else
                  {
                     AV13error = "we couldn't find the contact on your contact list";
                  }
               }
               else
               {
                  AV13error = "There was a problem signin the message: " + AV13error;
               }
            }
         }
         else
         {
            AV13error = "There was a problem creating creating a new group on the server: " + AV13error;
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
         AV13error = "";
         AV14externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV15extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV25wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV18group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT4 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11emptyGroupId = Guid.Empty;
         AV20group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21grpupId = Guid.Empty;
         AV12encryptionKey = "";
         AV17extPubKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
         AV16extKeyInfoRootBIP48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV22message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char5 = "";
         AV24sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV9allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char6 = "";
         AV8all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV19group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         /* GeneXus formulas. */
      }

      private int AV26GXV1 ;
      private int AV27GXV2 ;
      private long GXt_int7 ;
      private string AV13error ;
      private string AV12encryptionKey ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private Guid AV23referenceGroupId ;
      private Guid AV11emptyGroupId ;
      private Guid AV21grpupId ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV14externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV15extKeyInfoRoot ;
      private GeneXus.Programs.wallet.SdtWallet AV25wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV18group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT4 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV20group_sdt_temp ;
      private GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo AV17extPubKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV16extKeyInfoRootBIP48 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV22message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV24sdt_message ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV9allContacts ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV8all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt_delete ;
      private string aP1_error ;
   }

}
