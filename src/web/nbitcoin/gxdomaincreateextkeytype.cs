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
   public class gxdomaincreateextkeytype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaincreateextkeytype ()
      {
         domain[(short)10] = "Random";
         domain[(short)20] = "Random Mnemonic";
         domain[(short)30] = "From Mnemonic";
         domain[(short)40] = "From Private Key And Chain Code";
         domain[(short)50] = "From Seed";
         domain[(short)60] = "From Extended Public Key";
         domain[(short)70] = "From Extended Private Key";
         domain[(short)80] = "From Enc WIFand Chain Code";
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
            domainMap["RandomMnemonic"] = (short)20;
            domainMap["FromMnemonic"] = (short)30;
            domainMap["FromPrivateKeyAndChainCode"] = (short)40;
            domainMap["FromSeed"] = (short)50;
            domainMap["FromExtendedPublicKey"] = (short)60;
            domainMap["FromExtendedPrivateKey"] = (short)70;
            domainMap["FromEncWIFandChainCode"] = (short)80;
         }
         return (short)domainMap[key] ;
      }

   }

}
