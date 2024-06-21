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
namespace GeneXus.Programs.nbitcoin {
   public class gxdomainmnemonicnumberwords
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainmnemonicnumberwords ()
      {
         domain[(short)12] = "12";
         domain[(short)15] = "15";
         domain[(short)18] = "18";
         domain[(short)21] = "21";
         domain[(short)24] = "24";
      }

      public static string getDescription( IGxContext context ,
                                           short key )
      {
         string value;
         value = (string)(domain[key]==null?"":domain[key]);
         return value ;
      }

      public static GxSimpleCollection<short> getValues( )
      {
         GxSimpleCollection<short> value = new GxSimpleCollection<short>();
         ArrayList aKeys = new ArrayList(domain.Keys);
         aKeys.Sort();
         foreach (short key in aKeys)
         {
            value.Add(key);
         }
         return value;
      }

      public static short getValue( string key )
      {
         if(domainMap == null)
         {
            domainMap = new Hashtable();
            domainMap["N12"] = (short)12;
            domainMap["N15"] = (short)15;
            domainMap["N18"] = (short)18;
            domainMap["N21"] = (short)21;
            domainMap["N24"] = (short)24;
         }
         return (short)domainMap[key] ;
      }

   }

}
