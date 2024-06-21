using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.nostr {
   public class gxdomainmessagetype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainmessagetype ()
      {
         domain[(int)10] = "Ping";
         domain[(int)20] = "Ping_Ack";
         domain[(int)30] = "Invite User";
         domain[(int)40] = "Invite User Accept";
         domain[(int)50] = "Send Chat";
         domain[(int)60] = "Recive Chat";
         domain[(int)70] = "Invite Group";
         domain[(int)80] = "Invite Group Accept";
         domain[(int)90] = "group Activated";
         domain[(int)100] = "notify Group Restore";
         domain[(int)110] = "Mu Sig One Signature";
         domain[(int)120] = "Mu Sig Finished Signature";
      }

      public static string getDescription( IGxContext context ,
                                           int key )
      {
         string value;
         value = (string)(domain[key]==null?"":domain[key]);
         return value ;
      }

      public static GxSimpleCollection<int> getValues( )
      {
         GxSimpleCollection<int> value = new GxSimpleCollection<int>();
         ArrayList aKeys = new ArrayList(domain.Keys);
         aKeys.Sort();
         foreach (int key in aKeys)
         {
            value.Add(key);
         }
         return value;
      }

      public static int getValue( string key )
      {
         if(domainMap == null)
         {
            domainMap = new Hashtable();
            domainMap["Ping"] = (int)10;
            domainMap["Ping_Ack"] = (int)20;
            domainMap["InviteUser"] = (int)30;
            domainMap["InviteUserAccept"] = (int)40;
            domainMap["SendChat"] = (int)50;
            domainMap["ReciveChat"] = (int)60;
            domainMap["InviteGroup"] = (int)70;
            domainMap["InviteGroupAccept"] = (int)80;
            domainMap["groupActivated"] = (int)90;
            domainMap["notifyGroupRestore"] = (int)100;
            domainMap["MuSigOneSignature"] = (int)110;
            domainMap["MuSigFinishedSignature"] = (int)120;
         }
         return (int)domainMap[key] ;
      }

   }

}
