/*
				   File: type_SdtGAMWSLoginOutUserAttSDT
			Description: GAMWSLoginOutUserAttSDT
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
using GeneXus.Programs.distcrypt;
namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="GAMWSLoginOutUserAttSDT")]
	[XmlType(TypeName="GAMWSLoginOutUserAttSDT" , Namespace="GAM" )]
	[Serializable]
	public class SdtGAMWSLoginOutUserAttSDT : GxUserType
	{
		public SdtGAMWSLoginOutUserAttSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGAMWSLoginOutUserAttSDT_Id = "";

			gxTv_SdtGAMWSLoginOutUserAttSDT_Value = "";

		}

		public SdtGAMWSLoginOutUserAttSDT(IGxContext context)
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
			AddObjectProperty("Id", gxTpr_Id, false);


			AddObjectProperty("IsMultiValue", gxTpr_Ismultivalue, false);


			AddObjectProperty("Value", gxTpr_Value, false);

			if (gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues != null)
			{
				AddObjectProperty("MultiValues", gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtGAMWSLoginOutUserAttSDT_Id; 
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttSDT_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="IsMultiValue")]
		[XmlElement(ElementName="IsMultiValue")]
		public bool gxTpr_Ismultivalue
		{
			get {
				return gxTv_SdtGAMWSLoginOutUserAttSDT_Ismultivalue; 
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttSDT_Ismultivalue = value;
				SetDirty("Ismultivalue");
			}
		}




		[SoapElement(ElementName="Value")]
		[XmlElement(ElementName="Value")]
		public string gxTpr_Value
		{
			get {
				return gxTv_SdtGAMWSLoginOutUserAttSDT_Value; 
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttSDT_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="MultiValues" )]
		[XmlArray(ElementName="MultiValues"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT> gxTpr_Multivalues_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues == null )
				{
					gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = new GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT>( context, "GAMWSLoginOutUserAttMVSDT", "");
				}
				return gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues;
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N = false;
				gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT> gxTpr_Multivalues
		{
			get {
				if ( gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues == null )
				{
					gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = new GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT>( context, "GAMWSLoginOutUserAttMVSDT", "");
				}
				gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N = false;
				SetDirty("Multivalues");
				return gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues ;
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N = false;
				gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = value;
				SetDirty("Multivalues");
			}
		}

		public void gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_SetNull()
		{
			gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N = true;
			gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = null;
		}

		public bool gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_IsNull()
		{
			return gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues == null;
		}
		public bool ShouldSerializegxTpr_Multivalues_GXBaseCollection_Json()
		{
			return gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues != null && gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues.Count > 0;

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
			gxTv_SdtGAMWSLoginOutUserAttSDT_Id = "";

			gxTv_SdtGAMWSLoginOutUserAttSDT_Value = "";

			gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGAMWSLoginOutUserAttSDT_Id;
		 

		protected bool gxTv_SdtGAMWSLoginOutUserAttSDT_Ismultivalue;
		 

		protected string gxTv_SdtGAMWSLoginOutUserAttSDT_Value;
		 
		protected bool gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues_N;
		protected GXBaseCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT> gxTv_SdtGAMWSLoginOutUserAttSDT_Multivalues = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GAMWSLoginOutUserAttSDT", Namespace="GAM")]
	public class SdtGAMWSLoginOutUserAttSDT_RESTInterface : GxGenericCollectionItem<SdtGAMWSLoginOutUserAttSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGAMWSLoginOutUserAttSDT_RESTInterface( ) : base()
		{	
		}

		public SdtGAMWSLoginOutUserAttSDT_RESTInterface( SdtGAMWSLoginOutUserAttSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Id);

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("IsMultiValue")]
		[JsonPropertyOrder(1)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="IsMultiValue", Order=1)]
		public bool gxTpr_Ismultivalue
		{
			get { 
				return sdt.gxTpr_Ismultivalue;

			}
			set { 
				sdt.gxTpr_Ismultivalue = value;
			}
		}

		[JsonPropertyName("Value")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Value", Order=2)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Value);

			}
			set { 
				 sdt.gxTpr_Value = value;
			}
		}

		[JsonPropertyName("MultiValues")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="MultiValues", Order=3, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT_RESTInterface> gxTpr_Multivalues
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Multivalues_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.distcrypt.sso.SdtGAMWSLoginOutUserAttMVSDT_RESTInterface>(sdt.gxTpr_Multivalues);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Multivalues);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGAMWSLoginOutUserAttSDT sdt
		{
			get { 
				return (SdtGAMWSLoginOutUserAttSDT)Sdt;
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
				sdt = new SdtGAMWSLoginOutUserAttSDT() ;
			}
		}
	}
	#endregion
}