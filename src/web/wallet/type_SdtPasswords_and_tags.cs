/*
				   File: type_SdtPasswords_and_tags
			Description: Passwords_and_tags
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
	[XmlRoot(ElementName="Passwords_and_tags")]
	[XmlType(TypeName="Passwords_and_tags" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPasswords_and_tags : GxUserType
	{
		public SdtPasswords_and_tags( )
		{
			/* Constructor for serialization */
		}

		public SdtPasswords_and_tags(IGxContext context)
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
			if (gxTv_SdtPasswords_and_tags_Password != null)
			{
				AddObjectProperty("Password", gxTv_SdtPasswords_and_tags_Password, false);
			}
			if (gxTv_SdtPasswords_and_tags_Password_tag != null)
			{
				AddObjectProperty("Password_tag", gxTv_SdtPasswords_and_tags_Password_tag, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Password" )]
		[XmlArray(ElementName="Password"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Password_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPasswords_and_tags_Password == null )
				{
					gxTv_SdtPasswords_and_tags_Password = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				return gxTv_SdtPasswords_and_tags_Password;
			}
			set {
				gxTv_SdtPasswords_and_tags_Password_N = false;
				gxTv_SdtPasswords_and_tags_Password = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTpr_Password
		{
			get {
				if ( gxTv_SdtPasswords_and_tags_Password == null )
				{
					gxTv_SdtPasswords_and_tags_Password = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "");
				}
				gxTv_SdtPasswords_and_tags_Password_N = false;
				SetDirty("Password");
				return gxTv_SdtPasswords_and_tags_Password ;
			}
			set {
				gxTv_SdtPasswords_and_tags_Password_N = false;
				gxTv_SdtPasswords_and_tags_Password = value;
				SetDirty("Password");
			}
		}

		public void gxTv_SdtPasswords_and_tags_Password_SetNull()
		{
			gxTv_SdtPasswords_and_tags_Password_N = true;
			gxTv_SdtPasswords_and_tags_Password = null;
		}

		public bool gxTv_SdtPasswords_and_tags_Password_IsNull()
		{
			return gxTv_SdtPasswords_and_tags_Password == null;
		}
		public bool ShouldSerializegxTpr_Password_GXBaseCollection_Json()
		{
			return gxTv_SdtPasswords_and_tags_Password != null && gxTv_SdtPasswords_and_tags_Password.Count > 0;

		}


		[SoapElement(ElementName="Password_tag" )]
		[XmlArray(ElementName="Password_tag"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag_GXBaseCollection
		{
			get {
				if ( gxTv_SdtPasswords_and_tags_Password_tag == null )
				{
					gxTv_SdtPasswords_and_tags_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				return gxTv_SdtPasswords_and_tags_Password_tag;
			}
			set {
				gxTv_SdtPasswords_and_tags_Password_tag_N = false;
				gxTv_SdtPasswords_and_tags_Password_tag = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTpr_Password_tag
		{
			get {
				if ( gxTv_SdtPasswords_and_tags_Password_tag == null )
				{
					gxTv_SdtPasswords_and_tags_Password_tag = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "");
				}
				gxTv_SdtPasswords_and_tags_Password_tag_N = false;
				SetDirty("Password_tag");
				return gxTv_SdtPasswords_and_tags_Password_tag ;
			}
			set {
				gxTv_SdtPasswords_and_tags_Password_tag_N = false;
				gxTv_SdtPasswords_and_tags_Password_tag = value;
				SetDirty("Password_tag");
			}
		}

		public void gxTv_SdtPasswords_and_tags_Password_tag_SetNull()
		{
			gxTv_SdtPasswords_and_tags_Password_tag_N = true;
			gxTv_SdtPasswords_and_tags_Password_tag = null;
		}

		public bool gxTv_SdtPasswords_and_tags_Password_tag_IsNull()
		{
			return gxTv_SdtPasswords_and_tags_Password_tag == null;
		}
		public bool ShouldSerializegxTpr_Password_tag_GXBaseCollection_Json()
		{
			return gxTv_SdtPasswords_and_tags_Password_tag != null && gxTv_SdtPasswords_and_tags_Password_tag.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Password_GXBaseCollection_Json()|| 
				 ShouldSerializegxTpr_Password_tag_GXBaseCollection_Json()||  
				false);
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
			gxTv_SdtPasswords_and_tags_Password_N = true;


			gxTv_SdtPasswords_and_tags_Password_tag_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtPasswords_and_tags_Password_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> gxTv_SdtPasswords_and_tags_Password = null;  
		protected bool gxTv_SdtPasswords_and_tags_Password_tag_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> gxTv_SdtPasswords_and_tags_Password_tag = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Passwords_and_tags", Namespace="distributedcryptography")]
	public class SdtPasswords_and_tags_RESTInterface : GxGenericCollectionItem<SdtPasswords_and_tags>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPasswords_and_tags_RESTInterface( ) : base()
		{	
		}

		public SdtPasswords_and_tags_RESTInterface( SdtPasswords_and_tags psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Password")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Password", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_RESTInterface> gxTpr_Password
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Password_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.SdtPassword_RESTInterface>(sdt.gxTpr_Password);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Password);
			}
		}

		[JsonPropertyName("Password_tag")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Password_tag", Order=1, EmitDefaultValue=false)]
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
		public SdtPasswords_and_tags sdt
		{
			get { 
				return (SdtPasswords_and_tags)Sdt;
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
				sdt = new SdtPasswords_and_tags() ;
			}
		}
	}
	#endregion
}