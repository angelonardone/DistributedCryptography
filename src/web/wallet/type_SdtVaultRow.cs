/*
				   File: type_SdtVaultRow
			Description: VaultRow
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
	[XmlRoot(ElementName="VaultRow")]
	[XmlType(TypeName="VaultRow" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtVaultRow : GxUserType
	{
		public SdtVaultRow( )
		{
			/* Constructor for serialization */
			gxTv_SdtVaultRow_Description = "";

			gxTv_SdtVaultRow_Url = "";

			gxTv_SdtVaultRow_Login = "";


		}

		public SdtVaultRow(IGxContext context)
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
			AddObjectProperty("PasswordId", gxTpr_Passwordid, false);


			AddObjectProperty("Description", gxTpr_Description, false);


			AddObjectProperty("url", gxTpr_Url, false);


			AddObjectProperty("login", gxTpr_Login, false);


			AddObjectProperty("hasAuthenticator", gxTpr_Hasauthenticator, false);


			AddObjectProperty("hasNote", gxTpr_Hasnote, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PasswordId")]
		[XmlElement(ElementName="PasswordId")]
		public Guid gxTpr_Passwordid
		{
			get {
				return gxTv_SdtVaultRow_Passwordid; 
			}
			set {
				gxTv_SdtVaultRow_Passwordid = value;
				SetDirty("Passwordid");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtVaultRow_Description; 
			}
			set {
				gxTv_SdtVaultRow_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="url")]
		[XmlElement(ElementName="url")]
		public string gxTpr_Url
		{
			get {
				return gxTv_SdtVaultRow_Url; 
			}
			set {
				gxTv_SdtVaultRow_Url = value;
				SetDirty("Url");
			}
		}




		[SoapElement(ElementName="login")]
		[XmlElement(ElementName="login")]
		public string gxTpr_Login
		{
			get {
				return gxTv_SdtVaultRow_Login; 
			}
			set {
				gxTv_SdtVaultRow_Login = value;
				SetDirty("Login");
			}
		}




		[SoapElement(ElementName="hasAuthenticator")]
		[XmlElement(ElementName="hasAuthenticator")]
		public bool gxTpr_Hasauthenticator
		{
			get {
				return gxTv_SdtVaultRow_Hasauthenticator; 
			}
			set {
				gxTv_SdtVaultRow_Hasauthenticator = value;
				SetDirty("Hasauthenticator");
			}
		}




		[SoapElement(ElementName="hasNote")]
		[XmlElement(ElementName="hasNote")]
		public bool gxTpr_Hasnote
		{
			get {
				return gxTv_SdtVaultRow_Hasnote; 
			}
			set {
				gxTv_SdtVaultRow_Hasnote = value;
				SetDirty("Hasnote");
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
			gxTv_SdtVaultRow_Description = "";
			gxTv_SdtVaultRow_Url = "";
			gxTv_SdtVaultRow_Login = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtVaultRow_Passwordid;
		 

		protected string gxTv_SdtVaultRow_Description;
		 

		protected string gxTv_SdtVaultRow_Url;
		 

		protected string gxTv_SdtVaultRow_Login;
		 

		protected bool gxTv_SdtVaultRow_Hasauthenticator;
		 

		protected bool gxTv_SdtVaultRow_Hasnote;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"VaultRow", Namespace="distributedcryptography")]
	public class SdtVaultRow_RESTInterface : GxGenericCollectionItem<SdtVaultRow>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtVaultRow_RESTInterface( ) : base()
		{	
		}

		public SdtVaultRow_RESTInterface( SdtVaultRow psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("PasswordId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="PasswordId", Order=0)]
		public Guid gxTpr_Passwordid
		{
			get { 
				return sdt.gxTpr_Passwordid;

			}
			set { 
				sdt.gxTpr_Passwordid = value;
			}
		}

		[JsonPropertyName("Description")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Description", Order=1)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("url")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="url", Order=2)]
		public  string gxTpr_Url
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Url);

			}
			set { 
				 sdt.gxTpr_Url = value;
			}
		}

		[JsonPropertyName("login")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="login", Order=3)]
		public  string gxTpr_Login
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Login);

			}
			set { 
				 sdt.gxTpr_Login = value;
			}
		}

		[JsonPropertyName("hasAuthenticator")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="hasAuthenticator", Order=4)]
		public bool gxTpr_Hasauthenticator
		{
			get { 
				return sdt.gxTpr_Hasauthenticator;

			}
			set { 
				sdt.gxTpr_Hasauthenticator = value;
			}
		}

		[JsonPropertyName("hasNote")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="hasNote", Order=5)]
		public bool gxTpr_Hasnote
		{
			get { 
				return sdt.gxTpr_Hasnote;

			}
			set { 
				sdt.gxTpr_Hasnote = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtVaultRow sdt
		{
			get { 
				return (SdtVaultRow)Sdt;
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
				sdt = new SdtVaultRow() ;
			}
		}
	}
	#endregion
}