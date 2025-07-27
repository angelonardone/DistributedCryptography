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
namespace GeneXus.Programs.distcrypt {
   public class gxdomainconstants
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainconstants ()
      {
         domain["DistCryptExternalUser"] = "DisCrypt External User";
         domain["DistCryptExternalToken"] = "DistCrypt External Token";
         domain["distcrypt.conf"] = "DistCrypt Config File";
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
            domainMap["DistCryptExternalUser"] = "DistCryptExternalUser";
            domainMap["DistCryptExternalToken"] = "DistCryptExternalToken";
            domainMap["DistCryptConfigFile"] = "distcrypt.conf";
         }
         return (string)domainMap[key] ;
      }

   }

}
