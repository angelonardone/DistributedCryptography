/*
				   File: type_SdtGroupSharedRecord
			Description: GroupSharedRecord
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
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="GroupSharedRecord")]
	[XmlType(TypeName="GroupSharedRecord" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupSharedRecord : GxUserType
	{
		public SdtGroupSharedRecord( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupSharedRecord_Recordkey = "";

			gxTv_SdtGroupSharedRecord_Createdby = "";

			gxTv_SdtGroupSharedRecord_Updatedby = "";

			gxTv_SdtGroupSharedRecord_Createdat = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroupSharedRecord_Updatedat = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroupSharedRecord_Cleartext = "";

		}

		public SdtGroupSharedRecord(IGxContext context)
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
			AddObjectProperty("recordKey", gxTpr_Recordkey, false);


			AddObjectProperty("version", gxTpr_Version, false);


			AddObjectProperty("createdBy", gxTpr_Createdby, false);


			AddObjectProperty("updatedBy", gxTpr_Updatedby, false);


			datetime_STZ = gxTpr_Createdat;
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
			AddObjectProperty("createdAt", sDateCnv, false);



			datetime_STZ = gxTpr_Updatedat;
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
			AddObjectProperty("updatedAt", sDateCnv, false);



			AddObjectProperty("deleted", gxTpr_Deleted, false);


			AddObjectProperty("clearText", gxTpr_Cleartext, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="recordKey")]
		[XmlElement(ElementName="recordKey")]
		public string gxTpr_Recordkey
		{
			get {
				return gxTv_SdtGroupSharedRecord_Recordkey; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Recordkey = value;
				SetDirty("Recordkey");
			}
		}




		[SoapElement(ElementName="version")]
		[XmlElement(ElementName="version")]
		public long gxTpr_Version
		{
			get {
				return gxTv_SdtGroupSharedRecord_Version; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="createdBy")]
		[XmlElement(ElementName="createdBy")]
		public string gxTpr_Createdby
		{
			get {
				return gxTv_SdtGroupSharedRecord_Createdby; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Createdby = value;
				SetDirty("Createdby");
			}
		}




		[SoapElement(ElementName="updatedBy")]
		[XmlElement(ElementName="updatedBy")]
		public string gxTpr_Updatedby
		{
			get {
				return gxTv_SdtGroupSharedRecord_Updatedby; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Updatedby = value;
				SetDirty("Updatedby");
			}
		}



		[SoapElement(ElementName="createdAt")]
		[XmlElement(ElementName="createdAt" , IsNullable=true)]
		public string gxTpr_Createdat_Nullable
		{
			get {
				if ( gxTv_SdtGroupSharedRecord_Createdat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroupSharedRecord_Createdat).value ;
			}
			set {
				gxTv_SdtGroupSharedRecord_Createdat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Createdat
		{
			get {
				return gxTv_SdtGroupSharedRecord_Createdat; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Createdat = value;
				SetDirty("Createdat");
			}
		}


		[SoapElement(ElementName="updatedAt")]
		[XmlElement(ElementName="updatedAt" , IsNullable=true)]
		public string gxTpr_Updatedat_Nullable
		{
			get {
				if ( gxTv_SdtGroupSharedRecord_Updatedat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroupSharedRecord_Updatedat).value ;
			}
			set {
				gxTv_SdtGroupSharedRecord_Updatedat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Updatedat
		{
			get {
				return gxTv_SdtGroupSharedRecord_Updatedat; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Updatedat = value;
				SetDirty("Updatedat");
			}
		}



		[SoapElement(ElementName="deleted")]
		[XmlElement(ElementName="deleted")]
		public bool gxTpr_Deleted
		{
			get {
				return gxTv_SdtGroupSharedRecord_Deleted; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Deleted = value;
				SetDirty("Deleted");
			}
		}




		[SoapElement(ElementName="clearText")]
		[XmlElement(ElementName="clearText")]
		public string gxTpr_Cleartext
		{
			get {
				return gxTv_SdtGroupSharedRecord_Cleartext; 
			}
			set {
				gxTv_SdtGroupSharedRecord_Cleartext = value;
				SetDirty("Cleartext");
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
			gxTv_SdtGroupSharedRecord_Recordkey = "";

			gxTv_SdtGroupSharedRecord_Createdby = "";
			gxTv_SdtGroupSharedRecord_Updatedby = "";
			gxTv_SdtGroupSharedRecord_Createdat = (DateTime)(DateTime.MinValue);
			gxTv_SdtGroupSharedRecord_Updatedat = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroupSharedRecord_Cleartext = "";
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

		protected string gxTv_SdtGroupSharedRecord_Recordkey;
		 

		protected long gxTv_SdtGroupSharedRecord_Version;
		 

		protected string gxTv_SdtGroupSharedRecord_Createdby;
		 

		protected string gxTv_SdtGroupSharedRecord_Updatedby;
		 

		protected DateTime gxTv_SdtGroupSharedRecord_Createdat;
		 

		protected DateTime gxTv_SdtGroupSharedRecord_Updatedat;
		 

		protected bool gxTv_SdtGroupSharedRecord_Deleted;
		 

		protected string gxTv_SdtGroupSharedRecord_Cleartext;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupSharedRecord", Namespace="distributedcryptography")]
	public class SdtGroupSharedRecord_RESTInterface : GxGenericCollectionItem<SdtGroupSharedRecord>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupSharedRecord_RESTInterface( ) : base()
		{	
		}

		public SdtGroupSharedRecord_RESTInterface( SdtGroupSharedRecord psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("recordKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="recordKey", Order=0)]
		public  string gxTpr_Recordkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Recordkey);

			}
			set { 
				 sdt.gxTpr_Recordkey = value;
			}
		}

		[JsonPropertyName("version")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="version", Order=1)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Version, 10, 0));

			}
			set { 
				sdt.gxTpr_Version = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("createdBy")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="createdBy", Order=2)]
		public  string gxTpr_Createdby
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Createdby);

			}
			set { 
				 sdt.gxTpr_Createdby = value;
			}
		}

		[JsonPropertyName("updatedBy")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="updatedBy", Order=3)]
		public  string gxTpr_Updatedby
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Updatedby);

			}
			set { 
				 sdt.gxTpr_Updatedby = value;
			}
		}

		[JsonPropertyName("createdAt")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="createdAt", Order=4)]
		public  string gxTpr_Createdat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Createdat,context);

			}
			set { 
				sdt.gxTpr_Createdat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("updatedAt")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="updatedAt", Order=5)]
		public  string gxTpr_Updatedat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Updatedat,context);

			}
			set { 
				sdt.gxTpr_Updatedat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("deleted")]
		[JsonPropertyOrder(6)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="deleted", Order=6)]
		public bool gxTpr_Deleted
		{
			get { 
				return sdt.gxTpr_Deleted;

			}
			set { 
				sdt.gxTpr_Deleted = value;
			}
		}

		[JsonPropertyName("clearText")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="clearText", Order=7)]
		public  string gxTpr_Cleartext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Cleartext);

			}
			set { 
				 sdt.gxTpr_Cleartext = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupSharedRecord sdt
		{
			get { 
				return (SdtGroupSharedRecord)Sdt;
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
				sdt = new SdtGroupSharedRecord() ;
			}
		}
	}
	#endregion
}