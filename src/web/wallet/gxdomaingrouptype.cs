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
namespace GeneXus.Programs.wallet {
   public class gxdomaingrouptype
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaingrouptype ()
      {
         domain[(short)0] = "Select Group Type";
         domain[(short)10] = "Wallet Backup";
         domain[(short)200] = "Delegation Multi-Signature Wallet";
         domain[(short)300] = "Shared based Multi-Signature Wallet";
         domain[(short)800] = "Synchronous Delegation Mult-Signature Wallet";
         domain[(short)600] = "Concensus File Vault";
         domain[(short)700] = "Concensus Timed File Vault";
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
            domainMap["SelectGroupType"] = (short)0;
            domainMap["WalletBackup"] = (short)10;
            domainMap["DelegationMultiSignature"] = (short)200;
            domainMap["SharedMultiSignature"] = (short)300;
            domainMap["DelegationMultiSignature2"] = (short)800;
            domainMap["ConcensusFileVault"] = (short)600;
            domainMap["ConcensusTimedFileVault"] = (short)700;
         }
         return (short)domainMap[key] ;
      }

   }

}
