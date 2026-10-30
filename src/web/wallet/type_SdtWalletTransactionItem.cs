/*
				   File: type_SdtWalletTransactionItem
			Description: WalletTransactionItem
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
	[XmlRoot(ElementName="WalletTransactionItem")]
	[XmlType(TypeName="WalletTransactionItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWalletTransactionItem : GxUserType
	{
		public SdtWalletTransactionItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtWalletTransactionItem_Transactionid = "";

			gxTv_SdtWalletTransactionItem_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtWalletTransactionItem_Kind = "";

			gxTv_SdtWalletTransactionItem_Label = "";


		}

		public SdtWalletTransactionItem(IGxContext context)
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
			AddObjectProperty("transactionId", gxTpr_Transactionid, false);


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



			AddObjectProperty("kind", gxTpr_Kind, false);


			AddObjectProperty("label", gxTpr_Label, false);


			AddObjectProperty("received", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Received, 16, 8)), false);


			AddObjectProperty("sent", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Sent, 16, 8)), false);


			AddObjectProperty("fee", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Fee, 16, 8)), false);


			AddObjectProperty("feeRate", gxTpr_Feerate, false);


			AddObjectProperty("virtualSize", gxTpr_Virtualsize, false);


			AddObjectProperty("balance", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Balance, 16, 8)), false);


			AddObjectProperty("confirmations", gxTpr_Confirmations, false);


			AddObjectProperty("pending", gxTpr_Pending, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="transactionId")]
		[XmlElement(ElementName="transactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtWalletTransactionItem_Transactionid; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}



		[SoapElement(ElementName="dateTime")]
		[XmlElement(ElementName="dateTime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtWalletTransactionItem_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtWalletTransactionItem_Datetime).value ;
			}
			set {
				gxTv_SdtWalletTransactionItem_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtWalletTransactionItem_Datetime; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="kind")]
		[XmlElement(ElementName="kind")]
		public string gxTpr_Kind
		{
			get {
				return gxTv_SdtWalletTransactionItem_Kind; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Kind = value;
				SetDirty("Kind");
			}
		}




		[SoapElement(ElementName="label")]
		[XmlElement(ElementName="label")]
		public string gxTpr_Label
		{
			get {
				return gxTv_SdtWalletTransactionItem_Label; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Label = value;
				SetDirty("Label");
			}
		}



		[SoapElement(ElementName="received")]
		[XmlElement(ElementName="received")]
		public string gxTpr_Received_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletTransactionItem_Received, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletTransactionItem_Received = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Received
		{
			get {
				return gxTv_SdtWalletTransactionItem_Received; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Received = value;
				SetDirty("Received");
			}
		}



		[SoapElement(ElementName="sent")]
		[XmlElement(ElementName="sent")]
		public string gxTpr_Sent_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletTransactionItem_Sent, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletTransactionItem_Sent = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Sent
		{
			get {
				return gxTv_SdtWalletTransactionItem_Sent; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Sent = value;
				SetDirty("Sent");
			}
		}



		[SoapElement(ElementName="fee")]
		[XmlElement(ElementName="fee")]
		public string gxTpr_Fee_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletTransactionItem_Fee, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletTransactionItem_Fee = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Fee
		{
			get {
				return gxTv_SdtWalletTransactionItem_Fee; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Fee = value;
				SetDirty("Fee");
			}
		}



		[SoapElement(ElementName="feeRate")]
		[XmlElement(ElementName="feeRate")]
		public string gxTpr_Feerate_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletTransactionItem_Feerate, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletTransactionItem_Feerate = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feerate
		{
			get {
				return gxTv_SdtWalletTransactionItem_Feerate; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Feerate = value;
				SetDirty("Feerate");
			}
		}




		[SoapElement(ElementName="virtualSize")]
		[XmlElement(ElementName="virtualSize")]
		public long gxTpr_Virtualsize
		{
			get {
				return gxTv_SdtWalletTransactionItem_Virtualsize; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Virtualsize = value;
				SetDirty("Virtualsize");
			}
		}



		[SoapElement(ElementName="balance")]
		[XmlElement(ElementName="balance")]
		public string gxTpr_Balance_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletTransactionItem_Balance, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletTransactionItem_Balance = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Balance
		{
			get {
				return gxTv_SdtWalletTransactionItem_Balance; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Balance = value;
				SetDirty("Balance");
			}
		}




		[SoapElement(ElementName="confirmations")]
		[XmlElement(ElementName="confirmations")]
		public long gxTpr_Confirmations
		{
			get {
				return gxTv_SdtWalletTransactionItem_Confirmations; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Confirmations = value;
				SetDirty("Confirmations");
			}
		}




		[SoapElement(ElementName="pending")]
		[XmlElement(ElementName="pending")]
		public bool gxTpr_Pending
		{
			get {
				return gxTv_SdtWalletTransactionItem_Pending; 
			}
			set {
				gxTv_SdtWalletTransactionItem_Pending = value;
				SetDirty("Pending");
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
			gxTv_SdtWalletTransactionItem_Transactionid = "";
			gxTv_SdtWalletTransactionItem_Datetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtWalletTransactionItem_Kind = "";
			gxTv_SdtWalletTransactionItem_Label = "";








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

		protected string gxTv_SdtWalletTransactionItem_Transactionid;
		 

		protected DateTime gxTv_SdtWalletTransactionItem_Datetime;
		 

		protected string gxTv_SdtWalletTransactionItem_Kind;
		 

		protected string gxTv_SdtWalletTransactionItem_Label;
		 

		protected decimal gxTv_SdtWalletTransactionItem_Received;
		 

		protected decimal gxTv_SdtWalletTransactionItem_Sent;
		 

		protected decimal gxTv_SdtWalletTransactionItem_Fee;
		 

		protected decimal gxTv_SdtWalletTransactionItem_Feerate;
		 

		protected long gxTv_SdtWalletTransactionItem_Virtualsize;
		 

		protected decimal gxTv_SdtWalletTransactionItem_Balance;
		 

		protected long gxTv_SdtWalletTransactionItem_Confirmations;
		 

		protected bool gxTv_SdtWalletTransactionItem_Pending;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WalletTransactionItem", Namespace="distributedcryptography")]
	public class SdtWalletTransactionItem_RESTInterface : GxGenericCollectionItem<SdtWalletTransactionItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWalletTransactionItem_RESTInterface( ) : base()
		{	
		}

		public SdtWalletTransactionItem_RESTInterface( SdtWalletTransactionItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("transactionId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="transactionId", Order=0)]
		public  string gxTpr_Transactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Transactionid);

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
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

		[JsonPropertyName("kind")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="kind", Order=2)]
		public  string gxTpr_Kind
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Kind);

			}
			set { 
				 sdt.gxTpr_Kind = value;
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

		[JsonPropertyName("received")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="received", Order=4)]
		public  string gxTpr_Received
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Received, 16, 8));

			}
			set { 
				sdt.gxTpr_Received =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sent")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="sent", Order=5)]
		public  string gxTpr_Sent
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Sent, 16, 8));

			}
			set { 
				sdt.gxTpr_Sent =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("fee")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="fee", Order=6)]
		public  string gxTpr_Fee
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Fee, 16, 8));

			}
			set { 
				sdt.gxTpr_Fee =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("feeRate")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="feeRate", Order=7)]
		public  string gxTpr_Feerate
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Feerate, 10, 2));

			}
			set { 
				sdt.gxTpr_Feerate =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("virtualSize")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="virtualSize", Order=8)]
		public  string gxTpr_Virtualsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Virtualsize, 10, 0));

			}
			set { 
				sdt.gxTpr_Virtualsize = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("balance")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="balance", Order=9)]
		public  string gxTpr_Balance
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Balance, 16, 8));

			}
			set { 
				sdt.gxTpr_Balance =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("confirmations")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="confirmations", Order=10)]
		public  string gxTpr_Confirmations
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Confirmations, 10, 0));

			}
			set { 
				sdt.gxTpr_Confirmations = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("pending")]
		[JsonPropertyOrder(11)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="pending", Order=11)]
		public bool gxTpr_Pending
		{
			get { 
				return sdt.gxTpr_Pending;

			}
			set { 
				sdt.gxTpr_Pending = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtWalletTransactionItem sdt
		{
			get { 
				return (SdtWalletTransactionItem)Sdt;
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
				sdt = new SdtWalletTransactionItem() ;
			}
		}
	}
	#endregion
}