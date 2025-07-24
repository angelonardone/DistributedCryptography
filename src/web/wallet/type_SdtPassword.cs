/*
				   File: type_SdtPassword
			Description: Password
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="Password")]
	[XmlType(TypeName="Password" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPassword : GxUserType
	{
		public SdtPassword( )
		{
			/* Constructor for serialization */
			gxTv_SdtPassword_Description = "";

			gxTv_SdtPassword_Url = "";

			gxTv_SdtPassword_Login = "";

			gxTv_SdtPassword_Password = "";

			gxTv_SdtPassword_Based32key = "";

			gxTv_SdtPassword_Note = "";

		}

		public SdtPassword(IGxContext context)
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


			AddObjectProperty("password", gxTpr_Password, false);


			AddObjectProperty("based32Key", gxTpr_Based32key, false);


			AddObjectProperty("note", gxTpr_Note, false);

			if (gxTv_SdtPassword_Password_tag != null)
			{
				AddObjectProperty("Password_tag", gxTv_SdtPassword_Password_tag, false);
			}
			if (gxTv_SdtPassword_Contact != null)
			{
				AddObjectProperty("Contact", gxTv_SdtPassword_Contact, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PasswordId")]
		[XmlElement(ElementName="PasswordId")]
		public Guid gxTpr_Passwordid
		{
			get {
				return gxTv_SdtPassword_Passwordid; 
			}
			set {
				gxTv_SdtPassword_Passwordid = value;
				SetDirty("Passwordid");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtPassword_Description; 
			}
			set {
				gxTv_SdtPassword_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="url")]
		[XmlElement(ElementName="url")]
		public string gxTpr_Url
		{
			get {
				return gxTv_SdtPassword_Url; 
			}
			set {
				gxTv_SdtPassword_Url = value;
				SetDirty("Url");
			}
		}




		[SoapElement(ElementName="login")]
		[XmlElement(ElementName="login")]
		public string gxTpr_Login
		{
			get {
				return gxTv_SdtPassword_Login; 
			}
			set {
				gxTv_SdtPassword_Login = value;
				SetDirty("Login");
			}
		}




		[SoapElement(ElementName="password")]
		[XmlElement(ElementName="password")]
		public string gxTpr_Password
		{
			get {
				return gxTv_SdtPassword_Password; 
			}
			set {
				gxTv_SdtPassword_Password = value;
				SetDirty("Password");
			}
		}




		[SoapElement(ElementName="based32Key")]
		[XmlElement(ElementName="based32Key")]
		public string gxTpr_Based32key
		{
			get {
				return gxTv_SdtPassword_Based32key; 
			}
			set {
				gxTv_SdtPassword_Based32key = value;
				SetDirty("Based32key");
			}
		}




		[SoapElement(ElementName="note")]
		[XmlElement(ElementName="note")]
		public string gxTpr_Note
		{
			get {
				return gxTv_SdtPassword_Note; 
			}
			set {
				gxTv_SdtPassword_Note = value;
				SetDirty("Note");
			}
		}




		[SoapElement(ElementName="Password_tag" )]
		[XmlArray(ElementName="Password_tag"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPassword_Password_tag == null )
				{
					gxTv_SdtPassword_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				return gxTv_SdtPassword_Password_tag;
			}
			set {
				gxTv_SdtPassword_Password_tag_N = false;
				gxTv_SdtPassword_Password_tag = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag
		{
			get {
				if ( gxTv_SdtPassword_Password_tag == null )
				{
					gxTv_SdtPassword_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				gxTv_SdtPassword_Password_tag_N = false;
				SetDirty("Password_tag");
				return gxTv_SdtPassword_Password_tag ;
			}
			set {
				gxTv_SdtPassword_Password_tag_N = false;
				gxTv_SdtPassword_Password_tag = value;
				SetDirty("Password_tag");
			}
		}

		public void gxTv_SdtPassword_Password_tag_SetNull()
		{
			gxTv_SdtPassword_Password_tag_N = true;
			gxTv_SdtPassword_Password_tag = null;
		}

		public bool gxTv_SdtPassword_Password_tag_IsNull()
		{
			return gxTv_SdtPassword_Password_tag == null;
		}
		public bool ShouldSerializegxTpr_Password_tag_GXBaseCollection_Json()
		{
			return gxTv_SdtPassword_Password_tag != null && gxTv_SdtPassword_Password_tag.Count > 0;

		}


		[SoapElement(ElementName="Contact" )]
		[XmlArray(ElementName="Contact"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<Guid> gxTpr_Contact_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtPassword_Contact == null )
				{
					gxTv_SdtPassword_Contact = new GxSimpleCollection<Guid>( );
				}
				return gxTv_SdtPassword_Contact;
			}
			set {
				gxTv_SdtPassword_Contact_N = false;
				gxTv_SdtPassword_Contact = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<Guid> gxTpr_Contact
		{
			get {
				if ( gxTv_SdtPassword_Contact == null )
				{
					gxTv_SdtPassword_Contact = new GxSimpleCollection<Guid>();
				}
				gxTv_SdtPassword_Contact_N = false;
				SetDirty("Contact");
				return gxTv_SdtPassword_Contact ;
			}
			set {
				gxTv_SdtPassword_Contact_N = false;
				gxTv_SdtPassword_Contact = value;
				SetDirty("Contact");
			}
		}

		public void gxTv_SdtPassword_Contact_SetNull()
		{
			gxTv_SdtPassword_Contact_N = true;
			gxTv_SdtPassword_Contact = null;
		}

		public bool gxTv_SdtPassword_Contact_IsNull()
		{
			return gxTv_SdtPassword_Contact == null;
		}
		public bool ShouldSerializegxTpr_Contact_GxSimpleCollection_Json()
		{
			return gxTv_SdtPassword_Contact != null && gxTv_SdtPassword_Contact.Count > 0;

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
			gxTv_SdtPassword_Description = "";
			gxTv_SdtPassword_Url = "";
			gxTv_SdtPassword_Login = "";
			gxTv_SdtPassword_Password = "";
			gxTv_SdtPassword_Based32key = "";
			gxTv_SdtPassword_Note = "";

			gxTv_SdtPassword_Password_tag_N = true;


			gxTv_SdtPassword_Contact_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtPassword_Passwordid;
		 

		protected string gxTv_SdtPassword_Description;
		 

		protected string gxTv_SdtPassword_Url;
		 

		protected string gxTv_SdtPassword_Login;
		 

		protected string gxTv_SdtPassword_Password;
		 

		protected string gxTv_SdtPassword_Based32key;
		 

		protected string gxTv_SdtPassword_Note;
		 
		protected bool gxTv_SdtPassword_Password_tag_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTv_SdtPassword_Password_tag = null;  
		protected bool gxTv_SdtPassword_Contact_N;
		protected GxSimpleCollection<Guid> gxTv_SdtPassword_Contact = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Password", Namespace="distributedcryptography")]
	public class SdtPassword_RESTInterface : GxGenericCollectionItem<SdtPassword>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPassword_RESTInterface( ) : base()
		{	
		}

		public SdtPassword_RESTInterface( SdtPassword psdt ) : base(psdt)
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

		[JsonPropertyName("password")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="password", Order=4)]
		public  string gxTpr_Password
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Password);

			}
			set { 
				 sdt.gxTpr_Password = value;
			}
		}

		[JsonPropertyName("based32Key")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="based32Key", Order=5)]
		public  string gxTpr_Based32key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Based32key);

			}
			set { 
				 sdt.gxTpr_Based32key = value;
			}
		}

		[JsonPropertyName("note")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="note", Order=6)]
		public  string gxTpr_Note
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Note);

			}
			set { 
				 sdt.gxTpr_Note = value;
			}
		}

		[JsonPropertyName("Password_tag")]
		[JsonPropertyOrder(7)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Password_tag", Order=7, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_tag_RESTInterface> gxTpr_Password_tag
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Password_tag_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_tag_RESTInterface>(sdt.gxTpr_Password_tag);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Password_tag);
			}
		}

		[JsonPropertyName("Contact")]
		[JsonPropertyOrder(8)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Contact", Order=8, EmitDefaultValue=false)]
		public  GxSimpleCollection<Guid> gxTpr_Contact
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Contact_GxSimpleCollection_Json())
					return sdt.gxTpr_Contact;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Contact = value ;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtPassword sdt
		{
			get { 
				return (SdtPassword)Sdt;
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
				sdt = new SdtPassword() ;
			}
		}
	}
	#endregion
}