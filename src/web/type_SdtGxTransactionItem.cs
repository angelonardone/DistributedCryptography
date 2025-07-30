/*
				   File: type_SdtGxTransactionItem
			Description: GxTransactionItem
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxTransactionItem")]
	[XmlType(TypeName="GxTransactionItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxTransactionItem : GxUserType
	{
		public SdtGxTransactionItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxTransactionItem_Transactionid = "";

			gxTv_SdtGxTransactionItem_Scriptpubkey_address = "";

			gxTv_SdtGxTransactionItem_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtGxTransactionItem_Used_N = true;

		}

		public SdtGxTransactionItem(IGxContext context)
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
			AddObjectProperty("TransactionId", gxTpr_Transactionid, false);


			AddObjectProperty("n", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_N, 18, 0)), false);


			AddObjectProperty("value", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Value, 16, 8)), false);


			AddObjectProperty("scriptPubKey_address", gxTpr_Scriptpubkey_address, false);


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
			AddObjectProperty("datetime", sDateCnv, false);



			AddObjectProperty("Confirmations", gxTpr_Confirmations, false);

			if (gxTv_SdtGxTransactionItem_Used != null)
			{
				AddObjectProperty("Used", gxTv_SdtGxTransactionItem_Used, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TransactionId")]
		[XmlElement(ElementName="TransactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtGxTransactionItem_Transactionid; 
			}
			set {
				gxTv_SdtGxTransactionItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtGxTransactionItem_N; 
			}
			set {
				gxTv_SdtGxTransactionItem_N = value;
				SetDirty("N");
			}
		}



		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtGxTransactionItem_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtGxTransactionItem_Value = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtGxTransactionItem_Value; 
			}
			set {
				gxTv_SdtGxTransactionItem_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="scriptPubKey_address")]
		[XmlElement(ElementName="scriptPubKey_address")]
		public string gxTpr_Scriptpubkey_address
		{
			get {
				return gxTv_SdtGxTransactionItem_Scriptpubkey_address; 
			}
			set {
				gxTv_SdtGxTransactionItem_Scriptpubkey_address = value;
				SetDirty("Scriptpubkey_address");
			}
		}



		[SoapElement(ElementName="datetime")]
		[XmlElement(ElementName="datetime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtGxTransactionItem_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGxTransactionItem_Datetime).value ;
			}
			set {
				gxTv_SdtGxTransactionItem_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtGxTransactionItem_Datetime; 
			}
			set {
				gxTv_SdtGxTransactionItem_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="Confirmations")]
		[XmlElement(ElementName="Confirmations")]
		public long gxTpr_Confirmations
		{
			get {
				return gxTv_SdtGxTransactionItem_Confirmations; 
			}
			set {
				gxTv_SdtGxTransactionItem_Confirmations = value;
				SetDirty("Confirmations");
			}
		}



		[SoapElement(ElementName="Used")]
		[XmlElement(ElementName="Used")]
		public GeneXus.Programs.SdtGxUsedIn gxTpr_Used
		{
			get {
				if ( gxTv_SdtGxTransactionItem_Used == null )
				{
					gxTv_SdtGxTransactionItem_Used = new GeneXus.Programs.SdtGxUsedIn(context);
					SetDirty("Used");
				}
				return gxTv_SdtGxTransactionItem_Used; 
			}
			set {
				gxTv_SdtGxTransactionItem_Used = value;
				SetDirty("Used");
			}
		}
		public void gxTv_SdtGxTransactionItem_Used_SetNull()
		{
			gxTv_SdtGxTransactionItem_Used_N = true;
			gxTv_SdtGxTransactionItem_Used = null;
		}

		public bool gxTv_SdtGxTransactionItem_Used_IsNull()
		{
			return gxTv_SdtGxTransactionItem_Used == null;
		}
		public bool ShouldSerializegxTpr_Used_Json()
		{
			return gxTv_SdtGxTransactionItem_Used != null;

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
			gxTv_SdtGxTransactionItem_Transactionid = "";


			gxTv_SdtGxTransactionItem_Scriptpubkey_address = "";
			gxTv_SdtGxTransactionItem_Datetime = (DateTime)(DateTime.MinValue);


			gxTv_SdtGxTransactionItem_Used_N = true;

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

		protected string gxTv_SdtGxTransactionItem_Transactionid;
		 

		protected long gxTv_SdtGxTransactionItem_N;
		 

		protected decimal gxTv_SdtGxTransactionItem_Value;
		 

		protected string gxTv_SdtGxTransactionItem_Scriptpubkey_address;
		 

		protected DateTime gxTv_SdtGxTransactionItem_Datetime;
		 

		protected long gxTv_SdtGxTransactionItem_Confirmations;
		 

		protected GeneXus.Programs.SdtGxUsedIn gxTv_SdtGxTransactionItem_Used = null;
		protected bool gxTv_SdtGxTransactionItem_Used_N;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxTransactionItem", Namespace="distributedcryptography")]
	public class SdtGxTransactionItem_RESTInterface : GxGenericCollectionItem<SdtGxTransactionItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxTransactionItem_RESTInterface( ) : base()
		{	
		}

		public SdtGxTransactionItem_RESTInterface( SdtGxTransactionItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TransactionId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TransactionId", Order=0)]
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
		[JsonPropertyOrder(1)]
		[DataMember(Name="n", Order=1)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_N, 18, 0));

			}
			set { 
				sdt.gxTpr_N = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("value")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="value", Order=2)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Value, 16, 8));

			}
			set { 
				sdt.gxTpr_Value =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("scriptPubKey_address")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="scriptPubKey_address", Order=3)]
		public  string gxTpr_Scriptpubkey_address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Scriptpubkey_address);

			}
			set { 
				 sdt.gxTpr_Scriptpubkey_address = value;
			}
		}

		[JsonPropertyName("datetime")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="datetime", Order=4)]
		public  string gxTpr_Datetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Datetime,context);

			}
			set { 
				sdt.gxTpr_Datetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("Confirmations")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="Confirmations", Order=5)]
		public  string gxTpr_Confirmations
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Confirmations, 10, 0));

			}
			set { 
				sdt.gxTpr_Confirmations = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Used")]
		[JsonPropertyOrder(6)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Used", Order=6, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtGxUsedIn_RESTInterface gxTpr_Used
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Used_Json())
					return new GeneXus.Programs.SdtGxUsedIn_RESTInterface(sdt.gxTpr_Used);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Used = value.sdt;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGxTransactionItem sdt
		{
			get { 
				return (SdtGxTransactionItem)Sdt;
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
				sdt = new SdtGxTransactionItem() ;
			}
		}
	}
	#endregion
}