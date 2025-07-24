/*
				   File: type_SdtExtKeyCreate
			Description: ExtKeyCreate
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
	[XmlRoot(ElementName="ExtKeyCreate")]
	[XmlType(TypeName="ExtKeyCreate" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExtKeyCreate : GxUserType
	{
		public SdtExtKeyCreate( )
		{
			/* Constructor for serialization */
			gxTv_SdtExtKeyCreate_Createtext = "";

			gxTv_SdtExtKeyCreate_Privatekey = "";

			gxTv_SdtExtKeyCreate_Chaincode = "";

			gxTv_SdtExtKeyCreate_Seed = "";

			gxTv_SdtExtKeyCreate_Keypath = "";

			gxTv_SdtExtKeyCreate_Encryptedwif = "";

			gxTv_SdtExtKeyCreate_Extendedprivatekey = "";

			gxTv_SdtExtKeyCreate_Extendedpublickey = "";

			gxTv_SdtExtKeyCreate_Networktype = "";

		}

		public SdtExtKeyCreate(IGxContext context)
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
			AddObjectProperty("CreateExtKeyType", gxTpr_Createextkeytype, false);


			AddObjectProperty("CreateText", gxTpr_Createtext, false);


			AddObjectProperty("PrivateKey", gxTpr_Privatekey, false);


			AddObjectProperty("ChainCode", gxTpr_Chaincode, false);


			AddObjectProperty("Seed", gxTpr_Seed, false);


			AddObjectProperty("KeyPath", gxTpr_Keypath, false);


			AddObjectProperty("EncryptedWIF", gxTpr_Encryptedwif, false);


			AddObjectProperty("MnemonicLanguage", gxTpr_Mnemoniclanguage, false);


			AddObjectProperty("MnemonicNumberWords", gxTpr_Mnemonicnumberwords, false);


			AddObjectProperty("ExtendedPrivateKey", gxTpr_Extendedprivatekey, false);


			AddObjectProperty("ExtendedPublicKey", gxTpr_Extendedpublickey, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="CreateExtKeyType")]
		[XmlElement(ElementName="CreateExtKeyType")]
		public short gxTpr_Createextkeytype
		{
			get {
				return gxTv_SdtExtKeyCreate_Createextkeytype; 
			}
			set {
				gxTv_SdtExtKeyCreate_Createextkeytype = value;
				SetDirty("Createextkeytype");
			}
		}




		[SoapElement(ElementName="CreateText")]
		[XmlElement(ElementName="CreateText")]
		public string gxTpr_Createtext
		{
			get {
				return gxTv_SdtExtKeyCreate_Createtext; 
			}
			set {
				gxTv_SdtExtKeyCreate_Createtext = value;
				SetDirty("Createtext");
			}
		}




		[SoapElement(ElementName="PrivateKey")]
		[XmlElement(ElementName="PrivateKey")]
		public string gxTpr_Privatekey
		{
			get {
				return gxTv_SdtExtKeyCreate_Privatekey; 
			}
			set {
				gxTv_SdtExtKeyCreate_Privatekey = value;
				SetDirty("Privatekey");
			}
		}




		[SoapElement(ElementName="ChainCode")]
		[XmlElement(ElementName="ChainCode")]
		public string gxTpr_Chaincode
		{
			get {
				return gxTv_SdtExtKeyCreate_Chaincode; 
			}
			set {
				gxTv_SdtExtKeyCreate_Chaincode = value;
				SetDirty("Chaincode");
			}
		}




		[SoapElement(ElementName="Seed")]
		[XmlElement(ElementName="Seed")]
		public string gxTpr_Seed
		{
			get {
				return gxTv_SdtExtKeyCreate_Seed; 
			}
			set {
				gxTv_SdtExtKeyCreate_Seed = value;
				SetDirty("Seed");
			}
		}




		[SoapElement(ElementName="KeyPath")]
		[XmlElement(ElementName="KeyPath")]
		public string gxTpr_Keypath
		{
			get {
				return gxTv_SdtExtKeyCreate_Keypath; 
			}
			set {
				gxTv_SdtExtKeyCreate_Keypath = value;
				SetDirty("Keypath");
			}
		}




		[SoapElement(ElementName="EncryptedWIF")]
		[XmlElement(ElementName="EncryptedWIF")]
		public string gxTpr_Encryptedwif
		{
			get {
				return gxTv_SdtExtKeyCreate_Encryptedwif; 
			}
			set {
				gxTv_SdtExtKeyCreate_Encryptedwif = value;
				SetDirty("Encryptedwif");
			}
		}




		[SoapElement(ElementName="MnemonicLanguage")]
		[XmlElement(ElementName="MnemonicLanguage")]
		public short gxTpr_Mnemoniclanguage
		{
			get {
				return gxTv_SdtExtKeyCreate_Mnemoniclanguage; 
			}
			set {
				gxTv_SdtExtKeyCreate_Mnemoniclanguage = value;
				SetDirty("Mnemoniclanguage");
			}
		}




		[SoapElement(ElementName="MnemonicNumberWords")]
		[XmlElement(ElementName="MnemonicNumberWords")]
		public short gxTpr_Mnemonicnumberwords
		{
			get {
				return gxTv_SdtExtKeyCreate_Mnemonicnumberwords; 
			}
			set {
				gxTv_SdtExtKeyCreate_Mnemonicnumberwords = value;
				SetDirty("Mnemonicnumberwords");
			}
		}




		[SoapElement(ElementName="ExtendedPrivateKey")]
		[XmlElement(ElementName="ExtendedPrivateKey")]
		public string gxTpr_Extendedprivatekey
		{
			get {
				return gxTv_SdtExtKeyCreate_Extendedprivatekey; 
			}
			set {
				gxTv_SdtExtKeyCreate_Extendedprivatekey = value;
				SetDirty("Extendedprivatekey");
			}
		}




		[SoapElement(ElementName="ExtendedPublicKey")]
		[XmlElement(ElementName="ExtendedPublicKey")]
		public string gxTpr_Extendedpublickey
		{
			get {
				return gxTv_SdtExtKeyCreate_Extendedpublickey; 
			}
			set {
				gxTv_SdtExtKeyCreate_Extendedpublickey = value;
				SetDirty("Extendedpublickey");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtExtKeyCreate_Networktype; 
			}
			set {
				gxTv_SdtExtKeyCreate_Networktype = value;
				SetDirty("Networktype");
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
			gxTv_SdtExtKeyCreate_Createtext = "";
			gxTv_SdtExtKeyCreate_Privatekey = "";
			gxTv_SdtExtKeyCreate_Chaincode = "";
			gxTv_SdtExtKeyCreate_Seed = "";
			gxTv_SdtExtKeyCreate_Keypath = "";
			gxTv_SdtExtKeyCreate_Encryptedwif = "";


			gxTv_SdtExtKeyCreate_Extendedprivatekey = "";
			gxTv_SdtExtKeyCreate_Extendedpublickey = "";
			gxTv_SdtExtKeyCreate_Networktype = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtExtKeyCreate_Createextkeytype;
		 

		protected string gxTv_SdtExtKeyCreate_Createtext;
		 

		protected string gxTv_SdtExtKeyCreate_Privatekey;
		 

		protected string gxTv_SdtExtKeyCreate_Chaincode;
		 

		protected string gxTv_SdtExtKeyCreate_Seed;
		 

		protected string gxTv_SdtExtKeyCreate_Keypath;
		 

		protected string gxTv_SdtExtKeyCreate_Encryptedwif;
		 

		protected short gxTv_SdtExtKeyCreate_Mnemoniclanguage;
		 

		protected short gxTv_SdtExtKeyCreate_Mnemonicnumberwords;
		 

		protected string gxTv_SdtExtKeyCreate_Extendedprivatekey;
		 

		protected string gxTv_SdtExtKeyCreate_Extendedpublickey;
		 

		protected string gxTv_SdtExtKeyCreate_Networktype;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExtKeyCreate", Namespace="distributedcryptography")]
	public class SdtExtKeyCreate_RESTInterface : GxGenericCollectionItem<SdtExtKeyCreate>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExtKeyCreate_RESTInterface( ) : base()
		{	
		}

		public SdtExtKeyCreate_RESTInterface( SdtExtKeyCreate psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("CreateExtKeyType")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="CreateExtKeyType", Order=0)]
		public short gxTpr_Createextkeytype
		{
			get { 
				return sdt.gxTpr_Createextkeytype;

			}
			set { 
				sdt.gxTpr_Createextkeytype = value;
			}
		}

		[JsonPropertyName("CreateText")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="CreateText", Order=1)]
		public  string gxTpr_Createtext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Createtext);

			}
			set { 
				 sdt.gxTpr_Createtext = value;
			}
		}

		[JsonPropertyName("PrivateKey")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="PrivateKey", Order=2)]
		public  string gxTpr_Privatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Privatekey);

			}
			set { 
				 sdt.gxTpr_Privatekey = value;
			}
		}

		[JsonPropertyName("ChainCode")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="ChainCode", Order=3)]
		public  string gxTpr_Chaincode
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chaincode);

			}
			set { 
				 sdt.gxTpr_Chaincode = value;
			}
		}

		[JsonPropertyName("Seed")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Seed", Order=4)]
		public  string gxTpr_Seed
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Seed);

			}
			set { 
				 sdt.gxTpr_Seed = value;
			}
		}

		[JsonPropertyName("KeyPath")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="KeyPath", Order=5)]
		public  string gxTpr_Keypath
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Keypath);

			}
			set { 
				 sdt.gxTpr_Keypath = value;
			}
		}

		[JsonPropertyName("EncryptedWIF")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="EncryptedWIF", Order=6)]
		public  string gxTpr_Encryptedwif
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedwif);

			}
			set { 
				 sdt.gxTpr_Encryptedwif = value;
			}
		}

		[JsonPropertyName("MnemonicLanguage")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="MnemonicLanguage", Order=7)]
		public short gxTpr_Mnemoniclanguage
		{
			get { 
				return sdt.gxTpr_Mnemoniclanguage;

			}
			set { 
				sdt.gxTpr_Mnemoniclanguage = value;
			}
		}

		[JsonPropertyName("MnemonicNumberWords")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="MnemonicNumberWords", Order=8)]
		public short gxTpr_Mnemonicnumberwords
		{
			get { 
				return sdt.gxTpr_Mnemonicnumberwords;

			}
			set { 
				sdt.gxTpr_Mnemonicnumberwords = value;
			}
		}

		[JsonPropertyName("ExtendedPrivateKey")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="ExtendedPrivateKey", Order=9)]
		public  string gxTpr_Extendedprivatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extendedprivatekey);

			}
			set { 
				 sdt.gxTpr_Extendedprivatekey = value;
			}
		}

		[JsonPropertyName("ExtendedPublicKey")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="ExtendedPublicKey", Order=10)]
		public  string gxTpr_Extendedpublickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extendedpublickey);

			}
			set { 
				 sdt.gxTpr_Extendedpublickey = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="NetworkType", Order=11)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExtKeyCreate sdt
		{
			get { 
				return (SdtExtKeyCreate)Sdt;
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
				sdt = new SdtExtKeyCreate() ;
			}
		}
	}
	#endregion
}