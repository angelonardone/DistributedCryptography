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
   public class gxdomainkeypaths
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainkeypaths ()
      {
         domain["m/44'/0'/0'"] = "BIP44_Main Net";
         domain["m/44'/1'/0'"] = "BIP44_Test Net";
         domain["m/44'/1'/0'"] = "BIP44_Reg Test";
         domain["m/49'/0'/0'"] = "BIP49_Main Net";
         domain["m/49'/1'/0'"] = "BIP49_Test Net";
         domain["m/49'/1'/0'"] = "BIP49_Reg Test";
         domain["m/84'/0'/0'"] = "BIP84_Main Net";
         domain["m/84'/1'/0'"] = "BIP84_Test Net";
         domain["m/84'/1'/0'"] = "BIP84_Reg Test";
         domain["m/86'/0'/0'"] = "BIP86_Main Net";
         domain["m/86'/1'/0'"] = "BIP86_Test Net";
         domain["m/86'/1'/0'"] = "BIP86_Reg Test";
         domain["0"] = "Receiving";
         domain["1"] = "Change";
         domain["2"] = "Multi Signature";
         domain["3"] = "Mu Sig Change";
         domain["1000"] = "Login Dist Crypt Relative";
         domain["2000"] = "Chat Relative";
         domain["3000"] = "Group Relative";
         domain["4000"] = "Json Files Relative";
         domain["5000"] = "File Enc Relative";
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
            domainMap["BIP44_MainNet"] = "m/44'/0'/0'";
            domainMap["BIP44_TestNet"] = "m/44'/1'/0'";
            domainMap["BIP44_RegTest"] = "m/44'/1'/0'";
            domainMap["BIP49_MainNet"] = "m/49'/0'/0'";
            domainMap["BIP49_TestNet"] = "m/49'/1'/0'";
            domainMap["BIP49_RegTest"] = "m/49'/1'/0'";
            domainMap["BIP84_MainNet"] = "m/84'/0'/0'";
            domainMap["BIP84_TestNet"] = "m/84'/1'/0'";
            domainMap["BIP84_RegTest"] = "m/84'/1'/0'";
            domainMap["BIP86_MainNet"] = "m/86'/0'/0'";
            domainMap["BIP86_TestNet"] = "m/86'/1'/0'";
            domainMap["BIP86_RegTest"] = "m/86'/1'/0'";
            domainMap["Receiving"] = "0";
            domainMap["Change"] = "1";
            domainMap["MiuSigReceiving"] = "2";
            domainMap["MuSigChange"] = "3";
            domainMap["LoginDistCryptRelative"] = "1000";
            domainMap["ChatRelative"] = "2000";
            domainMap["GroupRelative"] = "3000";
            domainMap["JsonFilesRelative"] = "4000";
            domainMap["FileEncRelative"] = "5000";
         }
         return (string)domainMap[key] ;
      }

   }

}
