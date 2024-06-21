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
   public class gxdomaintoastrposition
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaintoastrposition ()
      {
         domain["toast-top-right"] = "Top Right";
         domain["toast-bottom-right"] = "Bottom Right";
         domain["toast-bottom-left"] = "Bottom Left";
         domain["toast-top-left"] = "Top Left";
         domain["toast-top-full-width"] = "Top Full Width";
         domain["toast-bottom-full-width"] = "Bottom Full Width";
         domain["toast-top-center"] = "Top Center";
         domain["toast-bottom-center"] = "Bottom Center";
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
            domainMap["TopRight"] = "toast-top-right";
            domainMap["BottomRight"] = "toast-bottom-right";
            domainMap["BottomLeft"] = "toast-bottom-left";
            domainMap["TopLeft"] = "toast-top-left";
            domainMap["TopFullWidth"] = "toast-top-full-width";
            domainMap["BottomFullWidth"] = "toast-bottom-full-width";
            domainMap["TopCenter"] = "toast-top-center";
            domainMap["BottomCenter"] = "toast-bottom-center";
         }
         return (string)domainMap[key] ;
      }

   }

}
