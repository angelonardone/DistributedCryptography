/*
				   File: type_SdtWallet
			Description: Wallet
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
	[XmlRoot(ElementName="Wallet")]
	[XmlType(TypeName="Wallet" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWallet : GxUserType
	{
		public SdtWallet( )
		{
			/* Constructor for serialization */
			gxTv_SdtWallet_Walletname = "";

			gxTv_SdtWallet_Wallettype = "";

			gxTv_SdtWallet_Encryptedsecret = "";

			gxTv_SdtWallet_Networktype = "";

			gxTv_SdtWallet_Walletfilename = "";

			gxTv_SdtWallet_Walletbasedirectory = "";

			gxTv_SdtWallet_Keystorage = "";

			gxTv_SdtWallet_Rubydevice = "";

			gxTv_SdtWallet_Rubyfingerprint = "";

			gxTv_SdtWallet_Encryptedoracle = "";

		}

		public SdtWallet(IGxContext context)
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
			AddObjectProperty("WalletName", gxTpr_Walletname, false);


			AddObjectProperty("WalletType", gxTpr_Wallettype, false);


			AddObjectProperty("EncryptedSecret", gxTpr_Encryptedsecret, false);


			AddObjectProperty("useAuthenticator", gxTpr_Useauthenticator, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


			AddObjectProperty("WalletFileName", gxTpr_Walletfilename, false);


			AddObjectProperty("WalletBaseDirectory", gxTpr_Walletbasedirectory, false);


			AddObjectProperty("WalletReadBalanceOnStart", gxTpr_Walletreadbalanceonstart, false);


			AddObjectProperty("KeyStorage", gxTpr_Keystorage, false);


			AddObjectProperty("RubyDevice", gxTpr_Rubydevice, false);


			AddObjectProperty("RubyFingerprint", gxTpr_Rubyfingerprint, false);


			AddObjectProperty("EncryptedOracle", gxTpr_Encryptedoracle, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="WalletName")]
		[XmlElement(ElementName="WalletName")]
		public string gxTpr_Walletname
		{
			get {
				return gxTv_SdtWallet_Walletname; 
			}
			set {
				gxTv_SdtWallet_Walletname = value;
				SetDirty("Walletname");
			}
		}




		[SoapElement(ElementName="WalletType")]
		[XmlElement(ElementName="WalletType")]
		public string gxTpr_Wallettype
		{
			get {
				return gxTv_SdtWallet_Wallettype; 
			}
			set {
				gxTv_SdtWallet_Wallettype = value;
				SetDirty("Wallettype");
			}
		}




		[SoapElement(ElementName="EncryptedSecret")]
		[XmlElement(ElementName="EncryptedSecret")]
		public string gxTpr_Encryptedsecret
		{
			get {
				return gxTv_SdtWallet_Encryptedsecret; 
			}
			set {
				gxTv_SdtWallet_Encryptedsecret = value;
				SetDirty("Encryptedsecret");
			}
		}




		[SoapElement(ElementName="useAuthenticator")]
		[XmlElement(ElementName="useAuthenticator")]
		public bool gxTpr_Useauthenticator
		{
			get {
				return gxTv_SdtWallet_Useauthenticator; 
			}
			set {
				gxTv_SdtWallet_Useauthenticator = value;
				SetDirty("Useauthenticator");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtWallet_Networktype; 
			}
			set {
				gxTv_SdtWallet_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="WalletFileName")]
		[XmlElement(ElementName="WalletFileName")]
		public string gxTpr_Walletfilename
		{
			get {
				return gxTv_SdtWallet_Walletfilename; 
			}
			set {
				gxTv_SdtWallet_Walletfilename = value;
				SetDirty("Walletfilename");
			}
		}




		[SoapElement(ElementName="WalletBaseDirectory")]
		[XmlElement(ElementName="WalletBaseDirectory")]
		public string gxTpr_Walletbasedirectory
		{
			get {
				return gxTv_SdtWallet_Walletbasedirectory; 
			}
			set {
				gxTv_SdtWallet_Walletbasedirectory = value;
				SetDirty("Walletbasedirectory");
			}
		}




		[SoapElement(ElementName="WalletReadBalanceOnStart")]
		[XmlElement(ElementName="WalletReadBalanceOnStart")]
		public bool gxTpr_Walletreadbalanceonstart
		{
			get {
				return gxTv_SdtWallet_Walletreadbalanceonstart; 
			}
			set {
				gxTv_SdtWallet_Walletreadbalanceonstart = value;
				SetDirty("Walletreadbalanceonstart");
			}
		}




		[SoapElement(ElementName="KeyStorage")]
		[XmlElement(ElementName="KeyStorage")]
		public string gxTpr_Keystorage
		{
			get {
				return gxTv_SdtWallet_Keystorage; 
			}
			set {
				gxTv_SdtWallet_Keystorage = value;
				SetDirty("Keystorage");
			}
		}




		[SoapElement(ElementName="RubyDevice")]
		[XmlElement(ElementName="RubyDevice")]
		public string gxTpr_Rubydevice
		{
			get {
				return gxTv_SdtWallet_Rubydevice; 
			}
			set {
				gxTv_SdtWallet_Rubydevice = value;
				SetDirty("Rubydevice");
			}
		}




		[SoapElement(ElementName="RubyFingerprint")]
		[XmlElement(ElementName="RubyFingerprint")]
		public string gxTpr_Rubyfingerprint
		{
			get {
				return gxTv_SdtWallet_Rubyfingerprint; 
			}
			set {
				gxTv_SdtWallet_Rubyfingerprint = value;
				SetDirty("Rubyfingerprint");
			}
		}




		[SoapElement(ElementName="EncryptedOracle")]
		[XmlElement(ElementName="EncryptedOracle")]
		public string gxTpr_Encryptedoracle
		{
			get {
				return gxTv_SdtWallet_Encryptedoracle; 
			}
			set {
				gxTv_SdtWallet_Encryptedoracle = value;
				SetDirty("Encryptedoracle");
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
			gxTv_SdtWallet_Walletname = "";
			gxTv_SdtWallet_Wallettype = "";
			gxTv_SdtWallet_Encryptedsecret = "";

			gxTv_SdtWallet_Networktype = "";
			gxTv_SdtWallet_Walletfilename = "";
			gxTv_SdtWallet_Walletbasedirectory = "";

			gxTv_SdtWallet_Keystorage = "";
			gxTv_SdtWallet_Rubydevice = "";
			gxTv_SdtWallet_Rubyfingerprint = "";
			gxTv_SdtWallet_Encryptedoracle = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWallet_Walletname;
		 

		protected string gxTv_SdtWallet_Wallettype;
		 

		protected string gxTv_SdtWallet_Encryptedsecret;
		 

		protected bool gxTv_SdtWallet_Useauthenticator;
		 

		protected string gxTv_SdtWallet_Networktype;
		 

		protected string gxTv_SdtWallet_Walletfilename;
		 

		protected string gxTv_SdtWallet_Walletbasedirectory;
		 

		protected bool gxTv_SdtWallet_Walletreadbalanceonstart;
		 

		protected string gxTv_SdtWallet_Keystorage;
		 

		protected string gxTv_SdtWallet_Rubydevice;
		 

		protected string gxTv_SdtWallet_Rubyfingerprint;
		 

		protected string gxTv_SdtWallet_Encryptedoracle;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Wallet", Namespace="distributedcryptography")]
	public class SdtWallet_RESTInterface : GxGenericCollectionItem<SdtWallet>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWallet_RESTInterface( ) : base()
		{	
		}

		public SdtWallet_RESTInterface( SdtWallet psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("WalletName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="WalletName", Order=0)]
		public  string gxTpr_Walletname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Walletname);

			}
			set { 
				 sdt.gxTpr_Walletname = value;
			}
		}

		[JsonPropertyName("WalletType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="WalletType", Order=1)]
		public  string gxTpr_Wallettype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Wallettype);

			}
			set { 
				 sdt.gxTpr_Wallettype = value;
			}
		}

		[JsonPropertyName("EncryptedSecret")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="EncryptedSecret", Order=2)]
		public  string gxTpr_Encryptedsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedsecret);

			}
			set { 
				 sdt.gxTpr_Encryptedsecret = value;
			}
		}

		[JsonPropertyName("useAuthenticator")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="useAuthenticator", Order=3)]
		public bool gxTpr_Useauthenticator
		{
			get { 
				return sdt.gxTpr_Useauthenticator;

			}
			set { 
				sdt.gxTpr_Useauthenticator = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="NetworkType", Order=4)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("WalletFileName")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="WalletFileName", Order=5)]
		public  string gxTpr_Walletfilename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Walletfilename);

			}
			set { 
				 sdt.gxTpr_Walletfilename = value;
			}
		}

		[JsonPropertyName("WalletBaseDirectory")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="WalletBaseDirectory", Order=6)]
		public  string gxTpr_Walletbasedirectory
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Walletbasedirectory);

			}
			set { 
				 sdt.gxTpr_Walletbasedirectory = value;
			}
		}

		[JsonPropertyName("WalletReadBalanceOnStart")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="WalletReadBalanceOnStart", Order=7)]
		public bool gxTpr_Walletreadbalanceonstart
		{
			get { 
				return sdt.gxTpr_Walletreadbalanceonstart;

			}
			set { 
				sdt.gxTpr_Walletreadbalanceonstart = value;
			}
		}

		[JsonPropertyName("KeyStorage")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="KeyStorage", Order=8)]
		public  string gxTpr_Keystorage
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Keystorage);

			}
			set { 
				 sdt.gxTpr_Keystorage = value;
			}
		}

		[JsonPropertyName("RubyDevice")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="RubyDevice", Order=9)]
		public  string gxTpr_Rubydevice
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Rubydevice);

			}
			set { 
				 sdt.gxTpr_Rubydevice = value;
			}
		}

		[JsonPropertyName("RubyFingerprint")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="RubyFingerprint", Order=10)]
		public  string gxTpr_Rubyfingerprint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Rubyfingerprint);

			}
			set { 
				 sdt.gxTpr_Rubyfingerprint = value;
			}
		}

		[JsonPropertyName("EncryptedOracle")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="EncryptedOracle", Order=11)]
		public  string gxTpr_Encryptedoracle
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedoracle);

			}
			set { 
				 sdt.gxTpr_Encryptedoracle = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtWallet sdt
		{
			get { 
				return (SdtWallet)Sdt;
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
				sdt = new SdtWallet() ;
			}
		}
	}
	#endregion
}