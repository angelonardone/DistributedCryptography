/*
				   File: type_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem
			Description: GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem")]
	[XmlType(TypeName="GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem : GxUserType
	{
		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Transactionid = "";

			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Scriptpubkey_address = "";

			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime = (DateTime)(DateTime.MinValue);

		}

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem(IGxContext context)
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

			if (gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used != null)
			{
				AddObjectProperty("Used", gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used, false);
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
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Transactionid; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_N; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_N = value;
				SetDirty("N");
			}
		}



		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Value = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Value; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="scriptPubKey_address")]
		[XmlElement(ElementName="scriptPubKey_address")]
		public string gxTpr_Scriptpubkey_address
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Scriptpubkey_address; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Scriptpubkey_address = value;
				SetDirty("Scriptpubkey_address");
			}
		}



		[SoapElement(ElementName="datetime")]
		[XmlElement(ElementName="datetime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime).value ;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="Confirmations")]
		[XmlElement(ElementName="Confirmations")]
		public long gxTpr_Confirmations
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Confirmations; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Confirmations = value;
				SetDirty("Confirmations");
			}
		}



		[SoapElement(ElementName="Used")]
		[XmlElement(ElementName="Used")]
		public GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used gxTpr_Used
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used == null )
				{
					gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used = new GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used(context);
				}
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used = value;
				SetDirty("Used");
			}
		}
		public void gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used_SetNull()
		{
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used_N = true;
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used = null;
		}

		public bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used_IsNull()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used == null;
		}
		public bool ShouldSerializegxTpr_Used_Json()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used != null;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Transactionid = "";


			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Scriptpubkey_address = "";
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime = (DateTime)(DateTime.MinValue);


			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used_N = true;

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

		protected string gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Transactionid;
		 

		protected long gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_N;
		 

		protected decimal gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Value;
		 

		protected string gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Scriptpubkey_address;
		 

		protected DateTime gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Datetime;
		 

		protected long gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Confirmations;
		 

		protected GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used = null;
		protected bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_Used_N;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem", Namespace="distributedcryptography")]
	public class SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_RESTInterface( SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="TransactionId", Order=0)]
		public  string gxTpr_Transactionid
		{
			get { 
				return sdt.gxTpr_Transactionid;

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
			}
		}

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

		[DataMember(Name="scriptPubKey_address", Order=3)]
		public  string gxTpr_Scriptpubkey_address
		{
			get { 
				return sdt.gxTpr_Scriptpubkey_address;

			}
			set { 
				 sdt.gxTpr_Scriptpubkey_address = value;
			}
		}

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

		[DataMember(Name="Used", Order=6, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_RESTInterface gxTpr_Used
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Used_Json())
					return new GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_RESTInterface(sdt.gxTpr_Used);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Used = value.sdt;
			}
		}


		#endregion

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem sdt
		{
			get { 
				return (SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem)Sdt;
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
				sdt = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem() ;
			}
		}
	}
	#endregion
}