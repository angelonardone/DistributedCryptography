/*
				   File: type_SdtPaymentRequest
			Description: PaymentRequest
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="PaymentRequest")]
	[XmlType(TypeName="PaymentRequest" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPaymentRequest : GxUserType
	{
		public SdtPaymentRequest( )
		{
			/* Constructor for serialization */
			gxTv_SdtPaymentRequest_Address = "";

			gxTv_SdtPaymentRequest_Label = "";

			gxTv_SdtPaymentRequest_Createdat = (DateTime)(DateTime.MinValue);

			gxTv_SdtPaymentRequest_Expiresat = (DateTime)(DateTime.MinValue);

			gxTv_SdtPaymentRequest_Status = "";

			gxTv_SdtPaymentRequest_Uri = "";

		}

		public SdtPaymentRequest(IGxContext context)
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
			AddObjectProperty("requestId", gxTpr_Requestid, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("addressIndex", gxTpr_Addressindex, false);


			AddObjectProperty("label", gxTpr_Label, false);


			AddObjectProperty("amount", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amount, 16, 8)), false);


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



			datetime_STZ = gxTpr_Expiresat;
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
			AddObjectProperty("expiresAt", sDateCnv, false);



			AddObjectProperty("cancelled", gxTpr_Cancelled, false);


			AddObjectProperty("status", gxTpr_Status, false);


			AddObjectProperty("receivedAmount", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Receivedamount, 16, 8)), false);


			AddObjectProperty("uri", gxTpr_Uri, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="requestId")]
		[XmlElement(ElementName="requestId")]
		public Guid gxTpr_Requestid
		{
			get {
				return gxTv_SdtPaymentRequest_Requestid; 
			}
			set {
				gxTv_SdtPaymentRequest_Requestid = value;
				SetDirty("Requestid");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtPaymentRequest_Address; 
			}
			set {
				gxTv_SdtPaymentRequest_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="addressIndex")]
		[XmlElement(ElementName="addressIndex")]
		public long gxTpr_Addressindex
		{
			get {
				return gxTv_SdtPaymentRequest_Addressindex; 
			}
			set {
				gxTv_SdtPaymentRequest_Addressindex = value;
				SetDirty("Addressindex");
			}
		}




		[SoapElement(ElementName="label")]
		[XmlElement(ElementName="label")]
		public string gxTpr_Label
		{
			get {
				return gxTv_SdtPaymentRequest_Label; 
			}
			set {
				gxTv_SdtPaymentRequest_Label = value;
				SetDirty("Label");
			}
		}



		[SoapElement(ElementName="amount")]
		[XmlElement(ElementName="amount")]
		public string gxTpr_Amount_double
		{
			get {
				return Convert.ToString(gxTv_SdtPaymentRequest_Amount, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtPaymentRequest_Amount = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amount
		{
			get {
				return gxTv_SdtPaymentRequest_Amount; 
			}
			set {
				gxTv_SdtPaymentRequest_Amount = value;
				SetDirty("Amount");
			}
		}



		[SoapElement(ElementName="createdAt")]
		[XmlElement(ElementName="createdAt" , IsNullable=true)]
		public string gxTpr_Createdat_Nullable
		{
			get {
				if ( gxTv_SdtPaymentRequest_Createdat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtPaymentRequest_Createdat).value ;
			}
			set {
				gxTv_SdtPaymentRequest_Createdat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Createdat
		{
			get {
				return gxTv_SdtPaymentRequest_Createdat; 
			}
			set {
				gxTv_SdtPaymentRequest_Createdat = value;
				SetDirty("Createdat");
			}
		}


		[SoapElement(ElementName="expiresAt")]
		[XmlElement(ElementName="expiresAt" , IsNullable=true)]
		public string gxTpr_Expiresat_Nullable
		{
			get {
				if ( gxTv_SdtPaymentRequest_Expiresat == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtPaymentRequest_Expiresat).value ;
			}
			set {
				gxTv_SdtPaymentRequest_Expiresat = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Expiresat
		{
			get {
				return gxTv_SdtPaymentRequest_Expiresat; 
			}
			set {
				gxTv_SdtPaymentRequest_Expiresat = value;
				SetDirty("Expiresat");
			}
		}



		[SoapElement(ElementName="cancelled")]
		[XmlElement(ElementName="cancelled")]
		public bool gxTpr_Cancelled
		{
			get {
				return gxTv_SdtPaymentRequest_Cancelled; 
			}
			set {
				gxTv_SdtPaymentRequest_Cancelled = value;
				SetDirty("Cancelled");
			}
		}




		[SoapElement(ElementName="status")]
		[XmlElement(ElementName="status")]
		public string gxTpr_Status
		{
			get {
				return gxTv_SdtPaymentRequest_Status; 
			}
			set {
				gxTv_SdtPaymentRequest_Status = value;
				SetDirty("Status");
			}
		}



		[SoapElement(ElementName="receivedAmount")]
		[XmlElement(ElementName="receivedAmount")]
		public string gxTpr_Receivedamount_double
		{
			get {
				return Convert.ToString(gxTv_SdtPaymentRequest_Receivedamount, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtPaymentRequest_Receivedamount = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Receivedamount
		{
			get {
				return gxTv_SdtPaymentRequest_Receivedamount; 
			}
			set {
				gxTv_SdtPaymentRequest_Receivedamount = value;
				SetDirty("Receivedamount");
			}
		}




		[SoapElement(ElementName="uri")]
		[XmlElement(ElementName="uri")]
		public string gxTpr_Uri
		{
			get {
				return gxTv_SdtPaymentRequest_Uri; 
			}
			set {
				gxTv_SdtPaymentRequest_Uri = value;
				SetDirty("Uri");
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
			gxTv_SdtPaymentRequest_Address = "";

			gxTv_SdtPaymentRequest_Label = "";

			gxTv_SdtPaymentRequest_Createdat = (DateTime)(DateTime.MinValue);
			gxTv_SdtPaymentRequest_Expiresat = (DateTime)(DateTime.MinValue);

			gxTv_SdtPaymentRequest_Status = "";

			gxTv_SdtPaymentRequest_Uri = "";
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

		protected Guid gxTv_SdtPaymentRequest_Requestid;
		 

		protected string gxTv_SdtPaymentRequest_Address;
		 

		protected long gxTv_SdtPaymentRequest_Addressindex;
		 

		protected string gxTv_SdtPaymentRequest_Label;
		 

		protected decimal gxTv_SdtPaymentRequest_Amount;
		 

		protected DateTime gxTv_SdtPaymentRequest_Createdat;
		 

		protected DateTime gxTv_SdtPaymentRequest_Expiresat;
		 

		protected bool gxTv_SdtPaymentRequest_Cancelled;
		 

		protected string gxTv_SdtPaymentRequest_Status;
		 

		protected decimal gxTv_SdtPaymentRequest_Receivedamount;
		 

		protected string gxTv_SdtPaymentRequest_Uri;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"PaymentRequest", Namespace="distributedcryptography")]
	public class SdtPaymentRequest_RESTInterface : GxGenericCollectionItem<SdtPaymentRequest>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPaymentRequest_RESTInterface( ) : base()
		{	
		}

		public SdtPaymentRequest_RESTInterface( SdtPaymentRequest psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("requestId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="requestId", Order=0)]
		public Guid gxTpr_Requestid
		{
			get { 
				return sdt.gxTpr_Requestid;

			}
			set { 
				sdt.gxTpr_Requestid = value;
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="address", Order=1)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("addressIndex")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="addressIndex", Order=2)]
		public  string gxTpr_Addressindex
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Addressindex, 10, 0));

			}
			set { 
				sdt.gxTpr_Addressindex = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("label")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="label", Order=3)]
		public  string gxTpr_Label
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Label);

			}
			set { 
				 sdt.gxTpr_Label = value;
			}
		}

		[JsonPropertyName("amount")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="amount", Order=4)]
		public  string gxTpr_Amount
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amount, 16, 8));

			}
			set { 
				sdt.gxTpr_Amount =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("createdAt")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="createdAt", Order=5)]
		public  string gxTpr_Createdat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Createdat,context);

			}
			set { 
				sdt.gxTpr_Createdat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("expiresAt")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="expiresAt", Order=6)]
		public  string gxTpr_Expiresat
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Expiresat,context);

			}
			set { 
				sdt.gxTpr_Expiresat = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("cancelled")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="cancelled", Order=7)]
		public bool gxTpr_Cancelled
		{
			get { 
				return sdt.gxTpr_Cancelled;

			}
			set { 
				sdt.gxTpr_Cancelled = value;
			}
		}

		[JsonPropertyName("status")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="status", Order=8)]
		public  string gxTpr_Status
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Status);

			}
			set { 
				 sdt.gxTpr_Status = value;
			}
		}

		[JsonPropertyName("receivedAmount")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="receivedAmount", Order=9)]
		public  string gxTpr_Receivedamount
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Receivedamount, 16, 8));

			}
			set { 
				sdt.gxTpr_Receivedamount =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("uri")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="uri", Order=10)]
		public  string gxTpr_Uri
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Uri);

			}
			set { 
				 sdt.gxTpr_Uri = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtPaymentRequest sdt
		{
			get { 
				return (SdtPaymentRequest)Sdt;
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
				sdt = new SdtPaymentRequest() ;
			}
		}
	}
	#endregion
}