/*
				   File: type_SdtGroupRecordResponse_records_RecordItem
			Description: Record
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
	[XmlRoot(ElementName="GroupRecordResponse.records.RecordItem")]
	[XmlType(TypeName="GroupRecordResponse.records.RecordItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupRecordResponse_records_RecordItem : GxUserType
	{
		public SdtGroupRecordResponse_records_RecordItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupRecordResponse_records_RecordItem_Recordkey = "";

			gxTv_SdtGroupRecordResponse_records_RecordItem_Encryptedrecord = "";

			gxTv_SdtGroupRecordResponse_records_RecordItem_Createdby = "";

			gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedby = "";

			gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat = (DateTime)(DateTime.MinValue);


		}

		public SdtGroupRecordResponse_records_RecordItem(IGxContext context)
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
			AddObjectProperty("RecordKey", gxTpr_Recordkey, false);


			AddObjectProperty("EncryptedRecord", gxTpr_Encryptedrecord, false);


			AddObjectProperty("CreatedBy", gxTpr_Createdby, false);


			AddObjectProperty("UpdatedBy", gxTpr_Updatedby, false);


			AddObjectProperty("Version", gxTpr_Version, false);


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
			AddObjectProperty("CreatedAt", sDateCnv, false);



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
			AddObjectProperty("UpdatedAt", sDateCnv, false);



			AddObjectProperty("Deleted", gxTpr_Deleted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="RecordKey")]
		[XmlElement(ElementName="RecordKey")]
		public string gxTpr_Recordkey
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Recordkey; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Recordkey = value;
				SetDirty("Recordkey");
			}
		}




		[SoapElement(ElementName="EncryptedRecord")]
		[XmlElement(ElementName="EncryptedRecord")]
		public string gxTpr_Encryptedrecord
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Encryptedrecord; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Encryptedrecord = value;
				SetDirty("Encryptedrecord");
			}
		}




		[SoapElement(ElementName="CreatedBy")]
		[XmlElement(ElementName="CreatedBy")]
		public string gxTpr_Createdby
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Createdby; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Createdby = value;
				SetDirty("Createdby");
			}
		}




		[SoapElement(ElementName="UpdatedBy")]
		[XmlElement(ElementName="UpdatedBy")]
		public string gxTpr_Updatedby
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedby; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedby = value;
				SetDirty("Updatedby");
			}
		}




		[SoapElement(ElementName="Version")]
		[XmlElement(ElementName="Version")]
		public long gxTpr_Version
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Version; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Version = value;
				SetDirty("Version");
			}
		}



		[SoapElement(ElementName="CreatedAt")]
		[XmlElement(ElementName="CreatedAt" , IsNullable=true)]
		public string gxTpr_Createdat_Nullable
		{
			get {
				if ( gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat).value ;
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Createdat
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat = value;
				SetDirty("Createdat");
			}
		}


		[SoapElement(ElementName="UpdatedAt")]
		[XmlElement(ElementName="UpdatedAt" , IsNullable=true)]
		public string gxTpr_Updatedat_Nullable
		{
			get {
				if ( gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat).value ;
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Updatedat
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat = value;
				SetDirty("Updatedat");
			}
		}



		[SoapElement(ElementName="Deleted")]
		[XmlElement(ElementName="Deleted")]
		public bool gxTpr_Deleted
		{
			get {
				return gxTv_SdtGroupRecordResponse_records_RecordItem_Deleted; 
			}
			set {
				gxTv_SdtGroupRecordResponse_records_RecordItem_Deleted = value;
				SetDirty("Deleted");
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
			gxTv_SdtGroupRecordResponse_records_RecordItem_Recordkey = "";
			gxTv_SdtGroupRecordResponse_records_RecordItem_Encryptedrecord = "";
			gxTv_SdtGroupRecordResponse_records_RecordItem_Createdby = "";
			gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedby = "";

			gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat = (DateTime)(DateTime.MinValue);
			gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat = (DateTime)(DateTime.MinValue);

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

		protected string gxTv_SdtGroupRecordResponse_records_RecordItem_Recordkey;
		 

		protected string gxTv_SdtGroupRecordResponse_records_RecordItem_Encryptedrecord;
		 

		protected string gxTv_SdtGroupRecordResponse_records_RecordItem_Createdby;
		 

		protected string gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedby;
		 

		protected long gxTv_SdtGroupRecordResponse_records_RecordItem_Version;
		 

		protected DateTime gxTv_SdtGroupRecordResponse_records_RecordItem_Createdat;
		 

		protected DateTime gxTv_SdtGroupRecordResponse_records_RecordItem_Updatedat;
		 

		protected bool gxTv_SdtGroupRecordResponse_records_RecordItem_Deleted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"GroupRecordResponse.records.RecordItem", Namespace="distributedcryptography")]
	public class SdtGroupRecordResponse_records_RecordItem_RESTInterface : GxGenericCollectionItem<SdtGroupRecordResponse_records_RecordItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupRecordResponse_records_RecordItem_RESTInterface( ) : base()
		{	
		}

		public SdtGroupRecordResponse_records_RecordItem_RESTInterface( SdtGroupRecordResponse_records_RecordItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("RecordKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="RecordKey", Order=0)]
		public  string gxTpr_Recordkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Recordkey);

			}
			set { 
				 sdt.gxTpr_Recordkey = value;
			}
		}

		[JsonPropertyName("EncryptedRecord")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="EncryptedRecord", Order=1)]
		public  string gxTpr_Encryptedrecord
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedrecord);

			}
			set { 
				 sdt.gxTpr_Encryptedrecord = value;
			}
		}

		[JsonPropertyName("CreatedBy")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="CreatedBy", Order=2)]
		public  string gxTpr_Createdby
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Createdby);

			}
			set { 
				 sdt.gxTpr_Createdby = value;
			}
		}

		[JsonPropertyName("UpdatedBy")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="UpdatedBy", Order=3)]
		public  string gxTpr_Updatedby
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Updatedby);

			}
			set { 
				 sdt.gxTpr_Updatedby = value;
			}
		}

		[JsonPropertyName("Version")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Version", Order=4)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Version, 10, 0));

			}
			set { 
				sdt.gxTpr_Version = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("CreatedAt")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="CreatedAt", Order=5)]
		public  string gxTpr_Createdat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Createdat,context);

			}
			set { 
				sdt.gxTpr_Createdat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("UpdatedAt")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="UpdatedAt", Order=6)]
		public  string gxTpr_Updatedat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Updatedat,context);

			}
			set { 
				sdt.gxTpr_Updatedat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("Deleted")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="Deleted", Order=7)]
		public bool gxTpr_Deleted
		{
			get { 
				return sdt.gxTpr_Deleted;

			}
			set { 
				sdt.gxTpr_Deleted = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupRecordResponse_records_RecordItem sdt
		{
			get { 
				return (SdtGroupRecordResponse_records_RecordItem)Sdt;
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
				sdt = new SdtGroupRecordResponse_records_RecordItem() ;
			}
		}
	}
	#endregion
}