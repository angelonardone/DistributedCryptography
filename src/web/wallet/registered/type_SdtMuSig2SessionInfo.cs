/*
				   File: type_SdtMuSig2SessionInfo
			Description: MuSig2SessionInfo
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
	[XmlRoot(ElementName="MuSig2SessionInfo")]
	[XmlType(TypeName="MuSig2SessionInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2SessionInfo : GxUserType
	{
		public SdtMuSig2SessionInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2SessionInfo_Error = "";

			gxTv_SdtMuSig2SessionInfo_Session = "";

			gxTv_SdtMuSig2SessionInfo_Sessionid = "";

			gxTv_SdtMuSig2SessionInfo_Path = "";

			gxTv_SdtMuSig2SessionInfo_Signers = "";

			gxTv_SdtMuSig2SessionInfo_Me = "";

			gxTv_SdtMuSig2SessionInfo_Sendto = "";

			gxTv_SdtMuSig2SessionInfo_Changeto = "";


		}

		public SdtMuSig2SessionInfo(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("session", gxTpr_Session, false);


			AddObjectProperty("sessionId", gxTpr_Sessionid, false);


			AddObjectProperty("path", gxTpr_Path, false);


			AddObjectProperty("signers", gxTpr_Signers, false);


			AddObjectProperty("required", gxTpr_Required, false);


			AddObjectProperty("iAmSigner", gxTpr_Iamsigner, false);


			AddObjectProperty("me", gxTpr_Me, false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("amountBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amountbtc, 16, 8)), false);


			AddObjectProperty("changeTo", gxTpr_Changeto, false);


			AddObjectProperty("changeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Changebtc, 16, 8)), false);


			AddObjectProperty("totalInBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Totalinbtc, 16, 8)), false);


			AddObjectProperty("feeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Feebtc, 16, 8)), false);


			AddObjectProperty("vsize", gxTpr_Vsize, false);


			AddObjectProperty("inputs", gxTpr_Inputs, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Success; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Error; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="session")]
		[XmlElement(ElementName="session")]
		public string gxTpr_Session
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Session; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Session = value;
				SetDirty("Session");
			}
		}




		[SoapElement(ElementName="sessionId")]
		[XmlElement(ElementName="sessionId")]
		public string gxTpr_Sessionid
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Sessionid; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Sessionid = value;
				SetDirty("Sessionid");
			}
		}




		[SoapElement(ElementName="path")]
		[XmlElement(ElementName="path")]
		public string gxTpr_Path
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Path; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Path = value;
				SetDirty("Path");
			}
		}




		[SoapElement(ElementName="signers")]
		[XmlElement(ElementName="signers")]
		public string gxTpr_Signers
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Signers; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Signers = value;
				SetDirty("Signers");
			}
		}




		[SoapElement(ElementName="required")]
		[XmlElement(ElementName="required")]
		public short gxTpr_Required
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Required; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Required = value;
				SetDirty("Required");
			}
		}




		[SoapElement(ElementName="iAmSigner")]
		[XmlElement(ElementName="iAmSigner")]
		public bool gxTpr_Iamsigner
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Iamsigner; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Iamsigner = value;
				SetDirty("Iamsigner");
			}
		}




		[SoapElement(ElementName="me")]
		[XmlElement(ElementName="me")]
		public string gxTpr_Me
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Me; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Me = value;
				SetDirty("Me");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Sendto; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Sendto = value;
				SetDirty("Sendto");
			}
		}



		[SoapElement(ElementName="amountBtc")]
		[XmlElement(ElementName="amountBtc")]
		public string gxTpr_Amountbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2SessionInfo_Amountbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Amountbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Amountbtc; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}




		[SoapElement(ElementName="changeTo")]
		[XmlElement(ElementName="changeTo")]
		public string gxTpr_Changeto
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Changeto; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Changeto = value;
				SetDirty("Changeto");
			}
		}



		[SoapElement(ElementName="changeBtc")]
		[XmlElement(ElementName="changeBtc")]
		public string gxTpr_Changebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2SessionInfo_Changebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Changebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Changebtc
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Changebtc; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Changebtc = value;
				SetDirty("Changebtc");
			}
		}



		[SoapElement(ElementName="totalInBtc")]
		[XmlElement(ElementName="totalInBtc")]
		public string gxTpr_Totalinbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2SessionInfo_Totalinbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Totalinbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Totalinbtc
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Totalinbtc; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Totalinbtc = value;
				SetDirty("Totalinbtc");
			}
		}



		[SoapElement(ElementName="feeBtc")]
		[XmlElement(ElementName="feeBtc")]
		public string gxTpr_Feebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2SessionInfo_Feebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Feebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feebtc
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Feebtc; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Feebtc = value;
				SetDirty("Feebtc");
			}
		}




		[SoapElement(ElementName="vsize")]
		[XmlElement(ElementName="vsize")]
		public long gxTpr_Vsize
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Vsize; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Vsize = value;
				SetDirty("Vsize");
			}
		}




		[SoapElement(ElementName="inputs")]
		[XmlElement(ElementName="inputs")]
		public short gxTpr_Inputs
		{
			get {
				return gxTv_SdtMuSig2SessionInfo_Inputs; 
			}
			set {
				gxTv_SdtMuSig2SessionInfo_Inputs = value;
				SetDirty("Inputs");
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
			gxTv_SdtMuSig2SessionInfo_Error = "";
			gxTv_SdtMuSig2SessionInfo_Session = "";
			gxTv_SdtMuSig2SessionInfo_Sessionid = "";
			gxTv_SdtMuSig2SessionInfo_Path = "";
			gxTv_SdtMuSig2SessionInfo_Signers = "";


			gxTv_SdtMuSig2SessionInfo_Me = "";
			gxTv_SdtMuSig2SessionInfo_Sendto = "";

			gxTv_SdtMuSig2SessionInfo_Changeto = "";





			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtMuSig2SessionInfo_Success;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Error;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Session;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Sessionid;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Path;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Signers;
		 

		protected short gxTv_SdtMuSig2SessionInfo_Required;
		 

		protected bool gxTv_SdtMuSig2SessionInfo_Iamsigner;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Me;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Sendto;
		 

		protected decimal gxTv_SdtMuSig2SessionInfo_Amountbtc;
		 

		protected string gxTv_SdtMuSig2SessionInfo_Changeto;
		 

		protected decimal gxTv_SdtMuSig2SessionInfo_Changebtc;
		 

		protected decimal gxTv_SdtMuSig2SessionInfo_Totalinbtc;
		 

		protected decimal gxTv_SdtMuSig2SessionInfo_Feebtc;
		 

		protected long gxTv_SdtMuSig2SessionInfo_Vsize;
		 

		protected short gxTv_SdtMuSig2SessionInfo_Inputs;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2SessionInfo", Namespace="distributedcryptography")]
	public class SdtMuSig2SessionInfo_RESTInterface : GxGenericCollectionItem<SdtMuSig2SessionInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2SessionInfo_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2SessionInfo_RESTInterface( SdtMuSig2SessionInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("session")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="session", Order=2)]
		public  string gxTpr_Session
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Session);

			}
			set { 
				 sdt.gxTpr_Session = value;
			}
		}

		[JsonPropertyName("sessionId")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="sessionId", Order=3)]
		public  string gxTpr_Sessionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sessionid);

			}
			set { 
				 sdt.gxTpr_Sessionid = value;
			}
		}

		[JsonPropertyName("path")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="path", Order=4)]
		public  string gxTpr_Path
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Path);

			}
			set { 
				 sdt.gxTpr_Path = value;
			}
		}

		[JsonPropertyName("signers")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="signers", Order=5)]
		public  string gxTpr_Signers
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signers);

			}
			set { 
				 sdt.gxTpr_Signers = value;
			}
		}

		[JsonPropertyName("required")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="required", Order=6)]
		public short gxTpr_Required
		{
			get { 
				return sdt.gxTpr_Required;

			}
			set { 
				sdt.gxTpr_Required = value;
			}
		}

		[JsonPropertyName("iAmSigner")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="iAmSigner", Order=7)]
		public bool gxTpr_Iamsigner
		{
			get { 
				return sdt.gxTpr_Iamsigner;

			}
			set { 
				sdt.gxTpr_Iamsigner = value;
			}
		}

		[JsonPropertyName("me")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="me", Order=8)]
		public  string gxTpr_Me
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Me);

			}
			set { 
				 sdt.gxTpr_Me = value;
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="sendTo", Order=9)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("amountBtc")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="amountBtc", Order=10)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amountbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Amountbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("changeTo")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="changeTo", Order=11)]
		public  string gxTpr_Changeto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Changeto);

			}
			set { 
				 sdt.gxTpr_Changeto = value;
			}
		}

		[JsonPropertyName("changeBtc")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="changeBtc", Order=12)]
		public  string gxTpr_Changebtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Changebtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Changebtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("totalInBtc")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="totalInBtc", Order=13)]
		public  string gxTpr_Totalinbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Totalinbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Totalinbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("feeBtc")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="feeBtc", Order=14)]
		public  string gxTpr_Feebtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Feebtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Feebtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("vsize")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="vsize", Order=15)]
		public  string gxTpr_Vsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vsize, 10, 0));

			}
			set { 
				sdt.gxTpr_Vsize = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("inputs")]
		[JsonPropertyOrder(16)]
		[DataMember(Name="inputs", Order=16)]
		public short gxTpr_Inputs
		{
			get { 
				return sdt.gxTpr_Inputs;

			}
			set { 
				sdt.gxTpr_Inputs = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2SessionInfo sdt
		{
			get { 
				return (SdtMuSig2SessionInfo)Sdt;
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
				sdt = new SdtMuSig2SessionInfo() ;
			}
		}
	}
	#endregion
}