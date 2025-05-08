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
namespace GeneXus.Programs {
   public class gxdomainswipedirection
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainswipedirection ()
      {
         domain[(short)1] = "Up";
         domain[(short)2] = "Down";
         domain[(short)3] = "Left";
         domain[(short)4] = "Right";
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
            domainMap["Up"] = (short)1;
            domainMap["Down"] = (short)2;
            domainMap["Left"] = (short)3;
            domainMap["Right"] = (short)4;
         }
         return (short)domainMap[key] ;
      }

   }

}
