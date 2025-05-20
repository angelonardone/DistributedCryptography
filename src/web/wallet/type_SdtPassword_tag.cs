/*
				   File: type_SdtPassword_tag
			Description: Password_tag
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="Password_tag")]
	[XmlType(TypeName="Password_tag" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPassword_tag : GxUserType
	{
		public SdtPassword_tag( )
		{
			/* Constructor for serialization */
			gxTv_SdtPassword_tag_Name = "";

		}

		public SdtPassword_tag(IGxContext context)
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
			AddObjectProperty("TagId", gxTpr_Tagid, false);


			AddObjectProperty("Name", gxTpr_Name, false);

			if (gxTv_SdtPassword_tag_Passwords != null)
			{
				AddObjectProperty("Passwords", gxTv_SdtPassword_tag_Passwords, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TagId")]
		[XmlElement(ElementName="TagId")]
		public Guid gxTpr_Tagid
		{
			get {
				return gxTv_SdtPassword_tag_Tagid; 
			}
			set {
				gxTv_SdtPassword_tag_Tagid = value;
				SetDirty("Tagid");
			}
		}




		[SoapElement(ElementName="Name")]
		[XmlElement(ElementName="Name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtPassword_tag_Name; 
			}
			set {
				gxTv_SdtPassword_tag_Name = value;
				SetDirty("Name");
			}
		}




		[SoapElement(ElementName="Passwords" )]
		[XmlArray(ElementName="Passwords"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Passwords_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPassword_tag_Passwords == null )
				{
					gxTv_SdtPassword_tag_Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				return gxTv_SdtPassword_tag_Passwords;
			}
			set {
				gxTv_SdtPassword_tag_Passwords_N = false;
				gxTv_SdtPassword_tag_Passwords = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Passwords
		{
			get {
				if ( gxTv_SdtPassword_tag_Passwords == null )
				{
					gxTv_SdtPassword_tag_Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				gxTv_SdtPassword_tag_Passwords_N = false;
				SetDirty("Passwords");
				return gxTv_SdtPassword_tag_Passwords ;
			}
			set {
				gxTv_SdtPassword_tag_Passwords_N = false;
				gxTv_SdtPassword_tag_Passwords = value;
				SetDirty("Passwords");
			}
		}

		public void gxTv_SdtPassword_tag_Passwords_SetNull()
		{
			gxTv_SdtPassword_tag_Passwords_N = true;
			gxTv_SdtPassword_tag_Passwords = null;
		}

		public bool gxTv_SdtPassword_tag_Passwords_IsNull()
		{
			return gxTv_SdtPassword_tag_Passwords == null;
		}
		public bool ShouldSerializegxTpr_Passwords_GXBaseCollection_Json()
		{
			return gxTv_SdtPassword_tag_Passwords != null && gxTv_SdtPassword_tag_Passwords.Count > 0;

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
			gxTv_SdtPassword_tag_Name = "";

			gxTv_SdtPassword_tag_Passwords_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtPassword_tag_Tagid;
		 

		protected string gxTv_SdtPassword_tag_Name;
		 
		protected bool gxTv_SdtPassword_tag_Passwords_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTv_SdtPassword_tag_Passwords = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Password_tag", Namespace="distributedcryptography")]
	public class SdtPassword_tag_RESTInterface : GxGenericCollectionItem<SdtPassword_tag>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPassword_tag_RESTInterface( ) : base()
		{	
		}

		public SdtPassword_tag_RESTInterface( SdtPassword_tag psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TagId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TagId", Order=0)]
		public Guid gxTpr_Tagid
		{
			get { 
				return sdt.gxTpr_Tagid;

			}
			set { 
				sdt.gxTpr_Tagid = value;
			}
		}

		[JsonPropertyName("Name")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Name", Order=1)]
		public  string gxTpr_Name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Name);

			}
			set { 
				 sdt.gxTpr_Name = value;
			}
		}

		[JsonPropertyName("Passwords")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Passwords", Order=2, EmitDefaultValue=false)]
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


		#endregion
		[JsonIgnore]
		public SdtPassword_tag sdt
		{
			get { 
				return (SdtPassword_tag)Sdt;
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
				sdt = new SdtPassword_tag() ;
			}
		}
	}
	#endregion
}