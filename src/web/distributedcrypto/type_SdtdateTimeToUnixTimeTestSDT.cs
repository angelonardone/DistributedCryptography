/*
				   File: type_SdtdateTimeToUnixTimeTestSDT
			Description: dateTimeToUnixTimeTestSDT
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
namespace GeneXus.Programs.distributedcrypto
{
	[XmlRoot(ElementName="dateTimeToUnixTimeTestSDT")]
	[XmlType(TypeName="dateTimeToUnixTimeTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdateTimeToUnixTimeTestSDT : GxUserType
	{
		public SdtdateTimeToUnixTimeTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtdateTimeToUnixTimeTestSDT_Testcaseid = "";

			gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtdateTimeToUnixTimeTestSDT_Msgunixtime = "";

		}

		public SdtdateTimeToUnixTimeTestSDT(IGxContext context)
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
			AddObjectProperty("TestCaseId", gxTpr_Testcaseid, false);


			datetime_STZ = gxTpr_Datetime;
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
			AddObjectProperty("dateTime", sDateCnv, false);



			AddObjectProperty("unixTime", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Unixtime, 18, 0)), false);


			AddObjectProperty("ExpectedunixTime", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Expectedunixtime, 18, 0)), false);


			AddObjectProperty("MsgunixTime", gxTpr_Msgunixtime, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdtdateTimeToUnixTimeTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="dateTime")]
		[XmlElement(ElementName="dateTime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime).value ;
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime; 
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="unixTime")]
		[XmlElement(ElementName="unixTime")]
		public long gxTpr_Unixtime
		{
			get {
				return gxTv_SdtdateTimeToUnixTimeTestSDT_Unixtime; 
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Unixtime = value;
				SetDirty("Unixtime");
			}
		}




		[SoapElement(ElementName="ExpectedunixTime")]
		[XmlElement(ElementName="ExpectedunixTime")]
		public long gxTpr_Expectedunixtime
		{
			get {
				return gxTv_SdtdateTimeToUnixTimeTestSDT_Expectedunixtime; 
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Expectedunixtime = value;
				SetDirty("Expectedunixtime");
			}
		}




		[SoapElement(ElementName="MsgunixTime")]
		[XmlElement(ElementName="MsgunixTime")]
		public string gxTpr_Msgunixtime
		{
			get {
				return gxTv_SdtdateTimeToUnixTimeTestSDT_Msgunixtime; 
			}
			set {
				gxTv_SdtdateTimeToUnixTimeTestSDT_Msgunixtime = value;
				SetDirty("Msgunixtime");
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
			gxTv_SdtdateTimeToUnixTimeTestSDT_Testcaseid = "";
			gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime = (DateTime)(DateTime.MinValue);


			gxTv_SdtdateTimeToUnixTimeTestSDT_Msgunixtime = "";
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

		protected string gxTv_SdtdateTimeToUnixTimeTestSDT_Testcaseid;
		 

		protected DateTime gxTv_SdtdateTimeToUnixTimeTestSDT_Datetime;
		 

		protected long gxTv_SdtdateTimeToUnixTimeTestSDT_Unixtime;
		 

		protected long gxTv_SdtdateTimeToUnixTimeTestSDT_Expectedunixtime;
		 

		protected string gxTv_SdtdateTimeToUnixTimeTestSDT_Msgunixtime;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"dateTimeToUnixTimeTestSDT", Namespace="distributedcryptography")]
	public class SdtdateTimeToUnixTimeTestSDT_RESTInterface : GxGenericCollectionItem<SdtdateTimeToUnixTimeTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdateTimeToUnixTimeTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtdateTimeToUnixTimeTestSDT_RESTInterface( SdtdateTimeToUnixTimeTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TestCaseId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TestCaseId", Order=0)]
		public  string gxTpr_Testcaseid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Testcaseid);

			}
			set { 
				 sdt.gxTpr_Testcaseid = value;
			}
		}

		[JsonPropertyName("dateTime")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="dateTime", Order=1)]
		public  string gxTpr_Datetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Datetime,context);

			}
			set { 
				sdt.gxTpr_Datetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("unixTime")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="unixTime", Order=2)]
		public  string gxTpr_Unixtime
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Unixtime, 18, 0));

			}
			set { 
				sdt.gxTpr_Unixtime = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("ExpectedunixTime")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="ExpectedunixTime", Order=3)]
		public  string gxTpr_Expectedunixtime
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Expectedunixtime, 18, 0));

			}
			set { 
				sdt.gxTpr_Expectedunixtime = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("MsgunixTime")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="MsgunixTime", Order=4)]
		public  string gxTpr_Msgunixtime
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgunixtime);

			}
			set { 
				 sdt.gxTpr_Msgunixtime = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtdateTimeToUnixTimeTestSDT sdt
		{
			get { 
				return (SdtdateTimeToUnixTimeTestSDT)Sdt;
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
				sdt = new SdtdateTimeToUnixTimeTestSDT() ;
			}
		}
	}
	#endregion
}