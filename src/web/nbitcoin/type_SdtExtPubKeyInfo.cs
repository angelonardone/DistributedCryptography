/*
				   File: type_SdtExtPubKeyInfo
			Description: ExtPubKeyInfo
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="ExtPubKeyInfo")]
	[XmlType(TypeName="ExtPubKeyInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExtPubKeyInfo : GxUserType
	{
		public SdtExtPubKeyInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtExtPubKeyInfo_Publickey = "";

			gxTv_SdtExtPubKeyInfo_Publickeysegwitp2sh = "";

			gxTv_SdtExtPubKeyInfo_Publickeysegwit = "";

			gxTv_SdtExtPubKeyInfo_Publickeytaproot = "";

			gxTv_SdtExtPubKeyInfo_Addresslegacy = "";

			gxTv_SdtExtPubKeyInfo_Addresssegwitp2sh = "";

			gxTv_SdtExtPubKeyInfo_Addresssegwit = "";

			gxTv_SdtExtPubKeyInfo_Addresstaproot = "";

			gxTv_SdtExtPubKeyInfo_Ec_publickey = "";

			gxTv_SdtExtPubKeyInfo_Fingerprint = "";

			gxTv_SdtExtPubKeyInfo_Parentfingerprint = "";

			gxTv_SdtExtPubKeyInfo_Keypath = "";

		}

		public SdtExtPubKeyInfo(IGxContext context)
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
			AddObjectProperty("PublicKey", gxTpr_Publickey, false);


			AddObjectProperty("PublicKeySegwitP2SH", gxTpr_Publickeysegwitp2sh, false);


			AddObjectProperty("PublicKeySegwit", gxTpr_Publickeysegwit, false);


			AddObjectProperty("PublicKeyTaproot", gxTpr_Publickeytaproot, false);


			AddObjectProperty("AddressLegacy", gxTpr_Addresslegacy, false);


			AddObjectProperty("AddressSegwitP2SH", gxTpr_Addresssegwitp2sh, false);


			AddObjectProperty("AddressSegwit", gxTpr_Addresssegwit, false);


			AddObjectProperty("AddressTaproot", gxTpr_Addresstaproot, false);


			AddObjectProperty("EC_PublicKey", gxTpr_Ec_publickey, false);


			AddObjectProperty("Fingerprint", gxTpr_Fingerprint, false);


			AddObjectProperty("ParentFingerprint", gxTpr_Parentfingerprint, false);


			AddObjectProperty("Depth", gxTpr_Depth, false);


			AddObjectProperty("Child", gxTpr_Child, false);


			AddObjectProperty("isHardended", gxTpr_Ishardended, false);


			AddObjectProperty("keyPath", gxTpr_Keypath, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PublicKey")]
		[XmlElement(ElementName="PublicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Publickey; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Publickey = value;
				SetDirty("Publickey");
			}
		}




		[SoapElement(ElementName="PublicKeySegwitP2SH")]
		[XmlElement(ElementName="PublicKeySegwitP2SH")]
		public string gxTpr_Publickeysegwitp2sh
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Publickeysegwitp2sh; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Publickeysegwitp2sh = value;
				SetDirty("Publickeysegwitp2sh");
			}
		}




		[SoapElement(ElementName="PublicKeySegwit")]
		[XmlElement(ElementName="PublicKeySegwit")]
		public string gxTpr_Publickeysegwit
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Publickeysegwit; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Publickeysegwit = value;
				SetDirty("Publickeysegwit");
			}
		}




		[SoapElement(ElementName="PublicKeyTaproot")]
		[XmlElement(ElementName="PublicKeyTaproot")]
		public string gxTpr_Publickeytaproot
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Publickeytaproot; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Publickeytaproot = value;
				SetDirty("Publickeytaproot");
			}
		}




		[SoapElement(ElementName="AddressLegacy")]
		[XmlElement(ElementName="AddressLegacy")]
		public string gxTpr_Addresslegacy
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Addresslegacy; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Addresslegacy = value;
				SetDirty("Addresslegacy");
			}
		}




		[SoapElement(ElementName="AddressSegwitP2SH")]
		[XmlElement(ElementName="AddressSegwitP2SH")]
		public string gxTpr_Addresssegwitp2sh
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Addresssegwitp2sh; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Addresssegwitp2sh = value;
				SetDirty("Addresssegwitp2sh");
			}
		}




		[SoapElement(ElementName="AddressSegwit")]
		[XmlElement(ElementName="AddressSegwit")]
		public string gxTpr_Addresssegwit
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Addresssegwit; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Addresssegwit = value;
				SetDirty("Addresssegwit");
			}
		}




		[SoapElement(ElementName="AddressTaproot")]
		[XmlElement(ElementName="AddressTaproot")]
		public string gxTpr_Addresstaproot
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Addresstaproot; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Addresstaproot = value;
				SetDirty("Addresstaproot");
			}
		}




		[SoapElement(ElementName="EC_PublicKey")]
		[XmlElement(ElementName="EC_PublicKey")]
		public string gxTpr_Ec_publickey
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Ec_publickey; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Ec_publickey = value;
				SetDirty("Ec_publickey");
			}
		}




		[SoapElement(ElementName="Fingerprint")]
		[XmlElement(ElementName="Fingerprint")]
		public string gxTpr_Fingerprint
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Fingerprint; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Fingerprint = value;
				SetDirty("Fingerprint");
			}
		}




		[SoapElement(ElementName="ParentFingerprint")]
		[XmlElement(ElementName="ParentFingerprint")]
		public string gxTpr_Parentfingerprint
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Parentfingerprint; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Parentfingerprint = value;
				SetDirty("Parentfingerprint");
			}
		}




		[SoapElement(ElementName="Depth")]
		[XmlElement(ElementName="Depth")]
		public short gxTpr_Depth
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Depth; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Depth = value;
				SetDirty("Depth");
			}
		}




		[SoapElement(ElementName="Child")]
		[XmlElement(ElementName="Child")]
		public long gxTpr_Child
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Child; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Child = value;
				SetDirty("Child");
			}
		}




		[SoapElement(ElementName="isHardended")]
		[XmlElement(ElementName="isHardended")]
		public bool gxTpr_Ishardended
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Ishardended; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Ishardended = value;
				SetDirty("Ishardended");
			}
		}




		[SoapElement(ElementName="keyPath")]
		[XmlElement(ElementName="keyPath")]
		public string gxTpr_Keypath
		{
			get {
				return gxTv_SdtExtPubKeyInfo_Keypath; 
			}
			set {
				gxTv_SdtExtPubKeyInfo_Keypath = value;
				SetDirty("Keypath");
			}
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
			gxTv_SdtExtPubKeyInfo_Publickey = "";
			gxTv_SdtExtPubKeyInfo_Publickeysegwitp2sh = "";
			gxTv_SdtExtPubKeyInfo_Publickeysegwit = "";
			gxTv_SdtExtPubKeyInfo_Publickeytaproot = "";
			gxTv_SdtExtPubKeyInfo_Addresslegacy = "";
			gxTv_SdtExtPubKeyInfo_Addresssegwitp2sh = "";
			gxTv_SdtExtPubKeyInfo_Addresssegwit = "";
			gxTv_SdtExtPubKeyInfo_Addresstaproot = "";
			gxTv_SdtExtPubKeyInfo_Ec_publickey = "";
			gxTv_SdtExtPubKeyInfo_Fingerprint = "";
			gxTv_SdtExtPubKeyInfo_Parentfingerprint = "";



			gxTv_SdtExtPubKeyInfo_Keypath = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExtPubKeyInfo_Publickey;
		 

		protected string gxTv_SdtExtPubKeyInfo_Publickeysegwitp2sh;
		 

		protected string gxTv_SdtExtPubKeyInfo_Publickeysegwit;
		 

		protected string gxTv_SdtExtPubKeyInfo_Publickeytaproot;
		 

		protected string gxTv_SdtExtPubKeyInfo_Addresslegacy;
		 

		protected string gxTv_SdtExtPubKeyInfo_Addresssegwitp2sh;
		 

		protected string gxTv_SdtExtPubKeyInfo_Addresssegwit;
		 

		protected string gxTv_SdtExtPubKeyInfo_Addresstaproot;
		 

		protected string gxTv_SdtExtPubKeyInfo_Ec_publickey;
		 

		protected string gxTv_SdtExtPubKeyInfo_Fingerprint;
		 

		protected string gxTv_SdtExtPubKeyInfo_Parentfingerprint;
		 

		protected short gxTv_SdtExtPubKeyInfo_Depth;
		 

		protected long gxTv_SdtExtPubKeyInfo_Child;
		 

		protected bool gxTv_SdtExtPubKeyInfo_Ishardended;
		 

		protected string gxTv_SdtExtPubKeyInfo_Keypath;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExtPubKeyInfo", Namespace="distributedcryptography")]
	public class SdtExtPubKeyInfo_RESTInterface : GxGenericCollectionItem<SdtExtPubKeyInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExtPubKeyInfo_RESTInterface( ) : base()
		{	
		}

		public SdtExtPubKeyInfo_RESTInterface( SdtExtPubKeyInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("PublicKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="PublicKey", Order=0)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}

		[JsonPropertyName("PublicKeySegwitP2SH")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="PublicKeySegwitP2SH", Order=1)]
		public  string gxTpr_Publickeysegwitp2sh
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickeysegwitp2sh);

			}
			set { 
				 sdt.gxTpr_Publickeysegwitp2sh = value;
			}
		}

		[JsonPropertyName("PublicKeySegwit")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="PublicKeySegwit", Order=2)]
		public  string gxTpr_Publickeysegwit
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickeysegwit);

			}
			set { 
				 sdt.gxTpr_Publickeysegwit = value;
			}
		}

		[JsonPropertyName("PublicKeyTaproot")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="PublicKeyTaproot", Order=3)]
		public  string gxTpr_Publickeytaproot
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickeytaproot);

			}
			set { 
				 sdt.gxTpr_Publickeytaproot = value;
			}
		}

		[JsonPropertyName("AddressLegacy")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="AddressLegacy", Order=4)]
		public  string gxTpr_Addresslegacy
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Addresslegacy);

			}
			set { 
				 sdt.gxTpr_Addresslegacy = value;
			}
		}

		[JsonPropertyName("AddressSegwitP2SH")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="AddressSegwitP2SH", Order=5)]
		public  string gxTpr_Addresssegwitp2sh
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Addresssegwitp2sh);

			}
			set { 
				 sdt.gxTpr_Addresssegwitp2sh = value;
			}
		}

		[JsonPropertyName("AddressSegwit")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="AddressSegwit", Order=6)]
		public  string gxTpr_Addresssegwit
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Addresssegwit);

			}
			set { 
				 sdt.gxTpr_Addresssegwit = value;
			}
		}

		[JsonPropertyName("AddressTaproot")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="AddressTaproot", Order=7)]
		public  string gxTpr_Addresstaproot
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Addresstaproot);

			}
			set { 
				 sdt.gxTpr_Addresstaproot = value;
			}
		}

		[JsonPropertyName("EC_PublicKey")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="EC_PublicKey", Order=8)]
		public  string gxTpr_Ec_publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Ec_publickey);

			}
			set { 
				 sdt.gxTpr_Ec_publickey = value;
			}
		}

		[JsonPropertyName("Fingerprint")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="Fingerprint", Order=9)]
		public  string gxTpr_Fingerprint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fingerprint);

			}
			set { 
				 sdt.gxTpr_Fingerprint = value;
			}
		}

		[JsonPropertyName("ParentFingerprint")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="ParentFingerprint", Order=10)]
		public  string gxTpr_Parentfingerprint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Parentfingerprint);

			}
			set { 
				 sdt.gxTpr_Parentfingerprint = value;
			}
		}

		[JsonPropertyName("Depth")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="Depth", Order=11)]
		public short gxTpr_Depth
		{
			get { 
				return sdt.gxTpr_Depth;

			}
			set { 
				sdt.gxTpr_Depth = value;
			}
		}

		[JsonPropertyName("Child")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="Child", Order=12)]
		public  string gxTpr_Child
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Child, 10, 0));

			}
			set { 
				sdt.gxTpr_Child = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("isHardended")]
		[JsonPropertyOrder(13)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isHardended", Order=13)]
		public bool gxTpr_Ishardended
		{
			get { 
				return sdt.gxTpr_Ishardended;

			}
			set { 
				sdt.gxTpr_Ishardended = value;
			}
		}

		[JsonPropertyName("keyPath")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="keyPath", Order=14)]
		public  string gxTpr_Keypath
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Keypath);

			}
			set { 
				 sdt.gxTpr_Keypath = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExtPubKeyInfo sdt
		{
			get { 
				return (SdtExtPubKeyInfo)Sdt;
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
				sdt = new SdtExtPubKeyInfo() ;
			}
		}
	}
	#endregion
}