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
   public class gxdomaincreatekeytype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaincreatekeytype ()
      {
         domain[(short)10] = "Random";
         domain[(short)20] = "Brain Wallet";
         domain[(short)30] = "From Private Key";
         domain[(short)100] = "From WIF";
         domain[(short)110] = "From Enc WIF";
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
            domainMap["Random"] = (short)10;
            domainMap["BrainWallet"] = (short)20;
            domainMap["FromPrivateKey"] = (short)30;
            domainMap["FromWIF"] = (short)100;
            domainMap["FromEncWIF"] = (short)110;
         }
         return (short)domainMap[key] ;
      }

   }

}
