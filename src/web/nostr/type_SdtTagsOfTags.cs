/*
				   File: type_SdtTagsOfTags
			Description: TagsOfTags
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
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="TagsOfTags")]
	[XmlType(TypeName="TagsOfTags" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtTagsOfTags : GxUserType
	{
		public SdtTagsOfTags( )
		{
			/* Constructor for serialization */
		}

		public SdtTagsOfTags(IGxContext context)
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
			if (gxTv_SdtTagsOfTags_Tag != null)
			{
				AddObjectProperty("tag", gxTv_SdtTagsOfTags_Tag, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="tag" )]
		[XmlArray(ElementName="tag"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Tag_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtTagsOfTags_Tag == null )
				{
					gxTv_SdtTagsOfTags_Tag = new GxSimpleCollection<string>( );
				}
				return gxTv_SdtTagsOfTags_Tag;
			}
			set {
				gxTv_SdtTagsOfTags_Tag_N = false;
				gxTv_SdtTagsOfTags_Tag = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Tag
		{
			get {
				if ( gxTv_SdtTagsOfTags_Tag == null )
				{
					gxTv_SdtTagsOfTags_Tag = new GxSimpleCollection<string>();
				}
				gxTv_SdtTagsOfTags_Tag_N = false;
				SetDirty("Tag");
				return gxTv_SdtTagsOfTags_Tag ;
			}
			set {
				gxTv_SdtTagsOfTags_Tag_N = false;
				gxTv_SdtTagsOfTags_Tag = value;
				SetDirty("Tag");
			}
		}

		public void gxTv_SdtTagsOfTags_Tag_SetNull()
		{
			gxTv_SdtTagsOfTags_Tag_N = true;
			gxTv_SdtTagsOfTags_Tag = null;
		}

		public bool gxTv_SdtTagsOfTags_Tag_IsNull()
		{
			return gxTv_SdtTagsOfTags_Tag == null;
		}
		public bool ShouldSerializegxTpr_Tag_GxSimpleCollection_Json()
		{
			return gxTv_SdtTagsOfTags_Tag != null && gxTv_SdtTagsOfTags_Tag.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Tag_GxSimpleCollection_Json()||  
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
			gxTv_SdtTagsOfTags_Tag_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtTagsOfTags_Tag_N;
		protected GxSimpleCollection<string> gxTv_SdtTagsOfTags_Tag = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"TagsOfTags", Namespace="distributedcryptography")]
	public class SdtTagsOfTags_RESTInterface : GxGenericCollectionItem<SdtTagsOfTags>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtTagsOfTags_RESTInterface( ) : base()
		{	
		}

		public SdtTagsOfTags_RESTInterface( SdtTagsOfTags psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("tag")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="tag", Order=0, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Tag
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Tag_GxSimpleCollection_Json())
					return sdt.gxTpr_Tag;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Tag = value ;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtTagsOfTags sdt
		{
			get { 
				return (SdtTagsOfTags)Sdt;
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
				sdt = new SdtTagsOfTags() ;
			}
		}
	}
	#endregion
}