/*
				   File: type_SdtRubyResult
			Description: RubyResult
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="RubyResult")]
	[XmlType(TypeName="RubyResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubyResult : GxUserType
	{
		public SdtRubyResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubyResult_Error = "";

			gxTv_SdtRubyResult_State = "";

			gxTv_SdtRubyResult_Version = "";

			gxTv_SdtRubyResult_Networks = "";

			gxTv_SdtRubyResult_Oraclekey = "";

			gxTv_SdtRubyResult_Encryptedoracle = "";

			gxTv_SdtRubyResult_Publickey = "";

			gxTv_SdtRubyResult_Oracleresult = "";

			gxTv_SdtRubyResult_Fingerprint = "";

			gxTv_SdtRubyResult_Account_N = true;

			gxTv_SdtRubyResult_Bip48_N = true;

			gxTv_SdtRubyResult_Login = "";

			gxTv_SdtRubyResult_Chat = "";

			gxTv_SdtRubyResult_Groups = "";

			gxTv_SdtRubyResult_Jsonfiles = "";

			gxTv_SdtRubyResult_Fileencryption = "";

			gxTv_SdtRubyResult_Hsm = "";

			gxTv_SdtRubyResult_Hextransaction = "";


		}

		public SdtRubyResult(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("state", gxTpr_State, false);


			AddObjectProperty("version", gxTpr_Version, false);


			AddObjectProperty("networks", gxTpr_Networks, false);


			AddObjectProperty("oracleKey", gxTpr_Oraclekey, false);


			AddObjectProperty("encryptedOracle", gxTpr_Encryptedoracle, false);


			AddObjectProperty("publicKey", gxTpr_Publickey, false);


			AddObjectProperty("unlocked", gxTpr_Unlocked, false);


			AddObjectProperty("oracleResult", gxTpr_Oracleresult, false);


			AddObjectProperty("fingerprint", gxTpr_Fingerprint, false);

			if (gxTv_SdtRubyResult_Account != null)
			{
				AddObjectProperty("account", gxTv_SdtRubyResult_Account, false);
			}
			if (gxTv_SdtRubyResult_Bip48 != null)
			{
				AddObjectProperty("bip48", gxTv_SdtRubyResult_Bip48, false);
			}

			AddObjectProperty("login", gxTpr_Login, false);


			AddObjectProperty("chat", gxTpr_Chat, false);


			AddObjectProperty("groups", gxTpr_Groups, false);


			AddObjectProperty("jsonFiles", gxTpr_Jsonfiles, false);


			AddObjectProperty("fileEncryption", gxTpr_Fileencryption, false);


			AddObjectProperty("hsm", gxTpr_Hsm, false);


			AddObjectProperty("hexTransaction", gxTpr_Hextransaction, false);


			AddObjectProperty("virtualSize", gxTpr_Virtualsize, false);

			if (gxTv_SdtRubyResult_Devices != null)
			{
				AddObjectProperty("devices", gxTv_SdtRubyResult_Devices, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtRubyResult_Success; 
			}
			set {
				gxTv_SdtRubyResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtRubyResult_Error; 
			}
			set {
				gxTv_SdtRubyResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="state")]
		[XmlElement(ElementName="state")]
		public string gxTpr_State
		{
			get {
				return gxTv_SdtRubyResult_State; 
			}
			set {
				gxTv_SdtRubyResult_State = value;
				SetDirty("State");
			}
		}




		[SoapElement(ElementName="version")]
		[XmlElement(ElementName="version")]
		public string gxTpr_Version
		{
			get {
				return gxTv_SdtRubyResult_Version; 
			}
			set {
				gxTv_SdtRubyResult_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="networks")]
		[XmlElement(ElementName="networks")]
		public string gxTpr_Networks
		{
			get {
				return gxTv_SdtRubyResult_Networks; 
			}
			set {
				gxTv_SdtRubyResult_Networks = value;
				SetDirty("Networks");
			}
		}




		[SoapElement(ElementName="oracleKey")]
		[XmlElement(ElementName="oracleKey")]
		public string gxTpr_Oraclekey
		{
			get {
				return gxTv_SdtRubyResult_Oraclekey; 
			}
			set {
				gxTv_SdtRubyResult_Oraclekey = value;
				SetDirty("Oraclekey");
			}
		}




		[SoapElement(ElementName="encryptedOracle")]
		[XmlElement(ElementName="encryptedOracle")]
		public string gxTpr_Encryptedoracle
		{
			get {
				return gxTv_SdtRubyResult_Encryptedoracle; 
			}
			set {
				gxTv_SdtRubyResult_Encryptedoracle = value;
				SetDirty("Encryptedoracle");
			}
		}




		[SoapElement(ElementName="publicKey")]
		[XmlElement(ElementName="publicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtRubyResult_Publickey; 
			}
			set {
				gxTv_SdtRubyResult_Publickey = value;
				SetDirty("Publickey");
			}
		}




		[SoapElement(ElementName="unlocked")]
		[XmlElement(ElementName="unlocked")]
		public bool gxTpr_Unlocked
		{
			get {
				return gxTv_SdtRubyResult_Unlocked; 
			}
			set {
				gxTv_SdtRubyResult_Unlocked = value;
				SetDirty("Unlocked");
			}
		}




		[SoapElement(ElementName="oracleResult")]
		[XmlElement(ElementName="oracleResult")]
		public string gxTpr_Oracleresult
		{
			get {
				return gxTv_SdtRubyResult_Oracleresult; 
			}
			set {
				gxTv_SdtRubyResult_Oracleresult = value;
				SetDirty("Oracleresult");
			}
		}




		[SoapElement(ElementName="fingerprint")]
		[XmlElement(ElementName="fingerprint")]
		public string gxTpr_Fingerprint
		{
			get {
				return gxTv_SdtRubyResult_Fingerprint; 
			}
			set {
				gxTv_SdtRubyResult_Fingerprint = value;
				SetDirty("Fingerprint");
			}
		}



		[SoapElement(ElementName="account")]
		[XmlElement(ElementName="account")]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTpr_Account
		{
			get {
				if ( gxTv_SdtRubyResult_Account == null )
				{
					gxTv_SdtRubyResult_Account = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
					SetDirty("Account");
				}
				return gxTv_SdtRubyResult_Account; 
			}
			set {
				gxTv_SdtRubyResult_Account = value;
				SetDirty("Account");
			}
		}
		public void gxTv_SdtRubyResult_Account_SetNull()
		{
			gxTv_SdtRubyResult_Account_N = true;
			gxTv_SdtRubyResult_Account = null;
		}

		public bool gxTv_SdtRubyResult_Account_IsNull()
		{
			return gxTv_SdtRubyResult_Account == null;
		}
		public bool ShouldSerializegxTpr_Account_Json()
		{
			return gxTv_SdtRubyResult_Account != null;

		}

		[SoapElement(ElementName="bip48")]
		[XmlElement(ElementName="bip48")]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTpr_Bip48
		{
			get {
				if ( gxTv_SdtRubyResult_Bip48 == null )
				{
					gxTv_SdtRubyResult_Bip48 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
					SetDirty("Bip48");
				}
				return gxTv_SdtRubyResult_Bip48; 
			}
			set {
				gxTv_SdtRubyResult_Bip48 = value;
				SetDirty("Bip48");
			}
		}
		public void gxTv_SdtRubyResult_Bip48_SetNull()
		{
			gxTv_SdtRubyResult_Bip48_N = true;
			gxTv_SdtRubyResult_Bip48 = null;
		}

		public bool gxTv_SdtRubyResult_Bip48_IsNull()
		{
			return gxTv_SdtRubyResult_Bip48 == null;
		}
		public bool ShouldSerializegxTpr_Bip48_Json()
		{
			return gxTv_SdtRubyResult_Bip48 != null;

		}


		[SoapElement(ElementName="login")]
		[XmlElement(ElementName="login")]
		public string gxTpr_Login
		{
			get {
				return gxTv_SdtRubyResult_Login; 
			}
			set {
				gxTv_SdtRubyResult_Login = value;
				SetDirty("Login");
			}
		}




		[SoapElement(ElementName="chat")]
		[XmlElement(ElementName="chat")]
		public string gxTpr_Chat
		{
			get {
				return gxTv_SdtRubyResult_Chat; 
			}
			set {
				gxTv_SdtRubyResult_Chat = value;
				SetDirty("Chat");
			}
		}




		[SoapElement(ElementName="groups")]
		[XmlElement(ElementName="groups")]
		public string gxTpr_Groups
		{
			get {
				return gxTv_SdtRubyResult_Groups; 
			}
			set {
				gxTv_SdtRubyResult_Groups = value;
				SetDirty("Groups");
			}
		}




		[SoapElement(ElementName="jsonFiles")]
		[XmlElement(ElementName="jsonFiles")]
		public string gxTpr_Jsonfiles
		{
			get {
				return gxTv_SdtRubyResult_Jsonfiles; 
			}
			set {
				gxTv_SdtRubyResult_Jsonfiles = value;
				SetDirty("Jsonfiles");
			}
		}




		[SoapElement(ElementName="fileEncryption")]
		[XmlElement(ElementName="fileEncryption")]
		public string gxTpr_Fileencryption
		{
			get {
				return gxTv_SdtRubyResult_Fileencryption; 
			}
			set {
				gxTv_SdtRubyResult_Fileencryption = value;
				SetDirty("Fileencryption");
			}
		}




		[SoapElement(ElementName="hsm")]
		[XmlElement(ElementName="hsm")]
		public string gxTpr_Hsm
		{
			get {
				return gxTv_SdtRubyResult_Hsm; 
			}
			set {
				gxTv_SdtRubyResult_Hsm = value;
				SetDirty("Hsm");
			}
		}




		[SoapElement(ElementName="hexTransaction")]
		[XmlElement(ElementName="hexTransaction")]
		public string gxTpr_Hextransaction
		{
			get {
				return gxTv_SdtRubyResult_Hextransaction; 
			}
			set {
				gxTv_SdtRubyResult_Hextransaction = value;
				SetDirty("Hextransaction");
			}
		}




		[SoapElement(ElementName="virtualSize")]
		[XmlElement(ElementName="virtualSize")]
		public long gxTpr_Virtualsize
		{
			get {
				return gxTv_SdtRubyResult_Virtualsize; 
			}
			set {
				gxTv_SdtRubyResult_Virtualsize = value;
				SetDirty("Virtualsize");
			}
		}




		[SoapElement(ElementName="devices" )]
		[XmlArray(ElementName="devices"  )]
		[XmlArrayItemAttribute(ElementName="devicesItem" , IsNullable=false )]
		public GXBaseCollection<SdtRubyResult_devicesItem> gxTpr_Devices
		{
			get {
				if ( gxTv_SdtRubyResult_Devices == null )
				{
					gxTv_SdtRubyResult_Devices = new GXBaseCollection<SdtRubyResult_devicesItem>( context, "RubyResult.devicesItem", "");
				}
				SetDirty("Devices");
				return gxTv_SdtRubyResult_Devices;
			}
			set {
				gxTv_SdtRubyResult_Devices_N = false;
				gxTv_SdtRubyResult_Devices = value;
				SetDirty("Devices");
			}
		}

		public void gxTv_SdtRubyResult_Devices_SetNull()
		{
			gxTv_SdtRubyResult_Devices_N = true;
			gxTv_SdtRubyResult_Devices = null;
		}

		public bool gxTv_SdtRubyResult_Devices_IsNull()
		{
			return gxTv_SdtRubyResult_Devices == null;
		}
		public bool ShouldSerializegxTpr_Devices_GxSimpleCollection_Json()
		{
			return gxTv_SdtRubyResult_Devices != null && gxTv_SdtRubyResult_Devices.Count > 0;

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
			gxTv_SdtRubyResult_Error = "";
			gxTv_SdtRubyResult_State = "";
			gxTv_SdtRubyResult_Version = "";
			gxTv_SdtRubyResult_Networks = "";
			gxTv_SdtRubyResult_Oraclekey = "";
			gxTv_SdtRubyResult_Encryptedoracle = "";
			gxTv_SdtRubyResult_Publickey = "";

			gxTv_SdtRubyResult_Oracleresult = "";
			gxTv_SdtRubyResult_Fingerprint = "";

			gxTv_SdtRubyResult_Account_N = true;


			gxTv_SdtRubyResult_Bip48_N = true;

			gxTv_SdtRubyResult_Login = "";
			gxTv_SdtRubyResult_Chat = "";
			gxTv_SdtRubyResult_Groups = "";
			gxTv_SdtRubyResult_Jsonfiles = "";
			gxTv_SdtRubyResult_Fileencryption = "";
			gxTv_SdtRubyResult_Hsm = "";
			gxTv_SdtRubyResult_Hextransaction = "";


			gxTv_SdtRubyResult_Devices_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtRubyResult_Success;
		 

		protected string gxTv_SdtRubyResult_Error;
		 

		protected string gxTv_SdtRubyResult_State;
		 

		protected string gxTv_SdtRubyResult_Version;
		 

		protected string gxTv_SdtRubyResult_Networks;
		 

		protected string gxTv_SdtRubyResult_Oraclekey;
		 

		protected string gxTv_SdtRubyResult_Encryptedoracle;
		 

		protected string gxTv_SdtRubyResult_Publickey;
		 

		protected bool gxTv_SdtRubyResult_Unlocked;
		 

		protected string gxTv_SdtRubyResult_Oracleresult;
		 

		protected string gxTv_SdtRubyResult_Fingerprint;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTv_SdtRubyResult_Account = null;
		protected bool gxTv_SdtRubyResult_Account_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTv_SdtRubyResult_Bip48 = null;
		protected bool gxTv_SdtRubyResult_Bip48_N;
		 

		protected string gxTv_SdtRubyResult_Login;
		 

		protected string gxTv_SdtRubyResult_Chat;
		 

		protected string gxTv_SdtRubyResult_Groups;
		 

		protected string gxTv_SdtRubyResult_Jsonfiles;
		 

		protected string gxTv_SdtRubyResult_Fileencryption;
		 

		protected string gxTv_SdtRubyResult_Hsm;
		 

		protected string gxTv_SdtRubyResult_Hextransaction;
		 

		protected long gxTv_SdtRubyResult_Virtualsize;
		 
		protected bool gxTv_SdtRubyResult_Devices_N;
		protected GXBaseCollection<SdtRubyResult_devicesItem> gxTv_SdtRubyResult_Devices = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RubyResult", Namespace="distributedcryptography")]
	public class SdtRubyResult_RESTInterface : GxGenericCollectionItem<SdtRubyResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubyResult_RESTInterface( ) : base()
		{	
		}

		public SdtRubyResult_RESTInterface( SdtRubyResult psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("state")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="state", Order=2)]
		public  string gxTpr_State
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_State);

			}
			set { 
				 sdt.gxTpr_State = value;
			}
		}

		[JsonPropertyName("version")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="version", Order=3)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Version);

			}
			set { 
				 sdt.gxTpr_Version = value;
			}
		}

		[JsonPropertyName("networks")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="networks", Order=4)]
		public  string gxTpr_Networks
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networks);

			}
			set { 
				 sdt.gxTpr_Networks = value;
			}
		}

		[JsonPropertyName("oracleKey")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="oracleKey", Order=5)]
		public  string gxTpr_Oraclekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Oraclekey);

			}
			set { 
				 sdt.gxTpr_Oraclekey = value;
			}
		}

		[JsonPropertyName("encryptedOracle")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="encryptedOracle", Order=6)]
		public  string gxTpr_Encryptedoracle
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedoracle);

			}
			set { 
				 sdt.gxTpr_Encryptedoracle = value;
			}
		}

		[JsonPropertyName("publicKey")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="publicKey", Order=7)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}

		[JsonPropertyName("unlocked")]
		[JsonPropertyOrder(8)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="unlocked", Order=8)]
		public bool gxTpr_Unlocked
		{
			get { 
				return sdt.gxTpr_Unlocked;

			}
			set { 
				sdt.gxTpr_Unlocked = value;
			}
		}

		[JsonPropertyName("oracleResult")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="oracleResult", Order=9)]
		public  string gxTpr_Oracleresult
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Oracleresult);

			}
			set { 
				 sdt.gxTpr_Oracleresult = value;
			}
		}

		[JsonPropertyName("fingerprint")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="fingerprint", Order=10)]
		public  string gxTpr_Fingerprint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fingerprint);

			}
			set { 
				 sdt.gxTpr_Fingerprint = value;
			}
		}

		[JsonPropertyName("account")]
		[JsonPropertyOrder(11)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="account", Order=11, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface gxTpr_Account
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Account_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface(sdt.gxTpr_Account);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Account = value.sdt;
			}
		}

		[JsonPropertyName("bip48")]
		[JsonPropertyOrder(12)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="bip48", Order=12, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface gxTpr_Bip48
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Bip48_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface(sdt.gxTpr_Bip48);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Bip48 = value.sdt;
			}
		}

		[JsonPropertyName("login")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="login", Order=13)]
		public  string gxTpr_Login
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Login);

			}
			set { 
				 sdt.gxTpr_Login = value;
			}
		}

		[JsonPropertyName("chat")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="chat", Order=14)]
		public  string gxTpr_Chat
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chat);

			}
			set { 
				 sdt.gxTpr_Chat = value;
			}
		}

		[JsonPropertyName("groups")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="groups", Order=15)]
		public  string gxTpr_Groups
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Groups);

			}
			set { 
				 sdt.gxTpr_Groups = value;
			}
		}

		[JsonPropertyName("jsonFiles")]
		[JsonPropertyOrder(16)]
		[DataMember(Name="jsonFiles", Order=16)]
		public  string gxTpr_Jsonfiles
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Jsonfiles);

			}
			set { 
				 sdt.gxTpr_Jsonfiles = value;
			}
		}

		[JsonPropertyName("fileEncryption")]
		[JsonPropertyOrder(17)]
		[DataMember(Name="fileEncryption", Order=17)]
		public  string gxTpr_Fileencryption
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fileencryption);

			}
			set { 
				 sdt.gxTpr_Fileencryption = value;
			}
		}

		[JsonPropertyName("hsm")]
		[JsonPropertyOrder(18)]
		[DataMember(Name="hsm", Order=18)]
		public  string gxTpr_Hsm
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Hsm);

			}
			set { 
				 sdt.gxTpr_Hsm = value;
			}
		}

		[JsonPropertyName("hexTransaction")]
		[JsonPropertyOrder(19)]
		[DataMember(Name="hexTransaction", Order=19)]
		public  string gxTpr_Hextransaction
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Hextransaction);

			}
			set { 
				 sdt.gxTpr_Hextransaction = value;
			}
		}

		[JsonPropertyName("virtualSize")]
		[JsonPropertyOrder(20)]
		[DataMember(Name="virtualSize", Order=20)]
		public  string gxTpr_Virtualsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Virtualsize, 10, 0));

			}
			set { 
				sdt.gxTpr_Virtualsize = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("devices")]
		[JsonPropertyOrder(21)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="devices", Order=21, EmitDefaultValue=false)]
		public GxGenericCollection<SdtRubyResult_devicesItem_RESTInterface> gxTpr_Devices
		{
			get {
				if (sdt.ShouldSerializegxTpr_Devices_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtRubyResult_devicesItem_RESTInterface>(sdt.gxTpr_Devices);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Devices);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubyResult sdt
		{
			get { 
				return (SdtRubyResult)Sdt;
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
				sdt = new SdtRubyResult() ;
			}
		}
	}
	#endregion
}