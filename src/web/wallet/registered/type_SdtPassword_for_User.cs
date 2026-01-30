/*
				   File: type_SdtPassword_for_User
			Description: Password_for_User
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
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Password_for_User")]
	[XmlType(TypeName="Password_for_User" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPassword_for_User : GxUserType
	{
		public SdtPassword_for_User( )
		{
			/* Constructor for serialization */
		}

		public SdtPassword_for_User(IGxContext context)
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
			AddObjectProperty("UserId", gxTpr_Userid, false);

			if (gxTv_SdtPassword_for_User_Passwords != null)
			{
				AddObjectProperty("Passwords", gxTv_SdtPassword_for_User_Passwords, false);
			}
			if (gxTv_SdtPassword_for_User_Password_tag != null)
			{
				AddObjectProperty("Password_tag", gxTv_SdtPassword_for_User_Password_tag, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UserId")]
		[XmlElement(ElementName="UserId")]
		public Guid gxTpr_Userid
		{
			get {
				return gxTv_SdtPassword_for_User_Userid; 
			}
			set {
				gxTv_SdtPassword_for_User_Userid = value;
				SetDirty("Userid");
			}
		}




		[SoapElement(ElementName="Passwords" )]
		[XmlArray(ElementName="Passwords"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Passwords_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPassword_for_User_Passwords == null )
				{
					gxTv_SdtPassword_for_User_Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				return gxTv_SdtPassword_for_User_Passwords;
			}
			set {
				gxTv_SdtPassword_for_User_Passwords_N = false;
				gxTv_SdtPassword_for_User_Passwords = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Passwords
		{
			get {
				if ( gxTv_SdtPassword_for_User_Passwords == null )
				{
					gxTv_SdtPassword_for_User_Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				gxTv_SdtPassword_for_User_Passwords_N = false;
				SetDirty("Passwords");
				return gxTv_SdtPassword_for_User_Passwords ;
			}
			set {
				gxTv_SdtPassword_for_User_Passwords_N = false;
				gxTv_SdtPassword_for_User_Passwords = value;
				SetDirty("Passwords");
			}
		}

		public void gxTv_SdtPassword_for_User_Passwords_SetNull()
		{
			gxTv_SdtPassword_for_User_Passwords_N = true;
			gxTv_SdtPassword_for_User_Passwords = null;
		}

		public bool gxTv_SdtPassword_for_User_Passwords_IsNull()
		{
			return gxTv_SdtPassword_for_User_Passwords == null;
		}
		public bool ShouldSerializegxTpr_Passwords_GXBaseCollection_Json()
		{
			return gxTv_SdtPassword_for_User_Passwords != null && gxTv_SdtPassword_for_User_Passwords.Count > 0;

		}


		[SoapElement(ElementName="Password_tag" )]
		[XmlArray(ElementName="Password_tag"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPassword_for_User_Password_tag == null )
				{
					gxTv_SdtPassword_for_User_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				return gxTv_SdtPassword_for_User_Password_tag;
			}
			set {
				gxTv_SdtPassword_for_User_Password_tag_N = false;
				gxTv_SdtPassword_for_User_Password_tag = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag
		{
			get {
				if ( gxTv_SdtPassword_for_User_Password_tag == null )
				{
					gxTv_SdtPassword_for_User_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				gxTv_SdtPassword_for_User_Password_tag_N = false;
				SetDirty("Password_tag");
				return gxTv_SdtPassword_for_User_Password_tag ;
			}
			set {
				gxTv_SdtPassword_for_User_Password_tag_N = false;
				gxTv_SdtPassword_for_User_Password_tag = value;
				SetDirty("Password_tag");
			}
		}

		public void gxTv_SdtPassword_for_User_Password_tag_SetNull()
		{
			gxTv_SdtPassword_for_User_Password_tag_N = true;
			gxTv_SdtPassword_for_User_Password_tag = null;
		}

		public bool gxTv_SdtPassword_for_User_Password_tag_IsNull()
		{
			return gxTv_SdtPassword_for_User_Password_tag == null;
		}
		public bool ShouldSerializegxTpr_Password_tag_GXBaseCollection_Json()
		{
			return gxTv_SdtPassword_for_User_Password_tag != null && gxTv_SdtPassword_for_User_Password_tag.Count > 0;

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
			gxTv_SdtPassword_for_User_Passwords_N = true;


			gxTv_SdtPassword_for_User_Password_tag_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtPassword_for_User_Userid;
		 
		protected bool gxTv_SdtPassword_for_User_Passwords_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTv_SdtPassword_for_User_Passwords = null;  
		protected bool gxTv_SdtPassword_for_User_Password_tag_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTv_SdtPassword_for_User_Password_tag = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Password_for_User", Namespace="distributedcryptography")]
	public class SdtPassword_for_User_RESTInterface : GxGenericCollectionItem<SdtPassword_for_User>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPassword_for_User_RESTInterface( ) : base()
		{	
		}

		public SdtPassword_for_User_RESTInterface( SdtPassword_for_User psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UserId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UserId", Order=0)]
		public Guid gxTpr_Userid
		{
			get { 
				return sdt.gxTpr_Userid;

			}
			set { 
				sdt.gxTpr_Userid = value;
			}
		}

		[JsonPropertyName("Passwords")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Passwords", Order=1, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_RESTInterface> gxTpr_Passwords
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Passwords_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_RESTInterface>(sdt.gxTpr_Passwords);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Passwords);
			}
		}

		[JsonPropertyName("Password_tag")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Password_tag", Order=2, EmitDefaultValue=false)]
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


		#endregion
		[JsonIgnore]
		public SdtPassword_for_User sdt
		{
			get { 
				return (SdtPassword_for_User)Sdt;
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
				sdt = new SdtPassword_for_User() ;
			}
		}
	}
	#endregion
}