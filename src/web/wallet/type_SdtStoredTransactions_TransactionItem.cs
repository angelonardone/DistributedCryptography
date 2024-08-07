/*
				   File: type_SdtStoredTransactions_TransactionItem
			Description: Transaction
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

using GeneXus.Programs;
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="StoredTransactions.TransactionItem")]
	[XmlType(TypeName="StoredTransactions.TransactionItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtStoredTransactions_TransactionItem : GxUserType
	{
		public SdtStoredTransactions_TransactionItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtStoredTransactions_TransactionItem_Transactionid = "";

			gxTv_SdtStoredTransactions_TransactionItem_Scriptpubkey_address = "";

			gxTv_SdtStoredTransactions_TransactionItem_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtStoredTransactions_TransactionItem_Description = "";

		}

		public SdtStoredTransactions_TransactionItem(IGxContext context)
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



			AddObjectProperty("AddressGeneratedType", gxTpr_Addressgeneratedtype, false);


			AddObjectProperty("AddressCreationSequence", gxTpr_Addresscreationsequence, false);


			AddObjectProperty("Description", gxTpr_Description, false);


			AddObjectProperty("Confirmations", gxTpr_Confirmations, false);

			if (gxTv_SdtStoredTransactions_TransactionItem_Usedin != null)
			{
				AddObjectProperty("UsedIn", gxTv_SdtStoredTransactions_TransactionItem_Usedin, false);
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
				return gxTv_SdtStoredTransactions_TransactionItem_Transactionid; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_N; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_N = value;
				SetDirty("N");
			}
		}



		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtStoredTransactions_TransactionItem_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Value = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Value; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="scriptPubKey_address")]
		[XmlElement(ElementName="scriptPubKey_address")]
		public string gxTpr_Scriptpubkey_address
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Scriptpubkey_address; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Scriptpubkey_address = value;
				SetDirty("Scriptpubkey_address");
			}
		}



		[SoapElement(ElementName="datetime")]
		[XmlElement(ElementName="datetime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtStoredTransactions_TransactionItem_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtStoredTransactions_TransactionItem_Datetime).value ;
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Datetime; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="AddressGeneratedType")]
		[XmlElement(ElementName="AddressGeneratedType")]
		public short gxTpr_Addressgeneratedtype
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Addressgeneratedtype; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Addressgeneratedtype = value;
				SetDirty("Addressgeneratedtype");
			}
		}




		[SoapElement(ElementName="AddressCreationSequence")]
		[XmlElement(ElementName="AddressCreationSequence")]
		public short gxTpr_Addresscreationsequence
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Addresscreationsequence; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Addresscreationsequence = value;
				SetDirty("Addresscreationsequence");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Description; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="Confirmations")]
		[XmlElement(ElementName="Confirmations")]
		public short gxTpr_Confirmations
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_Confirmations; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Confirmations = value;
				SetDirty("Confirmations");
			}
		}



		[SoapElement(ElementName="UsedIn" )]
		[XmlElement(ElementName="UsedIn" )]
		public SdtStoredTransactions_TransactionItem_UsedIn gxTpr_Usedin
		{
			get {
				if ( gxTv_SdtStoredTransactions_TransactionItem_Usedin == null )
				{
					gxTv_SdtStoredTransactions_TransactionItem_Usedin = new SdtStoredTransactions_TransactionItem_UsedIn(context);
				}
				gxTv_SdtStoredTransactions_TransactionItem_Usedin_N = false;
				return gxTv_SdtStoredTransactions_TransactionItem_Usedin;
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_Usedin_N = false;
				gxTv_SdtStoredTransactions_TransactionItem_Usedin = value;
				SetDirty("Usedin");
			}

		}

		public void gxTv_SdtStoredTransactions_TransactionItem_Usedin_SetNull()
		{
			gxTv_SdtStoredTransactions_TransactionItem_Usedin_N = true;
			gxTv_SdtStoredTransactions_TransactionItem_Usedin = null;
		}

		public bool gxTv_SdtStoredTransactions_TransactionItem_Usedin_IsNull()
		{
			return gxTv_SdtStoredTransactions_TransactionItem_Usedin == null;
		}
		public bool ShouldSerializegxTpr_Usedin_Json()
		{
				return (gxTv_SdtStoredTransactions_TransactionItem_Usedin != null && gxTv_SdtStoredTransactions_TransactionItem_Usedin.ShouldSerializeSdtJson());

		}


		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtStoredTransactions_TransactionItem_Transactionid = "";


			gxTv_SdtStoredTransactions_TransactionItem_Scriptpubkey_address = "";
			gxTv_SdtStoredTransactions_TransactionItem_Datetime = (DateTime)(DateTime.MinValue);


			gxTv_SdtStoredTransactions_TransactionItem_Description = "";


			gxTv_SdtStoredTransactions_TransactionItem_Usedin_N = true;

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

		protected string gxTv_SdtStoredTransactions_TransactionItem_Transactionid;
		 

		protected long gxTv_SdtStoredTransactions_TransactionItem_N;
		 

		protected decimal gxTv_SdtStoredTransactions_TransactionItem_Value;
		 

		protected string gxTv_SdtStoredTransactions_TransactionItem_Scriptpubkey_address;
		 

		protected DateTime gxTv_SdtStoredTransactions_TransactionItem_Datetime;
		 

		protected short gxTv_SdtStoredTransactions_TransactionItem_Addressgeneratedtype;
		 

		protected short gxTv_SdtStoredTransactions_TransactionItem_Addresscreationsequence;
		 

		protected string gxTv_SdtStoredTransactions_TransactionItem_Description;
		 

		protected short gxTv_SdtStoredTransactions_TransactionItem_Confirmations;
		 
		protected bool gxTv_SdtStoredTransactions_TransactionItem_Usedin_N;
		protected SdtStoredTransactions_TransactionItem_UsedIn gxTv_SdtStoredTransactions_TransactionItem_Usedin = null; 



		#endregion
	}
	#region Rest interface
	[DataContract(Name=@"StoredTransactions.TransactionItem", Namespace="distributedcryptography")]
	public class SdtStoredTransactions_TransactionItem_RESTInterface : GxGenericCollectionItem<SdtStoredTransactions_TransactionItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtStoredTransactions_TransactionItem_RESTInterface( ) : base()
		{	
		}

		public SdtStoredTransactions_TransactionItem_RESTInterface( SdtStoredTransactions_TransactionItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
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
				return StringUtil.RTrim( sdt.gxTpr_Scriptpubkey_address);

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

		[DataMember(Name="AddressGeneratedType", Order=5)]
		public short gxTpr_Addressgeneratedtype
		{
			get { 
				return sdt.gxTpr_Addressgeneratedtype;

			}
			set { 
				sdt.gxTpr_Addressgeneratedtype = value;
			}
		}

		[DataMember(Name="AddressCreationSequence", Order=6)]
		public short gxTpr_Addresscreationsequence
		{
			get { 
				return sdt.gxTpr_Addresscreationsequence;

			}
			set { 
				sdt.gxTpr_Addresscreationsequence = value;
			}
		}

		[DataMember(Name="Description", Order=7)]
		public  string gxTpr_Description
		{
			get { 
				return sdt.gxTpr_Description;

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[DataMember(Name="Confirmations", Order=8)]
		public short gxTpr_Confirmations
		{
			get { 
				return sdt.gxTpr_Confirmations;

			}
			set { 
				sdt.gxTpr_Confirmations = value;
			}
		}

		[DataMember(Name="UsedIn", Order=9, EmitDefaultValue=false)]
		public SdtStoredTransactions_TransactionItem_UsedIn_RESTInterface gxTpr_Usedin
		{
			get {
				if (sdt.ShouldSerializegxTpr_Usedin_Json())
					return new SdtStoredTransactions_TransactionItem_UsedIn_RESTInterface(sdt.gxTpr_Usedin);
				else
					return null;

			}

			set {
				sdt.gxTpr_Usedin = value.sdt;
			}

		}


		#endregion

		public SdtStoredTransactions_TransactionItem sdt
		{
			get { 
				return (SdtStoredTransactions_TransactionItem)Sdt;
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
				sdt = new SdtStoredTransactions_TransactionItem() ;
			}
		}
	}
	#endregion
}