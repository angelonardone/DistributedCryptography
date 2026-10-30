/*
				   File: type_SdtCoinControlItem
			Description: CoinControlItem
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
	[XmlRoot(ElementName="CoinControlItem")]
	[XmlType(TypeName="CoinControlItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCoinControlItem : GxUserType
	{
		public SdtCoinControlItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtCoinControlItem_Outpoint = "";

			gxTv_SdtCoinControlItem_Transactionid = "";

			gxTv_SdtCoinControlItem_Address = "";

			gxTv_SdtCoinControlItem_Receiveddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtCoinControlItem_Description = "";

		}

		public SdtCoinControlItem(IGxContext context)
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
			AddObjectProperty("outpoint", gxTpr_Outpoint, false);


			AddObjectProperty("selected", gxTpr_Selected, false);


			AddObjectProperty("transactionId", gxTpr_Transactionid, false);


			AddObjectProperty("n", gxTpr_N, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("amount", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amount, 16, 8)), false);


			AddObjectProperty("confirmations", gxTpr_Confirmations, false);


			datetime_STZ = gxTpr_Receiveddatetime;
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
			AddObjectProperty("receivedDateTime", sDateCnv, false);



			AddObjectProperty("description", gxTpr_Description, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="outpoint")]
		[XmlElement(ElementName="outpoint")]
		public string gxTpr_Outpoint
		{
			get {
				return gxTv_SdtCoinControlItem_Outpoint; 
			}
			set {
				gxTv_SdtCoinControlItem_Outpoint = value;
				SetDirty("Outpoint");
			}
		}




		[SoapElement(ElementName="selected")]
		[XmlElement(ElementName="selected")]
		public bool gxTpr_Selected
		{
			get {
				return gxTv_SdtCoinControlItem_Selected; 
			}
			set {
				gxTv_SdtCoinControlItem_Selected = value;
				SetDirty("Selected");
			}
		}




		[SoapElement(ElementName="transactionId")]
		[XmlElement(ElementName="transactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtCoinControlItem_Transactionid; 
			}
			set {
				gxTv_SdtCoinControlItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtCoinControlItem_N; 
			}
			set {
				gxTv_SdtCoinControlItem_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtCoinControlItem_Address; 
			}
			set {
				gxTv_SdtCoinControlItem_Address = value;
				SetDirty("Address");
			}
		}



		[SoapElement(ElementName="amount")]
		[XmlElement(ElementName="amount")]
		public string gxTpr_Amount_double
		{
			get {
				return Convert.ToString(gxTv_SdtCoinControlItem_Amount, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtCoinControlItem_Amount = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amount
		{
			get {
				return gxTv_SdtCoinControlItem_Amount; 
			}
			set {
				gxTv_SdtCoinControlItem_Amount = value;
				SetDirty("Amount");
			}
		}




		[SoapElement(ElementName="confirmations")]
		[XmlElement(ElementName="confirmations")]
		public long gxTpr_Confirmations
		{
			get {
				return gxTv_SdtCoinControlItem_Confirmations; 
			}
			set {
				gxTv_SdtCoinControlItem_Confirmations = value;
				SetDirty("Confirmations");
			}
		}



		[SoapElement(ElementName="receivedDateTime")]
		[XmlElement(ElementName="receivedDateTime" , IsNullable=true)]
		public string gxTpr_Receiveddatetime_Nullable
		{
			get {
				if ( gxTv_SdtCoinControlItem_Receiveddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtCoinControlItem_Receiveddatetime).value ;
			}
			set {
				gxTv_SdtCoinControlItem_Receiveddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Receiveddatetime
		{
			get {
				return gxTv_SdtCoinControlItem_Receiveddatetime; 
			}
			set {
				gxTv_SdtCoinControlItem_Receiveddatetime = value;
				SetDirty("Receiveddatetime");
			}
		}



		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtCoinControlItem_Description; 
			}
			set {
				gxTv_SdtCoinControlItem_Description = value;
				SetDirty("Description");
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
			gxTv_SdtCoinControlItem_Outpoint = "";

			gxTv_SdtCoinControlItem_Transactionid = "";

			gxTv_SdtCoinControlItem_Address = "";


			gxTv_SdtCoinControlItem_Receiveddatetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtCoinControlItem_Description = "";
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

		protected string gxTv_SdtCoinControlItem_Outpoint;
		 

		protected bool gxTv_SdtCoinControlItem_Selected;
		 

		protected string gxTv_SdtCoinControlItem_Transactionid;
		 

		protected long gxTv_SdtCoinControlItem_N;
		 

		protected string gxTv_SdtCoinControlItem_Address;
		 

		protected decimal gxTv_SdtCoinControlItem_Amount;
		 

		protected long gxTv_SdtCoinControlItem_Confirmations;
		 

		protected DateTime gxTv_SdtCoinControlItem_Receiveddatetime;
		 

		protected string gxTv_SdtCoinControlItem_Description;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"CoinControlItem", Namespace="distributedcryptography")]
	public class SdtCoinControlItem_RESTInterface : GxGenericCollectionItem<SdtCoinControlItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCoinControlItem_RESTInterface( ) : base()
		{	
		}

		public SdtCoinControlItem_RESTInterface( SdtCoinControlItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("outpoint")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="outpoint", Order=0)]
		public  string gxTpr_Outpoint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Outpoint);

			}
			set { 
				 sdt.gxTpr_Outpoint = value;
			}
		}

		[JsonPropertyName("selected")]
		[JsonPropertyOrder(1)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="selected", Order=1)]
		public bool gxTpr_Selected
		{
			get { 
				return sdt.gxTpr_Selected;

			}
			set { 
				sdt.gxTpr_Selected = value;
			}
		}

		[JsonPropertyName("transactionId")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="transactionId", Order=2)]
		public  string gxTpr_Transactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Transactionid);

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
			}
		}

		[JsonPropertyName("n")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="n", Order=3)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_N, 10, 0));

			}
			set { 
				sdt.gxTpr_N = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="address", Order=4)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("amount")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="amount", Order=5)]
		public  string gxTpr_Amount
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amount, 16, 8));

			}
			set { 
				sdt.gxTpr_Amount =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("confirmations")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="confirmations", Order=6)]
		public  string gxTpr_Confirmations
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Confirmations, 10, 0));

			}
			set { 
				sdt.gxTpr_Confirmations = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("receivedDateTime")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="receivedDateTime", Order=7)]
		public  string gxTpr_Receiveddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Receiveddatetime,context);

			}
			set { 
				sdt.gxTpr_Receiveddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="description", Order=8)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtCoinControlItem sdt
		{
			get { 
				return (SdtCoinControlItem)Sdt;
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
				sdt = new SdtCoinControlItem() ;
			}
		}
	}
	#endregion
}