/*
				   File: type_SdtExtKeyInfo
			Description: ExtKeyInfo
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
	[XmlRoot(ElementName="ExtKeyInfo")]
	[XmlType(TypeName="ExtKeyInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExtKeyInfo : GxUserType
	{
		public SdtExtKeyInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtExtKeyInfo_Privatekey = "";

			gxTv_SdtExtKeyInfo_Publickey = "";

			gxTv_SdtExtKeyInfo_Chaincode = "";

			gxTv_SdtExtKeyInfo_Mnemonic = "";

			gxTv_SdtExtKeyInfo_Wif = "";

			gxTv_SdtExtKeyInfo_Encryptedwif = "";

		}

		public SdtExtKeyInfo(IGxContext context)
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
			AddObjectProperty("PrivateKey", gxTpr_Privatekey, false);


			AddObjectProperty("PublicKey", gxTpr_Publickey, false);


			AddObjectProperty("ChainCode", gxTpr_Chaincode, false);


			AddObjectProperty("Mnemonic", gxTpr_Mnemonic, false);


			AddObjectProperty("WIF", gxTpr_Wif, false);


			AddObjectProperty("encryptedWIF", gxTpr_Encryptedwif, false);

			if (gxTv_SdtExtKeyInfo_Extended != null)
			{
				AddObjectProperty("Extended", gxTv_SdtExtKeyInfo_Extended, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PrivateKey")]
		[XmlElement(ElementName="PrivateKey")]
		public string gxTpr_Privatekey
		{
			get {
				return gxTv_SdtExtKeyInfo_Privatekey; 
			}
			set {
				gxTv_SdtExtKeyInfo_Privatekey = value;
				SetDirty("Privatekey");
			}
		}




		[SoapElement(ElementName="PublicKey")]
		[XmlElement(ElementName="PublicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtExtKeyInfo_Publickey; 
			}
			set {
				gxTv_SdtExtKeyInfo_Publickey = value;
				SetDirty("Publickey");
			}
		}




		[SoapElement(ElementName="ChainCode")]
		[XmlElement(ElementName="ChainCode")]
		public string gxTpr_Chaincode
		{
			get {
				return gxTv_SdtExtKeyInfo_Chaincode; 
			}
			set {
				gxTv_SdtExtKeyInfo_Chaincode = value;
				SetDirty("Chaincode");
			}
		}




		[SoapElement(ElementName="Mnemonic")]
		[XmlElement(ElementName="Mnemonic")]
		public string gxTpr_Mnemonic
		{
			get {
				return gxTv_SdtExtKeyInfo_Mnemonic; 
			}
			set {
				gxTv_SdtExtKeyInfo_Mnemonic = value;
				SetDirty("Mnemonic");
			}
		}




		[SoapElement(ElementName="WIF")]
		[XmlElement(ElementName="WIF")]
		public string gxTpr_Wif
		{
			get {
				return gxTv_SdtExtKeyInfo_Wif; 
			}
			set {
				gxTv_SdtExtKeyInfo_Wif = value;
				SetDirty("Wif");
			}
		}




		[SoapElement(ElementName="encryptedWIF")]
		[XmlElement(ElementName="encryptedWIF")]
		public string gxTpr_Encryptedwif
		{
			get {
				return gxTv_SdtExtKeyInfo_Encryptedwif; 
			}
			set {
				gxTv_SdtExtKeyInfo_Encryptedwif = value;
				SetDirty("Encryptedwif");
			}
		}



		[SoapElement(ElementName="Extended" )]
		[XmlElement(ElementName="Extended" )]
		public SdtExtKeyInfo_Extended gxTpr_Extended
		{
			get {
				if ( gxTv_SdtExtKeyInfo_Extended == null )
				{
					gxTv_SdtExtKeyInfo_Extended = new SdtExtKeyInfo_Extended(context);
				}
				gxTv_SdtExtKeyInfo_Extended_N = false;
				SetDirty("Extended");
				return gxTv_SdtExtKeyInfo_Extended;
			}
			set {
				gxTv_SdtExtKeyInfo_Extended_N = false;
				gxTv_SdtExtKeyInfo_Extended = value;
				SetDirty("Extended");
			}

		}

		public void gxTv_SdtExtKeyInfo_Extended_SetNull()
		{
			gxTv_SdtExtKeyInfo_Extended_N = true;
			gxTv_SdtExtKeyInfo_Extended = null;
		}

		public bool gxTv_SdtExtKeyInfo_Extended_IsNull()
		{
			return gxTv_SdtExtKeyInfo_Extended == null;
		}
		public bool ShouldSerializegxTpr_Extended_Json()
		{
				return (gxTv_SdtExtKeyInfo_Extended != null && gxTv_SdtExtKeyInfo_Extended.ShouldSerializeSdtJson());

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
			gxTv_SdtExtKeyInfo_Privatekey = "";
			gxTv_SdtExtKeyInfo_Publickey = "";
			gxTv_SdtExtKeyInfo_Chaincode = "";
			gxTv_SdtExtKeyInfo_Mnemonic = "";
			gxTv_SdtExtKeyInfo_Wif = "";
			gxTv_SdtExtKeyInfo_Encryptedwif = "";

			gxTv_SdtExtKeyInfo_Extended_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExtKeyInfo_Privatekey;
		 

		protected string gxTv_SdtExtKeyInfo_Publickey;
		 

		protected string gxTv_SdtExtKeyInfo_Chaincode;
		 

		protected string gxTv_SdtExtKeyInfo_Mnemonic;
		 

		protected string gxTv_SdtExtKeyInfo_Wif;
		 

		protected string gxTv_SdtExtKeyInfo_Encryptedwif;
		 
		protected bool gxTv_SdtExtKeyInfo_Extended_N;
		protected SdtExtKeyInfo_Extended gxTv_SdtExtKeyInfo_Extended = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExtKeyInfo", Namespace="distributedcryptography")]
	public class SdtExtKeyInfo_RESTInterface : GxGenericCollectionItem<SdtExtKeyInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExtKeyInfo_RESTInterface( ) : base()
		{	
		}

		public SdtExtKeyInfo_RESTInterface( SdtExtKeyInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("PrivateKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="PrivateKey", Order=0)]
		public  string gxTpr_Privatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Privatekey);

			}
			set { 
				 sdt.gxTpr_Privatekey = value;
			}
		}

		[JsonPropertyName("PublicKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="PublicKey", Order=1)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}

		[JsonPropertyName("ChainCode")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="ChainCode", Order=2)]
		public  string gxTpr_Chaincode
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chaincode);

			}
			set { 
				 sdt.gxTpr_Chaincode = value;
			}
		}

		[JsonPropertyName("Mnemonic")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Mnemonic", Order=3)]
		public  string gxTpr_Mnemonic
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Mnemonic);

			}
			set { 
				 sdt.gxTpr_Mnemonic = value;
			}
		}

		[JsonPropertyName("WIF")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="WIF", Order=4)]
		public  string gxTpr_Wif
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Wif);

			}
			set { 
				 sdt.gxTpr_Wif = value;
			}
		}

		[JsonPropertyName("encryptedWIF")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="encryptedWIF", Order=5)]
		public  string gxTpr_Encryptedwif
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedwif);

			}
			set { 
				 sdt.gxTpr_Encryptedwif = value;
			}
		}

		[JsonPropertyName("Extended")]
		[JsonPropertyOrder(6)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Extended", Order=6, EmitDefaultValue=false)]
		public SdtExtKeyInfo_Extended_RESTInterface gxTpr_Extended
		{
			get {
				if (sdt.ShouldSerializegxTpr_Extended_Json())
					return new SdtExtKeyInfo_Extended_RESTInterface(sdt.gxTpr_Extended);
				else
					return null;

			}

			set {
				sdt.gxTpr_Extended = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtExtKeyInfo sdt
		{
			get { 
				return (SdtExtKeyInfo)Sdt;
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
				sdt = new SdtExtKeyInfo() ;
			}
		}
	}
	#endregion
}