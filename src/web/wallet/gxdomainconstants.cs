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
   public class gxdomainconstants
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomainconstants ()
      {
         domain["TempPassword"] = "Temp Password";
         domain["TempPINAuthenticator"] = "Temp PIN";
         domain["Wallet"] = "Wallet";
         domain["Key"] = "Key";
         domain["ExtendedKey"] = "Extended Key";
         domain["Wallets"] = "Wallet Directory";
         domain[".json"] = "Wallet File Extension";
         domain["*.json"] = "Wallet Directory Extension";
         domain["WalletAllLines"] = "Wallet All Lines";
         domain["*.note"] = "Notes Directory Extension";
         domain[".note"] = "Note File Extension";
         domain["Notes"] = "Notes";
         domain["notes.json"] = "File All Notes";
         domain["Files"] = "Files";
         domain["files.json"] = "Files All Files";
         domain["HistoryWithBalance"] = "History With Balance";
         domain["transactions.trn"] = "Transactions File";
         domain["AllKeys"] = "All Keys";
         domain["ChangeAddressess"] = "Change Addressess";
         domain["ReceiveAddressess"] = "Receive Addressess";
         domain["AllAdresssess"] = "All Addressess";
         domain["0.00001000"] = "Fee Used For Estimates";
         domain["authenticators.auth"] = "Authenticators File";
         domain["AuthenticatorSession"] = "Authenticator Session";
         domain["connection.conf"] = "Rest Connection File";
         domain["LoginDistCryptKey"] = "Login Dist Crypt Key";
         domain["contacts.enc"] = "Contacts File";
         domain["Contact_Session"] = "Contact Session";
         domain["Comqueue"] = "Com Queue";
         domain[".queue"] = "Com Queue File Extension";
         domain["Chat"] = "Chat Directory";
         domain["gropus.enc"] = "Groups File";
         domain["DefaultJasonKey"] = "Dafault Jason Key";
         domain["FileEncKey"] = "File Enc Key";
         domain["LastJasonEncKey"] = "Last Jason Enc Key";
         domain[".gtrn"] = "Group Transacton Extension";
         domain["MULTISIGNATURE IN PROGRESS"] = "Temp Mul Sig In Progress";
         domain["PublicTempStorage"] = "PublicTempStorage";
         domain["PrivateTempStorage"] = "PrivateTempStorage";
         domain["electrum.conf"] = "Electrum Config File";
         domain["nostr.conf"] = "Nostr Config File";
         domain["encpasswords.enc"] = "Passwords File";
         domain["hsm.dat"] = "HSMFile";
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
            domainMap["TempPassword"] = "TempPassword";
            domainMap["TempPIN"] = "TempPINAuthenticator";
            domainMap["Wallet"] = "Wallet";
            domainMap["Key"] = "Key";
            domainMap["ExtendedKey"] = "ExtendedKey";
            domainMap["WalletDirectory"] = "Wallets";
            domainMap["WalletFileExtension"] = ".json";
            domainMap["WalletDirectoryExtension"] = "*.json";
            domainMap["WalletAllLines"] = "WalletAllLines";
            domainMap["NotesDirectoryExtension"] = "*.note";
            domainMap["NoteFileExtension"] = ".note";
            domainMap["Notes"] = "Notes";
            domainMap["FileAllNotes"] = "notes.json";
            domainMap["Files"] = "Files";
            domainMap["FilesAllFiles"] = "files.json";
            domainMap["HistoryWithBalance"] = "HistoryWithBalance";
            domainMap["TransactionsFile"] = "transactions.trn";
            domainMap["AllKeys"] = "AllKeys";
            domainMap["ChangeAddressess"] = "ChangeAddressess";
            domainMap["ReceiveAddressess"] = "ReceiveAddressess";
            domainMap["AllAddressess"] = "AllAdresssess";
            domainMap["FeeUsedForEstimates"] = "0.00001000";
            domainMap["AuthenticatorsFile"] = "authenticators.auth";
            domainMap["AuthenticatorSession"] = "AuthenticatorSession";
            domainMap["RestConnectionFile"] = "connection.conf";
            domainMap["LoginDistCryptKey"] = "LoginDistCryptKey";
            domainMap["ContactsFile"] = "contacts.enc";
            domainMap["ContactSession"] = "Contact_Session";
            domainMap["ComQueue"] = "Comqueue";
            domainMap["ComQueueFileExtension"] = ".queue";
            domainMap["ChatDirectory"] = "Chat";
            domainMap["GroupsFile"] = "gropus.enc";
            domainMap["DafaultJasonKey"] = "DefaultJasonKey";
            domainMap["FileEncKey"] = "FileEncKey";
            domainMap["LastJasonEncKey"] = "LastJasonEncKey";
            domainMap["GroupTransactonExtension"] = ".gtrn";
            domainMap["TempMulSigInProgress"] = "MULTISIGNATURE IN PROGRESS";
            domainMap["PublicTempStorage"] = "PublicTempStorage";
            domainMap["PrivateTempStorage"] = "PrivateTempStorage";
            domainMap["ElectrumConfigFile"] = "electrum.conf";
            domainMap["NostrConfigFile"] = "nostr.conf";
            domainMap["PasswordsFile"] = "encpasswords.enc";
            domainMap["HSMFile"] = "hsm.dat";
         }
         return (string)domainMap[key] ;
      }

   }

}
