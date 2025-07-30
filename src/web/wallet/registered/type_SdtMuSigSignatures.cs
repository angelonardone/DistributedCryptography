/*
				   File: type_SdtMuSigSignatures
			Description: MuSigSignatures
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

using GeneXus.Programs;
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="MuSigSignatures")]
	[XmlType(TypeName="MuSigSignatures" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSigSignatures : GxUserType
	{
		public SdtMuSigSignatures( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSigSignatures_Description = "";

			gxTv_SdtMuSigSignatures_Signeddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtMuSigSignatures_Sendto = "";

			gxTv_SdtMuSigSignatures_Changeto = "";

			gxTv_SdtMuSigSignatures_Senderusername = "";

			gxTv_SdtMuSigSignatures_Sendersignature = "";

		}

		public SdtMuSigSignatures(IGxContext context)
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
			AddObjectProperty("id", gxTpr_Id, false);


			AddObjectProperty("description", gxTpr_Description, false);


			datetime_STZ = gxTpr_Signeddatetime;
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
			AddObjectProperty("signedDateTime", sDateCnv, false);



			AddObjectProperty("compleated", gxTpr_Compleated, false);


			AddObjectProperty("sendAllCoins", gxTpr_Sendallcoins, false);


			AddObjectProperty("sendCoins", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Sendcoins, 16, 8)), false);


			AddObjectProperty("transactionFee", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Transactionfee, 16, 8)), false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("changeTo", gxTpr_Changeto, false);


			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);


			AddObjectProperty("senderUserName", gxTpr_Senderusername, false);


			AddObjectProperty("senderSignature", gxTpr_Sendersignature, false);

			if (gxTv_SdtMuSigSignatures_Transactions != null)
			{
				AddObjectProperty("transactions", gxTv_SdtMuSigSignatures_Transactions, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtMuSigSignatures_Id; 
			}
			set {
				gxTv_SdtMuSigSignatures_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtMuSigSignatures_Description; 
			}
			set {
				gxTv_SdtMuSigSignatures_Description = value;
				SetDirty("Description");
			}
		}



		[SoapElement(ElementName="signedDateTime")]
		[XmlElement(ElementName="signedDateTime" , IsNullable=true)]
		public string gxTpr_Signeddatetime_Nullable
		{
			get {
				if ( gxTv_SdtMuSigSignatures_Signeddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtMuSigSignatures_Signeddatetime).value ;
			}
			set {
				gxTv_SdtMuSigSignatures_Signeddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Signeddatetime
		{
			get {
				return gxTv_SdtMuSigSignatures_Signeddatetime; 
			}
			set {
				gxTv_SdtMuSigSignatures_Signeddatetime = value;
				SetDirty("Signeddatetime");
			}
		}



		[SoapElement(ElementName="compleated")]
		[XmlElement(ElementName="compleated")]
		public bool gxTpr_Compleated
		{
			get {
				return gxTv_SdtMuSigSignatures_Compleated; 
			}
			set {
				gxTv_SdtMuSigSignatures_Compleated = value;
				SetDirty("Compleated");
			}
		}




		[SoapElement(ElementName="sendAllCoins")]
		[XmlElement(ElementName="sendAllCoins")]
		public bool gxTpr_Sendallcoins
		{
			get {
				return gxTv_SdtMuSigSignatures_Sendallcoins; 
			}
			set {
				gxTv_SdtMuSigSignatures_Sendallcoins = value;
				SetDirty("Sendallcoins");
			}
		}



		[SoapElement(ElementName="sendCoins")]
		[XmlElement(ElementName="sendCoins")]
		public string gxTpr_Sendcoins_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSigSignatures_Sendcoins, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSigSignatures_Sendcoins = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Sendcoins
		{
			get {
				return gxTv_SdtMuSigSignatures_Sendcoins; 
			}
			set {
				gxTv_SdtMuSigSignatures_Sendcoins = value;
				SetDirty("Sendcoins");
			}
		}



		[SoapElement(ElementName="transactionFee")]
		[XmlElement(ElementName="transactionFee")]
		public string gxTpr_Transactionfee_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSigSignatures_Transactionfee, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSigSignatures_Transactionfee = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Transactionfee
		{
			get {
				return gxTv_SdtMuSigSignatures_Transactionfee; 
			}
			set {
				gxTv_SdtMuSigSignatures_Transactionfee = value;
				SetDirty("Transactionfee");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtMuSigSignatures_Sendto; 
			}
			set {
				gxTv_SdtMuSigSignatures_Sendto = value;
				SetDirty("Sendto");
			}
		}




		[SoapElement(ElementName="changeTo")]
		[XmlElement(ElementName="changeTo")]
		public string gxTpr_Changeto
		{
			get {
				return gxTv_SdtMuSigSignatures_Changeto; 
			}
			set {
				gxTv_SdtMuSigSignatures_Changeto = value;
				SetDirty("Changeto");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtMuSigSignatures_Referencegroupid; 
			}
			set {
				gxTv_SdtMuSigSignatures_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="senderUserName")]
		[XmlElement(ElementName="senderUserName")]
		public string gxTpr_Senderusername
		{
			get {
				return gxTv_SdtMuSigSignatures_Senderusername; 
			}
			set {
				gxTv_SdtMuSigSignatures_Senderusername = value;
				SetDirty("Senderusername");
			}
		}




		[SoapElement(ElementName="senderSignature")]
		[XmlElement(ElementName="senderSignature")]
		public string gxTpr_Sendersignature
		{
			get {
				return gxTv_SdtMuSigSignatures_Sendersignature; 
			}
			set {
				gxTv_SdtMuSigSignatures_Sendersignature = value;
				SetDirty("Sendersignature");
			}
		}




		[SoapElement(ElementName="transactions" )]
		[XmlArray(ElementName="transactions"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> gxTpr_Transactions_GXBaseCollection
		{
			get {
				if ( gxTv_SdtMuSigSignatures_Transactions == null )
				{
					gxTv_SdtMuSigSignatures_Transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "");
				}
				return gxTv_SdtMuSigSignatures_Transactions;
			}
			set {
				gxTv_SdtMuSigSignatures_Transactions_N = false;
				gxTv_SdtMuSigSignatures_Transactions = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> gxTpr_Transactions
		{
			get {
				if ( gxTv_SdtMuSigSignatures_Transactions == null )
				{
					gxTv_SdtMuSigSignatures_Transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "");
				}
				gxTv_SdtMuSigSignatures_Transactions_N = false;
				SetDirty("Transactions");
				return gxTv_SdtMuSigSignatures_Transactions ;
			}
			set {
				gxTv_SdtMuSigSignatures_Transactions_N = false;
				gxTv_SdtMuSigSignatures_Transactions = value;
				SetDirty("Transactions");
			}
		}

		public void gxTv_SdtMuSigSignatures_Transactions_SetNull()
		{
			gxTv_SdtMuSigSignatures_Transactions_N = true;
			gxTv_SdtMuSigSignatures_Transactions = null;
		}

		public bool gxTv_SdtMuSigSignatures_Transactions_IsNull()
		{
			return gxTv_SdtMuSigSignatures_Transactions == null;
		}
		public bool ShouldSerializegxTpr_Transactions_GXBaseCollection_Json()
		{
			return gxTv_SdtMuSigSignatures_Transactions != null && gxTv_SdtMuSigSignatures_Transactions.Count > 0;

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
			gxTv_SdtMuSigSignatures_Description = "";
			gxTv_SdtMuSigSignatures_Signeddatetime = (DateTime)(DateTime.MinValue);




			gxTv_SdtMuSigSignatures_Sendto = "";
			gxTv_SdtMuSigSignatures_Changeto = "";

			gxTv_SdtMuSigSignatures_Senderusername = "";
			gxTv_SdtMuSigSignatures_Sendersignature = "";

			gxTv_SdtMuSigSignatures_Transactions_N = true;

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

		protected Guid gxTv_SdtMuSigSignatures_Id;
		 

		protected string gxTv_SdtMuSigSignatures_Description;
		 

		protected DateTime gxTv_SdtMuSigSignatures_Signeddatetime;
		 

		protected bool gxTv_SdtMuSigSignatures_Compleated;
		 

		protected bool gxTv_SdtMuSigSignatures_Sendallcoins;
		 

		protected decimal gxTv_SdtMuSigSignatures_Sendcoins;
		 

		protected decimal gxTv_SdtMuSigSignatures_Transactionfee;
		 

		protected string gxTv_SdtMuSigSignatures_Sendto;
		 

		protected string gxTv_SdtMuSigSignatures_Changeto;
		 

		protected Guid gxTv_SdtMuSigSignatures_Referencegroupid;
		 

		protected string gxTv_SdtMuSigSignatures_Senderusername;
		 

		protected string gxTv_SdtMuSigSignatures_Sendersignature;
		 
		protected bool gxTv_SdtMuSigSignatures_Transactions_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> gxTv_SdtMuSigSignatures_Transactions = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSigSignatures", Namespace="distributedcryptography")]
	public class SdtMuSigSignatures_RESTInterface : GxGenericCollectionItem<SdtMuSigSignatures>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSigSignatures_RESTInterface( ) : base()
		{	
		}

		public SdtMuSigSignatures_RESTInterface( SdtMuSigSignatures psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="id", Order=0)]
		public Guid gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="description", Order=1)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("signedDateTime")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="signedDateTime", Order=2)]
		public  string gxTpr_Signeddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Signeddatetime,context);

			}
			set { 
				sdt.gxTpr_Signeddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("compleated")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="compleated", Order=3)]
		public bool gxTpr_Compleated
		{
			get { 
				return sdt.gxTpr_Compleated;

			}
			set { 
				sdt.gxTpr_Compleated = value;
			}
		}

		[JsonPropertyName("sendAllCoins")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="sendAllCoins", Order=4)]
		public bool gxTpr_Sendallcoins
		{
			get { 
				return sdt.gxTpr_Sendallcoins;

			}
			set { 
				sdt.gxTpr_Sendallcoins = value;
			}
		}

		[JsonPropertyName("sendCoins")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="sendCoins", Order=5)]
		public  string gxTpr_Sendcoins
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Sendcoins, 16, 8));

			}
			set { 
				sdt.gxTpr_Sendcoins =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("transactionFee")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="transactionFee", Order=6)]
		public  string gxTpr_Transactionfee
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Transactionfee, 16, 8));

			}
			set { 
				sdt.gxTpr_Transactionfee =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="sendTo", Order=7)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("changeTo")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="changeTo", Order=8)]
		public  string gxTpr_Changeto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Changeto);

			}
			set { 
				 sdt.gxTpr_Changeto = value;
			}
		}

		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="referenceGroupId", Order=9)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}

		[JsonPropertyName("senderUserName")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="senderUserName", Order=10)]
		public  string gxTpr_Senderusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Senderusername);

			}
			set { 
				 sdt.gxTpr_Senderusername = value;
			}
		}

		[JsonPropertyName("senderSignature")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="senderSignature", Order=11)]
		public  string gxTpr_Sendersignature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendersignature);

			}
			set { 
				 sdt.gxTpr_Sendersignature = value;
			}
		}

		[JsonPropertyName("transactions")]
		[JsonPropertyOrder(12)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="transactions", Order=12, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory_RESTInterface> gxTpr_Transactions
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Transactions_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory_RESTInterface>(sdt.gxTpr_Transactions);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Transactions);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSigSignatures sdt
		{
			get { 
				return (SdtMuSigSignatures)Sdt;
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
				sdt = new SdtMuSigSignatures() ;
			}
		}
	}
	#endregion
}