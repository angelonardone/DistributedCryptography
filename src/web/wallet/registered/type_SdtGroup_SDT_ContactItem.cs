/*
				   File: type_SdtGroup_SDT_ContactItem
			Description: Contact
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using GeneXus.Programs;
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Group_SDT.ContactItem")]
	[XmlType(TypeName="Group_SDT.ContactItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroup_SDT_ContactItem : GxUserType
	{
		public SdtGroup_SDT_ContactItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroup_SDT_ContactItem_Contactprivatename = "";

			gxTv_SdtGroup_SDT_ContactItem_Contactusername = "";

			gxTv_SdtGroup_SDT_ContactItem_Contactuserpubkey = "";

			gxTv_SdtGroup_SDT_ContactItem_Contactencryptedkey = "";

			gxTv_SdtGroup_SDT_ContactItem_Contactencryptedtext = "";

			gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroup_SDT_ContactItem_Contactgroupencpassword = "";

			gxTv_SdtGroup_SDT_ContactItem_Cleartextshare = "";

			gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigreceiving = "";

			gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigchange = "";

		}

		public SdtGroup_SDT_ContactItem(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("contactId", gxTpr_Contactid, false);


			AddObjectProperty("numShares", gxTpr_Numshares, false);


			AddObjectProperty("contactPrivateName", gxTpr_Contactprivatename, false);


			AddObjectProperty("contactUserName", gxTpr_Contactusername, false);


			AddObjectProperty("contactUserPubKey", gxTpr_Contactuserpubkey, false);


			AddObjectProperty("contactencryptedKey", gxTpr_Contactencryptedkey, false);


			AddObjectProperty("contactEncryptedText", gxTpr_Contactencryptedtext, false);


			datetime_STZ = gxTpr_Contactinvitationsent;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("contactInvitationSent", sDateCnv, false);



			datetime_STZ = gxTpr_Contactinvitacionaccepted;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("contactInvitacionAccepted", sDateCnv, false);



			AddObjectProperty("contactInvitationDelined", gxTpr_Contactinvitationdelined, false);


			AddObjectProperty("contactInviSent", gxTpr_Contactinvisent, false);


			AddObjectProperty("contactInvRec", gxTpr_Contactinvrec, false);


			AddObjectProperty("contactGroupId", gxTpr_Contactgroupid, false);


			AddObjectProperty("contactGroupEncPassword", gxTpr_Contactgroupencpassword, false);


			AddObjectProperty("clearTextShare", gxTpr_Cleartextshare, false);


			AddObjectProperty("numOfSharesReached", gxTpr_Numofsharesreached, false);


			AddObjectProperty("extPubKeyMultiSigReceiving", gxTpr_Extpubkeymultisigreceiving, false);


			AddObjectProperty("extPubKeyMultiSigChange", gxTpr_Extpubkeymultisigchange, false);

			if (gxTv_SdtGroup_SDT_ContactItem_Musigsignatures != null)
			{
				AddObjectProperty("muSigSignatures", gxTv_SdtGroup_SDT_ContactItem_Musigsignatures, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contactId")]
		[XmlElement(ElementName="contactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactid; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="numShares")]
		[XmlElement(ElementName="numShares")]
		public short gxTpr_Numshares
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Numshares; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Numshares = value;
				SetDirty("Numshares");
			}
		}




		[SoapElement(ElementName="contactPrivateName")]
		[XmlElement(ElementName="contactPrivateName")]
		public string gxTpr_Contactprivatename
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactprivatename; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactprivatename = value;
				SetDirty("Contactprivatename");
			}
		}




		[SoapElement(ElementName="contactUserName")]
		[XmlElement(ElementName="contactUserName")]
		public string gxTpr_Contactusername
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactusername; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactusername = value;
				SetDirty("Contactusername");
			}
		}




		[SoapElement(ElementName="contactUserPubKey")]
		[XmlElement(ElementName="contactUserPubKey")]
		public string gxTpr_Contactuserpubkey
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactuserpubkey; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactuserpubkey = value;
				SetDirty("Contactuserpubkey");
			}
		}




		[SoapElement(ElementName="contactencryptedKey")]
		[XmlElement(ElementName="contactencryptedKey")]
		public string gxTpr_Contactencryptedkey
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactencryptedkey; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactencryptedkey = value;
				SetDirty("Contactencryptedkey");
			}
		}




		[SoapElement(ElementName="contactEncryptedText")]
		[XmlElement(ElementName="contactEncryptedText")]
		public string gxTpr_Contactencryptedtext
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactencryptedtext; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactencryptedtext = value;
				SetDirty("Contactencryptedtext");
			}
		}



		[SoapElement(ElementName="contactInvitationSent")]
		[XmlElement(ElementName="contactInvitationSent" , IsNullable=true)]
		public string gxTpr_Contactinvitationsent_Nullable
		{
			get {
				if ( gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent).value ;
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Contactinvitationsent
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent = value;
				SetDirty("Contactinvitationsent");
			}
		}


		[SoapElement(ElementName="contactInvitacionAccepted")]
		[XmlElement(ElementName="contactInvitacionAccepted" , IsNullable=true)]
		public string gxTpr_Contactinvitacionaccepted_Nullable
		{
			get {
				if ( gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted).value ;
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Contactinvitacionaccepted
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted = value;
				SetDirty("Contactinvitacionaccepted");
			}
		}



		[SoapElement(ElementName="contactInvitationDelined")]
		[XmlElement(ElementName="contactInvitationDelined")]
		public bool gxTpr_Contactinvitationdelined
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactinvitationdelined; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvitationdelined = value;
				SetDirty("Contactinvitationdelined");
			}
		}




		[SoapElement(ElementName="contactInviSent")]
		[XmlElement(ElementName="contactInviSent")]
		public bool gxTpr_Contactinvisent
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactinvisent; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvisent = value;
				SetDirty("Contactinvisent");
			}
		}




		[SoapElement(ElementName="contactInvRec")]
		[XmlElement(ElementName="contactInvRec")]
		public bool gxTpr_Contactinvrec
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactinvrec; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactinvrec = value;
				SetDirty("Contactinvrec");
			}
		}




		[SoapElement(ElementName="contactGroupId")]
		[XmlElement(ElementName="contactGroupId")]
		public Guid gxTpr_Contactgroupid
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactgroupid; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactgroupid = value;
				SetDirty("Contactgroupid");
			}
		}




		[SoapElement(ElementName="contactGroupEncPassword")]
		[XmlElement(ElementName="contactGroupEncPassword")]
		public string gxTpr_Contactgroupencpassword
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Contactgroupencpassword; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Contactgroupencpassword = value;
				SetDirty("Contactgroupencpassword");
			}
		}




		[SoapElement(ElementName="clearTextShare")]
		[XmlElement(ElementName="clearTextShare")]
		public string gxTpr_Cleartextshare
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Cleartextshare; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Cleartextshare = value;
				SetDirty("Cleartextshare");
			}
		}




		[SoapElement(ElementName="numOfSharesReached")]
		[XmlElement(ElementName="numOfSharesReached")]
		public bool gxTpr_Numofsharesreached
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Numofsharesreached; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Numofsharesreached = value;
				SetDirty("Numofsharesreached");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigReceiving")]
		[XmlElement(ElementName="extPubKeyMultiSigReceiving")]
		public string gxTpr_Extpubkeymultisigreceiving
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigreceiving; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigreceiving = value;
				SetDirty("Extpubkeymultisigreceiving");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigChange")]
		[XmlElement(ElementName="extPubKeyMultiSigChange")]
		public string gxTpr_Extpubkeymultisigchange
		{
			get {
				return gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigchange; 
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigchange = value;
				SetDirty("Extpubkeymultisigchange");
			}
		}




		[SoapElement(ElementName="muSigSignatures" )]
		[XmlArray(ElementName="muSigSignatures"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> gxTpr_Musigsignatures_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGroup_SDT_ContactItem_Musigsignatures == null )
				{
					gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
				}
				return gxTv_SdtGroup_SDT_ContactItem_Musigsignatures;
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N = false;
				gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> gxTpr_Musigsignatures
		{
			get {
				if ( gxTv_SdtGroup_SDT_ContactItem_Musigsignatures == null )
				{
					gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures>( context, "MuSigSignatures", "");
				}
				gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N = false;
				SetDirty("Musigsignatures");
				return gxTv_SdtGroup_SDT_ContactItem_Musigsignatures ;
			}
			set {
				gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N = false;
				gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = value;
				SetDirty("Musigsignatures");
			}
		}

		public void gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_SetNull()
		{
			gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N = true;
			gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = null;
		}

		public bool gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_IsNull()
		{
			return gxTv_SdtGroup_SDT_ContactItem_Musigsignatures == null;
		}
		public bool ShouldSerializegxTpr_Musigsignatures_GXBaseCollection_Json()
		{
			return gxTv_SdtGroup_SDT_ContactItem_Musigsignatures != null && gxTv_SdtGroup_SDT_ContactItem_Musigsignatures.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtGroup_SDT_ContactItem_Contactprivatename = "";
			gxTv_SdtGroup_SDT_ContactItem_Contactusername = "";
			gxTv_SdtGroup_SDT_ContactItem_Contactuserpubkey = "";
			gxTv_SdtGroup_SDT_ContactItem_Contactencryptedkey = "";
			gxTv_SdtGroup_SDT_ContactItem_Contactencryptedtext = "";
			gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent = (DateTime)(DateTime.MinValue);
			gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted = (DateTime)(DateTime.MinValue);




			gxTv_SdtGroup_SDT_ContactItem_Contactgroupencpassword = "";
			gxTv_SdtGroup_SDT_ContactItem_Cleartextshare = "";

			gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigreceiving = "";
			gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigchange = "";

			gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N = true;

			datetime_STZ = (DateTime)(DateTime.MinValue);
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected DateTime datetime_STZ ;

		protected Guid gxTv_SdtGroup_SDT_ContactItem_Contactid;
		 

		protected short gxTv_SdtGroup_SDT_ContactItem_Numshares;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactprivatename;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactusername;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactuserpubkey;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactencryptedkey;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactencryptedtext;
		 

		protected DateTime gxTv_SdtGroup_SDT_ContactItem_Contactinvitationsent;
		 

		protected DateTime gxTv_SdtGroup_SDT_ContactItem_Contactinvitacionaccepted;
		 

		protected bool gxTv_SdtGroup_SDT_ContactItem_Contactinvitationdelined;
		 

		protected bool gxTv_SdtGroup_SDT_ContactItem_Contactinvisent;
		 

		protected bool gxTv_SdtGroup_SDT_ContactItem_Contactinvrec;
		 

		protected Guid gxTv_SdtGroup_SDT_ContactItem_Contactgroupid;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Contactgroupencpassword;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Cleartextshare;
		 

		protected bool gxTv_SdtGroup_SDT_ContactItem_Numofsharesreached;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigreceiving;
		 

		protected string gxTv_SdtGroup_SDT_ContactItem_Extpubkeymultisigchange;
		 
		protected bool gxTv_SdtGroup_SDT_ContactItem_Musigsignatures_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures> gxTv_SdtGroup_SDT_ContactItem_Musigsignatures = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"Group_SDT.ContactItem", Namespace="distributedcryptography")]
	public class SdtGroup_SDT_ContactItem_RESTInterface : GxGenericCollectionItem<SdtGroup_SDT_ContactItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroup_SDT_ContactItem_RESTInterface( ) : base()
		{	
		}

		public SdtGroup_SDT_ContactItem_RESTInterface( SdtGroup_SDT_ContactItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contactId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="contactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}

		[JsonPropertyName("numShares")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="numShares", Order=1)]
		public short gxTpr_Numshares
		{
			get { 
				return sdt.gxTpr_Numshares;

			}
			set { 
				sdt.gxTpr_Numshares = value;
			}
		}

		[JsonPropertyName("contactPrivateName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="contactPrivateName", Order=2)]
		public  string gxTpr_Contactprivatename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactprivatename);

			}
			set { 
				 sdt.gxTpr_Contactprivatename = value;
			}
		}

		[JsonPropertyName("contactUserName")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="contactUserName", Order=3)]
		public  string gxTpr_Contactusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactusername);

			}
			set { 
				 sdt.gxTpr_Contactusername = value;
			}
		}

		[JsonPropertyName("contactUserPubKey")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="contactUserPubKey", Order=4)]
		public  string gxTpr_Contactuserpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactuserpubkey);

			}
			set { 
				 sdt.gxTpr_Contactuserpubkey = value;
			}
		}

		[JsonPropertyName("contactencryptedKey")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="contactencryptedKey", Order=5)]
		public  string gxTpr_Contactencryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactencryptedkey);

			}
			set { 
				 sdt.gxTpr_Contactencryptedkey = value;
			}
		}

		[JsonPropertyName("contactEncryptedText")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="contactEncryptedText", Order=6)]
		public  string gxTpr_Contactencryptedtext
		{
			get { 
				return sdt.gxTpr_Contactencryptedtext;

			}
			set { 
				 sdt.gxTpr_Contactencryptedtext = value;
			}
		}

		[JsonPropertyName("contactInvitationSent")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="contactInvitationSent", Order=7)]
		public  string gxTpr_Contactinvitationsent
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Contactinvitationsent,context);

			}
			set { 
				sdt.gxTpr_Contactinvitationsent = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("contactInvitacionAccepted")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="contactInvitacionAccepted", Order=8)]
		public  string gxTpr_Contactinvitacionaccepted
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Contactinvitacionaccepted,context);

			}
			set { 
				sdt.gxTpr_Contactinvitacionaccepted = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("contactInvitationDelined")]
		[JsonPropertyOrder(9)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="contactInvitationDelined", Order=9)]
		public bool gxTpr_Contactinvitationdelined
		{
			get { 
				return sdt.gxTpr_Contactinvitationdelined;

			}
			set { 
				sdt.gxTpr_Contactinvitationdelined = value;
			}
		}

		[JsonPropertyName("contactInviSent")]
		[JsonPropertyOrder(10)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="contactInviSent", Order=10)]
		public bool gxTpr_Contactinvisent
		{
			get { 
				return sdt.gxTpr_Contactinvisent;

			}
			set { 
				sdt.gxTpr_Contactinvisent = value;
			}
		}

		[JsonPropertyName("contactInvRec")]
		[JsonPropertyOrder(11)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="contactInvRec", Order=11)]
		public bool gxTpr_Contactinvrec
		{
			get { 
				return sdt.gxTpr_Contactinvrec;

			}
			set { 
				sdt.gxTpr_Contactinvrec = value;
			}
		}

		[JsonPropertyName("contactGroupId")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="contactGroupId", Order=12)]
		public Guid gxTpr_Contactgroupid
		{
			get { 
				return sdt.gxTpr_Contactgroupid;

			}
			set { 
				sdt.gxTpr_Contactgroupid = value;
			}
		}

		[JsonPropertyName("contactGroupEncPassword")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="contactGroupEncPassword", Order=13)]
		public  string gxTpr_Contactgroupencpassword
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactgroupencpassword);

			}
			set { 
				 sdt.gxTpr_Contactgroupencpassword = value;
			}
		}

		[JsonPropertyName("clearTextShare")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="clearTextShare", Order=14)]
		public  string gxTpr_Cleartextshare
		{
			get { 
				return sdt.gxTpr_Cleartextshare;

			}
			set { 
				 sdt.gxTpr_Cleartextshare = value;
			}
		}

		[JsonPropertyName("numOfSharesReached")]
		[JsonPropertyOrder(15)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="numOfSharesReached", Order=15)]
		public bool gxTpr_Numofsharesreached
		{
			get { 
				return sdt.gxTpr_Numofsharesreached;

			}
			set { 
				sdt.gxTpr_Numofsharesreached = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigReceiving")]
		[JsonPropertyOrder(16)]
		[DataMember(Name="extPubKeyMultiSigReceiving", Order=16)]
		public  string gxTpr_Extpubkeymultisigreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigreceiving = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigChange")]
		[JsonPropertyOrder(17)]
		[DataMember(Name="extPubKeyMultiSigChange", Order=17)]
		public  string gxTpr_Extpubkeymultisigchange
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigchange);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigchange = value;
			}
		}

		[JsonPropertyName("muSigSignatures")]
		[JsonPropertyOrder(18)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="muSigSignatures", Order=18, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures_RESTInterface> gxTpr_Musigsignatures
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Musigsignatures_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.registered.SdtMuSigSignatures_RESTInterface>(sdt.gxTpr_Musigsignatures);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Musigsignatures);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroup_SDT_ContactItem sdt
		{
			get { 
				return (SdtGroup_SDT_ContactItem)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtGroup_SDT_ContactItem() ;
			}
		}
	}
	#endregion
}