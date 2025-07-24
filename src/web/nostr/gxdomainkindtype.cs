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
namespace GeneXus.Programs.nostr {
   public class gxdomainkindtype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainkindtype ()
      {
         domain[(int)0] = "Metadata";
         domain[(int)1] = "Short Text Note";
         domain[(int)4] = "Encrypted Dm";
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
            domainMap["Metadata"] = (int)0;
            domainMap["ShortTextNote"] = (int)1;
            domainMap["EncryptedDm"] = (int)4;
         }
         return (int)domainMap[key] ;
      }

   }

}
