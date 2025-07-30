/*
				   File: type_SdtEvent
			Description: Event
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="Event")]
	[XmlType(TypeName="Event" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtEvent : GxUserType
	{
		public SdtEvent( )
		{
			/* Constructor for serialization */
			gxTv_SdtEvent_Id = "";

			gxTv_SdtEvent_Pubkey = "";

			gxTv_SdtEvent_Content = "";

			gxTv_SdtEvent_Sig = "";

		}

		public SdtEvent(IGxContext context)
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
			AddObjectProperty("id", gxTpr_Id, false);


			AddObjectProperty("pubkey", gxTpr_Pubkey, false);


			AddObjectProperty("created_at", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Created_at, 18, 0)), false);


			AddObjectProperty("kind", gxTpr_Kind, false);

			if (gxTv_SdtEvent_Tags != null)
			{
				AddObjectProperty("tags", gxTv_SdtEvent_Tags, false);
			}

			AddObjectProperty("content", gxTpr_Content, false);


			AddObjectProperty("sig", gxTpr_Sig, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtEvent_Id; 
			}
			set {
				gxTv_SdtEvent_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="pubkey")]
		[XmlElement(ElementName="pubkey")]
		public string gxTpr_Pubkey
		{
			get {
				return gxTv_SdtEvent_Pubkey; 
			}
			set {
				gxTv_SdtEvent_Pubkey = value;
				SetDirty("Pubkey");
			}
		}




		[SoapElement(ElementName="created_at")]
		[XmlElement(ElementName="created_at")]
		public long gxTpr_Created_at
		{
			get {
				return gxTv_SdtEvent_Created_at; 
			}
			set {
				gxTv_SdtEvent_Created_at = value;
				SetDirty("Created_at");
			}
		}




		[SoapElement(ElementName="kind")]
		[XmlElement(ElementName="kind")]
		public int gxTpr_Kind
		{
			get {
				return gxTv_SdtEvent_Kind; 
			}
			set {
				gxTv_SdtEvent_Kind = value;
				SetDirty("Kind");
			}
		}




		[SoapElement(ElementName="tags" )]
		[XmlArray(ElementName="tags"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nostr.SdtTagsOfTags> gxTpr_Tags_GXBaseCollection
		{
			get {
				if ( gxTv_SdtEvent_Tags == null )
				{
					gxTv_SdtEvent_Tags = new GXBaseCollection<GeneXus.Programs.nostr.SdtTagsOfTags>( context, "TagsOfTags", "");
				}
				return gxTv_SdtEvent_Tags;
			}
			set {
				gxTv_SdtEvent_Tags_N = false;
				gxTv_SdtEvent_Tags = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nostr.SdtTagsOfTags> gxTpr_Tags
		{
			get {
				if ( gxTv_SdtEvent_Tags == null )
				{
					gxTv_SdtEvent_Tags = new GXBaseCollection<GeneXus.Programs.nostr.SdtTagsOfTags>( context, "TagsOfTags", "");
				}
				gxTv_SdtEvent_Tags_N = false;
				SetDirty("Tags");
				return gxTv_SdtEvent_Tags ;
			}
			set {
				gxTv_SdtEvent_Tags_N = false;
				gxTv_SdtEvent_Tags = value;
				SetDirty("Tags");
			}
		}

		public void gxTv_SdtEvent_Tags_SetNull()
		{
			gxTv_SdtEvent_Tags_N = true;
			gxTv_SdtEvent_Tags = null;
		}

		public bool gxTv_SdtEvent_Tags_IsNull()
		{
			return gxTv_SdtEvent_Tags == null;
		}
		public bool ShouldSerializegxTpr_Tags_GXBaseCollection_Json()
		{
			return gxTv_SdtEvent_Tags != null && gxTv_SdtEvent_Tags.Count > 0;

		}


		[SoapElement(ElementName="content")]
		[XmlElement(ElementName="content")]
		public string gxTpr_Content
		{
			get {
				return gxTv_SdtEvent_Content; 
			}
			set {
				gxTv_SdtEvent_Content = value;
				SetDirty("Content");
			}
		}




		[SoapElement(ElementName="sig")]
		[XmlElement(ElementName="sig")]
		public string gxTpr_Sig
		{
			get {
				return gxTv_SdtEvent_Sig; 
			}
			set {
				gxTv_SdtEvent_Sig = value;
				SetDirty("Sig");
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
			gxTv_SdtEvent_Id = "";
			gxTv_SdtEvent_Pubkey = "";



			gxTv_SdtEvent_Tags_N = true;

			gxTv_SdtEvent_Content = "";
			gxTv_SdtEvent_Sig = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtEvent_Id;
		 

		protected string gxTv_SdtEvent_Pubkey;
		 

		protected long gxTv_SdtEvent_Created_at;
		 

		protected int gxTv_SdtEvent_Kind;
		 
		protected bool gxTv_SdtEvent_Tags_N;
		protected GXBaseCollection<GeneXus.Programs.nostr.SdtTagsOfTags> gxTv_SdtEvent_Tags = null;  

		protected string gxTv_SdtEvent_Content;
		 

		protected string gxTv_SdtEvent_Sig;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Event", Namespace="distributedcryptography")]
	public class SdtEvent_RESTInterface : GxGenericCollectionItem<SdtEvent>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtEvent_RESTInterface( ) : base()
		{	
		}

		public SdtEvent_RESTInterface( SdtEvent psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Id);

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("pubkey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="pubkey", Order=1)]
		public  string gxTpr_Pubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Pubkey);

			}
			set { 
				 sdt.gxTpr_Pubkey = value;
			}
		}

		[JsonPropertyName("created_at")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="created_at", Order=2)]
		public  string gxTpr_Created_at
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Created_at, 18, 0));

			}
			set { 
				sdt.gxTpr_Created_at = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("kind")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="kind", Order=3)]
		public int gxTpr_Kind
		{
			get { 
				return sdt.gxTpr_Kind;

			}
			set { 
				sdt.gxTpr_Kind = value;
			}
		}

		[JsonPropertyName("tags")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="tags", Order=4, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.nostr.SdtTagsOfTags_RESTInterface> gxTpr_Tags
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Tags_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.nostr.SdtTagsOfTags_RESTInterface>(sdt.gxTpr_Tags);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Tags);
			}
		}

		[JsonPropertyName("content")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="content", Order=5)]
		public  string gxTpr_Content
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Content);

			}
			set { 
				 sdt.gxTpr_Content = value;
			}
		}

		[JsonPropertyName("sig")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="sig", Order=6)]
		public  string gxTpr_Sig
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sig);

			}
			set { 
				 sdt.gxTpr_Sig = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtEvent sdt
		{
			get { 
				return (SdtEvent)Sdt;
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
				sdt = new SdtEvent() ;
			}
		}
	}
	#endregion
}