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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet {
   public class gxdomainwallettype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainwallettype ()
      {
         domain["SelectWalletType"] = "Select Wallet Type";
         domain["ImportedWIF"] = "Imported WIF";
         domain["BrainWallet"] = "Brain Wallet";
         domain["BIP44"] = "BIP44 (Legacy)";
         domain["BIP49"] = "BIP49 (SegwitP2SH)";
         domain["BIP84"] = "BIP84 (native Segwit)";
         domain["BIP86"] = "BIP86 (Taproot P2TR)";
      }

      public static string getDescription( IGxContext context ,
                                           string key )
      {
         string rtkey;
         string value;
         rtkey = ((key==null) ? "" : StringUtil.Trim( (string)(key)));
         value = (string)(domain[rtkey]==null?"":domain[rtkey]);
         return value ;
      }

      public static GxSimpleCollection<string> getValues( )
      {
         GxSimpleCollection<string> value = new GxSimpleCollection<string>();
         ArrayList aKeys = new ArrayList(domain.Keys);
         aKeys.Sort();
         foreach (string key in aKeys)
         {
            value.Add(key);
         }
         return value;
      }

      public static string getValue( string key )
      {
         if(domainMap == null)
         {
            domainMap = new Hashtable();
            domainMap["SelectWalletType"] = "SelectWalletType";
            domainMap["ImportedWIF"] = "ImportedWIF";
            domainMap["BrainWallet"] = "BrainWallet";
            domainMap["BIP44"] = "BIP44";
            domainMap["BIP49"] = "BIP49";
            domainMap["BIP84"] = "BIP84";
            domainMap["BIP86"] = "BIP86";
         }
         return (string)domainMap[key] ;
      }

   }

}
