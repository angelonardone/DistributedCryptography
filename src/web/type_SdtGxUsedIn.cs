/*
				   File: type_SdtGxUsedIn
			Description: GxUsedIn
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxUsedIn")]
	[XmlType(TypeName="GxUsedIn" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxUsedIn : GxUserType
	{
		public SdtGxUsedIn( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxUsedIn_Usedid = "";

			gxTv_SdtGxUsedIn_Useddatetime = (DateTime)(DateTime.MinValue);

		}

		public SdtGxUsedIn(IGxContext context)
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
			AddObjectProperty("UsedId", gxTpr_Usedid, false);


			AddObjectProperty("UsedN", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Usedn, 18, 0)), false);


			datetime_STZ = gxTpr_Useddatetime;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("UsedDateTime", sDateCnv, false);


			if (gxTv_SdtGxUsedIn_Usedto != null)
			{
				AddObjectProperty("UsedTo", gxTv_SdtGxUsedIn_Usedto, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UsedId")]
		[XmlElement(ElementName="UsedId")]
		public string gxTpr_Usedid
		{
			get {
				return gxTv_SdtGxUsedIn_Usedid; 
			}
			set {
				gxTv_SdtGxUsedIn_Usedid = value;
				SetDirty("Usedid");
			}
		}




		[SoapElement(ElementName="UsedN")]
		[XmlElement(ElementName="UsedN")]
		public long gxTpr_Usedn
		{
			get {
				return gxTv_SdtGxUsedIn_Usedn; 
			}
			set {
				gxTv_SdtGxUsedIn_Usedn = value;
				SetDirty("Usedn");
			}
		}



		[SoapElement(ElementName="UsedDateTime")]
		[XmlElement(ElementName="UsedDateTime" , IsNullable=true)]
		public string gxTpr_Useddatetime_Nullable
		{
			get {
				if ( gxTv_SdtGxUsedIn_Useddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGxUsedIn_Useddatetime).value ;
			}
			set {
				gxTv_SdtGxUsedIn_Useddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Useddatetime
		{
			get {
				return gxTv_SdtGxUsedIn_Useddatetime; 
			}
			set {
				gxTv_SdtGxUsedIn_Useddatetime = value;
				SetDirty("Useddatetime");
			}
		}



		[SoapElement(ElementName="UsedTo" )]
		[XmlArray(ElementName="UsedTo"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtGXUsedToItem> gxTpr_Usedto_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGxUsedIn_Usedto == null )
				{
					gxTv_SdtGxUsedIn_Usedto = new GXBaseCollection<GeneXus.Programs.SdtGXUsedToItem>( context, "GXUsedToItem", "");
				}
				return gxTv_SdtGxUsedIn_Usedto;
			}
			set {
				gxTv_SdtGxUsedIn_Usedto_N = false;
				gxTv_SdtGxUsedIn_Usedto = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtGXUsedToItem> gxTpr_Usedto
		{
			get {
				if ( gxTv_SdtGxUsedIn_Usedto == null )
				{
					gxTv_SdtGxUsedIn_Usedto = new GXBaseCollection<GeneXus.Programs.SdtGXUsedToItem>( context, "GXUsedToItem", "");
				}
				gxTv_SdtGxUsedIn_Usedto_N = false;
				SetDirty("Usedto");
				return gxTv_SdtGxUsedIn_Usedto ;
			}
			set {
				gxTv_SdtGxUsedIn_Usedto_N = false;
				gxTv_SdtGxUsedIn_Usedto = value;
				SetDirty("Usedto");
			}
		}

		public void gxTv_SdtGxUsedIn_Usedto_SetNull()
		{
			gxTv_SdtGxUsedIn_Usedto_N = true;
			gxTv_SdtGxUsedIn_Usedto = null;
		}

		public bool gxTv_SdtGxUsedIn_Usedto_IsNull()
		{
			return gxTv_SdtGxUsedIn_Usedto == null;
		}
		public bool ShouldSerializegxTpr_Usedto_GXBaseCollection_Json()
		{
			return gxTv_SdtGxUsedIn_Usedto != null && gxTv_SdtGxUsedIn_Usedto.Count > 0;

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
			gxTv_SdtGxUsedIn_Usedid = "";

			gxTv_SdtGxUsedIn_Useddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtGxUsedIn_Usedto_N = true;

			datetime_STZ = (DateTime)(DateTime.MinValue);
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected DateTime datetime_STZ ;

		protected string gxTv_SdtGxUsedIn_Usedid;
		 

		protected long gxTv_SdtGxUsedIn_Usedn;
		 

		protected DateTime gxTv_SdtGxUsedIn_Useddatetime;
		 
		protected bool gxTv_SdtGxUsedIn_Usedto_N;
		protected GXBaseCollection<GeneXus.Programs.SdtGXUsedToItem> gxTv_SdtGxUsedIn_Usedto = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxUsedIn", Namespace="distributedcryptography")]
	public class SdtGxUsedIn_RESTInterface : GxGenericCollectionItem<SdtGxUsedIn>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxUsedIn_RESTInterface( ) : base()
		{	
		}

		public SdtGxUsedIn_RESTInterface( SdtGxUsedIn psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UsedId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UsedId", Order=0)]
		public  string gxTpr_Usedid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Usedid);

			}
			set { 
				 sdt.gxTpr_Usedid = value;
			}
		}

		[JsonPropertyName("UsedN")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="UsedN", Order=1)]
		public  string gxTpr_Usedn
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Usedn, 18, 0));

			}
			set { 
				sdt.gxTpr_Usedn = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("UsedDateTime")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="UsedDateTime", Order=2)]
		public  string gxTpr_Useddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Useddatetime,context);

			}
			set { 
				sdt.gxTpr_Useddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("UsedTo")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="UsedTo", Order=3, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtGXUsedToItem_RESTInterface> gxTpr_Usedto
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Usedto_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtGXUsedToItem_RESTInterface>(sdt.gxTpr_Usedto);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Usedto);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGxUsedIn sdt
		{
			get { 
				return (SdtGxUsedIn)Sdt;
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
				sdt = new SdtGxUsedIn() ;
			}
		}
	}
	#endregion
}