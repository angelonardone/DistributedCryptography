/*
				   File: type_SdtMuSig2Room
			Description: MuSig2Room
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
	[XmlRoot(ElementName="MuSig2Room")]
	[XmlType(TypeName="MuSig2Room" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2Room : GxUserType
	{
		public SdtMuSig2Room( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2Room_Initiator = "";

			gxTv_SdtMuSig2Room_Description = "";

			gxTv_SdtMuSig2Room_Sendto = "";

			gxTv_SdtMuSig2Room_State = "";

			gxTv_SdtMuSig2Room_Present = "";

			gxTv_SdtMuSig2Room_Sessionjson = "";

			gxTv_SdtMuSig2Room_Sessionid = "";

			gxTv_SdtMuSig2Room_Signers = "";

			gxTv_SdtMuSig2Room_Noncesmap = "";

			gxTv_SdtMuSig2Room_Partialsmap = "";

			gxTv_SdtMuSig2Room_Txid = "";

			gxTv_SdtMuSig2Room_Info = "";

			gxTv_SdtMuSig2Room_Chatlog = "";

		}

		public SdtMuSig2Room(IGxContext context)
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
			AddObjectProperty("roomId", gxTpr_Roomid, false);


			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);


			AddObjectProperty("initiator", gxTpr_Initiator, false);


			AddObjectProperty("description", gxTpr_Description, false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("amountBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amountbtc, 16, 8)), false);


			AddObjectProperty("sendAll", gxTpr_Sendall, false);


			AddObjectProperty("createdUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Createdunix, 18, 0)), false);


			AddObjectProperty("gatherDeadlineUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Gatherdeadlineunix, 18, 0)), false);


			AddObjectProperty("signDeadlineUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Signdeadlineunix, 18, 0)), false);


			AddObjectProperty("state", gxTpr_State, false);


			AddObjectProperty("present", gxTpr_Present, false);


			AddObjectProperty("sessionJson", gxTpr_Sessionjson, false);


			AddObjectProperty("sessionId", gxTpr_Sessionid, false);


			AddObjectProperty("signers", gxTpr_Signers, false);


			AddObjectProperty("noncesMap", gxTpr_Noncesmap, false);


			AddObjectProperty("partialsMap", gxTpr_Partialsmap, false);


			AddObjectProperty("feeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Feebtc, 16, 8)), false);


			AddObjectProperty("vsize", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Vsize, 18, 0)), false);


			AddObjectProperty("txId", gxTpr_Txid, false);


			AddObjectProperty("info", gxTpr_Info, false);


			AddObjectProperty("lastUpdateUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Lastupdateunix, 18, 0)), false);


			AddObjectProperty("chatLog", gxTpr_Chatlog, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="roomId")]
		[XmlElement(ElementName="roomId")]
		public Guid gxTpr_Roomid
		{
			get {
				return gxTv_SdtMuSig2Room_Roomid; 
			}
			set {
				gxTv_SdtMuSig2Room_Roomid = value;
				SetDirty("Roomid");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtMuSig2Room_Referencegroupid; 
			}
			set {
				gxTv_SdtMuSig2Room_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="initiator")]
		[XmlElement(ElementName="initiator")]
		public string gxTpr_Initiator
		{
			get {
				return gxTv_SdtMuSig2Room_Initiator; 
			}
			set {
				gxTv_SdtMuSig2Room_Initiator = value;
				SetDirty("Initiator");
			}
		}




		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtMuSig2Room_Description; 
			}
			set {
				gxTv_SdtMuSig2Room_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtMuSig2Room_Sendto; 
			}
			set {
				gxTv_SdtMuSig2Room_Sendto = value;
				SetDirty("Sendto");
			}
		}



		[SoapElement(ElementName="amountBtc")]
		[XmlElement(ElementName="amountBtc")]
		public string gxTpr_Amountbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2Room_Amountbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2Room_Amountbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtMuSig2Room_Amountbtc; 
			}
			set {
				gxTv_SdtMuSig2Room_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}




		[SoapElement(ElementName="sendAll")]
		[XmlElement(ElementName="sendAll")]
		public bool gxTpr_Sendall
		{
			get {
				return gxTv_SdtMuSig2Room_Sendall; 
			}
			set {
				gxTv_SdtMuSig2Room_Sendall = value;
				SetDirty("Sendall");
			}
		}




		[SoapElement(ElementName="createdUnix")]
		[XmlElement(ElementName="createdUnix")]
		public long gxTpr_Createdunix
		{
			get {
				return gxTv_SdtMuSig2Room_Createdunix; 
			}
			set {
				gxTv_SdtMuSig2Room_Createdunix = value;
				SetDirty("Createdunix");
			}
		}




		[SoapElement(ElementName="gatherDeadlineUnix")]
		[XmlElement(ElementName="gatherDeadlineUnix")]
		public long gxTpr_Gatherdeadlineunix
		{
			get {
				return gxTv_SdtMuSig2Room_Gatherdeadlineunix; 
			}
			set {
				gxTv_SdtMuSig2Room_Gatherdeadlineunix = value;
				SetDirty("Gatherdeadlineunix");
			}
		}




		[SoapElement(ElementName="signDeadlineUnix")]
		[XmlElement(ElementName="signDeadlineUnix")]
		public long gxTpr_Signdeadlineunix
		{
			get {
				return gxTv_SdtMuSig2Room_Signdeadlineunix; 
			}
			set {
				gxTv_SdtMuSig2Room_Signdeadlineunix = value;
				SetDirty("Signdeadlineunix");
			}
		}




		[SoapElement(ElementName="state")]
		[XmlElement(ElementName="state")]
		public string gxTpr_State
		{
			get {
				return gxTv_SdtMuSig2Room_State; 
			}
			set {
				gxTv_SdtMuSig2Room_State = value;
				SetDirty("State");
			}
		}




		[SoapElement(ElementName="present")]
		[XmlElement(ElementName="present")]
		public string gxTpr_Present
		{
			get {
				return gxTv_SdtMuSig2Room_Present; 
			}
			set {
				gxTv_SdtMuSig2Room_Present = value;
				SetDirty("Present");
			}
		}




		[SoapElement(ElementName="sessionJson")]
		[XmlElement(ElementName="sessionJson")]
		public string gxTpr_Sessionjson
		{
			get {
				return gxTv_SdtMuSig2Room_Sessionjson; 
			}
			set {
				gxTv_SdtMuSig2Room_Sessionjson = value;
				SetDirty("Sessionjson");
			}
		}




		[SoapElement(ElementName="sessionId")]
		[XmlElement(ElementName="sessionId")]
		public string gxTpr_Sessionid
		{
			get {
				return gxTv_SdtMuSig2Room_Sessionid; 
			}
			set {
				gxTv_SdtMuSig2Room_Sessionid = value;
				SetDirty("Sessionid");
			}
		}




		[SoapElement(ElementName="signers")]
		[XmlElement(ElementName="signers")]
		public string gxTpr_Signers
		{
			get {
				return gxTv_SdtMuSig2Room_Signers; 
			}
			set {
				gxTv_SdtMuSig2Room_Signers = value;
				SetDirty("Signers");
			}
		}




		[SoapElement(ElementName="noncesMap")]
		[XmlElement(ElementName="noncesMap")]
		public string gxTpr_Noncesmap
		{
			get {
				return gxTv_SdtMuSig2Room_Noncesmap; 
			}
			set {
				gxTv_SdtMuSig2Room_Noncesmap = value;
				SetDirty("Noncesmap");
			}
		}




		[SoapElement(ElementName="partialsMap")]
		[XmlElement(ElementName="partialsMap")]
		public string gxTpr_Partialsmap
		{
			get {
				return gxTv_SdtMuSig2Room_Partialsmap; 
			}
			set {
				gxTv_SdtMuSig2Room_Partialsmap = value;
				SetDirty("Partialsmap");
			}
		}



		[SoapElement(ElementName="feeBtc")]
		[XmlElement(ElementName="feeBtc")]
		public string gxTpr_Feebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2Room_Feebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2Room_Feebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feebtc
		{
			get {
				return gxTv_SdtMuSig2Room_Feebtc; 
			}
			set {
				gxTv_SdtMuSig2Room_Feebtc = value;
				SetDirty("Feebtc");
			}
		}




		[SoapElement(ElementName="vsize")]
		[XmlElement(ElementName="vsize")]
		public long gxTpr_Vsize
		{
			get {
				return gxTv_SdtMuSig2Room_Vsize; 
			}
			set {
				gxTv_SdtMuSig2Room_Vsize = value;
				SetDirty("Vsize");
			}
		}




		[SoapElement(ElementName="txId")]
		[XmlElement(ElementName="txId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtMuSig2Room_Txid; 
			}
			set {
				gxTv_SdtMuSig2Room_Txid = value;
				SetDirty("Txid");
			}
		}




		[SoapElement(ElementName="info")]
		[XmlElement(ElementName="info")]
		public string gxTpr_Info
		{
			get {
				return gxTv_SdtMuSig2Room_Info; 
			}
			set {
				gxTv_SdtMuSig2Room_Info = value;
				SetDirty("Info");
			}
		}




		[SoapElement(ElementName="lastUpdateUnix")]
		[XmlElement(ElementName="lastUpdateUnix")]
		public long gxTpr_Lastupdateunix
		{
			get {
				return gxTv_SdtMuSig2Room_Lastupdateunix; 
			}
			set {
				gxTv_SdtMuSig2Room_Lastupdateunix = value;
				SetDirty("Lastupdateunix");
			}
		}




		[SoapElement(ElementName="chatLog")]
		[XmlElement(ElementName="chatLog")]
		public string gxTpr_Chatlog
		{
			get {
				return gxTv_SdtMuSig2Room_Chatlog; 
			}
			set {
				gxTv_SdtMuSig2Room_Chatlog = value;
				SetDirty("Chatlog");
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
			gxTv_SdtMuSig2Room_Initiator = "";
			gxTv_SdtMuSig2Room_Description = "";
			gxTv_SdtMuSig2Room_Sendto = "";





			gxTv_SdtMuSig2Room_State = "";
			gxTv_SdtMuSig2Room_Present = "";
			gxTv_SdtMuSig2Room_Sessionjson = "";
			gxTv_SdtMuSig2Room_Sessionid = "";
			gxTv_SdtMuSig2Room_Signers = "";
			gxTv_SdtMuSig2Room_Noncesmap = "";
			gxTv_SdtMuSig2Room_Partialsmap = "";


			gxTv_SdtMuSig2Room_Txid = "";
			gxTv_SdtMuSig2Room_Info = "";

			gxTv_SdtMuSig2Room_Chatlog = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtMuSig2Room_Roomid;
		 

		protected Guid gxTv_SdtMuSig2Room_Referencegroupid;
		 

		protected string gxTv_SdtMuSig2Room_Initiator;
		 

		protected string gxTv_SdtMuSig2Room_Description;
		 

		protected string gxTv_SdtMuSig2Room_Sendto;
		 

		protected decimal gxTv_SdtMuSig2Room_Amountbtc;
		 

		protected bool gxTv_SdtMuSig2Room_Sendall;
		 

		protected long gxTv_SdtMuSig2Room_Createdunix;
		 

		protected long gxTv_SdtMuSig2Room_Gatherdeadlineunix;
		 

		protected long gxTv_SdtMuSig2Room_Signdeadlineunix;
		 

		protected string gxTv_SdtMuSig2Room_State;
		 

		protected string gxTv_SdtMuSig2Room_Present;
		 

		protected string gxTv_SdtMuSig2Room_Sessionjson;
		 

		protected string gxTv_SdtMuSig2Room_Sessionid;
		 

		protected string gxTv_SdtMuSig2Room_Signers;
		 

		protected string gxTv_SdtMuSig2Room_Noncesmap;
		 

		protected string gxTv_SdtMuSig2Room_Partialsmap;
		 

		protected decimal gxTv_SdtMuSig2Room_Feebtc;
		 

		protected long gxTv_SdtMuSig2Room_Vsize;
		 

		protected string gxTv_SdtMuSig2Room_Txid;
		 

		protected string gxTv_SdtMuSig2Room_Info;
		 

		protected long gxTv_SdtMuSig2Room_Lastupdateunix;
		 

		protected string gxTv_SdtMuSig2Room_Chatlog;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2Room", Namespace="distributedcryptography")]
	public class SdtMuSig2Room_RESTInterface : GxGenericCollectionItem<SdtMuSig2Room>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2Room_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2Room_RESTInterface( SdtMuSig2Room psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("roomId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="roomId", Order=0)]
		public Guid gxTpr_Roomid
		{
			get { 
				return sdt.gxTpr_Roomid;

			}
			set { 
				sdt.gxTpr_Roomid = value;
			}
		}

		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="referenceGroupId", Order=1)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}

		[JsonPropertyName("initiator")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="initiator", Order=2)]
		public  string gxTpr_Initiator
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Initiator);

			}
			set { 
				 sdt.gxTpr_Initiator = value;
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="description", Order=3)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="sendTo", Order=4)]
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
		[JsonPropertyOrder(5)]
		[DataMember(Name="amountBtc", Order=5)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amountbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Amountbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sendAll")]
		[JsonPropertyOrder(6)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="sendAll", Order=6)]
		public bool gxTpr_Sendall
		{
			get { 
				return sdt.gxTpr_Sendall;

			}
			set { 
				sdt.gxTpr_Sendall = value;
			}
		}

		[JsonPropertyName("createdUnix")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="createdUnix", Order=7)]
		public  string gxTpr_Createdunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Createdunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Createdunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("gatherDeadlineUnix")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="gatherDeadlineUnix", Order=8)]
		public  string gxTpr_Gatherdeadlineunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Gatherdeadlineunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Gatherdeadlineunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("signDeadlineUnix")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="signDeadlineUnix", Order=9)]
		public  string gxTpr_Signdeadlineunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Signdeadlineunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Signdeadlineunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("state")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="state", Order=10)]
		public  string gxTpr_State
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_State);

			}
			set { 
				 sdt.gxTpr_State = value;
			}
		}

		[JsonPropertyName("present")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="present", Order=11)]
		public  string gxTpr_Present
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Present);

			}
			set { 
				 sdt.gxTpr_Present = value;
			}
		}

		[JsonPropertyName("sessionJson")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="sessionJson", Order=12)]
		public  string gxTpr_Sessionjson
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sessionjson);

			}
			set { 
				 sdt.gxTpr_Sessionjson = value;
			}
		}

		[JsonPropertyName("sessionId")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="sessionId", Order=13)]
		public  string gxTpr_Sessionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sessionid);

			}
			set { 
				 sdt.gxTpr_Sessionid = value;
			}
		}

		[JsonPropertyName("signers")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="signers", Order=14)]
		public  string gxTpr_Signers
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signers);

			}
			set { 
				 sdt.gxTpr_Signers = value;
			}
		}

		[JsonPropertyName("noncesMap")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="noncesMap", Order=15)]
		public  string gxTpr_Noncesmap
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Noncesmap);

			}
			set { 
				 sdt.gxTpr_Noncesmap = value;
			}
		}

		[JsonPropertyName("partialsMap")]
		[JsonPropertyOrder(16)]
		[DataMember(Name="partialsMap", Order=16)]
		public  string gxTpr_Partialsmap
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Partialsmap);

			}
			set { 
				 sdt.gxTpr_Partialsmap = value;
			}
		}

		[JsonPropertyName("feeBtc")]
		[JsonPropertyOrder(17)]
		[DataMember(Name="feeBtc", Order=17)]
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
		[JsonPropertyOrder(18)]
		[DataMember(Name="vsize", Order=18)]
		public  string gxTpr_Vsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vsize, 18, 0));

			}
			set { 
				sdt.gxTpr_Vsize = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("txId")]
		[JsonPropertyOrder(19)]
		[DataMember(Name="txId", Order=19)]
		public  string gxTpr_Txid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txid);

			}
			set { 
				 sdt.gxTpr_Txid = value;
			}
		}

		[JsonPropertyName("info")]
		[JsonPropertyOrder(20)]
		[DataMember(Name="info", Order=20)]
		public  string gxTpr_Info
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Info);

			}
			set { 
				 sdt.gxTpr_Info = value;
			}
		}

		[JsonPropertyName("lastUpdateUnix")]
		[JsonPropertyOrder(21)]
		[DataMember(Name="lastUpdateUnix", Order=21)]
		public  string gxTpr_Lastupdateunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Lastupdateunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Lastupdateunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("chatLog")]
		[JsonPropertyOrder(22)]
		[DataMember(Name="chatLog", Order=22)]
		public  string gxTpr_Chatlog
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chatlog);

			}
			set { 
				 sdt.gxTpr_Chatlog = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2Room sdt
		{
			get { 
				return (SdtMuSig2Room)Sdt;
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
				sdt = new SdtMuSig2Room() ;
			}
		}
	}
	#endregion
}