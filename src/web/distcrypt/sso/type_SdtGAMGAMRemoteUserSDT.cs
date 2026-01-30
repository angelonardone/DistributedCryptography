/*
				   File: type_SdtGAMGAMRemoteUserSDT
			Description: GAMGAMRemoteUserSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
using GeneXus.Programs.distcrypt;

namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="GAMGAMRemoteUserSDT")]
	[XmlType(TypeName="GAMGAMRemoteUserSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGAMGAMRemoteUserSDT : GxUserType
	{
		public SdtGAMGAMRemoteUserSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGAMGAMRemoteUserSDT_Guid = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Username = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Email = "";

			gxTv_SdtGAMGAMRemoteUserSDT_First_name = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Last_name = "";

			gxTv_SdtGAMGAMRemoteUserSDT_External_id = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Gender = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Url_image = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Url_profile = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Phone = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Address = "";

			gxTv_SdtGAMGAMRemoteUserSDT_City = "";

			gxTv_SdtGAMGAMRemoteUserSDT_State = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Post_code = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Language = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Timezone = "";

		}

		public SdtGAMGAMRemoteUserSDT(IGxContext context)
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
			AddObjectProperty("guid", gxTpr_Guid, false);


			AddObjectProperty("username", gxTpr_Username, false);


			AddObjectProperty("email", gxTpr_Email, false);


			AddObjectProperty("verified_email", gxTpr_Verified_email, false);


			AddObjectProperty("first_name", gxTpr_First_name, false);


			AddObjectProperty("last_name", gxTpr_Last_name, false);


			AddObjectProperty("external_id", gxTpr_External_id, false);


			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(gxTpr_Birthday)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Month(gxTpr_Birthday)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(gxTpr_Birthday)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("birthday", sDateCnv, false);



			AddObjectProperty("gender", gxTpr_Gender, false);


			AddObjectProperty("url_image", gxTpr_Url_image, false);


			AddObjectProperty("url_profile", gxTpr_Url_profile, false);


			AddObjectProperty("phone", gxTpr_Phone, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("city", gxTpr_City, false);


			AddObjectProperty("state", gxTpr_State, false);


			AddObjectProperty("post_code", gxTpr_Post_code, false);


			AddObjectProperty("language", gxTpr_Language, false);


			AddObjectProperty("timezone", gxTpr_Timezone, false);

			if (gxTv_SdtGAMGAMRemoteUserSDT_Attributes != null)
			{
				AddObjectProperty("attributes", gxTv_SdtGAMGAMRemoteUserSDT_Attributes, false);
			}
			if (gxTv_SdtGAMGAMRemoteUserSDT_Roles != null)
			{
				AddObjectProperty("roles", gxTv_SdtGAMGAMRemoteUserSDT_Roles, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="guid")]
		[XmlElement(ElementName="guid")]
		public string gxTpr_Guid
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Guid; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Guid = value;
				SetDirty("Guid");
			}
		}




		[SoapElement(ElementName="username")]
		[XmlElement(ElementName="username")]
		public string gxTpr_Username
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Username; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Username = value;
				SetDirty("Username");
			}
		}




		[SoapElement(ElementName="email")]
		[XmlElement(ElementName="email")]
		public string gxTpr_Email
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Email; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Email = value;
				SetDirty("Email");
			}
		}




		[SoapElement(ElementName="verified_email")]
		[XmlElement(ElementName="verified_email")]
		public bool gxTpr_Verified_email
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Verified_email; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Verified_email = value;
				SetDirty("Verified_email");
			}
		}




		[SoapElement(ElementName="first_name")]
		[XmlElement(ElementName="first_name")]
		public string gxTpr_First_name
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_First_name; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_First_name = value;
				SetDirty("First_name");
			}
		}




		[SoapElement(ElementName="last_name")]
		[XmlElement(ElementName="last_name")]
		public string gxTpr_Last_name
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Last_name; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Last_name = value;
				SetDirty("Last_name");
			}
		}




		[SoapElement(ElementName="external_id")]
		[XmlElement(ElementName="external_id")]
		public string gxTpr_External_id
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_External_id; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_External_id = value;
				SetDirty("External_id");
			}
		}



		[SoapElement(ElementName="birthday")]
		[XmlElement(ElementName="birthday" , IsNullable=true)]
		public string gxTpr_Birthday_Nullable
		{
			get {
				if ( gxTv_SdtGAMGAMRemoteUserSDT_Birthday == DateTime.MinValue)
					return null;
				return new GxDateString(gxTv_SdtGAMGAMRemoteUserSDT_Birthday).value ;
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Birthday = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Birthday
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Birthday; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Birthday = value;
				SetDirty("Birthday");
			}
		}



		[SoapElement(ElementName="gender")]
		[XmlElement(ElementName="gender")]
		public string gxTpr_Gender
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Gender; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Gender = value;
				SetDirty("Gender");
			}
		}




		[SoapElement(ElementName="url_image")]
		[XmlElement(ElementName="url_image")]
		public string gxTpr_Url_image
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Url_image; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Url_image = value;
				SetDirty("Url_image");
			}
		}




		[SoapElement(ElementName="url_profile")]
		[XmlElement(ElementName="url_profile")]
		public string gxTpr_Url_profile
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Url_profile; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Url_profile = value;
				SetDirty("Url_profile");
			}
		}




		[SoapElement(ElementName="phone")]
		[XmlElement(ElementName="phone")]
		public string gxTpr_Phone
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Phone; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Phone = value;
				SetDirty("Phone");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Address; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="city")]
		[XmlElement(ElementName="city")]
		public string gxTpr_City
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_City; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_City = value;
				SetDirty("City");
			}
		}




		[SoapElement(ElementName="state")]
		[XmlElement(ElementName="state")]
		public string gxTpr_State
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_State; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_State = value;
				SetDirty("State");
			}
		}




		[SoapElement(ElementName="post_code")]
		[XmlElement(ElementName="post_code")]
		public string gxTpr_Post_code
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Post_code; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Post_code = value;
				SetDirty("Post_code");
			}
		}




		[SoapElement(ElementName="language")]
		[XmlElement(ElementName="language")]
		public string gxTpr_Language
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Language; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Language = value;
				SetDirty("Language");
			}
		}




		[SoapElement(ElementName="timezone")]
		[XmlElement(ElementName="timezone")]
		public string gxTpr_Timezone
		{
			get {
				return gxTv_SdtGAMGAMRemoteUserSDT_Timezone; 
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Timezone = value;
				SetDirty("Timezone");
			}
		}




		[SoapElement(ElementName="attributes" )]
		[XmlArray(ElementName="attributes"  )]
		[XmlArrayItemAttribute(ElementName="attribute" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT> gxTpr_Attributes_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGAMGAMRemoteUserSDT_Attributes == null )
				{
					gxTv_SdtGAMGAMRemoteUserSDT_Attributes = new GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT>( context, "GAMWSLoginOutUserAttSDT", "");
				}
				return gxTv_SdtGAMGAMRemoteUserSDT_Attributes;
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N = false;
				gxTv_SdtGAMGAMRemoteUserSDT_Attributes = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT> gxTpr_Attributes
		{
			get {
				if ( gxTv_SdtGAMGAMRemoteUserSDT_Attributes == null )
				{
					gxTv_SdtGAMGAMRemoteUserSDT_Attributes = new GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT>( context, "GAMWSLoginOutUserAttSDT", "");
				}
				gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N = false;
				SetDirty("Attributes");
				return gxTv_SdtGAMGAMRemoteUserSDT_Attributes ;
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N = false;
				gxTv_SdtGAMGAMRemoteUserSDT_Attributes = value;
				SetDirty("Attributes");
			}
		}

		public void gxTv_SdtGAMGAMRemoteUserSDT_Attributes_SetNull()
		{
			gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N = true;
			gxTv_SdtGAMGAMRemoteUserSDT_Attributes = null;
		}

		public bool gxTv_SdtGAMGAMRemoteUserSDT_Attributes_IsNull()
		{
			return gxTv_SdtGAMGAMRemoteUserSDT_Attributes == null;
		}
		public bool ShouldSerializegxTpr_Attributes_GXBaseCollection_Json()
		{
			return gxTv_SdtGAMGAMRemoteUserSDT_Attributes != null && gxTv_SdtGAMGAMRemoteUserSDT_Attributes.Count > 0;

		}


		[SoapElement(ElementName="roles" )]
		[XmlArray(ElementName="roles"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Roles_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtGAMGAMRemoteUserSDT_Roles == null )
				{
					gxTv_SdtGAMGAMRemoteUserSDT_Roles = new GxSimpleCollection<string>( );
				}
				return gxTv_SdtGAMGAMRemoteUserSDT_Roles;
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Roles_N = false;
				gxTv_SdtGAMGAMRemoteUserSDT_Roles = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Roles
		{
			get {
				if ( gxTv_SdtGAMGAMRemoteUserSDT_Roles == null )
				{
					gxTv_SdtGAMGAMRemoteUserSDT_Roles = new GxSimpleCollection<string>();
				}
				gxTv_SdtGAMGAMRemoteUserSDT_Roles_N = false;
				SetDirty("Roles");
				return gxTv_SdtGAMGAMRemoteUserSDT_Roles ;
			}
			set {
				gxTv_SdtGAMGAMRemoteUserSDT_Roles_N = false;
				gxTv_SdtGAMGAMRemoteUserSDT_Roles = value;
				SetDirty("Roles");
			}
		}

		public void gxTv_SdtGAMGAMRemoteUserSDT_Roles_SetNull()
		{
			gxTv_SdtGAMGAMRemoteUserSDT_Roles_N = true;
			gxTv_SdtGAMGAMRemoteUserSDT_Roles = null;
		}

		public bool gxTv_SdtGAMGAMRemoteUserSDT_Roles_IsNull()
		{
			return gxTv_SdtGAMGAMRemoteUserSDT_Roles == null;
		}
		public bool ShouldSerializegxTpr_Roles_GxSimpleCollection_Json()
		{
			return gxTv_SdtGAMGAMRemoteUserSDT_Roles != null && gxTv_SdtGAMGAMRemoteUserSDT_Roles.Count > 0;

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
			gxTv_SdtGAMGAMRemoteUserSDT_Guid = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Username = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Email = "";

			gxTv_SdtGAMGAMRemoteUserSDT_First_name = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Last_name = "";
			gxTv_SdtGAMGAMRemoteUserSDT_External_id = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Gender = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Url_image = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Url_profile = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Phone = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Address = "";
			gxTv_SdtGAMGAMRemoteUserSDT_City = "";
			gxTv_SdtGAMGAMRemoteUserSDT_State = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Post_code = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Language = "";
			gxTv_SdtGAMGAMRemoteUserSDT_Timezone = "";

			gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N = true;


			gxTv_SdtGAMGAMRemoteUserSDT_Roles_N = true;

			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected string gxTv_SdtGAMGAMRemoteUserSDT_Guid;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Username;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Email;
		 

		protected bool gxTv_SdtGAMGAMRemoteUserSDT_Verified_email;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_First_name;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Last_name;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_External_id;
		 

		protected DateTime gxTv_SdtGAMGAMRemoteUserSDT_Birthday;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Gender;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Url_image;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Url_profile;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Phone;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Address;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_City;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_State;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Post_code;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Language;
		 

		protected string gxTv_SdtGAMGAMRemoteUserSDT_Timezone;
		 
		protected bool gxTv_SdtGAMGAMRemoteUserSDT_Attributes_N;
		protected GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT> gxTv_SdtGAMGAMRemoteUserSDT_Attributes = null;  
		protected bool gxTv_SdtGAMGAMRemoteUserSDT_Roles_N;
		protected GxSimpleCollection<string> gxTv_SdtGAMGAMRemoteUserSDT_Roles = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GAMGAMRemoteUserSDT", Namespace="distributedcryptography")]
	public class SdtGAMGAMRemoteUserSDT_RESTInterface : GxGenericCollectionItem<SdtGAMGAMRemoteUserSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGAMGAMRemoteUserSDT_RESTInterface( ) : base()
		{	
		}

		public SdtGAMGAMRemoteUserSDT_RESTInterface( SdtGAMGAMRemoteUserSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("guid")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="guid", Order=0)]
		public  string gxTpr_Guid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Guid);

			}
			set { 
				 sdt.gxTpr_Guid = value;
			}
		}

		[JsonPropertyName("username")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="username", Order=1)]
		public  string gxTpr_Username
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Username);

			}
			set { 
				 sdt.gxTpr_Username = value;
			}
		}

		[JsonPropertyName("email")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="email", Order=2)]
		public  string gxTpr_Email
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Email);

			}
			set { 
				 sdt.gxTpr_Email = value;
			}
		}

		[JsonPropertyName("verified_email")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="verified_email", Order=3)]
		public bool gxTpr_Verified_email
		{
			get { 
				return sdt.gxTpr_Verified_email;

			}
			set { 
				sdt.gxTpr_Verified_email = value;
			}
		}

		[JsonPropertyName("first_name")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="first_name", Order=4)]
		public  string gxTpr_First_name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_First_name);

			}
			set { 
				 sdt.gxTpr_First_name = value;
			}
		}

		[JsonPropertyName("last_name")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="last_name", Order=5)]
		public  string gxTpr_Last_name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Last_name);

			}
			set { 
				 sdt.gxTpr_Last_name = value;
			}
		}

		[JsonPropertyName("external_id")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="external_id", Order=6)]
		public  string gxTpr_External_id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_External_id);

			}
			set { 
				 sdt.gxTpr_External_id = value;
			}
		}

		[JsonPropertyName("birthday")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="birthday", Order=7)]
		public  string gxTpr_Birthday
		{
			get { 
				return DateTimeUtil.DToC2( sdt.gxTpr_Birthday);

			}
			set { 
				sdt.gxTpr_Birthday = DateTimeUtil.CToD2(value);
			}
		}

		[JsonPropertyName("gender")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="gender", Order=8)]
		public  string gxTpr_Gender
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Gender);

			}
			set { 
				 sdt.gxTpr_Gender = value;
			}
		}

		[JsonPropertyName("url_image")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="url_image", Order=9)]
		public  string gxTpr_Url_image
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Url_image);

			}
			set { 
				 sdt.gxTpr_Url_image = value;
			}
		}

		[JsonPropertyName("url_profile")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="url_profile", Order=10)]
		public  string gxTpr_Url_profile
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Url_profile);

			}
			set { 
				 sdt.gxTpr_Url_profile = value;
			}
		}

		[JsonPropertyName("phone")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="phone", Order=11)]
		public  string gxTpr_Phone
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Phone);

			}
			set { 
				 sdt.gxTpr_Phone = value;
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="address", Order=12)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("city")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="city", Order=13)]
		public  string gxTpr_City
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_City);

			}
			set { 
				 sdt.gxTpr_City = value;
			}
		}

		[JsonPropertyName("state")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="state", Order=14)]
		public  string gxTpr_State
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_State);

			}
			set { 
				 sdt.gxTpr_State = value;
			}
		}

		[JsonPropertyName("post_code")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="post_code", Order=15)]
		public  string gxTpr_Post_code
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Post_code);

			}
			set { 
				 sdt.gxTpr_Post_code = value;
			}
		}

		[JsonPropertyName("language")]
		[JsonPropertyOrder(16)]
		[DataMember(Name="language", Order=16)]
		public  string gxTpr_Language
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Language);

			}
			set { 
				 sdt.gxTpr_Language = value;
			}
		}

		[JsonPropertyName("timezone")]
		[JsonPropertyOrder(17)]
		[DataMember(Name="timezone", Order=17)]
		public  string gxTpr_Timezone
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Timezone);

			}
			set { 
				 sdt.gxTpr_Timezone = value;
			}
		}

		[JsonPropertyName("attributes")]
		[JsonPropertyOrder(18)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="attributes", Order=18, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT_RESTInterface> gxTpr_Attributes
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Attributes_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttSDT_RESTInterface>(sdt.gxTpr_Attributes);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Attributes);
			}
		}

		[JsonPropertyName("roles")]
		[JsonPropertyOrder(19)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="roles", Order=19, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Roles
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Roles_GxSimpleCollection_Json())
					return sdt.gxTpr_Roles;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Roles = value ;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGAMGAMRemoteUserSDT sdt
		{
			get { 
				return (SdtGAMGAMRemoteUserSDT)Sdt;
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
				sdt = new SdtGAMGAMRemoteUserSDT() ;
			}
		}
	}
	#endregion
}