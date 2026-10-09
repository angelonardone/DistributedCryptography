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
   public class signtimevaultrestore : GXProcedure
   {
      public signtimevaultrestore( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public signtimevaultrestore( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( Guid aP0_groupId ,
                           out bool aP1_restored ,
                           out string aP2_error )
      {
         this.AV22groupId = aP0_groupId;
         this.AV29restored = false ;
         this.AV14error = "" ;
         initialize();
         ExecuteImpl();
         aP1_restored=this.AV29restored;
         aP2_error=this.AV14error;
      }

      public string executeUdp( Guid aP0_groupId ,
                                out bool aP1_restored )
      {
         execute(aP0_groupId, out aP1_restored, out aP2_error);
         return AV14error ;
      }

      public void executeSubmit( Guid aP0_groupId ,
                                 out bool aP1_restored ,
                                 out string aP2_error )
      {
         this.AV22groupId = aP0_groupId;
         this.AV29restored = false ;
         this.AV14error = "" ;
         SubmitImpl();
         aP1_restored=this.AV29restored;
         aP2_error=this.AV14error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV29restored = false;
         AV25numOfSharedWasReach = false;
         AV35sharesToRecover.Clear();
         GXt_SdtExternalUserPublic1 = AV16externalUserPublic;
         new GeneXus.Programs.distcrypt.getexternaluserpublic(context ).execute( out  GXt_SdtExternalUserPublic1) ;
         AV16externalUserPublic = GXt_SdtExternalUserPublic1;
         GXt_SdtGroup_SDT2 = AV18group_sdt_my;
         new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV22groupId, out  GXt_SdtGroup_SDT2) ;
         AV18group_sdt_my = GXt_SdtGroup_SDT2;
         if ( (Guid.Empty==AV18group_sdt_my.gxTpr_Groupid) )
         {
            AV14error = "We couldn't find the group";
            cleanup();
            if (true) return;
         }
         GXt_char3 = AV14error;
         new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV18group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV18group_sdt_my.gxTpr_Othergroup.gxTpr_Encpassword, out  AV17group_sdt, out  GXt_char3) ;
         AV14error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            cleanup();
            if (true) return;
         }
         GXt_SdtGroup_SDT_TimeConstrainItem4 = AV27oneTimeConstrain;
         new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV17group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem4) ;
         AV27oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem4;
         GXt_char3 = AV14error;
         new GeneXus.Programs.electrum.getsecretfromoneaddress(context ).execute(  StringUtil.Trim( AV27oneTimeConstrain.gxTpr_Address), out  AV9bountyActive, out  AV31secret, out  GXt_char3) ;
         AV14error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            cleanup();
            if (true) return;
         }
         if ( AV9bountyActive || String.IsNullOrEmpty(StringUtil.RTrim( AV31secret)) )
         {
            AV14error = "The bounty has not been collected yet: the backup can not be restored before the restore date";
            cleanup();
            if (true) return;
         }
         /* User Code */
          var secretBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV31secret);
         /* User Code */
          AV8based64Key = System.Convert.ToBase64String(secretBytes);
         AV12DecryptionResult = AV13EncryptionService.decrypt(AV17group_sdt.gxTpr_Encryptedtextshare, AV8based64Key);
         AV31secret = "";
         AV8based64Key = "";
         if ( ! AV12DecryptionResult.gxTpr_Success )
         {
            AV14error = "We couldn't decrypt the second share with the bounty key";
            cleanup();
            if (true) return;
         }
         AV34share2 = AV12DecryptionResult.gxTpr_Decryptedtext;
         AV12DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         /* Execute user subroutine: 'LOOK FOR OTHERS SHARES' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            /* Execute user subroutine: 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV34share2 = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            /* Execute user subroutine: 'SEND COMBINED SHARES TO GROUP MEMBERS' */
            S131 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV29restored = AV25numOfSharedWasReach;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOOK FOR OTHERS SHARES' Routine */
         returnInSub = false;
         AV38GXV1 = 1;
         while ( AV38GXV1 <= AV17group_sdt.gxTpr_Contact.Count )
         {
            AV20groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV38GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV20groupContact.gxTpr_Contactusername), StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) != 0 )
            {
               AV11contactFound = false;
               AV39GXV2 = 1;
               while ( AV39GXV2 <= AV18group_sdt_my.gxTpr_Contact.Count )
               {
                  AV21groupContactMy = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV18group_sdt_my.gxTpr_Contact.Item(AV39GXV2));
                  if ( StringUtil.StrCmp(AV21groupContactMy.gxTpr_Contactusername, AV20groupContact.gxTpr_Contactusername) == 0 )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21groupContactMy.gxTpr_Cleartextshare)) )
                     {
                        if ( AV21groupContactMy.gxTpr_Numofsharesreached )
                        {
                           AV18group_sdt_my.gxTpr_Cleartextshare = AV21groupContactMy.gxTpr_Cleartextshare;
                           AV18group_sdt_my.gxTpr_Numofsharesreached = AV21groupContactMy.gxTpr_Numofsharesreached;
                           AV25numOfSharedWasReach = true;
                        }
                        else
                        {
                           AV36sharesToRecoverTemp.Clear();
                           AV36sharesToRecoverTemp.FromJSonString(AV21groupContactMy.gxTpr_Cleartextshare, null);
                           AV40GXV3 = 1;
                           while ( AV40GXV3 <= AV36sharesToRecoverTemp.Count )
                           {
                              AV26oneShare = ((string)AV36sharesToRecoverTemp.Item(AV40GXV3));
                              AV35sharesToRecover.Add(AV26oneShare, 0);
                              AV40GXV3 = (int)(AV40GXV3+1);
                           }
                        }
                        AV11contactFound = true;
                        if (true) break;
                     }
                  }
                  AV39GXV2 = (int)(AV39GXV2+1);
               }
               if ( ! AV11contactFound )
               {
                  GXt_char3 = AV15errorContact;
                  new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV20groupContact.gxTpr_Contactgroupid,  AV20groupContact.gxTpr_Contactgroupencpassword, out  AV19group_sdt_temp, out  GXt_char3) ;
                  AV15errorContact = GXt_char3;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15errorContact)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV19group_sdt_temp.gxTpr_Cleartextshare)) && ! AV19group_sdt_temp.gxTpr_Numofsharesreached )
                  {
                     AV36sharesToRecoverTemp.Clear();
                     AV36sharesToRecoverTemp.FromJSonString(AV19group_sdt_temp.gxTpr_Cleartextshare, null);
                     AV41GXV4 = 1;
                     while ( AV41GXV4 <= AV36sharesToRecoverTemp.Count )
                     {
                        AV26oneShare = ((string)AV36sharesToRecoverTemp.Item(AV41GXV4));
                        AV35sharesToRecover.Add(AV26oneShare, 0);
                        AV41GXV4 = (int)(AV41GXV4+1);
                     }
                  }
               }
            }
            AV38GXV1 = (int)(AV38GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' Routine */
         returnInSub = false;
         AV42GXV5 = 1;
         while ( AV42GXV5 <= AV17group_sdt.gxTpr_Contact.Count )
         {
            AV20groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV42GXV5));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV20groupContact.gxTpr_Contactusername), StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.distcrypt.decryptwithgroupskey(context ).execute(  AV20groupContact.gxTpr_Contactencryptedtext,  AV20groupContact.gxTpr_Contactencryptedkey, out  AV32share, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  AV37userShares.FromJSonString(AV32share, null);
                  AV32share = "";
                  if ( ! AV25numOfSharedWasReach )
                  {
                     AV18group_sdt_my.gxTpr_Cleartextshare = AV37userShares.ToJSonString(false);
                  }
                  AV43GXV6 = 1;
                  while ( AV43GXV6 <= AV37userShares.Count )
                  {
                     AV26oneShare = ((string)AV37userShares.Item(AV43GXV6));
                     AV35sharesToRecover.Add(AV26oneShare, 0);
                     AV43GXV6 = (int)(AV43GXV6+1);
                  }
                  if ( ! AV25numOfSharedWasReach && ( AV17group_sdt.gxTpr_Minimumshares > 1 ) && ( AV35sharesToRecover.Count >= AV17group_sdt.gxTpr_Minimumshares ) )
                  {
                     GXt_char3 = AV14error;
                     new GeneXus.Programs.shamirss.combineshares(context ).execute(  AV35sharesToRecover, out  AV33share1, out  GXt_char3) ;
                     AV14error = GXt_char3;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV33share1)) )
                     {
                        AV35sharesToRecover.Clear();
                        AV35sharesToRecover.Add(AV33share1, 0);
                        AV35sharesToRecover.Add(AV34share2, 0);
                        AV33share1 = "";
                        GXt_char3 = AV14error;
                        new GeneXus.Programs.shamirss.combineshares(context ).execute(  AV35sharesToRecover, out  AV28recoveredSecret, out  GXt_char3) ;
                        AV14error = GXt_char3;
                        AV35sharesToRecover.Clear();
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV28recoveredSecret)) )
                        {
                           AV18group_sdt_my.gxTpr_Numofsharesreached = true;
                           AV18group_sdt_my.gxTpr_Cleartextshare = AV28recoveredSecret;
                           AV25numOfSharedWasReach = true;
                        }
                        AV28recoveredSecret = "";
                     }
                  }
               }
               if (true) break;
            }
            AV42GXV5 = (int)(AV42GXV5+1);
         }
      }

      protected void S131( )
      {
         /* 'SEND COMBINED SHARES TO GROUP MEMBERS' Routine */
         returnInSub = false;
         AV19group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV19group_sdt_temp.gxTpr_Groupname = AV18group_sdt_my.gxTpr_Groupname;
         AV19group_sdt_temp.gxTpr_Grouptype = AV18group_sdt_my.gxTpr_Grouptype;
         AV19group_sdt_temp.gxTpr_Cleartextshare = AV18group_sdt_my.gxTpr_Cleartextshare;
         AV19group_sdt_temp.gxTpr_Numofsharesreached = AV18group_sdt_my.gxTpr_Numofsharesreached;
         AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV18group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid;
         AV24message_signature.gxTpr_Username = StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username);
         AV24message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV16externalUserPublic.gxTpr_Groupskeyinfo.gxTpr_Publickey);
         GXt_char3 = AV14error;
         GXt_char5 = AV24message_signature.gxTpr_Signature;
         new GeneXus.Programs.distcrypt.signwithgroupskey(context ).execute(  StringUtil.Trim( AV24message_signature.gxTpr_Username)+StringUtil.Trim( AV24message_signature.gxTpr_Grouppubkey), out  GXt_char5, out  GXt_char3) ;
         AV24message_signature.gxTpr_Signature = GXt_char5;
         AV14error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV14error = "There was a problem Signing the invitation: " + AV14error;
            returnInSub = true;
            if (true) return;
         }
         AV19group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV24message_signature.gxTpr_Signature);
         AV44GXV7 = 1;
         while ( AV44GXV7 <= AV17group_sdt.gxTpr_Contact.Count )
         {
            AV20groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV17group_sdt.gxTpr_Contact.Item(AV44GXV7));
            if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV20groupContact.gxTpr_Contactusername), StringUtil.Trim( AV16externalUserPublic.gxTpr_Userinfo.gxTpr_Username)) == 0 ) )
            {
               AV30sdt_message.gxTpr_Id = Guid.NewGuid( );
               GXt_int6 = 0;
               new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int6) ;
               AV30sdt_message.gxTpr_Datetimeunix = GXt_int6;
               AV30sdt_message.gxTpr_Messagetype = 100;
               AV30sdt_message.gxTpr_Message = AV19group_sdt_temp.ToJSonString(false, true);
               AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
               AV10contact.gxTpr_Username = StringUtil.Trim( AV20groupContact.gxTpr_Contactusername);
               AV10contact.gxTpr_Messagepubkey = StringUtil.Trim( AV20groupContact.gxTpr_Contactuserpubkey);
               GXt_char5 = AV14error;
               new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV10contact,  AV30sdt_message, out  GXt_char5) ;
               AV14error = GXt_char5;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  AV14error = "There was a problem sending the Message to the Group: " + AV14error;
                  returnInSub = true;
                  if (true) return;
               }
            }
            AV44GXV7 = (int)(AV44GXV7+1);
         }
         GXt_char5 = AV14error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV18group_sdt_my,  StringUtil.Trim( AV18group_sdt_my.gxTpr_Encpassword), out  AV23grpupId, out  GXt_char5) ;
         AV14error = GXt_char5;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            GXt_char5 = AV14error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV18group_sdt_my, out  GXt_char5) ;
            AV14error = GXt_char5;
         }
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
         AV14error = "";
         AV35sharesToRecover = new GxSimpleCollection<string>();
         AV16externalUserPublic = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         GXt_SdtExternalUserPublic1 = new GeneXus.Programs.distcrypt.SdtExternalUserPublic(context);
         AV18group_sdt_my = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV27oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem4 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV31secret = "";
         AV8based64Key = "";
         AV12DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV13EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         AV34share2 = "";
         AV20groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV21groupContactMy = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV36sharesToRecoverTemp = new GxSimpleCollection<string>();
         AV26oneShare = "";
         AV15errorContact = "";
         AV19group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV32share = "";
         AV37userShares = new GxSimpleCollection<string>();
         AV33share1 = "";
         AV28recoveredSecret = "";
         AV24message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         GXt_char3 = "";
         AV30sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV10contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV23grpupId = Guid.Empty;
         GXt_char5 = "";
         /* GeneXus formulas. */
      }

      private int AV38GXV1 ;
      private int AV39GXV2 ;
      private int AV40GXV3 ;
      private int AV41GXV4 ;
      private int AV42GXV5 ;
      private int AV43GXV6 ;
      private int AV44GXV7 ;
      private long GXt_int6 ;
      private string AV14error ;
      private string AV31secret ;
      private string AV8based64Key ;
      private string AV15errorContact ;
      private string AV28recoveredSecret ;
      private string GXt_char3 ;
      private string GXt_char5 ;
      private bool AV29restored ;
      private bool AV25numOfSharedWasReach ;
      private bool AV9bountyActive ;
      private bool returnInSub ;
      private bool AV11contactFound ;
      private string AV34share2 ;
      private string AV26oneShare ;
      private string AV32share ;
      private string AV33share1 ;
      private Guid AV22groupId ;
      private Guid AV23grpupId ;
      private GxSimpleCollection<string> AV35sharesToRecover ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic AV16externalUserPublic ;
      private GeneXus.Programs.distcrypt.SdtExternalUserPublic GXt_SdtExternalUserPublic1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV18group_sdt_my ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT2 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV27oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem4 ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV12DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV13EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV20groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV21groupContactMy ;
      private GxSimpleCollection<string> AV36sharesToRecoverTemp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV19group_sdt_temp ;
      private GxSimpleCollection<string> AV37userShares ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV24message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV30sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV10contact ;
      private bool aP1_restored ;
      private string aP2_error ;
   }

}
